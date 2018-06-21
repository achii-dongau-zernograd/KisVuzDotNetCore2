using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KisVuzDotNetCore2.Controllers
{
    public class AbiturController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по СПО
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SrProf()
        {
            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по бакалавриату
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Bachelor()
        {
            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по специалитету
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Special()
        {
            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по магистратуре
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Magistr()
        {
            return View();
        }

        /// <summary>
        /// Подраздел для представления информации по аспирантуре
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Postgraduate()
        {
            return View();
        }
    }
}