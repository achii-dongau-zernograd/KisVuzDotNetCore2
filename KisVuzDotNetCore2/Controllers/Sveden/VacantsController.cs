using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Priem;
using KisVuzDotNetCore2.Models.Education;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы, Учебная часть")]
    public class VacantsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public VacantsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduVacants
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.Vacants
                .Include(v => v.EduProfile)
                .Include(v => v.EduNapravl)
                    .ThenInclude(n => n.EduUgs)
                        .ThenInclude(ugs => ugs.EduLevel)
                .Include(v => v.EduForm)
                .Include(v => v.EduKurs);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduVacants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacant = await _context.Vacants
                .Include(v => v.EduForm)
                .Include(v => v.EduKurs)
                .Include(v => v.EduNapravl)
                .Include(v => v.EduNapravl.EduUgs.EduLevel)
                .SingleOrDefaultAsync(m => m.VacantId == id);
            if (vacant == null)
            {
                return NotFound();
            }

            return View(vacant);
        }

        // GET: EduVacants/Create
        public IActionResult Create()
        {
            ViewData["EduForms"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursId");
            ViewData["GetEduNapravlFullName"] = new SelectList(_context.EduNapravls.Include(v => v.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName");
            return View();
        }

        // POST: EduVacants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VacantId,NumberBFVacant,NumberBRVacant,NumberBMVacant,NumberPVacant,EduFormId,EduKursId,EduNapravlId")] Vacant vacant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", vacant.EduFormId);
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursId", vacant.EduKursId);
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "EduNapravlId", vacant.EduNapravlId);
            return View(vacant);
        }

        // GET: EduVacants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacant = await _context.Vacants
                .Include(v=>v.EduNapravl.EduUgs)
                .Include(v => v.EduProfile.EduNapravl.EduUgs)
                .SingleOrDefaultAsync(m => m.VacantId == id);
            if (vacant == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", vacant.EduFormId);
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursId", vacant.EduKursId);
            //ViewData["GetEduNapravlFullName"] = new SelectList(_context.EduNapravls.Include(v => v.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName", vacant.EduNapravlId);
            ViewData["GetEduProfileFullName"] = new SelectList(_context.EduProfiles.Include(v => v.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", vacant.EduProfileId);
            return View(vacant);
        }

        // POST: EduVacants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VacantId,NumberBFVacant,NumberBRVacant,NumberBMVacant,NumberPVacant,EduFormId,EduKursId,EduProfileId")] Vacant vacant)
        {
            if (id != vacant.VacantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var profile = await _context.EduProfiles.FirstOrDefaultAsync(p => p.EduProfileId == vacant.EduProfileId);
                    vacant.EduNapravlId = profile.EduNapravlId;
                    _context.Update(vacant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacantExists(vacant.VacantId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", vacant.EduFormId);
            ViewData["EduKursId"] = new SelectList(_context.EduKurses, "EduKursId", "EduKursId", vacant.EduKursId);
            ViewData["GetEduNapravlFullName"] = new SelectList(_context.EduNapravls.Include(v => v.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName", vacant.EduNapravlId);
            return View(vacant);
        }

        // GET: EduVacants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacant = await _context.Vacants
                .Include(v => v.EduForm)
                .Include(v => v.EduKurs)
                .Include(v => v.EduNapravl)
                .Include(v => v.EduNapravl.EduUgs.EduLevel)
                .Include(v => v.EduNapravl)
                .Include(v => v.EduNapravl)
                .SingleOrDefaultAsync(m => m.VacantId == id);
            if (vacant == null)
            {
                return NotFound();
            }

            return View(vacant);
        }

        // POST: EduVacants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacant = await _context.Vacants.SingleOrDefaultAsync(m => m.VacantId == id);
            _context.Vacants.Remove(vacant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacantExists(int id)
        {
            return _context.Vacants.Any(e => e.VacantId == id);
        }
    }
}
