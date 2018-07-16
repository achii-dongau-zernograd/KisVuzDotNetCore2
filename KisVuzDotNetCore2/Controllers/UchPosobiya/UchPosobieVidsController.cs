using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.UchPosobiya;

namespace KisVuzDotNetCore2.Controllers.UchPosobiya
{
    public class UchPosobieVidsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UchPosobieVidsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UchPosobieVids
        public async Task<IActionResult> Index()
        {
            return View(await _context.UchPosobieVid.ToListAsync());
        }

        // GET: UchPosobieVids/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UchPosobieVids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UchPosobieVidId,UchPosobieVidName")] UchPosobieVid uchPosobieVid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uchPosobieVid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uchPosobieVid);
        }

        // GET: UchPosobieVids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieVid = await _context.UchPosobieVid.SingleOrDefaultAsync(m => m.UchPosobieVidId == id);
            if (uchPosobieVid == null)
            {
                return NotFound();
            }
            return View(uchPosobieVid);
        }

        // POST: UchPosobieVids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UchPosobieVidId,UchPosobieVidName")] UchPosobieVid uchPosobieVid)
        {
            if (id != uchPosobieVid.UchPosobieVidId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uchPosobieVid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UchPosobieVidExists(uchPosobieVid.UchPosobieVidId))
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
            return View(uchPosobieVid);
        }

        // GET: UchPosobieVids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieVid = await _context.UchPosobieVid
                .SingleOrDefaultAsync(m => m.UchPosobieVidId == id);
            if (uchPosobieVid == null)
            {
                return NotFound();
            }

            return View(uchPosobieVid);
        }

        // POST: UchPosobieVids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uchPosobieVid = await _context.UchPosobieVid.SingleOrDefaultAsync(m => m.UchPosobieVidId == id);
            _context.UchPosobieVid.Remove(uchPosobieVid);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UchPosobieVidExists(int id)
        {
            return _context.UchPosobieVid.Any(e => e.UchPosobieVidId == id);
        }
    }
}
