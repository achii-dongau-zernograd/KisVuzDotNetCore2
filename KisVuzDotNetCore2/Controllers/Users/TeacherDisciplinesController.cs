using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Users;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers.Users
{
    public class TeacherDisciplinesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public TeacherDisciplinesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: TeacherDisciplines
        public async Task<IActionResult> Index(int? EduPlanId, int? EduYearId)
        {
            if(EduYearId==null)
            {
                return RedirectToAction(nameof(ChooseEduYear));
            }
            if (EduPlanId == null)
            {
                return RedirectToAction(nameof(ChooseEduPlan),new { EduYearId });
            }

            var appIdentityDBContext = _context.TeacherDisciplines
                .Where(t=>t.EduPlanEduYear.EduYearId == EduYearId && t.EduPlanEduYear.EduPlanId== EduPlanId)
                .Include(t => t.Discipline.DisciplineName)                
                .Include(t => t.EduPlanEduYear.EduYear)
                .Include(t => t.Teacher.AppUser);

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
            if(EduYearId == null)
            {
                return RedirectToAction(nameof(ChooseEduYear));
            }
            return RedirectToAction(nameof(Index),new { EduYearId });
        }

        /// <summary>
        /// Выбор учебного плана, действующего в учебном году EduYearId
        /// </summary>
        /// <param name="EduYearId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ChooseEduPlan(int? EduYearId)
        {
            if(EduYearId==null)
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
                                    if(eduPlanEduYear.EduYearId == EduYearId)
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

            //var eduPlans = await _context.EduPlans.ToListAsync();
            //ViewData["EduPlans"] = new SelectList(eduPlans, "EduPlanId", "EduPlanId");
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


        
        // GET: TeacherDisciplines/Create
        public async Task<IActionResult> Create(int? EduPlanId, int? EduYearId)
        {
            var eduPlan = await _context.EduPlans
                .Include(p=>p.EduPlanEduYears)
                .Include(p=>p.BlokDiscipl)
                    .ThenInclude(bd=>bd.BlokDisciplChast)
                        .ThenInclude(bdc => bdc.Disciplines)
                            .ThenInclude(d => d.DisciplineName)
                .SingleOrDefaultAsync(p=>p.EduPlanId == EduPlanId);

            var disciplinesOfEduPlanOnEduYear = new List<Discipline>();
            foreach (var BlokDiscipl in eduPlan.BlokDiscipl)
            {
                foreach (var BlokDisciplChast in BlokDiscipl.BlokDisciplChast)
                {
                    disciplinesOfEduPlanOnEduYear.AddRange(BlokDisciplChast.Disciplines);
                }
            }

            var eduPlanEduYear = await _context.EduPlanEduYears
                .Include(py=>py.EduYear)
                .SingleOrDefaultAsync(py=>py.EduPlanId == EduPlanId && py.EduYearId == EduYearId);
            ViewBag.eduPlanEduYear = eduPlanEduYear;
            
            ViewData["DisciplineId"] = new SelectList(disciplinesOfEduPlanOnEduYear, "DisciplineId", "DisciplineName.DisciplineNameName");
            
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio");
            return View();
        }

        // POST: TeacherDisciplines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherDisciplineId,EduPlanEduYearId,TeacherId,DisciplineId")] TeacherDiscipline teacherDiscipline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherDiscipline);
                await _context.SaveChangesAsync();
                var eduPlanEduYear = await _context.EduPlanEduYears.SingleOrDefaultAsync(py => py.EduPlanEduYearId == teacherDiscipline.EduPlanEduYearId);
                return RedirectToAction(nameof(Index), new { eduPlanEduYear?.EduPlanId, eduPlanEduYear?.EduYearId });
            }
            //ViewData["DisciplineId"] = new SelectList(_context.Disciplines.Include(d => d.DisciplineName), "DisciplineId", "DisciplineName.DisciplineNameName", teacherDiscipline.DisciplineId);
            //ViewData["EduPlanEduYearId"] = new SelectList(_context.EduPlanEduYears, "EduPlanEduYearId", "EduPlanEduYearName", teacherDiscipline.EduPlanEduYearId);
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio", teacherDiscipline.TeacherId);
            return View(teacherDiscipline);
        }

        // GET: TeacherDisciplines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherDiscipline = await _context.TeacherDisciplines
                .Include(t => t.Discipline)
                .Include(t => t.EduPlanEduYear.EduYear)
                .Include(t => t.EduPlanEduYear.EduPlan)
                .Include(t => t.Teacher)                
                .SingleOrDefaultAsync(m => m.TeacherDisciplineId == id);
            if (teacherDiscipline == null)
            {
                return NotFound();
            }

            var eduPlan = await _context.EduPlans
                .Include(p => p.EduPlanEduYears)
                .Include(p => p.BlokDiscipl)
                    .ThenInclude(bd => bd.BlokDisciplChast)
                        .ThenInclude(bdc => bdc.Disciplines)
                            .ThenInclude(d => d.DisciplineName)
                .SingleOrDefaultAsync(p => p.EduPlanId == teacherDiscipline.EduPlanEduYear.EduPlanId);

            var disciplinesOfEduPlanOnEduYear = new List<Discipline>();
            foreach (var BlokDiscipl in eduPlan.BlokDiscipl)
            {
                foreach (var BlokDisciplChast in BlokDiscipl.BlokDisciplChast)
                {
                    disciplinesOfEduPlanOnEduYear.AddRange(BlokDisciplChast.Disciplines);
                }
            }
            
            ViewData["DisciplineId"] = new SelectList(disciplinesOfEduPlanOnEduYear, "DisciplineId", "DisciplineName.DisciplineNameName", teacherDiscipline.DisciplineId);            
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio", teacherDiscipline.TeacherId);
            return View(teacherDiscipline);
        }

        // POST: TeacherDisciplines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherDisciplineId,EduPlanEduYearId,TeacherId,DisciplineId")] TeacherDiscipline teacherDiscipline)
        {
            if (id != teacherDiscipline.TeacherDisciplineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherDiscipline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherDisciplineExists(teacherDiscipline.TeacherDisciplineId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var eduPlanEduYear = await _context.EduPlanEduYears.SingleOrDefaultAsync(py => py.EduPlanEduYearId == teacherDiscipline.EduPlanEduYearId);
                return RedirectToAction(nameof(Index), new { eduPlanEduYear?.EduPlanId, eduPlanEduYear?.EduYearId });
            }
            //ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "DisciplineId", "DisciplineId", teacherDiscipline.DisciplineId);
            //ViewData["EduPlanEduYearId"] = new SelectList(_context.EduPlanEduYears, "EduPlanEduYearId", "EduPlanEduYearId", teacherDiscipline.EduPlanEduYearId);
            //ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", teacherDiscipline.TeacherId);
            return View(teacherDiscipline);
        }

        // GET: TeacherDisciplines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherDiscipline = await _context.TeacherDisciplines
                .Include(t => t.Discipline.DisciplineName)
                .Include(t => t.EduPlanEduYear.EduYear)
                .Include(t => t.EduPlanEduYear.EduPlan)
                .Include(t => t.Teacher.AppUser)
                .SingleOrDefaultAsync(m => m.TeacherDisciplineId == id);
            if (teacherDiscipline == null)
            {
                return NotFound();
            }

            return View(teacherDiscipline);
        }

        // POST: TeacherDisciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherDiscipline = await _context.TeacherDisciplines.SingleOrDefaultAsync(m => m.TeacherDisciplineId == id);
            _context.TeacherDisciplines.Remove(teacherDiscipline);
            await _context.SaveChangesAsync();

            var eduPlanEduYear = await _context.EduPlanEduYears.SingleOrDefaultAsync(py => py.EduPlanEduYearId == teacherDiscipline.EduPlanEduYearId);
            return RedirectToAction(nameof(Index), new { eduPlanEduYear?.EduPlanId, eduPlanEduYear?.EduYearId });
        }

        private bool TeacherDisciplineExists(int id)
        {
            return _context.TeacherDisciplines.Any(e => e.TeacherDisciplineId == id);
        }
    }
}
