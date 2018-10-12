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
    public class RucovodstvoController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public RucovodstvoController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Rucovodstvo
        public async Task<IActionResult> Index()
        {
            return View(await _context.SvedenRucovodstvo.Include(r=>r.AppUser).ToListAsync());
        }

        // GET: Rucovodstvo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rucovodstvo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RucovodstvoId,Fio,Post,Telephone,Email")] Rucovodstvo rucovodstvo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rucovodstvo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rucovodstvo);
        }

        // GET: Rucovodstvo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rucovodstvo = await _context.SvedenRucovodstvo.SingleOrDefaultAsync(m => m.RucovodstvoId == id);
            if (rucovodstvo == null)
            {
                return NotFound();
            }
            ViewBag.AppUsers = new SelectList(_context.Users, "Id", "GetFullName", rucovodstvo.AppUserId);
            return View(rucovodstvo);
        }

        // POST: Rucovodstvo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RucovodstvoId,Fio,Post,Telephone,Email,AppUserId")] Rucovodstvo rucovodstvo)
        {
            if (id != rucovodstvo.RucovodstvoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rucovodstvo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RucovodstvoExists(rucovodstvo.RucovodstvoId))
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
            return View(rucovodstvo);
        }

        // GET: Rucovodstvo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rucovodstvo = await _context.SvedenRucovodstvo
                .SingleOrDefaultAsync(m => m.RucovodstvoId == id);
            if (rucovodstvo == null)
            {
                return NotFound();
            }

            return View(rucovodstvo);
        }

        // POST: Rucovodstvo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rucovodstvo = await _context.SvedenRucovodstvo.SingleOrDefaultAsync(m => m.RucovodstvoId == id);
            _context.SvedenRucovodstvo.Remove(rucovodstvo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RucovodstvoExists(int id)
        {
            return _context.SvedenRucovodstvo.Any(e => e.RucovodstvoId == id);
        }
    }
}
