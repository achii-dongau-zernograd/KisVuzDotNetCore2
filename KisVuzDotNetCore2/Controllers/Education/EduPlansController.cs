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
    public class EduPlansController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduPlansController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduPlans
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduPlans.Include(e => e.EduForm).Include(e => e.EduPlanPdf).Include(e => e.EduProfile).Include(e => e.EduProgramPodg).Include(e => e.EduSrok).Include(e => e.StructKaf);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlan = await _context.EduPlans
                .Include(e => e.EduForm)
                .Include(e => e.EduPlanPdf)
                .Include(e => e.EduProfile)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.EduSrok)
                .Include(e => e.StructKaf)
                .SingleOrDefaultAsync(m => m.EduPlanId == id);
            if (eduPlan == null)
            {
                return NotFound();
            }

            return View(eduPlan);
        }

        // GET: EduPlans/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId");
            ViewData["EduPlanPdfId"] = new SelectList(_context.Files, "Id", "Id");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId");
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgId");
            ViewData["EduSrokId"] = new SelectList(_context.EduSrok, "EduSrokId", "EduSrokId");
            ViewData["StructKafId"] = new SelectList(_context.StructKafs, "StructKafId", "StructKafId");
            return View();
        }

        // POST: EduPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduPlanId,EduProfileId,ProtokolNumber,ProtokolDate,UtverjdDate,StructKafId,EduProgramPodgId,EduFormId,EduSrokId,EduPlanPdfId")] EduPlan eduPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduPlan.EduFormId);
            ViewData["EduPlanPdfId"] = new SelectList(_context.Files, "Id", "Id", eduPlan.EduPlanPdfId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", eduPlan.EduProfileId);
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgId", eduPlan.EduProgramPodgId);
            ViewData["EduSrokId"] = new SelectList(_context.EduSrok, "EduSrokId", "EduSrokId", eduPlan.EduSrokId);
            ViewData["StructKafId"] = new SelectList(_context.StructKafs, "StructKafId", "StructKafId", eduPlan.StructKafId);
            return View(eduPlan);
        }

        // GET: EduPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlan = await _context.EduPlans.SingleOrDefaultAsync(m => m.EduPlanId == id);
            if (eduPlan == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduPlan.EduFormId);
            ViewData["EduPlanPdfId"] = new SelectList(_context.Files, "Id", "Id", eduPlan.EduPlanPdfId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", eduPlan.EduProfileId);
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgId", eduPlan.EduProgramPodgId);
            ViewData["EduSrokId"] = new SelectList(_context.EduSrok, "EduSrokId", "EduSrokId", eduPlan.EduSrokId);
            ViewData["StructKafId"] = new SelectList(_context.StructKafs, "StructKafId", "StructKafId", eduPlan.StructKafId);
            return View(eduPlan);
        }

        // POST: EduPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduPlanId,EduProfileId,ProtokolNumber,ProtokolDate,UtverjdDate,StructKafId,EduProgramPodgId,EduFormId,EduSrokId,EduPlanPdfId")] EduPlan eduPlan)
        {
            if (id != eduPlan.EduPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduPlanExists(eduPlan.EduPlanId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduPlan.EduFormId);
            ViewData["EduPlanPdfId"] = new SelectList(_context.Files, "Id", "Id", eduPlan.EduPlanPdfId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", eduPlan.EduProfileId);
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgId", eduPlan.EduProgramPodgId);
            ViewData["EduSrokId"] = new SelectList(_context.EduSrok, "EduSrokId", "EduSrokId", eduPlan.EduSrokId);
            ViewData["StructKafId"] = new SelectList(_context.StructKafs, "StructKafId", "StructKafId", eduPlan.StructKafId);
            return View(eduPlan);
        }

        // GET: EduPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlan = await _context.EduPlans
                .Include(e => e.EduForm)
                .Include(e => e.EduPlanPdf)
                .Include(e => e.EduProfile)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.EduSrok)
                .Include(e => e.StructKaf)
                .SingleOrDefaultAsync(m => m.EduPlanId == id);
            if (eduPlan == null)
            {
                return NotFound();
            }

            return View(eduPlan);
        }

        // POST: EduPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduPlan = await _context.EduPlans.SingleOrDefaultAsync(m => m.EduPlanId == id);
            _context.EduPlans.Remove(eduPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduPlanExists(int id)
        {
            return _context.EduPlans.Any(e => e.EduPlanId == id);
        }
    }
}
