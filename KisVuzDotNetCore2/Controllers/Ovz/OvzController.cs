using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Gradebook;
using KisVuzDotNetCore2.Models.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.ElGradebooks
{
    /// <summary>
    /// Контроллер для работы с ОВЗ
    /// </summary>
    [Authorize(Roles = "Администраторы, ОВЗ")]
    public class OvzController : Controller
    {       

        public OvzController()
        {
            
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
