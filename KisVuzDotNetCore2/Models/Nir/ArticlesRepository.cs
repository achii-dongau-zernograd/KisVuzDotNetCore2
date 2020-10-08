using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Репозиторий научных статей
    /// </summary>
    public class ArticlesRepository : IArticlesRepository
    {
        AppIdentityDBContext _context;
        IFileModelRepository _fileModelRepository;

        public ArticlesRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Подтверждение статьи
        /// </summary>
        /// <param name="articleId"></param>
        public void ConfirmArticle(int articleId)
        {
            var article = _context.Articles.SingleOrDefault(a => a.ArticleId == articleId);
            article.RowStatusId = (int)RowStatusEnum.Confirmed;
            _context.SaveChanges();
        }

        /// <summary>
        /// Создаёт новую статью
        /// </summary>
        /// <param name="article"></param>
        public void AddArticle(Article article)
        {
            _context.Articles.Add(article);
            _context.SaveChanges();
        }

        /// <summary>
        /// Возвращает научную статью по переданному УИД
        /// </summary>
        /// <param name="articleId">УИД статьи</param>
        /// <returns></returns>
        public Article GetArticle(int? articleId)
        {
            if (articleId == null) return null;
            var article = GetArticles().SingleOrDefault(a=>a.ArticleId == articleId);
            return article;
        }

        /// <summary>
        /// Возвращает список научных статей
        /// </summary>
        /// <returns></returns>
        public List<Article> GetArticles()
        {
            List<Article> articles = GetArticlesAll()
                .OrderByDescending(a => a.Year.YearName)
                .ToList();
            return articles;
        }

        /// <summary>
        /// Возвращает список научных статей, опубликованных до указанного года включительно
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public IQueryable<Article> GetArticles(int year)
        {
            var data = GetArticlesAll().Where(a => a.YearId <= year);
            return data;
        }

        /// <summary>
        /// Все статьи
        /// </summary>
        /// <returns></returns>
        private IQueryable<Article> GetArticlesAll()
        {
            return _context.Articles
                .Include(a => a.FileModel)
                .Include(a => a.ScienceJournal)
                .Include(a => a.RowStatus)
                .Include(a => a.Year)
                .Include(a => a.ArticleAuthors)
                      .ThenInclude(aa => aa.Author)
                .Include(a => a.ArticleNirSpecials)
            .Include(a => a.ArticleNirTemas);
        }

        /// <summary>
        /// Удаляет научную статью по её УИД
        /// </summary>
        /// <param name="articleId">УИД статьи</param>
        public async Task RemoveArticleAsync(int articleId)
        {
            var article = GetArticle(articleId);
            _context.Articles.Remove(article);
            if (article.FileModel != null)
            {
                await _fileModelRepository.RemoveFileModelAsync(article.FileModel);
            }
            _context.SaveChanges();
        }

        public void UpdateArticle(Article articleEntry, Article article)
        {
            articleEntry.ArticleName = article.ArticleName;
            articleEntry.FileModelId = article.FileModelId;
            articleEntry.Pages = article.Pages;
            articleEntry.ScienceJournalId = article.ScienceJournalId;
            articleEntry.VipuskNumber = article.VipuskNumber;
            articleEntry.YearId = article.YearId;
            articleEntry.RowStatusId = article.RowStatusId;
            ////////
            /// + списки направлений, авторов и пр.
            if (article.ArticleNirSpecials != null && article.ArticleNirSpecials.Count > 0)
            {
                foreach (var articleNirSpecial in article.ArticleNirSpecials)
                {
                    bool isExists = false;
                    foreach (var articleNirSpecialEntry in articleEntry.ArticleNirSpecials)
                    {
                        if (articleNirSpecialEntry.NirSpecialId == articleNirSpecial.NirSpecialId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        articleEntry.ArticleNirSpecials.Add(articleNirSpecial);
                    }
                }
            }

            if (article.ArticleAuthors != null && article.ArticleAuthors.Count > 0)
            {
                foreach (var articleAuthor in article.ArticleAuthors)
                {
                    bool isExists = false;
                    foreach (var articleAuthorEntry in articleEntry.ArticleAuthors)
                    {
                        if (articleAuthorEntry.ArticleAuthorId == articleAuthor.ArticleAuthorId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        articleEntry.ArticleAuthors.Add(articleAuthor);
                    }
                }

                decimal firstAuthorPart = 1;
                for (int i = 1; i < articleEntry.ArticleAuthors.Count; i++)
                {
                    firstAuthorPart -= articleEntry.ArticleAuthors[i].AuthorPart;
                }

                if (firstAuthorPart < 0) firstAuthorPart = 0;
                articleEntry.ArticleAuthors[0].AuthorPart = firstAuthorPart;
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Статьи, ожидающие подтверждения
        /// </summary>
        public IEnumerable<Article> GetArticlesNotConfirmed()
        {
            return GetArticlesAll().Where(a => a.RowStatusId == (int?)RowStatusEnum.NotConfirmed);
        }

        /// <summary>
        /// Удаляет статьи
        /// </summary>
        /// <param name="articlesToDelete"></param>
        /// <returns></returns>
        public async Task RemoveArticlesAsync(List<Article> articlesToDelete)
        {
            foreach (var articleToDelete in articlesToDelete)
            {
                await RemoveArticleAsync(articleToDelete.ArticleId);
            }
        }
    }
}
