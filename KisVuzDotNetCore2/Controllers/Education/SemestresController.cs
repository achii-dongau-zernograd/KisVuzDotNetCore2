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
    public class SemestresController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public SemestresController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Semestres
        public async Task<IActionResult> Index(int? KursId)
        {
            if (KursId != null)
            {
                var appIdentityDBContext = _context.Semestres
                .Include(s => s.Kurs)
                .Include(s => s.SemestrName)
                .Where(s => s.KursId == KursId);
                ViewBag.KursId = KursId;
                return View(await appIdentityDBContext.ToListAsync());
            }
            else
            {
                var appIdentityDBContext = _context.Semestres
                .Include(s => s.Kurs)
                .Include(s => s.SemestrName);
                return View(await appIdentityDBContext.ToListAsync());
            }
            
        }

        // GET: Semestres/Details/5
        public async Task<IActionResult> Details(int? id, int? KursId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestr = await _context.Semestres
                .Include(s => s.Kurs)
                .Include(s => s.SemestrName)
                .SingleOrDefaultAsync(m => m.SemestrId == id);
            if (semestr == null)
            {
                return NotFound();
            }

            ViewBag.KursId = KursId;
            return View(semestr);
        }

        // GET: Semestres/Create
        public IActionResult Create(int? KursId)
        {
            if (KursId != null)
            {
                ViewBag.KursId = KursId;
                ViewData["SemestrNameId"] = new SelectList(_context.SemestrNames, "SemestrNameId", "SemestrNameNumber");
                return View();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Semestres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemestrId,KursId,SemestrNameId")] Semestr semestr, int? KursId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semestr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { KursId});
            }

            ViewBag.KursId = KursId;
            ViewData["SemestrNameId"] = new SelectList(_context.SemestrNames, "SemestrNameId", "SemestrNameNumber", semestr.SemestrNameId);
            return View(semestr);
        }

        // GET: Semestres/Edit/5
        public async Task<IActionResult> Edit(int? id, int? KursId)
        {
            if (id == null || KursId == null)
            {
                return NotFound();
            }

            var semestr = await _context.Semestres.SingleOrDefaultAsync(m => m.SemestrId == id);
            if (semestr == null)
            {
                return NotFound();
            }
            
            ViewData["SemestrNameId"] = new SelectList(_context.Set<SemestrName>(), "SemestrNameId", "SemestrNameNumber", semestr.SemestrNameId);
            ViewBag.KursId = KursId;
            return View(semestr);
        }

        // POST: Semestres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SemestrId,KursId,SemestrNameId")] Semestr semestr, int KursId)
        {
            if (id != semestr.SemestrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semestr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemestrExists(semestr.SemestrId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { KursId});
            }
            
            ViewData["SemestrNameId"] = new SelectList(_context.Set<SemestrName>(), "SemestrNameId", "SemestrNameId", semestr.SemestrNameId);
            return View(semestr);
        }

        // GET: Semestres/Delete/5
        public async Task<IActionResult> Delete(int? id, int? KursId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestr = await _context.Semestres
                .Include(s => s.Kurs)
                .Include(s => s.SemestrName)
                .SingleOrDefaultAsync(m => m.SemestrId == id);
            if (semestr == null)
            {
                return NotFound();
            }

            ViewBag.KursId = KursId;
            return View(semestr);
        }

        // POST: Semestres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semestr = await _context.Semestres.SingleOrDefaultAsync(m => m.SemestrId == id);
            _context.Semestres.Remove(semestr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { KursId = semestr.KursId});
        }

        private bool SemestrExists(int id)
        {
            return _context.Semestres.Any(e => e.SemestrId == id);
        }
    }
}
