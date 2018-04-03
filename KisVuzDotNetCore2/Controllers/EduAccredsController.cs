using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers
{
    public class EduAccredsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduAccredsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduAccreds
        public async Task<IActionResult> Index()
        {
            var eduAccreds = await _context.EduAccreds.Include(e => e.EduAccredFile).ToListAsync();
            return View(eduAccreds);
        }

        // GET: EduAccreds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduAccred = await _context.EduAccreds
                .Include(e => e.EduAccredFile)
                .SingleOrDefaultAsync(m => m.EduAccredId == id);
            if (eduAccred == null)
            {
                return NotFound();
            }

            return View(eduAccred);
        }

        // GET: EduAccreds/Create
        public IActionResult Create()
        {
            ViewData["EduAccredFileId"] = new SelectList(_context.Files, "Id", "Name");
            return View();
        }

        // POST: EduAccreds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduAccredId,EduAccredNumber,EduAccredDate,EduAccredDateExpiration,EduAccredFileId")] EduAccred eduAccred)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduAccred);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduAccredFileId"] = new SelectList(_context.Files, "Id", "Id", eduAccred.EduAccredFileId);
            return View(eduAccred);
        }

        // GET: EduAccreds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduAccred = await _context.EduAccreds.SingleOrDefaultAsync(m => m.EduAccredId == id);
            if (eduAccred == null)
            {
                return NotFound();
            }
            ViewData["EduAccredFileId"] = new SelectList(_context.Files, "Id", "Name", eduAccred.EduAccredFileId);
            return View(eduAccred);
        }

        // POST: EduAccreds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduAccredId,EduAccredNumber,EduAccredDate,EduAccredDateExpiration,EduAccredFileId")] EduAccred eduAccred)
        {
            if (id != eduAccred.EduAccredId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduAccred);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduAccredExists(eduAccred.EduAccredId))
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
            ViewData["EduAccredFileId"] = new SelectList(_context.Files, "Id", "Id", eduAccred.EduAccredFileId);
            return View(eduAccred);
        }

        // GET: EduAccreds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduAccred = await _context.EduAccreds
                .Include(e => e.EduAccredFile)
                .SingleOrDefaultAsync(m => m.EduAccredId == id);
            if (eduAccred == null)
            {
                return NotFound();
            }

            return View(eduAccred);
        }

        // POST: EduAccreds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduAccred = await _context.EduAccreds.SingleOrDefaultAsync(m => m.EduAccredId == id);
            _context.EduAccreds.Remove(eduAccred);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduAccredExists(int id)
        {
            return _context.EduAccreds.Any(e => e.EduAccredId == id);
        }
    }
}
