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
    [Authorize(Roles = "Администраторы")]
    public class UserAchievmentTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UserAchievmentTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UserAchievmentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserAchievmentTypes.ToListAsync());
        }

        // GET: UserAchievmentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAchievmentType = await _context.UserAchievmentTypes
                .SingleOrDefaultAsync(m => m.UserAchievmentTypeId == id);
            if (userAchievmentType == null)
            {
                return NotFound();
            }

            return View(userAchievmentType);
        }

        // GET: UserAchievmentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserAchievmentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserAchievmentTypeId,UserAchievmentTypeName")] UserAchievmentType userAchievmentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAchievmentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userAchievmentType);
        }

        // GET: UserAchievmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAchievmentType = await _context.UserAchievmentTypes.SingleOrDefaultAsync(m => m.UserAchievmentTypeId == id);
            if (userAchievmentType == null)
            {
                return NotFound();
            }
            return View(userAchievmentType);
        }

        // POST: UserAchievmentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserAchievmentTypeId,UserAchievmentTypeName")] UserAchievmentType userAchievmentType)
        {
            if (id != userAchievmentType.UserAchievmentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAchievmentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAchievmentTypeExists(userAchievmentType.UserAchievmentTypeId))
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
            return View(userAchievmentType);
        }

        // GET: UserAchievmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAchievmentType = await _context.UserAchievmentTypes
                .SingleOrDefaultAsync(m => m.UserAchievmentTypeId == id);
            if (userAchievmentType == null)
            {
                return NotFound();
            }

            return View(userAchievmentType);
        }

        // POST: UserAchievmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAchievmentType = await _context.UserAchievmentTypes.SingleOrDefaultAsync(m => m.UserAchievmentTypeId == id);
            _context.UserAchievmentTypes.Remove(userAchievmentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAchievmentTypeExists(int id)
        {
            return _context.UserAchievmentTypes.Any(e => e.UserAchievmentTypeId == id);
        }
    }
}
