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

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class PomeshenieTypepomesheniyasController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public PomeshenieTypepomesheniyasController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: PomeshenieTypepomesheniyas
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.PomeshenieTypepomesheniya.Include(p => p.Pomeshenie).Include(p => p.PomeshenieType);
            return View(await appIdentityDBContext.ToListAsync());
        }
        
        // GET: PomeshenieTypepomesheniyas/Create
        public IActionResult Create()
        {
            ViewData["PomeshenieId"] = new SelectList(_context.Pomeshenie, "PomeshenieId", "PomeshenieName");
            ViewData["PomeshenieTypeId"] = new SelectList(_context.PomeshenieType, "PomeshenieTypeId", "PomeshenieTypeName");
            return View();
        }

        // POST: PomeshenieTypepomesheniyas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( PomeshenieTypepomesheniya pomeshenieTypepomesheniya)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pomeshenieTypepomesheniya);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PomeshenieId"] = new SelectList(_context.Pomeshenie, "PomeshenieId", "PomeshenieName", pomeshenieTypepomesheniya.PomeshenieId);
            ViewData["PomeshenieTypeId"] = new SelectList(_context.PomeshenieType, "PomeshenieTypeId", "PomeshenieTypeName", pomeshenieTypepomesheniya.PomeshenieTypeId);
            return View(pomeshenieTypepomesheniya);
        }

        // GET: PomeshenieTypepomesheniyas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pomeshenieTypepomesheniya = await _context.PomeshenieTypepomesheniya.SingleOrDefaultAsync(m => m.PomeshenieTypepomesheniyaId == id);
            if (pomeshenieTypepomesheniya == null)
            {
                return NotFound();
            }
            ViewData["PomeshenieId"] = new SelectList(_context.Pomeshenie, "PomeshenieId", "PomeshenieName", pomeshenieTypepomesheniya.PomeshenieId);
            ViewData["PomeshenieTypeId"] = new SelectList(_context.PomeshenieType, "PomeshenieTypeId", "PomeshenieTypeName", pomeshenieTypepomesheniya.PomeshenieTypeId);
            return View(pomeshenieTypepomesheniya);
        }

        // POST: PomeshenieTypepomesheniyas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PomeshenieTypepomesheniya pomeshenieTypepomesheniya)
        {
            if (id != pomeshenieTypepomesheniya.PomeshenieTypepomesheniyaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pomeshenieTypepomesheniya);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PomeshenieTypepomesheniyaExists(pomeshenieTypepomesheniya.PomeshenieTypepomesheniyaId))
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
            ViewData["PomeshenieId"] = new SelectList(_context.Pomeshenie, "PomeshenieId", "PomeshenieName", pomeshenieTypepomesheniya.PomeshenieId);
            ViewData["PomeshenieTypeId"] = new SelectList(_context.PomeshenieType, "PomeshenieTypeId", "PomeshenieTypeName", pomeshenieTypepomesheniya.PomeshenieTypeId);
            return View(pomeshenieTypepomesheniya);
        }

        // GET: PomeshenieTypepomesheniyas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pomeshenieTypepomesheniya = await _context.PomeshenieTypepomesheniya
                .Include(p => p.Pomeshenie)
                .Include(p => p.PomeshenieType)
                .SingleOrDefaultAsync(m => m.PomeshenieTypepomesheniyaId == id);
            if (pomeshenieTypepomesheniya == null)
            {
                return NotFound();
            }

            return View(pomeshenieTypepomesheniya);
        }

        // POST: PomeshenieTypepomesheniyas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pomeshenieTypepomesheniya = await _context.PomeshenieTypepomesheniya.SingleOrDefaultAsync(m => m.PomeshenieTypepomesheniyaId == id);
            _context.PomeshenieTypepomesheniya.Remove(pomeshenieTypepomesheniya);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PomeshenieTypepomesheniyaExists(int id)
        {
            return _context.PomeshenieTypepomesheniya.Any(e => e.PomeshenieTypepomesheniyaId == id);
        }
    }
}
