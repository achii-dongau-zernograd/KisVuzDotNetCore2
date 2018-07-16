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
    public class UchPosobieAuthorsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UchPosobieAuthorsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UchPosobieAuthors
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.UchPosobieAuthor
                .Include(u => u.Author)
                .Include(u => u.UchPosobie);
            return View(await appIdentityDBContext.ToListAsync());
        }

        
        // GET: UchPosobieAuthors/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName");
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName");
            return View();
        }

        // POST: UchPosobieAuthors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UchPosobieAuthorId,AuthorId,UchPosobieId")] UchPosobieAuthor uchPosobieAuthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uchPosobieAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName", uchPosobieAuthor.AuthorId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName", uchPosobieAuthor.UchPosobieId);
            return View(uchPosobieAuthor);
        }

        // GET: UchPosobieAuthors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieAuthor = await _context.UchPosobieAuthor.SingleOrDefaultAsync(m => m.UchPosobieAuthorId == id);
            if (uchPosobieAuthor == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName", uchPosobieAuthor.AuthorId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName", uchPosobieAuthor.UchPosobieId);
            return View(uchPosobieAuthor);
        }

        // POST: UchPosobieAuthors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UchPosobieAuthorId,AuthorId,UchPosobieId")] UchPosobieAuthor uchPosobieAuthor)
        {
            if (id != uchPosobieAuthor.UchPosobieAuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uchPosobieAuthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UchPosobieAuthorExists(uchPosobieAuthor.UchPosobieAuthorId))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorId", uchPosobieAuthor.AuthorId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieId", uchPosobieAuthor.UchPosobieId);
            return View(uchPosobieAuthor);
        }

        // GET: UchPosobieAuthors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieAuthor = await _context.UchPosobieAuthor
                .Include(u => u.Author)
                .Include(u => u.UchPosobie)
                .SingleOrDefaultAsync(m => m.UchPosobieAuthorId == id);
            if (uchPosobieAuthor == null)
            {
                return NotFound();
            }

            return View(uchPosobieAuthor);
        }

        // POST: UchPosobieAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uchPosobieAuthor = await _context.UchPosobieAuthor.SingleOrDefaultAsync(m => m.UchPosobieAuthorId == id);
            _context.UchPosobieAuthor.Remove(uchPosobieAuthor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UchPosobieAuthorExists(int id)
        {
            return _context.UchPosobieAuthor.Any(e => e.UchPosobieAuthorId == id);
        }
    }
}
