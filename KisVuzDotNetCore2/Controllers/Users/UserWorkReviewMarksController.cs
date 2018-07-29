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
    public class UserWorkReviewMarksController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UserWorkReviewMarksController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UserWorkReviewMarks
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserWorkReviewMarks.ToListAsync());
        }

        // GET: UserWorkReviewMarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkReviewMark = await _context.UserWorkReviewMarks
                .SingleOrDefaultAsync(m => m.UserWorkReviewMarkId == id);
            if (userWorkReviewMark == null)
            {
                return NotFound();
            }

            return View(userWorkReviewMark);
        }

        // GET: UserWorkReviewMarks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserWorkReviewMarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserWorkReviewMarkId,UserWorkReviewMarkName")] UserWorkReviewMark userWorkReviewMark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userWorkReviewMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userWorkReviewMark);
        }

        // GET: UserWorkReviewMarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkReviewMark = await _context.UserWorkReviewMarks.SingleOrDefaultAsync(m => m.UserWorkReviewMarkId == id);
            if (userWorkReviewMark == null)
            {
                return NotFound();
            }
            return View(userWorkReviewMark);
        }

        // POST: UserWorkReviewMarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserWorkReviewMarkId,UserWorkReviewMarkName")] UserWorkReviewMark userWorkReviewMark)
        {
            if (id != userWorkReviewMark.UserWorkReviewMarkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userWorkReviewMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserWorkReviewMarkExists(userWorkReviewMark.UserWorkReviewMarkId))
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
            return View(userWorkReviewMark);
        }

        // GET: UserWorkReviewMarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkReviewMark = await _context.UserWorkReviewMarks
                .SingleOrDefaultAsync(m => m.UserWorkReviewMarkId == id);
            if (userWorkReviewMark == null)
            {
                return NotFound();
            }

            return View(userWorkReviewMark);
        }

        // POST: UserWorkReviewMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userWorkReviewMark = await _context.UserWorkReviewMarks.SingleOrDefaultAsync(m => m.UserWorkReviewMarkId == id);
            _context.UserWorkReviewMarks.Remove(userWorkReviewMark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserWorkReviewMarkExists(int id)
        {
            return _context.UserWorkReviewMarks.Any(e => e.UserWorkReviewMarkId == id);
        }
    }
}
