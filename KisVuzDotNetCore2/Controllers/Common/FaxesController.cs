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
    public class FaxesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public FaxesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Faxes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Faxes.ToListAsync());
        }

        // GET: Faxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fax = await _context.Faxes
                .SingleOrDefaultAsync(m => m.FaxId == id);
            if (fax == null)
            {
                return NotFound();
            }

            return View(fax);
        }

        // GET: Faxes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Faxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaxId,FaxValue,FaxComment")] Fax fax)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fax);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fax);
        }

        // GET: Faxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fax = await _context.Faxes.SingleOrDefaultAsync(m => m.FaxId == id);
            if (fax == null)
            {
                return NotFound();
            }
            return View(fax);
        }

        // POST: Faxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FaxId,FaxValue,FaxComment")] Fax fax)
        {
            if (id != fax.FaxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fax);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaxExists(fax.FaxId))
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
            return View(fax);
        }

        // GET: Faxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fax = await _context.Faxes
                .SingleOrDefaultAsync(m => m.FaxId == id);
            if (fax == null)
            {
                return NotFound();
            }

            return View(fax);
        }

        // POST: Faxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fax = await _context.Faxes.SingleOrDefaultAsync(m => m.FaxId == id);
            _context.Faxes.Remove(fax);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaxExists(int id)
        {
            return _context.Faxes.Any(e => e.FaxId == id);
        }
    }
}
