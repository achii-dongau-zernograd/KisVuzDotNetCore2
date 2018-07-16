using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.UchPosobiya;

namespace KisVuzDotNetCore2.Controllers.UchPosobiya
{
    public class UchPosobiesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UchPosobiesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UchPosobies
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.UchPosobie.Include(u => u.FileModel).Include(u => u.UchPosobieFormaIzdaniya);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: UchPosobies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobie = await _context.UchPosobie
                .Include(u => u.FileModel)
                .Include(u => u.UchPosobieFormaIzdaniya)
                .SingleOrDefaultAsync(m => m.UchPosobieId == id);
            if (uchPosobie == null)
            {
                return NotFound();
            }

            return View(uchPosobie);
        }

        // GET: UchPosobies/Create
        public IActionResult Create()
        {
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id");
            ViewData["UchPosobieFormaIzdaniyaId"] = new SelectList(_context.UchPosobieFormaIzdaniya, "UchPosobieFormaIzdaniyaId", "UchPosobieFormaIzdaniyaName");
            return View();
        }

        // POST: UchPosobies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UchPosobieId,GodIzdaniya,UchPosobieName,BiblOpisanie,UchPosobieFormaIzdaniyaId,FileModelId")] UchPosobie uchPosobie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uchPosobie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", uchPosobie.FileModelId);
            ViewData["UchPosobieFormaIzdaniyaId"] = new SelectList(_context.UchPosobieFormaIzdaniya, "UchPosobieFormaIzdaniyaId", "UchPosobieFormaIzdaniyaId", uchPosobie.UchPosobieFormaIzdaniyaId);
            return View(uchPosobie);
        }

        // GET: UchPosobies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobie = await _context.UchPosobie.SingleOrDefaultAsync(m => m.UchPosobieId == id);
            if (uchPosobie == null)
            {
                return NotFound();
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", uchPosobie.FileModelId);
            ViewData["UchPosobieFormaIzdaniyaId"] = new SelectList(_context.UchPosobieFormaIzdaniya, "UchPosobieFormaIzdaniyaId", "UchPosobieFormaIzdaniyaId", uchPosobie.UchPosobieFormaIzdaniyaId);
            return View(uchPosobie);
        }

        // POST: UchPosobies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UchPosobieId,GodIzdaniya,UchPosobieName,BiblOpisanie,UchPosobieFormaIzdaniyaId,FileModelId")] UchPosobie uchPosobie)
        {
            if (id != uchPosobie.UchPosobieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uchPosobie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UchPosobieExists(uchPosobie.UchPosobieId))
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
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", uchPosobie.FileModelId);
            ViewData["UchPosobieFormaIzdaniyaId"] = new SelectList(_context.UchPosobieFormaIzdaniya, "UchPosobieFormaIzdaniyaId", "UchPosobieFormaIzdaniyaId", uchPosobie.UchPosobieFormaIzdaniyaId);
            return View(uchPosobie);
        }

        // GET: UchPosobies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobie = await _context.UchPosobie
                .Include(u => u.FileModel)
                .Include(u => u.UchPosobieFormaIzdaniya)
                .SingleOrDefaultAsync(m => m.UchPosobieId == id);
            if (uchPosobie == null)
            {
                return NotFound();
            }

            return View(uchPosobie);
        }

        // POST: UchPosobies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uchPosobie = await _context.UchPosobie.SingleOrDefaultAsync(m => m.UchPosobieId == id);
            _context.UchPosobie.Remove(uchPosobie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UchPosobieExists(int id)
        {
            return _context.UchPosobie.Any(e => e.UchPosobieId == id);
        }
    }
}
