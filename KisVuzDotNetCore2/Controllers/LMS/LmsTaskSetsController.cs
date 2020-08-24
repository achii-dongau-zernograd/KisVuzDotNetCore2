using KisVuzDotNetCore2.Infrastructure;
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
    /// Наборы заданий СДО
    /// </summary>
    [Authorize(Roles = "Администраторы СДО")]
    public class LmsTaskSetsController : Controller
    {
        private readonly ILmsTaskSetRepository _lmsTaskSetRepository;
        private readonly ILmsTaskRepository _lmsTaskRepository;
        private readonly ISelectListRepository _selectListRepository;

        public LmsTaskSetsController(ILmsTaskSetRepository lmsTaskSetsRepository,
            ILmsTaskRepository lmsTaskRepository,
            ISelectListRepository selectListRepository)
        {
            _lmsTaskSetRepository = lmsTaskSetsRepository;
            _lmsTaskRepository = lmsTaskRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index()
        {
            var lmsTaskSets = _lmsTaskSetRepository.GetLmsTaskSets();

            return View(await lmsTaskSets.ToListAsync());
        }

        #region Работа с наборами заданий
        public IActionResult CreateLmsTaskSet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLmsTaskSet(LmsTaskSet lmsTaskSet)
        {
            await _lmsTaskSetRepository.AddLmsTaskSet(lmsTaskSet);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Редактирование сущности "Набор заданий"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditLmsTaskSet(int id)
        {
            var lmsTaskSet = await _lmsTaskSetRepository.GetLmsTaskSetAsync(id);

            if (lmsTaskSet == null)
                return NotFound();

            return View(lmsTaskSet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLmsTaskSet(LmsTaskSet lmsTaskSet)
        {
            await _lmsTaskSetRepository.UpdateLmsTaskSet(lmsTaskSet);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Удаление сущности "Набор заданий"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteLmsTaskSet(int id)
        {
            var lmsTaskSet = await _lmsTaskSetRepository.GetLmsTaskSetAsync(id);

            if (lmsTaskSet == null)
                return NotFound();

            return View(lmsTaskSet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLmsTaskSet(LmsTaskSet lmsTaskSet)
        {
            await _lmsTaskSetRepository.RemoveLmsTaskSet(lmsTaskSet);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Работа со списком заданий
        public async Task<IActionResult> EditLmsTaskSetLmsTasks(LmsTaskSetLmsTask lmsTaskSetLmsTask,
            LmsTasksFilterAndSortModel lmsTasksFilterAndSortModel,
            string mode)
        {
            ViewBag.LmsTasksFilterAndSortModel = lmsTasksFilterAndSortModel;

            if (lmsTaskSetLmsTask.LmsTaskSetId == 0)
                return NotFound();

            if(lmsTaskSetLmsTask.LmsTaskId != 0)
            {
                if(mode == "AddTaskToTaskSet")
                    await _lmsTaskSetRepository.AddLmsTaskSetLmsTaskAsync(lmsTaskSetLmsTask);

                if (mode == "RemoveTaskFromTaskSet")
                    await _lmsTaskSetRepository.RemoveLmsTaskSetLmsTaskAsync(lmsTaskSetLmsTask);
            }            

            ViewBag.AppUsers = _selectListRepository.GetSelectListLmsTaskAppUsers(lmsTasksFilterAndSortModel.FilterAppUserId);
            ViewBag.DisciplineNames = _selectListRepository.GetSelectListLmsTaskDisciplineNames(lmsTasksFilterAndSortModel.FilterDisciplineNameId);
            ViewBag.LmsTaskTypes = _selectListRepository.GetSelectListLmsTaskTypes(lmsTasksFilterAndSortModel.FilterLmsTaskTypeId);

            if (lmsTasksFilterAndSortModel.IsRequestDataImmediately)
            {
                lmsTaskSetLmsTask.LmsTaskSet = await _lmsTaskSetRepository.GetLmsTaskSetAsync(lmsTaskSetLmsTask.LmsTaskSetId);

                ViewBag.LmsTasks = _lmsTaskRepository.GetLmsTasks(lmsTasksFilterAndSortModel);
                return View(lmsTaskSetLmsTask);
            }
            else
            {
                lmsTaskSetLmsTask.LmsTaskSet = await _lmsTaskSetRepository.GetLmsTaskSetAsync(lmsTaskSetLmsTask.LmsTaskSetId);

                ViewBag.LmsTasks = (await _lmsTaskSetRepository.GetLmsTaskSetAsync(lmsTaskSetLmsTask.LmsTaskSetId)).LmsTaskSetLmsTasks.Select(t => t.LmsTask);
                return View(lmsTaskSetLmsTask);                
            }                
        }

        /// <summary>
        /// Настройка порядка отображения вопросов для указанного набора заданий
        /// </summary>
        /// <param name="lmsTaskSetId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditLmsTaskSetLmsTasksOrder(int lmsTaskSetId)
        {
            var lmsTaskSetLmsTasks = await _lmsTaskSetRepository.GetLmsTaskSetAsync(lmsTaskSetId);
            ViewBag.LmsTaskSetId = lmsTaskSetId;
            return View(lmsTaskSetLmsTasks.LmsTaskSetLmsTasks);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLmsTaskSetLmsTasksOrder(int lmsTaskSetId,
            List<LmsTaskSetLmsTask> lmsTaskSetLmsTasks,
            int?[] lmsTaskSetLmsTaskOrder,
            int[]  lmsTaskSetLmsTaskId)
        {
            await _lmsTaskSetRepository.UpdateLmsTaskSetLmsTasksOrderAsync(lmsTaskSetLmsTaskId, lmsTaskSetLmsTaskOrder);

            return RedirectToAction(nameof(EditLmsTaskSetLmsTasksOrder), new { lmsTaskSetId });
        }
        #endregion
    }
}
