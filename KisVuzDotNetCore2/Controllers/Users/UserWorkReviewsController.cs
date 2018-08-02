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
    public class UserWorkReviewsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        private IIncludableQueryable<UserWorkReview,UserWorkReviewMark> _userWorkReviews => _context.UserWorkReviews
            .Include(u => u.FileModel)
            .Include(u => u.Reviewer)
            .Include(u => u.UserWork.UserWorkType)
            .Include(u => u.UserWorkReviewMark);

        public UserWorkReviewsController(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Запрос рецензий на работу пользователя
        /// </summary>
        /// <param name="UserWorkId">УИД работы пользователя</param>
        /// <returns></returns>
        private IQueryable<UserWorkReview> GetUserWorkReviews(int? UserWorkId)
        {
            if (UserWorkId == null) return null;
            return _userWorkReviews.Where(r => r.UserWorkId == UserWorkId);
        }

        /// <summary>
        /// Возвращает объект активного пользователя
        /// </summary>
        /// <returns></returns>
        private async Task<AppUser> GetCurrentUser()
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == User.Identity.Name);
        }

        // GET: UserWorkReviews
        public async Task<IActionResult> Index(int? UserWorkId)
        {
            if(UserWorkId==null)
            {
                return RedirectToAction("Index", "UserWorks");
            }

            ViewBag.UserWorkId = UserWorkId;
            var reviews = GetUserWorkReviews(UserWorkId);
            return View(await reviews.ToListAsync());
        }
               

        // GET: UserWorkReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkReview = await _context.UserWorkReviews
                .Include(u => u.FileModel)
                .Include(u => u.Reviewer)
                .Include(u => u.UserWork)
                .Include(u => u.UserWorkReviewMark)
                .SingleOrDefaultAsync(m => m.UserWorkReviewId == id);
            if (userWorkReview == null)
            {
                return NotFound();
            }

            return View(userWorkReview);
        }

        // GET: UserWorkReviews/Create
        public async Task<IActionResult> Create(int UserWorkId)
        {
            if (_context.UserWorks.SingleOrDefaultAsync(w => w.UserWorkId == UserWorkId) == null) return NotFound();
            
            ViewData["ReviewerId"] = (await GetCurrentUser()).Id;
            ViewData["UserWorkId"] = UserWorkId;
            ViewData["UserWorkReviewMarkId"] = new SelectList(_context.UserWorkReviewMarks, "UserWorkReviewMarkId", "UserWorkReviewMarkName");
            return View();
        }

        // POST: UserWorkReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserWorkReviewId,UserWorkId,ReviewerId,UserWorkReviewText,FileModelId,UserWorkReviewMarkId")] UserWorkReview userWorkReview,
            IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if(uploadedFile!=null)
                {
                    var loadedFile = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Рецензия на работу пользователя", FileDataTypeEnum.UserWorkRecenziya);
                    userWorkReview.FileModelId = loadedFile.Id;
                }
                _context.Add(userWorkReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { userWorkReview.UserWorkId });
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", userWorkReview.FileModelId);
            ViewData["ReviewerId"] = new SelectList(_context.Users, "Id", "Id", userWorkReview.ReviewerId);
            ViewData["UserWorkId"] = new SelectList(_context.UserWorks, "UserWorkId", "UserWorkId", userWorkReview.UserWorkId);
            ViewData["UserWorkReviewMarkId"] = new SelectList(_context.UserWorkReviewMarks, "UserWorkReviewMarkId", "UserWorkReviewMarkId", userWorkReview.UserWorkReviewMarkId);
            return View(userWorkReview);
        }

        // GET: UserWorkReviews/Edit/5
        public async Task<IActionResult> Edit(int? id, int UserWorkId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkReview = await _context.UserWorkReviews
                .Include(r=>r.FileModel)
                .SingleOrDefaultAsync(m => m.UserWorkReviewId == id);
            if (userWorkReview == null)
            {
                return NotFound();
            }
            ViewData["ReviewerId"] = (await GetCurrentUser()).Id;
            ViewData["UserWorkId"] = UserWorkId;
            ViewData["UserWorkReviewMarkId"] = new SelectList(_context.UserWorkReviewMarks, "UserWorkReviewMarkId", "UserWorkReviewMarkName", userWorkReview.UserWorkReviewMarkId);
            return View(userWorkReview);
        }

        // POST: UserWorkReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserWorkReviewId,UserWorkId,ReviewerId,UserWorkReviewText,FileModelId,UserWorkReviewMarkId")] UserWorkReview userWorkReview,
            IFormFile uploadedFile)
        {
            if (id != userWorkReview.UserWorkReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                        int? fileToRemoveId = userWorkReview.FileModelId;
                        if (fileToRemoveId!=null)
                        {
                            userWorkReview.FileModelId = null;
                            _context.Update(userWorkReview);
                            Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                            await _context.SaveChangesAsync();
                        }
                        var loadedFile = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Рецензия на работу пользователя", FileDataTypeEnum.UserWorkRecenziya);
                        userWorkReview.FileModelId = loadedFile.Id;
                    }
                    _context.Update(userWorkReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserWorkReviewExists(userWorkReview.UserWorkReviewId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { userWorkReview.UserWorkId });
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", userWorkReview.FileModelId);
            ViewData["ReviewerId"] = new SelectList(_context.Users, "Id", "Id", userWorkReview.ReviewerId);
            ViewData["UserWorkId"] = new SelectList(_context.UserWorks, "UserWorkId", "UserWorkId", userWorkReview.UserWorkId);
            ViewData["UserWorkReviewMarkId"] = new SelectList(_context.UserWorkReviewMarks, "UserWorkReviewMarkId", "UserWorkReviewMarkId", userWorkReview.UserWorkReviewMarkId);
            return View(userWorkReview);
        }

        // GET: UserWorkReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkReview = await _context.UserWorkReviews
                .Include(u => u.FileModel)
                .Include(u => u.Reviewer)
                .Include(u => u.UserWork)
                .Include(u => u.UserWorkReviewMark)
                .SingleOrDefaultAsync(m => m.UserWorkReviewId == id);
            if (userWorkReview == null)
            {
                return NotFound();
            }

            return View(userWorkReview);
        }

        // POST: UserWorkReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userWorkReview = await _context.UserWorkReviews.SingleOrDefaultAsync(m => m.UserWorkReviewId == id);
            _context.UserWorkReviews.Remove(userWorkReview);
            if(userWorkReview.FileModelId!=null)
            {
                await _context.SaveChangesAsync();
                Files.RemoveFile(_context,_appEnvironment, userWorkReview.FileModelId);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { userWorkReview.UserWorkId });
        }

        private bool UserWorkReviewExists(int id)
        {
            return _context.UserWorkReviews.Any(e => e.UserWorkReviewId == id);
        }
    }
}
