using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Users
{
    /// <summary>
    /// Контроллер внешних ресурсов пользователя
    /// </summary>
    public class ExternalResourcesController : Controller
    {
        private IExternalResourcesRepository _externalResourcesRepository;
        private ISelectListRepository _selectListRepository;
        public ExternalResourcesController(IExternalResourcesRepository externalResourcesRepository, ISelectListRepository selectListRepository)
        {
            _externalResourcesRepository = externalResourcesRepository;
            _selectListRepository = selectListRepository;
        }

        public IActionResult Index()
        {
            var externalResources = _externalResourcesRepository.GetExternalResources();
            return View(externalResources.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.ExternalResourceTypes = _selectListRepository.GetSelectListExternalResourceTypes();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ExternalResource externalResource)
        {
            _externalResourcesRepository.AddExternalResource(externalResource);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var externalResource = _externalResourcesRepository.GetExternalResource(id);
            ViewBag.ExternalResourceTypes = _selectListRepository.GetSelectListExternalResourceTypes(externalResource.ExternalResourceTypeId);
            return View(externalResource);
        }
    }
}
