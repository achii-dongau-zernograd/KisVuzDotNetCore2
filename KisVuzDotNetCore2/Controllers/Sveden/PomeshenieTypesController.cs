using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;
using KisVuzDotNetCore2.Models.Sveden;

namespace KisVuzDotNetCore2.Controllers.Sveden
{
    [Authorize(Roles = "Администраторы, Учебная часть")]
    public class PomeshenieTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public PomeshenieTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: PomeshenieTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PomeshenieType.ToListAsync());
        }

        // GET: PomeshenieTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pomeshenieType = await _context.PomeshenieType
                .SingleOrDefaultAsync(m => m.PomeshenieTypeId == id);
            if (pomeshenieType == null)
            {
                return NotFound();
            }

            return View(pomeshenieType);
        }

        // GET: PomeshenieTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PomeshenieTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PomeshenieTypeId,PomeshenieTypeName")] PomeshenieType pomeshenieType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pomeshenieType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pomeshenieType);
        }

        // GET: PomeshenieTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pomeshenieType = await _context.PomeshenieType.SingleOrDefaultAsync(m => m.PomeshenieTypeId == id);
            if (pomeshenieType == null)
            {
                return NotFound();
            }
            return View(pomeshenieType);
        }

        // POST: PomeshenieTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PomeshenieTypeId,PomeshenieTypeName")] PomeshenieType pomeshenieType)
        {
            if (id != pomeshenieType.PomeshenieTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pomeshenieType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PomeshenieTypeExists(pomeshenieType.PomeshenieTypeId))
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
            return View(pomeshenieType);
        }

        // GET: PomeshenieTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pomeshenieType = await _context.PomeshenieType
                .SingleOrDefaultAsync(m => m.PomeshenieTypeId == id);
            if (pomeshenieType == null)
            {
                return NotFound();
            }

            return View(pomeshenieType);
        }

        // POST: PomeshenieTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pomeshenieType = await _context.PomeshenieType.SingleOrDefaultAsync(m => m.PomeshenieTypeId == id);
            _context.PomeshenieType.Remove(pomeshenieType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PomeshenieTypeExists(int id)
        {
            return _context.PomeshenieType.Any(e => e.PomeshenieTypeId == id);
        }
    }
}
