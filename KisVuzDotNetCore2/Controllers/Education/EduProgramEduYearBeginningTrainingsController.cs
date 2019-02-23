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
    public class EduProgramEduYearBeginningTrainingsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduProgramEduYearBeginningTrainingsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduProgramEduYearBeginningTrainings
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduProgramEduYearBeginningTraining.Include(e => e.EduProgram).Include(e => e.EduYearBeginningTraining);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduProgramEduYearBeginningTrainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramEduYearBeginningTraining = await _context.EduProgramEduYearBeginningTraining
                .Include(e => e.EduProgram)
                .Include(e => e.EduYearBeginningTraining)
                .SingleOrDefaultAsync(m => m.EduProgramEduYearBeginningTrainingId == id);
            if (eduProgramEduYearBeginningTraining == null)
            {
                return NotFound();
            }

            return View(eduProgramEduYearBeginningTraining);
        }

        // GET: EduProgramEduYearBeginningTrainings/Create
        public IActionResult Create()
        {
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId");
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingId");
            return View();
        }

        // POST: EduProgramEduYearBeginningTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduProgramEduYearBeginningTrainingId,EduProgramId,EduYearBeginningTrainingId")] EduProgramEduYearBeginningTraining eduProgramEduYearBeginningTraining)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduProgramEduYearBeginningTraining);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId", eduProgramEduYearBeginningTraining.EduProgramId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingId", eduProgramEduYearBeginningTraining.EduYearBeginningTrainingId);
            return View(eduProgramEduYearBeginningTraining);
        }

        // GET: EduProgramEduYearBeginningTrainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramEduYearBeginningTraining = await _context.EduProgramEduYearBeginningTraining.SingleOrDefaultAsync(m => m.EduProgramEduYearBeginningTrainingId == id);
            if (eduProgramEduYearBeginningTraining == null)
            {
                return NotFound();
            }
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId", eduProgramEduYearBeginningTraining.EduProgramId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingId", eduProgramEduYearBeginningTraining.EduYearBeginningTrainingId);
            return View(eduProgramEduYearBeginningTraining);
        }

        // POST: EduProgramEduYearBeginningTrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduProgramEduYearBeginningTrainingId,EduProgramId,EduYearBeginningTrainingId")] EduProgramEduYearBeginningTraining eduProgramEduYearBeginningTraining)
        {
            if (id != eduProgramEduYearBeginningTraining.EduProgramEduYearBeginningTrainingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduProgramEduYearBeginningTraining);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduProgramEduYearBeginningTrainingExists(eduProgramEduYearBeginningTraining.EduProgramEduYearBeginningTrainingId))
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
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId", eduProgramEduYearBeginningTraining.EduProgramId);
            ViewData["EduYearBeginningTrainingId"] = new SelectList(_context.EduYearBeginningTrainings, "EduYearBeginningTrainingId", "EduYearBeginningTrainingId", eduProgramEduYearBeginningTraining.EduYearBeginningTrainingId);
            return View(eduProgramEduYearBeginningTraining);
        }

        // GET: EduProgramEduYearBeginningTrainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramEduYearBeginningTraining = await _context.EduProgramEduYearBeginningTraining
                .Include(e => e.EduProgram)
                .Include(e => e.EduYearBeginningTraining)
                .SingleOrDefaultAsync(m => m.EduProgramEduYearBeginningTrainingId == id);
            if (eduProgramEduYearBeginningTraining == null)
            {
                return NotFound();
            }

            return View(eduProgramEduYearBeginningTraining);
        }

        // POST: EduProgramEduYearBeginningTrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduProgramEduYearBeginningTraining = await _context.EduProgramEduYearBeginningTraining.SingleOrDefaultAsync(m => m.EduProgramEduYearBeginningTrainingId == id);
            _context.EduProgramEduYearBeginningTraining.Remove(eduProgramEduYearBeginningTraining);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduProgramEduYearBeginningTrainingExists(int id)
        {
            return _context.EduProgramEduYearBeginningTraining.Any(e => e.EduProgramEduYearBeginningTrainingId == id);
        }
    }
}
