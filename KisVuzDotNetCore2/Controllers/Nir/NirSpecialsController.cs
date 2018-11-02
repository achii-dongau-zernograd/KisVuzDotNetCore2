using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Nir;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    public class NirSpecialsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public NirSpecialsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: NirSpecials
        public async Task<IActionResult> Index()
        {
            return View(await _context.NirSpecials.ToListAsync());
        }

        
        // GET: NirSpecials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NirSpecials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NirSpecialId,NirSpecialCode,NirSpecialName")] NirSpecial nirSpecial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nirSpecial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nirSpecial);
        }

        // GET: NirSpecials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirSpecial = await _context.NirSpecials.SingleOrDefaultAsync(m => m.NirSpecialId == id);
            if (nirSpecial == null)
            {
                return NotFound();
            }
            return View(nirSpecial);
        }

        // POST: NirSpecials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NirSpecialId,NirSpecialCode,NirSpecialName")] NirSpecial nirSpecial)
        {
            if (id != nirSpecial.NirSpecialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nirSpecial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NirSpecialExists(nirSpecial.NirSpecialId))
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
            return View(nirSpecial);
        }

        // GET: NirSpecials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirSpecial = await _context.NirSpecials
                .SingleOrDefaultAsync(m => m.NirSpecialId == id);
            if (nirSpecial == null)
            {
                return NotFound();
            }

            return View(nirSpecial);
        }

        // POST: NirSpecials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nirSpecial = await _context.NirSpecials.SingleOrDefaultAsync(m => m.NirSpecialId == id);
            _context.NirSpecials.Remove(nirSpecial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NirSpecialExists(int id)
        {
            return _context.NirSpecials.Any(e => e.NirSpecialId == id);
        }
    }
}
