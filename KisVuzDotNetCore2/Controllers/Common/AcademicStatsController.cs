using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;

namespace KisVuzDotNetCore2.Controllers
{
    public class AcademicStatsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public AcademicStatsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: AcademicStats
        public async Task<IActionResult> Index()
        {
            return View(await _context.AcademicStats.ToListAsync());
        }

        // GET: AcademicStats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicStat = await _context.AcademicStats
                .SingleOrDefaultAsync(m => m.AcademicStatId == id);
            if (academicStat == null)
            {
                return NotFound();
            }

            return View(academicStat);
        }

        // GET: AcademicStats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AcademicStats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AcademicStatId,AcademicStatName")] AcademicStat academicStat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicStat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(academicStat);
        }

        // GET: AcademicStats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicStat = await _context.AcademicStats.SingleOrDefaultAsync(m => m.AcademicStatId == id);
            if (academicStat == null)
            {
                return NotFound();
            }
            return View(academicStat);
        }

        // POST: AcademicStats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AcademicStatId,AcademicStatName")] AcademicStat academicStat)
        {
            if (id != academicStat.AcademicStatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicStat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicStatExists(academicStat.AcademicStatId))
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
            return View(academicStat);
        }

        // GET: AcademicStats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicStat = await _context.AcademicStats
                .SingleOrDefaultAsync(m => m.AcademicStatId == id);
            if (academicStat == null)
            {
                return NotFound();
            }

            return View(academicStat);
        }

        // POST: AcademicStats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicStat = await _context.AcademicStats.SingleOrDefaultAsync(m => m.AcademicStatId == id);
            _context.AcademicStats.Remove(academicStat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicStatExists(int id)
        {
            return _context.AcademicStats.Any(e => e.AcademicStatId == id);
        }
    }
}
