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
    public class KursesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public KursesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Kurses
        public async Task<IActionResult> Index(int? DisciplineId)
        {
            if (DisciplineId != null)
            {
                var appIdentityDBContext = _context.Kurses
                .Include(k => k.Discipline)
                .Include(k => k.EduKurs)
                .Where(k => k.DisciplineId == DisciplineId);
                ViewBag.DisciplineId = DisciplineId;
                return View(await appIdentityDBContext.ToListAsync());
            }
            else
            {
                var appIdentityDBContext = _context.Kurses
                .Include(k => k.Discipline)
                .Include(k => k.EduKurs);
                return View(await appIdentityDBContext.ToListAsync());
            }
            
        }

        // GET: Kurses/Details/5
        public async Task<IActionResult> Details(int? id, int? DisciplineId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurses
                .Include(k => k.Discipline)
                .Include(k => k.EduKurs)
                .SingleOrDefaultAsync(m => m.KursId == id);
            if (kurs == null)
            {
                return NotFound();
            }

            ViewBag.DisciplineId = DisciplineId;
            return View(kurs);
        }

        // GET: Kurses/Create
        public IActionResult Create(int? DisciplineId)
        {
            if (DisciplineId != null)
            {
                ViewBag.DisciplineId = DisciplineId;
                ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursNumber");
                return View();
            }
            else
            {
                return NotFound();
            }

        }

        // POST: Kurses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KursId,EduKursId,DisciplineId")] Kurs kurs, int? DisciplineId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { DisciplineId});
            }

            ViewBag.DisciplineId = DisciplineId;
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursNumber", kurs.EduKursId);
            return View(kurs);
        }

        // GET: Kurses/Edit/5
        public async Task<IActionResult> Edit(int? id, int? DisciplineId)
        {
            if (id == null || DisciplineId == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurses.SingleOrDefaultAsync(m => m.KursId == id);
            if (kurs == null)
            {
                return NotFound();
            }

            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursId", kurs.EduKursId);
            ViewBag.DisciplineId = DisciplineId;
            return View(kurs);
        }

        // POST: Kurses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KursId,EduKursId,DisciplineId")] Kurs kurs, int DisciplineId)
        {
            if (id != kurs.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KursExists(kurs.KursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { DisciplineId});
            }
            
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursId", kurs.EduKursId);
            return View(kurs);
        }

        // GET: Kurses/Delete/5
        public async Task<IActionResult> Delete(int? id, int? DisciplineId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurses
                .Include(k => k.Discipline)
                .Include(k => k.EduKurs)
                .SingleOrDefaultAsync(m => m.KursId == id);
            if (kurs == null)
            {
                return NotFound();
            }

            ViewBag.DisciplineId = DisciplineId;
            return View(kurs);
        }

        // POST: Kurses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kurs = await _context.Kurses.SingleOrDefaultAsync(m => m.KursId == id);
            _context.Kurses.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { DisciplineId = kurs.DisciplineId});
        }

        private bool KursExists(int id)
        {
            return _context.Kurses.Any(e => e.KursId == id);
        }
    }
}
