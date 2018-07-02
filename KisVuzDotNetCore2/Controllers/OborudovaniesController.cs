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
    public class OborudovaniesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public OborudovaniesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Oborudovanies
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.Oborudovanie.Include(o => o.Pomeshenie);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: Oborudovanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oborudovanie = await _context.Oborudovanie
                .Include(o => o.Pomeshenie)
                .SingleOrDefaultAsync(m => m.OborudovanieId == id);
            if (oborudovanie == null)
            {
                return NotFound();
            }

            return View(oborudovanie);
        }

        // GET: Oborudovanies/Create
        public IActionResult Create()
        {
            ViewData["PomeshenieId"] = new SelectList(_context.Pomeshenie, "PomeshenieId", "PomeshenieName");
            return View();
        }

        // POST: Oborudovanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Oborudovanie oborudovanie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oborudovanie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PomeshenieId"] = new SelectList(_context.Pomeshenie, "PomeshenieId", "PomeshenieName", oborudovanie.PomeshenieId);
            return View(oborudovanie);
        }

        // GET: Oborudovanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oborudovanie = await _context.Oborudovanie.SingleOrDefaultAsync(m => m.OborudovanieId == id);
            if (oborudovanie == null)
            {
                return NotFound();
            }
            ViewData["PomeshenieId"] = new SelectList(_context.Pomeshenie, "PomeshenieId", "PomeshenieName", oborudovanie.PomeshenieId);
            return View(oborudovanie);
        }

        // POST: Oborudovanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OborudovanieId,PomeshenieId,OborudovanieName,OborudovanieCount")] Oborudovanie oborudovanie)
        {
            if (id != oborudovanie.OborudovanieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oborudovanie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OborudovanieExists(oborudovanie.OborudovanieId))
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
            ViewData["PomeshenieId"] = new SelectList(_context.Pomeshenie, "PomeshenieId", "PomeshenieName", oborudovanie.PomeshenieId);
            return View(oborudovanie);
        }

        // GET: Oborudovanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oborudovanie = await _context.Oborudovanie
                .Include(o => o.Pomeshenie)
                .SingleOrDefaultAsync(m => m.OborudovanieId == id);
            if (oborudovanie == null)
            {
                return NotFound();
            }

            return View(oborudovanie);
        }

        // POST: Oborudovanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oborudovanie = await _context.Oborudovanie.SingleOrDefaultAsync(m => m.OborudovanieId == id);
            _context.Oborudovanie.Remove(oborudovanie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OborudovanieExists(int id)
        {
            return _context.Oborudovanie.Any(e => e.OborudovanieId == id);
        }
    }
}
