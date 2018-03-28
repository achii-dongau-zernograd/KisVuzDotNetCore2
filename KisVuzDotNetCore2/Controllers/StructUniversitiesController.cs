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
    public class StructUniversitiesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public StructUniversitiesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: StructUniversities
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.StructUniversities.Include(s => s.Address);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: StructUniversities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structUniversity = await _context.StructUniversities
                .Include(s => s.Address)
                .SingleOrDefaultAsync(m => m.StructUniversityId == id);
            if (structUniversity == null)
            {
                return NotFound();
            }

            return View(structUniversity);
        }

        // GET: StructUniversities/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            return View();
        }

        // POST: StructUniversities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StructUniversityId,StructUniversityName,DateOfCreation,AddressId,ExistenceOfFilials,WorkingRegime,WorkingSchedule")] StructUniversity structUniversity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(structUniversity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", structUniversity.AddressId);
            return View(structUniversity);
        }

        // GET: StructUniversities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structUniversity = await _context.StructUniversities.SingleOrDefaultAsync(m => m.StructUniversityId == id);
            if (structUniversity == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", structUniversity.AddressId);
            return View(structUniversity);
        }

        // POST: StructUniversities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StructUniversityId,StructUniversityName,DateOfCreation,AddressId,ExistenceOfFilials,WorkingRegime,WorkingSchedule")] StructUniversity structUniversity)
        {
            if (id != structUniversity.StructUniversityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(structUniversity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StructUniversityExists(structUniversity.StructUniversityId))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", structUniversity.AddressId);
            return View(structUniversity);
        }

        // GET: StructUniversities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structUniversity = await _context.StructUniversities
                .Include(s => s.Address)
                .SingleOrDefaultAsync(m => m.StructUniversityId == id);
            if (structUniversity == null)
            {
                return NotFound();
            }

            return View(structUniversity);
        }

        // POST: StructUniversities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var structUniversity = await _context.StructUniversities.SingleOrDefaultAsync(m => m.StructUniversityId == id);
            _context.StructUniversities.Remove(structUniversity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StructUniversityExists(int id)
        {
            return _context.StructUniversities.Any(e => e.StructUniversityId == id);
        }
    }
}
