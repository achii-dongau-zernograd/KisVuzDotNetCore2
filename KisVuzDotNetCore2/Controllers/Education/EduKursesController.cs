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
    public class EduKursesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduKursesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduKurses
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduKurses.ToListAsync());
        }

        // GET: EduKurses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduKurs = await _context.EduKurses
                .SingleOrDefaultAsync(m => m.EduKursId == id);
            if (eduKurs == null)
            {
                return NotFound();
            }

            return View(eduKurs);
        }

        // GET: EduKurses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduKurses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduKursId,EduKursNumber,EduKursName")] EduKurs eduKurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduKurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduKurs);
        }

        // GET: EduKurses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduKurs = await _context.EduKurses.SingleOrDefaultAsync(m => m.EduKursId == id);
            if (eduKurs == null)
            {
                return NotFound();
            }
            return View(eduKurs);
        }

        // POST: EduKurses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduKursId,EduKursNumber,EduKursName")] EduKurs eduKurs)
        {
            if (id != eduKurs.EduKursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduKurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduKursExists(eduKurs.EduKursId))
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
            return View(eduKurs);
        }

        // GET: EduKurses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduKurs = await _context.EduKurses
                .SingleOrDefaultAsync(m => m.EduKursId == id);
            if (eduKurs == null)
            {
                return NotFound();
            }

            return View(eduKurs);
        }

        // POST: EduKurses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduKurs = await _context.EduKurses.SingleOrDefaultAsync(m => m.EduKursId == id);
            _context.EduKurses.Remove(eduKurs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduKursExists(int id)
        {
            return _context.EduKurses.Any(e => e.EduKursId == id);
        }
    }
}
