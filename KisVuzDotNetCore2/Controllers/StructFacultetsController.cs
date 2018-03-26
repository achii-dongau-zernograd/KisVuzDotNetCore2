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
    public class StructFacultetsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public StructFacultetsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: StructFacultets
        public async Task<IActionResult> Index()
        {
            return View(await _context.StructFacultets.Include(fakultet=> fakultet.StructInstitute).ToListAsync());
        }

       
        // GET: StructFacultets/Create
        public IActionResult Create()
        {
            ViewData["StructInstituteId"] = new SelectList(_context.StructInstitutes, "StructInstituteId", "StructInstituteName");
            return View();
        }

        // POST: StructFacultets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StructFacultetId,StructFacultetName,StructInstituteId")] StructFacultet structFacultet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(structFacultet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(structFacultet);
        }

        // GET: StructFacultets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structFacultet = await _context.StructFacultets.SingleOrDefaultAsync(m => m.StructFacultetId == id);
            if (structFacultet == null)
            {
                return NotFound();
            }
            ViewData["StructInstituteId"] = new SelectList(_context.StructInstitutes, "StructInstituteId", "StructInstituteName");
            return View(structFacultet);
        }

        // POST: StructFacultets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StructFacultetId,StructFacultetName,StructInstituteId")] StructFacultet structFacultet)
        {
            if (id != structFacultet.StructFacultetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(structFacultet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StructFacultetExists(structFacultet.StructFacultetId))
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
            return View(structFacultet);
        }

        // GET: StructFacultets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structFacultet = await _context.StructFacultets
                .Include(m => m.StructInstitute)
                .SingleOrDefaultAsync(m => m.StructFacultetId == id);
            if (structFacultet == null)
            {
                return NotFound();
            }

            return View(structFacultet);
        }

        // POST: StructFacultets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var structFacultet = await _context.StructFacultets.SingleOrDefaultAsync(m => m.StructFacultetId == id);
            _context.StructFacultets.Remove(structFacultet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StructFacultetExists(int id)
        {
            return _context.StructFacultets.Any(e => e.StructFacultetId == id);
        }
    }
}
