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
    public class UchPosobieDisciplineNamesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UchPosobieDisciplineNamesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UchPosobieDisciplineNames
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.UchPosobieDisciplineName
                .Include(u => u.DisciplineName)
                .Include(u => u.UchPosobie);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: UchPosobieDisciplineNames/Create
        public IActionResult Create()
        {
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameName");
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName");
            return View();
        }

        // POST: UchPosobieDisciplineNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UchPosobieDisciplineNameId,UchPosobieId,DisciplineNameId")] UchPosobieDisciplineName uchPosobieDisciplineName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uchPosobieDisciplineName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameId", uchPosobieDisciplineName.DisciplineNameId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieId", uchPosobieDisciplineName.UchPosobieId);
            return View(uchPosobieDisciplineName);
        }

        // GET: UchPosobieDisciplineNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieDisciplineName = await _context.UchPosobieDisciplineName.SingleOrDefaultAsync(m => m.UchPosobieDisciplineNameId == id);
            if (uchPosobieDisciplineName == null)
            {
                return NotFound();
            }
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameName", uchPosobieDisciplineName.DisciplineNameId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName", uchPosobieDisciplineName.UchPosobieId);
            return View(uchPosobieDisciplineName);
        }

        // POST: UchPosobieDisciplineNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UchPosobieDisciplineNameId,UchPosobieId,DisciplineNameId")] UchPosobieDisciplineName uchPosobieDisciplineName)
        {
            if (id != uchPosobieDisciplineName.UchPosobieDisciplineNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uchPosobieDisciplineName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UchPosobieDisciplineNameExists(uchPosobieDisciplineName.UchPosobieDisciplineNameId))
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
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameId", uchPosobieDisciplineName.DisciplineNameId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieId", uchPosobieDisciplineName.UchPosobieId);
            return View(uchPosobieDisciplineName);
        }

        // GET: UchPosobieDisciplineNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieDisciplineName = await _context.UchPosobieDisciplineName
                .Include(u => u.DisciplineName)
                .Include(u => u.UchPosobie)
                .SingleOrDefaultAsync(m => m.UchPosobieDisciplineNameId == id);
            if (uchPosobieDisciplineName == null)
            {
                return NotFound();
            }

            return View(uchPosobieDisciplineName);
        }

        // POST: UchPosobieDisciplineNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uchPosobieDisciplineName = await _context.UchPosobieDisciplineName.SingleOrDefaultAsync(m => m.UchPosobieDisciplineNameId == id);
            _context.UchPosobieDisciplineName.Remove(uchPosobieDisciplineName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UchPosobieDisciplineNameExists(int id)
        {
            return _context.UchPosobieDisciplineName.Any(e => e.UchPosobieDisciplineNameId == id);
        }
    }
}
