using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Abitur;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using KisVuzDotNetCore2.Models.Priem;

namespace KisVuzDotNetCore2.Controllers.Priem
{
    [Authorize(Roles = "Администраторы, Приёмная комиссия, Приёмная комиссия (консультанты)")]
    public class ConsentToEnrollmentsController : Controller
    {        
        private readonly IConsentToEnrollmentRepository _consentToEnrollmentRepository;
        private readonly ISelectListRepository _selectListRepository;

        public ConsentToEnrollmentsController(IConsentToEnrollmentRepository consentToEnrollmentRepository,
            ISelectListRepository selectListRepository)
        {            
            _consentToEnrollmentRepository = consentToEnrollmentRepository;
            _selectListRepository = selectListRepository;
        }

        // GET: ConsentToEnrollments
        public async Task<IActionResult> Index(ConsentToEnrollmentsFilterAndSortModel filterAndSortModel)
        {
            var consentToEnrollments = _consentToEnrollmentRepository
                .GetConsentToEnrollments(filterAndSortModel)
                .OrderByDescending(c => c.ChangingDateTime);

            ViewBag.FilterAndSortModel = filterAndSortModel;

            ViewBag.EduForms    = _selectListRepository.GetSelectListEduForms(filterAndSortModel.EduFormId);
            ViewBag.EduProfiles = _selectListRepository.GetSelectListEduProfileFullNames(filterAndSortModel.EduProfileId);
            ViewBag.QuotaTypes  = _selectListRepository.GetSelectListQuotaTypes(filterAndSortModel.QuotaTypeId);
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses(filterAndSortModel.RowStatusId);

            if (filterAndSortModel.IsRequestDataImmediately)
                return View(await consentToEnrollments.ToListAsync());
            else
                return View(new List<ConsentToEnrollment>());
        }

        // GET: ConsentToEnrollments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var consentToEnrollment = await _consentToEnrollmentRepository.GetConsentToEnrollmentAsync(id);
            if (consentToEnrollment == null)
            {
                return NotFound();
            }

            return View(consentToEnrollment);
        }

        // GET: ConsentToEnrollments/Create
        public IActionResult Create()
        {
            ViewBag.ApplicationForAdmissions = _selectListRepository.GetSelectListApplicationForAdmissions();
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses();
            return View();
        }

        // POST: ConsentToEnrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsentToEnrollment consentToEnrollment, IFormFile uploadedFile)
        {
            await _consentToEnrollmentRepository.CreateConsentToEnrollmentAsync(consentToEnrollment, uploadedFile);
            return RedirectToAction(nameof(Index));
        }

        // GET: ConsentToEnrollments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var consentToEnrollment = await _consentToEnrollmentRepository.GetConsentToEnrollmentAsync(id);
            if (consentToEnrollment == null)
            {
                return NotFound();
            }

            ViewBag.ApplicationForAdmissions = _selectListRepository.GetSelectListApplicationForAdmissions(consentToEnrollment.ApplicationForAdmissionId);
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses(consentToEnrollment.RowStatusId);
            return View(consentToEnrollment);
        }

        // POST: ConsentToEnrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConsentToEnrollment consentToEnrollment, IFormFile uploadedFile)
        {
            await _consentToEnrollmentRepository.UpdateConsentToEnrollmentAsync(consentToEnrollment, uploadedFile);
            return RedirectToAction(nameof(Index));
        }

        // GET: ConsentToEnrollments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var consentToEnrollment = await _consentToEnrollmentRepository.GetConsentToEnrollmentAsync(id);
            if (consentToEnrollment == null)
            {
                return NotFound();
            }

            return View(consentToEnrollment);
        }

        // POST: ConsentToEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consentToEnrollment = await _consentToEnrollmentRepository.GetConsentToEnrollmentAsync(id);
            await _consentToEnrollmentRepository.RemoveConsentToEnrollmentAsync(consentToEnrollment);
            return RedirectToAction(nameof(Index));
        }        
    }
}
