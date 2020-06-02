using KisVuzDotNetCore2.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Common
{
    public class ContractsController : Controller
    {
        private readonly IContractRepository _contractRepository;

        public ContractsController(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Contract> contracts = _contractRepository.GetContracts();

            return View(await contracts.ToListAsync());
        }
    }
}
