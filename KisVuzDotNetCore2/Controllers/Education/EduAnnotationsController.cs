using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers.Education
{
    public class EduAnnotationsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduAnnotationsController(AppIdentityDBContext context)
        {
            _context = context;
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

        // GET: EduAnnotations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduAnnotation = await _context.EduAnnotations
                .Include(e => e.Discipline)
                .Include(e => e.FileModel)
                .SingleOrDefaultAsync(m => m.EduAnnotationId == id);
            if (eduAnnotation == null)
            {
                return NotFound();
            }

            return View(eduAnnotation);
        }

        // GET: EduAnnotations/Create
        public IActionResult Create()
        {
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "DisciplineId", "DisciplineId");
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id");
            return View();
        }

        // POST: EduAnnotations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduAnnotationId,DisciplineId,FileModelId")] EduAnnotation eduAnnotation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduAnnotation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "DisciplineId", "DisciplineId", eduAnnotation.DisciplineId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", eduAnnotation.FileModelId);
            return View(eduAnnotation);
        }

        // GET: EduAnnotations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduAnnotation = await _context.EduAnnotations.SingleOrDefaultAsync(m => m.EduAnnotationId == id);
            if (eduAnnotation == null)
            {
                return NotFound();
            }
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "DisciplineId", "DisciplineId", eduAnnotation.DisciplineId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", eduAnnotation.FileModelId);
            return View(eduAnnotation);
        }

        // POST: EduAnnotations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduAnnotationId,DisciplineId,FileModelId")] EduAnnotation eduAnnotation)
        {
            if (id != eduAnnotation.EduAnnotationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "DisciplineId", "DisciplineId", eduAnnotation.DisciplineId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", eduAnnotation.FileModelId);
            return View(eduAnnotation);
        }

        // GET: EduAnnotations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduAnnotation = await _context.EduAnnotations
                .Include(e => e.Discipline)
                .Include(e => e.FileModel)
                .SingleOrDefaultAsync(m => m.EduAnnotationId == id);
            if (eduAnnotation == null)
            {
                return NotFound();
            }

            return View(eduAnnotation);
        }

        // POST: EduAnnotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduAnnotation = await _context.EduAnnotations.SingleOrDefaultAsync(m => m.EduAnnotationId == id);
            _context.EduAnnotations.Remove(eduAnnotation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduAnnotationExists(int id)
        {
            return _context.EduAnnotations.Any(e => e.EduAnnotationId == id);
        }
    }
}
