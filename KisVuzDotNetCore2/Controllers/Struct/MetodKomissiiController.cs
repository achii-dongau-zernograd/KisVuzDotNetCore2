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

namespace KisVuzDotNetCore2.Controllers.Struct
{
    [Authorize(Roles = "Администраторы")]
    public class MetodKomissiiController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public MetodKomissiiController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: MetodKomissii
        public async Task<IActionResult> Index()
        {
            return View(await _context.MetodKomissii.ToListAsync());
        }

        // GET: MetodKomissii/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodKomissiya = await _context.MetodKomissii
                .SingleOrDefaultAsync(m => m.MetodKomissiyaId == id);
            if (metodKomissiya == null)
            {
                return NotFound();
            }

            return View(metodKomissiya);
        }

        // GET: MetodKomissii/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetodKomissii/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetodKomissiyaId,MetodKomissiyaName")] MetodKomissiya metodKomissiya)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodKomissiya);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(metodKomissiya);
        }

        // GET: MetodKomissii/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodKomissiya = await _context.MetodKomissii.SingleOrDefaultAsync(m => m.MetodKomissiyaId == id);
            if (metodKomissiya == null)
            {
                return NotFound();
            }
            return View(metodKomissiya);
        }

        // POST: MetodKomissii/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetodKomissiyaId,MetodKomissiyaName")] MetodKomissiya metodKomissiya)
        {
            if (id != metodKomissiya.MetodKomissiyaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metodKomissiya);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodKomissiyaExists(metodKomissiya.MetodKomissiyaId))
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
            return View(metodKomissiya);
        }

        // GET: MetodKomissii/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodKomissiya = await _context.MetodKomissii
                .SingleOrDefaultAsync(m => m.MetodKomissiyaId == id);
            if (metodKomissiya == null)
            {
                return NotFound();
            }

            return View(metodKomissiya);
        }

        // POST: MetodKomissii/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metodKomissiya = await _context.MetodKomissii.SingleOrDefaultAsync(m => m.MetodKomissiyaId == id);
            _context.MetodKomissii.Remove(metodKomissiya);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetodKomissiyaExists(int id)
        {
            return _context.MetodKomissii.Any(e => e.MetodKomissiyaId == id);
        }
    }
}
