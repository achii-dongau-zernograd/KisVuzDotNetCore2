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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">УИД учебного плана</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            if (id==null)
            {
                var appIdentityDBContext = _context.BlokDiscipl
                .Include(b => b.BlokDisciplName)
                .Include(e => e.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduPlan.EduForm);

                return View(await appIdentityDBContext.ToListAsync());
            }
            else
            {
                var appIdentityDBContext = _context.BlokDiscipl
                .Include(b => b.BlokDisciplName)
                .Include(e => e.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduPlan.EduForm)
                .Where(b => b.EduPlanId == id);
                ViewBag.EduPlanId = id;
                return View(await appIdentityDBContext.ToListAsync());
            }
            
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
                .Include(e => e.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduPlan.EduForm)
                .SingleOrDefaultAsync(m => m.BlokDisciplId == id);
            if (blokDiscipl == null)
            {
                return NotFound();
            }

            return View(blokDiscipl);
        }

        // GET: BlokDiscipls/Create
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.EduPlanId = id;
            }
            else
            {
                var EduPlans = _context.EduPlans
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduForm);                
                ViewData["EduPlans"] = new SelectList(EduPlans, "EduPlanId", "EduPlanDescription", id);                
            }
            ViewData["BlokDisciplNameId"] = new SelectList(_context.Set<BlokDisciplName>(), "BlokDisciplNameId", "BlokDisciplNameName");
            return View();
        }

        // POST: BlokDiscipls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlokDisciplId,BlokDisciplNameId,EduPlanId")] BlokDiscipl blokDiscipl, int? EduPlanIdFilter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blokDiscipl);
                await _context.SaveChangesAsync();

                if(EduPlanIdFilter!=null)
                {
                    return RedirectToAction(nameof(Index), new { id = EduPlanIdFilter });
                }

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

            var EduPlans = _context.EduPlans
               .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
               .Include(e => e.EduForm);
            ViewData["BlokDisciplNameId"] = new SelectList(_context.Set<BlokDisciplName>(), "BlokDisciplNameId", "BlokDisciplNameName", blokDiscipl.BlokDisciplNameId);
            ViewData["EduPlanId"] = new SelectList(EduPlans, "EduPlanId", "EduPlanDescription", blokDiscipl.EduPlanId);
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
                .Include(e => e.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduPlan.EduForm)
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
