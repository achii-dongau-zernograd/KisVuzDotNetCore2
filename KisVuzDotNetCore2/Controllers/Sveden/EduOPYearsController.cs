using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Sveden
{
    [Authorize(Roles = "Администраторы, Учебная часть")]
    public class EduOPYearsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduOPYearsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduOPYears
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduOPYears
                .Include(e => e.EduOPEduYearName)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduYearBeginningTraining);
            return View(await appIdentityDBContext.ToListAsync());
        }               

        // GET: EduOPYears/Create
        public IActionResult Create()
        {
            ViewData["EduOPEduYearNameId"] = new SelectList(_context.EduOPEduYearNames, "EduOPEduYearNameId", "EduOPEduYearNameName");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingName");
            return View();
        }

        // POST: EduOPYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduOPYearId,EduProfileId,IsOVZ,EduYearBeginningTrainingId,EduOPEduYearNameId,VOchn,VOchnZaochn,VZaochn")] EduOPYear eduOPYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduOPYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduOPEduYearNameId"] = new SelectList(_context.EduOPEduYearNames, "EduOPEduYearNameId", "EduOPEduYearNameName", eduOPYear.EduOPEduYearNameId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", eduOPYear.EduProfileId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingName", eduOPYear.EduYearBeginningTrainingId);
            return View(eduOPYear);
        }

        // GET: EduOPYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduOPYear = await _context.EduOPYears.SingleOrDefaultAsync(m => m.EduOPYearId == id);
            if (eduOPYear == null)
            {
                return NotFound();
            }
            ViewData["EduOPEduYearNameId"] = new SelectList(_context.EduOPEduYearNames, "EduOPEduYearNameId", "EduOPEduYearNameName", eduOPYear.EduOPEduYearNameId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", eduOPYear.EduProfileId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingName", eduOPYear.EduYearBeginningTrainingId);
            return View(eduOPYear);
        }

        // POST: EduOPYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduOPYearId,EduProfileId,IsOVZ,EduYearBeginningTrainingId,EduOPEduYearNameId,VOchn,VOchnZaochn,VZaochn")] EduOPYear eduOPYear)
        {
            if (id != eduOPYear.EduOPYearId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduOPYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduOPYearExists(eduOPYear.EduOPYearId))
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
            ViewData["EduOPEduYearNameId"] = new SelectList(_context.EduOPEduYearNames, "EduOPEduYearNameId", "EduOPEduYearNameName", eduOPYear.EduOPEduYearNameId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", eduOPYear.EduProfileId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingName", eduOPYear.EduYearBeginningTrainingId);
            return View(eduOPYear);
        }

        // GET: EduOPYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduOPYear = await _context.EduOPYears
                .Include(e => e.EduOPEduYearName)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduYearBeginningTraining)
                .SingleOrDefaultAsync(m => m.EduOPYearId == id);
            if (eduOPYear == null)
            {
                return NotFound();
            }

            return View(eduOPYear);
        }

        // POST: EduOPYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduOPYear = await _context.EduOPYears.SingleOrDefaultAsync(m => m.EduOPYearId == id);
            _context.EduOPYears.Remove(eduOPYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduOPYearExists(int id)
        {
            return _context.EduOPYears.Any(e => e.EduOPYearId == id);
        }
    }
}
