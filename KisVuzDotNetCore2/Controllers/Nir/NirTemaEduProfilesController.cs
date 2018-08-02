using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    [Authorize(Roles = "Администраторы")]
    public class NirTemaEduProfilesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public NirTemaEduProfilesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: NirTemaEduProfiles
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.NirTemaEduProfile.Include(n => n.EduProfile.EduNapravl.EduUgs.EduLevel).Include(n => n.NirTema);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: NirTemaEduProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirTemaEduProfile = await _context.NirTemaEduProfile
                .Include(n => n.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(n => n.NirTema)
                .SingleOrDefaultAsync(m => m.NirTemaEduProfileId == id);
            if (nirTemaEduProfile == null)
            {
                return NotFound();
            }

            return View(nirTemaEduProfile);
        }

        // GET: NirTemaEduProfiles/Create
        public IActionResult Create()
        {
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(m => m.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            ViewData["NirTemaId"] = new SelectList(_context.NirTema, "NirTemaId", "NirTemaName");

            

            return View();
        }

        // POST: NirTemaEduProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NirTemaEduProfileId,NirTemaId,EduProfileId")] NirTemaEduProfile nirTemaEduProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nirTemaEduProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(m => m.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", nirTemaEduProfile.EduProfileId);
            ViewData["NirTemaId"] = new SelectList(_context.NirTema, "NirTemaId", "NirTemaName", nirTemaEduProfile.NirTemaId);

            

            return View(nirTemaEduProfile);
        }

        // GET: NirTemaEduProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirTemaEduProfile = await _context.NirTemaEduProfile.SingleOrDefaultAsync(m => m.NirTemaEduProfileId == id);
            if (nirTemaEduProfile == null)
            {
                return NotFound();
            }
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(m => m.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", nirTemaEduProfile.EduProfileId);
            ViewData["NirTemaId"] = new SelectList(_context.NirTema, "NirTemaId", "NirTemaName", nirTemaEduProfile.NirTemaId);
            return View(nirTemaEduProfile);
        }

        // POST: NirTemaEduProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NirTemaEduProfileId,NirTemaId,EduProfileId")] NirTemaEduProfile nirTemaEduProfile)
        {
            if (id != nirTemaEduProfile.NirTemaEduProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nirTemaEduProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NirTemaEduProfileExists(nirTemaEduProfile.NirTemaEduProfileId))
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
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(m => m.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", nirTemaEduProfile.EduProfileId);
            ViewData["NirTemaId"] = new SelectList(_context.NirTema, "NirTemaId", "NirTemaName", nirTemaEduProfile.NirTemaId);
            return View(nirTemaEduProfile);
        }

        // GET: NirTemaEduProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirTemaEduProfile = await _context.NirTemaEduProfile
                .Include(n => n.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(n => n.NirTema)
                .SingleOrDefaultAsync(m => m.NirTemaEduProfileId == id);
            if (nirTemaEduProfile == null)
            {
                return NotFound();
            }

            return View(nirTemaEduProfile);
        }

        // POST: NirTemaEduProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nirTemaEduProfile = await _context.NirTemaEduProfile.SingleOrDefaultAsync(m => m.NirTemaEduProfileId == id);
            _context.NirTemaEduProfile.Remove(nirTemaEduProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NirTemaEduProfileExists(int id)
        {
            return _context.NirTemaEduProfile.Any(e => e.NirTemaEduProfileId == id);
        }
    }
}
