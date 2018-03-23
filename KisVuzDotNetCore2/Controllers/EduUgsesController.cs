using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers
{
    public class EduUgsesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduUgsesController(AppIdentityDBContext context)
        {
            _context = context;
        }
          
        // GET: EduUgses
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduUgses.Include(e => e.EduLevel);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduUgses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduUgs = await _context.EduUgses
                .Include(e => e.EduLevel)
                .SingleOrDefaultAsync(m => m.EduUgsId == id);
            if (eduUgs == null)
            {
                return NotFound();
            }

            return View(eduUgs);
        }

        // GET: EduUgses/Create
        public IActionResult Create()
        {
            ViewData["EduLevelId"] = new SelectList(_context.EduLevels, "EduLevelId", "EduLevelName");
            return View();
        }

        // POST: EduUgses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduUgsId,EduUgsCode,EduUgsName,EduLevelId")] EduUgs eduUgs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduUgs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduLevelId"] = new SelectList(_context.EduLevels, "EduLevelId", "EduLevelId", eduUgs.EduLevelId);
            return View(eduUgs);
        }

        // GET: EduUgses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduUgs = await _context.EduUgses.SingleOrDefaultAsync(m => m.EduUgsId == id);
            if (eduUgs == null)
            {
                return NotFound();
            }
            ViewData["EduLevelId"] = new SelectList(_context.EduLevels, "EduLevelId", "EduLevelName", eduUgs.EduLevelId);
            return View(eduUgs);
        }

        // POST: EduUgses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduUgsId,EduUgsCode,EduUgsName,EduLevelId")] EduUgs eduUgs)
        {
            if (id != eduUgs.EduUgsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduUgs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduUgsExists(eduUgs.EduUgsId))
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
            ViewData["EduLevelId"] = new SelectList(_context.EduLevels, "EduLevelId", "EduLevelId", eduUgs.EduLevelId);
            return View(eduUgs);
        }

        // GET: EduUgses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduUgs = await _context.EduUgses
                .Include(e => e.EduLevel)
                .SingleOrDefaultAsync(m => m.EduUgsId == id);
            if (eduUgs == null)
            {
                return NotFound();
            }

            return View(eduUgs);
        }

        // POST: EduUgses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduUgs = await _context.EduUgses.SingleOrDefaultAsync(m => m.EduUgsId == id);
            _context.EduUgses.Remove(eduUgs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduUgsExists(int id)
        {
            return _context.EduUgses.Any(e => e.EduUgsId == id);
        }
    }
}
