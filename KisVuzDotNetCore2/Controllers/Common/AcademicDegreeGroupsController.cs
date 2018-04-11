using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;

namespace KisVuzDotNetCore2.Controllers
{
    public class AcademicDegreeGroupsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public AcademicDegreeGroupsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: AcademicDegreeGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.AcademicDegreeGroups.ToListAsync());
        }

        // GET: AcademicDegreeGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicDegreeGroup = await _context.AcademicDegreeGroups
                .SingleOrDefaultAsync(m => m.AcademicDegreeGroupId == id);
            if (academicDegreeGroup == null)
            {
                return NotFound();
            }

            return View(academicDegreeGroup);
        }

        // GET: AcademicDegreeGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AcademicDegreeGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AcademicDegreeGroupId,AcademicDegreeGroupName")] AcademicDegreeGroup academicDegreeGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicDegreeGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(academicDegreeGroup);
        }

        // GET: AcademicDegreeGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicDegreeGroup = await _context.AcademicDegreeGroups.SingleOrDefaultAsync(m => m.AcademicDegreeGroupId == id);
            if (academicDegreeGroup == null)
            {
                return NotFound();
            }
            return View(academicDegreeGroup);
        }

        // POST: AcademicDegreeGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AcademicDegreeGroupId,AcademicDegreeGroupName")] AcademicDegreeGroup academicDegreeGroup)
        {
            if (id != academicDegreeGroup.AcademicDegreeGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicDegreeGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicDegreeGroupExists(academicDegreeGroup.AcademicDegreeGroupId))
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
            return View(academicDegreeGroup);
        }

        // GET: AcademicDegreeGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicDegreeGroup = await _context.AcademicDegreeGroups
                .SingleOrDefaultAsync(m => m.AcademicDegreeGroupId == id);
            if (academicDegreeGroup == null)
            {
                return NotFound();
            }

            return View(academicDegreeGroup);
        }

        // POST: AcademicDegreeGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicDegreeGroup = await _context.AcademicDegreeGroups.SingleOrDefaultAsync(m => m.AcademicDegreeGroupId == id);
            _context.AcademicDegreeGroups.Remove(academicDegreeGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicDegreeGroupExists(int id)
        {
            return _context.AcademicDegreeGroups.Any(e => e.AcademicDegreeGroupId == id);
        }
    }
}
