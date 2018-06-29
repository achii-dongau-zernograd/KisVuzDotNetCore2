using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Sveden;

namespace KisVuzDotNetCore2.Controllers
{
    public class EduPrsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduPrsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduPrs
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduPr.Include(e => e.EduForm).Include(e => e.EduProfile).Include(e => e.EduYearBeginningTraining);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduPrs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPr = await _context.EduPr
                .Include(e => e.EduForm)
                .Include(e => e.EduProfile)
                .Include(e => e.EduYearBeginningTraining)
                .SingleOrDefaultAsync(m => m.EduPrId == id);
            if (eduPr == null)
            {
                return NotFound();
            }

            return View(eduPr);
        }

        // GET: EduPrs/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId");
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingId");
            return View();
        }

        // POST: EduPrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduPrId,EduProfileId,IsOVZ,EduYearBeginningTrainingId,EduFormId,EduPrUchebn,EduPrProizv,EduPrPreddiplomn")] EduPr eduPr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduPr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduPr.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", eduPr.EduProfileId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingId", eduPr.EduYearBeginningTrainingId);
            return View(eduPr);
        }

        // GET: EduPrs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPr = await _context.EduPr.SingleOrDefaultAsync(m => m.EduPrId == id);
            if (eduPr == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduPr.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", eduPr.EduProfileId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingId", eduPr.EduYearBeginningTrainingId);
            return View(eduPr);
        }

        // POST: EduPrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduPrId,EduProfileId,IsOVZ,EduYearBeginningTrainingId,EduFormId,EduPrUchebn,EduPrProizv,EduPrPreddiplomn")] EduPr eduPr)
        {
            if (id != eduPr.EduPrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduPr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduPrExists(eduPr.EduPrId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduPr.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", eduPr.EduProfileId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingId", eduPr.EduYearBeginningTrainingId);
            return View(eduPr);
        }

        // GET: EduPrs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPr = await _context.EduPr
                .Include(e => e.EduForm)
                .Include(e => e.EduProfile)
                .Include(e => e.EduYearBeginningTraining)
                .SingleOrDefaultAsync(m => m.EduPrId == id);
            if (eduPr == null)
            {
                return NotFound();
            }

            return View(eduPr);
        }

        // POST: EduPrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduPr = await _context.EduPr.SingleOrDefaultAsync(m => m.EduPrId == id);
            _context.EduPr.Remove(eduPr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduPrExists(int id)
        {
            return _context.EduPr.Any(e => e.EduPrId == id);
        }
    }
}
