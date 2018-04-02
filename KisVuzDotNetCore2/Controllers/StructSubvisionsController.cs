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
    public class StructSubvisionsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public StructSubvisionsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: StructSubvisions
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.StructSubvisions
                .Include(s => s.StructSubvisionAdress)
                .Include(s => s.StructSubvisionPostChief)
                .Include(s => s.StructSubvisionType)
                .Include(s=>s.StructStandingOrder);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: StructSubvisions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structSubvision = await _context.StructSubvisions
                .Include(s => s.StructSubvisionAdress)
                .Include(s => s.StructSubvisionPostChief)
                .Include(s => s.StructSubvisionType)
                .SingleOrDefaultAsync(m => m.StructSubvisionId == id);
            if (structSubvision == null)
            {
                return NotFound();
            }

            return View(structSubvision);
        }

        // GET: StructSubvisions/Create
        public IActionResult Create()
        {
            ViewData["StructSubvisionAdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            ViewData["StructSubvisionPostChiefId"] = new SelectList(_context.Posts, "PostId", "PostId");
            ViewData["StructSubvisionTypeId"] = new SelectList(_context.StructSubvisionTypes, "StructSubvisionTypeId", "StructSubvisionTypeId");
            
            ViewData["files"] = new SelectList(_context.Files, "Id", "Name");
            return View();
        }

        // POST: StructSubvisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StructSubvision structSubvision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(structSubvision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StructSubvisionAdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", structSubvision.StructSubvisionAdressId);
            ViewData["StructSubvisionPostChiefId"] = new SelectList(_context.Posts, "PostId", "PostId", structSubvision.StructSubvisionPostChiefId);
            ViewData["StructSubvisionTypeId"] = new SelectList(_context.StructSubvisionTypes, "StructSubvisionTypeId", "StructSubvisionTypeId", structSubvision.StructSubvisionTypeId);
            return View(structSubvision);
        }

        // GET: StructSubvisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structSubvision = await _context.StructSubvisions.SingleOrDefaultAsync(m => m.StructSubvisionId == id);
            if (structSubvision == null)
            {
                return NotFound();
            }
            ViewData["StructSubvisionAdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", structSubvision.StructSubvisionAdressId);
            ViewData["StructSubvisionPostChiefId"] = new SelectList(_context.Posts, "PostId", "PostId", structSubvision.StructSubvisionPostChiefId);
            ViewData["StructSubvisionTypeId"] = new SelectList(_context.StructSubvisionTypes, "StructSubvisionTypeId", "StructSubvisionTypeId", structSubvision.StructSubvisionTypeId);
            return View(structSubvision);
        }

        // POST: StructSubvisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StructSubvisionId,StructSubvisionName,StructSubvisionFioChief,StructSubvisionPostChiefId,StructSubvisionAdressId,StructSubvisionSite,StructSubvisionTypeId")] StructSubvision structSubvision)
        {
            if (id != structSubvision.StructSubvisionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(structSubvision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StructSubvisionExists(structSubvision.StructSubvisionId))
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
            ViewData["StructSubvisionAdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", structSubvision.StructSubvisionAdressId);
            ViewData["StructSubvisionPostChiefId"] = new SelectList(_context.Posts, "PostId", "PostId", structSubvision.StructSubvisionPostChiefId);
            ViewData["StructSubvisionTypeId"] = new SelectList(_context.StructSubvisionTypes, "StructSubvisionTypeId", "StructSubvisionTypeId", structSubvision.StructSubvisionTypeId);
            return View(structSubvision);
        }

        // GET: StructSubvisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structSubvision = await _context.StructSubvisions
                .Include(s => s.StructSubvisionAdress)
                .Include(s => s.StructSubvisionPostChief)
                .Include(s => s.StructSubvisionType)
                .SingleOrDefaultAsync(m => m.StructSubvisionId == id);
            if (structSubvision == null)
            {
                return NotFound();
            }

            return View(structSubvision);
        }

        // POST: StructSubvisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var structSubvision = await _context.StructSubvisions.SingleOrDefaultAsync(m => m.StructSubvisionId == id);
            _context.StructSubvisions.Remove(structSubvision);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StructSubvisionExists(int id)
        {
            return _context.StructSubvisions.Any(e => e.StructSubvisionId == id);
        }
    }
}
