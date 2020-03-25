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
    [Authorize(Roles = "Администраторы, Библиотека, НИЧ")]    
    public class AuthorsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public AuthorsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.Author
                .Include(a => a.AppUser)
                .OrderBy(a => a.AuthorName);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users.OrderBy(u => u.GetFullName), "Id", "GetFullName");
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,AuthorName,AppUserId")] Author author)
        {
            if (ModelState.IsValid)
            {
                // Проверяем, имеется ли уже зарегистрированный автор с указанными данными (ФИО)
                bool isRegistered = _context.Author.Any(a => a.AuthorName == author.AuthorName);
                if (isRegistered)
                    return View("AuthorIsAlreadyRegistered");

                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users.OrderBy(u => u.GetFullName), "Id", "GetFullName", author.AppUserId);
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.SingleOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users.OrderBy(u => u.GetFullName), "Id", "GetFullName", author.AppUserId);
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,AuthorName,AppUserId")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorId))
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
            ViewData["AppUserId"] = new SelectList(_context.Users.OrderBy(u => u.GetFullName), "Id", "Id", author.AppUserId);
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .Include(a => a.AppUser)
                .SingleOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Author.SingleOrDefaultAsync(m => m.AuthorId == id);
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.AuthorId == id);
        }
    }
}
