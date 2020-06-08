using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Репозиторий привязок пользователей к событиям СДО
    /// </summary>
    public class AppUserLmsEventRepository : IAppUserLmsEventRepository
    {
        private readonly AppIdentityDBContext _context;

        public AppUserLmsEventRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет привязку пользователя к событию СДО 
        /// </summary>
        /// <param name="appUserLmsEvent"></param>
        /// <returns></returns>
        public async Task<AppUserLmsEvent> AddAppUserLmsEventAsync(AppUserLmsEvent appUserLmsEvent)
        {
            _context.AppUserLmsEvents.Add(appUserLmsEvent);
            await _context.SaveChangesAsync();
            return appUserLmsEvent;
        }

        /// <summary>
        /// Возвращает привязку пользователя к событию по переданному УИД
        /// </summary>
        /// <param name="appUserLmsEventId"></param>
        /// <returns></returns>
        public async Task<AppUserLmsEvent> GetAppUserLmsEventAsync(int appUserLmsEventId)
        {
            var appUserLmsEvent = await GetAppUserLmsEvents().SingleOrDefaultAsync(a => a.AppUserLmsEventId == appUserLmsEventId);

            return appUserLmsEvent;
        }

        public IQueryable<AppUserLmsEvent> GetAppUserLmsEvents()
        {
            var query = _context.AppUserLmsEvents
                .Include(a => a.AppUserLmsEventUserRole)
                .Include(a => a.AppUser)
                .Include(a => a.LmsEvent.LmsEventType.LmsEventTypeGroup)
                .Include(a => a.LmsEvent.LmsEventAdditionalMaterials)
                .Include(a => a.LmsEvent.LmsEventChatMessages)
                    .ThenInclude(c => c.AppUser)
                .Include(a => a.LmsEvent.LmsEventLmsTaskSets);

            return query;
        }

        /// <summary>
        /// Удаляет участника мероприятия
        /// </summary>
        /// <param name="appUserLmsEvent"></param>
        /// <returns></returns>
        public async Task RemoveAppUserLmsEventAsync(AppUserLmsEvent appUserLmsEvent)
        {
            _context.AppUserLmsEvents.Remove(appUserLmsEvent);
            await _context.SaveChangesAsync();
        }
    }
}
