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
    [Authorize(Roles = "Администраторы, Учебная часть")]
    public class priemKolTargetsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public priemKolTargetsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: priemKolTargets
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.priemKolTarget.Include(p => p.EduNapravl);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: priemKolTargets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priemKolTarget = await _context.priemKolTarget
                .Include(p => p.EduNapravl)
                .SingleOrDefaultAsync(m => m.priemKolTargetId == id);
            if (priemKolTarget == null)
            {
                return NotFound();
            }

            return View(priemKolTarget);
        }

        // GET: priemKolTargets/Create
        public IActionResult Create()
        {
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId");
            return View();
        }

        // POST: priemKolTargets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("priemKolTargetId,EduNapravlId,Mesta_v_ramkahtQuota_och,Mesta_v_ramkah_zaoch,Mesta_v_ramkah_och_zaoch,Mesta_v_pridelah_osoboi_och,Mesta_v_pridelah_osoboi_zaoch,Mesta_v_pridelah_osoboi_och_zaoch,Mesta_v_pridelah_celevoi_och,Mesta_v_pridelah_celevoi_zaoch,Mesta_v_pridelah_celevoi_och_zaoch")] priemKolTarget priemKolTarget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priemKolTarget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", priemKolTarget.EduNapravlId);
            return View(priemKolTarget);
        }

        // GET: priemKolTargets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priemKolTarget = await _context.priemKolTarget.SingleOrDefaultAsync(m => m.priemKolTargetId == id);
            if (priemKolTarget == null)
            {
                return NotFound();
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", priemKolTarget.EduNapravlId);
            return View(priemKolTarget);
        }

        // POST: priemKolTargets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("priemKolTargetId,EduNapravlId,Mesta_v_ramkahtQuota_och,Mesta_v_ramkah_zaoch,Mesta_v_ramkah_och_zaoch,Mesta_v_pridelah_osoboi_och,Mesta_v_pridelah_osoboi_zaoch,Mesta_v_pridelah_osoboi_och_zaoch,Mesta_v_pridelah_celevoi_och,Mesta_v_pridelah_celevoi_zaoch,Mesta_v_pridelah_celevoi_och_zaoch")] priemKolTarget priemKolTarget)
        {
            if (id != priemKolTarget.priemKolTargetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priemKolTarget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!priemKolTargetExists(priemKolTarget.priemKolTargetId))
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
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", priemKolTarget.EduNapravlId);
            return View(priemKolTarget);
        }

        // GET: priemKolTargets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priemKolTarget = await _context.priemKolTarget
                .Include(p => p.EduNapravl)
                .SingleOrDefaultAsync(m => m.priemKolTargetId == id);
            if (priemKolTarget == null)
            {
                return NotFound();
            }

            return View(priemKolTarget);
        }

        // POST: priemKolTargets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priemKolTarget = await _context.priemKolTarget.SingleOrDefaultAsync(m => m.priemKolTargetId == id);
            _context.priemKolTarget.Remove(priemKolTarget);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool priemKolTargetExists(int id)
        {
            return _context.priemKolTarget.Any(e => e.priemKolTargetId == id);
        }
    }
}
