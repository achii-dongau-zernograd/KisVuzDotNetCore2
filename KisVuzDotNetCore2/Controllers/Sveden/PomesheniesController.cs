using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Models.Sveden
{
    [Authorize(Roles = "Администраторы")]
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
            var pomeshenies = await _context.Pomeshenie
                .Include(p => p.Korpus)
                .Include(p=>p.PomeshenieTypes)
                .ToListAsync();

            foreach (var pomeshenie in pomeshenies)
            {
                foreach (var pomeshenieType in pomeshenie.PomeshenieTypes)
                {
                    pomeshenieType.PomeshenieType = await _context.PomeshenieType.SingleOrDefaultAsync(t => t.PomeshenieTypeId == pomeshenieType.PomeshenieTypeId);
                }
            }

            return View(pomeshenies);
        }

        // GET: Pomeshenies/Create
        public IActionResult Create()
        {
            ViewData["KorpusId"] = new SelectList(_context.Korpus, "KorpusId", "KorpusName");

            List<PomeshenieType> pomeshenieTypes = _context.PomeshenieType.ToList();
            ViewData["PomeshenieType"] = pomeshenieTypes;

            return View();
        }

        // POST: Pomeshenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Pomeshenie pomeshenie, int[] PomeshenieTypeIds)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pomeshenie);
                await _context.SaveChangesAsync();

                if (PomeshenieTypeIds != null)
                {
                    var pomeshenieTypes = new List<PomeshenieTypepomesheniya>();
                    foreach (var PomeshenieTypeId in PomeshenieTypeIds)
                    {
                        PomeshenieTypepomesheniya pomeshenieTypepomesheniya = new PomeshenieTypepomesheniya();
                        pomeshenieTypepomesheniya.PomeshenieId = pomeshenie.PomeshenieId;
                        pomeshenieTypepomesheniya.PomeshenieTypeId = PomeshenieTypeId;
                        pomeshenieTypes.Add(pomeshenieTypepomesheniya);
                    }
                    await _context.PomeshenieTypepomesheniya.AddRangeAsync(pomeshenieTypes);
                    await _context.SaveChangesAsync();
                }


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

            var pomeshenie = await _context.Pomeshenie
                .Include(p => p.PomeshenieTypes)
                .SingleOrDefaultAsync(m => m.PomeshenieId == id);
            if (pomeshenie == null)
            {
                return NotFound();
            }
            
            ViewData["KorpusId"] = new SelectList(_context.Korpus, "KorpusId", "KorpusName", pomeshenie.KorpusId);
            
            List<PomeshenieType> pomeshenieTypes = _context.PomeshenieType.ToList();
            ViewData["PomeshenieType"] = pomeshenieTypes;

            return View(pomeshenie);
        }

        // POST: Pomeshenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pomeshenie pomeshenie, int[] PomeshenieTypeIds)
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

                    if (PomeshenieTypeIds != null)
                    {
                        _context.PomeshenieTypepomesheniya.RemoveRange(_context.PomeshenieTypepomesheniya.Where(v => v.PomeshenieId == pomeshenie.PomeshenieId));
                        await _context.SaveChangesAsync();

                        var pomeshenieTypes = new List<PomeshenieTypepomesheniya>();
                        foreach (var PomeshenieTypeId in PomeshenieTypeIds)
                        {
                            PomeshenieTypepomesheniya pomeshenieTypepomesheniya = new PomeshenieTypepomesheniya();
                            pomeshenieTypepomesheniya.PomeshenieId = pomeshenie.PomeshenieId;
                            pomeshenieTypepomesheniya.PomeshenieTypeId = PomeshenieTypeId;
                            pomeshenieTypes.Add(pomeshenieTypepomesheniya);
                        }
                        await _context.PomeshenieTypepomesheniya.AddRangeAsync(pomeshenieTypes);
                        await _context.SaveChangesAsync();
                    }

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
                .Include(p => p.PomeshenieTypes)
                .SingleOrDefaultAsync(m => m.PomeshenieId == id);
            if (pomeshenie == null)
            {
                return NotFound();
            }

            foreach (var pomeshenieType in pomeshenie.PomeshenieTypes)
            {
                pomeshenieType.PomeshenieType = await _context.PomeshenieType.SingleOrDefaultAsync(t => t.PomeshenieTypeId == pomeshenieType.PomeshenieTypeId);
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
