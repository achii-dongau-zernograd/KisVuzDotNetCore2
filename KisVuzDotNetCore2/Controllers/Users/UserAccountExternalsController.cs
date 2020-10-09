using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.Users
{
    /// <summary>
    /// Контроллер "Внешние ресурсы пользователей"
    /// </summary>
    public class UserAccountExternalsController : Controller
    {
        private readonly IUserAccountExternalsRepository _userAccountExternalsRepository;
        private readonly ISelectListRepository _selectListRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserAccountExternalsController(IUserAccountExternalsRepository userAccountExternalsRepository,
            ISelectListRepository selectListRepository,
            IUserProfileRepository userProfileRepository
            )
        {
            _userAccountExternalsRepository = userAccountExternalsRepository;
            _selectListRepository = selectListRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<IActionResult> Index()
        {
            var externalResources = await _userAccountExternalsRepository.GetUserAccountExternals(User.Identity.Name).ToListAsync();

            return View(externalResources);
        }

        public IActionResult Create()
        {
            ViewBag.ExternalResources = _selectListRepository.GetSelectListExternalResources();
            ViewBag.AppUserId = _userProfileRepository.GetAppUserId(User.Identity.Name);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserAccountExternal userAccountExternal)
        {
            await _userAccountExternalsRepository.AddAsync(userAccountExternal);
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Edit(int id)
        {
            var userAccountExternal = _userAccountExternalsRepository.GetUserAccountExternal(id);
            ViewBag.ExternalResources = _selectListRepository.GetSelectListExternalResources();
            
            return View(userAccountExternal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserAccountExternal userAccountExternal)
        {
            await _userAccountExternalsRepository.UpdateAsync(userAccountExternal);
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Delete(int id)
        {
            var userAccountExternal = _userAccountExternalsRepository.GetUserAccountExternal(id);
            ViewBag.ExternalResources = _selectListRepository.GetSelectListExternalResources();

            return View(userAccountExternal);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserAccountExternal userAccountExternal)
        {
            await _userAccountExternalsRepository.RemoveAsync(userAccountExternal.UserAccountExternalId);
            return RedirectToAction(nameof(Index));
        }
    }
}
