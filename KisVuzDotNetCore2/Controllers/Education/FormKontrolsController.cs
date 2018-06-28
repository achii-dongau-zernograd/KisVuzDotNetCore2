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
    public class FormKontrolsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public FormKontrolsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: FormKontrols
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormKontrol.ToListAsync());
        }
                
        // GET: FormKontrols/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormKontrols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormKontrolId,FormKontrolName")] FormKontrol formKontrol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formKontrol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formKontrol);
        }

        // GET: FormKontrols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formKontrol = await _context.FormKontrol.SingleOrDefaultAsync(m => m.FormKontrolId == id);
            if (formKontrol == null)
            {
                return NotFound();
            }
            return View(formKontrol);
        }

        // POST: FormKontrols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormKontrolId,FormKontrolName")] FormKontrol formKontrol)
        {
            if (id != formKontrol.FormKontrolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formKontrol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormKontrolExists(formKontrol.FormKontrolId))
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
            return View(formKontrol);
        }

        // GET: FormKontrols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formKontrol = await _context.FormKontrol
                .SingleOrDefaultAsync(m => m.FormKontrolId == id);
            if (formKontrol == null)
            {
                return NotFound();
            }

            return View(formKontrol);
        }

        // POST: FormKontrols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formKontrol = await _context.FormKontrol.SingleOrDefaultAsync(m => m.FormKontrolId == id);
            _context.FormKontrol.Remove(formKontrol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormKontrolExists(int id)
        {
            return _context.FormKontrol.Any(e => e.FormKontrolId == id);
        }
    }
}
