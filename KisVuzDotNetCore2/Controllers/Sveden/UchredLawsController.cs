using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Sveden;

namespace KisVuzDotNetCore2.Controllers.Sveden
{
    public class UchredLawsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public UchredLawsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: UchredLaws
        public async Task<IActionResult> Index()
        {
            return View(await _context.UchredLaw.ToListAsync());
        }

        // GET: UchredLaws/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UchredLaws/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UchredLawId,NameUchred,FullnameUchred,AddressUchred,TelUchred,mailUchred,WebsiteUchred")] UchredLaw uchredLaw)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uchredLaw);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uchredLaw);
        }

        // GET: UchredLaws/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchredLaw = await _context.UchredLaw.SingleOrDefaultAsync(m => m.UchredLawId == id);
            if (uchredLaw == null)
            {
                return NotFound();
            }
            return View(uchredLaw);
        }

        // POST: UchredLaws/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UchredLawId,NameUchred,FullnameUchred,AddressUchred,TelUchred,mailUchred,WebsiteUchred")] UchredLaw uchredLaw)
        {
            if (id != uchredLaw.UchredLawId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uchredLaw);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UchredLawExists(uchredLaw.UchredLawId))
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
            return View(uchredLaw);
        }

        // GET: UchredLaws/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uchredLaw = await _context.UchredLaw
                .SingleOrDefaultAsync(m => m.UchredLawId == id);
            if (uchredLaw == null)
            {
                return NotFound();
            }

            return View(uchredLaw);
        }

        // POST: UchredLaws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uchredLaw = await _context.UchredLaw.SingleOrDefaultAsync(m => m.UchredLawId == id);
            _context.UchredLaw.Remove(uchredLaw);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UchredLawExists(int id)
        {
            return _context.UchredLaw.Any(e => e.UchredLawId == id);
        }
    }
}
