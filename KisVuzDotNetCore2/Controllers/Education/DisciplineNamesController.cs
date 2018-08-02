using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Education
{
    [Authorize(Roles = "Администраторы")]
    public class DisciplineNamesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public DisciplineNamesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: DisciplineNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.DisciplineNames.ToListAsync());
        }

        // GET: DisciplineNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineName = await _context.DisciplineNames
                .SingleOrDefaultAsync(m => m.DisciplineNameId == id);
            if (disciplineName == null)
            {
                return NotFound();
            }

            return View(disciplineName);
        }

        // GET: DisciplineNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DisciplineNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisciplineNameId,DisciplineNameName")] DisciplineName disciplineName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplineName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disciplineName);
        }

        // GET: DisciplineNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineName = await _context.DisciplineNames.SingleOrDefaultAsync(m => m.DisciplineNameId == id);
            if (disciplineName == null)
            {
                return NotFound();
            }
            return View(disciplineName);
        }

        // POST: DisciplineNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisciplineNameId,DisciplineNameName")] DisciplineName disciplineName)
        {
            if (id != disciplineName.DisciplineNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplineName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplineNameExists(disciplineName.DisciplineNameId))
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
            return View(disciplineName);
        }

        // GET: DisciplineNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineName = await _context.DisciplineNames
                .SingleOrDefaultAsync(m => m.DisciplineNameId == id);
            if (disciplineName == null)
            {
                return NotFound();
            }

            return View(disciplineName);
        }

        // POST: DisciplineNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplineName = await _context.DisciplineNames.SingleOrDefaultAsync(m => m.DisciplineNameId == id);
            _context.DisciplineNames.Remove(disciplineName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplineNameExists(int id)
        {
            return _context.DisciplineNames.Any(e => e.DisciplineNameId == id);
        }
    }
}
