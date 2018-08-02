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
    public class SemestrFormKontrolNamesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public SemestrFormKontrolNamesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: SemestrFormKontrolNames
        public async Task<IActionResult> Index(int? SemestrId)
        {
            if (SemestrId != null)
            {
                var appIdentityDBContext = _context.SemestrFormKontrolName
                .Include(s => s.FormKontrolName)
                .Include(s => s.Semestr)
                .Where(s => s.SemestrId == SemestrId);
                ViewBag.SemestrId = SemestrId;
                return View(await appIdentityDBContext.ToListAsync());
            }
            else
            {
                var appIdentityDBContext = _context.SemestrFormKontrolName
                    .Include(s => s.FormKontrolName)
                    .Include(s => s.Semestr);
                return View(await appIdentityDBContext.ToListAsync());
            }
                
        }

        // GET: SemestrFormKontrolNames/Details/5
        public async Task<IActionResult> Details(int? id, int? SemestrId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestrFormKontrolName = await _context.SemestrFormKontrolName
                .Include(s => s.FormKontrolName)
                .Include(s => s.Semestr)
                .SingleOrDefaultAsync(m => m.SemestrFormKontrolNameId == id);
            if (semestrFormKontrolName == null)
            {
                return NotFound();
            }

            ViewBag.SemestrId = SemestrId;
            return View(semestrFormKontrolName);
        }

        // GET: SemestrFormKontrolNames/Create
        public IActionResult Create(int? SemestrId)
        {
            if (SemestrId != null)
            {
                ViewBag.SemestrId = SemestrId;
                ViewData["FormKontrolNameId"] = new SelectList(_context.FormKontrolNames, "FormKontrolNameId", "FormKontrolNameName");                
                return View();
            }
            else
            {
                return NotFound();
            }

        }

        // POST: SemestrFormKontrolNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemestrFormKontrolNameId,FormKontrolNameId,SemestrId")] SemestrFormKontrolName semestrFormKontrolName, int? SemestrId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semestrFormKontrolName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { SemestrId});
            }

            ViewBag.SemestrId = SemestrId;
            ViewData["FormKontrolNameId"] = new SelectList(_context.FormKontrolNames, "FormKontrolNameId", "FormKontrolNameName", semestrFormKontrolName.FormKontrolNameId);            
            return View(semestrFormKontrolName);
        }

        // GET: SemestrFormKontrolNames/Edit/5
        public async Task<IActionResult> Edit(int? id, int? SemestrId)
        {
            if (id == null || SemestrId == null)
            {
                return NotFound();
            }

            var semestrFormKontrolName = await _context.SemestrFormKontrolName.SingleOrDefaultAsync(m => m.SemestrFormKontrolNameId == id);
            if (semestrFormKontrolName == null)
            {
                return NotFound();
            }

            ViewData["FormKontrolNameId"] = new SelectList(_context.FormKontrolNames, "FormKontrolNameId", "FormKontrolNameName", semestrFormKontrolName.FormKontrolNameId);
            ViewBag.SemestrId = SemestrId;
            return View(semestrFormKontrolName);
        }

        // POST: SemestrFormKontrolNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SemestrFormKontrolNameId,FormKontrolNameId,SemestrId")] SemestrFormKontrolName semestrFormKontrolName, int SemestrId)
        {
            if (id != semestrFormKontrolName.SemestrFormKontrolNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semestrFormKontrolName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemestrFormKontrolNameExists(semestrFormKontrolName.SemestrFormKontrolNameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { SemestrId});
            }
            ViewData["FormKontrolNameId"] = new SelectList(_context.FormKontrolNames, "FormKontrolNameId", "FormKontrolNameName", semestrFormKontrolName.FormKontrolNameId);
            return View(semestrFormKontrolName);
        }

        // GET: SemestrFormKontrolNames/Delete/5
        public async Task<IActionResult> Delete(int? id, int? SemestrId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestrFormKontrolName = await _context.SemestrFormKontrolName
                .Include(s => s.FormKontrolName)
                .Include(s => s.Semestr)
                .SingleOrDefaultAsync(m => m.SemestrFormKontrolNameId == id);
            if (semestrFormKontrolName == null)
            {
                return NotFound();
            }

            ViewBag.SemestrId = SemestrId;
            return View(semestrFormKontrolName);
        }

        // POST: SemestrFormKontrolNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semestrFormKontrolName = await _context.SemestrFormKontrolName.SingleOrDefaultAsync(m => m.SemestrFormKontrolNameId == id);
            _context.SemestrFormKontrolName.Remove(semestrFormKontrolName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { SemestrId = semestrFormKontrolName.SemestrId });
        }

        private bool SemestrFormKontrolNameExists(int id)
        {
            return _context.SemestrFormKontrolName.Any(e => e.SemestrFormKontrolNameId == id);
        }
    }
}
