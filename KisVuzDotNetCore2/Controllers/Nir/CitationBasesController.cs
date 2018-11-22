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
    public class CitationBasesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public CitationBasesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: CitationBases
        public async Task<IActionResult> Index()
        {
            return View(await _context.CitationBases.ToListAsync());
        }

        // GET: CitationBases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CitationBases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitationBaseId,CitationBaseName")] CitationBase citationBase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citationBase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(citationBase);
        }

        // GET: CitationBases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citationBase = await _context.CitationBases.SingleOrDefaultAsync(m => m.CitationBaseId == id);
            if (citationBase == null)
            {
                return NotFound();
            }
            return View(citationBase);
        }

        // POST: CitationBases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitationBaseId,CitationBaseName")] CitationBase citationBase)
        {
            if (id != citationBase.CitationBaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citationBase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitationBaseExists(citationBase.CitationBaseId))
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
            return View(citationBase);
        }

        // GET: CitationBases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citationBase = await _context.CitationBases
                .SingleOrDefaultAsync(m => m.CitationBaseId == id);
            if (citationBase == null)
            {
                return NotFound();
            }

            return View(citationBase);
        }

        // POST: CitationBases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citationBase = await _context.CitationBases.SingleOrDefaultAsync(m => m.CitationBaseId == id);
            _context.CitationBases.Remove(citationBase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitationBaseExists(int id)
        {
            return _context.CitationBases.Any(e => e.CitationBaseId == id);
        }
    }
}
