using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Common;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Common
{
    [Authorize(Roles = "Администраторы, Отдел кадров")]
    public class EmploymentFormsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EmploymentFormsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EmploymentForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmploymentForms.ToListAsync());
        }
               

        // GET: EmploymentForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmploymentForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmploymentFormId,EmploymentFormName")] EmploymentForm employmentForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employmentForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employmentForm);
        }

        // GET: EmploymentForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentForm = await _context.EmploymentForms.SingleOrDefaultAsync(m => m.EmploymentFormId == id);
            if (employmentForm == null)
            {
                return NotFound();
            }
            return View(employmentForm);
        }

        // POST: EmploymentForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmploymentFormId,EmploymentFormName")] EmploymentForm employmentForm)
        {
            if (id != employmentForm.EmploymentFormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employmentForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentFormExists(employmentForm.EmploymentFormId))
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
            return View(employmentForm);
        }

        // GET: EmploymentForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentForm = await _context.EmploymentForms
                .SingleOrDefaultAsync(m => m.EmploymentFormId == id);
            if (employmentForm == null)
            {
                return NotFound();
            }

            return View(employmentForm);
        }

        // POST: EmploymentForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employmentForm = await _context.EmploymentForms.SingleOrDefaultAsync(m => m.EmploymentFormId == id);
            _context.EmploymentForms.Remove(employmentForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmploymentFormExists(int id)
        {
            return _context.EmploymentForms.Any(e => e.EmploymentFormId == id);
        }
    }
}
