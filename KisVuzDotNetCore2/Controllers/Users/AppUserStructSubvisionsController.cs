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

namespace KisVuzDotNetCore2.Controllers.Users
{
    [Authorize(Roles = "Администраторы, Отдел кадров")]
    public class AppUserStructSubvisionsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public AppUserStructSubvisionsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: AppUserStructSubvisions
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.AppUserStructSubvisions.Include(a => a.AppUser).Include(a => a.EmploymentForm).Include(a => a.Post).Include(a => a.StructSubvision);
            return View(await appIdentityDBContext.ToListAsync());
        }
               

        // GET: AppUserStructSubvisions/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, nameof(AppUser.Id), nameof(AppUser.GetFullName));
            ViewData["EmploymentFormId"] = new SelectList(_context.EmploymentForms, "EmploymentFormId", "EmploymentFormName");
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName");
            ViewData["StructSubvisionId"] = new SelectList(_context.StructSubvisions, "StructSubvisionId", "StructSubvisionName");
            return View();
        }

        // POST: AppUserStructSubvisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserStructSubvisionId,AppUserId,StructSubvisionId,PostId,EmploymentFormId")] AppUserStructSubvision appUserStructSubvision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appUserStructSubvision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "GetFullName", appUserStructSubvision.AppUserId);
            ViewData["EmploymentFormId"] = new SelectList(_context.EmploymentForms, "EmploymentFormId", "EmploymentFormName", appUserStructSubvision.EmploymentFormId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName", appUserStructSubvision.PostId);
            ViewData["StructSubvisionId"] = new SelectList(_context.StructSubvisions, "StructSubvisionId", "StructSubvisionName", appUserStructSubvision.StructSubvisionId);
            return View(appUserStructSubvision);
        }

        // GET: AppUserStructSubvisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserStructSubvision = await _context.AppUserStructSubvisions.SingleOrDefaultAsync(m => m.AppUserStructSubvisionId == id);
            if (appUserStructSubvision == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "GetFullName", appUserStructSubvision.AppUserId);
            ViewData["EmploymentFormId"] = new SelectList(_context.EmploymentForms, "EmploymentFormId", "EmploymentFormName", appUserStructSubvision.EmploymentFormId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName", appUserStructSubvision.PostId);
            ViewData["StructSubvisionId"] = new SelectList(_context.StructSubvisions, "StructSubvisionId", "StructSubvisionName", appUserStructSubvision.StructSubvisionId);
            return View(appUserStructSubvision);
        }

        // POST: AppUserStructSubvisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppUserStructSubvisionId,AppUserId,StructSubvisionId,PostId,EmploymentFormId")] AppUserStructSubvision appUserStructSubvision)
        {
            if (id != appUserStructSubvision.AppUserStructSubvisionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUserStructSubvision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserStructSubvisionExists(appUserStructSubvision.AppUserStructSubvisionId))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "GetFullName", appUserStructSubvision.AppUserId);
            ViewData["EmploymentFormId"] = new SelectList(_context.EmploymentForms, "EmploymentFormId", "EmploymentFormName", appUserStructSubvision.EmploymentFormId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostName", appUserStructSubvision.PostId);
            ViewData["StructSubvisionId"] = new SelectList(_context.StructSubvisions, "StructSubvisionId", "StructSubvisionName", appUserStructSubvision.StructSubvisionId);
            return View(appUserStructSubvision);
        }

        // GET: AppUserStructSubvisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserStructSubvision = await _context.AppUserStructSubvisions
                .Include(a => a.AppUser)
                .Include(a => a.EmploymentForm)
                .Include(a => a.Post)
                .Include(a => a.StructSubvision)
                .SingleOrDefaultAsync(m => m.AppUserStructSubvisionId == id);
            if (appUserStructSubvision == null)
            {
                return NotFound();
            }

            return View(appUserStructSubvision);
        }

        // POST: AppUserStructSubvisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appUserStructSubvision = await _context.AppUserStructSubvisions.SingleOrDefaultAsync(m => m.AppUserStructSubvisionId == id);
            _context.AppUserStructSubvisions.Remove(appUserStructSubvision);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserStructSubvisionExists(int id)
        {
            return _context.AppUserStructSubvisions.Any(e => e.AppUserStructSubvisionId == id);
        }
    }
}
