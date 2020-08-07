using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.LMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.LMS
{
    /// <summary>
    /// Контроллер администрирования результатов работ пользователей в СДО
    /// </summary>
    [Authorize(Roles = "Администраторы СДО")]
    public class LmsEventLmsTaskSetAppUserAnswersCheckController : Controller
    {
        private readonly ILmsEventLmsTasksetAppUserAnswerRepository _lmsEventLmsTasksetAppUserAnswerRepository;
        private readonly ISelectListRepository _selectListRepository;

        public LmsEventLmsTaskSetAppUserAnswersCheckController(
            ILmsEventLmsTasksetAppUserAnswerRepository lmsEventLmsTasksetAppUserAnswerRepository,
            ISelectListRepository selectListRepository)
        {
            _lmsEventLmsTasksetAppUserAnswerRepository = lmsEventLmsTasksetAppUserAnswerRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index(int lmsEventId, string userName)
        {
            if (lmsEventId == 0)
                return RedirectToAction(nameof(SetLmsEventTypeId));

            if (string.IsNullOrWhiteSpace(userName))
                return RedirectToAction(nameof(ChooseUserName), new { lmsEventId });

            var lmsEventTasks = await _lmsEventLmsTasksetAppUserAnswerRepository.GetLmsEventTasksAsync(lmsEventId);
            if (lmsEventTasks == null)
                return NotFound();

            var userAnswers = await _lmsEventLmsTasksetAppUserAnswerRepository.GetLmsEventAppUserAnswers(userName, lmsEventId).ToListAsync();

            ViewBag.LmsEvent = await _lmsEventLmsTasksetAppUserAnswerRepository.GetLmsEvent(lmsEventId);

            ViewBag.LmsEventId = lmsEventId;
            ViewBag.UserName = userName;

            return View(lmsEventTasks);
        }

        #region Выставление оценки
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNumberOfPoints(int lmsEventId,
            string userName,
            int lmsEventLmsTasksetAppUserAnswerId,
            int mark)
        {
            ViewBag.LmsEventId = lmsEventId;
            ViewBag.UserName = userName;

            await _lmsEventLmsTasksetAppUserAnswerRepository.SetNumberOfPointsAsync(lmsEventLmsTasksetAppUserAnswerId, mark);

            return RedirectToAction(nameof(Index), new { lmsEventId, userName });
        }
        #endregion

        #region Выбор мероприятия и пользователя для просмотра

        /// <summary>
        /// Выбор типа мероприятия
        /// </summary>
        /// <returns></returns>
        public IActionResult SetLmsEventTypeId()
        {
            ViewBag.LmsEventTypes = _selectListRepository.GetSelectListLmsEventTypes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetLmsEventTypeId(LmsEventType lmsEventType)
        {
            return RedirectToAction(nameof(SetLmsEventId), new { lmsEventType.LmsEventTypeId });
        }

        /// <summary>
        /// Выбор мероприятия
        /// </summary>
        /// <returns></returns>
        public IActionResult SetLmsEventId(int lmsEventTypeId)
        {
            ViewBag.LmsEvents = _selectListRepository.GetSelectListLmsEvents(lmsEventTypeId, 0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetLmsEventId(LmsEvent lmsEvent)
        {
            return RedirectToAction(nameof(Index), new { lmsEvent.LmsEventId });
        }


        /// <summary>
        /// Выбор пользователя, результаты которого проверяем
        /// </summary>
        /// <returns></returns>
        public IActionResult ChooseUserName(int lmsEventId)
        {
            ViewBag.LmsEventId = lmsEventId;
            ViewBag.LmsEventParticipants = _selectListRepository.GetSelectListLmsEventParticipants(lmsEventId, 0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseUserName(int lmsEventId, AppUser appUser)
        {
            return RedirectToAction(nameof(Index), new { lmsEventId, appUser.UserName });
        }

        #endregion

        /// <summary>
        /// Редактирование ответа
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswerId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditLmsEventLmsTasksetAppUserAnswer(int lmsEventLmsTasksetAppUserAnswerId)
        {
            var entry = await _lmsEventLmsTasksetAppUserAnswerRepository.GetLmsEventAppUserAnswerAsync(lmsEventLmsTasksetAppUserAnswerId);
            
            return View(entry);
        }

        [HttpPost]
        public async Task<IActionResult> EditLmsEventLmsTasksetAppUserAnswer(LmsEventLmsTasksetAppUserAnswer lmsEventLmsTasksetAppUserAnswer,
            int lmsEventId,
            string userName)
        {
            await _lmsEventLmsTasksetAppUserAnswerRepository.UpdateLmsEventLmsTasksetAppUserAnswerAsync(lmsEventLmsTasksetAppUserAnswer);

            return RedirectToAction(nameof(Index), new { lmsEventId, UserName = userName });
        }


        /// <summary>
        /// Добавление ответа пользователя, выбранного из списка предложенных
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswerId"></param>
        /// <returns></returns>
        public async Task<IActionResult> LmsEventLmsTasksetAppUserAnswerTaskAnswers_Add(int lmsEventLmsTasksetAppUserAnswerId)
        {
            var entry = await _lmsEventLmsTasksetAppUserAnswerRepository.GetLmsEventAppUserAnswerAsync(lmsEventLmsTasksetAppUserAnswerId);

            var newItem = new LmsEventLmsTasksetAppUserAnswerTaskAnswer
            {
                LmsEventLmsTasksetAppUserAnswer = entry,
                LmsEventLmsTasksetAppUserAnswerId = entry.LmsEventLmsTasksetAppUserAnswerId
            };

            ViewBag.LmsTaskAnswers = _selectListRepository.GetSelectListLmsTaskAnswers(entry.LmsTaskId, 0);

            return View(newItem);
        }

        [HttpPost]
        public async Task<IActionResult> LmsEventLmsTasksetAppUserAnswerTaskAnswers_Add(
            LmsEventLmsTasksetAppUserAnswerTaskAnswer lmsEventLmsTasksetAppUserAnswerTaskAnswer,
            int lmsEventLmsTasksetAppUserAnswerId)
        {
            await _lmsEventLmsTasksetAppUserAnswerRepository.AddLmsEventLmsTasksetAppUserAnswerTaskAnswerAsync(lmsEventLmsTasksetAppUserAnswerTaskAnswer);
            
            return RedirectToAction(nameof(EditLmsEventLmsTasksetAppUserAnswer), new { lmsEventLmsTasksetAppUserAnswerId });
        }



        /// <summary>
        /// Удаление ответа пользователя, выбранного из списка
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswerTaskAnswerId"></param>
        /// <returns></returns>
        public async Task<IActionResult> LmsEventLmsTasksetAppUserAnswerTaskAnswers_Remove(int lmsEventLmsTasksetAppUserAnswerTaskAnswerId)
        {
            var entry = await _lmsEventLmsTasksetAppUserAnswerRepository.GetLmsEventLmsTasksetAppUserAnswerTaskAnswerAsync(lmsEventLmsTasksetAppUserAnswerTaskAnswerId);
            return View(entry);
        }

        [HttpPost]
        public async Task<IActionResult> LmsEventLmsTasksetAppUserAnswerTaskAnswers_Remove(
            LmsEventLmsTasksetAppUserAnswerTaskAnswer lmsEventLmsTasksetAppUserAnswerTaskAnswer)
        {
            await _lmsEventLmsTasksetAppUserAnswerRepository.RemoveLmsEventLmsTasksetAppUserAnswerTaskAnswerAsync(lmsEventLmsTasksetAppUserAnswerTaskAnswer.LmsEventLmsTasksetAppUserAnswerTaskAnswerId);

            return RedirectToAction(nameof(EditLmsEventLmsTasksetAppUserAnswer), new { lmsEventLmsTasksetAppUserAnswerTaskAnswer.LmsEventLmsTasksetAppUserAnswerId });
        }

    }
}
