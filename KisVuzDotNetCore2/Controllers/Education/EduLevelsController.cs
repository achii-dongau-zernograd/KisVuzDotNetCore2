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

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class EduLevelsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduLevelsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduLevels.ToListAsync());
        }
                

        // GET: EduLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduLevelId,EduLevelName")] EduLevel eduLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduLevel);
        }

        // GET: EduLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduLevel = await _context.EduLevels.SingleOrDefaultAsync(m => m.EduLevelId == id);
            if (eduLevel == null)
            {
                return NotFound();
            }
            return View(eduLevel);
        }

        // POST: EduLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduLevelId,EduLevelName")] EduLevel eduLevel)
        {
            if (id != eduLevel.EduLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduLevelExists(eduLevel.EduLevelId))
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
            return View(eduLevel);
        }

        // GET: EduLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduLevel = await _context.EduLevels
                .SingleOrDefaultAsync(m => m.EduLevelId == id);
            if (eduLevel == null)
            {
                return NotFound();
            }

            return View(eduLevel);
        }

        // POST: EduLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduLevel = await _context.EduLevels.SingleOrDefaultAsync(m => m.EduLevelId == id);
            _context.EduLevels.Remove(eduLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduLevelExists(int id)
        {
            return _context.EduLevels.Any(e => e.EduLevelId == id);
        }
    }
}
