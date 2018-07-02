using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;

namespace KisVuzDotNetCore2.Models.Sveden
{
    public class PomesheniesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public PomesheniesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Pomeshenies
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.Pomeshenie.Include(p => p.Korpus);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: Pomeshenies/Create
        public IActionResult Create()
        {
            ViewData["KorpusId"] = new SelectList(_context.Korpus, "KorpusId", "KorpusName");
            return View();
        }

        // POST: Pomeshenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Pomeshenie pomeshenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pomeshenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorpusId"] = new SelectList(_context.Korpus, "KorpusId", "KorpusName", pomeshenie.KorpusId);
            return View(pomeshenie);
        }

        // GET: Pomeshenies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pomeshenie = await _context.Pomeshenie.SingleOrDefaultAsync(m => m.PomeshenieId == id);
            if (pomeshenie == null)
            {
                return NotFound();
            }
            ViewData["KorpusId"] = new SelectList(_context.Korpus, "KorpusId", "KorpusName", pomeshenie.KorpusId);
            return View(pomeshenie);
        }

        // POST: Pomeshenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pomeshenie pomeshenie)
        {
            if (id != pomeshenie.PomeshenieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pomeshenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PomeshenieExists(pomeshenie.PomeshenieId))
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
            ViewData["KorpusId"] = new SelectList(_context.Korpus, "KorpusId", "KorpusName", pomeshenie.KorpusId);
            return View(pomeshenie);
        }

        // GET: Pomeshenies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pomeshenie = await _context.Pomeshenie
                .Include(p => p.Korpus)
                .SingleOrDefaultAsync(m => m.PomeshenieId == id);
            if (pomeshenie == null)
            {
                return NotFound();
            }

            return View(pomeshenie);
        }

        // POST: Pomeshenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pomeshenie = await _context.Pomeshenie.SingleOrDefaultAsync(m => m.PomeshenieId == id);
            _context.Pomeshenie.Remove(pomeshenie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PomeshenieExists(int id)
        {
            return _context.Pomeshenie.Any(e => e.PomeshenieId == id);
        }
    }
}
