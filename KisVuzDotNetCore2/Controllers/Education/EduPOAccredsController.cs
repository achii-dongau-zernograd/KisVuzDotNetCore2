using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.LMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Education
{
    /// <summary>
    /// Контроллер "Информация о профессионально-общественной и/или общественной аккредитации образовательной программы"
    /// </summary>
    [Authorize(Roles = "Администраторы, Учабная часть")]
    public class EduPOAccredsController : Controller
    {
        private readonly IEduPOAccredRepository _eduPOAccredRepository;
        private readonly ISelectListRepository _selectListRepository;

        public EduPOAccredsController(IEduPOAccredRepository eduPOAccredRepository,
            ISelectListRepository selectListRepository)
        {
            _eduPOAccredRepository = eduPOAccredRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index()
        {            
            ViewBag.SelectListEduNapravlFullNames = _selectListRepository.GetSelectListEduNapravlFullNames();

            var eduPOAccreds = _eduPOAccredRepository.GetEduPOAccreds();
            var data =  await eduPOAccreds                
                .ToListAsync();

            return View(data);
        }               

        #region Добавление
        public IActionResult Create()
        {
            ViewBag.SelectListEduNapravlFullNames = _selectListRepository.GetSelectListEduNapravlFullNames();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EduPOAccred eduPOAccred,
            IFormFile uploadedFile)
        {
            if (eduPOAccred == null) return NotFound();
            await _eduPOAccredRepository.AddAsync(eduPOAccred, uploadedFile);
                        
            return RedirectToAction(nameof(Index));
        }
        #endregion           

        
        #region Редактирование
        public async Task<IActionResult> Edit(int id)
        {
            var entry = await _eduPOAccredRepository.GetAsync(id);

            ViewBag.SelectListEduNapravlFullNames = _selectListRepository.GetSelectListEduNapravlFullNames();

            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EduPOAccred eduPOAccred,
            IFormFile uploadedFile)
        {
            if (eduPOAccred == null) return NotFound();
            await _eduPOAccredRepository.UpdateAsync(eduPOAccred, uploadedFile);

            return RedirectToAction(nameof(Index));
        }
        #endregion
                
        #region Удаление
        public async Task<IActionResult> Delete(int id)
        {
            var entry = await _eduPOAccredRepository.GetAsync(id);                       

            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EduPOAccred eduPOAccred)
        {
            if (eduPOAccred == null) return NotFound();

            var entry = await _eduPOAccredRepository.GetAsync(eduPOAccred.EduPOAccredId);
            await _eduPOAccredRepository.RemoveAsync(entry.EduPOAccredId);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
