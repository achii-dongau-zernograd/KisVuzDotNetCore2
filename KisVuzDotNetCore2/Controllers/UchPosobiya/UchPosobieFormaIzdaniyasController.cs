using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.UchPosobiya;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.UchPosobiya
{
    [Authorize(Roles = "Администраторы")]
    public class UchPosobieFormaIzdaniyasController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UchPosobieFormaIzdaniyasController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UchPosobieFormaIzdaniyas
        public async Task<IActionResult> Index()
        {
            return View(await _context.UchPosobieFormaIzdaniya.ToListAsync());
        }

        // GET: UchPosobieFormaIzdaniyas/Create
        public IActionResult Create()
        {
           

            return View();
        }

        // POST: UchPosobieFormaIzdaniyas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UchPosobieFormaIzdaniyaId,UchPosobieFormaIzdaniyaName")] UchPosobieFormaIzdaniya uchPosobieFormaIzdaniya)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uchPosobieFormaIzdaniya);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uchPosobieFormaIzdaniya);
        }

        // GET: UchPosobieFormaIzdaniyas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieFormaIzdaniya = await _context.UchPosobieFormaIzdaniya.SingleOrDefaultAsync(m => m.UchPosobieFormaIzdaniyaId == id);
            if (uchPosobieFormaIzdaniya == null)
            {
                return NotFound();
            }
            return View(uchPosobieFormaIzdaniya);
        }

        // POST: UchPosobieFormaIzdaniyas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UchPosobieFormaIzdaniyaId,UchPosobieFormaIzdaniyaName")] UchPosobieFormaIzdaniya uchPosobieFormaIzdaniya)
        {
            if (id != uchPosobieFormaIzdaniya.UchPosobieFormaIzdaniyaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uchPosobieFormaIzdaniya);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UchPosobieFormaIzdaniyaExists(uchPosobieFormaIzdaniya.UchPosobieFormaIzdaniyaId))
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
            return View(uchPosobieFormaIzdaniya);
        }

        // GET: UchPosobieFormaIzdaniyas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieFormaIzdaniya = await _context.UchPosobieFormaIzdaniya
                .SingleOrDefaultAsync(m => m.UchPosobieFormaIzdaniyaId == id);
            if (uchPosobieFormaIzdaniya == null)
            {
                return NotFound();
            }

            return View(uchPosobieFormaIzdaniya);
        }

        // POST: UchPosobieFormaIzdaniyas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uchPosobieFormaIzdaniya = await _context.UchPosobieFormaIzdaniya.SingleOrDefaultAsync(m => m.UchPosobieFormaIzdaniyaId == id);
            _context.UchPosobieFormaIzdaniya.Remove(uchPosobieFormaIzdaniya);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UchPosobieFormaIzdaniyaExists(int id)
        {
            return _context.UchPosobieFormaIzdaniya.Any(e => e.UchPosobieFormaIzdaniyaId == id);
        }
    }
}
