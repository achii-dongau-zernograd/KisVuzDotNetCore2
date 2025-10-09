using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Common;
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

        /// <summary>
        /// Возвращает УИД пользователя по его имени
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetAppUserId(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName).Id;
        }

        public AppUser GetAppUser(string userName)
        {
            var appUser = GetUsers()
                .SingleOrDefault(u => u.UserName == userName);

            return appUser;
        }

        public IQueryable<AppUser> GetUsers()
        {
            return _context.Users
                //////////// Фактический адрес ////////////
                .Include(u => u.AddressCurrent.PopulatedLocality.District.GpsGeometryCenter)
                .Include(u => u.AddressCurrent.PopulatedLocality.District.Region.Country)
                //////////// Иностранные языки ////////////
                .Include(u => u.AppUserForeignLanguages)
                    .ThenInclude(aufl=> aufl.ForeignLanguage)
                /////////////// Абитуриенты ///////////////
                .Include(u => u.Abiturient.ApplicationForAdmissions)
                .Include(u => u.Abiturient.AbiturientStatus)
                ////////////////// Авторы /////////////////
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
                    .ThenInclude(ua => ua.UserAchievmentType)
                ///////////////// Преподаватели ////////////////////
                .Include(u => u.Teachers)
                /////////////////// Студенты ///////////////////////
                .Include(u => u.Students)
                    .ThenInclude(s => s.StudentGroup)
                        .ThenInclude(sg => sg.EduKurs)
                ///////////////// Документы пользователя ///////////
                .Include(u => u.UserDocuments)
                    .ThenInclude(ud => ud.RowStatus)
                .Include(u => u.UserDocuments)
                    .ThenInclude(ud => ud.FileModel)
                .Include(u => u.UserDocuments)
                    .ThenInclude(ud => ud.FileDataType)
                ///////////// Работы пользователя //////////////////
                .Include(u => u.UserWorks)
                    .ThenInclude(uw => uw.UserWorkReviews)
                        .ThenInclude(uwr => uwr.FileModel)
                .Include(u => u.UserAchievments)
                .Include(u => u.StructSubvisions)
                .Include(u => u.AppUserStructSubvisions)
                .Include(u => u.MessagesFromAppUserToStudentGroups)
                .Include(u => u.ProfessionalRetrainings)
                .Include(u => u.Qualifications)
                .Include(u => u.RefresherCourses)
                .Include(u => u.ScienceJournalAddingClaims);
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
        public void UpdatePatent(Nir.Patent patentEntry, Nir.Patent patent)
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
        public Nir.Patent RemovePatent(int patentId, string userName)
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
        public IQueryable<AppUser> FindAppUsers(AppUserSearchModel appUserSearchModel)
        {
            var query = GetUsers();

            if (!string.IsNullOrWhiteSpace(appUserSearchModel.LastNameSearchFragment))
            {
                query = query.Where(u => u.LastName.Contains(appUserSearchModel.LastNameSearchFragment));
            }

            if (!string.IsNullOrWhiteSpace(appUserSearchModel.EmailSearchFragment))
            {
                query = query.Where(u => u.Email.Contains(appUserSearchModel.EmailSearchFragment));
            }

            if (appUserSearchModel.AppUserStatusId != null)
            {
                query = query.Where(u => u.AppUserStatusId == appUserSearchModel.AppUserStatusId);
            }

            query = query.OrderBy(u => u.LastName);

            return query;
        }

        /// <summary>
        /// Определяет, является ли пользователь преподавателем
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsTeacherAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;

            var appUser = await _context.Users
                .Include(u => u.Teachers)
                .SingleOrDefaultAsync(u => u.UserName == userName);

            if (appUser.Teachers.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Устанавливает статус пользователя
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="appUserStatusEnum"></param>
        /// <returns></returns>
        public async Task SetAppUserStatusAsync(AppUser appUser, AppUserStatusEnum appUserStatusEnum)
        {
            appUser.AppUserStatusId = (int)appUserStatusEnum;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Устанавливает статус документа
        /// </summary>
        /// <param name="userDocument"></param>
        /// <param name="rowStatusEnum"></param>
        /// <returns></returns>
        public async Task SetUserDocumentStatusAsync(UserDocument userDocument, RowStatusEnum rowStatusEnum)
        {
            userDocument.RowStatusId = (int)rowStatusEnum;
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Возвращает запрос на выборку квалификаций пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<Qualification> GetQualifications(string userName)
        {
            var query = _context.Qualifications.Where(q => q.AppUserId == GetAppUser(userName).Id);
            return query;
        }



        /// <summary>
        /// Удаляет аккаунт пользователя и все связанные данные
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveAppUserAsync(string userName)
        {
            var appUser = GetAppUser(userName);

            if (appUser == null)
                throw new Exception($"Пользователь {userName} не найден!");

            // Удаление данных абитуриента
            if(appUser.Abiturient != null)
            {
                
            }

            // Удаляем все пользовательские сообщения
            var userMessages = await _context.UserMessages
                .Where(um => um.UserSenderId == appUser.Id || um.UserReceiverId == appUser.Id)
                .ToListAsync();
            _context.UserMessages.RemoveRange(userMessages); 
            await _context.SaveChangesAsync();

            //// Удаляем все групповые сообщения // Удаляется автоматически
            //if (appUser.MessagesFromAppUserToStudentGroups != null && appUser.MessagesFromAppUserToStudentGroups.Count > 0)
            //{
                
            //}
            if(appUser.UserDocuments != null && appUser.UserDocuments.Count > 0)
            {
                await _fileModelRepository.RemoveUserDocumentsAsync(appUser.UserDocuments);                
            }

            // Удаление работ пользователя
            if(appUser.UserWorks != null && appUser.UserWorks.Count > 0)
            {
                await RemoveUserWorksAsync(appUser.UserWorks);
            }

            // Удаление повышений квалификации
            if (appUser.RefresherCourses != null && appUser.RefresherCourses.Count > 0)
            {
                await RemoveRefresherCoursesAsync(appUser.RefresherCourses);
            }

            // Удаление профессиональной переподготовки
            if (appUser.ProfessionalRetrainings != null && appUser.ProfessionalRetrainings.Count > 0)
            {
                await RemoveProfessionalRetrainingsAsync(appUser.ProfessionalRetrainings);
            }
            


            var appUserLmsEvents = _context.AppUserLmsEvents.Where(e => e.AppUserId == appUser.Id);
            _context.AppUserLmsEvents.RemoveRange(appUserLmsEvents);

            _context.Users.Remove(appUser);
            await _context.SaveChangesAsync();

            //var user = userProfileRepository.GetAppUser(userName);

            //if (user!=null)
            //{ 
            //    IdentityResult result = await userManager.DeleteAsync(user);
            //    if (result.Succeeded)
            //        return RedirectToAction(nameof(Search));
            //    else
            //        AddErrorsFromResult(result);
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Пользователь не найден");
            //}
        }

        /// <summary>
        /// Удаляет проф. переподготовку
        /// </summary>
        /// <param name="professionalRetrainings"></param>
        /// <returns></returns>
        private async Task RemoveProfessionalRetrainingsAsync(List<ProfessionalRetraining> professionalRetrainings)
        {
            if (professionalRetrainings == null) return;

            var fileModelIds = new List<int>();

            foreach (var professionalRetraining in professionalRetrainings)
            {
                if(professionalRetraining.ProfessionalRetrainingFileId != 0)
                    fileModelIds.Add(professionalRetraining.ProfessionalRetrainingFileId);
            }

            foreach (var fileModelId in fileModelIds)
            {
                await _fileModelRepository.RemoveFileAsync(fileModelId);
                await _context.SaveChangesAsync();
            }
            _context.ProfessionalRetrainings.RemoveRange(professionalRetrainings);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет повышения квалификации
        /// </summary>
        /// <param name="refresherCourses"></param>
        /// <returns></returns>
        private async Task RemoveRefresherCoursesAsync(List<RefresherCourse> refresherCourses)
        {
            if (refresherCourses == null) return;

            var fileModelIds = new List<int>();

            foreach (var refresherCourse in refresherCourses)
            {
                fileModelIds.Add(refresherCourse.RefresherCourseFileId);
            }

            foreach (var fileModelId in fileModelIds)
            {
                await _fileModelRepository.RemoveFileAsync(fileModelId);
                await _context.SaveChangesAsync();
            }
            _context.RefresherCourses.RemoveRange(refresherCourses);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет работы пользователя
        /// </summary>
        /// <param name="userWorks"></param>
        /// <returns></returns>
        private async Task RemoveUserWorksAsync(List<UserWork> userWorks)
        {
            if (userWorks == null) return;

            var userWorkFileModelIds = new List<int>();
            foreach (var userWork in userWorks)
            {
                if(userWork.UserWorkReviews != null && userWork.UserWorkReviews.Count > 0)
                {
                    var userWorkReviewFileModelIds = new List<int>();
                    foreach (var userWorkReview in userWork.UserWorkReviews)
                    {
                        if(userWorkReview.FileModelId != null)
                            userWorkReviewFileModelIds.Add((int)userWorkReview.FileModelId);
                    }

                    foreach (var userWorkReviewFileModelId in userWorkReviewFileModelIds)
                    {
                        await _fileModelRepository.RemoveFileAsync(userWorkReviewFileModelId);                        
                        await _context.SaveChangesAsync();
                    }
                    _context.UserWorkReviews.RemoveRange(userWork.UserWorkReviews);
                    await _context.SaveChangesAsync();
                }

                if(userWork.FileModelId != null)
                    userWorkFileModelIds.Add((int)userWork.FileModelId);
            }

            foreach (var userWorkFileModelId in userWorkFileModelIds)
            {
                await _fileModelRepository.RemoveFileAsync(userWorkFileModelId);
                await _context.SaveChangesAsync();
            }

            _context.UserWorks.RemoveRange(userWorks);
            await _context.SaveChangesAsync();
        }


        #region Паспортные данные
        /// <summary>
        /// Добавляет паспортные данные
        /// </summary>
        /// <param name="passportData"></param>
        /// <returns></returns>
        public async Task AddPassportDataAsync(PassportData passportData)
        {
            _context.PassportDataSet.Add(passportData);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает паспортные данные пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<PassportData> GetPassportDataAsync(string userName)
        {
            var appUser = await _context.Users
                .Include(u => u.PassportData.Address.PopulatedLocality.PopulatedLocalityType)
                .Include(u => u.PassportData.Address.PopulatedLocality.District.GpsGeometryCenter)
                .Include(u => u.PassportData.Address.PopulatedLocality.District.Region.Country)
                .Include(u => u.PassportData.RowStatus)
                .Include(u => u.PassportData.UserDocument.FileDataType)
                .Include(u => u.PassportData.UserDocument.FileModel)
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (appUser == null) return null;
            if (appUser.PassportData == null) return null;

            return appUser.PassportData;
        }

        /// <summary>
        /// Проверяет наличие наспортных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsPassportDataExistsAsync(string userName)
        {
            var passportData = await GetPassportDataAsync(userName);

            if (passportData == null) return false;

            return true;
        }

        /// <summary>
        /// Проверяет наличие паспортных данных
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public bool IsPassportDataExists(AppUser appUser)
        {
            if(appUser.PassportData == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Добавляет иностранный язык
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="foreignLanguageId"></param>
        /// <returns></returns>
        public async Task AddAppUserForeignLanguage(AppUser appUser, int foreignLanguageId)
        {
            if (appUser == null) return;
            if (appUser.AppUserForeignLanguages == null)
            {
                appUser.AppUserForeignLanguages = new List<AppUserForeignLanguage>();
            }
            var appUserForeignLanguage = new AppUserForeignLanguage
            {
                ForeignLanguageId = foreignLanguageId
            };
            appUser.AppUserForeignLanguages.Add(appUserForeignLanguage);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавляет контакт ближайшего родственника
        /// </summary>
        /// <param name="familyMemberContact"></param>
        /// <returns></returns>
        public async Task AddFamilyMemberContact(FamilyMemberContact familyMemberContact)
        {
            _context.FamilyMemberContacts.Add(familyMemberContact);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет паспортные данные
        /// </summary>
        /// <param name="passportData"></param>
        /// <returns></returns>
        public async Task UpdatePassportDataAsync(PassportData passportData)
        {
            _context.PassportDataSet.Update(passportData);
            await _context.SaveChangesAsync();
        }

        #endregion

        public List<string> GetUsersNotStudentsAndNotTeachersAndNoRoles()
        {
            var usersNotStudentsAndNotTeachers = GetUsers()
                .Where(user => user.Students.Count == 0
                        && user.Teachers.Count==0)
                .Select(user => new { user.Id, user.UserName})
                .ToList();

            var usersWithRoles = _context.UserRoles
                .Select(ur=>ur.UserId)
                .Distinct()
                .ToList();

            var usersWithoutRoles = new List<string>();
            foreach (var userName in usersNotStudentsAndNotTeachers)
            {
                if (usersWithRoles.Contains(userName.Id))
                    continue;
                                
                usersWithoutRoles.Add(userName.UserName);
            }

            return usersWithoutRoles;
        }

    }
}
