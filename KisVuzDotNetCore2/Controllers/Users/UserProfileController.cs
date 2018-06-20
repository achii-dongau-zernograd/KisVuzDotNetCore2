using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers
{
    public class UserProfileController:Controller
    {
        private AppIdentityDBContext context;
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private Task<AppUser> CurrentUser => userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public UserProfileController(AppIdentityDBContext ctx, UserManager<AppUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            context = ctx;
            userManager = userMgr;
            roleManager = roleMgr;  
        }

        
        public async Task<IActionResult> Index(string id)
        {
            AppUser user;

            if (id==null)
            { 
                user = await CurrentUser;                
            }
            else
            {
                user = await userManager.FindByIdAsync(id);
                if (user == null)
                    return NotFound();
            }

            
            bool canEdit = false;// Флаг разрешения редактирования профиля
            if(HttpContext.User.Identity.Name != null)
            {
                AppUser currentUser = await CurrentUser;
                if (user.Id == currentUser.Id)
                {
                    canEdit = true;
                }
            }
            
                
            ViewBag.CanEdit = canEdit;
            return View(user);
        }
        
        public async Task<IActionResult> ChangeProfile()
        {
            AppUser user = await CurrentUser;
            UserProfileModificationModel profile = new UserProfileModificationModel();
            profile.AppUserPhoto = user.AppUserPhoto;
            profile.UserName = user.UserName;
            profile.Email = user.Email;
            profile.PhoneNumber = user.PhoneNumber;
            profile.LastName = user.LastName;
            profile.FirstName = user.FirstName;
            profile.Patronymic = user.Patronymic;
            profile.Birthdate = user.Birthdate;
            profile.EduLevelGroupId = user.EduLevelGroupId;
            profile.AcademicDegreeId = user.AcademicDegreeId;
            profile.AcademicStatId = user.AcademicStatId;
            profile.DateStartWorking = user.DateStartWorking;
            profile.DateStartWorkingSpec = user.DateStartWorkingSpec;

            ViewData["EduLevelGroups"] = new SelectList(context.EduLevelGroups, "EduLevelGroupId", "EduLevelGroupName", user.EduLevelGroupId);
            ViewData["AcademicDegrees"] = new SelectList(context.AcademicDegrees, "AcademicDegreeId", "AcademicDegreeName", user.AcademicDegreeId);
            ViewData["AcademicStats"] = new SelectList(context.AcademicStats, "AcademicStatId", "AcademicStatName", user.AcademicStatId);

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfile(UserProfileModificationModel user)
        {
            if(ModelState.IsValid)
            {
                AppUser changingUser = await CurrentUser;
                //changingUser.UserName    = user.UserName;
                //changingUser.Email       = user.Email;
                changingUser.PhoneNumber = user.PhoneNumber;
                changingUser.LastName    = user.LastName;
                changingUser.FirstName   = user.FirstName;
                changingUser.Patronymic  = user.Patronymic;
                changingUser.Birthdate   = user.Birthdate;
                changingUser.EduLevelGroupId = user.EduLevelGroupId;
                changingUser.AcademicDegreeId = user.AcademicDegreeId;
                changingUser.AcademicStatId = user.AcademicStatId;
                changingUser.DateStartWorking = user.DateStartWorking;
                changingUser.DateStartWorkingSpec = user.DateStartWorkingSpec;

                if (user.AppUserPhotoFile != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(user.AppUserPhotoFile.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)user.AppUserPhotoFile.Length);
                    }                                        
                    changingUser.AppUserPhoto = imageData;                    
                }

                await userManager.UpdateAsync(changingUser);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        
        [HttpPost]
        public async Task<IActionResult> AppUserFotoUpload(IFormFile AppUserPhoto)
        {
            if (AppUserPhoto != null)
            {
                byte[] imageData = null;
                
                using (var binaryReader = new BinaryReader(AppUserPhoto.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)AppUserPhoto.Length);
                }
                
                AppUser changingUser = await CurrentUser;
                changingUser.AppUserPhoto = imageData;
                await userManager.UpdateAsync(changingUser);                                
            }
            return RedirectToAction("Index");
        }
    }
}
