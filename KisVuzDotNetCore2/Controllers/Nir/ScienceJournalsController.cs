using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    [Authorize(Roles = "Администраторы, НИЧ")]
    public class ScienceJournalsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public ScienceJournalsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: ScienceJournals
        public async Task<IActionResult> Index()
        {
            var scienceJournal = _context.ScienceJournals
                .Include(m => m.ScienceJournalCitationBases)
                    .ThenInclude(a=>a.CitationBase);

            return View(scienceJournal);
        }

        // GET: ScienceJournals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScienceJournals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScienceJournalId,ScienceJournalName,IsVak,IsZarubejn,ELibraryLink")] ScienceJournal scienceJournal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scienceJournal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scienceJournal);
        }

        // GET: ScienceJournals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scienceJournal = await _context.ScienceJournals.SingleOrDefaultAsync(m => m.ScienceJournalId == id);
            if (scienceJournal == null)
            {
                return NotFound();
            }
            return View(scienceJournal);
        }

        // POST: ScienceJournals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScienceJournalId,ScienceJournalName,IsVak,IsZarubejn,ELibraryLink")] ScienceJournal scienceJournal)
        {
            if (id != scienceJournal.ScienceJournalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scienceJournal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScienceJournalExists(scienceJournal.ScienceJournalId))
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
            return View(scienceJournal);
        }

        // GET: ScienceJournals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scienceJournal = await _context.ScienceJournals
                .SingleOrDefaultAsync(m => m.ScienceJournalId == id);
            if (scienceJournal == null)
            {
                return NotFound();
            }

            return View(scienceJournal);
        }

        // POST: ScienceJournals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scienceJournal = await _context.ScienceJournals.SingleOrDefaultAsync(m => m.ScienceJournalId == id);
            _context.ScienceJournals.Remove(scienceJournal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScienceJournalExists(int id)
        {
            return _context.ScienceJournals.Any(e => e.ScienceJournalId == id);
        }
    }
}
