using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Files;
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
        IFileModelRepository _fileModelRepository;

        public UserProfileRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
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
                            .ThenInclude(a => a.ArticleAuthors)
                                .ThenInclude(aa => aa.Author)
                .Include(u => u.Author)
                    .ThenInclude(a => a.ArticleAuthors)
                        .ThenInclude(aa => aa.Article)
                            .ThenInclude(a=>a.FileModel)
                .Include(u => u.Author)
                    .ThenInclude(a => a.ArticleAuthors)
                        .ThenInclude(aa => aa.Article)
                            .ThenInclude(a => a.Year)
                .Include(u => u.Author)
                    .ThenInclude(a => a.ArticleAuthors)
                        .ThenInclude(aa => aa.Article)
                            .ThenInclude(a => a.RowStatus)

                .Include(u => u.Author)
                    .ThenInclude(a => a.PatentAuthors)
                        .ThenInclude(pa => pa.Patent)
                            .ThenInclude(p => p.RowStatus)
                .Include(u => u.Author)
                    .ThenInclude(a => a.PatentAuthors)
                        .ThenInclude(pa => pa.Patent)
                            .ThenInclude(p => p.FileModel)
                .Include(u => u.Author)
                    .ThenInclude(a => a.PatentAuthors)
                        .ThenInclude(pa => pa.Patent)
                            .ThenInclude(p => p.Year)
                .Include(u => u.Author)
                    .ThenInclude(a => a.PatentAuthors)
                        .ThenInclude(pa => pa.Patent)
                            .ThenInclude(p => p.PatentAuthors)
                                .ThenInclude(pa => pa.Author)
                .Include(u => u.Author)
                    .ThenInclude(a => a.PatentAuthors)
                        .ThenInclude(pa => pa.Patent)
                            .ThenInclude(p => p.Year)
                .Include(u => u.Author)
                    .ThenInclude(a => a.PatentAuthors)
                        .ThenInclude(pa => pa.Patent)
                            .ThenInclude(p => p.PatentNirSpecials)
                                .ThenInclude(pn => pn.NirSpecial)
                .Include(u => u.Author)
                    .ThenInclude(a => a.PatentAuthors)
                        .ThenInclude(pa => pa.Patent)
                            .ThenInclude(p => p.PatentVid)
                                .ThenInclude(pv => pv.PatentVidGroup)
                 .Include(u => u.Author)
                    .ThenInclude(a => a.PatentAuthors)
                        .ThenInclude(pa => pa.Patent)
                            .ThenInclude(p => p.PatentNirTemas)
                                .ThenInclude(pn => pn.NirTema)

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
            articleEntry.RowStatusId = article.RowStatusId;
            ////////
            /// + списки направлений, авторов и пр.
            if (article.ArticleNirSpecials!=null && article.ArticleNirSpecials.Count > 0)
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
                for(int i=1;i < articleEntry.ArticleAuthors.Count;i++)
                {
                    firstAuthorPart -= articleEntry.ArticleAuthors[i].AuthorPart;
                }

                if (firstAuthorPart < 0) firstAuthorPart = 0;
                articleEntry.ArticleAuthors[0].AuthorPart = firstAuthorPart;
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет статью пользователя userName
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Article RemoveArticle(int articleId, string userName)
        {
            var article = GetArticle(articleId, userName);
            if (article == null) return null;

            _context.Articles.Remove(article);
            if(article.FileModel!=null)
            {
                _fileModelRepository.RemoveFileModelAsync(article.FileModel);
            }
            _context.SaveChanges();

            return article;
        }

        /// <summary>
        /// Возвращает список патентов (свидетельств) пользователя userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<Patent> GetPatents(string userName)
        {
            List<Patent> patents = new List<Patent>();
            AppUser appUser = GetAppUser(userName);
            appUser.Author
                .ForEach(a =>
                    a.PatentAuthors
                        .ForEach(pa =>
                            patents.Add(pa.Patent)));

            return patents;
        }

        /// <summary>
        /// Возвращает патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Patent GetPatent(int? id, string userName)
        {
            Patent patent = new Patent();

            if (id == null)
            {
                return patent;
            }

            List<Patent> userPatents = GetPatents(userName);
            patent = userPatents.SingleOrDefault(p => p.PatentId == id);
            return patent;
        }

        /// <summary>
        /// Добавляет патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="patent"></param>
        /// <param name="userName"></param>
        public void CreatePatent(Patent patent, string userName)
        {
            if (patent.PatentId != 0) return;

            AppUser appUser = GetAppUser(userName);
            Author author = appUser.Author.FirstOrDefault();
            if (author == null)
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

            _context.Patents.Add(patent);

            PatentAuthor patentAuthor = new PatentAuthor
            {
                PatentId = patent.PatentId,
                AuthorId = author.AuthorId,
                AuthorPart = 1
            };
            _context.PatentAuthors.Add(patentAuthor);
            _context.SaveChanges();
        }

        public void UpdatePatent(Patent patentEntry, Patent patent)
        {
            patentEntry.PatentNumber = patent.PatentNumber;
            patentEntry.PatentName = patent.PatentName;
            patentEntry.PatentVidId = patent.PatentVidId;
            patentEntry.FileModelId = patent.FileModelId;
            patentEntry.YearId = patent.YearId;
            patentEntry.PatentOwner = patent.PatentOwner;
            patentEntry.IsACHII = patent.IsACHII;
            patentEntry.IsZarubejn = patent.IsZarubejn;
            patentEntry.RowStatusId = patent.RowStatusId;
            ////////
            /// + списки направлений, авторов и пр.
            if (patent.PatentNirSpecials != null && patent.PatentNirSpecials.Count > 0)
            {
                foreach (var patentNirSpecial in patent.PatentNirSpecials)
                {
                    bool isExists = false;
                    foreach (var patentNirSpecialEntry in patentEntry.PatentNirSpecials)
                    {
                        if (patentNirSpecialEntry.NirSpecialId == patentNirSpecial.NirSpecialId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        patentEntry.PatentNirSpecials.Add(patentNirSpecial);
                    }
                }
            }

            if (patent.PatentAuthors != null && patent.PatentAuthors.Count > 0)
            {
                foreach (var patentAuthor in patent.PatentAuthors)
                {
                    bool isExists = false;
                    foreach (var patentAuthorEntry in patentEntry.PatentAuthors)
                    {
                        if (patentAuthorEntry.PatentAuthorId == patentAuthor.PatentAuthorId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        patentEntry.PatentAuthors.Add(patentAuthor);
                    }
                }

                decimal firstAuthorPart = 1;
                for (int i = 1; i < patentEntry.PatentAuthors.Count; i++)
                {
                    firstAuthorPart -= patentEntry.PatentAuthors[i].AuthorPart;
                }

                if (firstAuthorPart < 0) firstAuthorPart = 0;
                patentEntry.PatentAuthors[0].AuthorPart = firstAuthorPart;
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="patentId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Patent RemovePatent(int patentId, string userName)
        {
            var patent = GetPatent(patentId, userName);
            if (patent == null) return null;

            _context.Patents.Remove(patent);
            if (patent.FileModel != null)
            {
                _fileModelRepository.RemoveFileModelAsync(patent.FileModel);
            }
            _context.SaveChanges();

            return patent;
        }

    }
}
