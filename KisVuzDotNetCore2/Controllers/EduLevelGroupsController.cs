using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;

namespace KisVuzDotNetCore2.Controllers
{
    public class EduLevelGroupsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduLevelGroupsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduLevelGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduLevelGroups.ToListAsync());
        }

        // GET: EduLevelGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduLevelGroup = await _context.EduLevelGroups
                .SingleOrDefaultAsync(m => m.EduLevelGroupId == id);
            if (eduLevelGroup == null)
            {
                return NotFound();
            }

            return View(eduLevelGroup);
        }

        // GET: EduLevelGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduLevelGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduLevelGroupId,EduLevelGroupName")] EduLevelGroup eduLevelGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduLevelGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduLevelGroup);
        }

        // GET: EduLevelGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduLevelGroup = await _context.EduLevelGroups.SingleOrDefaultAsync(m => m.EduLevelGroupId == id);
            if (eduLevelGroup == null)
            {
                return NotFound();
            }
            return View(eduLevelGroup);
        }

        // POST: EduLevelGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduLevelGroupId,EduLevelGroupName")] EduLevelGroup eduLevelGroup)
        {
            if (id != eduLevelGroup.EduLevelGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduLevelGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduLevelGroupExists(eduLevelGroup.EduLevelGroupId))
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
            return View(eduLevelGroup);
        }

        // GET: EduLevelGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduLevelGroup = await _context.EduLevelGroups
                .SingleOrDefaultAsync(m => m.EduLevelGroupId == id);
            if (eduLevelGroup == null)
            {
                return NotFound();
            }

            return View(eduLevelGroup);
        }

        // POST: EduLevelGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduLevelGroup = await _context.EduLevelGroups.SingleOrDefaultAsync(m => m.EduLevelGroupId == id);
            _context.EduLevelGroups.Remove(eduLevelGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduLevelGroupExists(int id)
        {
            return _context.EduLevelGroups.Any(e => e.EduLevelGroupId == id);
        }
    }
}
