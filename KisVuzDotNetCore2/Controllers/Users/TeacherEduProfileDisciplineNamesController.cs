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
    public class TeacherEduProfileDisciplineNamesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public TeacherEduProfileDisciplineNamesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: TeacherEduProfileDisciplineNames
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.TeacherEduProfileDisciplineNames
                .Include(t => t.DisciplineName)
                .Include(t => t.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(t => t.Teacher);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: TeacherEduProfileDisciplineNames/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(teacherEduProfileDisciplineName);
        }

        // GET: TeacherEduProfileDisciplineNames/Create
        public IActionResult Create()
        {
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameName");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio");
            return View();
        }

        // POST: TeacherEduProfileDisciplineNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherEduProfileDisciplineNameId,TeacherId,EduProfileId,DisciplineNameId")] TeacherEduProfileDisciplineName teacherEduProfileDisciplineName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherEduProfileDisciplineName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameName", teacherEduProfileDisciplineName.DisciplineNameId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", teacherEduProfileDisciplineName.EduProfileId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio", teacherEduProfileDisciplineName.TeacherId);
            return View(teacherEduProfileDisciplineName);
        }

        // GET: TeacherEduProfileDisciplineNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameName", teacherEduProfileDisciplineName.DisciplineNameId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", teacherEduProfileDisciplineName.EduProfileId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio", teacherEduProfileDisciplineName.TeacherId);
            return View(teacherEduProfileDisciplineName);
        }

        // POST: TeacherEduProfileDisciplineNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherEduProfileDisciplineNameId,TeacherId,EduProfileId,DisciplineNameId")] TeacherEduProfileDisciplineName teacherEduProfileDisciplineName)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplineNameId"] = new SelectList(_context.DisciplineNames, "DisciplineNameId", "DisciplineNameName", teacherEduProfileDisciplineName.DisciplineNameId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", teacherEduProfileDisciplineName.EduProfileId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFio", teacherEduProfileDisciplineName.TeacherId);
            return View(teacherEduProfileDisciplineName);
        }

        // GET: TeacherEduProfileDisciplineNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(teacherEduProfileDisciplineName);
        }

        // POST: TeacherEduProfileDisciplineNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherEduProfileDisciplineName = await _context.TeacherEduProfileDisciplineNames.SingleOrDefaultAsync(m => m.TeacherEduProfileDisciplineNameId == id);
            _context.TeacherEduProfileDisciplineNames.Remove(teacherEduProfileDisciplineName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherEduProfileDisciplineNameExists(int id)
        {
            return _context.TeacherEduProfileDisciplineNames.Any(e => e.TeacherEduProfileDisciplineNameId == id);
        }
    }
}
