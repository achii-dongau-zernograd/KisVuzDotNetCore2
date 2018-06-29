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
    public class EduPlanEduVidDeyatsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduPlanEduVidDeyatsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduPlanEduVidDeyats
        public async Task<IActionResult> Index()
        {                        
            var appIdentityDBContext = _context.EduPlanEduVidDeyats
                .Include(e => e.EduPlan.EduPlanEduYearBeginningTrainings)
                .Include(e => e.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e=> e.EduPlan.EduForm)
                .Include(e => e.EduVidDeyat);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduPlanEduVidDeyats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlanEduVidDeyat = await _context.EduPlanEduVidDeyats
                .Include(e => e.EduPlan)
                .Include(e => e.EduVidDeyat)
                .SingleOrDefaultAsync(m => m.EduPlanEduVidDeyatId == id);
            if (eduPlanEduVidDeyat == null)
            {
                return NotFound();
            }

            return View(eduPlanEduVidDeyat);
        }

        // GET: EduPlanEduVidDeyats/Create
        public IActionResult Create(int? id)
        {
            var EduPlans = _context.EduPlans
                .Include(p => p.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(p => p.EduForm);
                //.Include(p=> p.EduPlanEduYearBeginningTrainings);
            ViewData["EduPlanId"] = new SelectList(EduPlans, "EduPlanId", "EduPlanDescription", id);
            ViewData["EduVidDeyatId"] = new SelectList(_context.EduVidDeyat, "EduVidDeyatId", "EduVidDeyatName", id);
            return View();
        }

        // POST: EduPlanEduVidDeyats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EduPlanEduVidDeyat eduPlanEduVidDeyat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduPlanEduVidDeyat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanId", eduPlanEduVidDeyat.EduPlanId);
            ViewData["EduVidDeyatId"] = new SelectList(_context.EduVidDeyat, "EduVidDeyatId", "EduVidDeyatId", eduPlanEduVidDeyat.EduVidDeyatId);
            return View(eduPlanEduVidDeyat);
        }

        // GET: EduPlanEduVidDeyats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlanEduVidDeyat = await _context.EduPlanEduVidDeyats.SingleOrDefaultAsync(m => m.EduPlanEduVidDeyatId == id);
            if (eduPlanEduVidDeyat == null)
            {
                return NotFound();
            }
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanId", eduPlanEduVidDeyat.EduPlanId);
            ViewData["EduVidDeyatId"] = new SelectList(_context.EduVidDeyat, "EduVidDeyatId", "EduVidDeyatId", eduPlanEduVidDeyat.EduVidDeyatId);
            return View(eduPlanEduVidDeyat);
        }

        // POST: EduPlanEduVidDeyats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduPlanEduVidDeyatId,EduPlanId,EduVidDeyatId")] EduPlanEduVidDeyat eduPlanEduVidDeyat)
        {
            if (id != eduPlanEduVidDeyat.EduPlanEduVidDeyatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduPlanEduVidDeyat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduPlanEduVidDeyatExists(eduPlanEduVidDeyat.EduPlanEduVidDeyatId))
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
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanId", eduPlanEduVidDeyat.EduPlanId);
            ViewData["EduVidDeyatId"] = new SelectList(_context.EduVidDeyat, "EduVidDeyatId", "EduVidDeyatId", eduPlanEduVidDeyat.EduVidDeyatId);
            return View(eduPlanEduVidDeyat);
        }

        // GET: EduPlanEduVidDeyats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlanEduVidDeyat = await _context.EduPlanEduVidDeyats
                .Include(e => e.EduPlan)
                .Include(e => e.EduVidDeyat)
                .SingleOrDefaultAsync(m => m.EduPlanEduVidDeyatId == id);
            if (eduPlanEduVidDeyat == null)
            {
                return NotFound();
            }

            return View(eduPlanEduVidDeyat);
        }

        // POST: EduPlanEduVidDeyats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduPlanEduVidDeyat = await _context.EduPlanEduVidDeyats.SingleOrDefaultAsync(m => m.EduPlanEduVidDeyatId == id);
            _context.EduPlanEduVidDeyats.Remove(eduPlanEduVidDeyat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduPlanEduVidDeyatExists(int id)
        {
            return _context.EduPlanEduVidDeyats.Any(e => e.EduPlanEduVidDeyatId == id);
        }
    }
}
