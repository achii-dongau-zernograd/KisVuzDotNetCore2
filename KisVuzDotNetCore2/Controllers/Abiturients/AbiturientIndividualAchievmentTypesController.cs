using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Abitur;

namespace KisVuzDotNetCore2.Controllers.Abiturients
{
    public class AbiturientIndividualAchievmentTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public AbiturientIndividualAchievmentTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: AbiturientIndividualAchievmentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AbiturientIndividualAchievmentTypes.ToListAsync());
        }

        // GET: AbiturientIndividualAchievmentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abiturientIndividualAchievmentType = await _context.AbiturientIndividualAchievmentTypes
                .SingleOrDefaultAsync(m => m.AbiturientIndividualAchievmentTypeId == id);
            if (abiturientIndividualAchievmentType == null)
            {
                return NotFound();
            }

            return View(abiturientIndividualAchievmentType);
        }

        // GET: AbiturientIndividualAchievmentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AbiturientIndividualAchievmentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AbiturientIndividualAchievmentTypeId,AbiturientIndividualAchievmentTypeName,Point")] AbiturientIndividualAchievmentType abiturientIndividualAchievmentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abiturientIndividualAchievmentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(abiturientIndividualAchievmentType);
        }

        // GET: AbiturientIndividualAchievmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abiturientIndividualAchievmentType = await _context.AbiturientIndividualAchievmentTypes.SingleOrDefaultAsync(m => m.AbiturientIndividualAchievmentTypeId == id);
            if (abiturientIndividualAchievmentType == null)
            {
                return NotFound();
            }
            return View(abiturientIndividualAchievmentType);
        }

        // POST: AbiturientIndividualAchievmentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AbiturientIndividualAchievmentTypeId,AbiturientIndividualAchievmentTypeName,Point")] AbiturientIndividualAchievmentType abiturientIndividualAchievmentType)
        {
            if (id != abiturientIndividualAchievmentType.AbiturientIndividualAchievmentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abiturientIndividualAchievmentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbiturientIndividualAchievmentTypeExists(abiturientIndividualAchievmentType.AbiturientIndividualAchievmentTypeId))
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
            return View(abiturientIndividualAchievmentType);
        }

        // GET: AbiturientIndividualAchievmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abiturientIndividualAchievmentType = await _context.AbiturientIndividualAchievmentTypes
                .SingleOrDefaultAsync(m => m.AbiturientIndividualAchievmentTypeId == id);
            if (abiturientIndividualAchievmentType == null)
            {
                return NotFound();
            }

            return View(abiturientIndividualAchievmentType);
        }

        // POST: AbiturientIndividualAchievmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abiturientIndividualAchievmentType = await _context.AbiturientIndividualAchievmentTypes.SingleOrDefaultAsync(m => m.AbiturientIndividualAchievmentTypeId == id);
            _context.AbiturientIndividualAchievmentTypes.Remove(abiturientIndividualAchievmentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbiturientIndividualAchievmentTypeExists(int id)
        {
            return _context.AbiturientIndividualAchievmentTypes.Any(e => e.AbiturientIndividualAchievmentTypeId == id);
        }
    }
}
