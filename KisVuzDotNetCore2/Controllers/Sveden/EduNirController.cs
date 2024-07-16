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

namespace KisVuzDotNetCore2.Controllers.Sveden
{
    [Authorize(Roles = "Администраторы, Учебная часть, Деканат инженерно-технологического факультета, Деканат факультета СПО, Деканат факультета ЭиУТ, Деканат энергетического факультета")]
    public class EduNirController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduNirController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduNir
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduNir.Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel);
            return View(await appIdentityDBContext.ToListAsync());
        }
               

        // GET: EduNir/Create
        public IActionResult Create()
        {
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls.Include(n=>n.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(n => n.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            return View();
        }

        // POST: EduNir/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EduNir eduNir)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduNir);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls.Include(n => n.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(n => n.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            return View(eduNir);
        }

        // GET: EduNir/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNir = await _context.EduNir.SingleOrDefaultAsync(m => m.EduNirId == id);
            if (eduNir == null)
            {
                return NotFound();
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls.Include(n => n.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName", eduNir.EduNapravlId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(n => n.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            return View(eduNir);
        }

        // POST: EduNir/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EduNir eduNir)
        {
            if (id != eduNir.EduNirId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduNir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduNirExists(eduNir.EduNirId))
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
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls.Include(n => n.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName", eduNir.EduNapravlId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(n => n.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            return View(eduNir);
        }

        // GET: EduNir/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNir = await _context.EduNir
                .Include(e => e.EduNapravl.EduUgs.EduLevel)
                .SingleOrDefaultAsync(m => m.EduNirId == id);
            if (eduNir == null)
            {
                return NotFound();
            }

            return View(eduNir);
        }

        // POST: EduNir/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduNir = await _context.EduNir.SingleOrDefaultAsync(m => m.EduNirId == id);
            _context.EduNir.Remove(eduNir);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduNirExists(int id)
        {
            return _context.EduNir.Any(e => e.EduNirId == id);
        }
    }
}
