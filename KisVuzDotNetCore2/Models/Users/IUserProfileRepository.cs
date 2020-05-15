using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Nir;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Интерфейс профиля пользователя
    /// </summary>
    public interface IUserProfileRepository
    {
        /// <summary>
        /// Возвращает список достижений пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<UserAchievment> GetAchievments(string userName);

        /// <summary>
        /// Возвращает достижение пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        UserAchievment GetAchievment(int? id, string userName);

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
        /// Добавляет паспортные данные
        /// </summary>
        /// <param name="passportData"></param>
        /// <returns></returns>
        Task AddPassportDataAsync(PassportData passportData);

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
        /// Возвращает запрос на выборку квалификаций пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<Qualification> GetQualifications(string userName);

        /// <summary>
        /// Удаляет статью пользователя userName
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Article RemoveArticle(int articleId, string userName);
        
        /// <summary>
        /// Поиск пользователей по заданным параметрам
        /// </summary>
        /// <param name="appUserSearchModel"></param>
        /// <returns></returns>
        IEnumerable<AppUser> FindAppUsers(AppUserSearchModel appUserSearchModel);

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
        /// Возвращает паспортные данные указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<PassportData> GetPassportDataAsync(string userName);

        /// <summary>
        /// Проверяет наличие наспортных данных указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsPassportDataExistsAsync(string userName);

        /// <summary>
        /// Возвращает Id пользователя по его имени
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string GetAppUserId(string userName);
        
        /// <summary>
        /// Определяет, является ли пользователь преподавателем
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsTeacherAsync(string userName);

        /// <summary>
        /// Возвращает патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Patent GetPatent(int? id, string userName);

        /// <summary>
        /// Устанавливает статус пользователя
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="appUserStatusEnum"></param>
        /// <returns></returns>
        Task SetAppUserStatusAsync(AppUser appUser, AppUserStatusEnum appUserStatusEnum);

        /// <summary>
        /// Добавляет патент (свидетельство) пользователя userName
        /// </summary>
        /// <param name="article"></param>
        /// <param name="userName"></param>
        void CreatePatent(Patent patent, string userName);

        /// <summary>
        /// Удаляет аккаунт пользователя и все связанные данные
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RemoveAppUserAsync(string userName);

        /// <summary>
        /// Устанавливает статус документа
        /// </summary>
        /// <param name="userDocument"></param>
        /// <param name="rowStatusEnum"></param>
        /// <returns></returns>
        Task SetUserDocumentStatusAsync(UserDocument userDocument, RowStatusEnum rowStatusEnum);

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

        /// <summary>
        /// Возвращает список монографий пользователя userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<Monograf> GetMonografs(string userName);
        
        /// <summary>
        /// Возвращает монографию пользователя userName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Monograf GetMonograf(int? id, string userName);

        /// <summary>
        /// Добавляет монографию пользователя userName
        /// </summary>
        /// <param name="monograf"></param>
        /// <param name="userName"></param>
        void CreateMonograf(Monograf monograf, string userName);

        /// <summary>
        /// Обновляет монографию пользователя userName
        /// </summary>
        /// <param name="monografEntry"></param>
        /// <param name="monograf"></param>        
        void UpdateMonograf(Monograf monografEntry, Monograf monograf);

        /// <summary>
        /// Удаляет монографию пользователя userName
        /// </summary>
        /// <param name="monografId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>       
        Monograf RemoveMonograf(int monografId, string userName);


    }
}
