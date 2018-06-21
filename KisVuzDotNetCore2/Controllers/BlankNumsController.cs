using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Priem;

namespace KisVuzDotNetCore2.Controllers
{
    public class BlankNumsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public BlankNumsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: BlankNums
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.BlankNums.Include(b => b.EduNapravl);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: BlankNums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blankNum = await _context.BlankNums
                .Include(b => b.EduNapravl)
                .SingleOrDefaultAsync(m => m.BlankNumId == id);
            if (blankNum == null)
            {
                return NotFound();
            }

            return View(blankNum);
        }

        // GET: BlankNums/Create
        public IActionResult Create()
        {
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId");
            return View();
        }

        // POST: BlankNums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlankNumId,EduNapravlId,Och,Zaoch,OchZaoch")] BlankNum blankNum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blankNum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", blankNum.EduNapravlId);
            return View(blankNum);
        }

        // GET: BlankNums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blankNum = await _context.BlankNums.SingleOrDefaultAsync(m => m.BlankNumId == id);
            if (blankNum == null)
            {
                return NotFound();
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", blankNum.EduNapravlId);
            return View(blankNum);
        }

        // POST: BlankNums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlankNumId,EduNapravlId,Och,Zaoch,OchZaoch")] BlankNum blankNum)
        {
            if (id != blankNum.BlankNumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blankNum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlankNumExists(blankNum.BlankNumId))
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
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", blankNum.EduNapravlId);
            return View(blankNum);
        }

        // GET: BlankNums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blankNum = await _context.BlankNums
                .Include(b => b.EduNapravl)
                .SingleOrDefaultAsync(m => m.BlankNumId == id);
            if (blankNum == null)
            {
                return NotFound();
            }

            return View(blankNum);
        }

        // POST: BlankNums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blankNum = await _context.BlankNums.SingleOrDefaultAsync(m => m.BlankNumId == id);
            _context.BlankNums.Remove(blankNum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlankNumExists(int id)
        {
            return _context.BlankNums.Any(e => e.BlankNumId == id);
        }
    }
}
