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
    public class FilInfoesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public FilInfoesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: FilInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FilInfo.ToListAsync());
        }

        // GET: FilInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filInfo = await _context.FilInfo
                .SingleOrDefaultAsync(m => m.FilInfoId == id);
            if (filInfo == null)
            {
                return NotFound();
            }

            return View(filInfo);
        }

        // GET: FilInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FilInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilInfoId,NameFil,AddressFil,WebsiteFil")] FilInfo filInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filInfo);
        }

        // GET: FilInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filInfo = await _context.FilInfo.SingleOrDefaultAsync(m => m.FilInfoId == id);
            if (filInfo == null)
            {
                return NotFound();
            }
            return View(filInfo);
        }

        // POST: FilInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilInfoId,NameFil,AddressFil,WebsiteFil")] FilInfo filInfo)
        {
            if (id != filInfo.FilInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilInfoExists(filInfo.FilInfoId))
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
            return View(filInfo);
        }

        // GET: FilInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filInfo = await _context.FilInfo
                .SingleOrDefaultAsync(m => m.FilInfoId == id);
            if (filInfo == null)
            {
                return NotFound();
            }

            return View(filInfo);
        }

        // POST: FilInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filInfo = await _context.FilInfo.SingleOrDefaultAsync(m => m.FilInfoId == id);
            _context.FilInfo.Remove(filInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilInfoExists(int id)
        {
            return _context.FilInfo.Any(e => e.FilInfoId == id);
        }
    }
}
