using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    public class NirTemasController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public NirTemasController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: NirTemas
        public async Task<IActionResult> Index()
        {
            return View(await _context.NirTema.ToListAsync());
        }

        // GET: NirTemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirTema = await _context.NirTema
                .Include(n => n.NirTemaEduProfileList)
                    .ThenInclude(m=>m.EduProfile.EduNapravl.EduUgs.EduLevel)
                .SingleOrDefaultAsync(m => m.NirTemaId == id);
            if (nirTema == null)
            {
                return NotFound();
            }

           
            return View(nirTema);
        }

        // GET: NirTemas/Create
        public IActionResult Create()
        {
            List<EduProfile> EduProfiles = _context.EduProfiles
                .Include(p=>p.EduNapravl.EduUgs.EduLevel)
                .ToList();
            ViewData["EduProfiles"] = EduProfiles;

            return View();
        }

        // POST: NirTemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NirTemaId,NirTemaName")] NirTema nirTema, int[] EduProfileIds)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nirTema);
                await _context.SaveChangesAsync();

                if (EduProfileIds != null)
                {
                    var NirTemaEduProfile = new List<NirTemaEduProfile>();
                    foreach (var EduProfileId in EduProfileIds)
                    {
                        NirTemaEduProfile nirTemaEduProfile = new NirTemaEduProfile();
                        nirTemaEduProfile.EduProfileId = EduProfileId;
                        nirTemaEduProfile.NirTemaId = nirTema.NirTemaId;
                        NirTemaEduProfile.Add(nirTemaEduProfile);
                    }
                    await _context.NirTemaEduProfile.AddRangeAsync(NirTemaEduProfile);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            List<EduProfile> EduProfiles = _context.EduProfiles
                .Include(p => p.EduNapravl.EduUgs.EduLevel)
                .ToList();
            ViewData["EduProfiles"] = EduProfiles;

            return View(nirTema);
        }

        // GET: NirTemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirTema = await _context.NirTema
                .Include(n => n.NirTemaEduProfileList)
                .SingleOrDefaultAsync(m => m.NirTemaId == id);
            if (nirTema == null)
            {
                return NotFound();
            }
            List<EduProfile> EduProfiles = _context.EduProfiles
                .Include(p => p.EduNapravl.EduUgs.EduLevel)
                .ToList();
            ViewData["EduProfiles"] = EduProfiles;
            return View(nirTema);
        }

        // POST: NirTemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NirTemaId,NirTemaName")] NirTema nirTema, int[] EduProfileIds)
        {
            if (id != nirTema.NirTemaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nirTema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NirTemaExists(nirTema.NirTemaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (EduProfileIds != null)
                {
                    _context.NirTemaEduProfile.RemoveRange(_context.NirTemaEduProfile.Where(y => y.NirTemaId == nirTema.NirTemaId));
                    await _context.SaveChangesAsync();

                    var NirTemaEduProfile = new List<NirTemaEduProfile>();
                    foreach (var EduProfileId in EduProfileIds)
                    {
                        NirTemaEduProfile nirTemaEduProfile = new NirTemaEduProfile();
                        nirTemaEduProfile.EduProfileId = EduProfileId;
                        nirTemaEduProfile.NirTemaId = nirTema.NirTemaId;
                        NirTemaEduProfile.Add(nirTemaEduProfile);
                    }
                    await _context.NirTemaEduProfile.AddRangeAsync(NirTemaEduProfile);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }         

            return View(nirTema);
        }

        // GET: NirTemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nirTema = await _context.NirTema
                .Include(n => n.NirTemaEduProfileList)
                    .ThenInclude(m => m.EduProfile.EduNapravl.EduUgs.EduLevel)
                .SingleOrDefaultAsync(m => m.NirTemaId == id);
            if (nirTema == null)
            {
                return NotFound();
            }

            return View(nirTema);
        }

        // POST: NirTemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nirTema = await _context.NirTema.SingleOrDefaultAsync(m => m.NirTemaId == id);
            _context.NirTema.Remove(nirTema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NirTemaExists(int id)
        {
            return _context.NirTema.Any(e => e.NirTemaId == id);
        }
    }
}
