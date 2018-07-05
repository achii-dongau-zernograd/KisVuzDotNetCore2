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
    public class EduPlanEduYearBeginningTrainingsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduPlanEduYearBeginningTrainingsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduPlanEduYearBeginningTrainings
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduPlanEduYearBeginningTraining
                .Include(e => e.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduPlan.EduForm)
                .Include(e => e.EduYearBeginningTraining);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduPlanEduYearBeginningTrainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlanEduYearBeginningTraining = await _context.EduPlanEduYearBeginningTraining
                .Include(e => e.EduPlan)
                .Include(e => e.EduYearBeginningTraining)
                .SingleOrDefaultAsync(m => m.EduPlanEduYearBeginningTrainingId == id);
            if (eduPlanEduYearBeginningTraining == null)
            {
                return NotFound();
            }

            return View(eduPlanEduYearBeginningTraining);
        }

        // GET: EduPlanEduYearBeginningTrainings/Create
        public IActionResult Create(int? id)
        {
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanDescription", id);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingName", id);
            return View();
        }

        // POST: EduPlanEduYearBeginningTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EduPlanEduYearBeginningTraining eduPlanEduYearBeginningTraining)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduPlanEduYearBeginningTraining);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanDescription", eduPlanEduYearBeginningTraining.EduPlanId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingName");
            return View(eduPlanEduYearBeginningTraining);
        }

        // GET: EduPlanEduYearBeginningTrainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlanEduYearBeginningTraining = await _context.EduPlanEduYearBeginningTraining.SingleOrDefaultAsync(m => m.EduPlanEduYearBeginningTrainingId == id);
            if (eduPlanEduYearBeginningTraining == null)
            {
                return NotFound();
            }
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanId", eduPlanEduYearBeginningTraining.EduPlanId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingId", eduPlanEduYearBeginningTraining.EduYearBeginningTrainingId);
            return View(eduPlanEduYearBeginningTraining);
        }

        // POST: EduPlanEduYearBeginningTrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduPlanEduYearBeginningTrainingId,EduPlanId,EduYearBeginningTrainingId")] EduPlanEduYearBeginningTraining eduPlanEduYearBeginningTraining)
        {
            if (id != eduPlanEduYearBeginningTraining.EduPlanEduYearBeginningTrainingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduPlanEduYearBeginningTraining);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduPlanEduYearBeginningTrainingExists(eduPlanEduYearBeginningTraining.EduPlanEduYearBeginningTrainingId))
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
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanId", eduPlanEduYearBeginningTraining.EduPlanId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingId", eduPlanEduYearBeginningTraining.EduYearBeginningTrainingId);
            return View(eduPlanEduYearBeginningTraining);
        }

        // GET: EduPlanEduYearBeginningTrainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlanEduYearBeginningTraining = await _context.EduPlanEduYearBeginningTraining
                .Include(e => e.EduPlan)
                .Include(e => e.EduYearBeginningTraining)
                .SingleOrDefaultAsync(m => m.EduPlanEduYearBeginningTrainingId == id);
            if (eduPlanEduYearBeginningTraining == null)
            {
                return NotFound();
            }

            return View(eduPlanEduYearBeginningTraining);
        }

        // POST: EduPlanEduYearBeginningTrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduPlanEduYearBeginningTraining = await _context.EduPlanEduYearBeginningTraining.SingleOrDefaultAsync(m => m.EduPlanEduYearBeginningTrainingId == id);
            _context.EduPlanEduYearBeginningTraining.Remove(eduPlanEduYearBeginningTraining);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduPlanEduYearBeginningTrainingExists(int id)
        {
            return _context.EduPlanEduYearBeginningTraining.Any(e => e.EduPlanEduYearBeginningTrainingId == id);
        }
    }
}
