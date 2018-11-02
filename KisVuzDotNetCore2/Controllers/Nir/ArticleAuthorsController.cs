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
    public class ArticleAuthorsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public ArticleAuthorsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: ArticleAuthors
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.ArticleAuthors.Include(a => a.Article).Include(a => a.Author);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: ArticleAuthors/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "ArticleName");
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorName");
            return View();
        }

        // POST: ArticleAuthors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleAuthorId,AuthorId,ArticleId,AuthorPart")] ArticleAuthor articleAuthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "ArticleId", articleAuthor.ArticleId);
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorId", articleAuthor.AuthorId);
            return View(articleAuthor);
        }

        // GET: ArticleAuthors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleAuthor = await _context.ArticleAuthors.SingleOrDefaultAsync(m => m.ArticleAuthorId == id);
            if (articleAuthor == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "ArticleId", articleAuthor.ArticleId);
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorId", articleAuthor.AuthorId);
            return View(articleAuthor);
        }

        // POST: ArticleAuthors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleAuthorId,AuthorId,ArticleId,AuthorPart")] ArticleAuthor articleAuthor)
        {
            if (id != articleAuthor.ArticleAuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleAuthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleAuthorExists(articleAuthor.ArticleAuthorId))
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
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "ArticleId", articleAuthor.ArticleId);
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorId", articleAuthor.AuthorId);
            return View(articleAuthor);
        }

        // GET: ArticleAuthors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleAuthor = await _context.ArticleAuthors
                .Include(a => a.Article)
                .Include(a => a.Author)
                .SingleOrDefaultAsync(m => m.ArticleAuthorId == id);
            if (articleAuthor == null)
            {
                return NotFound();
            }

            return View(articleAuthor);
        }

        // POST: ArticleAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleAuthor = await _context.ArticleAuthors.SingleOrDefaultAsync(m => m.ArticleAuthorId == id);
            _context.ArticleAuthors.Remove(articleAuthor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleAuthorExists(int id)
        {
            return _context.ArticleAuthors.Any(e => e.ArticleAuthorId == id);
        }
    }
}
