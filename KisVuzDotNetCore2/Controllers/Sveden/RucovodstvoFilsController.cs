using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class RucovodstvoFilsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public RucovodstvoFilsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: RucovodstvoFils
        public async Task<IActionResult> Index()
        {
            return View(await _context.RucovodstvoFil.ToListAsync());
        }

        // GET: RucovodstvoFils/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rucovodstvoFil = await _context.RucovodstvoFil
                .SingleOrDefaultAsync(m => m.RucovodstvoFilId == id);
            if (rucovodstvoFil == null)
            {
                return NotFound();
            }

            return View(rucovodstvoFil);
        }

        // GET: RucovodstvoFils/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RucovodstvoFils/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RucovodstvoFilId,NameFil,Fio,Post,Telephone,Email")] RucovodstvoFil rucovodstvoFil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rucovodstvoFil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rucovodstvoFil);
        }

        // GET: RucovodstvoFils/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rucovodstvoFil = await _context.RucovodstvoFil.SingleOrDefaultAsync(m => m.RucovodstvoFilId == id);
            if (rucovodstvoFil == null)
            {
                return NotFound();
            }
            return View(rucovodstvoFil);
        }

        // POST: RucovodstvoFils/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RucovodstvoFilId,NameFil,Fio,Post,Telephone,Email")] RucovodstvoFil rucovodstvoFil)
        {
            if (id != rucovodstvoFil.RucovodstvoFilId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rucovodstvoFil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RucovodstvoFilExists(rucovodstvoFil.RucovodstvoFilId))
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
            return View(rucovodstvoFil);
        }

        // GET: RucovodstvoFils/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rucovodstvoFil = await _context.RucovodstvoFil
                .SingleOrDefaultAsync(m => m.RucovodstvoFilId == id);
            if (rucovodstvoFil == null)
            {
                return NotFound();
            }

            return View(rucovodstvoFil);
        }

        // POST: RucovodstvoFils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rucovodstvoFil = await _context.RucovodstvoFil.SingleOrDefaultAsync(m => m.RucovodstvoFilId == id);
            _context.RucovodstvoFil.Remove(rucovodstvoFil);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RucovodstvoFilExists(int id)
        {
            return _context.RucovodstvoFil.Any(e => e.RucovodstvoFilId == id);
        }
    }
}
