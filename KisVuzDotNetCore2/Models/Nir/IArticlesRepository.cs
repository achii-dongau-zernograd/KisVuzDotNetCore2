using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Репозиторий научных статей
    /// </summary>
    public interface IArticlesRepository
    {
        /// <summary>
        /// Подтверждение статьи
        /// </summary>
        /// <param name="articleId"></param>
        void ConfirmArticle(int articleId);

        /// <summary>
        /// Возвращает список научных статей
        /// </summary>
        /// <returns></returns>
        List<Article> GetArticles();

        /// <summary>
        /// Возвращает список научных статей, опубликованных до указанного года включительно
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        IQueryable<Article> GetArticles(int year);

        /// <summary>
        /// Возвращает статью
        /// </summary>
        /// <param name="articleId">УИД статьи</param>
        /// <returns></returns>
        Article GetArticle(int? articleId);

        /// <summary>
        /// Создаёт научную статью
        /// </summary>
        /// <param name="article"></param>        
        void AddArticle(Article article);
        

        /// <summary>
        /// Обновляет научную статью
        /// </summary>
        /// <param name="articleEntry"></param>
        /// <param name="article"></param>
        void UpdateArticle(Article articleEntry, Article article);

        /// <summary>
        /// Удаляет научную статью
        /// </summary>
        /// <param name="articleId"></param>
        Task RemoveArticleAsync(int articleId);

        /// <summary>
        /// Статьи, ожидающие подтверждения
        /// </summary>
        IEnumerable<Article> GetArticlesNotConfirmed();

        /// <summary>
        /// Удаляет статьи
        /// </summary>
        /// <param name="articlesToDelete"></param>
        /// <returns></returns>
        Task RemoveArticlesAsync(List<Article> articlesToDelete);
    }
}
