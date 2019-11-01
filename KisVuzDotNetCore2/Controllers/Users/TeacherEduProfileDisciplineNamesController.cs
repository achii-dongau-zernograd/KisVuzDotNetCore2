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
    public class TeacherEduProfileDisciplineNamesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public TeacherEduProfileDisciplineNamesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: TeacherEduProfileDisciplineNames
        public async Task<IActionResult> Index(string fioSearch)
        {
            var appIdentityDBContext = _context.TeacherEduProfileDisciplineNames
                .Include(t => t.DisciplineName)
                .Include(t => t.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(t => t.Teacher)                
                .OrderBy(t => t.Teacher.TeacherFio)
                .ThenBy(t => t.DisciplineName.DisciplineNameName);

            if (!string.IsNullOrWhiteSpace(fioSearch))
            {
                ViewBag.fioSearch = fioSearch;
                return View(await appIdentityDBContext.Where(t => t.Teacher.TeacherFio.Contains(fioSearch)).ToListAsync());
            }

            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: TeacherEduProfileDisciplineNames/Details/5
        public async Task<IActionResult> Details(int? id,
            string fioSearch)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherEduProfileDisciplineName = await _context.TeacherEduProfileDisciplineNames
                .Include(t => t.DisciplineName)
                .Include(t => t.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(t => t.Teacher)
                .SingleOrDefaultAsync(m => m.TeacherEduProfileDisciplineNameId == id);
            if (teacherEduProfileDisciplineName == null)
            {
                return NotFound();
            }

            ViewBag.fioSearch = fioSearch;
            return View(teacherEduProfileDisciplineName);
        }

        // GET: TeacherEduProfileDisciplineNames/Create
        public IActionResult Create(string fioSearch)
        {
            ViewBag.fioSearch = fioSearch;

            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames.OrderBy(d => d.DisciplineNameName), "DisciplineNameId", "DisciplineNameName");
            ViewData["EduProfileId"]     = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            ViewData["TeacherId"]        = new SelectList(_context
                .Teachers
                .Where(t => string.IsNullOrWhiteSpace(fioSearch) ? true : t.TeacherFio.Contains(fioSearch))
                .OrderBy(t => t.TeacherFio), "TeacherId", "TeacherFio");
            return View();
        }

        // POST: TeacherEduProfileDisciplineNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherEduProfileDisciplineName teacherEduProfileDisciplineName,
            string fioSearch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherEduProfileDisciplineName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { fioSearch });
            }

            ViewBag.fioSearch = fioSearch;
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames.OrderBy(d => d.DisciplineNameName), "DisciplineNameId", "DisciplineNameName", teacherEduProfileDisciplineName.DisciplineNameId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", teacherEduProfileDisciplineName.EduProfileId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers.OrderBy(t => t.TeacherFio), "TeacherId", "TeacherFio", teacherEduProfileDisciplineName.TeacherId);

            return View(teacherEduProfileDisciplineName);
        }

        // GET: TeacherEduProfileDisciplineNames/Edit/5
        public async Task<IActionResult> Edit(int? id,
            string fioSearch)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherEduProfileDisciplineName = await _context.TeacherEduProfileDisciplineNames.SingleOrDefaultAsync(m => m.TeacherEduProfileDisciplineNameId == id);
            if (teacherEduProfileDisciplineName == null)
            {
                return NotFound();
            }
            ViewBag.fioSearch = fioSearch;

            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames.OrderBy(d => d.DisciplineNameName), "DisciplineNameId", "DisciplineNameName", teacherEduProfileDisciplineName.DisciplineNameId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", teacherEduProfileDisciplineName.EduProfileId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers.OrderBy(t => t.TeacherFio), "TeacherId", "TeacherFio", teacherEduProfileDisciplineName.TeacherId);

            return View(teacherEduProfileDisciplineName);
        }

        // POST: TeacherEduProfileDisciplineNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherEduProfileDisciplineNameId,TeacherId,EduProfileId,DisciplineNameId")] TeacherEduProfileDisciplineName teacherEduProfileDisciplineName,
            string fioSearch)
        {
            if (id != teacherEduProfileDisciplineName.TeacherEduProfileDisciplineNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherEduProfileDisciplineName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherEduProfileDisciplineNameExists(teacherEduProfileDisciplineName.TeacherEduProfileDisciplineNameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { fioSearch });
            }

            ViewBag.fioSearch = fioSearch;
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames.OrderBy(d => d.DisciplineNameName), "DisciplineNameId", "DisciplineNameName", teacherEduProfileDisciplineName.DisciplineNameId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", teacherEduProfileDisciplineName.EduProfileId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers.OrderBy(t => t.TeacherFio), "TeacherId", "TeacherFio", teacherEduProfileDisciplineName.TeacherId);

            return View(teacherEduProfileDisciplineName);
        }

        // GET: TeacherEduProfileDisciplineNames/Delete/5
        public async Task<IActionResult> Delete(int? id,
            string fioSearch)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherEduProfileDisciplineName = await _context.TeacherEduProfileDisciplineNames
                .Include(t => t.DisciplineName)
                .Include(t => t.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(t => t.Teacher)
                .SingleOrDefaultAsync(m => m.TeacherEduProfileDisciplineNameId == id);
            if (teacherEduProfileDisciplineName == null)
            {
                return NotFound();
            }

            ViewBag.fioSearch = fioSearch;
            return View(teacherEduProfileDisciplineName);
        }

        // POST: TeacherEduProfileDisciplineNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,
            string fioSearch)
        {
            var teacherEduProfileDisciplineName = await _context.TeacherEduProfileDisciplineNames.SingleOrDefaultAsync(m => m.TeacherEduProfileDisciplineNameId == id);
            _context.TeacherEduProfileDisciplineNames.Remove(teacherEduProfileDisciplineName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { fioSearch });
        }        

        private bool TeacherEduProfileDisciplineNameExists(int id)
        {
            return _context.TeacherEduProfileDisciplineNames.Any(e => e.TeacherEduProfileDisciplineNameId == id);
        }
    }
}
