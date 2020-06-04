using KisVuzDotNetCore2.Infrastructure;
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
    [Authorize(Roles = "Администраторы, Приёмная комиссия, Юротдел")]
    public class ContractsController : Controller
    {
        private readonly IContractRepository _contractRepository;
        private readonly ISelectListRepository _selectListRepository;

        public ContractsController(IContractRepository contractRepository,
            ISelectListRepository selectListRepository)
        {
            _contractRepository = contractRepository;
            _selectListRepository = selectListRepository;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Contract> contracts = _contractRepository.GetContracts();

            return View(await contracts.ToListAsync());
        }

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
