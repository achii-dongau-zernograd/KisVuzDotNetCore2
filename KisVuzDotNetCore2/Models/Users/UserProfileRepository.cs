﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Nir;
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
    }
}
