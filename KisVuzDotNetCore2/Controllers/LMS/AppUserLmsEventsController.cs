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

        public AppUserLmsEventsController(IAppUserLmsEventRepository appUserLmsEventRepository,
            ILmsEventLmsTasksetAppUserAnswerRepository lmsEventLmsTasksetAppUserAnswerRepository)
        {
            _appUserLmsEventRepository = appUserLmsEventRepository;
            _lmsEventLmsTasksetAppUserAnswerRepository = lmsEventLmsTasksetAppUserAnswerRepository;
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

            ViewBag.TasksNumberReceived = _lmsEventLmsTasksetAppUserAnswerRepository.GetLmsEventAppUserAnswers(User.Identity.Name, appUserLmsEvent.LmsEventId).Count();
            ViewBag.TasksNumber = (await _lmsEventLmsTasksetAppUserAnswerRepository.GetLmsEventTasksAsync(appUserLmsEvent.LmsEventId)).Count();

            return View(appUserLmsEvent);
        }
        #endregion
    }
}
