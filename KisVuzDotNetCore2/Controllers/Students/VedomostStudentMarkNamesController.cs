using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Students;

namespace KisVuzDotNetCore2.Controllers
{
    public class VedomostStudentMarkNamesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public VedomostStudentMarkNamesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: VedomostStudentMarkNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.VedomostStudentMarkNames.ToListAsync());
        }

        // GET: VedomostStudentMarkNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vedomostStudentMarkName = await _context.VedomostStudentMarkNames
                .SingleOrDefaultAsync(m => m.VedomostStudentMarkNameId == id);
            if (vedomostStudentMarkName == null)
            {
                return NotFound();
            }

            return View(vedomostStudentMarkName);
        }

        // GET: VedomostStudentMarkNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VedomostStudentMarkNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VedomostStudentMarkNameId,VedomostStudentMarkNameName")] VedomostStudentMarkName vedomostStudentMarkName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vedomostStudentMarkName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vedomostStudentMarkName);
        }

        // GET: VedomostStudentMarkNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vedomostStudentMarkName = await _context.VedomostStudentMarkNames.SingleOrDefaultAsync(m => m.VedomostStudentMarkNameId == id);
            if (vedomostStudentMarkName == null)
            {
                return NotFound();
            }
            return View(vedomostStudentMarkName);
        }

        // POST: VedomostStudentMarkNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VedomostStudentMarkNameId,VedomostStudentMarkNameName")] VedomostStudentMarkName vedomostStudentMarkName)
        {
            if (id != vedomostStudentMarkName.VedomostStudentMarkNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vedomostStudentMarkName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VedomostStudentMarkNameExists(vedomostStudentMarkName.VedomostStudentMarkNameId))
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
            return View(vedomostStudentMarkName);
        }

        // GET: VedomostStudentMarkNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vedomostStudentMarkName = await _context.VedomostStudentMarkNames
                .SingleOrDefaultAsync(m => m.VedomostStudentMarkNameId == id);
            if (vedomostStudentMarkName == null)
            {
                return NotFound();
            }

            return View(vedomostStudentMarkName);
        }

        // POST: VedomostStudentMarkNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vedomostStudentMarkName = await _context.VedomostStudentMarkNames.SingleOrDefaultAsync(m => m.VedomostStudentMarkNameId == id);
            _context.VedomostStudentMarkNames.Remove(vedomostStudentMarkName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VedomostStudentMarkNameExists(int id)
        {
            return _context.VedomostStudentMarkNames.Any(e => e.VedomostStudentMarkNameId == id);
        }
    }
}
