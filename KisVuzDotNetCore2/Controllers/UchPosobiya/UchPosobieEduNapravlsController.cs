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
    public class UchPosobieEduNapravlsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UchPosobieEduNapravlsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UchPosobieEduNapravls
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.UchPosobieEduNapravl.Include(u => u.EduNapravl).Include(u => u.UchPosobie);
            return View(await appIdentityDBContext.ToListAsync());
        }

         // GET: UchPosobieEduNapravls/Create
        public IActionResult Create()
        {
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls.Include(n => n.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName");
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName");
            return View();
        }

        // POST: UchPosobieEduNapravls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UchPosobieEduNapravlId,UchPosobieId,EduNapravlId")] UchPosobieEduNapravl uchPosobieEduNapravl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uchPosobieEduNapravl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", uchPosobieEduNapravl.EduNapravlId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieId", uchPosobieEduNapravl.UchPosobieId);
            return View(uchPosobieEduNapravl);
        }

        // GET: UchPosobieEduNapravls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieEduNapravl = await _context.UchPosobieEduNapravl.SingleOrDefaultAsync(m => m.UchPosobieEduNapravlId == id);
            if (uchPosobieEduNapravl == null)
            {
                return NotFound();
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlName", uchPosobieEduNapravl.EduNapravlId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName", uchPosobieEduNapravl.UchPosobieId);
            return View(uchPosobieEduNapravl);
        }

        // POST: UchPosobieEduNapravls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UchPosobieEduNapravlId,UchPosobieId,EduNapravlId")] UchPosobieEduNapravl uchPosobieEduNapravl)
        {
            if (id != uchPosobieEduNapravl.UchPosobieEduNapravlId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uchPosobieEduNapravl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UchPosobieEduNapravlExists(uchPosobieEduNapravl.UchPosobieEduNapravlId))
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
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlName", uchPosobieEduNapravl.EduNapravlId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName", uchPosobieEduNapravl.UchPosobieId);
            return View(uchPosobieEduNapravl);
        }

        // GET: UchPosobieEduNapravls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieEduNapravl = await _context.UchPosobieEduNapravl
                .Include(u => u.EduNapravl)
                .Include(u => u.UchPosobie)
                .SingleOrDefaultAsync(m => m.UchPosobieEduNapravlId == id);
            if (uchPosobieEduNapravl == null)
            {
                return NotFound();
            }

            return View(uchPosobieEduNapravl);
        }

        // POST: UchPosobieEduNapravls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uchPosobieEduNapravl = await _context.UchPosobieEduNapravl.SingleOrDefaultAsync(m => m.UchPosobieEduNapravlId == id);
            _context.UchPosobieEduNapravl.Remove(uchPosobieEduNapravl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UchPosobieEduNapravlExists(int id)
        {
            return _context.UchPosobieEduNapravl.Any(e => e.UchPosobieEduNapravlId == id);
        }
    }
}
