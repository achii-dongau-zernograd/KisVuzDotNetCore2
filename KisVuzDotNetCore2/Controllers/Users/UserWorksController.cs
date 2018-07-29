using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Users
{
    [Authorize]
    public class UserWorksController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public UserWorksController(AppIdentityDBContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        private IIncludableQueryable<UserWork,UserWorkType> userWorks
                => _context.UserWorks
                .Include(u => u.AppUser)
                .Include(u => u.FileModel)
                .Include(u => u.UserWorkType);

        /// <summary>
        /// Возвращает объект работы пользователя по переданному УИД
        /// </summary>
        /// <param name="userWorkId">УИД работы</param>
        /// <returns></returns>
        private async Task<UserWork> GetUserWorkByIdAsync(int? userWorkId)
        {
            if (userWorkId == null) return null;
            return await userWorks.SingleOrDefaultAsync(w=>w.UserWorkId == userWorkId);
        }

        /// <summary>
        /// Возвращает объект активного пользователя
        /// </summary>
        /// <returns></returns>
        private async Task<AppUser> GetCurrentUser()
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == User.Identity.Name);             
        }

        // GET: UserWorks
        /// <summary>
        /// Перечень работ, загруженных активным пользователем
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null) return NotFound();

            var userWorksOrderedByDescending = userWorks
                .Include(w=>w.UserWorkReviews)
                .Where(u => u.AppUserId == currentUser.Id)
                .OrderByDescending(u => u.DatePosting);
            return View(await userWorksOrderedByDescending.ToListAsync());
        }

        // GET: UserWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWork = await GetUserWorkByIdAsync(id);
            if (userWork == null)
            {
                return NotFound();
            }

            return View(userWork);
        }

        // GET: UserWorks/Create
        public async Task<IActionResult> Create()
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null) return RedirectToAction(nameof(Index));

            ViewData["AppUserId"] = currentUser.Id;            
            ViewData["UserWorkTypeId"] = new SelectList(_context.UserWorkTypes, "UserWorkTypeId", "UserWorkTypeName");
            return View();
        }

        // POST: UserWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserWorkId,AppUserId,DatePosting,UserWorkName,UserWorkDescription,UserWorkTypeId")] UserWork userWork,
            IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if(uploadedFile!=null)
                {
                    var loadedFile = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Работа пользователя", FileDataTypeEnum.UserWork);
                    if(loadedFile!=null)
                    {
                        userWork.FileModelId = loadedFile.Id;
                    }
                }

                _context.Add(userWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var currentUser = await _context.Users.SingleOrDefaultAsync(u => u.UserName == User.Identity.Name);
            if (currentUser == null) return RedirectToAction(nameof(Index));

            ViewData["AppUserId"] = currentUser.Id;            
            ViewData["UserWorkTypeId"] = new SelectList(_context.UserWorkTypes, "UserWorkTypeId", "UserWorkTypeName", userWork.UserWorkTypeId);
            return View(userWork);
        }

        // GET: UserWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWork = await GetUserWorkByIdAsync(id);
            if (userWork == null)
            {
                return NotFound();
            }

            // Разрешаем доступ только автору работы и администраторам
            AppUser currentUser = await GetCurrentUser();
            if (userWork.AppUserId != currentUser.Id && !User.IsInRole("Администраторы"))
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["userWork"] = userWork;
            ViewData["UserWorkTypeId"] = new SelectList(_context.UserWorkTypes, "UserWorkTypeId", "UserWorkTypeName", userWork.UserWorkTypeId);
            return View(userWork);
        }

        // POST: UserWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserWorkId,AppUserId,DatePosting,UserWorkName,UserWorkDescription,UserWorkTypeId,FileModelId")] UserWork userWork,
            IFormFile uploadedFile)
        {
            if (id != userWork.UserWorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                        var loadedFile = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Работа пользователя", FileDataTypeEnum.UserWork);
                        await _context.SaveChangesAsync();
                        var fileToRemoveId = userWork.FileModelId;
                        userWork.FileModelId = loadedFile.Id;
                        await _context.SaveChangesAsync();
                        Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                    }

                    _context.Update(userWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserWorkExists(userWork.UserWorkId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userWork.AppUserId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", userWork.FileModelId);
            ViewData["UserWorkTypeId"] = new SelectList(_context.UserWorkTypes, "UserWorkTypeId", "UserWorkTypeId", userWork.UserWorkTypeId);
            return View(userWork);
        }

        // GET: UserWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWork = await GetUserWorkByIdAsync(id);
            if (userWork == null)
            {
                return NotFound();
            }

            return View(userWork);
        }

        // POST: UserWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userWork = await _context.UserWorks.SingleOrDefaultAsync(m => m.UserWorkId == id);
            _context.UserWorks.Remove(userWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserWorkExists(int id)
        {
            return _context.UserWorks.Any(e => e.UserWorkId == id);
        }
    }
}
