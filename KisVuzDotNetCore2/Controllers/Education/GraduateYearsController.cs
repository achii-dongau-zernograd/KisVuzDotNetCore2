using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы, ЦПОРМ")]
    public class GraduateYearsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public GraduateYearsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: GraduateYears
        public async Task<IActionResult> Index()
        {
            return View(await _context.GraduateYear.ToListAsync());
        }

        // GET: GraduateYears/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GraduateYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GraduateYearId,GraduateYearName")] GraduateYear graduateYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(graduateYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(graduateYear);
        }

        // GET: GraduateYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduateYear = await _context.GraduateYear.SingleOrDefaultAsync(m => m.GraduateYearId == id);
            if (graduateYear == null)
            {
                return NotFound();
            }
            return View(graduateYear);
        }

        // POST: GraduateYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GraduateYearId,GraduateYearName")] GraduateYear graduateYear)
        {
            if (id != graduateYear.GraduateYearId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(graduateYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GraduateYearExists(graduateYear.GraduateYearId))
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
            return View(graduateYear);
        }

        // GET: GraduateYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduateYear = await _context.GraduateYear
                .SingleOrDefaultAsync(m => m.GraduateYearId == id);
            if (graduateYear == null)
            {
                return NotFound();
            }

            return View(graduateYear);
        }

        // POST: GraduateYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var graduateYear = await _context.GraduateYear.SingleOrDefaultAsync(m => m.GraduateYearId == id);
            _context.GraduateYear.Remove(graduateYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GraduateYearExists(int id)
        {
            return _context.GraduateYear.Any(e => e.GraduateYearId == id);
        }
    }
}
