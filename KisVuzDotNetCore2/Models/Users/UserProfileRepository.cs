using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Nir;
using KisVuzDotNetCore2.Models.UchPosobiya;
using Microsoft.EntityFrameworkCore;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Репозиторий профилей пользователей
    /// </summary>
    public class UserProfileRepository : IUserProfileRepository
    {
        AppIdentityDBContext _context;

        public UserProfileRepository(AppIdentityDBContext context)
        {
            _context = context;
        }
        
        public AppUser GetAppUser(string userName)
        {
            var appUser = _context.Users
                .Include(u => u.Author)
                    .ThenInclude(a => a.ArticleAuthors)
                        .ThenInclude(aa => aa.Article)
                            .ThenInclude(a => a.ScienceJournal)
                                .ThenInclude(s => s.ScienceJournalCitationBases)
                                    .ThenInclude(c => c.CitationBase)
                .Include(u => u.Author)
                    .ThenInclude(a => a.ArticleAuthors)
                        .ThenInclude(aa => aa.Article)
                            .ThenInclude(a => a.ArticleNirTemas)
                                .ThenInclude(t => t.NirTema)
                                    .ThenInclude(n => n.NirTemaEduProfileList)
                                        .ThenInclude(np => np.EduProfile)
                                            .ThenInclude(p => p.EduNapravl)
                                                .ThenInclude(n => n.EduUgs)
                                                    .ThenInclude(u => u.EduLevel)
                .Include(u => u.Author)
                    .ThenInclude(a => a.ArticleAuthors)
                        .ThenInclude(aa => aa.Article)
                            .ThenInclude(aas => aas.ArticleNirSpecials)
                                .ThenInclude(ns => ns.NirSpecial)
                .Include(u => u.Author)
                    .ThenInclude(a => a.ArticleAuthors)
                        .ThenInclude(aa => aa.Article)
                            .ThenInclude(a=>a.FileModel)
                .Include(u => u.Author)
                    .ThenInclude(a => a.ArticleAuthors)
                        .ThenInclude(aa => aa.Article)
                            .ThenInclude(a => a.Year)
                .SingleOrDefault(u => u.UserName == userName);

            return appUser;
        }

        /// <summary>
        /// Возвращает статью пользователя userName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Article GetArticle(int? id, string userName)
        {
            Article article = new Article();            

            if (id == null)
            {
                return article;
            }
                        
            List<Article> userArticles = GetArticles(userName);
            article = userArticles.SingleOrDefault(a => a.ArticleId == id);
            return article;
        }

        /// <summary>
        /// Возвращает список статей пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<Article> GetArticles(string userName)
        {
            List<Article> articles = new List<Article>();
            AppUser appUser = GetAppUser(userName);
            appUser.Author
                .ForEach(a =>
                    a.ArticleAuthors
                        .ForEach(aa =>
                            articles.Add(aa.Article)));

            return articles;
        }

        /// <summary>
        /// Добавляет статью пользователя userName
        /// </summary>
        /// <param name="article"></param>
        /// <param name="userName"></param>
        public void CreateArticle(Article article, string userName)
        {
            if (article.ArticleId != 0) return;

            AppUser appUser = GetAppUser(userName);
            Author author = appUser.Author.FirstOrDefault();
            if(author == null)
            {
                author = new Author
                {
                    AppUserId = appUser.Id,
                    AuthorName = appUser.GetFullName
                };
                _context.Author.Add(author);
                //_context.SaveChanges();
            }
            if (author.AuthorId == 0) return;

            _context.Articles.Add(article);

            ArticleAuthor articleAuthor = new ArticleAuthor
            {
                ArticleId = article.ArticleId,
                AuthorId = author.AuthorId,
                AuthorPart = 1
            };
            _context.ArticleAuthors.Add(articleAuthor);
            _context.SaveChanges();
        }

        /// <summary>
        /// Обновляет статью пользователя userName
        /// </summary>
        /// <param name="articleEntry"></param>
        /// <param name="article"></param>        
        public void UpdateArticle(Article articleEntry, Article article)
        {
            articleEntry.ArticleName = article.ArticleName;
            articleEntry.FileModelId = article.FileModelId;
            articleEntry.Pages = article.Pages;
            articleEntry.ScienceJournalId = article.ScienceJournalId;
            articleEntry.VipuskNumber = article.VipuskNumber;
            articleEntry.YearId = article.YearId;
            ////////
            /// + списки направлений, авторов и пр.
            if(article.ArticleNirSpecials!=null && article.ArticleNirSpecials.Count > 0)
            {
                foreach (var articleNirSpecial in article.ArticleNirSpecials)
                {
                    bool isExists = false;
                    foreach (var articleNirSpecialEntry in articleEntry.ArticleNirSpecials)
                    {
                        if(articleNirSpecialEntry.NirSpecialId == articleNirSpecial.NirSpecialId)
                        {
                            isExists = true;
                        }
                    }
                    if(!isExists)
                    {
                        articleEntry.ArticleNirSpecials.Add(articleNirSpecial);
                    }
                }
            }

            _context.SaveChanges();
        }
    }
}
