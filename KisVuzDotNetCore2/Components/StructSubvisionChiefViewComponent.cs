using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Components
{    
    public class StructSubvisionChiefViewComponent : ViewComponent
    {
        private readonly IStructSubvisionChiefRepository _structSubvisionChiefRepository;

        public StructSubvisionChiefViewComponent(IStructSubvisionChiefRepository structSubvisionChiefRepository)
        {
            _structSubvisionChiefRepository = structSubvisionChiefRepository;
        }

        public IViewComponentResult Invoke()
        {
            bool IsEduProgramUser = _structSubvisionChiefRepository.IsStructSubvisionChief(User.Identity.Name);

            return View(IsEduProgramUser);
        }
    }
}
