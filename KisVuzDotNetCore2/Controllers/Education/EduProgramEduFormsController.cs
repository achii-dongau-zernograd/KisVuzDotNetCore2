using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class EduProgramEduFormsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        /// <summary>
        /// Возвращает упорядоченный запрос со связанными данными таблицы EduProgramEduForms
        /// </summary>
        IOrderedQueryable<EduProgramEduForm> GetEduProgramEduFormsDBTableContext
        {
            get
            {
               return _context.EduProgramEduForms
                        .Include(e => e.EduForm)
                        .Include(e => e.EduProgram.EduProgramPodg)
                        .Include(e => e.EduProgram.EduProfile.EduNapravl.EduUgs.EduLevel)
                        .OrderBy(e => e.EduProgram.GetEduProgramFullName);
            }
        }

        public EduProgramEduFormsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduProgramEduForms
        public async Task<IActionResult> Index()
        {            
            return View(await GetEduProgramEduFormsDBTableContext.ToListAsync());
        }

        // GET: EduProgramEduForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramEduForm = await GetEduProgramEduFormsDBTableContext
                .SingleOrDefaultAsync(m => m.EduProgramEduFormId == id);
            if (eduProgramEduForm == null)
            {
                return NotFound();
            }

            return View(eduProgramEduForm);
        }

        // GET: EduProgramEduForms/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms
                .Include(p=>p.EduProgramPodg)
                .Include(p=>p.EduProfile.EduNapravl.EduUgs.EduLevel),
                "EduProgramId",
                "GetEduProgramFullName");
            return View();
        }

        // POST: EduProgramEduForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduProgramEduFormId,EduProgramId,EduFormId")] EduProgramEduForm eduProgramEduForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduProgramEduForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduProgramEduForm.EduFormId);
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId", eduProgramEduForm.EduProgramId);
            return View(eduProgramEduForm);
        }

        // GET: EduProgramEduForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramEduForm = await _context.EduProgramEduForms.SingleOrDefaultAsync(m => m.EduProgramEduFormId == id);
            if (eduProgramEduForm == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduProgramEduForm.EduFormId);
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId", eduProgramEduForm.EduProgramId);
            return View(eduProgramEduForm);
        }

        // POST: EduProgramEduForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduProgramEduFormId,EduProgramId,EduFormId")] EduProgramEduForm eduProgramEduForm)
        {
            if (id != eduProgramEduForm.EduProgramEduFormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduProgramEduForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduProgramEduFormExists(eduProgramEduForm.EduProgramEduFormId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduProgramEduForm.EduFormId);
            ViewData["EduProgramId"] = new SelectList(_context.EduPrograms, "EduProgramId", "EduProgramId", eduProgramEduForm.EduProgramId);
            return View(eduProgramEduForm);
        }

        // GET: EduProgramEduForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgramEduForm = await _context.EduProgramEduForms
                .Include(e => e.EduForm)
                .Include(e => e.EduProgram)
                .SingleOrDefaultAsync(m => m.EduProgramEduFormId == id);
            if (eduProgramEduForm == null)
            {
                return NotFound();
            }

            return View(eduProgramEduForm);
        }

        // POST: EduProgramEduForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduProgramEduForm = await _context.EduProgramEduForms.SingleOrDefaultAsync(m => m.EduProgramEduFormId == id);
            _context.EduProgramEduForms.Remove(eduProgramEduForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduProgramEduFormExists(int id)
        {
            return _context.EduProgramEduForms.Any(e => e.EduProgramEduFormId == id);
        }
    }
}
