using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Users;

namespace KisVuzDotNetCore2.Controllers
{
    public class ProfessionalRetrainingsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public ProfessionalRetrainingsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: ProfessionalRetrainings
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.ProfessionalRetrainings.Include(p => p.AppUser).Include(p => p.ProfessionalRetrainingFile);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: ProfessionalRetrainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professionalRetraining = await _context.ProfessionalRetrainings
                .Include(p => p.AppUser)
                .Include(p => p.ProfessionalRetrainingFile)
                .SingleOrDefaultAsync(m => m.ProfessionalRetrainingId == id);
            if (professionalRetraining == null)
            {
                return NotFound();
            }

            return View(professionalRetraining);
        }

        // GET: ProfessionalRetrainings/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProfessionalRetrainingFileId"] = new SelectList(_context.Files, "Id", "Id");
            return View();
        }

        // POST: ProfessionalRetrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessionalRetrainingId,ProfessionalRetrainingDiplomRegNumber,ProfessionalRetrainingDiplomNumber,ProfessionalRetrainingProgramName,ProfessionalRetrainingHours,ProfessionalRetrainingCity,ProfessionalRetrainingInstitition,ProfessionalRetrainingDateStart,ProfessionalRetrainingDateFinish,ProfessionalRetrainingDateIssue,ProfessionalRetrainingFileId,AppUserId")] ProfessionalRetraining professionalRetraining)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professionalRetraining);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", professionalRetraining.AppUserId);
            ViewData["ProfessionalRetrainingFileId"] = new SelectList(_context.Files, "Id", "Id", professionalRetraining.ProfessionalRetrainingFileId);
            return View(professionalRetraining);
        }

        // GET: ProfessionalRetrainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professionalRetraining = await _context.ProfessionalRetrainings.SingleOrDefaultAsync(m => m.ProfessionalRetrainingId == id);
            if (professionalRetraining == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", professionalRetraining.AppUserId);
            ViewData["ProfessionalRetrainingFileId"] = new SelectList(_context.Files, "Id", "Id", professionalRetraining.ProfessionalRetrainingFileId);
            return View(professionalRetraining);
        }

        // POST: ProfessionalRetrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessionalRetrainingId,ProfessionalRetrainingDiplomRegNumber,ProfessionalRetrainingDiplomNumber,ProfessionalRetrainingProgramName,ProfessionalRetrainingHours,ProfessionalRetrainingCity,ProfessionalRetrainingInstitition,ProfessionalRetrainingDateStart,ProfessionalRetrainingDateFinish,ProfessionalRetrainingDateIssue,ProfessionalRetrainingFileId,AppUserId")] ProfessionalRetraining professionalRetraining)
        {
            if (id != professionalRetraining.ProfessionalRetrainingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professionalRetraining);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessionalRetrainingExists(professionalRetraining.ProfessionalRetrainingId))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", professionalRetraining.AppUserId);
            ViewData["ProfessionalRetrainingFileId"] = new SelectList(_context.Files, "Id", "Id", professionalRetraining.ProfessionalRetrainingFileId);
            return View(professionalRetraining);
        }

        // GET: ProfessionalRetrainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professionalRetraining = await _context.ProfessionalRetrainings
                .Include(p => p.AppUser)
                .Include(p => p.ProfessionalRetrainingFile)
                .SingleOrDefaultAsync(m => m.ProfessionalRetrainingId == id);
            if (professionalRetraining == null)
            {
                return NotFound();
            }

            return View(professionalRetraining);
        }

        // POST: ProfessionalRetrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professionalRetraining = await _context.ProfessionalRetrainings.SingleOrDefaultAsync(m => m.ProfessionalRetrainingId == id);
            _context.ProfessionalRetrainings.Remove(professionalRetraining);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessionalRetrainingExists(int id)
        {
            return _context.ProfessionalRetrainings.Any(e => e.ProfessionalRetrainingId == id);
        }
    }
}
