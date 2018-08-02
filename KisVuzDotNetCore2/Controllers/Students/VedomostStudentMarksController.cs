using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Students;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Students
{
    [Authorize(Roles = "Администраторы")]
    public class VedomostStudentMarksController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public VedomostStudentMarksController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: VedomostStudentMarks
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.VedomostStudentMarks
                .Include(v => v.Student)
                .Include(v => v.Vedomost)
                .Include(v => v.VedomostStudentMarkName);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: VedomostStudentMarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vedomostStudentMark = await _context.VedomostStudentMarks
                .Include(v => v.Student)
                .Include(v => v.Vedomost)
                .Include(v => v.VedomostStudentMarkName)
                .SingleOrDefaultAsync(m => m.VedomostStudentMarkId == id);
            if (vedomostStudentMark == null)
            {
                return NotFound();
            }

            return View(vedomostStudentMark);
        }

        // GET: VedomostStudentMarks/Create
        public async Task<IActionResult> Create(int VedomostId)
        {
            var vedomost = await _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup.EduKurs)
                .Include(v => v.StudentGroup.Students)
                .Include(v => v.VedomostStudentMarks)
                    .ThenInclude(m => m.VedomostStudentMarkName)
                .SingleOrDefaultAsync(v => v.VedomostId == VedomostId);
            if (vedomost == null) return NotFound();

            ViewData["vedomost"] = vedomost;
            ViewData["StudentId"] = new SelectList(vedomost.StudentGroup.Students, "StudentId", "StudentFio");            
            ViewData["VedomostStudentMarkNameId"] = new SelectList(_context.VedomostStudentMarkNames, "VedomostStudentMarkNameId", "VedomostStudentMarkNameName");
            return View();
        }

        // POST: VedomostStudentMarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VedomostStudentMarkId,VedomostId,StudentId,VedomostStudentMarkNameId")] VedomostStudentMark vedomostStudentMark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vedomostStudentMark);
                await _context.SaveChangesAsync();
                return RedirectToAction("VedomostStudentMarks","Vedomosti",new { id = vedomostStudentMark.VedomostId });
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", vedomostStudentMark.StudentId);
            ViewData["VedomostId"] = new SelectList(_context.Vedomosti, "VedomostId", "VedomostId", vedomostStudentMark.VedomostId);
            ViewData["VedomostStudentMarkNameId"] = new SelectList(_context.VedomostStudentMarkNames, "VedomostStudentMarkNameId", "VedomostStudentMarkNameId", vedomostStudentMark.VedomostStudentMarkNameId);
            return View(vedomostStudentMark);
        }

        // GET: VedomostStudentMarks/Edit/5
        public async Task<IActionResult> Edit(int? id, int VedomostId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vedomost = await _context.Vedomosti
                .Include(v => v.EduYear)
                .Include(v => v.SemestrName)
                .Include(v => v.StudentGroup.EduKurs)
                .Include(v => v.StudentGroup.Students)
                .Include(v => v.VedomostStudentMarks)
                    .ThenInclude(m => m.VedomostStudentMarkName)
                .SingleOrDefaultAsync(v => v.VedomostId == VedomostId);
            if (vedomost == null) return NotFound();
            ViewData["vedomost"] = vedomost;

            var vedomostStudentMark = await _context.VedomostStudentMarks.SingleOrDefaultAsync(m => m.VedomostStudentMarkId == id);
            if (vedomostStudentMark == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(vedomost.StudentGroup.Students, "StudentId", "StudentFio", vedomostStudentMark.StudentId);
            //ViewData["VedomostId"] = new SelectList(_context.Vedomosti, "VedomostId", "VedomostId", vedomostStudentMark.VedomostId);
            ViewData["VedomostStudentMarkNameId"] = new SelectList(_context.VedomostStudentMarkNames, "VedomostStudentMarkNameId", "VedomostStudentMarkNameName", vedomostStudentMark.VedomostStudentMarkNameId);
            return View(vedomostStudentMark);
        }

        // POST: VedomostStudentMarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("VedomostStudentMarkId,VedomostId,StudentId,VedomostStudentMarkNameId")] VedomostStudentMark vedomostStudentMark)
        {
            if (id != vedomostStudentMark.VedomostStudentMarkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vedomostStudentMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VedomostStudentMarkExists(vedomostStudentMark.VedomostStudentMarkId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("VedomostStudentMarks", "Vedomosti", new { id = vedomostStudentMark.VedomostId });
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", vedomostStudentMark.StudentId);
            ViewData["VedomostId"] = new SelectList(_context.Vedomosti, "VedomostId", "VedomostId", vedomostStudentMark.VedomostId);
            ViewData["VedomostStudentMarkNameId"] = new SelectList(_context.VedomostStudentMarkNames, "VedomostStudentMarkNameId", "VedomostStudentMarkNameId", vedomostStudentMark.VedomostStudentMarkNameId);
            return View(vedomostStudentMark);
        }

        // GET: VedomostStudentMarks/Delete/5
        public async Task<IActionResult> Delete(int? id, int VedomostId)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var vedomost = await _context.Vedomosti
            //    .Include(v => v.EduYear)
            //    .Include(v => v.SemestrName)
            //    .Include(v => v.StudentGroup.EduKurs)
            //    .Include(v => v.StudentGroup.Students)
            //    .Include(v => v.VedomostStudentMarks)
            //        .ThenInclude(m => m.VedomostStudentMarkName)
            //    .SingleOrDefaultAsync(v => v.VedomostId == VedomostId);
            //if (vedomost == null) return NotFound();

            //ViewData["vedomost"] = vedomost;

            var vedomostStudentMark = await _context.VedomostStudentMarks
                .Include(v => v.Student.StudentGroup.EduKurs)
                .Include(v => v.Vedomost)
                .Include(v => v.VedomostStudentMarkName)
                .SingleOrDefaultAsync(m => m.VedomostStudentMarkId == id);
            if (vedomostStudentMark == null)
            {
                return NotFound();
            }

            return View(vedomostStudentMark);
        }

        // POST: VedomostStudentMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int VedomostId)
        {
            var vedomostStudentMark = await _context.VedomostStudentMarks.SingleOrDefaultAsync(m => m.VedomostStudentMarkId == id);
            _context.VedomostStudentMarks.Remove(vedomostStudentMark);
            await _context.SaveChangesAsync();
            return RedirectToAction("VedomostStudentMarks", "Vedomosti", new { id = vedomostStudentMark.VedomostId });
        }

        private bool VedomostStudentMarkExists(int id)
        {
            return _context.VedomostStudentMarks.Any(e => e.VedomostStudentMarkId == id);
        }
    }
}
