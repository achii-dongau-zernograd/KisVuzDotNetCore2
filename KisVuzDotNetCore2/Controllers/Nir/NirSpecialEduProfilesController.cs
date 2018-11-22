using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    [Authorize(Roles = "Администраторы, НИЧ")]
    public class NirSpecialEduProfilesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public NirSpecialEduProfilesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: NirSpecialEduProfiles
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.NirSpecialEduProfiles.Include(n => n.EduProfile).Include(n => n.NirSpecial);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: NirSpecialEduProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirSpecialEduProfile = await _context.NirSpecialEduProfiles
                .Include(n => n.EduProfile)
                .Include(n => n.NirSpecial)
                .SingleOrDefaultAsync(m => m.NirSpecialEduProfileId == id);
            if (nirSpecialEduProfile == null)
            {
                return NotFound();
            }

            return View(nirSpecialEduProfile);
        }

        // GET: NirSpecialEduProfiles/Create
        public IActionResult Create()
        {
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId");
            ViewData["NirSpecialId"] = new SelectList(_context.NirSpecials, "NirSpecialId", "NirSpecialId");
            return View();
        }

        // POST: NirSpecialEduProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NirSpecialEduProfileId,NirSpecialId,EduProfileId")] NirSpecialEduProfile nirSpecialEduProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nirSpecialEduProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", nirSpecialEduProfile.EduProfileId);
            ViewData["NirSpecialId"] = new SelectList(_context.NirSpecials, "NirSpecialId", "NirSpecialId", nirSpecialEduProfile.NirSpecialId);
            return View(nirSpecialEduProfile);
        }

        // GET: NirSpecialEduProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirSpecialEduProfile = await _context.NirSpecialEduProfiles.SingleOrDefaultAsync(m => m.NirSpecialEduProfileId == id);
            if (nirSpecialEduProfile == null)
            {
                return NotFound();
            }
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", nirSpecialEduProfile.EduProfileId);
            ViewData["NirSpecialId"] = new SelectList(_context.NirSpecials, "NirSpecialId", "NirSpecialId", nirSpecialEduProfile.NirSpecialId);
            return View(nirSpecialEduProfile);
        }

        // POST: NirSpecialEduProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NirSpecialEduProfileId,NirSpecialId,EduProfileId")] NirSpecialEduProfile nirSpecialEduProfile)
        {
            if (id != nirSpecialEduProfile.NirSpecialEduProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nirSpecialEduProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NirSpecialEduProfileExists(nirSpecialEduProfile.NirSpecialEduProfileId))
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
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", nirSpecialEduProfile.EduProfileId);
            ViewData["NirSpecialId"] = new SelectList(_context.NirSpecials, "NirSpecialId", "NirSpecialId", nirSpecialEduProfile.NirSpecialId);
            return View(nirSpecialEduProfile);
        }

        // GET: NirSpecialEduProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirSpecialEduProfile = await _context.NirSpecialEduProfiles
                .Include(n => n.EduProfile)
                .Include(n => n.NirSpecial)
                .SingleOrDefaultAsync(m => m.NirSpecialEduProfileId == id);
            if (nirSpecialEduProfile == null)
            {
                return NotFound();
            }

            return View(nirSpecialEduProfile);
        }

        // POST: NirSpecialEduProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nirSpecialEduProfile = await _context.NirSpecialEduProfiles.SingleOrDefaultAsync(m => m.NirSpecialEduProfileId == id);
            _context.NirSpecialEduProfiles.Remove(nirSpecialEduProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NirSpecialEduProfileExists(int id)
        {
            return _context.NirSpecialEduProfiles.Any(e => e.NirSpecialEduProfileId == id);
        }
    }
}
