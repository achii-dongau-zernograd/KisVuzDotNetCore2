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
    public class EduProgramPodgsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduProgramPodgsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduProgramPodgs
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduProgramPodg.ToListAsync());
        }

        // GET: EduProgramPodgs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduProgramPodgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduProgramPodgId,EduProgramPodgName")] EduProgramPodg eduProgramPodg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduProgramPodg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduProgramPodg);
        }

        // GET: EduProgramPodgs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramPodg = await _context.EduProgramPodg.SingleOrDefaultAsync(m => m.EduProgramPodgId == id);
            if (eduProgramPodg == null)
            {
                return NotFound();
            }
            return View(eduProgramPodg);
        }

        // POST: EduProgramPodgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduProgramPodgId,EduProgramPodgName")] EduProgramPodg eduProgramPodg)
        {
            if (id != eduProgramPodg.EduProgramPodgId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduProgramPodg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduProgramPodgExists(eduProgramPodg.EduProgramPodgId))
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
            return View(eduProgramPodg);
        }

        // GET: EduProgramPodgs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramPodg = await _context.EduProgramPodg
                .SingleOrDefaultAsync(m => m.EduProgramPodgId == id);
            if (eduProgramPodg == null)
            {
                return NotFound();
            }

            return View(eduProgramPodg);
        }

        // POST: EduProgramPodgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduProgramPodg = await _context.EduProgramPodg.SingleOrDefaultAsync(m => m.EduProgramPodgId == id);
            _context.EduProgramPodg.Remove(eduProgramPodg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduProgramPodgExists(int id)
        {
            return _context.EduProgramPodg.Any(e => e.EduProgramPodgId == id);
        }
    }
}
