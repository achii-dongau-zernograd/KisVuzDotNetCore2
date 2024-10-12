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

namespace KisVuzDotNetCore2.Controllers.Sveden
{
    [Authorize(Roles = "Администраторы, Учебная часть")]
    public class KorpusController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public KorpusController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Korpus
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.Korpus.Include(k => k.KorpusAddress);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: Korpus/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress");
            return View();
        }

        // POST: Korpus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Korpus korpus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korpus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress", korpus.AddressId);
            return View(korpus);
        }

        // GET: Korpus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korpus = await _context.Korpus.SingleOrDefaultAsync(m => m.KorpusId == id);
            if (korpus == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress", korpus.AddressId);
            return View(korpus);
        }

        // POST: Korpus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Korpus korpus)
        {
            if (id != korpus.KorpusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korpus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorpusExists(korpus.KorpusId))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress", korpus.AddressId);
            return View(korpus);
        }

        // GET: Korpus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korpus = await _context.Korpus
                .Include(k => k.KorpusAddress)
                .SingleOrDefaultAsync(m => m.KorpusId == id);
            if (korpus == null)
            {
                return NotFound();
            }

            return View(korpus);
        }

        // POST: Korpus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var korpus = await _context.Korpus.SingleOrDefaultAsync(m => m.KorpusId == id);
            _context.Korpus.Remove(korpus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorpusExists(int id)
        {
            return _context.Korpus.Any(e => e.KorpusId == id);
        }
    }
}
