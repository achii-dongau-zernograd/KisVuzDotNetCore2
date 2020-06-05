using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Abitur;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Priem
{
    [Authorize(Roles = "Администраторы, Приёмная комиссия, Приёмная комиссия (консультанты)")]
    public class AbiturientIndividualAchievmentsController : Controller
    {
        private readonly IAbiturientIndividualAchievmentRepository _abiturientIndividualAchievmentRepository;
        private readonly ISelectListRepository _selectListRepository;

        public AbiturientIndividualAchievmentsController(IAbiturientIndividualAchievmentRepository abiturientIndividualAchievmentRepository,
            ISelectListRepository selectListRepository)
        {
            _abiturientIndividualAchievmentRepository = abiturientIndividualAchievmentRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index()
        {
            var achievments = _abiturientIndividualAchievmentRepository.GetAbiturientIndividualAchievments();
            achievments = achievments.OrderByDescending(a => a.FileModel.UploadDate);

            return View(await achievments.ToListAsync());
        }

        #region Создание
        public IActionResult Create()
        {
            ViewBag.Abiturients = _selectListRepository.GetSelectListAbiturientsConfirmed();
            ViewBag.AbiturientIndividualAchievmentTypes = _selectListRepository.GetSelectListAbiturientIndividualAchievmentTypes();
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AbiturientIndividualAchievment abiturientIndividualAchievment,
            IFormFile uploadedFile)
        {
            if (abiturientIndividualAchievment == null)
                return NotFound();

            await _abiturientIndividualAchievmentRepository.Create(abiturientIndividualAchievment, uploadedFile);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Редактирование
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                return NotFound();

            var achievment = await _abiturientIndividualAchievmentRepository.GetAbiturientIndividualAchievmentAsync(id);
            if (achievment == null)
                return NotFound();

            ViewBag.AbiturientIndividualAchievmentTypes = _selectListRepository.GetSelectListAbiturientIndividualAchievmentTypes(achievment.AbiturientIndividualAchievmentTypeId);
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses(achievment.RowStatusId);

            return View(achievment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AbiturientIndividualAchievment abiturientIndividualAchievment,
            IFormFile uploadedFile)
        {
            if (abiturientIndividualAchievment == null)
                return NotFound();

            await _abiturientIndividualAchievmentRepository.Update(abiturientIndividualAchievment, uploadedFile);
                        
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Удаление
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();

            var achievment = await _abiturientIndividualAchievmentRepository.GetAbiturientIndividualAchievmentAsync(id);
            if (achievment == null)
                return NotFound();                        

            return View(achievment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AbiturientIndividualAchievment abiturientIndividualAchievment)
        {
            if (abiturientIndividualAchievment == null)
                return NotFound();

            await _abiturientIndividualAchievmentRepository.Remove(abiturientIndividualAchievment.AbiturientIndividualAchievmentId);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
