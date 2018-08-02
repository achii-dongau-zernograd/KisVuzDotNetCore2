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
    public class StructSubvisionTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public StructSubvisionTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: StructSubvisionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.StructSubvisionTypes.ToListAsync());
        }

        // GET: StructSubvisionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structSubvisionType = await _context.StructSubvisionTypes
                .SingleOrDefaultAsync(m => m.StructSubvisionTypeId == id);
            if (structSubvisionType == null)
            {
                return NotFound();
            }

            return View(structSubvisionType);
        }

        // GET: StructSubvisionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StructSubvisionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StructSubvisionTypeId,StructSubvisionTypeName")] StructSubvisionType structSubvisionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(structSubvisionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(structSubvisionType);
        }

        // GET: StructSubvisionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structSubvisionType = await _context.StructSubvisionTypes.SingleOrDefaultAsync(m => m.StructSubvisionTypeId == id);
            if (structSubvisionType == null)
            {
                return NotFound();
            }
            return View(structSubvisionType);
        }

        // POST: StructSubvisionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StructSubvisionTypeId,StructSubvisionTypeName")] StructSubvisionType structSubvisionType)
        {
            if (id != structSubvisionType.StructSubvisionTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(structSubvisionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StructSubvisionTypeExists(structSubvisionType.StructSubvisionTypeId))
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
            return View(structSubvisionType);
        }

        // GET: StructSubvisionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structSubvisionType = await _context.StructSubvisionTypes
                .SingleOrDefaultAsync(m => m.StructSubvisionTypeId == id);
            if (structSubvisionType == null)
            {
                return NotFound();
            }

            return View(structSubvisionType);
        }

        // POST: StructSubvisionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var structSubvisionType = await _context.StructSubvisionTypes.SingleOrDefaultAsync(m => m.StructSubvisionTypeId == id);
            _context.StructSubvisionTypes.Remove(structSubvisionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StructSubvisionTypeExists(int id)
        {
            return _context.StructSubvisionTypes.Any(e => e.StructSubvisionTypeId == id);
        }
    }
}
