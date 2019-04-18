using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Hosting;
using KisVuzDotNetCore2.Models.Common;

namespace KisVuzDotNetCore2.Controllers.Users
{
    [Authorize]
    public class RefresherCoursesController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHostingEnvironment _appEnvironment;

        public RefresherCoursesController(AppIdentityDBContext context,
            UserManager<AppUser> userMgr,
            IHostingEnvironment appEnvironment
            )
        {
            _context = context;
            _userManager = userMgr;
            _appEnvironment = appEnvironment;
        }

        // GET: RefresherCourses
        public async Task<IActionResult> Index(string id)
        {
            // Если задан id и это id активного пользователя, отбираем данные пользователя с AppUserId, равным id
            if (id != null)
            {
                AppUser u = await _userManager.FindByNameAsync(User.Identity.Name);
                if (u == null) return NotFound();
                if (u.Id == id)
                {
                    var userQualifications = _context.RefresherCourses
                    .Where(r => r.AppUserId == id)
                    .Include(r => r.AppUser)
                    .Include(r => r.RefresherCourseFile)
                    .Include(r => r.RowStatus)
                    .OrderBy(r => r.RefresherCourseDateIssue);
                    ViewBag.Id = id;
                    return View(await userQualifications.ToListAsync());
                }
            }


            // Отображаем все данные только администраторам и отделу кадров
            if (User.IsInRole("Администраторы") || User.IsInRole("Отдел кадров"))
            {
                var refresherCourses = _context.RefresherCourses
                .Include(r => r.AppUser)
                .Include(r => r.RefresherCourseFile)
                .Include(r => r.RowStatus)
                .OrderBy(r => r.AppUser.GetFullName);
                return View(await refresherCourses.ToListAsync());
            }

            return RedirectToAction("AccessDenied", "Account");            
        }

        // GET: RefresherCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refresherCourse = await _context.RefresherCourses
                .Include(r => r.AppUser)
                .Include(r => r.RefresherCourseFile)
                .SingleOrDefaultAsync(m => m.RefresherCourseId == id);
            if (refresherCourse == null)
            {
                return NotFound();
            }

            return View(refresherCourse);
        }

        // GET: RefresherCourses/Create
        public async Task<IActionResult> Create(string id)
        {
            if(id==null)
            {
                return NotFound();
            }

            // Поиск пользователя с переданным id
            AppUser user = await _userManager.FindByIdAsync(id);
            // Поиск текущего пользователя
            var CurrentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null || CurrentUser == null)
            {
                return NotFound();
            }

            // Разрешаем доступ только администраторам, отделу кадров и пользователю для создания записи для себя
            bool IsAuthorized = User.IsInRole("Администраторы") || User.IsInRole("Отдел кадров") || id == CurrentUser.Id;
            if (!IsAuthorized)
            {
                return Unauthorized();
            }

            ViewBag.AppUserId = user.Id;
            return View();
        }

        // POST: RefresherCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefresherCourseId,RefresherCourseRegNumber,RefresherCourseName,RefresherCourseHours,RefresherCourseCity,RefresherCourseInstitition,RefresherCourseDateStart,RefresherCourseDateFinish,RefresherCourseDateIssue,AppUserId")] RefresherCourse refresherCourse,
            IFormFile uploadedFile)
        {
            if (ModelState.IsValid && uploadedFile != null)
            {
                FileModel fileModel = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Удостоверение о повышении квалификации", FileDataTypeEnum.UdostoverenieOPovisheniiKvalifikacii);
                refresherCourse.RefresherCourseFileId = fileModel.Id;
                refresherCourse.RowStatusId = (int)RowStatusEnum.NotConfirmed;
                _context.Add(refresherCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { id= refresherCourse.AppUserId });
            }

            ViewBag.AppUserId = refresherCourse.AppUserId;
            return View(refresherCourse);
        }

        // GET: RefresherCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refresherCourse = await _context.RefresherCourses
                .Include(c=>c.RefresherCourseFile)
                .SingleOrDefaultAsync(m => m.RefresherCourseId == id);
            if (refresherCourse == null)
            {
                return NotFound();
            }

            // Поиск текущего пользователя
            var CurrentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (CurrentUser == null)
            {
                return NotFound();
            }
            
            // Разрешаем доступ только администраторам, отделу кадров и пользователю для создания записи для себя
            bool IsAuthorized = User.IsInRole("Администраторы") || User.IsInRole("Отдел кадров") || refresherCourse.AppUserId == CurrentUser.Id;
            if (!IsAuthorized)
            {
                return Unauthorized();
            }

            
            return View(refresherCourse);
        }

        // POST: RefresherCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefresherCourseId,RefresherCourseRegNumber,RefresherCourseName,RefresherCourseHours,RefresherCourseCity,RefresherCourseInstitition,RefresherCourseDateStart,RefresherCourseDateFinish,RefresherCourseDateIssue,RefresherCourseFileId,AppUserId")] RefresherCourse refresherCourse,
            IFormFile uploadedFile)
        {
            if (id != refresherCourse.RefresherCourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                        FileModel fileModel = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Удостоверение о повышении квалификации", FileDataTypeEnum.UdostoverenieOPovisheniiKvalifikacii);
                        await _context.SaveChangesAsync();
                        int? fileToRemoveId = refresherCourse.RefresherCourseFileId;
                        refresherCourse.RefresherCourseFileId = fileModel.Id;                        
                        await _context.SaveChangesAsync();
                        Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                    }
                    refresherCourse.RowStatusId = (int)RowStatusEnum.NotConfirmed;
                    _context.Update(refresherCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefresherCourseExists(refresherCourse.RefresherCourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = refresherCourse.AppUserId });
            }
            
            return View(refresherCourse);
        }

        // GET: RefresherCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refresherCourse = await _context.RefresherCourses
                .Include(r => r.AppUser)
                .Include(r => r.RefresherCourseFile)
                .SingleOrDefaultAsync(m => m.RefresherCourseId == id);
            if (refresherCourse == null)
            {
                return NotFound();
            }

            return View(refresherCourse);
        }

        // POST: RefresherCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refresherCourse = await _context.RefresherCourses.SingleOrDefaultAsync(m => m.RefresherCourseId == id);
            _context.RefresherCourses.Remove(refresherCourse);
            Files.RemoveFile(_context, _appEnvironment, refresherCourse.RefresherCourseFileId);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = refresherCourse.AppUserId });
        }

        /// <summary>
        /// Подтверждение изменений в пользовательских данных
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Отдел кадров, Администраторы")]
        public async Task<IActionResult> ConfirmWaiting()
        {
            var appIdentityDBContext = _context.RefresherCourses
                .Where(q => q.RowStatusId == (int)RowStatusEnum.NotConfirmed)
                .Include(q => q.AppUser)
                .Include(q => q.RefresherCourseFile)
                .Include(q => q.RowStatus);
            return View(await appIdentityDBContext.ToListAsync());
        }

        [Authorize(Roles = "Отдел кадров, Администраторы")]
        public async Task<IActionResult> Confirm(int id)
        {
            var refresherCourse = await _context.RefresherCourses.SingleOrDefaultAsync(m => m.RefresherCourseId == id);

            if (refresherCourse != null)
            {
                refresherCourse.RowStatusId = 2;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ConfirmWaiting));
        }

        private bool RefresherCourseExists(int id)
        {
            return _context.RefresherCourses.Any(e => e.RefresherCourseId == id);
        }
    }
}
