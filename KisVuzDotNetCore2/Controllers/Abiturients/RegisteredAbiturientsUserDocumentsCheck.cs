using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Abiturients
{
    [Authorize(Roles = "Администраторы, Приёмная комиссия, Приёмная комиссия (консультанты)")]
    public class RegisteredAbiturientsUserDocumentsCheck : Controller
    {
        private readonly IUserDocumentRepository _userDocumentRepository;
        private readonly ISelectListRepository _selectListRepository;

        public RegisteredAbiturientsUserDocumentsCheck(IUserDocumentRepository userDocumentRepository,
            ISelectListRepository selectListRepository)
        {
            _userDocumentRepository = userDocumentRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index(string filterLastNameFragment, int? filterDocumentType, int? filterRowStatus, bool isRequestDataImmediately = false)
        {            
            ViewBag.filterLastNameFragment = filterLastNameFragment;
            ViewBag.filterDocumentType = filterDocumentType;
            ViewBag.filterRowStatus = filterRowStatus;

            ViewBag.UserDocumentTypes = _selectListRepository.GetSelectListAbiturientsUserDocumentTypes(filterDocumentType ?? 0);
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses(filterRowStatus ?? 0);
            
            var userDocuments = _userDocumentRepository.GetUserDocuments()
                .Where(ud => ud.AppUser.Abiturient != null && ud.AppUser.Abiturient.AbiturientStatusId == (int) AbiturientStatusEnum.ConfirmedAbiturient);

            if (!isRequestDataImmediately)
                return View();

            if (!string.IsNullOrWhiteSpace(filterLastNameFragment))
                userDocuments = userDocuments.Where(ud => ud.AppUser.LastName.Contains(filterLastNameFragment));
            if (filterDocumentType != null)
                userDocuments = userDocuments.Where(ud => ud.FileDataTypeId == filterDocumentType);
            if (filterRowStatus != null)
                userDocuments = userDocuments.Where(ud => ud.RowStatusId == filterRowStatus);
            
            return View(await userDocuments.ToListAsync());
        }

        public async Task<IActionResult> Edit(int id,
                                              string filterLastNameFragment,
                                              int? filterDocumentType,
                                              int? filterRowStatus)
        {
            ViewBag.filterLastNameFragment = filterLastNameFragment;
            ViewBag.filterDocumentType = filterDocumentType;
            ViewBag.filterRowStatus = filterRowStatus;

            var userDocument = await _userDocumentRepository.GetUserDocumentAsync(id);
            ViewBag.FileDataTypes = _selectListRepository.GetSelectListFileDataTypes(userDocument.FileDataTypeId);
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses(userDocument.RowStatusId ?? 0);
            return View(userDocument);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDocument userDocument, IFormFile uploadedFile,
                                              string filterLastNameFragment,
                                              int? filterDocumentType,
                                              int? filterRowStatus)
        {
            await _userDocumentRepository.UpdateUserDocument(userDocument, uploadedFile);
            return RedirectToAction(nameof(Index),
                new { filterLastNameFragment,
                      filterDocumentType = userDocument.FileDataTypeId,
                      filterRowStatus = userDocument.RowStatusId,
                      isRequestDataImmediately = true
            });
        }

        public async Task<IActionResult> Delete(int id,
                                                string filterLastNameFragment,
                                                int? filterDocumentType,
                                                int? filterRowStatus)
        {
            ViewBag.filterLastNameFragment = filterLastNameFragment;
            ViewBag.filterDocumentType = filterDocumentType;
            ViewBag.filterRowStatus = filterRowStatus;

            var userDocument = await _userDocumentRepository.GetUserDocumentAsync(id);

            return View(userDocument);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserDocument userDocument,
                                              string filterLastNameFragment,
                                              int? filterDocumentType,
                                              int? filterRowStatus)
        {
            await _userDocumentRepository.RemoveUserDocumentAsync(userDocument.UserDocumentId);
            return RedirectToAction(nameof(Index),
                new
                {
                    filterLastNameFragment,                    
                    isRequestDataImmediately = true
                });
        }
    }
}
