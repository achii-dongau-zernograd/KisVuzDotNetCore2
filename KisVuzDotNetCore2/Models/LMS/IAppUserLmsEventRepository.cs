using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Интерфейс репозитория привязок пользователей к событиям СДО
    /// </summary>
    public interface IAppUserLmsEventRepository
    {
        /// <summary>
        /// Добавляет привязку пользователя к событию СДО 
        /// </summary>
        /// <param name="appUserLmsEvent"></param>
        /// <returns></returns>
        Task<AppUserLmsEvent> AddAppUserLmsEventAsync(AppUserLmsEvent appUserLmsEvent);

        /// <summary>
        /// Возвращает привязку пользователя к событию по переданному УИД
        /// </summary>
        /// <param name="appUserLmsEventId"></param>
        /// <returns></returns>
        Task<AppUserLmsEvent> GetAppUserLmsEventAsync(int appUserLmsEventId);

        /// <summary>
        /// Удаляет участника мероприятия
        /// </summary>
        /// <param name="appUserLmsEvent"></param>
        /// <returns></returns>
        Task RemoveAppUserLmsEventAsync(AppUserLmsEvent appUserLmsEvent);
    }
}