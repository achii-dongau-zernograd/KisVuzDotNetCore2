using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;

namespace KisVuzDotNetCore2.Controllers
{
    public class priemKolMestsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public priemKolMestsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: priemKolMests
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.priemKolMest.Include(p => p.EduNapravl);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: priemKolMests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priemKolMest = await _context.priemKolMest
                .Include(p => p.EduNapravl)
                .SingleOrDefaultAsync(m => m.priemKolMestId == id);
            if (priemKolMest == null)
            {
                return NotFound();
            }

            return View(priemKolMest);
        }

        // GET: priemKolMests/Create
        public IActionResult Create()
        {
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlName");
            return View();
        }

        // POST: priemKolMests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("priemKolMestId,EduNapravlId,priemKolMestQuota_och,priemKolMestQuota_zaoch,priemKolMestQuota_och_zaoch,PriemKolMestCommon_och,PriemKolMestCommon_zaoch,PriemKolMestCommon_och_zaoch,PriemKolMestPaid_och,PriemKolMestPaid_zaoch,PriemKolMestPaid_och_zaoch")] priemKolMest priemKolMest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priemKolMest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", priemKolMest.EduNapravlId);
            return View(priemKolMest);
        }

        // GET: priemKolMests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priemKolMest = await _context.priemKolMest.SingleOrDefaultAsync(m => m.priemKolMestId == id);
            if (priemKolMest == null)
            {
                return NotFound();
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlName", priemKolMest.EduNapravlId);
            return View(priemKolMest);
        }

        // POST: priemKolMests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("priemKolMestId,EduNapravlId,priemKolMestQuota_och,priemKolMestQuota_zaoch,priemKolMestQuota_och_zaoch,PriemKolMestCommon_och,PriemKolMestCommon_zaoch,PriemKolMestCommon_och_zaoch,PriemKolMestPaid_och,PriemKolMestPaid_zaoch,PriemKolMestPaid_och_zaoch")] priemKolMest priemKolMest)
        {
            if (id != priemKolMest.priemKolMestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priemKolMest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!priemKolMestExists(priemKolMest.priemKolMestId))
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
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", priemKolMest.EduNapravlId);
            return View(priemKolMest);
        }

        // GET: priemKolMests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priemKolMest = await _context.priemKolMest
                .Include(p => p.EduNapravl)
                .SingleOrDefaultAsync(m => m.priemKolMestId == id);
            if (priemKolMest == null)
            {
                return NotFound();
            }

            return View(priemKolMest);
        }

        // POST: priemKolMests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priemKolMest = await _context.priemKolMest.SingleOrDefaultAsync(m => m.priemKolMestId == id);
            _context.priemKolMest.Remove(priemKolMest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool priemKolMestExists(int id)
        {
            return _context.priemKolMest.Any(e => e.priemKolMestId == id);
        }
    }
}
