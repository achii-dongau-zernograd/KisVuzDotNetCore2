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
    [Authorize(Roles = "Администраторы, Учебная часть")]
    public class EduPriemsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduPriemsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduPriems
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduPriem.Include(e => e.EduForm)
                .Include(e => e.EduNapravl)
                    .ThenInclude(n=> n.EduUgs)
                    .ThenInclude(u=> u.EduLevel)
                .Include(e=> e.EduForm);
            return View(await appIdentityDBContext.ToListAsync());
        }

        
        // GET: EduPriems/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");

            var EduNapravls = _context.EduNapravls.Include(n=> n.EduUgs)
                 .ThenInclude(u => u.EduLevel);
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View();
        }

        // POST: EduPriems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EduPriem eduPriem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduPriem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");

            var EduNapravls = _context.EduNapravls.Include(n => n.EduUgs)
                 .ThenInclude(u => u.EduLevel);
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View(eduPriem);
        }

        // GET: EduPriems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPriem = await _context.EduPriem.SingleOrDefaultAsync(m => m.EduPriemId == id);
            if (eduPriem == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");

            var EduNapravls = _context.EduNapravls.Include(n => n.EduUgs)
                 .ThenInclude(u => u.EduLevel);
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View(eduPriem);
        }

        // POST: EduPriems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduPriemId,EduNapravlId,EduFormId,FinBF,FinBR,FinBM,FinPV,SredSum")] EduPriem eduPriem)
        {
            if (id != eduPriem.EduPriemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduPriem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduPriemExists(eduPriem.EduPriemId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");

            var EduNapravls = _context.EduNapravls.Include(n => n.EduUgs)
                 .ThenInclude(u => u.EduLevel);
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View(eduPriem);
        }

        // GET: EduPriems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPriem = await _context.EduPriem
                .Include(e => e.EduForm)
                .Include(e => e.EduNapravl)
                .SingleOrDefaultAsync(m => m.EduPriemId == id);
            if (eduPriem == null)
            {
                return NotFound();
            }

            return View(eduPriem);
        }

        // POST: EduPriems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduPriem = await _context.EduPriem.SingleOrDefaultAsync(m => m.EduPriemId == id);
            _context.EduPriem.Remove(eduPriem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduPriemExists(int id)
        {
            return _context.EduPriem.Any(e => e.EduPriemId == id);
        }
    }
}
