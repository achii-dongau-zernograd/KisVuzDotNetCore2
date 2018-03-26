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
    public class EduYearsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduYearsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduYears
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduYears.ToListAsync());
        }

        // GET: EduYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduYear = await _context.EduYears
                .SingleOrDefaultAsync(m => m.EduYearId == id);
            if (eduYear == null)
            {
                return NotFound();
            }

            return View(eduYear);
        }

        // GET: EduYears/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduYearId,EduYearName")] EduYear eduYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduYear);
        }

        // GET: EduYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduYear = await _context.EduYears.SingleOrDefaultAsync(m => m.EduYearId == id);
            if (eduYear == null)
            {
                return NotFound();
            }
            return View(eduYear);
        }

        // POST: EduYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduYearId,EduYearName")] EduYear eduYear)
        {
            if (id != eduYear.EduYearId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduYearExists(eduYear.EduYearId))
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
            return View(eduYear);
        }

        // GET: EduYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduYear = await _context.EduYears
                .SingleOrDefaultAsync(m => m.EduYearId == id);
            if (eduYear == null)
            {
                return NotFound();
            }

            return View(eduYear);
        }

        // POST: EduYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduYear = await _context.EduYears.SingleOrDefaultAsync(m => m.EduYearId == id);
            _context.EduYears.Remove(eduYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduYearExists(int id)
        {
            return _context.EduYears.Any(e => e.EduYearId == id);
        }
    }
}
