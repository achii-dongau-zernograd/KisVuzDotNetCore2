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
    public class EduQualificationsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduQualificationsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduQualifications
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduQualification.ToListAsync());
        }

   
        // GET: EduQualifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduQualifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduQualificationId,EduQualificationName")] EduQualification eduQualification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduQualification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduQualification);
        }

        // GET: EduQualifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduQualification = await _context.EduQualification.SingleOrDefaultAsync(m => m.EduQualificationId == id);
            if (eduQualification == null)
            {
                return NotFound();
            }
            return View(eduQualification);
        }

        // POST: EduQualifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduQualificationId,EduQualificationName")] EduQualification eduQualification)
        {
            if (id != eduQualification.EduQualificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduQualification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduQualificationExists(eduQualification.EduQualificationId))
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
            return View(eduQualification);
        }

        // GET: EduQualifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduQualification = await _context.EduQualification
                .SingleOrDefaultAsync(m => m.EduQualificationId == id);
            if (eduQualification == null)
            {
                return NotFound();
            }

            return View(eduQualification);
        }

        // POST: EduQualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduQualification = await _context.EduQualification.SingleOrDefaultAsync(m => m.EduQualificationId == id);
            _context.EduQualification.Remove(eduQualification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduQualificationExists(int id)
        {
            return _context.EduQualification.Any(e => e.EduQualificationId == id);
        }
    }
}
