using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.LMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.LMS
{
    /// <summary>
    /// Контроллер "Банк заданий"
    /// </summary>
    [Authorize(Roles = "Администраторы СДО")]
    public class LmsTasksController : Controller
    {
        private readonly ILmsTaskRepository _lmsTaskRepository;
        private readonly ISelectListRepository _selectListRepository;

        public LmsTasksController(ILmsTaskRepository lmsTaskRepository,
            ISelectListRepository selectListRepository)
        {
            _lmsTaskRepository = lmsTaskRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index(LmsTasksFilterAndSortModel lmsTasksFilterAndSortModel)
        {
            ViewBag.LmsTasksFilterAndSortModel = lmsTasksFilterAndSortModel;
            
            ViewBag.AppUsers = _selectListRepository.GetSelectListLmsTaskAppUsers(lmsTasksFilterAndSortModel.FilterAppUserId);
            ViewBag.DisciplineNames = _selectListRepository.GetSelectListLmsTaskDisciplineNames(lmsTasksFilterAndSortModel.FilterDisciplineNameId);
            ViewBag.LmsTaskTypes = _selectListRepository.GetSelectListLmsTaskTypes(lmsTasksFilterAndSortModel.FilterLmsTaskTypeId);

            if (lmsTasksFilterAndSortModel.IsRequestDataImmediately)
            {
                var lmsTasks = _lmsTaskRepository.GetLmsTasks(lmsTasksFilterAndSortModel);
                lmsTasks = lmsTasks.OrderByDescending(t => t.DateTimeOfCreation);

                return View(await lmsTasks.ToListAsync());
            }
            else
                return View();
        }

        private void GetSelectListLmsTaskDisciplineNames(int filterDisciplineNameId)
        {
            throw new NotImplementedException();
        }

        #region Добавление задания
        public IActionResult CreateLmsTask()
        {
            ViewBag.AppUsersAuthors = _selectListRepository.GetSelectListAppUsersAuthors();
            ViewBag.LmsTaskTypes = _selectListRepository.GetSelectListLmsTaskTypes();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLmsTask(LmsTask lmsTask,
            IFormFile uploadedFile)
        {
            if (lmsTask == null) return NotFound();
            await _lmsTaskRepository.AddLmsTask(lmsTask, uploadedFile);

            //return RedirectToAction(nameof(CreateLmsTaskDisciplineNames), new { lmsTask.LmsTaskId });
            return RedirectToAction(nameof(CreateLmsTaskAnswers), new { lmsTask.LmsTaskId });
        }
        #endregion

        #region Добавление вариантов ответов
        public async Task<IActionResult> CreateLmsTaskAnswers(LmsTaskAnswer lmsTaskAnswer, IFormFile uploadedFile, bool addLmsTaskAnswer)
        {            
            lmsTaskAnswer.LmsTask = await _lmsTaskRepository.GetLmsTaskAsync(lmsTaskAnswer.LmsTaskId);

            if(addLmsTaskAnswer)
            {
                await _lmsTaskRepository.AddLmsTaskAnswer(lmsTaskAnswer, uploadedFile);
            }

            return View(lmsTaskAnswer);
        }
        #endregion

        #region Добавление привязки дисциплины к заданию СДО
        public async Task<IActionResult> CreateLmsTaskDisciplineNames(LmsTaskDisciplineName lmsTaskDisciplineName)
        {
            if(lmsTaskDisciplineName.LmsTaskId != 0)
            {
                lmsTaskDisciplineName.LmsTask = await _lmsTaskRepository.GetLmsTaskAsync(lmsTaskDisciplineName.LmsTaskId);

                if (lmsTaskDisciplineName.DisciplineNameId != 0)
                {
                    await _lmsTaskRepository.AddLmsTaskDisciplineName(lmsTaskDisciplineName.LmsTask, lmsTaskDisciplineName.DisciplineNameId);
                }
            }

            ViewBag.DisciplineNames = _selectListRepository.GetSelectListDisciplineNames();
            return View(lmsTaskDisciplineName);
        }
        #endregion

        #region Просмотр задания
        public async Task<IActionResult> DetailsLmsTask(int id)
        {
            var lmsTask = await _lmsTaskRepository.GetLmsTaskAsync(id);

            var lmsEventLmsTasksetAppUserAnswer = new LmsEventLmsTasksetAppUserAnswer { LmsTask = lmsTask };

            return View(lmsEventLmsTasksetAppUserAnswer);
        }
        #endregion

        #region Редактирование задания
        public async Task<IActionResult> EditLmsTask(int id)
        {
            var lmsTask = await _lmsTaskRepository.GetLmsTaskAsync(id);

            ViewBag.AppUsersAuthors = _selectListRepository.GetSelectListAppUsersAuthors(lmsTask.AppUserId);
            ViewBag.LmsTaskTypes = _selectListRepository.GetSelectListLmsTaskTypes(lmsTask.LmsTaskTypeId);

            return View(lmsTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLmsTask(LmsTask lmsTask,
            IFormFile uploadedFile)
        {
            if (lmsTask == null) return NotFound();
            await _lmsTaskRepository.UpdateLmsTaskAsync(lmsTask, uploadedFile);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Редактирование вариантов ответов
        public async Task<IActionResult> ListLmsTaskAnswers(int lmsTaskId)
        {
            var lmsTask = await _lmsTaskRepository.GetLmsTaskAsync(lmsTaskId);                       

            return View(lmsTask);
        }



        /// <summary>
        /// Добавление варианта ответа
        /// </summary>
        /// <param name="lmsTaskAnswerId"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateLmsTaskAnswer(int lmsTaskId)
        {
            var lmsTask = await _lmsTaskRepository.GetLmsTaskAsync(lmsTaskId);

            var lmsTaskAnswer = new LmsTaskAnswer
            {
                LmsTaskId = lmsTask.LmsTaskId,
                LmsTask = lmsTask
            };

            return View(lmsTaskAnswer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLmsTaskAnswer(LmsTaskAnswer lmsTaskAnswer,
            IFormFile uploadedFile)
        {
            await _lmsTaskRepository.AddLmsTaskAnswer(lmsTaskAnswer, uploadedFile);

            return RedirectToAction(nameof(ListLmsTaskAnswers), new { lmsTaskId = lmsTaskAnswer.LmsTaskId });
        }


        /// <summary>
        /// Редактирование варианта ответа
        /// </summary>
        /// <param name="lmsTaskAnswerId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditLmsTaskAnswer(int lmsTaskAnswerId)
        {
            var lmsTaskAnswer = await _lmsTaskRepository.GetLmsTaskAnswer(lmsTaskAnswerId);

            return View(lmsTaskAnswer);
        }

        [HttpPost]
        public async Task<IActionResult> EditLmsTaskAnswer(LmsTaskAnswer lmsTaskAnswer,
            IFormFile uploadedFile)
        { 
            await _lmsTaskRepository.UpdateLmsTaskAnswer(lmsTaskAnswer, uploadedFile);

            return RedirectToAction(nameof(ListLmsTaskAnswers), new { lmsTaskId = lmsTaskAnswer.LmsTaskId });
        }


        /// <summary>
        /// Удаление варианта ответа
        /// </summary>
        /// <param name="lmsTaskAnswerId"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteLmsTaskAnswer(int lmsTaskAnswerId)
        {
            var lmsTaskAnswer = await _lmsTaskRepository.GetLmsTaskAnswer(lmsTaskAnswerId);

            return View(lmsTaskAnswer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLmsTaskAnswer(LmsTaskAnswer lmsTaskAnswer)
        {
            await _lmsTaskRepository.RemoveLmsTaskAnswer(lmsTaskAnswer.LmsTaskAnswerId);

            return RedirectToAction(nameof(ListLmsTaskAnswers), new { lmsTaskId = lmsTaskAnswer.LmsTaskId });
        }
        #endregion

        #region Удаление задания
        public async Task<IActionResult> RemoveLmsTask(int id)
        {
            var lmsTask = await _lmsTaskRepository.GetLmsTaskAsync(id);                       

            return View(lmsTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLmsTask(LmsTask lmsTask)
        {
            if (lmsTask == null) return NotFound();

            var entry = await _lmsTaskRepository.GetLmsTaskAsync(lmsTask.LmsTaskId);
            await _lmsTaskRepository.RemoveLmsTaskAsync(entry);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
