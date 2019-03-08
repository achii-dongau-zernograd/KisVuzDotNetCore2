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
using KisVuzDotNetCore2.Infrastructure;

namespace KisVuzDotNetCore2.Controllers.Users
{
    [Authorize]
    public class UserAchievmentsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        IUserProfileRepository _userProfileRepository;
        ISelectListRepository _selectListRepository;

        public UserAchievmentsController(AppIdentityDBContext context,
            IUserProfileRepository userProfileRepository,
            ISelectListRepository selectListRepository)
        {
            _context = context;
            _userProfileRepository = userProfileRepository;
            _selectListRepository = selectListRepository;
        }

        // GET: UserAchievments
        public IActionResult Index()
        {
            var userAchievments = _userProfileRepository.GetAchievments(User.Identity.Name);            
            return View(userAchievments);
        }                
                

        // GET: UserAchievments/Edit/5
        public IActionResult CreateOrEdit(int? id)
        {
            var userAchievment = _userProfileRepository.GetAchievment(id, User.Identity.Name);
            if (userAchievment == null)
            {
                return NotFound();
            }
            
            ViewData["UserAchievmentTypes"] = _selectListRepository.GetSelectListUserAchievmentTypes();
            return View(userAchievment);
        }

        // POST: UserAchievments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(int id, [Bind("UserAchievmentId,AppUserId,Date,Description,UserAchievmentTypeId")] UserAchievment userAchievment)
        {
            if (id != userAchievment.UserAchievmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAchievment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAchievmentExists(userAchievment.UserAchievmentId))
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

            ViewData["UserAchievmentTypes"] = _selectListRepository.GetSelectListUserAchievmentTypes();
            return View(userAchievment);
        }

        // GET: UserAchievments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAchievment = await _context.UserAchievments
                .Include(u => u.AppUser)
                .Include(u => u.UserAchievmentType)
                .SingleOrDefaultAsync(m => m.UserAchievmentId == id);
            if (userAchievment == null)
            {
                return NotFound();
            }

            return View(userAchievment);
        }

        // POST: UserAchievments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAchievment = await _context.UserAchievments.SingleOrDefaultAsync(m => m.UserAchievmentId == id);
            _context.UserAchievments.Remove(userAchievment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAchievmentExists(int id)
        {
            return _context.UserAchievments.Any(e => e.UserAchievmentId == id);
        }
    }
}
