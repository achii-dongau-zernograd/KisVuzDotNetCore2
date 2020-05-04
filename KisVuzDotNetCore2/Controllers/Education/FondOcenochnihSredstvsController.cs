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
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class FondOcenochnihSredstvsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public FondOcenochnihSredstvsController(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: FondOcenochnihSredstvs
        public async Task<IActionResult> Index(int? EduPlanId)
        {
            var fondOcenochnihSredstvsAll = _context.FondOcenochnihSredstvs
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
                return View(await fondOcenochnihSredstvsAll.ToListAsync());
            }
            else
            {
                var fondOcenochnihSredstvsAllForEduPlanId = await fondOcenochnihSredstvsAll
                    .Where(a => a.Discipline.BlokDisciplChast.BlokDiscipl.EduPlanId == EduPlanId)
                    .ToListAsync();
                ViewBag.EduPlanId = EduPlanId;
                return View(fondOcenochnihSredstvsAllForEduPlanId);
            }            
        }
               

        // GET: FondOcenochnihSredstvs/Create
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

        // POST: FondOcenochnihSredstvs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FondOcenochnihSredstvId,DisciplineId,FileModelId")] FondOcenochnihSredstv fondOcenochnihSredstv, IFormFile uploadedFile, int? EduPlanId)
        {
            if (ModelState.IsValid && uploadedFile != null)
            {
                FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, "Фонд оценочных средств", FileDataTypeEnum.FondOcenochnihSredstv);
                fondOcenochnihSredstv.FileModelId = fileModel.Id;
                _context.Add(fondOcenochnihSredstv);
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

            return View(fondOcenochnihSredstv);
        }

        // GET: FondOcenochnihSredstvs/Edit/5
        public async Task<IActionResult> Edit(int? id, int? EduPlanId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fondOcenochnihSredstv = await _context.FondOcenochnihSredstvs
                .Include(m => m.FileModel)
                .SingleOrDefaultAsync(m => m.FondOcenochnihSredstvId == id);
            if (fondOcenochnihSredstv == null)
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
            return View(fondOcenochnihSredstv);
        }

        // POST: FondOcenochnihSredstvs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FondOcenochnihSredstvId,DisciplineId,FileModelId")] FondOcenochnihSredstv fondOcenochnihSredstv,
            IFormFile uploadedFile,
            int? EduPlanId)
        {
            if (id != fondOcenochnihSredstv.FondOcenochnihSredstvId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, "Фонд оценочных средств", FileDataTypeEnum.FondOcenochnihSredstv);
                    await _context.SaveChangesAsync();
                    int? fileToRemoveId = fondOcenochnihSredstv.FileModelId;
                    fondOcenochnihSredstv.FileModelId = fileModel.Id;
                    await _context.SaveChangesAsync();
                    KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                }

                try
                {
                    _context.Update(fondOcenochnihSredstv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FondOcenochnihSredstvExists(fondOcenochnihSredstv.FondOcenochnihSredstvId))
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
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "DisciplineId", "DisciplineId", fondOcenochnihSredstv.DisciplineId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", fondOcenochnihSredstv.FileModelId);
            return View(fondOcenochnihSredstv);
        }

        // GET: FondOcenochnihSredstvs/Delete/5
        public async Task<IActionResult> Delete(int? id, int? EduPlanId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fondOcenochnihSredstv = await _context.FondOcenochnihSredstvs
                .Include(f => f.Discipline)
                .Include(f => f.FileModel)
                .SingleOrDefaultAsync(m => m.FondOcenochnihSredstvId == id);
            if (fondOcenochnihSredstv == null)
            {
                return NotFound();
            }

            ViewBag.EduPlanId = EduPlanId;
            return View(fondOcenochnihSredstv);
        }

        // POST: FondOcenochnihSredstvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? EduPlanId)
        {
            var fondOcenochnihSredstv = await _context.FondOcenochnihSredstvs.SingleOrDefaultAsync(m => m.FondOcenochnihSredstvId == id);
            _context.FondOcenochnihSredstvs.Remove(fondOcenochnihSredstv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { EduPlanId });
        }

        private bool FondOcenochnihSredstvExists(int id)
        {
            return _context.FondOcenochnihSredstvs.Any(e => e.FondOcenochnihSredstvId == id);
        }
    }
}
