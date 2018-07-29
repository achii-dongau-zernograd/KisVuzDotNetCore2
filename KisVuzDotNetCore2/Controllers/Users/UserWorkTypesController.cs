using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Users;

namespace KisVuzDotNetCore2.Controllers.Users
{
    public class UserWorkTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UserWorkTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UserWorkTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserWorkTypes.ToListAsync());
        }

        // GET: UserWorkTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkType = await _context.UserWorkTypes
                .SingleOrDefaultAsync(m => m.UserWorkTypeId == id);
            if (userWorkType == null)
            {
                return NotFound();
            }

            return View(userWorkType);
        }

        // GET: UserWorkTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserWorkTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserWorkTypeId,UserWorkTypeName")] UserWorkType userWorkType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userWorkType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userWorkType);
        }

        // GET: UserWorkTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkType = await _context.UserWorkTypes.SingleOrDefaultAsync(m => m.UserWorkTypeId == id);
            if (userWorkType == null)
            {
                return NotFound();
            }
            return View(userWorkType);
        }

        // POST: UserWorkTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserWorkTypeId,UserWorkTypeName")] UserWorkType userWorkType)
        {
            if (id != userWorkType.UserWorkTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userWorkType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserWorkTypeExists(userWorkType.UserWorkTypeId))
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
            return View(userWorkType);
        }

        // GET: UserWorkTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkType = await _context.UserWorkTypes
                .SingleOrDefaultAsync(m => m.UserWorkTypeId == id);
            if (userWorkType == null)
            {
                return NotFound();
            }

            return View(userWorkType);
        }

        // POST: UserWorkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userWorkType = await _context.UserWorkTypes.SingleOrDefaultAsync(m => m.UserWorkTypeId == id);
            _context.UserWorkTypes.Remove(userWorkType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserWorkTypeExists(int id)
        {
            return _context.UserWorkTypes.Any(e => e.UserWorkTypeId == id);
        }
    }
}
