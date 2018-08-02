using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class GraduateTrudoustroustvoController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public GraduateTrudoustroustvoController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: GraduateTrudoustroustvo
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.GraduateTrudoustroustvo
                .Include(g => g.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(g => g.GraduateYearName);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: GraduateTrudoustroustvo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduateTrudoustroustvo = await _context.GraduateTrudoustroustvo
                .Include(g => g.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(g => g.GraduateYearName)
                .SingleOrDefaultAsync(m => m.GraduateTrudoustroustvoId == id);
            if (graduateTrudoustroustvo == null)
            {
                return NotFound();
            }

            return View(graduateTrudoustroustvo);
        }

        // GET: GraduateTrudoustroustvo/Create
        public IActionResult Create()
        {
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            ViewData["GraduateYearId"] = new SelectList(_context.GraduateYear, "GraduateYearId", "GraduateYearName");
            return View();
        }

        // POST: GraduateTrudoustroustvo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GraduateTrudoustroustvoId,EduProfileId,GraduateYearId,GraduateTrudoustroustvoNumber")] GraduateTrudoustroustvo graduateTrudoustroustvo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(graduateTrudoustroustvo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", graduateTrudoustroustvo.EduProfileId);
            ViewData["GraduateYearId"] = new SelectList(_context.GraduateYear, "GraduateYearId", "GraduateYearName", graduateTrudoustroustvo.GraduateYearId);
            return View(graduateTrudoustroustvo);
        }

        // GET: GraduateTrudoustroustvo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduateTrudoustroustvo = await _context.GraduateTrudoustroustvo.SingleOrDefaultAsync(m => m.GraduateTrudoustroustvoId == id);
            if (graduateTrudoustroustvo == null)
            {
                return NotFound();
            }
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", graduateTrudoustroustvo.EduProfileId);
            ViewData["GraduateYearId"] = new SelectList(_context.GraduateYear, "GraduateYearId", "GraduateYearName", graduateTrudoustroustvo.GraduateYearId);
            return View(graduateTrudoustroustvo);
        }

        // POST: GraduateTrudoustroustvo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GraduateTrudoustroustvoId,EduProfileId,GraduateYearId,GraduateTrudoustroustvoNumber")] GraduateTrudoustroustvo graduateTrudoustroustvo)
        {
            if (id != graduateTrudoustroustvo.GraduateTrudoustroustvoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(graduateTrudoustroustvo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GraduateTrudoustroustvoExists(graduateTrudoustroustvo.GraduateTrudoustroustvoId))
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
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", graduateTrudoustroustvo.EduProfileId);
            ViewData["GraduateYearId"] = new SelectList(_context.GraduateYear, "GraduateYearId", "GraduateYearName", graduateTrudoustroustvo.GraduateYearId);
            return View(graduateTrudoustroustvo);
        }

        // GET: GraduateTrudoustroustvo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduateTrudoustroustvo = await _context.GraduateTrudoustroustvo
                .Include(g => g.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(g => g.GraduateYearName)
                .SingleOrDefaultAsync(m => m.GraduateTrudoustroustvoId == id);
            if (graduateTrudoustroustvo == null)
            {
                return NotFound();
            }

            return View(graduateTrudoustroustvo);
        }

        // POST: GraduateTrudoustroustvo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var graduateTrudoustroustvo = await _context.GraduateTrudoustroustvo.SingleOrDefaultAsync(m => m.GraduateTrudoustroustvoId == id);
            _context.GraduateTrudoustroustvo.Remove(graduateTrudoustroustvo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GraduateTrudoustroustvoExists(int id)
        {
            return _context.GraduateTrudoustroustvo.Any(e => e.GraduateTrudoustroustvoId == id);
        }
    }
}
