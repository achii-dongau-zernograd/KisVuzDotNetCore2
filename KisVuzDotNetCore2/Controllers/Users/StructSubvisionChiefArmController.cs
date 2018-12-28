using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.AspNetCore.Mvc;

namespace KisVuzDotNetCore2.Controllers.Users
{
    public class StructSubvisionChiefArmController : Controller
    {
        public IActionResult Oborudovanie()
        {
            List<Oborudovanie> oborudovanie=new List<Oborudovanie>();
            return View(oborudovanie);
        }
    }
}