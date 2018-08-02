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
    public class SemestrVidUchebRabotisController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public SemestrVidUchebRabotisController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: SemestrVidUchebRabotis
        public async Task<IActionResult> Index(int? SemestrId)
        {
            if (SemestrId != null)
            {
                var appIdentityDBContext = _context.SemestrVidUchebRaboti
                .Include(s => s.Semestr)
                .Include(s => s.VidUchebRabotiName)
                .Where(s => s.SemestrId == SemestrId);
                ViewBag.SemestrId = SemestrId;
                return View(await appIdentityDBContext.ToListAsync());
            }
            else
            {
                var appIdentityDBContext = _context.SemestrVidUchebRaboti
                .Include(s => s.Semestr)
                .Include(s => s.VidUchebRabotiName);
                return View(await appIdentityDBContext.ToListAsync());
            }
            
        }

        // GET: SemestrVidUchebRabotis/Details/5
        public async Task<IActionResult> Details(int? id, int? SemestrId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestrVidUchebRaboti = await _context.SemestrVidUchebRaboti
                .Include(s => s.Semestr)
                .Include(s => s.VidUchebRabotiName)
                .SingleOrDefaultAsync(m => m.SemestrVidUchebRabotiId == id);
            if (semestrVidUchebRaboti == null)
            {
                return NotFound();
            }

            ViewBag.SemestrId = SemestrId;
            return View(semestrVidUchebRaboti);
        }

        // GET: SemestrVidUchebRabotis/Create
        public IActionResult Create(int? SemestrId)
        {
            if (SemestrId != null)
            {
                ViewBag.SemestrId = SemestrId;
                ViewData["VidUchebRabotiNameId"] = new SelectList(_context.VidUchebRabotiNames, "VidUchebRabotiNameId", "VidUchebRabotiNameName");
                return View();
            }
            else
            {
                return NotFound();
            }
            
        }

        // POST: SemestrVidUchebRabotis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemestrVidUchebRabotiId,VidUchebRabotiNameId,SemestrId,Hour")] SemestrVidUchebRaboti semestrVidUchebRaboti, int? SemestrId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semestrVidUchebRaboti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { SemestrId});
            }

            ViewBag.SemestrId = SemestrId;
            ViewData["VidUchebRabotiNameId"] = new SelectList(_context.VidUchebRabotiNames, "VidUchebRabotiNameId", "VidUchebRabotiNameName", semestrVidUchebRaboti.VidUchebRabotiNameId);
            return View(semestrVidUchebRaboti);
        }

        // GET: SemestrVidUchebRabotis/Edit/5
        public async Task<IActionResult> Edit(int? id, int? SemestrId)
        {
            if (id == null || SemestrId == null)
            {
                return NotFound();
            }

            var semestrVidUchebRaboti = await _context.SemestrVidUchebRaboti.SingleOrDefaultAsync(m => m.SemestrVidUchebRabotiId == id);
            if (semestrVidUchebRaboti == null)
            {
                return NotFound();
            }
            
            ViewData["VidUchebRabotiNameId"] = new SelectList(_context.VidUchebRabotiNames, "VidUchebRabotiNameId", "VidUchebRabotiNameName", semestrVidUchebRaboti.VidUchebRabotiNameId);
            ViewBag.SemestrId = SemestrId;
            return View(semestrVidUchebRaboti);
        }

        // POST: SemestrVidUchebRabotis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SemestrVidUchebRabotiId,VidUchebRabotiNameId,SemestrId,Hour")] SemestrVidUchebRaboti semestrVidUchebRaboti, int SemestrId)
        {
            if (id != semestrVidUchebRaboti.SemestrVidUchebRabotiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semestrVidUchebRaboti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemestrVidUchebRabotiExists(semestrVidUchebRaboti.SemestrVidUchebRabotiId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { SemestrId});
            }
            
            ViewData["VidUchebRabotiNameId"] = new SelectList(_context.VidUchebRabotiNames, "VidUchebRabotiNameId", "VidUchebRabotiNameId", semestrVidUchebRaboti.VidUchebRabotiNameId);
            return View(semestrVidUchebRaboti);
        }

        // GET: SemestrVidUchebRabotis/Delete/5
        public async Task<IActionResult> Delete(int? id, int? SemestrId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestrVidUchebRaboti = await _context.SemestrVidUchebRaboti
                .Include(s => s.Semestr)
                .Include(s => s.VidUchebRabotiName)
                .SingleOrDefaultAsync(m => m.SemestrVidUchebRabotiId == id);
            if (semestrVidUchebRaboti == null)
            {
                return NotFound();
            }

            ViewBag.SemestrId = SemestrId;
            return View(semestrVidUchebRaboti);
        }

        // POST: SemestrVidUchebRabotis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semestrVidUchebRaboti = await _context.SemestrVidUchebRaboti.SingleOrDefaultAsync(m => m.SemestrVidUchebRabotiId == id);
            _context.SemestrVidUchebRaboti.Remove(semestrVidUchebRaboti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { SemestrId = semestrVidUchebRaboti.SemestrId});
        }

        private bool SemestrVidUchebRabotiExists(int id)
        {
            return _context.SemestrVidUchebRaboti.Any(e => e.SemestrVidUchebRabotiId == id);
        }
    }
}
