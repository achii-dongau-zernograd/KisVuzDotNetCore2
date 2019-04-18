using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.MTO;
using KisVuzDotNetCore2.Models.Sveden;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace KisVuzDotNetCore2.Controllers.Users
{
    public class StructSubvisionChiefArmController : Controller
    {
        IStructSubvisionChiefRepository _structSubvisionChiefRepository;
        public StructSubvisionChiefArmController(IStructSubvisionChiefRepository structSubvisionChiefRepository)
        {
            _structSubvisionChiefRepository = structSubvisionChiefRepository;
        }

        public IActionResult Oborudovanie(int? pomeshenieId)
        {            
            PomeshenieViewModel pomeshenieViewModel = _structSubvisionChiefRepository.GetPomeshenieViewModel(pomeshenieId);
            List<Oborudovanie> oborudovanie=new List<Oborudovanie>();
            return View(oborudovanie);
        }
    }
}