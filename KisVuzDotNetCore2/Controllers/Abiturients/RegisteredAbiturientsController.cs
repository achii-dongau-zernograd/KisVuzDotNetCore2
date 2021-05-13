using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Priem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Abiturients
{
    /// <summary>
    /// Контроллер для работы с зарегистрированными абитуриентами
    /// </summary>
    [Authorize(Roles = "Администраторы, Приёмная комиссия, Приёмная комиссия (консультанты)")]
    public class RegisteredAbiturientsController : Controller
    {
        private readonly IAbiturientRepository _abiturRepository;
        private readonly ISelectListRepository _selectListRepository;

        public RegisteredAbiturientsController(IAbiturientRepository abiturRepository,
            ISelectListRepository selectListRepository)
        {
            _abiturRepository = abiturRepository;
            _selectListRepository = selectListRepository;
        }

        /// <summary>
        /// Список абитуриентов
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(AbiturientsFilterAndSortModel filterAndSortModel)
        {
            ViewBag.AbiturientsFilterAndSortModel = filterAndSortModel;
            ViewBag.AbiturientStatuses = _selectListRepository.GetSelectListAbiturientStatuses(filterAndSortModel.FilterAbiturientStatus ?? 0);
            ViewBag.SubmittingDocumentsTypes = _selectListRepository.GetSubmittingDocumentsTypes(filterAndSortModel.FilterSubmittingDocumentsType ?? 0);
            ViewBag.EntranceTestGroups = _selectListRepository.GetSelectListEntranceTestGroups(filterAndSortModel.FilterEntranceTestGroupId ?? 0);

            ViewBag.IsUserConsultant = User.IsInRole("Приёмная комиссия (консультанты)") ? true : false;
                       

            var abiturs = _abiturRepository.GetAbiturients();
            
            if(! string.IsNullOrWhiteSpace(filterAndSortModel.FilterLastNameFragment))
                abiturs = abiturs.Where(a => a.AppUser.LastName.Contains(filterAndSortModel.FilterLastNameFragment));

            if (filterAndSortModel.FilterAbiturientStatus != null)
                abiturs = abiturs.Where(a => a.AbiturientStatusId == filterAndSortModel.FilterAbiturientStatus);

            if (filterAndSortModel.FilterSubmittingDocumentsType != null)
                abiturs = abiturs.Where(a => a.SubmittingDocumentsTypeId == filterAndSortModel.FilterSubmittingDocumentsType);

            if (filterAndSortModel.FilterEntranceTestGroupId != null)
                abiturs = abiturs.Where(a => a.EntranceTestGroupId == filterAndSortModel.FilterEntranceTestGroupId);

            if (filterAndSortModel.FilterIsEduDocumentOriginal == true)
                abiturs = abiturs.Where(a => a.IsEduDocumentOriginal == true);

            if (filterAndSortModel.FilterRegisteredFromDate != null)
                abiturs = abiturs.Where(a => a.AppUser.RegisterDateTime > filterAndSortModel.FilterRegisteredFromDate);

            abiturs = abiturs.OrderByDescending(a => a.AppUser.RegisterDateTime);

            if (filterAndSortModel.IsRequestDataImmediately)
                return View(abiturs.ToList());
            else
                return View();
        }

        /// <summary>
        /// Список дублирующихся абитуриентов
        /// </summary>
        /// <returns></returns>
        public IActionResult FindDuplicates(AbiturientsFilterAndSortModel filterAndSortModel)
        {
            ViewBag.AbiturientsFilterAndSortModel = filterAndSortModel;

            ViewBag.AbiturientStatuses = _selectListRepository.GetSelectListAbiturientStatuses(filterAndSortModel.FilterAbiturientStatus ?? 0);
            ViewBag.EntranceTestGroups = _selectListRepository.GetSelectListEntranceTestGroups(filterAndSortModel.FilterEntranceTestGroupId ?? 0);

            ViewBag.IsUserConsultant = User.IsInRole("Приёмная комиссия (консультанты)");

            var abiturs = _abiturRepository.GetAbiturients();
            var groups = abiturs.GroupBy(a => a.AppUser.GetFullName).Where(g => g.Count() > 1).ToList();

            var duplicateAbiturs = new List<Abiturient>();
            foreach(var group in groups)
            {
                foreach (var item in group)
                {
                    var abiturient = _abiturRepository.GetAbiturients().FirstOrDefault(a => a.AbiturientId == item.AbiturientId);
                    if(abiturient != null)
                        duplicateAbiturs.Add(abiturient);
                }                
            }

            return View(nameof(Index), duplicateAbiturs.OrderBy(a=>a.AppUser.GetFullName).ToList());
        }

        /// <summary>
        /// Удаление аккаунта и всех данных абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string userName)
        {
            var appUser = await _abiturRepository.GetAbiturientAsync(userName);
            return View(appUser);
        }

        /// <summary>
        /// Удаление аккаунта и всех данных абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userName, string markAppUserAccountToDelete)
        {            
            bool boolMarkAppUserAccountToDelete = false;
            if(markAppUserAccountToDelete =="on")
                boolMarkAppUserAccountToDelete = true;

            await _abiturRepository.RemoveAbiturientAsync(userName, boolMarkAppUserAccountToDelete);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Подробные сведения об абитуриенте
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string userName, string mode)
        {
            ViewBag.IsUserConsultant = User.IsInRole("Приёмная комиссия (консультанты)") ? true : false;

            var abiturient = await _abiturRepository.GetAbiturientAsync(userName);

            switch (mode)
            {
                // Режим изменения статуса абитуриента
                case "ChangeAbiturStatus":
                    ViewBag.Mode = mode;                    
                    ViewBag.AbiturientStatuses = _selectListRepository.GetSelectListAbiturientStatuses(abiturient.AbiturientStatusId);
                    break;
                // Режим сохранения изменения статуса абитуриента
                case "ChangeAbiturStatusSave":
                    string abiturientStatusIdString = HttpContext.Request.Query["AbiturientStatusId"];
                    int abiturientStatusId;
                    if(int.TryParse(abiturientStatusIdString, out abiturientStatusId))
                    {
                        await _abiturRepository.SetAbiturientStatusAsync(abiturient, (AbiturientStatusEnum)abiturientStatusId);
                        return RedirectToAction(nameof(Details), new { userName });
                    }
                    break;
                // Режим изменения номера группы для прохождения вступительных испытаний
                case "ChangeEntranceTestGroupId":
                    ViewBag.Mode = mode;
                    ViewBag.AbiturientStatuses = _selectListRepository.GetSelectListAbiturientStatuses(abiturient.AbiturientStatusId);
                    break;
                // Режим сохранения изменения номера группы для прохождения вступительных испытаний
                case "ChangeEntranceTestGroupIdSave":
                    string entranceTestGroupIdString = HttpContext.Request.Query["EntranceTestGroupId"];
                    int entranceTestGroupId;
                    if (int.TryParse(entranceTestGroupIdString, out entranceTestGroupId))
                    {
                        await _abiturRepository.SetAbiturientEntranceTestGroupIdAsync(abiturient, entranceTestGroupId);
                        return RedirectToAction(nameof(Details), new { userName });
                    }
                    else
                    {
                        await _abiturRepository.SetAbiturientEntranceTestGroupIdAsync(abiturient, null);
                        return RedirectToAction(nameof(Details), new { userName });
                    }
                // Режим добавления сотрудника-консультанта
                case "ChangeAppUserAbiturientConsultant":
                    ViewBag.Mode = mode;
                    ViewBag.AppUserAbiturientConsultants = await _selectListRepository.GetSelectListAppUserAbiturientConsultantsAsync();
                    break;
                // Режим сохранения сотрудника-консультанта
                case "ChangeAppUserAbiturientConsultantSave":
                    string appUserAbiturientConsultantId = HttpContext.Request.Query["AppUserAbiturientConsultantId"];
                    await _abiturRepository.SetAppUserAbiturientConsultantAsync(abiturient, appUserAbiturientConsultantId);
                    return RedirectToAction(nameof(Details), new { userName });
                
                // Режим изменения способа подачи документов
                case "ChangeSubmittingDocumentsType":
                    ViewBag.Mode = mode;
                    ViewBag.SubmittingDocumentsTypes = _selectListRepository.GetSubmittingDocumentsTypes(abiturient.SubmittingDocumentsTypeId);
                    break;
                // Режим сохранения изменения способа подачи документов
                case "ChangeSubmittingDocumentsTypeSave":
                    string submittingDocumentsTypeIdString = HttpContext.Request.Query["SubmittingDocumentsTypeId"];
                    int submittingDocumentsTypeId;
                    if (int.TryParse(submittingDocumentsTypeIdString, out submittingDocumentsTypeId))
                    {
                        await _abiturRepository.SetAbiturientSubmittingDocumentsTypeAsync(abiturient, submittingDocumentsTypeId);
                        return RedirectToAction(nameof(Details), new { userName });
                    }
                    break;
                // Режим изменения наличия оригинала документа об образовании
                case "ChangeIsEduDocumentOriginal":
                    ViewBag.Mode = mode;
                    //ViewBag.AbiturientStatuses = _selectListRepository.GetSelectListAbiturientStatuses(abiturient.AbiturientStatusId);
                    break;
                // Режим сохранения изменения наличия оригинала документа об образовании
                case "ChangeIsEduDocumentOriginalSave":
                    string isEduDocumentOriginalString = HttpContext.Request.Query["IsEduDocumentOriginal"];
                    if(isEduDocumentOriginalString.Contains("true,false"))
                    {
                        await _abiturRepository.SetIsEduDocumentOriginalAsync(abiturient, true);
                    }
                    else if(isEduDocumentOriginalString.Contains("false"))
                    {
                        await _abiturRepository.SetIsEduDocumentOriginalAsync(abiturient, false);
                    }
                    //int abiturientStatusId;
                    //if (int.TryParse(abiturientStatusIdString, out abiturientStatusId))
                    //{
                    //    await _abiturRepository.SetAbiturientStatusAsync(abiturient, (AbiturientStatusEnum)abiturientStatusId);
                    //    return RedirectToAction(nameof(Details), new { userName });
                    //}
                    break;

                default:
                    break;
            }

            return View(abiturient);
        }

        /// <summary>
        /// Подробные сведения о скан-копиях документов,
        /// загруженных абитуриентом
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IActionResult> UserDocuments(string userName,
            string mode,
            int userDocumentId,
            int rowStatusId,
            string remark)
        {
            var abiturient = await _abiturRepository.GetAbiturientAsync(userName);

            UserDocument userDocument = null;
            if (userDocumentId != 0)
            {
                userDocument = abiturient.AppUser.UserDocuments
                        .FirstOrDefault(ud => ud.UserDocumentId == userDocumentId);
            }

            switch (mode)
            {
                // Режим изменения статуса документа
                case "ChangeRowStatus":
                    ViewBag.Mode = "ChangeRowStatus";                    
                    ViewBag.UserDocumentId = userDocumentId;                    
                    ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses(userDocument.RowStatusId ?? (int)RowStatusEnum.NotConfirmed);
                    break;
                // Режим сохранения изменения статуса документа
                case "ChangeRowStatusSave":
                    await _abiturRepository.SetUserDocumentStatusAsync(userDocument, (RowStatusEnum)rowStatusId);
                    return RedirectToAction(nameof(UserDocuments), new { userName });
                // Режим изменения замечания к документу
                case "ChangeRowRemark":
                    ViewBag.Mode = "ChangeRowRemark";
                    ViewBag.UserDocumentId = userDocumentId;                    
                    break;
                // Режим сохранения изменения замечания к документу
                case "ChangeRowRemarkSave":
                    await _abiturRepository.SetUserDocumentRemarkAsync(userDocument, remark);
                    return RedirectToAction(nameof(UserDocuments), new { userName });
                default:
                    break;
            }

            return View(abiturient);
        }

        /// <summary>
        /// Удаление пользовательского документа
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UserDocumentDelete(string userName,            
            int userDocumentId)
        {
            var userDocument = await _abiturRepository.GetUserDocumentAsync(userName, userDocumentId);
            if (userDocument == null) return NotFound("Документ не найден");

            return View(userDocument);
        }

        /// <summary>
        /// Удаление пользовательского документа
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDocumentDeleteConfirmed(string userName,
            int userDocumentId)
        {
            await _abiturRepository.RemoveUserDocumentAsync(userName, userDocumentId);
            return RedirectToAction(nameof(UserDocuments), new { userName });
        }
    }
}
