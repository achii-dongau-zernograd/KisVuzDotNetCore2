using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    public class NirTemasController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public NirTemasController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: NirTemas
        public async Task<IActionResult> Index()
        {
            return View(await _context.NirTema.ToListAsync());
        }

        // GET: NirTemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirTema = await _context.NirTema
                .SingleOrDefaultAsync(m => m.NirTemaId == id);
            if (nirTema == null)
            {
                return NotFound();
            }

            return View(nirTema);
        }

        // GET: NirTemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NirTemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NirTemaId,NirTemaName")] NirTema nirTema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nirTema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nirTema);
        }

        // GET: NirTemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirTema = await _context.NirTema.SingleOrDefaultAsync(m => m.NirTemaId == id);
            if (nirTema == null)
            {
                return NotFound();
            }
            return View(nirTema);
        }

        // POST: NirTemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NirTemaId,NirTemaName")] NirTema nirTema)
        {
            if (id != nirTema.NirTemaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nirTema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NirTemaExists(nirTema.NirTemaId))
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
            return View(nirTema);
        }

        // GET: NirTemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirTema = await _context.NirTema
                .SingleOrDefaultAsync(m => m.NirTemaId == id);
            if (nirTema == null)
            {
                return NotFound();
            }

            return View(nirTema);
        }

        // POST: NirTemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nirTema = await _context.NirTema.SingleOrDefaultAsync(m => m.NirTemaId == id);
            _context.NirTema.Remove(nirTema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NirTemaExists(int id)
        {
            return _context.NirTema.Any(e => e.NirTemaId == id);
        }
    }
}
