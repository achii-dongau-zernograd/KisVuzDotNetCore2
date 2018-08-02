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
    public class BlokDisciplChastNamesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public BlokDisciplChastNamesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: BlokDisciplChastNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlokDisciplChastName.ToListAsync());
        }

        // GET: BlokDisciplChastNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlokDisciplChastNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlokDisciplChastNameId,BlokDisciplChastNameName")] BlokDisciplChastName blokDisciplChastName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blokDisciplChastName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blokDisciplChastName);
        }

        // GET: BlokDisciplChastNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDisciplChastName = await _context.BlokDisciplChastName.SingleOrDefaultAsync(m => m.BlokDisciplChastNameId == id);
            if (blokDisciplChastName == null)
            {
                return NotFound();
            }
            return View(blokDisciplChastName);
        }

        // POST: BlokDisciplChastNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlokDisciplChastNameId,BlokDisciplChastNameName")] BlokDisciplChastName blokDisciplChastName)
        {
            if (id != blokDisciplChastName.BlokDisciplChastNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blokDisciplChastName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlokDisciplChastNameExists(blokDisciplChastName.BlokDisciplChastNameId))
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
            return View(blokDisciplChastName);
        }

        // GET: BlokDisciplChastNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDisciplChastName = await _context.BlokDisciplChastName
                .SingleOrDefaultAsync(m => m.BlokDisciplChastNameId == id);
            if (blokDisciplChastName == null)
            {
                return NotFound();
            }

            return View(blokDisciplChastName);
        }

        // POST: BlokDisciplChastNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blokDisciplChastName = await _context.BlokDisciplChastName.SingleOrDefaultAsync(m => m.BlokDisciplChastNameId == id);
            _context.BlokDisciplChastName.Remove(blokDisciplChastName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlokDisciplChastNameExists(int id)
        {
            return _context.BlokDisciplChastName.Any(e => e.BlokDisciplChastNameId == id);
        }
    }
}
