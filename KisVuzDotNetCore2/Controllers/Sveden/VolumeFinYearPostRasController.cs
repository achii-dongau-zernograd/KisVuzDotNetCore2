﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы, Бухгалтерия")]
    public class VolumeFinYearPostRasController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public VolumeFinYearPostRasController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: VolumeFinYearPostRas
        public async Task<IActionResult> Index()
        {
            return View(await _context.VolumeFinYearPostRas.ToListAsync());
        }

        // GET: VolumeFinYearPostRas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VolumeFinYearPostRas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VolumeFinYearPostRas volume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(volume);
        }

        // GET: VolumeFinYearPostRas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = await _context.VolumeFinYearPostRas.SingleOrDefaultAsync(m => m.VolumeFinYearPostRasId == id);
            if (volume == null)
            {
                return NotFound();
            }
            return View(volume);
        }

        // POST: VolumeFinYearPostRas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VolumeFinYearPostRas volume)
        {
            if (id != volume.VolumeFinYearPostRasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolumeFinYearPostRasExists(volume.VolumeFinYearPostRasId))
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
            return View(volume);
        }

        // GET: VolumeFinYearPostRas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = await _context.VolumeFinYearPostRas
                .SingleOrDefaultAsync(m => m.VolumeFinYearPostRasId == id);
            if (volume == null)
            {
                return NotFound();
            }

            return View(volume);
        }

        // POST: VolumeFinYearPostRas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volume = await _context.VolumeFinYearPostRas.SingleOrDefaultAsync(m => m.VolumeFinYearPostRasId == id);
            _context.Remove(volume);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolumeFinYearPostRasExists(int id)
        {
            return _context.VolumeFinYearPostRas.Any(e => e.VolumeFinYearPostRasId == id);
        }
    }
}
