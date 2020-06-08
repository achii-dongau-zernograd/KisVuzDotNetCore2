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
    public class LmsEventRepository : ILmsEventRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IAppUserLmsEventRepository _appUserLmsEventsRepository;
        private readonly ILmsTaskSetRepository _lmsTaskSetRepository;

        public LmsEventRepository(AppIdentityDBContext context,
            IAppUserLmsEventRepository appUserLmsEventsRepository,
            ILmsTaskSetRepository lmsTaskSetRepository)
        {
            _context = context;
            _appUserLmsEventsRepository = appUserLmsEventsRepository;
            _lmsTaskSetRepository = lmsTaskSetRepository;
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
                .OrderBy(l => l.DateTimeStart)
                .Include(l => l.LmsEventLmsTaskSets)
                    .ThenInclude(lt=>lt.LmsTaskSet);

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

        /// <summary>
        /// Добавляет набор заданий к мероприятию
        /// </summary>
        /// <param name="lmsEventLmsTaskSet"></param>
        /// <returns></returns>
        public async Task AddLmsEventLmsTaskSet(LmsEventLmsTaskSet lmsEventLmsTaskSet)
        {
            if (lmsEventLmsTaskSet.LmsEventId == 0 ||
                lmsEventLmsTaskSet.LmsTaskSetId == 0)
                return;

            var isExists = await _context.LmsEventLmsTaskSets
                .AnyAsync(t => t.LmsEventId == lmsEventLmsTaskSet.LmsEventId &&
                                t.LmsTaskSetId == lmsEventLmsTaskSet.LmsTaskSetId);

            if(!isExists)
            {
                _context.LmsEventLmsTaskSets.Add(lmsEventLmsTaskSet);
                await _context.SaveChangesAsync();
            }    
        }

        /// <summary>
        /// Удаляет набор заданий из списка наборов заданий к мероприятию
        /// </summary>
        /// <param name="lmsEventLmsTaskSet"></param>
        /// <returns></returns>
        public async Task RemoveLmsEventLmsTaskSet(LmsEventLmsTaskSet lmsEventLmsTaskSet)
        {
            _context.LmsEventLmsTaskSets.Remove(lmsEventLmsTaskSet);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает запрос на выборку всех заданий мероприятия СДО
        /// </summary>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        public async Task<List<LmsTask>> GetLmsEventTasks(int lmsEventId)
        {
            var lmsEvent = await GetLmsEventAsync(lmsEventId);

            var lmsTasks = new List<LmsTask>();
            foreach (var lmsEventLmsTaskSet in lmsEvent.LmsEventLmsTaskSets)
            {
                var lmsTaskSetEntry = await _lmsTaskSetRepository.GetLmsTaskSetAsync(lmsEventLmsTaskSet.LmsTaskSetId);
                lmsTaskSetEntry.LmsTaskSetLmsTasks.ForEach(tst => lmsTasks.Add(tst.LmsTask));
            }

            return lmsTasks;
        }
    }
}
