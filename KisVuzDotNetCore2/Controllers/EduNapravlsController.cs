using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers
{
    public class EduNapravlsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduNapravlsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduNapravls
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduNapravls.Include(e => e.EduUgs);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduNapravls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravl = await _context.EduNapravls
                .Include(e => e.EduUgs)
                .SingleOrDefaultAsync(m => m.EduNapravlId == id);
            if (eduNapravl == null)
            {
                return NotFound();
            }

            return View(eduNapravl);
        }

        // GET: EduNapravls/Create
        public IActionResult Create()
        {
            ViewData["EduUgsId"] = new SelectList(_context.EduUgses, "EduUgsId", "EduUgsId");
            return View();
        }

        // POST: EduNapravls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduNapravlId,EduNapravCode,EduNapravName,EduUgsId")] EduNapravl eduNapravl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduNapravl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduUgsId"] = new SelectList(_context.EduUgses, "EduUgsId", "EduUgsId", eduNapravl.EduUgsId);
            return View(eduNapravl);
        }

        // GET: EduNapravls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravl = await _context.EduNapravls.SingleOrDefaultAsync(m => m.EduNapravlId == id);
            if (eduNapravl == null)
            {
                return NotFound();
            }
            ViewData["EduUgsId"] = new SelectList(_context.EduUgses, "EduUgsId", "EduUgsName", eduNapravl.EduUgsId);
            return View(eduNapravl);
        }

        // POST: EduNapravls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduNapravlId,EduNapravCode,EduNapravName,EduUgsId")] EduNapravl eduNapravl)
        {
            if (id != eduNapravl.EduNapravlId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduNapravl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduNapravlExists(eduNapravl.EduNapravlId))
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
            ViewData["EduUgsId"] = new SelectList(_context.EduUgses, "EduUgsId", "EduUgsId", eduNapravl.EduUgsId);
            return View(eduNapravl);
        }

        // GET: EduNapravls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravl = await _context.EduNapravls
                .Include(e => e.EduUgs)
                .SingleOrDefaultAsync(m => m.EduNapravlId == id);
            if (eduNapravl == null)
            {
                return NotFound();
            }

            return View(eduNapravl);
        }

        // POST: EduNapravls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduNapravl = await _context.EduNapravls.SingleOrDefaultAsync(m => m.EduNapravlId == id);
            _context.EduNapravls.Remove(eduNapravl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduNapravlExists(int id)
        {
            return _context.EduNapravls.Any(e => e.EduNapravlId == id);
        }
    }
}
