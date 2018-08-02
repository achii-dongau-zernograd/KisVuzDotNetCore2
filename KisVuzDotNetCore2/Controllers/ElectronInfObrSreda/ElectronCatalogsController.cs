using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class ElectronCatalogsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public ElectronCatalogsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: ElectronCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ElectronCatalog.ToListAsync());
        }

        // GET: ElectronCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronCatalog = await _context.ElectronCatalog
                .SingleOrDefaultAsync(m => m.ElectronCatalogId == id);
            if (electronCatalog == null)
            {
                return NotFound();
            }

            return View(electronCatalog);
        }

        // GET: ElectronCatalogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ElectronCatalogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElectronCatalogId,NameEc,DescriptionEc")] ElectronCatalog electronCatalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(electronCatalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(electronCatalog);
        }

        // GET: ElectronCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronCatalog = await _context.ElectronCatalog.SingleOrDefaultAsync(m => m.ElectronCatalogId == id);
            if (electronCatalog == null)
            {
                return NotFound();
            }
            return View(electronCatalog);
        }

        // POST: ElectronCatalogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElectronCatalogId,NameEc,DescriptionEc")] ElectronCatalog electronCatalog)
        {
            if (id != electronCatalog.ElectronCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electronCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectronCatalogExists(electronCatalog.ElectronCatalogId))
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
            return View(electronCatalog);
        }

        // GET: ElectronCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronCatalog = await _context.ElectronCatalog
                .SingleOrDefaultAsync(m => m.ElectronCatalogId == id);
            if (electronCatalog == null)
            {
                return NotFound();
            }

            return View(electronCatalog);
        }

        // POST: ElectronCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electronCatalog = await _context.ElectronCatalog.SingleOrDefaultAsync(m => m.ElectronCatalogId == id);
            _context.ElectronCatalog.Remove(electronCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Preview()
        {
            var appIdentityDBContext = _context.ElectronCatalog;
            return View(await appIdentityDBContext.ToListAsync());
        }


        private bool ElectronCatalogExists(int id)
        {
            return _context.ElectronCatalog.Any(e => e.ElectronCatalogId == id);
        }
    }
}
