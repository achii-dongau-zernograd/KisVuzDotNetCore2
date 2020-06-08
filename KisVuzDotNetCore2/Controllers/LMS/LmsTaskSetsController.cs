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

        #region Работа со списком заданий
        public async Task<IActionResult> EditLmsTaskSetLmsTasks(LmsTaskSetLmsTask lmsTaskSetLmsTask)
        {
            if (lmsTaskSetLmsTask.LmsTaskSetId == 0)
                return NotFound();

            if(lmsTaskSetLmsTask.LmsTaskId != 0)
            {                
                await _lmsTaskSetRepository.AddLmsTaskSetLmsTaskAsync(lmsTaskSetLmsTask);                                
            }

            lmsTaskSetLmsTask.LmsTaskSet = await _lmsTaskSetRepository.GetLmsTaskSetAsync(lmsTaskSetLmsTask.LmsTaskSetId);

            ViewBag.LmsTasks = _lmsTaskRepository.GetLmsTasks();
            return View(lmsTaskSetLmsTask);
        }
        #endregion
    }
}
