using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers.Education
{
    public class BlokDisciplNamesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public BlokDisciplNamesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: BlokDisciplNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlokDisciplName.ToListAsync());
        }

        // GET: BlokDisciplNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlokDisciplNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlokDisciplNameId,BlokDisciplNameName")] BlokDisciplName blokDisciplName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blokDisciplName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blokDisciplName);
        }

        // GET: BlokDisciplNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDisciplName = await _context.BlokDisciplName.SingleOrDefaultAsync(m => m.BlokDisciplNameId == id);
            if (blokDisciplName == null)
            {
                return NotFound();
            }
            return View(blokDisciplName);
        }

        // POST: BlokDisciplNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlokDisciplNameId,BlokDisciplNameName")] BlokDisciplName blokDisciplName)
        {
            if (id != blokDisciplName.BlokDisciplNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blokDisciplName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlokDisciplNameExists(blokDisciplName.BlokDisciplNameId))
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
            return View(blokDisciplName);
        }

        // GET: BlokDisciplNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDisciplName = await _context.BlokDisciplName
                .SingleOrDefaultAsync(m => m.BlokDisciplNameId == id);
            if (blokDisciplName == null)
            {
                return NotFound();
            }

            return View(blokDisciplName);
        }

        // POST: BlokDisciplNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blokDisciplName = await _context.BlokDisciplName.SingleOrDefaultAsync(m => m.BlokDisciplNameId == id);
            _context.BlokDisciplName.Remove(blokDisciplName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlokDisciplNameExists(int id)
        {
            return _context.BlokDisciplName.Any(e => e.BlokDisciplNameId == id);
        }
    }
}
