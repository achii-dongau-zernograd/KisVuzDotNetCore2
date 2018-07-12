using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize]
    public class QualificationsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public QualificationsController(AppIdentityDBContext context, UserManager<AppUser> userMgr)
        {
            _context = context;
            _userManager = userMgr;
        }

        // GET: Qualifications
        public async Task<IActionResult> Index(string id)
        {            
            // Если задан id и это id активного пользователя, отбираем данные пользователя с AppUserId, равным id
            if (id != null)
            {
                AppUser u = await _userManager.FindByNameAsync(User.Identity.Name);
                if (u == null) return NotFound();
                if(u.Id==id)
                {
                    var userQualifications = _context.Qualifications
                    .Where(q => q.AppUserId == id)
                    .Include(q => q.AppUser)
                    .Include(q => q.RowStatus);
                    ViewBag.Id = id;
                    return View(await userQualifications.ToListAsync());
                }                
            }


            // Отображаем все данные только администраторам и отделу кадров
            if (User.IsInRole("Администраторы") || User.IsInRole("Отдел кадров"))
            {
                var appIdentityDBContext = _context.Qualifications
                .Include(q => q.AppUser)
                .Include(q => q.RowStatus);
                return View(await appIdentityDBContext.ToListAsync());
            }

            return RedirectToAction("AccessDenied","Account");
                
        }

        // GET: Qualifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualifications
                .Include(q => q.AppUser)
                .SingleOrDefaultAsync(m => m.QualificationId == id);
            if (qualification == null)
            {
                return NotFound();
            }

            return View(qualification);
        }

        // GET: Qualifications/Create
        public async Task<IActionResult> Create(string id)
        {
            if (id == null)
            {
                ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "GetFullName");
                return View();
            }
            else
            {
                // Проверяем существование пользователя
                AppUser user = await _userManager.FindByIdAsync(id);
                ViewBag.AppUserId = id;
                return View("CreateByUser");
            }

            
        }

        // POST: Qualifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QualificationId,NapravlName,QualificationName,AppUserId")] Qualification qualification, bool? ReturnToUsersQualifications)
        {
            if (ModelState.IsValid)
            {
                // Если данные введены пользователем, устанавливаем статус "Ожидает подтверждения"
                if(ReturnToUsersQualifications==true)
                {
                    qualification.RowStatusId = 1;
                }
                else // иначе, т.е. если данные вводятся администраторами или отделом кадров, устанавливаем статус "Подтверждено"
                {
                    qualification.RowStatusId = 2;
                }

                // Удостоверяемся, что данные изменяют либо администраторы, либо отдел кадров, либо активный пользователь свои собственные данные
                bool IsAuthorized = false;
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (ReturnToUsersQualifications == true && qualification.AppUserId == user?.Id) IsAuthorized = true;
                else if (User.IsInRole("Администраторы")) IsAuthorized = true;
                else if (User.IsInRole("Отдел кадров")) IsAuthorized = true;
                if (IsAuthorized)
                {
                    _context.Add(qualification);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Unauthorized();
                }
                    

                if (ReturnToUsersQualifications == true)
                {                    
                    return RedirectToAction(nameof(Index), "Qualifications", new { id = qualification.AppUserId });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                    
                }
                    
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "GetFullName", qualification.AppUserId);
            return View(qualification);
        }

        // GET: Qualifications/Edit/5
        public async Task<IActionResult> Edit(int? id, bool? ReturnToUsersQualifications)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualifications.SingleOrDefaultAsync(m => m.QualificationId == id);
            if (qualification == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "GetFullName", qualification.AppUserId);
            return View(qualification);
        }

        // POST: Qualifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QualificationId,NapravlName,QualificationName,AppUserId")] Qualification qualification)
        {
            if (id != qualification.QualificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    qualification.RowStatusId = 1;
                    _context.Update(qualification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QualificationExists(qualification.QualificationId))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "GetFullName", qualification.AppUserId);
            return View(qualification);
        }

        // GET: Qualifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualifications
                .Include(q => q.AppUser)
                .SingleOrDefaultAsync(m => m.QualificationId == id);
            if (qualification == null)
            {
                return NotFound();
            }

            return View(qualification);
        }

        // POST: Qualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qualification = await _context.Qualifications.SingleOrDefaultAsync(m => m.QualificationId == id);
            _context.Qualifications.Remove(qualification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Отдел кадров, Администраторы")]
        public async Task<IActionResult> Confirm(int id)
        {
            var qualification = await _context.Qualifications.SingleOrDefaultAsync(m => m.QualificationId == id);

            if(qualification!=null)
            {
                qualification.RowStatusId = 2;                
                await _context.SaveChangesAsync();
            }            
            
            return RedirectToAction(nameof(ConfirmWaiting));
        }

        /// <summary>
        /// Подтверждение изменений в пользовательских данных
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Отдел кадров, Администраторы")]
        public async Task<IActionResult> ConfirmWaiting()
        {            
            var appIdentityDBContext = _context.Qualifications
                .Where(q=>q.RowStatusId==1)
                .Include(q => q.AppUser)
                .Include(q => q.RowStatus);
            return View(await appIdentityDBContext.ToListAsync());
        }

        private bool QualificationExists(int id)
        {
            return _context.Qualifications.Any(e => e.QualificationId == id);
        }
    }
}
