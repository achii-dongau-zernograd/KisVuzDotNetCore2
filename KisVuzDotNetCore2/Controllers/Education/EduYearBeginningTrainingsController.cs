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
    public class EduYearBeginningTrainingsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduYearBeginningTrainingsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduYearBeginningTrainings
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduYearBeginningTrainings.ToListAsync());
        }

        // GET: EduYearBeginningTrainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduYearBeginningTraining = await _context.EduYearBeginningTrainings
                .SingleOrDefaultAsync(m => m.EduYearBeginningTrainingId == id);
            if (eduYearBeginningTraining == null)
            {
                return NotFound();
            }

            return View(eduYearBeginningTraining);
        }

        // GET: EduYearBeginningTrainings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduYearBeginningTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduYearBeginningTrainingId,EduYearBeginningTrainingName")] EduYearBeginningTraining eduYearBeginningTraining)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduYearBeginningTraining);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduYearBeginningTraining);
        }

        // GET: EduYearBeginningTrainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduYearBeginningTraining = await _context.EduYearBeginningTrainings.SingleOrDefaultAsync(m => m.EduYearBeginningTrainingId == id);
            if (eduYearBeginningTraining == null)
            {
                return NotFound();
            }
            return View(eduYearBeginningTraining);
        }

        // POST: EduYearBeginningTrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduYearBeginningTrainingId,EduYearBeginningTrainingName")] EduYearBeginningTraining eduYearBeginningTraining)
        {
            if (id != eduYearBeginningTraining.EduYearBeginningTrainingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduYearBeginningTraining);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduYearBeginningTrainingExists(eduYearBeginningTraining.EduYearBeginningTrainingId))
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
            return View(eduYearBeginningTraining);
        }

        // GET: EduYearBeginningTrainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduYearBeginningTraining = await _context.EduYearBeginningTrainings
                .SingleOrDefaultAsync(m => m.EduYearBeginningTrainingId == id);
            if (eduYearBeginningTraining == null)
            {
                return NotFound();
            }

            return View(eduYearBeginningTraining);
        }

        // POST: EduYearBeginningTrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduYearBeginningTraining = await _context.EduYearBeginningTrainings.SingleOrDefaultAsync(m => m.EduYearBeginningTrainingId == id);
            _context.EduYearBeginningTrainings.Remove(eduYearBeginningTraining);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduYearBeginningTrainingExists(int id)
        {
            return _context.EduYearBeginningTrainings.Any(e => e.EduYearBeginningTrainingId == id);
        }
    }
}
