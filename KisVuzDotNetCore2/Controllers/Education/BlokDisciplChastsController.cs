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
    [Authorize(Roles = "Администраторы")]
    public class BlokDisciplChastsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public BlokDisciplChastsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: BlokDisciplChasts
        public async Task<IActionResult> Index(int? id)
        {
            if (id!=null)
            {
                var appIdentityDBContext = _context.BlokDisciplChast
                    .Include(b => b.BlokDiscipl.BlokDisciplName)
                    .Include(b => b.BlokDisciplChastName)
                    .Where(b=> b.BlokDisciplId==id);
                ViewBag.BlokDisciplId = id;
                return View(await appIdentityDBContext.ToListAsync());
            }
            else
            {
                var appIdentityDBContext = _context.BlokDisciplChast
                    .Include(b => b.BlokDiscipl.BlokDisciplName)
                    .Include(b => b.BlokDisciplChastName);
                return View(await appIdentityDBContext.ToListAsync());
            }
            
        }

        // GET: BlokDisciplChasts/Details/5
        public async Task<IActionResult> Details(int? id, int? blokdisciplid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDisciplChast = await _context.BlokDisciplChast
                .Include(b => b.BlokDiscipl.BlokDisciplName)
                .Include(b => b.BlokDisciplChastName)
                .SingleOrDefaultAsync(m => m.BlokDisciplChastId == id);
            if (blokDisciplChast == null)
            {
                return NotFound();
            }

            ViewBag.BlokDisciplId = blokdisciplid;
            return View(blokDisciplChast);
        }

        // GET: BlokDisciplChasts/Create
        public IActionResult Create(int? blokdisciplid)
        {
            if (blokdisciplid != null)
            {
                ViewBag.blokdisciplid = blokdisciplid;
            }
            else
            {
                ViewData["BlokDisciplId"] = new SelectList(_context.BlokDiscipl, "BlokDisciplId", "BlokDisciplName");
            }

            ViewData["BlokDisciplChastNameId"] = new SelectList(_context.BlokDisciplChastName, "BlokDisciplChastNameId", "BlokDisciplChastNameName");
            return View();
        }

        // POST: BlokDisciplChasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlokDisciplChastId,BlokDisciplId,BlokDisciplChastNameId")] BlokDisciplChast blokDisciplChast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blokDisciplChast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { id = blokDisciplChast.BlokDisciplId});
            }
            ViewData["BlokDisciplId"] = new SelectList(_context.BlokDiscipl, "BlokDisciplId", "BlokDisciplId", blokDisciplChast.BlokDisciplId);
            ViewData["BlokDisciplChastNameId"] = new SelectList(_context.BlokDisciplChastName, "BlokDisciplChastNameId", "BlokDisciplChastNameId", blokDisciplChast.BlokDisciplChastNameId);
            return View(blokDisciplChast);
        }

        // GET: BlokDisciplChasts/Edit/5
        public async Task<IActionResult> Edit(int? id, int? blokdisciplid)
        {
            if (id == null || blokdisciplid == null)
            {
                return NotFound();
            }

            var blokDisciplChast = await _context.BlokDisciplChast.SingleOrDefaultAsync(m => m.BlokDisciplChastId == id);
            if (blokDisciplChast == null)
            {
                return NotFound();
            }
            ViewData["BlokDisciplChastNameId"] = new SelectList(_context.BlokDisciplChastName, "BlokDisciplChastNameId", "BlokDisciplChastNameName", blokDisciplChast.BlokDisciplChastNameId);
            ViewBag.BlokDisciplId = blokdisciplid;
            return View(blokDisciplChast);
        }

        // POST: BlokDisciplChasts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlokDisciplChastId,BlokDisciplId,BlokDisciplChastNameId")] BlokDisciplChast blokDisciplChast, int? blokdisciplid)
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
                return RedirectToAction(nameof(Index), new { id = blokdisciplid});
            }
            ViewData["BlokDisciplId"] = new SelectList(_context.BlokDiscipl, "BlokDisciplId", "BlokDisciplId", blokDisciplChast.BlokDisciplId);
            ViewData["BlokDisciplChastNameId"] = new SelectList(_context.BlokDisciplChastName, "BlokDisciplChastNameId", "BlokDisciplChastNameId", blokDisciplChast.BlokDisciplChastNameId);
            return View(blokDisciplChast);
        }

        // GET: BlokDisciplChasts/Delete/5
        public async Task<IActionResult> Delete(int? id, int? blokdisciplid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDisciplChast = await _context.BlokDisciplChast
                .Include(b => b.BlokDiscipl.BlokDisciplName)
                .Include(b => b.BlokDisciplChastName)
                .SingleOrDefaultAsync(m => m.BlokDisciplChastId == id);
            if (blokDisciplChast == null)
            {
                return NotFound();
            }

            ViewBag.BlokDisciplId = blokdisciplid;
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
            return RedirectToAction(nameof(Index), new { id= blokDisciplChast.BlokDisciplId});
        }

        private bool BlokDisciplChastExists(int id)
        {
            return _context.BlokDisciplChast.Any(e => e.BlokDisciplChastId == id);
        }
    }
}
