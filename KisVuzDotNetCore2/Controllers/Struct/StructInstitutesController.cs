using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class StructInstitutesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public StructInstitutesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: StructInstitutes
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.StructInstitutes.Include(s => s.Address).Include(s => s.University);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: StructInstitutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structInstitute = await _context.StructInstitutes
                .Include(s => s.Address)
                .Include(s => s.University)
                .SingleOrDefaultAsync(m => m.StructInstituteId == id);
            if (structInstitute == null)
            {
                return NotFound();
            }

            return View(structInstitute);
        }

        // GET: StructInstitutes/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress");
            ViewData["UniversityId"] = new SelectList(_context.StructUniversities, "StructUniversityId", "StructUniversityName");
            return View();
        }

        // POST: StructInstitutes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StructInstituteId,StructInstituteName,DateOfCreation,AddressId,ExistenceOfFilials,WorkingRegime,WorkingSchedule,UniversityId")] StructInstitute structInstitute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(structInstitute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress", structInstitute.AddressId);
            ViewData["UniversityId"] = new SelectList(_context.StructUniversities, "StructUniversityId", "StructUniversityName", structInstitute.UniversityId);
            return View(structInstitute);
        }

        // GET: StructInstitutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structInstitute = await _context.StructInstitutes.SingleOrDefaultAsync(m => m.StructInstituteId == id);
            if (structInstitute == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress", structInstitute.AddressId);
            ViewData["UniversityId"] = new SelectList(_context.StructUniversities, "StructUniversityId", "StructUniversityName", structInstitute.UniversityId);
            return View(structInstitute);
        }

        // POST: StructInstitutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StructInstituteId,StructInstituteName,DateOfCreation,AddressId,ExistenceOfFilials,WorkingRegime,WorkingSchedule,UniversityId")] StructInstitute structInstitute)
        {
            if (id != structInstitute.StructInstituteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(structInstitute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StructInstituteExists(structInstitute.StructInstituteId))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress", structInstitute.AddressId);
            ViewData["UniversityId"] = new SelectList(_context.StructUniversities, "StructUniversityId", "StructUniversityName", structInstitute.UniversityId);
            return View(structInstitute);
        }

        // GET: StructInstitutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structInstitute = await _context.StructInstitutes
                .Include(s => s.Address)
                .Include(s => s.University)
                .SingleOrDefaultAsync(m => m.StructInstituteId == id);
            if (structInstitute == null)
            {
                return NotFound();
            }

            return View(structInstitute);
        }

        // POST: StructInstitutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var structInstitute = await _context.StructInstitutes.SingleOrDefaultAsync(m => m.StructInstituteId == id);
            _context.StructInstitutes.Remove(structInstitute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StructInstituteExists(int id)
        {
            return _context.StructInstitutes.Any(e => e.StructInstituteId == id);
        }
    }
}
