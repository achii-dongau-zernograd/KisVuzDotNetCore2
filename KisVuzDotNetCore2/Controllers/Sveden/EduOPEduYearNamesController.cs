using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Sveden
{
    [Authorize(Roles = "Администраторы")]
    public class EduOPEduYearNamesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduOPEduYearNamesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduOPEduYearNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduOPEduYearNames.ToListAsync());
        }
                
        // GET: EduOPEduYearNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduOPEduYearNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduOPEduYearNameId,EduOPEduYearNameName")] EduOPEduYearName eduOPEduYearName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduOPEduYearName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduOPEduYearName);
        }

        // GET: EduOPEduYearNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduOPEduYearName = await _context.EduOPEduYearNames.SingleOrDefaultAsync(m => m.EduOPEduYearNameId == id);
            if (eduOPEduYearName == null)
            {
                return NotFound();
            }
            return View(eduOPEduYearName);
        }

        // POST: EduOPEduYearNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduOPEduYearNameId,EduOPEduYearNameName")] EduOPEduYearName eduOPEduYearName)
        {
            if (id != eduOPEduYearName.EduOPEduYearNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduOPEduYearName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduOPEduYearNameExists(eduOPEduYearName.EduOPEduYearNameId))
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
            return View(eduOPEduYearName);
        }

        // GET: EduOPEduYearNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduOPEduYearName = await _context.EduOPEduYearNames
                .SingleOrDefaultAsync(m => m.EduOPEduYearNameId == id);
            if (eduOPEduYearName == null)
            {
                return NotFound();
            }

            return View(eduOPEduYearName);
        }

        // POST: EduOPEduYearNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduOPEduYearName = await _context.EduOPEduYearNames.SingleOrDefaultAsync(m => m.EduOPEduYearNameId == id);
            _context.EduOPEduYearNames.Remove(eduOPEduYearName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduOPEduYearNameExists(int id)
        {
            return _context.EduOPEduYearNames.Any(e => e.EduOPEduYearNameId == id);
        }
    }
}
