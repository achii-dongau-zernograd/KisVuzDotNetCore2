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
    public class EduFormsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduFormsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduForms.ToListAsync());
        }


        // GET: EduForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduFormId,EduFormName")] EduForm eduForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduForm);
        }

        // GET: EduForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduForm = await _context.EduForms.SingleOrDefaultAsync(m => m.EduFormId == id);
            if (eduForm == null)
            {
                return NotFound();
            }
            return View(eduForm);
        }

        // POST: EduForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduFormId,EduFormName")] EduForm eduForm)
        {
            if (id != eduForm.EduFormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduFormExists(eduForm.EduFormId))
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
            return View(eduForm);
        }

        // GET: EduForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduForm = await _context.EduForms
                .SingleOrDefaultAsync(m => m.EduFormId == id);
            if (eduForm == null)
            {
                return NotFound();
            }

            return View(eduForm);
        }

        // POST: EduForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduForm = await _context.EduForms.SingleOrDefaultAsync(m => m.EduFormId == id);
            _context.EduForms.Remove(eduForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduFormExists(int id)
        {
            return _context.EduForms.Any(e => e.EduFormId == id);
        }
    }
}
