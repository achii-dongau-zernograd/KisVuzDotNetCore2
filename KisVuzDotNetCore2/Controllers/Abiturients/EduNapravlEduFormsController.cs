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
    public class EduNapravlEduFormsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduNapravlEduFormsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduNapravlEduForms
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduNapravlEduForms.Include(e => e.EduForm).Include(e => e.EduNapravl);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduNapravlEduForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravlEduForm = await _context.EduNapravlEduForms
                .Include(e => e.EduForm)
                .Include(e => e.EduNapravl)
                .SingleOrDefaultAsync(m => m.EduNapravlEduFormId == id);
            if (eduNapravlEduForm == null)
            {
                return NotFound();
            }

            return View(eduNapravlEduForm);
        }

        // GET: EduNapravlEduForms/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "GetEduNapravlName");
            return View();
        }

        // POST: EduNapravlEduForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduNapravlEduFormId,EduNapravlId,EduFormId")] EduNapravlEduForm eduNapravlEduForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduNapravlEduForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", eduNapravlEduForm.EduFormId);
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "GetEduNapravlName", eduNapravlEduForm.EduNapravlId);
            return View(eduNapravlEduForm);
        }

        // GET: EduNapravlEduForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravlEduForm = await _context.EduNapravlEduForms.SingleOrDefaultAsync(m => m.EduNapravlEduFormId == id);
            if (eduNapravlEduForm == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", eduNapravlEduForm.EduFormId);
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "GetEduNapravlName", eduNapravlEduForm.EduNapravlId);
            return View(eduNapravlEduForm);
        }

        // POST: EduNapravlEduForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduNapravlEduFormId,EduNapravlId,EduFormId")] EduNapravlEduForm eduNapravlEduForm)
        {
            if (id != eduNapravlEduForm.EduNapravlEduFormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduNapravlEduForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduNapravlEduFormExists(eduNapravlEduForm.EduNapravlEduFormId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", eduNapravlEduForm.EduFormId);
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "GetEduNapravlName", eduNapravlEduForm.EduNapravlId);
            return View(eduNapravlEduForm);
        }

        // GET: EduNapravlEduForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravlEduForm = await _context.EduNapravlEduForms
                .Include(e => e.EduForm)
                .Include(e => e.EduNapravl)
                .SingleOrDefaultAsync(m => m.EduNapravlEduFormId == id);
            if (eduNapravlEduForm == null)
            {
                return NotFound();
            }

            return View(eduNapravlEduForm);
        }

        // POST: EduNapravlEduForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduNapravlEduForm = await _context.EduNapravlEduForms.SingleOrDefaultAsync(m => m.EduNapravlEduFormId == id);
            _context.EduNapravlEduForms.Remove(eduNapravlEduForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduNapravlEduFormExists(int id)
        {
            return _context.EduNapravlEduForms.Any(e => e.EduNapravlEduFormId == id);
        }
    }
}
