using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Nir;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Интерфейс профиля пользователя
    /// </summary>
    public interface IUserProfileRepository
    {
        /// <summary>
        /// Возвращает список статей пользователя userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<Article> GetArticles(string userName);

        /// <summary>
        /// Возвращает статью пользователя userName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Article GetArticle(int? id, string userName);

        /// <summary>
        /// Добавляет статью пользователя userName
        /// </summary>
        /// <param name="article"></param>
        /// <param name="userName"></param>
        void CreateArticle(Article article, string userName);

        /// <summary>
        /// Обновляет статью пользователя userName
        /// </summary>
        /// <param name="articleEntry"></param>
        /// <param name="article"></param>        
        void UpdateArticle(Article articleEntry, Article article);

        /// <summary>
        /// Удаляет статью пользователя userName
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Article RemoveArticle(int articleId, string userName);
    }
}
