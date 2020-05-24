using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Интерфейс репозитория мероприятий СДО
    /// </summary>
    public interface ILmsEventsRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку всех мероприятий СДО
        /// </summary>
        /// <returns></returns>
        IQueryable<LmsEvent> GetLmsEvents();

        /// <summary>
        /// Возвращает запрос на выборку всех мероприятий СДО,
        /// проводимых приёмной комиссией
        /// </summary>
        /// <returns></returns>
        IQueryable<LmsEvent> GetLmsEventsPriem();

        /// <summary>
        /// Возвращает объект мероприятия по переданному УИД
        /// </summary>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        Task<LmsEvent> GetLmsEventAsync(int lmsEventId);

        /// <summary>
        /// Обновляет объект мероприятия
        /// и возвращает обновлённый объект из БД
        /// </summary>
        /// <param name="lmsEvent"></param>
        /// <returns></returns>
        Task<LmsEvent> UpdateLmsEventAsync(LmsEvent lmsEvent);

        /// <summary>
        /// Создаёт объект мероприятия
        /// и возвращает созданный объект из БД
        /// </summary>
        /// <param name="lmsEvent"></param>
        /// <returns></returns>
        Task<LmsEvent> CreateLmsEventAsync(LmsEvent lmsEvent);
        
        /// <summary>
        /// Добавляет привязку пользователя к событию и возвращает её из БД
        /// </summary>
        /// <param name="appUserLmsEvent"></param>
        /// <returns></returns>
        Task<AppUserLmsEvent> AddAppUserLmsEvent(AppUserLmsEvent appUserLmsEvent);

        /// <summary>
        /// Возвращает привязку пользователя к событию по преданному УИД привязки
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
