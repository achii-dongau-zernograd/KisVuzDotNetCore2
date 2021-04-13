using KisVuzDotNetCore2.Models.Sign;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Admin
{
    public class SignController : Controller
    {
        ISignRepository _signRepository;

        public SignController(ISignRepository signRepository)
        {
            _signRepository = signRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Создаёт ЭЦП для всех неподписанных документов
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateSignForUnsignedDocuments()
        {
            int numSignedDocuments = await _signRepository.CreateSignForUnsignedDocuments();

            return View(numSignedDocuments);
        }
    }
}
