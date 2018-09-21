using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Mvc;

namespace KisVuzDotNetCore2.Controllers.Struct
{
    /// <summary>
    /// Контроллер АРМ члена методкомиссии
    /// </summary>
    public class MetodKomissiyaMemberArmController : Controller
    {
        private readonly IMetodKomissiyaRepository _metodKomissiyaRepository;
        private readonly ISelectListRepository _selectListRepository;

        public MetodKomissiyaMemberArmController(IMetodKomissiyaRepository metodKomissiyaRepository,
            ISelectListRepository selectListRepository)
        {
            _metodKomissiyaRepository = metodKomissiyaRepository;
            _selectListRepository = selectListRepository;
        }

        /// <summary>
        /// Работа с образовательными программами
        /// </summary>
        /// <returns></returns>
        public IActionResult EduPrograms()
        {
            var eduPrograms = _metodKomissiyaRepository.GetEduProgramsByUserName(User.Identity.Name);
            return View(eduPrograms);
        }
    }
}