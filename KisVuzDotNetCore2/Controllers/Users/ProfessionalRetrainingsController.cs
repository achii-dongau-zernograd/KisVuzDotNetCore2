using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Common;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize]
    public class ProfessionalRetrainingsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHostingEnvironment _appEnvironment;

        public ProfessionalRetrainingsController(AppIdentityDBContext context,
            UserManager<AppUser> userMgr,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _userManager = userMgr;
            _appEnvironment = appEnvironment;
        }

        // GET: ProfessionalRetrainings
        public async Task<IActionResult> Index(string id)
        {
            // Если задан id и это id активного пользователя, отбираем данные пользователя с AppUserId, равным id
            if (id != null)
            {
                AppUser u = await _userManager.FindByNameAsync(User.Identity.Name);
                if (u == null) return NotFound();
                if (u.Id == id)
                {
                    var professionalRetrainings = _context.ProfessionalRetrainings
                    .Where(r => r.AppUserId == id)
                    .Include(r => r.AppUser)
                    .Include(r => r.ProfessionalRetrainingFile)
                    .Include(r => r.RowStatus)
                    .OrderBy(r => r.ProfessionalRetrainingDateIssue);
                    ViewBag.Id = id;
                    return View(await professionalRetrainings.ToListAsync());
                }
            }


            // Отображаем все данные только администраторам и отделу кадров
            if (User.IsInRole("Администраторы") || User.IsInRole("Отдел кадров"))
            {
                var professionalRetrainings = _context.ProfessionalRetrainings
                .Include(r => r.AppUser)
                .Include(r => r.ProfessionalRetrainingFile)
                .Include(r => r.RowStatus)
                .OrderBy(r => r.AppUser.GetFullName);
                return View(await professionalRetrainings.ToListAsync());
            }

            return RedirectToAction("AccessDenied", "Account");
        }

        // GET: ProfessionalRetrainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professionalRetraining = await _context.ProfessionalRetrainings
                .Include(p => p.AppUser)
                .Include(p => p.ProfessionalRetrainingFile)
                .SingleOrDefaultAsync(m => m.ProfessionalRetrainingId == id);
            if (professionalRetraining == null)
            {
                return NotFound();
            }

            return View(professionalRetraining);
        }

        // GET: ProfessionalRetrainings/Create
        public async Task<IActionResult> Create(string id)
        {
            if (id == null)
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

        // POST: ProfessionalRetrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessionalRetrainingId,ProfessionalRetrainingDiplomRegNumber,ProfessionalRetrainingDiplomNumber,ProfessionalRetrainingProgramName,ProfessionalRetrainingHours,ProfessionalRetrainingCity,ProfessionalRetrainingInstitition,ProfessionalRetrainingDateStart,ProfessionalRetrainingDateFinish,ProfessionalRetrainingDateIssue,AppUserId")] ProfessionalRetraining professionalRetraining,
            IFormFile uploadedFile)
        {
            if (ModelState.IsValid && uploadedFile != null)
            {
                FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, "Диплом о профессиональной переподготовке", FileDataTypeEnum.DiplomOProfessionalnoyPerepodgotovke);
                professionalRetraining.ProfessionalRetrainingFileId = fileModel.Id;
                professionalRetraining.RowStatusId = (int)RowStatusEnum.NotConfirmed;
                _context.Add(professionalRetraining);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = professionalRetraining.AppUserId });
            }

            ViewBag.AppUserId = professionalRetraining.AppUserId;
            return View(professionalRetraining);
        }

        // GET: ProfessionalRetrainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professionalRetraining = await _context.ProfessionalRetrainings
                .Include(m=>m.ProfessionalRetrainingFile)
                .SingleOrDefaultAsync(m => m.ProfessionalRetrainingId == id);
            if (professionalRetraining == null)
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
            bool IsAuthorized = User.IsInRole("Администраторы") || User.IsInRole("Отдел кадров") || professionalRetraining.AppUserId == CurrentUser.Id;
            if (!IsAuthorized)
            {
                return Unauthorized();
            }

            return View(professionalRetraining);
        }

        // POST: ProfessionalRetrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessionalRetrainingId,ProfessionalRetrainingDiplomRegNumber,ProfessionalRetrainingDiplomNumber,ProfessionalRetrainingProgramName,ProfessionalRetrainingHours,ProfessionalRetrainingCity,ProfessionalRetrainingInstitition,ProfessionalRetrainingDateStart,ProfessionalRetrainingDateFinish,ProfessionalRetrainingDateIssue,ProfessionalRetrainingFileId,AppUserId")] ProfessionalRetraining professionalRetraining,
            IFormFile uploadedFile)
        {
            if (id != professionalRetraining.ProfessionalRetrainingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                        FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, "Диплом о профессиональной переподготовке", FileDataTypeEnum.DiplomOProfessionalnoyPerepodgotovke);
                        await _context.SaveChangesAsync();
                        int? fileToRemoveId = professionalRetraining.ProfessionalRetrainingFileId;
                        professionalRetraining.ProfessionalRetrainingFileId = fileModel.Id;
                        await _context.SaveChangesAsync();
                        KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                    }
                    professionalRetraining.RowStatusId = (int)RowStatusEnum.NotConfirmed;
                    _context.Update(professionalRetraining);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessionalRetrainingExists(professionalRetraining.ProfessionalRetrainingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = professionalRetraining.AppUserId });
            }
            
            return View(professionalRetraining);
        }

        // GET: ProfessionalRetrainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professionalRetraining = await _context.ProfessionalRetrainings
                .Include(p => p.AppUser)
                .Include(p => p.ProfessionalRetrainingFile)
                .SingleOrDefaultAsync(m => m.ProfessionalRetrainingId == id);
            if (professionalRetraining == null)
            {
                return NotFound();
            }

            return View(professionalRetraining);
        }

        // POST: ProfessionalRetrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professionalRetraining = await _context.ProfessionalRetrainings.SingleOrDefaultAsync(m => m.ProfessionalRetrainingId == id);
            _context.ProfessionalRetrainings.Remove(professionalRetraining);
            KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context,_appEnvironment, professionalRetraining.ProfessionalRetrainingFileId);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = professionalRetraining.AppUserId });
        }

        /// <summary>
        /// Подтверждение изменений в пользовательских данных
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Отдел кадров, Администраторы")]
        public async Task<IActionResult> ConfirmWaiting()
        {
            var appIdentityDBContext = _context.ProfessionalRetrainings
                .Where(q => q.RowStatusId == (int)RowStatusEnum.NotConfirmed)
                .Include(q => q.AppUser)
                .Include(q => q.ProfessionalRetrainingFile)
                .Include(q => q.RowStatus);
            return View(await appIdentityDBContext.ToListAsync());
        }

        [Authorize(Roles = "Отдел кадров, Администраторы")]
        public async Task<IActionResult> Confirm(int id)
        {
            var professionalRetraining = await _context.ProfessionalRetrainings.SingleOrDefaultAsync(m => m.ProfessionalRetrainingId == id);

            if (professionalRetraining != null)
            {
                professionalRetraining.RowStatusId = (int)RowStatusEnum.Confirmed;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ConfirmWaiting));
        }

        private bool ProfessionalRetrainingExists(int id)
        {
            return _context.ProfessionalRetrainings.Any(e => e.ProfessionalRetrainingId == id);
        }
    }
}
