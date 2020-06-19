using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Common
{
    [Authorize(Roles = "Администраторы, Приёмная комиссия, Приёмная комиссия (консультанты), Юротдел")]
    public class ContractsController : Controller
    {
        private readonly IContractRepository _contractRepository;
        private readonly ISelectListRepository _selectListRepository;
        private readonly IApplicationForAdmissionRepository _applicationForAdmissionRepository;

        public ContractsController(IContractRepository contractRepository,
            ISelectListRepository selectListRepository,
            IApplicationForAdmissionRepository applicationForAdmissionRepository)
        {
            _contractRepository = contractRepository;
            _selectListRepository = selectListRepository;
            _applicationForAdmissionRepository = applicationForAdmissionRepository;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Contract> contracts = _contractRepository.GetContracts();

            return View(await contracts.ToListAsync());
        }

        #region Создание
        public IActionResult Create()
        {
            ViewBag.ApplicationForAdmissions = _selectListRepository.GetSelectListApplicationForAdmissions();
            ViewBag.ContractTypes = _selectListRepository.GetSelectListContractTypes();
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract, IFormFile uploadedFile)
        {
            if(contract.ApplicationForAdmissionId != null && contract.ApplicationForAdmissionId != 0)
            {
                var applicationForAdmission = await _applicationForAdmissionRepository.GetApplicationForAdmissionAsync(contract.ApplicationForAdmissionId ?? 0);
                if (applicationForAdmission == null) return NotFound();

                contract.AppUserId = applicationForAdmission.Abiturient.AppUserId;
            }
            else
            {
                return NotFound();
            }

            await _contractRepository.CreateContractAsync(contract, uploadedFile);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Редактирование
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();

            var contract = await _contractRepository.GetContractAsync(id);

            ViewBag.ContractTypes = _selectListRepository.GetSelectListContractTypes(contract.ContractTypeId);
            ViewBag.RowStatuses = _selectListRepository.GetSelectListRowStatuses(contract.RowStatusId);

            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Contract contract, IFormFile uploadedFile)
        {
            await _contractRepository.UpdateContractAsync(contract, uploadedFile);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Удаление
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();

            var contract = await _contractRepository.GetContractAsync(id);
                        
            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Contract contract)
        {
            if (contract == null) return NotFound();

            var entry = await _contractRepository.GetContractAsync(contract.ContractId);
            await _contractRepository.RemoveContractAsync(entry);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
