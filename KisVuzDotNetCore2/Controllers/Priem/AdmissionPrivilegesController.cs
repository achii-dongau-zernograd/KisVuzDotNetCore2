using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models;
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
    public class AdmissionPrivilegesController : Controller
    {
        private readonly IAdmissionPrivilegeRepository _admissionPrivilegeRepository;
        private readonly ISelectListRepository _selectListRepository;

        public AdmissionPrivilegesController(IAdmissionPrivilegeRepository admissionPrivilegeRepository,
            ISelectListRepository selectListRepository)
        {
            _admissionPrivilegeRepository = admissionPrivilegeRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index()
        {
            var admissionPrivileges = _admissionPrivilegeRepository.GetAdmissionPrivileges();
            admissionPrivileges = admissionPrivileges.OrderByDescending(q=>q.FileModel.UploadDate);

            return View(await admissionPrivileges.ToListAsync());
        }

        #region Создание
        public IActionResult Create()
        {
            ViewBag.ApplicationForAdmissions = _selectListRepository
                .GetSelectListApplicationForAdmissions();
            ViewBag.AdmissionPrivilegeTypes = _selectListRepository
                .GetSelectListAdmissionPrivilegeTypes();
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdmissionPrivilege admissionPrivilege,
            IFormFile uploadedFile)
        {
            if (admissionPrivilege == null)
                return NotFound();

            await _admissionPrivilegeRepository.CreateAdmissionPrivilegeAsync(admissionPrivilege, uploadedFile);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Редактирование
        public async Task<IActionResult> Edit(int id)
        {
            var entry = await _admissionPrivilegeRepository.GetAdmissionPrivilegeAsync(id);

            ViewBag.ApplicationForAdmissions = _selectListRepository
                .GetSelectListApplicationForAdmissions(entry.ApplicationForAdmission.AbiturientId,
                entry.ApplicationForAdmissionId);
            ViewBag.AdmissionPrivilegeTypes = _selectListRepository
                .GetSelectListAdmissionPrivilegeTypes(entry.AdmissionPrivilegeTypeId);
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses(entry.RowStatusId);

            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdmissionPrivilege admissionPrivilege,
            IFormFile uploadedFile)
        {
            if (admissionPrivilege == null)
                return NotFound();

            await _admissionPrivilegeRepository.UpdateAdmissionPrivilegeAsync(admissionPrivilege, uploadedFile);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Удаление
        public async Task<IActionResult> Delete(int id)
        {
            var entry = await _admissionPrivilegeRepository.GetAdmissionPrivilegeAsync(id);
                        
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AdmissionPrivilege admissionPrivilege)
        {
            if (admissionPrivilege == null)
                return NotFound();

            var entry = await _admissionPrivilegeRepository.GetAdmissionPrivilegeAsync(admissionPrivilege.AdmissionPrivilegeId);

            await _admissionPrivilegeRepository.RemoveAdmissionPrivilegeAsync(entry);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
