using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.LMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.LMS
{
    [Authorize]
    public class AppUserLmsEventsController : Controller
    {
        private readonly IAppUserLmsEventRepository _appUserLmsEventRepository;
        private readonly ILmsEventLmsTasksetAppUserAnswerRepository _lmsEventLmsTasksetAppUserAnswerRepository;
        private readonly IAbiturientRepository _abiturientRepository;

        public AppUserLmsEventsController(IAppUserLmsEventRepository appUserLmsEventRepository,
            ILmsEventLmsTasksetAppUserAnswerRepository lmsEventLmsTasksetAppUserAnswerRepository,
            IAbiturientRepository abiturientRepository)
        {
            _appUserLmsEventRepository = appUserLmsEventRepository;
            _lmsEventLmsTasksetAppUserAnswerRepository = lmsEventLmsTasksetAppUserAnswerRepository;
            _abiturientRepository = abiturientRepository;
        }

        #region Вступительные испытания
        /// <summary>
        /// Стартовая форма события СДО
        /// </summary>
        /// <param name="appUserLmsEventId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AppUserLmsEventStartForm(int appUserLmsEventId)
        {
            var appUserLmsEvent = await _appUserLmsEventRepository.GetAppUserLmsEventAsync(appUserLmsEventId);

            if (appUserLmsEvent.AppUser.UserName != User.Identity.Name)
                return NotFound();

            // Проверка наличия бланка регистрации на мероприятие
            bool isEntranceTestRegistrationFormExists = await _abiturientRepository.IsEntranceTestRegistrationFormExistsAsync(User.Identity.Name, appUserLmsEvent.LmsEventId);
            if (!isEntranceTestRegistrationFormExists)
                return RedirectToAction(nameof(CreateEntranceTestRegistrationForm), new { appUserLmsEventId });

            ViewBag.TasksNumberReceived = _lmsEventLmsTasksetAppUserAnswerRepository.GetLmsEventAppUserAnswers(User.Identity.Name, appUserLmsEvent.LmsEventId).Count();
            ViewBag.TasksNumber = (await _lmsEventLmsTasksetAppUserAnswerRepository.GetLmsEventTasksAsync(appUserLmsEvent.LmsEventId)).Count();

            return View(appUserLmsEvent);
        }

        /// <summary>
        /// Добавление бланка регистрации на мероприятие
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateEntranceTestRegistrationForm(int appUserLmsEventId)
        {
            var abiturient = await _abiturientRepository.GetAbiturientAsync(User.Identity.Name);
            if (abiturient == null) return NotFound();

            var appUserLmsEvent = await _appUserLmsEventRepository.GetAppUserLmsEventAsync(appUserLmsEventId);
            if (appUserLmsEvent == null) return NotFound();

            var entranceTestRegistrationForm = new EntranceTestRegistrationForm {
                AbiturientId   = abiturient.AbiturientId,
                Code           = abiturient.AbiturientId.ToString(),
                DisciplineName = appUserLmsEvent.LmsEvent.Description,
                LmsEventId     = appUserLmsEvent.LmsEventId,
                FirstName      = appUserLmsEvent.AppUser.FirstName,
                LastName       = appUserLmsEvent.AppUser.LastName,
                Patronymic     = appUserLmsEvent.AppUser.Patronymic,
                Date           = appUserLmsEvent.LmsEvent.DateTimeStart.Date
            };

            ViewBag.AppUserLmsEventId = appUserLmsEventId;

            return View(entranceTestRegistrationForm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntranceTestRegistrationForm(
            EntranceTestRegistrationForm entranceTestRegistrationForm,
            int appUserLmsEventId)
        {
            await _abiturientRepository.CreateEntranceTestRegistrationFormAsync(User.Identity.Name, entranceTestRegistrationForm);

            return RedirectToAction(nameof(AppUserLmsEventStartForm), new { appUserLmsEventId });
        }
        #endregion
    }
}
