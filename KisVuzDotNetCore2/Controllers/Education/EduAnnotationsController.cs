using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Education
{
    [Authorize(Roles = "Администраторы")]
    public class EduAnnotationsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public EduAnnotationsController(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: EduAnnotations
        public async Task<IActionResult> Index(int? EduPlanId)
        {
            var eduAnnotationsAll= _context.EduAnnotations
                .Include(e => e.Discipline.BlokDisciplChast.BlokDiscipl.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.Discipline.DisciplineName)
                .Include(e => e.FileModel);

            if (EduPlanId==null)
            {
                var eduLevels = await _context.EduLevels
                    .Include(l => l.EduUgses)
                        .ThenInclude(u => u.EduNapravls)
                            .ThenInclude(n => n.EduProfiles)
                                .ThenInclude(p => p.EduPlans)                                
                                    .ThenInclude(plan=>plan.EduPlanEduYearBeginningTrainings)
                                        .ThenInclude(year => year.EduYearBeginningTraining)
                     .Include(l => l.EduUgses)
                        .ThenInclude(u => u.EduNapravls)
                            .ThenInclude(n => n.EduProfiles)
                                .ThenInclude(p => p.EduPlans)
                                    .ThenInclude(plan => plan.EduPlanPdf)
                     .ToListAsync();
                
                ViewData["eduLevels"] = eduLevels;
                return View(await eduAnnotationsAll.ToListAsync());
            }
            else
            {
                var eduAnnotationsForEduPlanId = await eduAnnotationsAll
                    .Where(a=>a.Discipline.BlokDisciplChast.BlokDiscipl.EduPlanId == EduPlanId)
                    .ToListAsync();
                ViewBag.EduPlanId = EduPlanId;
                return View(eduAnnotationsForEduPlanId);
            }
            
        }                

        // GET: EduAnnotations/Create
        public IActionResult Create(int? EduPlanId)
        {
            if(EduPlanId==null)
            {
                return RedirectToAction(nameof(Index));
            }
            var eduPlans = _context.EduPlans                
                .Where(p => p.EduPlanId == EduPlanId)
                .Include(p=>p.BlokDiscipl)
                    .ThenInclude(bd=>bd.BlokDisciplChast)
                        .ThenInclude(bdc => bdc.Disciplines)
                            .ThenInclude(d => d.DisciplineName);
            List<Discipline> disciplinesByEduPlan=new List<Discipline>();
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

        // POST: EduAnnotations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduAnnotationId,DisciplineId,FileModelId")] EduAnnotation eduAnnotation, IFormFile uploadedFile, int? EduPlanId)
        {
            if (ModelState.IsValid && uploadedFile!=null)
            {
                FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, "Аннотация", FileDataTypeEnum.Annotation);
                eduAnnotation.FileModelId = fileModel.Id;
                _context.Add(eduAnnotation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { EduPlanId });
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

            return View(eduAnnotation);
        }

        // GET: EduAnnotations/Edit/5
        public async Task<IActionResult> Edit(int? id, int? EduPlanId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduAnnotation = await _context.EduAnnotations
                .Include(a=>a.FileModel)
                .SingleOrDefaultAsync(a => a.EduAnnotationId == id);
            if (eduAnnotation == null)
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

            return View(eduAnnotation);
        }

        // POST: EduAnnotations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("EduAnnotationId,DisciplineId,FileModelId")] EduAnnotation eduAnnotation,
            IFormFile uploadedFile,
            int? EduPlanId)
        {
            if (id != eduAnnotation.EduAnnotationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, "Аннотация", FileDataTypeEnum.Annotation);
                    await _context.SaveChangesAsync();
                    int? fileToRemoveId = eduAnnotation.FileModelId;
                    eduAnnotation.FileModelId = fileModel.Id;
                    await _context.SaveChangesAsync();
                    KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                }

                try
                {
                    _context.Update(eduAnnotation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduAnnotationExists(eduAnnotation.EduAnnotationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),new { EduPlanId });
            }
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "DisciplineId", "DisciplineId", eduAnnotation.DisciplineId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", eduAnnotation.FileModelId);
            return View(eduAnnotation);
        }

        // GET: EduAnnotations/Delete/5
        public async Task<IActionResult> Delete(int? id, int? EduPlanId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduAnnotation = await _context.EduAnnotations
                .Include(e => e.Discipline.DisciplineName)
                .Include(e => e.FileModel)
                .SingleOrDefaultAsync(m => m.EduAnnotationId == id);
            if (eduAnnotation == null)
            {
                return NotFound();
            }

            ViewBag.EduPlanId = EduPlanId;
            return View(eduAnnotation);
        }

        // POST: EduAnnotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? EduPlanId)
        {
            var eduAnnotation = await _context.EduAnnotations.SingleOrDefaultAsync(m => m.EduAnnotationId == id);
            _context.EduAnnotations.Remove(eduAnnotation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { EduPlanId });
        }

        private bool EduAnnotationExists(int id)
        {
            return _context.EduAnnotations.Any(e => e.EduAnnotationId == id);
        }
    }
}
