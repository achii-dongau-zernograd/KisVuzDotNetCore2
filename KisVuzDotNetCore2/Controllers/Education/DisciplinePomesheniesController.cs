using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Education
{
    [Authorize(Roles = "Администраторы")]
    public class DisciplinePomesheniesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public DisciplinePomesheniesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: DisciplinePomeshenies
        public async Task<IActionResult> Index(int? EduPlanId, int? EduYearId)
        {
            if (EduYearId == null)
            {
                return RedirectToAction(nameof(ChooseEduYear));
            }
            if (EduPlanId == null)
            {
                return RedirectToAction(nameof(ChooseEduPlan), new { EduYearId });
            }

            var appIdentityDBContext = _context.DisciplinePomeshenies
                .Where(t => t.EduPlanEduYear.EduYearId == EduYearId && t.EduPlanEduYear.EduPlanId == EduPlanId)
                .Include(d => d.Discipline.DisciplineName)
                .Include(d => d.EduPlanEduYear.EduYear)
                .Include(d => d.Pomeshenie.Korpus);
            ViewBag.EduYearId = EduYearId;
            ViewBag.EduPlanId = EduPlanId;
            return View(await appIdentityDBContext.ToListAsync());
        }

        public async Task<IActionResult> ChooseEduYear()
        {
            var eduYears = await _context.EduYears.ToListAsync();
            ViewData["EduYears"] = new SelectList(eduYears, "EduYearId", "EduYearName");

            return View();
        }

        [HttpPost]
        public IActionResult ChooseEduYear(int? EduYearId)
        {
            if (EduYearId == null)
            {
                return RedirectToAction(nameof(ChooseEduYear));
            }
            return RedirectToAction(nameof(Index), new { EduYearId });
        }

        /// <summary>
        /// Выбор учебного плана, действующего в учебном году EduYearId
        /// </summary>
        /// <param name="EduYearId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ChooseEduPlan(int? EduYearId)
        {
            if (EduYearId == null)
            {
                return RedirectToAction(nameof(ChooseEduYear));
            }

            var eduLevels = await _context.EduLevels
                    .Include(l => l.EduUgses)
                        .ThenInclude(u => u.EduNapravls)
                            .ThenInclude(n => n.EduProfiles)
                                .ThenInclude(p => p.EduPlans)
                                    .ThenInclude(plan => plan.EduPlanEduYears)
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
            foreach (var eduLevel in eduLevels)
            {
                foreach (var eduUgs in eduLevel.EduUgses)
                {
                    foreach (var eduNapravl in eduUgs.EduNapravls)
                    {
                        foreach (var eduProfile in eduNapravl.EduProfiles)
                        {
                            var eduPlansToRemoveFromList = new List<EduPlan>();

                            foreach (var eduPlan in eduProfile.EduPlans)
                            {
                                // Определяем, действует ли учебный план в учебном году EduYearId
                                bool IsActivePlan = false;
                                foreach (var eduPlanEduYear in eduPlan.EduPlanEduYears)
                                {
                                    if (eduPlanEduYear.EduYearId == EduYearId)
                                    {
                                        IsActivePlan = true;
                                    }
                                }
                                if (!IsActivePlan)
                                {
                                    eduPlansToRemoveFromList.Add(eduPlan);
                                }
                            }

                            foreach (var eduPlanToRemoveFromList in eduPlansToRemoveFromList)
                            {
                                eduProfile.EduPlans.Remove(eduPlanToRemoveFromList);
                            }
                        }
                    }
                }
            }

            ViewData["EduYearId"] = EduYearId;
            ViewData["eduLevels"] = eduLevels;
            
            return View();
        }

        [HttpPost]
        public IActionResult ChooseEduPlan(int? EduYearId, int? EduPlanId)
        {
            if (EduYearId == null)
            {
                return RedirectToAction(nameof(ChooseEduYear));
            }
            if (EduPlanId == null)
            {
                return RedirectToAction(nameof(ChooseEduPlan), new { EduYearId });
            }
            return RedirectToAction(nameof(Index), new { EduYearId, EduPlanId });
        }

        // GET: DisciplinePomeshenies/Create
        public async Task<IActionResult> Create(int? EduPlanId, int? EduYearId)
        {
            var eduPlan = await _context.EduPlans
                .Include(p => p.EduPlanEduYears)
                .Include(p => p.BlokDiscipl)
                    .ThenInclude(bd => bd.BlokDisciplChast)
                        .ThenInclude(bdc => bdc.Disciplines)
                            .ThenInclude(d => d.DisciplineName)
                .SingleOrDefaultAsync(p => p.EduPlanId == EduPlanId);

            var disciplinesOfEduPlanOnEduYear = new List<Discipline>();
            foreach (var BlokDiscipl in eduPlan.BlokDiscipl)
            {
                foreach (var BlokDisciplChast in BlokDiscipl.BlokDisciplChast)
                {
                    disciplinesOfEduPlanOnEduYear.AddRange(BlokDisciplChast.Disciplines);
                }
            }

            var eduPlanEduYear = await _context.EduPlanEduYears
                .Include(py => py.EduYear)
                .SingleOrDefaultAsync(py => py.EduPlanId == EduPlanId && py.EduYearId == EduYearId);
            ViewBag.eduPlanEduYear = eduPlanEduYear;

            ViewData["DisciplineId"] = new SelectList(disciplinesOfEduPlanOnEduYear, "DisciplineId", "DisciplineName.DisciplineNameName");
            ViewData["PomeshenieId"] = new SelectList(_context.Pomeshenie.Include(p=>p.Korpus), "PomeshenieId", "PomeshenieFullName");
            return View();
        }

        // POST: DisciplinePomeshenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisciplinePomeshenieId,DisciplineId,EduPlanEduYearId,PomeshenieId")] DisciplinePomeshenie disciplinePomeshenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplinePomeshenie);
                await _context.SaveChangesAsync();
                var eduPlanEduYear = await _context.EduPlanEduYears.SingleOrDefaultAsync(py => py.EduPlanEduYearId == disciplinePomeshenie.EduPlanEduYearId);
                return RedirectToAction(nameof(Index), new { eduPlanEduYear?.EduPlanId, eduPlanEduYear?.EduYearId });
            }
            
            return View(disciplinePomeshenie);
        }

        // GET: DisciplinePomeshenies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinePomeshenie = await _context.DisciplinePomeshenies
                .Include(t => t.Discipline.DisciplineName)
                .Include(t => t.EduPlanEduYear.EduYear)
                .Include(t => t.EduPlanEduYear.EduPlan)
                .Include(t => t.Pomeshenie.Korpus)
                .SingleOrDefaultAsync(m => m.DisciplinePomeshenieId == id);
            if (disciplinePomeshenie == null)
            {
                return NotFound();
            }
            var eduPlan = await _context.EduPlans
                .Include(p => p.EduPlanEduYears)
                .Include(p => p.BlokDiscipl)
                    .ThenInclude(bd => bd.BlokDisciplChast)
                        .ThenInclude(bdc => bdc.Disciplines)
                            .ThenInclude(d => d.DisciplineName)
                .SingleOrDefaultAsync(p => p.EduPlanId == disciplinePomeshenie.EduPlanEduYear.EduPlanId);

            var disciplinesOfEduPlanOnEduYear = new List<Discipline>();
            foreach (var BlokDiscipl in eduPlan.BlokDiscipl)
            {
                foreach (var BlokDisciplChast in BlokDiscipl.BlokDisciplChast)
                {
                    disciplinesOfEduPlanOnEduYear.AddRange(BlokDisciplChast.Disciplines);
                }
            }

            ViewData["DisciplineId"] = new SelectList(disciplinesOfEduPlanOnEduYear, "DisciplineId", "DisciplineName.DisciplineNameName", disciplinePomeshenie.DisciplineId);
            ViewData["PomeshenieId"] = new SelectList(_context.Pomeshenie, "PomeshenieId", "PomeshenieFullName", disciplinePomeshenie.PomeshenieId);
            return View(disciplinePomeshenie);
        }

        // POST: DisciplinePomeshenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisciplinePomeshenieId,DisciplineId,EduPlanEduYearId,PomeshenieId")] DisciplinePomeshenie disciplinePomeshenie)
        {
            if (id != disciplinePomeshenie.DisciplinePomeshenieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplinePomeshenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinePomeshenieExists(disciplinePomeshenie.DisciplinePomeshenieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var eduPlanEduYear = await _context.EduPlanEduYears.SingleOrDefaultAsync(py => py.EduPlanEduYearId == disciplinePomeshenie.EduPlanEduYearId);
                return RedirectToAction(nameof(Index), new { eduPlanEduYear?.EduPlanId, eduPlanEduYear?.EduYearId });
            }
            
            return View(disciplinePomeshenie);
        }

        // GET: DisciplinePomeshenies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinePomeshenie = await _context.DisciplinePomeshenies
                .Include(d => d.Discipline.DisciplineName)
                .Include(d => d.EduPlanEduYear.EduYear)
                .Include(d => d.EduPlanEduYear.EduPlan)
                .Include(d => d.Pomeshenie.Korpus)
                .SingleOrDefaultAsync(m => m.DisciplinePomeshenieId == id);
            if (disciplinePomeshenie == null)
            {
                return NotFound();
            }

            return View(disciplinePomeshenie);
        }

        // POST: DisciplinePomeshenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplinePomeshenie = await _context.DisciplinePomeshenies.SingleOrDefaultAsync(m => m.DisciplinePomeshenieId == id);
            _context.DisciplinePomeshenies.Remove(disciplinePomeshenie);
            await _context.SaveChangesAsync();

            var eduPlanEduYear = await _context.EduPlanEduYears.SingleOrDefaultAsync(py => py.EduPlanEduYearId == disciplinePomeshenie.EduPlanEduYearId);
            return RedirectToAction(nameof(Index), new { eduPlanEduYear?.EduPlanId, eduPlanEduYear?.EduYearId });
        }

        private bool DisciplinePomeshenieExists(int id)
        {
            return _context.DisciplinePomeshenies.Any(e => e.DisciplinePomeshenieId == id);
        }
    }
}
