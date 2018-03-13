using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private Task<AppUser> CurrentUser => userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public UserProfileController(UserManager<AppUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            userManager = userMgr;
            roleManager = roleMgr;  
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            AppUser currentUser = await CurrentUser;
            return View(currentUser);
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
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfile(UserProfileModificationModel user/*[Required]AppUser user*/)
        {
            if(ModelState.IsValid)
            {
                AppUser changingUser = await CurrentUser;
                changingUser.UserName    = user.UserName;
                changingUser.Email       = user.Email;
                changingUser.PhoneNumber = user.PhoneNumber;
                changingUser.LastName    = user.LastName;
                changingUser.FirstName   = user.FirstName;
                changingUser.Patronymic  = user.Patronymic;
                changingUser.Birthdate   = user.Birthdate;

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
