using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers
{
    /// <summary>
    /// Контроллер управления учётными записями пользователей
    /// </summary>
    [Authorize(Roles = "Администраторы")]
    public class UserAdminController : Controller
    {
        #region Закрытые поля        
        private UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        private AppIdentityDBContext context;
        private IUserProfileRepository userProfileRepository;
        #endregion

        #region Конструктор
        public UserAdminController(UserManager<AppUser> usrMgr,
            IUserValidator<AppUser> userValid,
            IPasswordValidator<AppUser> passValid,
            IPasswordHasher<AppUser> passwordHash,
            AppIdentityDBContext ctx,
            IUserProfileRepository userProfileRepo
            )
        {            
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            context = ctx;
            userProfileRepository = userProfileRepo;
        }
        #endregion

        #region Index, Search
        public ViewResult Index()
        {
            var users = context.Users
                .Include(u=>u.Students)
                .Include(u=>u.Teachers);
            return View(users);
        }

        public ViewResult Search(string LastNameSearchFragment)
        {
            var model = new AppUserSearchModel
            {
                LastNameSearchFragment = LastNameSearchFragment
            };
            return View(model);
        }

        [HttpPost]        
        public IActionResult Search([FromForm]AppUserSearchModel appUserSearchModel, [FromForm]string LastNameSearchFragment)
        {
            if (LastNameSearchFragment == null)
                return RedirectToAction(nameof(Search));

            appUserSearchModel.LastNameSearchFragment = this.ControllerContext.HttpContext.Request.Form["LastNameSearchFragment"];
                                            
            ViewBag.FindedAppUsers = userProfileRepository.FindAppUsers(appUserSearchModel);

            return View(appUserSearchModel);
        }
        #endregion

        #region Create
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AppUserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }        
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);

            if(user!=null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Search));
                else
                    AddErrorsFromResult(result);
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(nameof(Search));
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id,
            string email,
            string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await userValidator.ValidateAsync(userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if(!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, password);
                    if(validPass.Succeeded)
                    {
                        user.Password = password;
                        user.PasswordHash = passwordHasher.HashPassword(user, password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if((validEmail.Succeeded && validPass==null) || (validEmail.Succeeded && password!=string.Empty&&validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {
                        return RedirectToAction(nameof(Search), new { LastNameSearchFragment = user.LastName });
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(user);
        }
        #endregion

        #region Редактирование ролей пользователя
        /// <summary>
        /// Редактирование ролей пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditUserRoles(string id)
        {
            var appUser = await userManager.FindByIdAsync(id);
            var roles = context.Roles.ToList();
            var userRoles = await userManager.GetRolesAsync(appUser);

            ViewBag.Id = id;
            ViewBag.Roles = roles;
            ViewBag.UserRoles = userRoles;

            return View();
        }

        public async Task<IActionResult> ChangeUserRoles(string id, string[] selectedRoles)
        {
            var appUser = await userManager.FindByIdAsync(id);
            var roles = context.Roles.ToList();
            var userRoles = await userManager.GetRolesAsync(appUser);

            foreach (var role in roles)
            {
                if (userRoles.Contains(role.Name) && !selectedRoles.Contains(role.Name))
                {
                    await userManager.RemoveFromRoleAsync(appUser, role.Name);
                    continue;
                }

                if (!userRoles.Contains(role.Name) && selectedRoles.Contains(role.Name))
                {
                    await userManager.AddToRoleAsync(appUser, role.Name);                    
                }
            }

            return RedirectToAction(nameof(Search), new { LastNameSearchFragment = appUser.LastName });
            //return RedirectToAction(nameof(Search));
        }
        #endregion

        #region Вспомогательные методы
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }        
        #endregion
    }
}
