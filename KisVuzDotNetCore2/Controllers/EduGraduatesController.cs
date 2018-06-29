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
    public class EduGraduatesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduGraduatesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduGraduates
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduGraduate.Include(e => e.EduProfile).Include(e => e.GraduateYear);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduGraduates/Create
        public IActionResult Create()
        {
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileName");
            ViewData["GraduateYearId"] = new SelectList(_context.GraduateYear, "GraduateYearId", "GraduateYearName");
            return View();
        }

        // POST: EduGraduates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduGraduateId,EduProfileId,GraduateYearId,GraduateNumber,GraduateSredBall")] EduGraduate eduGraduate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduGraduate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileName", eduGraduate.EduProfileId);
            ViewData["GraduateYearId"] = new SelectList(_context.GraduateYear, "GraduateYearId", "GraduateYearName", eduGraduate.GraduateYearId);
            return View(eduGraduate);
        }

        // GET: EduGraduates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduGraduate = await _context.EduGraduate.SingleOrDefaultAsync(m => m.EduGraduateId == id);
            if (eduGraduate == null)
            {
                return NotFound();
            }
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileName", eduGraduate.EduProfileId);
            ViewData["GraduateYearId"] = new SelectList(_context.GraduateYear, "GraduateYearId", "GraduateYearName", eduGraduate.GraduateYearId);
            return View(eduGraduate);
        }

        // POST: EduGraduates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduGraduateId,EduProfileId,GraduateYearId,GraduateNumber,GraduateSredBall")] EduGraduate eduGraduate)
        {
            if (id != eduGraduate.EduGraduateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduGraduate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduGraduateExists(eduGraduate.EduGraduateId))
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
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileName", eduGraduate.EduProfileId);
            ViewData["GraduateYearId"] = new SelectList(_context.GraduateYear, "GraduateYearId", "GraduateYearName", eduGraduate.GraduateYearId);
            return View(eduGraduate);
        }

        // GET: EduGraduates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduGraduate = await _context.EduGraduate
                .Include(e => e.EduProfile)
                .Include(e => e.GraduateYear)
                .SingleOrDefaultAsync(m => m.EduGraduateId == id);
            if (eduGraduate == null)
            {
                return NotFound();
            }

            return View(eduGraduate);
        }

        // POST: EduGraduates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduGraduate = await _context.EduGraduate.SingleOrDefaultAsync(m => m.EduGraduateId == id);
            _context.EduGraduate.Remove(eduGraduate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduGraduateExists(int id)
        {
            return _context.EduGraduate.Any(e => e.EduGraduateId == id);
        }
    }
}
