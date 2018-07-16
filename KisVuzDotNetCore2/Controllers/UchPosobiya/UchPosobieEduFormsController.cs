using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.UchPosobiya;

namespace KisVuzDotNetCore2.Controllers.UchPosobiya
{
    public class UchPosobieEduFormsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UchPosobieEduFormsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UchPosobieEduForms
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.UchPosobieEduForm.Include(u => u.EduForm).Include(u => u.UchPosobieName);
            return View(await appIdentityDBContext.ToListAsync());
        }
                
        // GET: UchPosobieEduForms/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName");
            return View();
        }

        // POST: UchPosobieEduForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UchPosobieEduFormId,EduFormId,UchPosobieId")] UchPosobieEduForm uchPosobieEduForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uchPosobieEduForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", uchPosobieEduForm.EduFormId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName", uchPosobieEduForm.UchPosobieId);
            return View(uchPosobieEduForm);
        }

        // GET: UchPosobieEduForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieEduForm = await _context.UchPosobieEduForm.SingleOrDefaultAsync(m => m.UchPosobieEduFormId == id);
            if (uchPosobieEduForm == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", uchPosobieEduForm.EduFormId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName", uchPosobieEduForm.UchPosobieId);
            return View(uchPosobieEduForm);
        }

        // POST: UchPosobieEduForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UchPosobieEduFormId,EduFormId,UchPosobieId")] UchPosobieEduForm uchPosobieEduForm)
        {
            if (id != uchPosobieEduForm.UchPosobieEduFormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uchPosobieEduForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UchPosobieEduFormExists(uchPosobieEduForm.UchPosobieEduFormId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", uchPosobieEduForm.EduFormId);
            ViewData["UchPosobieId"] = new SelectList(_context.UchPosobie, "UchPosobieId", "UchPosobieName", uchPosobieEduForm.UchPosobieId);
            return View(uchPosobieEduForm);
        }

        // GET: UchPosobieEduForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchPosobieEduForm = await _context.UchPosobieEduForm
                .Include(u => u.EduForm)
                .Include(u => u.UchPosobieName)
                .SingleOrDefaultAsync(m => m.UchPosobieEduFormId == id);
            if (uchPosobieEduForm == null)
            {
                return NotFound();
            }

            return View(uchPosobieEduForm);
        }

        // POST: UchPosobieEduForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uchPosobieEduForm = await _context.UchPosobieEduForm.SingleOrDefaultAsync(m => m.UchPosobieEduFormId == id);
            _context.UchPosobieEduForm.Remove(uchPosobieEduForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UchPosobieEduFormExists(int id)
        {
            return _context.UchPosobieEduForm.Any(e => e.UchPosobieEduFormId == id);
        }
    }
}
