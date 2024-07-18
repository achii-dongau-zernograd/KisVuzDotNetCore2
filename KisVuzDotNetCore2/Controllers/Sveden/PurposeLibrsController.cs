using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы, ЗамДиректораПоСоцРаботе, ЗамДиректораПоМолПолВоспИСоцРаботе, ЗамДиректораПоУРиЦТ")]
    public class PurposeLibrsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public PurposeLibrsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: PurposeLibrs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PurposeLibr.ToListAsync());
        }

        // GET: PurposeLibrs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurposeLibrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurposeLibrId,VidPom,Adress,Square,NumberPlaces,PrisposoblOvz,itemprop")] PurposeLibr purposeLibr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purposeLibr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purposeLibr);
        }

        // GET: PurposeLibrs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purposeLibr = await _context.PurposeLibr.SingleOrDefaultAsync(m => m.PurposeLibrId == id);
            if (purposeLibr == null)
            {
                return NotFound();
            }
            return View(purposeLibr);
        }

        // POST: PurposeLibrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurposeLibrId,VidPom,Adress,Square,NumberPlaces,PrisposoblOvz,itemprop")] PurposeLibr purposeLibr)
        {
            if (id != purposeLibr.PurposeLibrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purposeLibr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurposeLibrExists(purposeLibr.PurposeLibrId))
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
            return View(purposeLibr);
        }

        // GET: PurposeLibrs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purposeLibr = await _context.PurposeLibr
                .SingleOrDefaultAsync(m => m.PurposeLibrId == id);
            if (purposeLibr == null)
            {
                return NotFound();
            }

            return View(purposeLibr);
        }

        // POST: PurposeLibrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purposeLibr = await _context.PurposeLibr.SingleOrDefaultAsync(m => m.PurposeLibrId == id);
            _context.PurposeLibr.Remove(purposeLibr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurposeLibrExists(int id)
        {
            return _context.PurposeLibr.Any(e => e.PurposeLibrId == id);
        }
    }
}
