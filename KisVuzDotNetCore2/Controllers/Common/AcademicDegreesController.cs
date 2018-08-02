using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class AcademicDegreesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public AcademicDegreesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: AcademicDegrees
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.AcademicDegrees.Include(a => a.AcademicDegreeGroup);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: AcademicDegrees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicDegree = await _context.AcademicDegrees
                .Include(a => a.AcademicDegreeGroup)
                .SingleOrDefaultAsync(m => m.AcademicDegreeId == id);
            if (academicDegree == null)
            {
                return NotFound();
            }

            return View(academicDegree);
        }

        // GET: AcademicDegrees/Create
        public IActionResult Create()
        {
            ViewData["AcademicDegreeGroupId"] = new SelectList(_context.AcademicDegreeGroups, "AcademicDegreeGroupId", "AcademicDegreeGroupName");
            return View();
        }

        // POST: AcademicDegrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AcademicDegreeId,AcademicDegreeName,AcademicDegreeGroupId")] AcademicDegree academicDegree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicDegree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicDegreeGroupId"] = new SelectList(_context.AcademicDegreeGroups, "AcademicDegreeGroupId", "AcademicDegreeGroupName", academicDegree.AcademicDegreeGroupId);
            return View(academicDegree);
        }

        // GET: AcademicDegrees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicDegree = await _context.AcademicDegrees.SingleOrDefaultAsync(m => m.AcademicDegreeId == id);
            if (academicDegree == null)
            {
                return NotFound();
            }
            ViewData["AcademicDegreeGroupId"] = new SelectList(_context.AcademicDegreeGroups, "AcademicDegreeGroupId", "AcademicDegreeGroupName", academicDegree.AcademicDegreeGroupId);
            return View(academicDegree);
        }

        // POST: AcademicDegrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AcademicDegreeId,AcademicDegreeName,AcademicDegreeGroupId")] AcademicDegree academicDegree)
        {
            if (id != academicDegree.AcademicDegreeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicDegree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicDegreeExists(academicDegree.AcademicDegreeId))
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
            ViewData["AcademicDegreeGroupId"] = new SelectList(_context.AcademicDegreeGroups, "AcademicDegreeGroupId", "AcademicDegreeGroupId", academicDegree.AcademicDegreeGroupId);
            return View(academicDegree);
        }

        // GET: AcademicDegrees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicDegree = await _context.AcademicDegrees
                .Include(a => a.AcademicDegreeGroup)
                .SingleOrDefaultAsync(m => m.AcademicDegreeId == id);
            if (academicDegree == null)
            {
                return NotFound();
            }

            return View(academicDegree);
        }

        // POST: AcademicDegrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicDegree = await _context.AcademicDegrees.SingleOrDefaultAsync(m => m.AcademicDegreeId == id);
            _context.AcademicDegrees.Remove(academicDegree);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicDegreeExists(int id)
        {
            return _context.AcademicDegrees.Any(e => e.AcademicDegreeId == id);
        }
    }
}
