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

        /// <summary>
        /// Возвращает количество непрочтённых сообщений
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        int? GetUnreadMessages(string userName);

        /// <summary>
        /// Возвращает патенты (свидетельства) пользователя userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<Patent> GetPatents(string userName);

        /// <summary>
        /// Возвращает объект пользователя по его имени
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        AppUser GetAppUser(string userName);

        /// <summary>
        /// Возвращает патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Patent GetPatent(int? id, string userName);

        /// <summary>
        /// Добавляет патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="article"></param>
        /// <param name="userName"></param>
        void CreatePatent(Patent patent, string userName);

        /// <summary>
        /// Обновляет патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="patentEntry"></param>
        /// <param name="patent"></param>
        void UpdatePatent(Patent patentEntry, Patent patent);

        /// <summary>
        /// Удаляет патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="patentId"></param>
        /// <param name="userName"></param>
        Patent RemovePatent(int patentId, string userName);

        /// <summary>
        /// Возвращает заявки пользователя userName
        /// на добавление научного журнала (сборника) в справочник
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<ScienceJournalAddingClaim> GetScienceJournalAddingClaims(string userName);

        /// <summary>
        /// Добавление заявки на добавление научного журнала в справочник
        /// </summary>
        /// <param name="newClaim"></param>
        void CreateClaimAddScienceJournal(ScienceJournalAddingClaim newClaim);

        /// <summary>
        /// Удаление заявки на добавление научного журнала в справочник
        /// </summary>
        /// <param name="claimEntry"></param>
        void RemoveClaimAddScienceJournal(ScienceJournalAddingClaim claimEntry);
    }
}
