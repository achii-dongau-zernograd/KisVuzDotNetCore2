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
    public class BlokDisciplsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public BlokDisciplsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: BlokDiscipls
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.BlokDiscipl.Include(b => b.BlokDisciplName).Include(b => b.EduPlan);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: BlokDiscipls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDiscipl = await _context.BlokDiscipl
                .Include(b => b.BlokDisciplName)
                .Include(b => b.EduPlan)
                .SingleOrDefaultAsync(m => m.BlokDisciplId == id);
            if (blokDiscipl == null)
            {
                return NotFound();
            }

            return View(blokDiscipl);
        }

        // GET: BlokDiscipls/Create
        public IActionResult Create()
        {
            ViewData["BlokDisciplNameId"] = new SelectList(_context.Set<BlokDisciplName>(), "BlokDisciplNameId", "BlokDisciplNameId");
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanId");
            return View();
        }

        // POST: BlokDiscipls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlokDisciplId,BlokDisciplNameId,EduPlanId")] BlokDiscipl blokDiscipl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blokDiscipl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlokDisciplNameId"] = new SelectList(_context.Set<BlokDisciplName>(), "BlokDisciplNameId", "BlokDisciplNameId", blokDiscipl.BlokDisciplNameId);
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanId", blokDiscipl.EduPlanId);
            return View(blokDiscipl);
        }

        // GET: BlokDiscipls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDiscipl = await _context.BlokDiscipl.SingleOrDefaultAsync(m => m.BlokDisciplId == id);
            if (blokDiscipl == null)
            {
                return NotFound();
            }
            ViewData["BlokDisciplNameId"] = new SelectList(_context.Set<BlokDisciplName>(), "BlokDisciplNameId", "BlokDisciplNameId", blokDiscipl.BlokDisciplNameId);
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanId", blokDiscipl.EduPlanId);
            return View(blokDiscipl);
        }

        // POST: BlokDiscipls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlokDisciplId,BlokDisciplNameId,EduPlanId")] BlokDiscipl blokDiscipl)
        {
            if (id != blokDiscipl.BlokDisciplId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blokDiscipl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlokDisciplExists(blokDiscipl.BlokDisciplId))
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
            ViewData["BlokDisciplNameId"] = new SelectList(_context.Set<BlokDisciplName>(), "BlokDisciplNameId", "BlokDisciplNameId", blokDiscipl.BlokDisciplNameId);
            ViewData["EduPlanId"] = new SelectList(_context.EduPlans, "EduPlanId", "EduPlanId", blokDiscipl.EduPlanId);
            return View(blokDiscipl);
        }

        // GET: BlokDiscipls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokDiscipl = await _context.BlokDiscipl
                .Include(b => b.BlokDisciplName)
                .Include(b => b.EduPlan)
                .SingleOrDefaultAsync(m => m.BlokDisciplId == id);
            if (blokDiscipl == null)
            {
                return NotFound();
            }

            return View(blokDiscipl);
        }

        // POST: BlokDiscipls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blokDiscipl = await _context.BlokDiscipl.SingleOrDefaultAsync(m => m.BlokDisciplId == id);
            _context.BlokDiscipl.Remove(blokDiscipl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlokDisciplExists(int id)
        {
            return _context.BlokDiscipl.Any(e => e.BlokDisciplId == id);
        }
    }
}
