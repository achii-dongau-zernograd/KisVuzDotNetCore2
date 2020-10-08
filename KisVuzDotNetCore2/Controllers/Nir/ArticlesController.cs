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
using KisVuzDotNetCore2.Infrastructure;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Files;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    [Authorize(Roles ="Администраторы, НИЧ")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesRepository _articlesRepository;
        private readonly ISelectListRepository _selectListRepository;
        private readonly IFileModelRepository _fileModelRepository;


        public ArticlesController(IArticlesRepository articlesRepository,
            ISelectListRepository selectListRepository,
            IFileModelRepository fileModelRepository)
        {
            _articlesRepository = articlesRepository;
            _selectListRepository = selectListRepository;
            _fileModelRepository = fileModelRepository;
        }

        // GET: Articles
        public IActionResult Index()
        {
            var articles = _articlesRepository.GetArticles();
            return View(articles);
        }

        /// <summary>
        /// Статьи, ожидающие подтверждения
        /// </summary>
        /// <returns></returns>
        public IActionResult ArticlesNotConfirmed()
        {
            var articlesNotConfirmed = _articlesRepository.GetArticlesNotConfirmed();
            return View(articlesNotConfirmed);
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
            _articlesRepository.ConfirmArticle(articleId);            
            return RedirectToAction("ArticlesNotConfirmed");
        }
               
        

        public IActionResult CreateOrEditArticle(int? id)
        {
            Article article = _articlesRepository.GetArticle(id);
            if (article == null)
            {
                article = new Article();
            }

            ViewBag.Authors = _selectListRepository.GetSelectListAuthors();
            ViewBag.ScienceJournals = _selectListRepository.GetSelectListScienceJournals(article.ScienceJournalId);
            ViewBag.Years = _selectListRepository.GetSelectListYears(article.YearId);
            ViewBag.NirSpecials = _selectListRepository.GetSelectListNirSpecials();
            ViewBag.NirTemas = _selectListRepository.GetSelectListNirTemas();

            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditArticle(Article article,
            string AuthorFilter,
            int AuthorIdAdd, int AuthorIdRemove, decimal AuthorPart,
            int NirSpecialIdAdd, int NirSpecialIdRemove,
            int NirTemaIdAdd, int NirTemaIdRemove,
            CreateOrEditNirDataModeEnum mode, IFormFile uploadFile)
        {
            Article articleEntry = _articlesRepository.GetArticle(article.ArticleId);

            if (articleEntry == null)
            {
                if (uploadFile != null)
                {
                    FileModel f = await _fileModelRepository.UploadArticleAsync(article, uploadFile);
                }
                _articlesRepository.AddArticle(article);
                articleEntry = article;
            }
            else
            {
                if (uploadFile != null)
                {
                    FileModel f = await _fileModelRepository.UploadArticleAsync(articleEntry, uploadFile);
                    article.FileModelId = articleEntry.FileModelId;
                }
                article.ArticleNirSpecials = articleEntry.ArticleNirSpecials;
                article.ArticleAuthors = articleEntry.ArticleAuthors;
                article.ArticleNirTemas = articleEntry.ArticleNirTemas;
                _articlesRepository.UpdateArticle(articleEntry, article);
            }


            switch (mode)
            {
                case CreateOrEditNirDataModeEnum.Saving:
                    article.RowStatusId = (int)RowStatusEnum.NotConfirmed;
                    _articlesRepository.UpdateArticle(articleEntry, article);
                    return RedirectToAction(nameof(Index));
                case CreateOrEditNirDataModeEnum.Canceling:
                    if (article.RowStatusId == null)
                    {
                        await _articlesRepository.RemoveArticleAsync(articleEntry.ArticleId);
                    }
                    return RedirectToAction(nameof(Index));
                case CreateOrEditNirDataModeEnum.AddingAuthor:
                    if (AuthorIdAdd != 0 && article.ArticleAuthors!=null)
                    {
                        var isExists = article.ArticleAuthors.FirstOrDefault(a => a.AuthorId == AuthorIdAdd) != null;
                        if (!isExists)
                        {
                            article.ArticleAuthors.Add(new ArticleAuthor
                            {
                                AuthorId = AuthorIdAdd,
                                AuthorPart = AuthorPart
                            });
                            _articlesRepository.UpdateArticle(articleEntry, article);
                        }
                    }
                    break;
                case CreateOrEditNirDataModeEnum.EditingAuthor:
                    break;
                case CreateOrEditNirDataModeEnum.RemovingAuthor:
                    if (AuthorIdRemove != 0)
                    {
                        var articleAuthorsToRemove = article.ArticleAuthors.FirstOrDefault(aa => aa.AuthorId == AuthorIdRemove);
                        if (articleAuthorsToRemove != null)
                        {
                            article.ArticleAuthors.Remove(articleAuthorsToRemove);
                            _articlesRepository.UpdateArticle(articleEntry, article);
                        }
                    }
                    break;
                case CreateOrEditNirDataModeEnum.ApplyAuthorFilter:
                    break;
                case CreateOrEditNirDataModeEnum.AddingNirSpecial:
                    if (NirSpecialIdAdd != 0)
                    {
                        var isExists = article.ArticleNirSpecials.FirstOrDefault(s => s.NirSpecialId == NirSpecialIdAdd) != null;
                        if (!isExists)
                        {
                            article.ArticleNirSpecials.Add(new ArticleNirSpecial { NirSpecialId = NirSpecialIdAdd });
                            _articlesRepository.UpdateArticle(articleEntry, article);
                        }
                    }
                    break;
                case CreateOrEditNirDataModeEnum.EditingNirSpecial:
                    break;
                case CreateOrEditNirDataModeEnum.RemovingNirSpecial:
                    if (NirSpecialIdRemove != 0)
                    {
                        var articleToRemove = article.ArticleNirSpecials.FirstOrDefault(s => s.NirSpecialId == NirSpecialIdRemove);
                        if (articleToRemove != null)
                        {
                            article.ArticleNirSpecials.Remove(articleToRemove);
                            _articlesRepository.UpdateArticle(articleEntry, article);
                        }
                    }
                    break;
                case CreateOrEditNirDataModeEnum.AddingNirTema:
                    if (NirTemaIdAdd != 0)
                    {
                        var isExists = article.ArticleNirTemas.FirstOrDefault(s => s.NirTemaId == NirTemaIdAdd) != null;
                        if (!isExists)
                        {
                            article.ArticleNirTemas.Add(new ArticleNirTema { NirTemaId = NirTemaIdAdd });
                            _articlesRepository.UpdateArticle(articleEntry, article);
                        }
                    }
                    break;
                case CreateOrEditNirDataModeEnum.EditingNirTema:
                    break;
                case CreateOrEditNirDataModeEnum.RemovingNirTema:
                    if (NirTemaIdRemove != 0)
                    {
                        var articleToRemove = article.ArticleNirTemas.FirstOrDefault(s => s.NirTemaId == NirTemaIdRemove);
                        if (articleToRemove != null)
                        {
                            article.ArticleNirTemas.Remove(articleToRemove);
                            _articlesRepository.UpdateArticle(articleEntry, article);
                        }
                    }
                    break;
                default:
                    break;
            }

            ViewBag.AuthorFilter = AuthorFilter;
            ViewBag.Authors = _selectListRepository.GetSelectListAuthors(AuthorFilter);
            ViewBag.ScienceJournals = _selectListRepository.GetSelectListScienceJournals(article.ScienceJournalId);
            ViewBag.Years = _selectListRepository.GetSelectListYears(article.YearId);
            ViewBag.NirSpecials = _selectListRepository.GetSelectListNirSpecials();
            ViewBag.NirTemas = _selectListRepository.GetSelectListNirTemas();

            return View(articleEntry);
        }

        // GET: Articles/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = _articlesRepository.GetArticle(id);
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
            await _articlesRepository.RemoveArticleAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
