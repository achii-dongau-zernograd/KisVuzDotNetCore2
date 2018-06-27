using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;

namespace KisVuzDotNetCore2.Controllers.Education
{
    public class VidUchebRabotisController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public VidUchebRabotisController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: VidUchebRabotis
        public async Task<IActionResult> Index()
        {
            return View(await _context.VidUchebRaboti.ToListAsync());
        }

        // GET: VidUchebRabotis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vidUchebRaboti = await _context.VidUchebRaboti
                .SingleOrDefaultAsync(m => m.VidUchebRabotiId == id);
            if (vidUchebRaboti == null)
            {
                return NotFound();
            }

            return View(vidUchebRaboti);
        }

        // GET: VidUchebRabotis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VidUchebRabotis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VidUchebRabotiId,VidUchebRabotiName")] VidUchebRaboti vidUchebRaboti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vidUchebRaboti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vidUchebRaboti);
        }

        // GET: VidUchebRabotis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vidUchebRaboti = await _context.VidUchebRaboti.SingleOrDefaultAsync(m => m.VidUchebRabotiId == id);
            if (vidUchebRaboti == null)
            {
                return NotFound();
            }
            return View(vidUchebRaboti);
        }

        // POST: VidUchebRabotis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VidUchebRabotiId,VidUchebRabotiName")] VidUchebRaboti vidUchebRaboti)
        {
            if (id != vidUchebRaboti.VidUchebRabotiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vidUchebRaboti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VidUchebRabotiExists(vidUchebRaboti.VidUchebRabotiId))
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
            return View(vidUchebRaboti);
        }

        // GET: VidUchebRabotis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vidUchebRaboti = await _context.VidUchebRaboti
                .SingleOrDefaultAsync(m => m.VidUchebRabotiId == id);
            if (vidUchebRaboti == null)
            {
                return NotFound();
            }

            return View(vidUchebRaboti);
        }

        // POST: VidUchebRabotis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vidUchebRaboti = await _context.VidUchebRaboti.SingleOrDefaultAsync(m => m.VidUchebRabotiId == id);
            _context.VidUchebRaboti.Remove(vidUchebRaboti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VidUchebRabotiExists(int id)
        {
            return _context.VidUchebRaboti.Any(e => e.VidUchebRabotiId == id);
        }
    }
}
