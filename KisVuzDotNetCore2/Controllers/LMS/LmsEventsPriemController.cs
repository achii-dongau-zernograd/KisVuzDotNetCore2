using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.LMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Priem
{
    /// <summary>
    /// Контроллер планирования мероприятий СДО приёмной комиссией
    /// </summary>
    [Authorize(Roles="Администраторы, Приёмная комиссия")]
    public class LmsEventsPriemController : Controller
    {
        private readonly ILmsEventRepository _lmsEventsRepository;
        private readonly ISelectListRepository _selectListRepository;

        public LmsEventsPriemController(ILmsEventRepository lmsEventsRepository,
            ISelectListRepository selectListRepository)
        {
            _lmsEventsRepository = lmsEventsRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index()
        {
            var events = _lmsEventsRepository.GetLmsEventsPriem();
            return View(await events.ToListAsync());
        }

        #region Создание мероприятия
        public IActionResult CreateLmsEvent()
        {
            var lmsEvent = new LmsEvent
            {
                DateTimeStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0),
                LmsEventTypeId = (int)LmsEventTypesEnum.Priem_EntranceTest
            };

            ViewBag.LmsEventTypes = _selectListRepository.GetSelectListLmsEventTypes((int)LmsEventTypeGroupsEnum.Priem, lmsEvent.LmsEventTypeId);
            return View(lmsEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLmsEvent(LmsEvent lmsEvent)
        {
            await _lmsEventsRepository.CreateLmsEventAsync(lmsEvent);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Редактирование мероприятия
        public async Task<IActionResult> EditLmsEvent(int lmsEventId)
        {
            var lmsEvent = await _lmsEventsRepository.GetLmsEventAsync(lmsEventId);

            ViewBag.LmsEventTypes = _selectListRepository.GetSelectListLmsEventTypes((int)LmsEventTypeGroupsEnum.Priem, lmsEvent.LmsEventTypeId);
            return View(lmsEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLmsEvent(LmsEvent lmsEvent)
        {
            await _lmsEventsRepository.UpdateLmsEventAsync(lmsEvent);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Редактирование участников мероприятия
        public async Task<IActionResult> LmsEventParticipants(int lmsEventId)
        {
            var lmsEvent = await _lmsEventsRepository.GetLmsEventAsync(lmsEventId);

            return View(lmsEvent);
        }

        /// <summary>
        /// Добавление абитуриента в список участников мероприятия
        /// </summary>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        public async Task<IActionResult> LmsEventParticipantsAddAbiturient(int lmsEventId)
        {
            var lmsEvent = await _lmsEventsRepository.GetLmsEventAsync(lmsEventId);

            var appUserLmsEvent = new AppUserLmsEvent
            {
                LmsEventId = lmsEvent.LmsEventId,
                LmsEvent = lmsEvent,
                AppUserLmsEventUserRoleId = (int)AppUserLmsEventUserRolesEnum.Participant
            };

            ViewBag.AppUsersAbiturients = _selectListRepository.GetSelectListAppUsersAbiturientsConfirmed();

            return View(appUserLmsEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LmsEventParticipantsAddAbiturient(AppUserLmsEvent appUserLmsEvent)
        {
            await _lmsEventsRepository.AddAppUserLmsEvent(appUserLmsEvent);

            return RedirectToAction(nameof(LmsEventParticipants), new { lmsEventId = appUserLmsEvent.LmsEventId });
        }

        /// <summary>
        /// Удаление участника мероприятия
        /// </summary>
        /// <param name="appUserLmsEventId"></param>
        /// <returns></returns>
        public async Task<IActionResult> LmsEventParticipantsDeleteParticipant(int appUserLmsEventId)
        {
            var appUserLmsEvent = await _lmsEventsRepository.GetAppUserLmsEventAsync(appUserLmsEventId);

            return View(appUserLmsEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LmsEventParticipantsDeleteParticipant(AppUserLmsEvent appUserLmsEvent)
        {
            await _lmsEventsRepository.RemoveAppUserLmsEventAsync(appUserLmsEvent);

            return RedirectToAction(nameof(LmsEventParticipants), new { lmsEventId = appUserLmsEvent.LmsEventId });
        }
        #endregion

        #region Редактирование набора заданий для мероприятия
        public async Task<IActionResult> EditLmsEventLmsTaskSets(int lmsEventId)
        {
            var lmsEvent = await _lmsEventsRepository.GetLmsEventAsync(lmsEventId);
            if (lmsEvent == null) return NotFound();

            ViewBag.LmsTaskSets = _selectListRepository.GetSelectListLmsTaskSets();

            return View(lmsEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLmsEventLmsTaskSet(LmsEventLmsTaskSet lmsEventLmsTaskSet)
        {
            await _lmsEventsRepository.AddLmsEventLmsTaskSet(lmsEventLmsTaskSet);
            return RedirectToAction(nameof(EditLmsEventLmsTaskSets), new { lmsEventLmsTaskSet.LmsEventId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLmsEventLmsTaskSet(LmsEventLmsTaskSet lmsEventLmsTaskSet)
        {
            await _lmsEventsRepository.RemoveLmsEventLmsTaskSet(lmsEventLmsTaskSet);
            return RedirectToAction(nameof(EditLmsEventLmsTaskSets), new { lmsEventLmsTaskSet.LmsEventId });
        }
        #endregion
    }
}
