using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;

namespace KisVuzDotNetCore2.Controllers.Users
{
    public class AppUserStatusesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public AppUserStatusesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: AppUserStatuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppUserStatuses.ToListAsync());
        }

        // GET: AppUserStatuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserStatus = await _context.AppUserStatuses
                .SingleOrDefaultAsync(m => m.AppUserStatusId == id);
            if (appUserStatus == null)
            {
                return NotFound();
            }

            return View(appUserStatus);
        }

        // GET: AppUserStatuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppUserStatuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserStatusId,AppUserStatusName")] AppUserStatus appUserStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appUserStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUserStatus);
        }

        // GET: AppUserStatuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserStatus = await _context.AppUserStatuses.SingleOrDefaultAsync(m => m.AppUserStatusId == id);
            if (appUserStatus == null)
            {
                return NotFound();
            }
            return View(appUserStatus);
        }

        // POST: AppUserStatuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppUserStatusId,AppUserStatusName")] AppUserStatus appUserStatus)
        {
            if (id != appUserStatus.AppUserStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUserStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserStatusExists(appUserStatus.AppUserStatusId))
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
            return View(appUserStatus);
        }

        // GET: AppUserStatuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserStatus = await _context.AppUserStatuses
                .SingleOrDefaultAsync(m => m.AppUserStatusId == id);
            if (appUserStatus == null)
            {
                return NotFound();
            }

            return View(appUserStatus);
        }

        // POST: AppUserStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appUserStatus = await _context.AppUserStatuses.SingleOrDefaultAsync(m => m.AppUserStatusId == id);
            _context.AppUserStatuses.Remove(appUserStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserStatusExists(int id)
        {
            return _context.AppUserStatuses.Any(e => e.AppUserStatusId == id);
        }
    }
}
