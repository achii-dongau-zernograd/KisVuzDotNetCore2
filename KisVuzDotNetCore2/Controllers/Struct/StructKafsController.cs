using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Struct;

namespace KisVuzDotNetCore2.Controllers
{
    public class StructKafsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public StructKafsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: StructKafs
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.StructKafs
                .Include(s => s.StructFacultet)
                    .ThenInclude(s=>s.StructSubvision)
                .Include(s => s.StructSubvision);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: StructKafs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structKaf = await _context.StructKafs
                .Include(s => s.StructFacultet)
                    .ThenInclude(f=>f.StructSubvision)
                .Include(s => s.StructSubvision)
                .SingleOrDefaultAsync(m => m.StructKafId == id);
            if (structKaf == null)
            {
                return NotFound();
            }

            return View(structKaf);
        }

        // GET: StructKafs/Create
        public IActionResult Create()
        {
            ViewData["StructFacultetId"] = new SelectList(_context.StructFacultets.Include(s=>s.StructSubvision) , "StructFacultetId", "StructSubvision.StructSubvisionName");
            ViewData["StructSubvisionId"] = new SelectList(_context.StructSubvisions, "StructSubvisionId", "StructSubvisionName");
            return View();
        }

        // POST: StructKafs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StructKafId,StructFacultetId,StructSubvisionId")] StructKaf structKaf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(structKaf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StructFacultetId"] = new SelectList(_context.StructFacultets, "StructFacultetId", "StructFacultetId", structKaf.StructFacultetId);
            ViewData["StructSubvisionId"] = new SelectList(_context.StructSubvisions, "StructSubvisionId", "StructSubvisionId", structKaf.StructSubvisionId);
            return View(structKaf);
        }

        // GET: StructKafs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structKaf = await _context.StructKafs.SingleOrDefaultAsync(m => m.StructKafId == id);
            if (structKaf == null)
            {
                return NotFound();
            }
            ViewData["StructFacultetId"] = new SelectList(_context.StructFacultets.Include(f=>f.StructSubvision), "StructFacultetId", "StructSubvision.StructSubvisionName", structKaf.StructFacultetId);
            ViewData["StructSubvisionId"] = new SelectList(_context.StructSubvisions, "StructSubvisionId", "StructSubvisionName", structKaf.StructSubvisionId);
            return View(structKaf);
        }

        // POST: StructKafs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StructKafId,StructFacultetId,StructSubvisionId")] StructKaf structKaf)
        {
            if (id != structKaf.StructKafId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(structKaf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StructKafExists(structKaf.StructKafId))
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
            ViewData["StructFacultetId"] = new SelectList(_context.StructFacultets, "StructFacultetId", "StructFacultetId", structKaf.StructFacultetId);
            ViewData["StructSubvisionId"] = new SelectList(_context.StructSubvisions, "StructSubvisionId", "StructSubvisionId", structKaf.StructSubvisionId);
            return View(structKaf);
        }

        // GET: StructKafs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structKaf = await _context.StructKafs
                .Include(s => s.StructFacultet)
                    .ThenInclude(f=>f.StructSubvision)
                .Include(s => s.StructSubvision)
                .SingleOrDefaultAsync(m => m.StructKafId == id);
            if (structKaf == null)
            {
                return NotFound();
            }

            return View(structKaf);
        }

        // POST: StructKafs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var structKaf = await _context.StructKafs.SingleOrDefaultAsync(m => m.StructKafId == id);
            _context.StructKafs.Remove(structKaf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StructKafExists(int id)
        {
            return _context.StructKafs.Any(e => e.StructKafId == id);
        }
    }
}
