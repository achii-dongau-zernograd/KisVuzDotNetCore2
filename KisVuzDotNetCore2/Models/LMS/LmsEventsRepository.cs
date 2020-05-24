using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Репозиторий событий СДО
    /// </summary>
    public class LmsEventsRepository : ILmsEventsRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IAppUserLmsEventsRepository _appUserLmsEventsRepository;

        public LmsEventsRepository(AppIdentityDBContext context,
            IAppUserLmsEventsRepository appUserLmsEventsRepository)
        {
            _context = context;
            _appUserLmsEventsRepository = appUserLmsEventsRepository;
        }

        
        /// <summary>
        /// Возвращает запрос на выборку всех мероприятий СДО
        /// </summary>
        /// <returns></returns>
        public IQueryable<LmsEvent> GetLmsEvents()
        {
            var query = _context.LmsEvents
                .Include(l => l.LmsEventType.LmsEventTypeGroup)
                .Include(l => l.AppUserLmsEvents)
                    .ThenInclude(l => l.AppUserLmsEventUserRole)
                .Include(l => l.AppUserLmsEvents)
                    .ThenInclude(l => l.AppUser)                
                        .ThenInclude(l => l.Abiturient.AppUser)
                .OrderBy(l => l.DateTimeStart);

            return query;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех мероприятий СДО, проводимых приёмной комиссией
        /// </summary>
        /// <returns></returns>
        public IQueryable<LmsEvent> GetLmsEventsPriem()
        {
            var query = GetLmsEvents();
            return query;
        }

        /// <summary>
        /// Возвращает мероприятие по переданному УИД
        /// </summary>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        public async Task<LmsEvent> GetLmsEventAsync(int lmsEventId)
        {
            var lmsEvent = await GetLmsEvents().SingleOrDefaultAsync(l => l.LmsEventId == lmsEventId);
            return lmsEvent;
        }

        /// <summary>
        /// Обновляет объект мероприятия
        /// и возвращает обновлённый объект из БД
        /// </summary>
        /// <param name="lmsEvent"></param>
        /// <returns></returns>
        public async Task<LmsEvent> UpdateLmsEventAsync(LmsEvent lmsEvent)
        {
            if (lmsEvent == null)
                return null;

            if (lmsEvent.LmsEventId == 0)
                return null;

            var entry = await GetLmsEventAsync(lmsEvent.LmsEventId);
            if (entry == null)
                return null;

            entry.LmsEventTypeId          = lmsEvent.LmsEventTypeId;
            entry.Description             = lmsEvent.Description;
            entry.WebLink                 = lmsEvent.WebLink;
            entry.DateTimeStart           = lmsEvent.DateTimeStart;
            entry.Duration                = lmsEvent.Duration;
            entry.IsLmsEventStartedManual = lmsEvent.IsLmsEventStartedManual;

            _context.LmsEvents.Update(entry);

            await _context.SaveChangesAsync();

            return entry;
        }

        /// <summary>
        /// Создаёт объект мероприятия
        /// и возвращает созданный объект из БД
        /// </summary>
        /// <param name="lmsEvent"></param>
        /// <returns></returns>
        public async Task<LmsEvent> CreateLmsEventAsync(LmsEvent lmsEvent)
        {
            if (lmsEvent == null) return null;
            if (lmsEvent.LmsEventId != 0) return null;

            _context.LmsEvents.Add(lmsEvent);
            await _context.SaveChangesAsync();

            return lmsEvent;
        }

        /// <summary>
        /// Добавляет привязку пользователя к событию и возвращает её из БД
        /// </summary>
        /// <param name="appUserLmsEvent"></param>
        /// <returns></returns>
        public async Task<AppUserLmsEvent> AddAppUserLmsEvent(AppUserLmsEvent appUserLmsEvent)
        {
            var entry = await _appUserLmsEventsRepository.AddAppUserLmsEventAsync(appUserLmsEvent);

            return entry;
        }

        /// <summary>
        /// Возвращает привязку пользователя к событию по переданному УИД
        /// </summary>
        /// <param name="appUserLmsEventId"></param>
        /// <returns></returns>
        public async Task<AppUserLmsEvent> GetAppUserLmsEventAsync(int appUserLmsEventId)
        {
            AppUserLmsEvent appUserLmsEvent = await _appUserLmsEventsRepository.GetAppUserLmsEventAsync(appUserLmsEventId);

            return appUserLmsEvent;
        }

        /// <summary>
        /// Удаляет участника мероприятия
        /// </summary>
        /// <param name="appUserLmsEvent"></param>
        /// <returns></returns>
        public async Task RemoveAppUserLmsEventAsync(AppUserLmsEvent appUserLmsEvent)
        {
            await _appUserLmsEventsRepository.RemoveAppUserLmsEventAsync(appUserLmsEvent);
        }
    }
}
