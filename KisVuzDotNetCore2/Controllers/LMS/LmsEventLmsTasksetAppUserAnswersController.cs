using KisVuzDotNetCore2.Models.LMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.LMS
{
    /// <summary>
    /// Контроллер тестирования СДО
    /// </summary>
    [Authorize]
    public class LmsEventLmsTasksetAppUserAnswersController : Controller
    {
        private readonly ILmsEventLmsTasksetAppUserAnswerRepository _repository;

        public LmsEventLmsTasksetAppUserAnswersController(ILmsEventLmsTasksetAppUserAnswerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Testing(int lmsEventId)
        {
            // Получаем шаблон ответа с заполненным вопросом из списка, на который пользователь ещё не отвечал
            var newAppuserAnswerWithLmsTaskNotAnswered = await _repository.GetLmsTaskNotAnsweredAsync(User.Identity.Name, lmsEventId);

            // Если вопроса нет, значит тестирование окончено.
            // Показываем сообщение об окончании тестирования
            if (newAppuserAnswerWithLmsTaskNotAnswered == null)
                return RedirectToAction(nameof(TestingFinished), new { lmsEventId });
                        
            ViewBag.LmsEventId = lmsEventId;

            return View(newAppuserAnswerWithLmsTaskNotAnswered);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAnswerProcessing(int lmsEventId,
            LmsEventLmsTasksetAppUserAnswer lmsEventLmsTasksetAppUserAnswer,
            int[] choosedAnswers,
            IFormFile uploadedFile)
        {
            await _repository.AddAppUserAnswerAsync(User.Identity.Name, lmsEventId, lmsEventLmsTasksetAppUserAnswer, choosedAnswers, uploadedFile);

            return RedirectToAction(nameof(Testing), new { lmsEventId });
        }

        /// <summary>
        /// Форма сообщения об окончании тестирования
        /// </summary>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        public async Task<IActionResult> TestingFinished(int lmsEventId)
        {            
            ViewBag.TasksNumberReceived = _repository.GetLmsEventAppUserAnswers(User.Identity.Name, lmsEventId).Count();
            ViewBag.TasksNumber = (await _repository.GetLmsEventTasksAsync(lmsEventId)).Count();
            return View();
        }
    }
}
