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
using KisVuzDotNetCore2.Infrastructure;

namespace KisVuzDotNetCore2.Controllers.Struct
{
    [Authorize(Roles = "Администраторы")]
    public class MetodKomissiyaEduProfilesController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly ISelectListRepository _selectListRepository;

        public MetodKomissiyaEduProfilesController(AppIdentityDBContext context,
            ISelectListRepository selectListRepository)
        {
            _context = context;
            _selectListRepository = selectListRepository;
        }

        // GET: MetodKomissiyaEduProfiles
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.MetodKomissiyaEduProfiles
                .Include(m => m.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(m => m.MetodKomissiya)
                .OrderBy(m => m.MetodKomissiya.MetodKomissiyaName);
            return View(await appIdentityDBContext.ToListAsync());
        }
                

        // GET: MetodKomissiyaEduProfiles/Create
        public IActionResult Create()
        {
            ViewData["EduProfileId"] = _selectListRepository.GetSelectListEduProfileFullNames();
            ViewData["MetodKomissiyaId"] = _selectListRepository.GetSelectListMetodKomissiyaNames();
            return View();
        }

        // POST: MetodKomissiyaEduProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetodKomissiyaEduProfileId,MetodKomissiyaId,EduProfileId")] MetodKomissiyaEduProfile metodKomissiyaEduProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodKomissiyaEduProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EduProfileId"] = _selectListRepository.GetSelectListEduProfileFullNames(metodKomissiyaEduProfile.EduProfileId);
            ViewData["MetodKomissiyaId"] = _selectListRepository.GetSelectListMetodKomissiyaNames(metodKomissiyaEduProfile.MetodKomissiyaId);
                        
            return View(metodKomissiyaEduProfile);
        }

        // GET: MetodKomissiyaEduProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodKomissiyaEduProfile = await _context.MetodKomissiyaEduProfiles.SingleOrDefaultAsync(m => m.MetodKomissiyaEduProfileId == id);
            if (metodKomissiyaEduProfile == null)
            {
                return NotFound();
            }
            ViewData["EduProfileId"] = _selectListRepository.GetSelectListEduProfileFullNames(metodKomissiyaEduProfile.EduProfileId);
            ViewData["MetodKomissiyaId"] = _selectListRepository.GetSelectListMetodKomissiyaNames(metodKomissiyaEduProfile.MetodKomissiyaId);
            return View(metodKomissiyaEduProfile);
        }

        // POST: MetodKomissiyaEduProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetodKomissiyaEduProfileId,MetodKomissiyaId,EduProfileId")] MetodKomissiyaEduProfile metodKomissiyaEduProfile)
        {
            if (id != metodKomissiyaEduProfile.MetodKomissiyaEduProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metodKomissiyaEduProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodKomissiyaEduProfileExists(metodKomissiyaEduProfile.MetodKomissiyaEduProfileId))
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
            ViewData["EduProfileId"] = _selectListRepository.GetSelectListEduProfileFullNames(metodKomissiyaEduProfile.EduProfileId);
            ViewData["MetodKomissiyaId"] = _selectListRepository.GetSelectListMetodKomissiyaNames(metodKomissiyaEduProfile.MetodKomissiyaId);
            return View(metodKomissiyaEduProfile);
        }

        // GET: MetodKomissiyaEduProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodKomissiyaEduProfile = await _context.MetodKomissiyaEduProfiles
                .Include(m => m.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(m => m.MetodKomissiya)
                .SingleOrDefaultAsync(m => m.MetodKomissiyaEduProfileId == id);
            if (metodKomissiyaEduProfile == null)
            {
                return NotFound();
            }

            return View(metodKomissiyaEduProfile);
        }

        // POST: MetodKomissiyaEduProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metodKomissiyaEduProfile = await _context.MetodKomissiyaEduProfiles.SingleOrDefaultAsync(m => m.MetodKomissiyaEduProfileId == id);
            _context.MetodKomissiyaEduProfiles.Remove(metodKomissiyaEduProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetodKomissiyaEduProfileExists(int id)
        {
            return _context.MetodKomissiyaEduProfiles.Any(e => e.MetodKomissiyaEduProfileId == id);
        }
    }
}
