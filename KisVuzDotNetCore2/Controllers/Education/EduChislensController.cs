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
    [Authorize(Roles = "Администраторы, Учебная часть")]
    public class EduChislensController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduChislensController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduChislens
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduChislens
                .Include(e => e.EduForm)
                .Include(e => e.EduProfile)
                    .ThenInclude(n => n.EduNapravl)
                    .ThenInclude(u => u.EduUgs)
                        .ThenInclude(l => l.EduLevel)
                .OrderBy(e=>e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .ThenBy(e=>e.EduProfile.EduNapravl.EduNapravlCode);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduChislens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduChislen = await _context.EduChislens
                .Include(e => e.EduForm)
                .Include(e => e.EduProfile)
                .ThenInclude(n => n.EduNapravl)
                .ThenInclude(u => u.EduUgs)
                .ThenInclude(l => l.EduLevel)
                .SingleOrDefaultAsync(m => m.EduChislenId == id);
            if (eduChislen == null)
            {
                return NotFound();
            }

            return View(eduChislen);
        }

        // GET: EduChislens/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles
                .Include(n => n.EduNapravl)
                .ThenInclude(u => u.EduUgs)
                .ThenInclude (l => l.EduLevel), "EduProfileId", "GetEduProfileFullName");
            return View();
        }

        // POST: EduChislens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduChislenId,NumberBFpriem,NumberBRpriem,NumberBMpriem,NumberPpriem,EduFormId,EduProfileId,NumberInostr")] EduChislen eduChislen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduChislen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduChislen.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", eduChislen.EduProfileId);
            return View(eduChislen);
        }

        // GET: EduChislens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduChislen = await _context.EduChislens.SingleOrDefaultAsync(m => m.EduChislenId == id);
            if (eduChislen == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", eduChislen.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles
                .Include(n => n.EduNapravl)
                .ThenInclude(u => u.EduUgs)
                .ThenInclude(l => l.EduLevel), "EduProfileId", "GetEduProfileFullName", eduChislen.EduProfileId);
            return View(eduChislen);
        }

        // POST: EduChislens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduChislenId,NumberBFpriem,NumberBRpriem,NumberBMpriem,NumberPpriem,EduFormId,EduProfileId,NumberInostr")] EduChislen eduChislen)
        {
            if (id != eduChislen.EduChislenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduChislen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduChislenExists(eduChislen.EduChislenId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduChislen.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", eduChislen.EduProfileId);
            return View(eduChislen);
        }

        // GET: EduChislens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduChislen = await _context.EduChislens
                .Include(e => e.EduForm)
                .Include(e => e.EduProfile)
                .ThenInclude(n => n.EduNapravl)
                .ThenInclude(u => u.EduUgs)
                .ThenInclude(l => l.EduLevel)
                .SingleOrDefaultAsync(m => m.EduChislenId == id);
            if (eduChislen == null)
            {
                return NotFound();
            }

            return View(eduChislen);
        }

        // POST: EduChislens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduChislen = await _context.EduChislens.SingleOrDefaultAsync(m => m.EduChislenId == id);
            _context.EduChislens.Remove(eduChislen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduChislenExists(int id)
        {
            return _context.EduChislens.Any(e => e.EduChislenId == id);
        }
    }
}
