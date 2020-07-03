using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Priem;
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
    public class ApplicationForAdmissionsController : Controller
    {
        private readonly IApplicationForAdmissionRepository _applicationForAdmissionRepository;
        private readonly ISelectListRepository _selectListRepository;

        public ApplicationForAdmissionsController(IApplicationForAdmissionRepository applicationForAdmissionRepository,
            ISelectListRepository selectListRepository)
        {
            _applicationForAdmissionRepository = applicationForAdmissionRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index(ApplicationForAdmissionsFilterAndSortModel filterAndSortModel)
        {
            var applicationForAdmissions = _applicationForAdmissionRepository.GetApplicationForAdmissions(filterAndSortModel);                       

            ViewBag.FilterAndSortModel = filterAndSortModel;

            ViewBag.EduForms = _selectListRepository.GetSelectListEduForms(filterAndSortModel.EduFormId);
            ViewBag.EducationDocuments = _selectListRepository.GetSelectListEducationDocumentsForAbiturients(filterAndSortModel.EducationDocumentFileDataTypeId);
            ViewBag.EduProfiles = _selectListRepository.GetSelectListEduProfileFullNames(filterAndSortModel.EduProfileId);
            ViewBag.QuotaTypes = _selectListRepository.GetSelectListQuotaTypes(filterAndSortModel.QuotaTypeId);
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses(filterAndSortModel.RowStatusId);

            if (filterAndSortModel.IsRequestDataImmediately)
                return View(await applicationForAdmissions.ToListAsync());
            else
                return View(new List<ApplicationForAdmission>());
        }

        #region Создание
        public IActionResult Create()
        {            
            ViewBag.Abiturients = _selectListRepository.GetSelectListAbiturientsConfirmed();
            ViewBag.EduProfiles = _selectListRepository.GetSelectListEduProfileFullNames();
            ViewBag.EduForms = _selectListRepository.GetSelectListEduFormsForAbiturient();
            ViewBag.QuotaTypes = _selectListRepository.GetSelectListQuotaTypes();
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses();

            var applicationForAdmission = new ApplicationForAdmission
            {
                CreationDate = DateTime.Now,
                RowStatusId = (int)RowStatusEnum.Confirmed,
                PriorityId = 1
            };

            return View(applicationForAdmission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationForAdmission applicationForAdmission, IFormFile uploadedFile)
        {
            if (applicationForAdmission == null) return NotFound();
            await _applicationForAdmissionRepository.CreateApplicationForAdmissionAsync(applicationForAdmission, uploadedFile);
                        
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Редактирование
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();
            var applicationForAdmission = await _applicationForAdmissionRepository.GetApplicationForAdmissionAsync(id);

            ViewBag.EduProfiles = _selectListRepository.GetSelectListEduProfileFullNames(applicationForAdmission.EduProfileId);
            ViewBag.EduForms = _selectListRepository.GetSelectListEduFormsForAbiturient(applicationForAdmission.EduFormId);
            ViewBag.QuotaTypes = _selectListRepository.GetSelectListQuotaTypes(applicationForAdmission.QuotaTypeId);
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses(applicationForAdmission.RowStatusId);

            return View(applicationForAdmission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationForAdmission applicationForAdmission, IFormFile uploadedFile)
        {
            if (applicationForAdmission == null) return NotFound();
            await _applicationForAdmissionRepository.UpdateApplicationForAdmissionAsync(applicationForAdmission, uploadedFile);


            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Удаление
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();
            var applicationForAdmission = await _applicationForAdmissionRepository.GetApplicationForAdmissionAsync(id);            

            return View(applicationForAdmission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ApplicationForAdmission applicationForAdmission)
        {
            if (applicationForAdmission == null) return NotFound();

            var entry = await _applicationForAdmissionRepository.GetApplicationForAdmissionAsync(applicationForAdmission.ApplicationForAdmissionId);
            if(entry == null) return NotFound();

            await _applicationForAdmissionRepository.RemoveApplicationForAdmissionAsync(entry);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
