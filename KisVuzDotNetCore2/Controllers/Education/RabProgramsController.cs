using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using Microsoft.AspNetCore.Hosting;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Education
{
    [Authorize(Roles = "Администраторы")]
    public class RabProgramsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public RabProgramsController(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: RabPrograms
        public async Task<IActionResult> Index(int? EduPlanId)
        {
            var eduRabProgramsAll = _context.RabPrograms
                .Include(e => e.Discipline.BlokDisciplChast.BlokDiscipl.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.Discipline.DisciplineName)
                .Include(e => e.FileModel);

            if (EduPlanId == null)
            {
                var eduLevels = await _context.EduLevels
                    .Include(l => l.EduUgses)
                        .ThenInclude(u => u.EduNapravls)
                            .ThenInclude(n => n.EduProfiles)
                                .ThenInclude(p => p.EduPlans)
                                    .ThenInclude(plan => plan.EduPlanEduYearBeginningTrainings)
                                        .ThenInclude(year => year.EduYearBeginningTraining)
                     .Include(l => l.EduUgses)
                        .ThenInclude(u => u.EduNapravls)
                            .ThenInclude(n => n.EduProfiles)
                                .ThenInclude(p => p.EduPlans)
                                    .ThenInclude(plan => plan.EduPlanPdf)
                     .ToListAsync();

                ViewData["eduLevels"] = eduLevels;
                return View(await eduRabProgramsAll.ToListAsync());
            }
            else
            {
                var edueduRabProgramsAllForEduPlanId = await eduRabProgramsAll
                    .Where(a => a.Discipline.BlokDisciplChast.BlokDiscipl.EduPlanId == EduPlanId)
                    .ToListAsync();
                ViewBag.EduPlanId = EduPlanId;
                return View(edueduRabProgramsAllForEduPlanId);
            }
        }
                

        // GET: RabPrograms/Create
        public IActionResult Create(int? EduPlanId)
        {
            if (EduPlanId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var eduPlans = _context.EduPlans
                .Where(p => p.EduPlanId == EduPlanId)
                .Include(p => p.BlokDiscipl)
                    .ThenInclude(bd => bd.BlokDisciplChast)
                        .ThenInclude(bdc => bdc.Disciplines)
                            .ThenInclude(d => d.DisciplineName);
            List<Discipline> disciplinesByEduPlan = new List<Discipline>();
            foreach (var plan in eduPlans)
            {
                foreach (var blokDiscipl in plan.BlokDiscipl)
                {
                    foreach (var blokDisciplChast in blokDiscipl.BlokDisciplChast)
                    {
                        foreach (var discipline in blokDisciplChast.Disciplines)
                        {
                            disciplinesByEduPlan.Add(discipline);
                        }
                    }
                }
            }

            ViewData["DisciplineId"] = new SelectList(disciplinesByEduPlan, "DisciplineId", "DisciplineName.DisciplineNameName");
            ViewBag.EduPlanId = EduPlanId;
            return View();
        }

        // POST: RabPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RabProgramId,DisciplineId,FileModelId")] RabProgram rabProgram, IFormFile uploadedFile, int? EduPlanId)
        {
            if (ModelState.IsValid && uploadedFile != null)
            {
                FileModel fileModel = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Рабочая программа", FileDataTypeEnum.RabProgrammaDisciplini);
                rabProgram.FileModelId = fileModel.Id;
                _context.Add(rabProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { EduPlanId });
            }

            var eduPlans = _context.EduPlans
                .Where(p => p.EduPlanId == EduPlanId)
                .Include(p => p.BlokDiscipl)
                    .ThenInclude(bd => bd.BlokDisciplChast)
                        .ThenInclude(bdc => bdc.Disciplines)
                            .ThenInclude(d => d.DisciplineName);
            List<Discipline> disciplinesByEduPlan = new List<Discipline>();
            foreach (var plan in eduPlans)
            {
                foreach (var blokDiscipl in plan.BlokDiscipl)
                {
                    foreach (var blokDisciplChast in blokDiscipl.BlokDisciplChast)
                    {
                        foreach (var discipline in blokDisciplChast.Disciplines)
                        {
                            disciplinesByEduPlan.Add(discipline);
                        }
                    }
                }
            }

            ViewData["DisciplineId"] = new SelectList(disciplinesByEduPlan, "DisciplineId", "DisciplineName.DisciplineNameName");
            ViewBag.EduPlanId = EduPlanId;

            return View(rabProgram);
        }

        // GET: RabPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id, int? EduPlanId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rabProgram = await _context.RabPrograms
                .Include(rp => rp.FileModel)
                .SingleOrDefaultAsync(m => m.RabProgramId == id);
            if (rabProgram == null)
            {
                return NotFound();
            }
            if (EduPlanId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var eduPlans = _context.EduPlans
                .Where(p => p.EduPlanId == EduPlanId)
                .Include(p => p.BlokDiscipl)
                    .ThenInclude(bd => bd.BlokDisciplChast)
                        .ThenInclude(bdc => bdc.Disciplines)
                            .ThenInclude(d => d.DisciplineName);
            List<Discipline> disciplinesByEduPlan = new List<Discipline>();
            foreach (var plan in eduPlans)
            {
                foreach (var blokDiscipl in plan.BlokDiscipl)
                {
                    foreach (var blokDisciplChast in blokDiscipl.BlokDisciplChast)
                    {
                        foreach (var discipline in blokDisciplChast.Disciplines)
                        {
                            disciplinesByEduPlan.Add(discipline);
                        }
                    }
                }
            }

            ViewData["DisciplineId"] = new SelectList(disciplinesByEduPlan, "DisciplineId", "DisciplineName.DisciplineNameName");
            ViewBag.EduPlanId = EduPlanId;
            return View(rabProgram);
        }

        // POST: RabPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("RabProgramId,DisciplineId,FileModelId")] RabProgram rabProgram,
            IFormFile uploadedFile,
            int? EduPlanId)
        {
            if (id != rabProgram.RabProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    FileModel fileModel = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Рабочая программа", FileDataTypeEnum.RabProgrammaDisciplini);
                    await _context.SaveChangesAsync();
                    int? fileToRemoveId = rabProgram.FileModelId;
                    rabProgram.FileModelId = fileModel.Id;
                    await _context.SaveChangesAsync();
                    Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                }

                try
                {
                    _context.Update(rabProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RabProgramExists(rabProgram.RabProgramId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { EduPlanId });
            }
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "DisciplineId", "DisciplineId", rabProgram.DisciplineId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", rabProgram.FileModelId);
            return View(rabProgram);
        }

        // GET: RabPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id, int? EduPlanId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rabProgram = await _context.RabPrograms
                .Include(r => r.Discipline.DisciplineName)
                .Include(r => r.FileModel)
                .SingleOrDefaultAsync(m => m.RabProgramId == id);
            if (rabProgram == null)
            {
                return NotFound();
            }

            ViewBag.EduPlanId = EduPlanId;
            return View(rabProgram);
        }

        // POST: RabPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? EduPlanId)
        {
            var rabProgram = await _context.RabPrograms.SingleOrDefaultAsync(m => m.RabProgramId == id);
            _context.RabPrograms.Remove(rabProgram);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { EduPlanId });
        }

        private bool RabProgramExists(int id)
        {
            return _context.RabPrograms.Any(e => e.RabProgramId == id);
        }
    }
}
