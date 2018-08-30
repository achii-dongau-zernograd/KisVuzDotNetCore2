using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Priem;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Education
{
    [Authorize(Roles = "Администраторы, Учебная часть")]
    public class EduPerevodsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduPerevodsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduPerevods
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.eduPerevod
                .Include(e => e.EduForm)
                .Include(e => e.EduNapravl)
                    .ThenInclude(n=>n.EduUgs)
                        .ThenInclude(ugs=>ugs.EduLevel);
            return View(await appIdentityDBContext.ToListAsync());
        }


        // GET: EduPerevods/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            var EduNapravls = _context.EduNapravls
                .Include(n => n.EduUgs)
                .ThenInclude(ugs => ugs.EduLevel); ;
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View();
        }

        // POST: EduPerevods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EduPerevod eduPerevod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduPerevod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            var EduNapravls = _context.EduNapravls
                .Include(n => n.EduUgs)
                .ThenInclude(ugs => ugs.EduLevel); ;
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View(eduPerevod);
        }

        // GET: EduPerevods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPerevod = await _context.eduPerevod.SingleOrDefaultAsync(m => m.EduPerevodId == id);
            if (eduPerevod == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            var EduNapravls = _context.EduNapravls
                .Include(n => n.EduUgs)
                .ThenInclude(ugs => ugs.EduLevel); ;
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View(eduPerevod);
        }

        // POST: EduPerevods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EduPerevod eduPerevod)
        {
            if (id != eduPerevod.EduPerevodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduPerevod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduPerevodExists(eduPerevod.EduPerevodId))
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
            var EduNapravls = _context.EduNapravls
                .Include(n => n.EduUgs)
                .ThenInclude(ugs => ugs.EduLevel); ;
            ViewData["EduNapravlId"] = new SelectList(EduNapravls, "EduNapravlId", "GetEduNapravlFullName");
            return View(eduPerevod);
        }

        // GET: EduPerevods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPerevod = await _context.eduPerevod
                .Include(e => e.EduForm)
                .Include(e => e.EduNapravl)
                    .ThenInclude(n => n.EduUgs)
                        .ThenInclude(ugs => ugs.EduLevel)
                .SingleOrDefaultAsync(m => m.EduPerevodId == id);
            if (eduPerevod == null)
            {
                return NotFound();
            }

            return View(eduPerevod);
        }

        // POST: EduPerevods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduPerevod = await _context.eduPerevod.SingleOrDefaultAsync(m => m.EduPerevodId == id);
            _context.eduPerevod.Remove(eduPerevod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduPerevodExists(int id)
        {
            return _context.eduPerevod.Any(e => e.EduPerevodId == id);
        }
    }
}
