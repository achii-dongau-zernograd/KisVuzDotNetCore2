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
using KisVuzDotNetCore2.Models.Common;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    [Authorize(Roles ="Администраторы, НИЧ")]
    public class ArticlesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public ArticlesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.Articles
                .Include(a => a.FileModel)
                .Include(a => a.ScienceJournal)
                .Include(a=>a.RowStatus);
            return View(await appIdentityDBContext.ToListAsync());
        }

        /// <summary>
        /// Подтверждение статьи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmArticle(int articleId)
        {
            var article = _context.Articles.SingleOrDefault(a => a.ArticleId == articleId);
            article.RowStatusId = (int)RowStatusEnum.Confirmed;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Name");
            ViewData["ScienceJournalId"] = new SelectList(_context.ScienceJournals, "ScienceJournalId", "ScienceJournalName");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,ArticleName,ScienceJournalId,VipuskNumber,Pages,FileModelId")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Name", article.FileModelId);
            ViewData["ScienceJournalId"] = new SelectList(_context.ScienceJournals, "ScienceJournalId", "ScienceJournalName", article.ScienceJournalId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.SingleOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Name", article.FileModelId);
            ViewData["ScienceJournalId"] = new SelectList(_context.ScienceJournals, "ScienceJournalId", "ScienceJournalName", article.ScienceJournalId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,ArticleName,ScienceJournalId,VipuskNumber,Pages,FileModelId")] Article article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
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
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Name", article.FileModelId);
            ViewData["ScienceJournalId"] = new SelectList(_context.ScienceJournals, "ScienceJournalId", "ScienceJournalName", article.ScienceJournalId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.FileModel)
                .Include(a => a.ScienceJournal)
                .SingleOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.SingleOrDefaultAsync(m => m.ArticleId == id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}
