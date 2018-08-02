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

namespace KisVuzDotNetCore2.Controllers.Education
{
    [Authorize(Roles = "Администраторы")]
    public class EduSroksController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduSroksController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduSroks
        public async Task<IActionResult> Index()
        {
            return View(await _context.EduSrok.ToListAsync());
        }

        // GET: EduSroks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EduSroks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduSrokId,EduSrokName")] EduSrok eduSrok)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduSrok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eduSrok);
        }

        // GET: EduSroks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduSrok = await _context.EduSrok.SingleOrDefaultAsync(m => m.EduSrokId == id);
            if (eduSrok == null)
            {
                return NotFound();
            }
            return View(eduSrok);
        }

        // POST: EduSroks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduSrokId,EduSrokName")] EduSrok eduSrok)
        {
            if (id != eduSrok.EduSrokId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduSrok);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduSrokExists(eduSrok.EduSrokId))
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
            return View(eduSrok);
        }

        // GET: EduSroks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduSrok = await _context.EduSrok
                .SingleOrDefaultAsync(m => m.EduSrokId == id);
            if (eduSrok == null)
            {
                return NotFound();
            }

            return View(eduSrok);
        }

        // POST: EduSroks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduSrok = await _context.EduSrok.SingleOrDefaultAsync(m => m.EduSrokId == id);
            _context.EduSrok.Remove(eduSrok);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduSrokExists(int id)
        {
            return _context.EduSrok.Any(e => e.EduSrokId == id);
        }
    }
}
