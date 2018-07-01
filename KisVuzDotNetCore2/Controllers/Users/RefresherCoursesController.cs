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
    public class RefresherCoursesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public RefresherCoursesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: RefresherCourses
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.RefresherCourses.Include(r => r.AppUser).Include(r => r.RefresherCourseFile);
            return View(await appIdentityDBContext.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RefresherCourseFileId"] = new SelectList(_context.Files, "Id", "Id");
            return View();
        }

        // POST: RefresherCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefresherCourseId,RefresherCourseRegNumber,RefresherCourseName,RefresherCourseHours,RefresherCourseCity,RefresherCourseInstitition,RefresherCourseDateStart,RefresherCourseDateFinish,RefresherCourseDateIssue,RefresherCourseFileId,AppUserId")] RefresherCourse refresherCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refresherCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", refresherCourse.AppUserId);
            ViewData["RefresherCourseFileId"] = new SelectList(_context.Files, "Id", "Id", refresherCourse.RefresherCourseFileId);
            return View(refresherCourse);
        }

        // GET: RefresherCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refresherCourse = await _context.RefresherCourses.SingleOrDefaultAsync(m => m.RefresherCourseId == id);
            if (refresherCourse == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", refresherCourse.AppUserId);
            ViewData["RefresherCourseFileId"] = new SelectList(_context.Files, "Id", "Id", refresherCourse.RefresherCourseFileId);
            return View(refresherCourse);
        }

        // POST: RefresherCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefresherCourseId,RefresherCourseRegNumber,RefresherCourseName,RefresherCourseHours,RefresherCourseCity,RefresherCourseInstitition,RefresherCourseDateStart,RefresherCourseDateFinish,RefresherCourseDateIssue,RefresherCourseFileId,AppUserId")] RefresherCourse refresherCourse)
        {
            if (id != refresherCourse.RefresherCourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", refresherCourse.AppUserId);
            ViewData["RefresherCourseFileId"] = new SelectList(_context.Files, "Id", "Id", refresherCourse.RefresherCourseFileId);
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
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RefresherCourseExists(int id)
        {
            return _context.RefresherCourses.Any(e => e.RefresherCourseId == id);
        }
    }
}
