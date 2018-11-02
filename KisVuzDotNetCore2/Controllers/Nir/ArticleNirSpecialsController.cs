using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    [Authorize(Roles ="Администраторы, НИЧ")]
    public class ArticleNirSpecialsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public ArticleNirSpecialsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: ArticleNirSpecials
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.ArticleNirSpecials.Include(a => a.Article).Include(a => a.NirSpecial);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: ArticleNirSpecials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleNirSpecial = await _context.ArticleNirSpecials
                .Include(a => a.Article)
                .Include(a => a.NirSpecial)
                .SingleOrDefaultAsync(m => m.ArticleNirSpecialId == id);
            if (articleNirSpecial == null)
            {
                return NotFound();
            }

            return View(articleNirSpecial);
        }

        // GET: ArticleNirSpecials/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "ArticleId");
            ViewData["NirSpecialId"] = new SelectList(_context.NirSpecials, "NirSpecialId", "NirSpecialId");
            return View();
        }

        // POST: ArticleNirSpecials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleNirSpecialId,ArticleId,NirSpecialId")] ArticleNirSpecial articleNirSpecial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleNirSpecial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "ArticleId", articleNirSpecial.ArticleId);
            ViewData["NirSpecialId"] = new SelectList(_context.NirSpecials, "NirSpecialId", "NirSpecialId", articleNirSpecial.NirSpecialId);
            return View(articleNirSpecial);
        }

        // GET: ArticleNirSpecials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleNirSpecial = await _context.ArticleNirSpecials.SingleOrDefaultAsync(m => m.ArticleNirSpecialId == id);
            if (articleNirSpecial == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "ArticleId", articleNirSpecial.ArticleId);
            ViewData["NirSpecialId"] = new SelectList(_context.NirSpecials, "NirSpecialId", "NirSpecialId", articleNirSpecial.NirSpecialId);
            return View(articleNirSpecial);
        }

        // POST: ArticleNirSpecials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleNirSpecialId,ArticleId,NirSpecialId")] ArticleNirSpecial articleNirSpecial)
        {
            if (id != articleNirSpecial.ArticleNirSpecialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleNirSpecial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleNirSpecialExists(articleNirSpecial.ArticleNirSpecialId))
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
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "ArticleId", articleNirSpecial.ArticleId);
            ViewData["NirSpecialId"] = new SelectList(_context.NirSpecials, "NirSpecialId", "NirSpecialId", articleNirSpecial.NirSpecialId);
            return View(articleNirSpecial);
        }

        // GET: ArticleNirSpecials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleNirSpecial = await _context.ArticleNirSpecials
                .Include(a => a.Article)
                .Include(a => a.NirSpecial)
                .SingleOrDefaultAsync(m => m.ArticleNirSpecialId == id);
            if (articleNirSpecial == null)
            {
                return NotFound();
            }

            return View(articleNirSpecial);
        }

        // POST: ArticleNirSpecials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleNirSpecial = await _context.ArticleNirSpecials.SingleOrDefaultAsync(m => m.ArticleNirSpecialId == id);
            _context.ArticleNirSpecials.Remove(articleNirSpecial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleNirSpecialExists(int id)
        {
            return _context.ArticleNirSpecials.Any(e => e.ArticleNirSpecialId == id);
        }
    }
}
