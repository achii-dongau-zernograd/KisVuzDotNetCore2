using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers
{
    public class EduProgramEduYearsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduProgramEduYearsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduProgramEduYears
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduProgramEduYears.Include(e => e.EduProgram).Include(e => e.EduYear);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduProgramEduYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramEduYear = await _context.EduProgramEduYears
                .Include(e => e.EduProgram)
                .Include(e => e.EduYear)
                .SingleOrDefaultAsync(m => m.EduProgramEduYearId == id);
            if (eduProgramEduYear == null)
            {
                return NotFound();
            }

            return View(eduProgramEduYear);
        }

        // GET: EduProgramEduYears/Create
        public IActionResult Create()
        {
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId");
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearId");
            return View();
        }

        // POST: EduProgramEduYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduProgramEduYearId,EduProgramId,EduYearId")] EduProgramEduYear eduProgramEduYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduProgramEduYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId", eduProgramEduYear.EduProgramId);
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearId", eduProgramEduYear.EduYearId);
            return View(eduProgramEduYear);
        }

        // GET: EduProgramEduYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramEduYear = await _context.EduProgramEduYears.SingleOrDefaultAsync(m => m.EduProgramEduYearId == id);
            if (eduProgramEduYear == null)
            {
                return NotFound();
            }
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId", eduProgramEduYear.EduProgramId);
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearId", eduProgramEduYear.EduYearId);
            return View(eduProgramEduYear);
        }

        // POST: EduProgramEduYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduProgramEduYearId,EduProgramId,EduYearId")] EduProgramEduYear eduProgramEduYear)
        {
            if (id != eduProgramEduYear.EduProgramEduYearId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduProgramEduYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduProgramEduYearExists(eduProgramEduYear.EduProgramEduYearId))
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
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId", eduProgramEduYear.EduProgramId);
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearId", eduProgramEduYear.EduYearId);
            return View(eduProgramEduYear);
        }

        // GET: EduProgramEduYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramEduYear = await _context.EduProgramEduYears
                .Include(e => e.EduProgram)
                .Include(e => e.EduYear)
                .SingleOrDefaultAsync(m => m.EduProgramEduYearId == id);
            if (eduProgramEduYear == null)
            {
                return NotFound();
            }

            return View(eduProgramEduYear);
        }

        // POST: EduProgramEduYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduProgramEduYear = await _context.EduProgramEduYears.SingleOrDefaultAsync(m => m.EduProgramEduYearId == id);
            _context.EduProgramEduYears.Remove(eduProgramEduYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduProgramEduYearExists(int id)
        {
            return _context.EduProgramEduYears.Any(e => e.EduProgramEduYearId == id);
        }
    }
}
