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
    public class BlokDisciplChastsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public BlokDisciplChastsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: BlokDisciplChasts
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.BlokDisciplChast.Include(b => b.BlokDisciplChastName);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: BlokDisciplChasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDisciplChast = await _context.BlokDisciplChast
                .Include(b => b.BlokDisciplChastName)
                .SingleOrDefaultAsync(m => m.BlokDisciplChastId == id);
            if (blokDisciplChast == null)
            {
                return NotFound();
            }

            return View(blokDisciplChast);
        }

        // GET: BlokDisciplChasts/Create
        public IActionResult Create()
        {
            ViewData["BlokDisciplChastNameId"] = new SelectList(_context.BlokDisciplChastName, "BlokDisciplChastNameId", "BlokDisciplChastNameId");
            return View();
        }

        // POST: BlokDisciplChasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlokDisciplChastId,BlokDisciplChastNameId")] BlokDisciplChast blokDisciplChast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blokDisciplChast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlokDisciplChastNameId"] = new SelectList(_context.BlokDisciplChastName, "BlokDisciplChastNameId", "BlokDisciplChastNameId", blokDisciplChast.BlokDisciplChastNameId);
            return View(blokDisciplChast);
        }

        // GET: BlokDisciplChasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDisciplChast = await _context.BlokDisciplChast.SingleOrDefaultAsync(m => m.BlokDisciplChastId == id);
            if (blokDisciplChast == null)
            {
                return NotFound();
            }
            ViewData["BlokDisciplChastNameId"] = new SelectList(_context.BlokDisciplChastName, "BlokDisciplChastNameId", "BlokDisciplChastNameId", blokDisciplChast.BlokDisciplChastNameId);
            return View(blokDisciplChast);
        }

        // POST: BlokDisciplChasts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlokDisciplChastId,BlokDisciplChastNameId")] BlokDisciplChast blokDisciplChast)
        {
            if (id != blokDisciplChast.BlokDisciplChastId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blokDisciplChast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlokDisciplChastExists(blokDisciplChast.BlokDisciplChastId))
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
            ViewData["BlokDisciplChastNameId"] = new SelectList(_context.BlokDisciplChastName, "BlokDisciplChastNameId", "BlokDisciplChastNameId", blokDisciplChast.BlokDisciplChastNameId);
            return View(blokDisciplChast);
        }

        // GET: BlokDisciplChasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDisciplChast = await _context.BlokDisciplChast
                .Include(b => b.BlokDisciplChastName)
                .SingleOrDefaultAsync(m => m.BlokDisciplChastId == id);
            if (blokDisciplChast == null)
            {
                return NotFound();
            }

            return View(blokDisciplChast);
        }

        // POST: BlokDisciplChasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blokDisciplChast = await _context.BlokDisciplChast.SingleOrDefaultAsync(m => m.BlokDisciplChastId == id);
            _context.BlokDisciplChast.Remove(blokDisciplChast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlokDisciplChastExists(int id)
        {
            return _context.BlokDisciplChast.Any(e => e.BlokDisciplChastId == id);
        }
    }
}
