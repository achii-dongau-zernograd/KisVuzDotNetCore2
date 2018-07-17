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
    public class SemestrNamesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public SemestrNamesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: SemestrNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.SemestrNames.ToListAsync());
        }

        // GET: SemestrNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestrName = await _context.SemestrNames
                .SingleOrDefaultAsync(m => m.SemestrNameId == id);
            if (semestrName == null)
            {
                return NotFound();
            }

            return View(semestrName);
        }

        // GET: SemestrNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SemestrNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SemestrName semestrName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semestrName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semestrName);
        }

        // GET: SemestrNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestrName = await _context.SemestrNames.SingleOrDefaultAsync(m => m.SemestrNameId == id);
            if (semestrName == null)
            {
                return NotFound();
            }
            return View(semestrName);
        }

        // POST: SemestrNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SemestrNameId,SemestrNameNumber,SemestrNameName")] SemestrName semestrName)
        {
            if (id != semestrName.SemestrNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semestrName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemestrNameExists(semestrName.SemestrNameId))
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
            return View(semestrName);
        }

        // GET: SemestrNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestrName = await _context.SemestrNames
                .SingleOrDefaultAsync(m => m.SemestrNameId == id);
            if (semestrName == null)
            {
                return NotFound();
            }

            return View(semestrName);
        }

        // POST: SemestrNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semestrName = await _context.SemestrNames.SingleOrDefaultAsync(m => m.SemestrNameId == id);
            _context.SemestrNames.Remove(semestrName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SemestrNameExists(int id)
        {
            return _context.SemestrNames.Any(e => e.SemestrNameId == id);
        }
    }
}
