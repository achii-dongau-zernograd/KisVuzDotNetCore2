using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Интерфейс репозитория мероприятий СДО
    /// </summary>
    public interface ILmsEventRepository
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
        
        /// <summary>
        /// Добавляет набор заданий к мероприятию
        /// </summary>
        /// <param name="lmsEventLmsTaskSet"></param>
        /// <returns></returns>
        Task AddLmsEventLmsTaskSet(LmsEventLmsTaskSet lmsEventLmsTaskSet);

        /// <summary>
        /// Удаляет набор заданий из списка наборов заданий к мероприятию
        /// </summary>
        /// <param name="lmsEventLmsTaskSet"></param>
        /// <returns></returns>
        Task RemoveLmsEventLmsTaskSet(LmsEventLmsTaskSet lmsEventLmsTaskSet);

        /// <summary>
        /// Возвращает запрос на выборку всех заданий мероприятия СДО
        /// </summary>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        Task<List<LmsTask>> GetLmsEventTasks(int lmsEventId);
        
        /// <summary>
        /// Добавляет пользователей, являющихся абитуриентами - участниками группы,
        /// сформированной приёмной комиссией для прохождения вступительных испытаний,
        /// к соответствующему мероприятию СДО
        /// </summary>
        /// <param name="lmsEventId"></param>
        /// <param name="entranceTestGroupId"></param>
        /// <returns></returns>
        Task AddAppUserLmsEventsByAbiturientsEntranceTestGroupAsync(int lmsEventId, int entranceTestGroupId);
    }
}
