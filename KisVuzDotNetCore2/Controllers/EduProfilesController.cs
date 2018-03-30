﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers
{
    public class EduProfilesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduProfilesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduProfiles
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduProfiles.Include(e => e.EduNapravl);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProfile = await _context.EduProfiles
                .Include(e => e.EduNapravl)
                .SingleOrDefaultAsync(m => m.EduProfileId == id);
            if (eduProfile == null)
            {
                return NotFound();
            }

            return View(eduProfile);
        }

        // GET: EduProfiles/Create
        public IActionResult Create()
        {
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravName");
            return View();
        }

        // POST: EduProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduProfileId,EduProfileName,EduNapravlId")] EduProfile eduProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", eduProfile.EduNapravlId);
            return View(eduProfile);
        }

        // GET: EduProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProfile = await _context.EduProfiles.SingleOrDefaultAsync(m => m.EduProfileId == id);
            if (eduProfile == null)
            {
                return NotFound();
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravName", eduProfile.EduNapravlId);
            return View(eduProfile);
        }

        // POST: EduProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduProfileId,EduProfileName,EduNapravlId")] EduProfile eduProfile)
        {
            if (id != eduProfile.EduProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduProfileExists(eduProfile.EduProfileId))
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
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", eduProfile.EduNapravlId);
            return View(eduProfile);
        }

        // GET: EduProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProfile = await _context.EduProfiles
                .Include(e => e.EduNapravl)
                .SingleOrDefaultAsync(m => m.EduProfileId == id);
            if (eduProfile == null)
            {
                return NotFound();
            }

            return View(eduProfile);
        }

        // POST: EduProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduProfile = await _context.EduProfiles.SingleOrDefaultAsync(m => m.EduProfileId == id);
            _context.EduProfiles.Remove(eduProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduProfileExists(int id)
        {
            return _context.EduProfiles.Any(e => e.EduProfileId == id);
        }
    }
}
