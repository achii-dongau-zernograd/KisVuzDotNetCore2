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
    public class EduVidDeyatsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduVidDeyatsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduVidDeyats
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduVidDeyat.ToListAsync());
        }
        
        // GET: EduVidDeyats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduVidDeyats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduVidDeyatId,EduVidDeyatName")] EduVidDeyat eduVidDeyat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduVidDeyat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduVidDeyat);
        }

        // GET: EduVidDeyats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduVidDeyat = await _context.EduVidDeyat.SingleOrDefaultAsync(m => m.EduVidDeyatId == id);
            if (eduVidDeyat == null)
            {
                return NotFound();
            }
            return View(eduVidDeyat);
        }

        // POST: EduVidDeyats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduVidDeyatId,EduVidDeyatName")] EduVidDeyat eduVidDeyat)
        {
            if (id != eduVidDeyat.EduVidDeyatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduVidDeyat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduVidDeyatExists(eduVidDeyat.EduVidDeyatId))
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
            return View(eduVidDeyat);
        }

        // GET: EduVidDeyats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduVidDeyat = await _context.EduVidDeyat
                .SingleOrDefaultAsync(m => m.EduVidDeyatId == id);
            if (eduVidDeyat == null)
            {
                return NotFound();
            }

            return View(eduVidDeyat);
        }

        // POST: EduVidDeyats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduVidDeyat = await _context.EduVidDeyat.SingleOrDefaultAsync(m => m.EduVidDeyatId == id);
            _context.EduVidDeyat.Remove(eduVidDeyat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduVidDeyatExists(int id)
        {
            return _context.EduVidDeyat.Any(e => e.EduVidDeyatId == id);
        }
    }
}
