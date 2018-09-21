using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Components
{
    public class MetodKomissiyaViewComponent : ViewComponent
    {
        private readonly IMetodKomissiyaRepository _metodKomissiyaRepository;

        public MetodKomissiyaViewComponent(IMetodKomissiyaRepository metodKomissiyaRepository)
        {
            _metodKomissiyaRepository = metodKomissiyaRepository;
        }

        public IViewComponentResult Invoke()
        {
            bool IsEduProgramUser = _metodKomissiyaRepository.IsMetodKomissiyaMember(User.Identity.Name);
                        
            return View(IsEduProgramUser);
        }
    }
}
