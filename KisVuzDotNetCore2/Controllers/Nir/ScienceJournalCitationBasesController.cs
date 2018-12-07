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
    public class ScienceJournalCitationBasesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public ScienceJournalCitationBasesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: ScienceJournalCitationBases
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.ScienceJournalCitationBases.Include(s => s.CitationBase).Include(s => s.ScienceJournal);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: ScienceJournalCitationBases/Create
        public IActionResult Create()
        {
            ViewData["CitationBaseId"] = new SelectList(_context.CitationBases, "CitationBaseId", "CitationBaseName");
            ViewData["ScienceJournalId"] = new SelectList(_context.ScienceJournals, "ScienceJournalId", "ScienceJournalName");
            return View();
        }

        // POST: ScienceJournalCitationBases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScienceJournalCitationBaseId,ScienceJournalId,CitationBaseId")] ScienceJournalCitationBase scienceJournalCitationBase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scienceJournalCitationBase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CitationBaseId"] = new SelectList(_context.CitationBases, "CitationBaseId", "CitationBaseName", scienceJournalCitationBase.CitationBaseId);
            ViewData["ScienceJournalId"] = new SelectList(_context.ScienceJournals, "ScienceJournalId", "ScienceJournalName", scienceJournalCitationBase.ScienceJournalId);
            return View(scienceJournalCitationBase);
        }

        // GET: ScienceJournalCitationBases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scienceJournalCitationBase = await _context.ScienceJournalCitationBases.SingleOrDefaultAsync(m => m.ScienceJournalCitationBaseId == id);
            if (scienceJournalCitationBase == null)
            {
                return NotFound();
            }
            ViewData["CitationBaseId"] = new SelectList(_context.CitationBases, "CitationBaseId", "CitationBaseName", scienceJournalCitationBase.CitationBaseId);
            ViewData["ScienceJournalId"] = new SelectList(_context.ScienceJournals, "ScienceJournalId", "ScienceJournalName", scienceJournalCitationBase.ScienceJournalId);
            return View(scienceJournalCitationBase);
        }

        // POST: ScienceJournalCitationBases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScienceJournalCitationBaseId,ScienceJournalId,CitationBaseId")] ScienceJournalCitationBase scienceJournalCitationBase)
        {
            if (id != scienceJournalCitationBase.ScienceJournalCitationBaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scienceJournalCitationBase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScienceJournalCitationBaseExists(scienceJournalCitationBase.ScienceJournalCitationBaseId))
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
            ViewData["CitationBaseId"] = new SelectList(_context.CitationBases, "CitationBaseId", "CitationBaseName", scienceJournalCitationBase.CitationBaseId);
            ViewData["ScienceJournalId"] = new SelectList(_context.ScienceJournals, "ScienceJournalId", "ScienceJournalName", scienceJournalCitationBase.ScienceJournalId);
            return View(scienceJournalCitationBase);
        }

        // GET: ScienceJournalCitationBases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scienceJournalCitationBase = await _context.ScienceJournalCitationBases
                .Include(s => s.CitationBase)
                .Include(s => s.ScienceJournal)
                .SingleOrDefaultAsync(m => m.ScienceJournalCitationBaseId == id);
            if (scienceJournalCitationBase == null)
            {
                return NotFound();
            }

            return View(scienceJournalCitationBase);
        }

        // POST: ScienceJournalCitationBases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scienceJournalCitationBase = await _context.ScienceJournalCitationBases.SingleOrDefaultAsync(m => m.ScienceJournalCitationBaseId == id);
            _context.ScienceJournalCitationBases.Remove(scienceJournalCitationBase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScienceJournalCitationBaseExists(int id)
        {
            return _context.ScienceJournalCitationBases.Any(e => e.ScienceJournalCitationBaseId == id);
        }
    }
}
