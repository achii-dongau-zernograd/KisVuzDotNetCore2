﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Nir;
using KisVuzDotNetCore2.Models.UchPosobiya;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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
            var appUser = GetUsers()
                .SingleOrDefault(u => u.UserName == userName);

            return appUser;
        }

        private IQueryable<AppUser> GetUsers()
        {
            return _context.Users
                .Include(u => u.Author)
                    .ThenInclude(a => a.ArticleAuthors)
                        .ThenInclude(aa => aa.Article)
                            .ThenInclude(a => a.ScienceJournal)
                                .ThenInclude(s => s.ScienceJournalCitationBases)
                                    .ThenInclude(c => c.CitationBase)
                            ////////////////// Статьи //////////////////
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
                                        .ThenInclude(a => a.FileModel)
                            .Include(u => u.Author)
                                .ThenInclude(a => a.ArticleAuthors)
                                    .ThenInclude(aa => aa.Article)
                                        .ThenInclude(a => a.Year)
                            .Include(u => u.Author)
                                .ThenInclude(a => a.ArticleAuthors)
                                    .ThenInclude(aa => aa.Article)
                                        .ThenInclude(a => a.RowStatus)
                            ////////////////// Патенты //////////////////
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
                            ////////////////// Монографии //////////////////
                            .Include(u => u.Author)
                                .ThenInclude(ma => ma.MonografAuthors)
                                    .ThenInclude(m => m.Monograf)
                                        .ThenInclude(p => p.RowStatus)
                            .Include(u => u.Author)
                                .ThenInclude(a => a.MonografAuthors)
                                    .ThenInclude(pa => pa.Monograf)
                                        .ThenInclude(p => p.FileModel)
                            .Include(u => u.Author)
                                .ThenInclude(a => a.MonografAuthors)
                                    .ThenInclude(pa => pa.Monograf)
                                        .ThenInclude(p => p.Year)
                            .Include(u => u.Author)
                                .ThenInclude(a => a.MonografAuthors)
                                    .ThenInclude(pa => pa.Monograf)
                                        .ThenInclude(p => p.MonografNirSpecials)
                                            .ThenInclude(pn => pn.NirSpecial)
                             .Include(u => u.Author)
                                .ThenInclude(a => a.MonografAuthors)
                                    .ThenInclude(pa => pa.Monograf)
                                        .ThenInclude(p => p.MonografNirTemas)
                                            .ThenInclude(pn => pn.NirTema)
                             .Include(u => u.Author)
                                .ThenInclude(a => a.MonografAuthors)
                                    .ThenInclude(pa => pa.Monograf)
                                        .ThenInclude(p => p.MonografAuthors)
                                            .ThenInclude(ma => ma.Author)

                            .Include(u => u.ScienceJournalAddingClaims)
                                .ThenInclude(u => u.RowStatus)
                            .Include(u => u.UserAchievments)
                                .ThenInclude(ua => ua.UserAchievmentType);
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
        /// Возвращает количество непрочтённых сообщений
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int? GetUnreadMessages(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }
            
            var appUser = GetAppUser(userName);
            int numUnreadMessages = _context.UserMessages.Where(um => um.UserReceiverId == appUser.Id && um.IsReadedByReceiver!=true).Count();
            return numUnreadMessages;
        }

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
        
        /// <summary>
        /// Обновляет патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="patentEntry"></param>
        /// <param name="patent"></param>
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

        /// <summary>
        /// Возвращает заявки пользователя userName
        /// на добавление научного журнала (сборника) в справочник
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<ScienceJournalAddingClaim> GetScienceJournalAddingClaims(string userName)
        {
            var appUser = GetAppUser(userName);
            var claims = appUser.ScienceJournalAddingClaims;
            return claims;
        }

        /// <summary>
        /// Добавление заявки на добавление научного журнала в справочник
        /// </summary>
        /// <param name="newClaim"></param>
        public void CreateClaimAddScienceJournal(ScienceJournalAddingClaim newClaim)
        {
            _context.ScienceJournalAddingClaims.Add(newClaim);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаление заявки на добавление научного журнала в справочник
        /// </summary>
        /// <param name="claimEntry"></param>
        public void RemoveClaimAddScienceJournal(ScienceJournalAddingClaim claimEntry)
        {
            _context.ScienceJournalAddingClaims.Remove(claimEntry);
            _context.SaveChanges();
        }

        /// <summary>
        /// Возвращает список достижений пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<UserAchievment> GetAchievments(string userName)
        {
            var appUser = GetAppUser(userName);
            return appUser.UserAchievments.OrderByDescending(a=>a.Date).ToList();
        }

        /// <summary>
        /// Возвращает достижение пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserAchievment GetAchievment(int? id, string userName)
        {
            var appUser = GetAppUser(userName);
            if (appUser == null)
            {
                return null;
            }

            if( id == null )
            {
                return new UserAchievment { AppUserId = appUser.Id };
            }
            else
            {
                var userAchievment = appUser.UserAchievments.FirstOrDefault(a => a.UserAchievmentId == id);
                return userAchievment;
            }            
        }       

        /// <summary>
        /// Возвращает список монографий пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<Monograf> GetMonografs(string userName)
        {
            List<Monograf> monografs = new List<Monograf>();
            AppUser appUser = GetAppUser(userName);
            appUser.Author
                .ForEach(a =>
                    a.MonografAuthors
                        .ForEach(aa =>
                            monografs.Add(aa.Monograf)));

            return monografs;
        }

        
        /// <summary>
        /// Возвращает монографию пользователя userName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Monograf GetMonograf(int? id, string userName)
        {
            Monograf monograf = new Monograf();

            if (id == null)
            {
                return monograf;
            }

            List<Monograf> userMonografs = GetMonografs(userName);
            monograf = userMonografs.SingleOrDefault(a => a.MonografId == id);
            return monograf; 
        }
        
        /// <summary>
        /// Добавляет статью пользователя userName
        /// </summary>
        /// <param name="monograf"></param>
        /// <param name="userName"></param>
        public void CreateMonograf(Monograf monograf, string userName)
        {
            if (monograf.MonografId != 0) return;

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

            _context.Monografs.Add(monograf);

            MonografAuthor monografAuthor = new MonografAuthor
            {
                MonografId = monograf.MonografId,
                AuthorId = author.AuthorId,
                AuthorPart = 1
            };
            _context.MonografAuthors.Add(monografAuthor);
            _context.SaveChanges();
        }

        
        /// <summary>
        /// Обновляет монографию пользователя userName
        /// </summary>
        /// <param name="monografEntry"></param>
        /// <param name="monograf"></param>   
        public void UpdateMonograf(Monograf monografEntry, Monograf monograf)
        {
            monografEntry.MonografName = monograf.MonografName;
            monografEntry.FileModelId = monograf.FileModelId;
            monografEntry.YearId = monograf.YearId;
            monografEntry.RowStatusId = monograf.RowStatusId;
            ////////
            /// + списки направлений, авторов и пр.
            if (monograf.MonografNirSpecials != null && monograf.MonografNirSpecials.Count > 0)
            {
                foreach (var monografNirSpecial in monograf.MonografNirSpecials)
                {
                    bool isExists = false;
                    foreach (var monografNirSpecialEntry in monografEntry.MonografNirSpecials)
                    {
                        if (monografNirSpecialEntry.NirSpecialId == monografNirSpecial.NirSpecialId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        monografEntry.MonografNirSpecials.Add(monografNirSpecial);
                    }
                }
            }

            if (monograf.MonografAuthors != null && monograf.MonografAuthors.Count > 0)
            {
                foreach (var monografAuthor in monograf.MonografAuthors)
                {
                    bool isExists = false;
                    foreach (var monografAuthorEntry in monografEntry.MonografAuthors)
                    {
                        if (monografAuthorEntry.MonografAuthorId == monografAuthor.MonografAuthorId)
                        {
                            isExists = true;
                        }
                    }
                    if (!isExists)
                    {
                        monografEntry.MonografAuthors.Add(monografAuthor);
                    }
                }

                decimal firstAuthorPart = 1;
                for (int i = 1; i < monografEntry.MonografAuthors.Count; i++)
                {
                    firstAuthorPart -= monografEntry.MonografAuthors[i].AuthorPart;
                }

                if (firstAuthorPart < 0) firstAuthorPart = 0;
                monografEntry.MonografAuthors[0].AuthorPart = firstAuthorPart;
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет монографию пользователя userName
        /// </summary>
        /// <param name="monografId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Monograf RemoveMonograf(int monografId, string userName)
        {
            var monograf = GetMonograf(monografId, userName);
            if (monograf == null) return null;

            _context.Monografs.Remove(monograf);
            if (monograf.FileModel != null)
            {
                _fileModelRepository.RemoveFileModelAsync(monograf.FileModel);
            }
            _context.SaveChanges();

            return monograf;
        }

        /// <summary>
        /// Поиск пользователей по заданным параметрам
        /// </summary>
        /// <param name="appUserSearchModel"></param>
        /// <returns></returns>
        public IEnumerable<AppUser> FindAppUsers(AppUserSearchModel appUserSearchModel)
        {
            var users = _context.Users
                .Where(u => u.LastName.ToLower().Contains(appUserSearchModel.LastNameSearchFragment))
                .Include(u => u.Students)
                .Include(u => u.Teachers);

            return users;
        }
    }
}
