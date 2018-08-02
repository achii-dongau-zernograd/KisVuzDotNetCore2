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
    public class EduPlanEduYearsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduPlanEduYearsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduPlanEduYears
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduPlanEduYears
                .Include(e => e.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduPlan.EduForm)
                .Include(e => e.EduYear);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduPlanEduYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlanEduYear = await _context.EduPlanEduYears
                .Include(e => e.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduPlan.EduForm)
                .Include(e => e.EduYear)
                .SingleOrDefaultAsync(m => m.EduPlanEduYearId == id);
            if (eduPlanEduYear == null)
            {
                return NotFound();
            }

            return View(eduPlanEduYear);
        }

        // GET: EduPlanEduYears/Create
        public IActionResult Create(int? id)
        {
            var EduPlans = _context.EduPlans
                .Include(p => p.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(p => p.EduForm);
            ViewData["EduPlanId"] = new SelectList(EduPlans, "EduPlanId", "EduPlanDescription", id);
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", id);
            return View();
        }

        // POST: EduPlanEduYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EduPlanEduYear eduPlanEduYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduPlanEduYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanDescription", eduPlanEduYear.EduPlanId);
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", eduPlanEduYear.EduYearId);
            return View(eduPlanEduYear);
        }

        // GET: EduPlanEduYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlanEduYear = await _context.EduPlanEduYears.SingleOrDefaultAsync(m => m.EduPlanEduYearId == id);
            if (eduPlanEduYear == null)
            {
                return NotFound();
            }
            var EduPlans = _context.EduPlans
                .Include(p => p.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(p => p.EduForm);
            ViewData["EduPlanId"] = new SelectList(EduPlans, "EduPlanId", "EduPlanDescription", eduPlanEduYear.EduPlanId);
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", eduPlanEduYear.EduYearId);
            return View(eduPlanEduYear);
        }

        // POST: EduPlanEduYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduPlanEduYearId,EduPlanId,EduYearId")] EduPlanEduYear eduPlanEduYear)
        {
            if (id != eduPlanEduYear.EduPlanEduYearId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduPlanEduYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduPlanEduYearExists(eduPlanEduYear.EduPlanEduYearId))
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
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanId", eduPlanEduYear.EduPlanId);
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearId", eduPlanEduYear.EduYearId);
            return View(eduPlanEduYear);
        }

        // GET: EduPlanEduYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlanEduYear = await _context.EduPlanEduYears
                .Include(e => e.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduPlan.EduForm)
                .Include(e => e.EduYear)
                .SingleOrDefaultAsync(m => m.EduPlanEduYearId == id);
            if (eduPlanEduYear == null)
            {
                return NotFound();
            }

            return View(eduPlanEduYear);
        }

        // POST: EduPlanEduYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduPlanEduYear = await _context.EduPlanEduYears.SingleOrDefaultAsync(m => m.EduPlanEduYearId == id);
            _context.EduPlanEduYears.Remove(eduPlanEduYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduPlanEduYearExists(int id)
        {
            return _context.EduPlanEduYears.Any(e => e.EduPlanEduYearId == id);
        }
    }
}
