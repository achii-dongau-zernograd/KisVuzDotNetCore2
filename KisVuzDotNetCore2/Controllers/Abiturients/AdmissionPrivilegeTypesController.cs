using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Abitur;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Abiturients
{
    [Authorize(Roles = "Администраторы, Приёмная комиссия, Приёмная комиссия (консультанты)")]
    public class AdmissionPrivilegeTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public AdmissionPrivilegeTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: AdmissionPrivilegeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdmissionPrivilegeTypes.ToListAsync());
        }

        // GET: AdmissionPrivilegeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admissionPrivilegeType = await _context.AdmissionPrivilegeTypes
                .SingleOrDefaultAsync(m => m.AdmissionPrivilegeTypeId == id);
            if (admissionPrivilegeType == null)
            {
                return NotFound();
            }

            return View(admissionPrivilegeType);
        }

        // GET: AdmissionPrivilegeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdmissionPrivilegeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdmissionPrivilegeTypeId,AdmissionPrivilegeTypeName")] AdmissionPrivilegeType admissionPrivilegeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admissionPrivilegeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admissionPrivilegeType);
        }

        // GET: AdmissionPrivilegeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admissionPrivilegeType = await _context.AdmissionPrivilegeTypes.SingleOrDefaultAsync(m => m.AdmissionPrivilegeTypeId == id);
            if (admissionPrivilegeType == null)
            {
                return NotFound();
            }
            return View(admissionPrivilegeType);
        }

        // POST: AdmissionPrivilegeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdmissionPrivilegeTypeId,AdmissionPrivilegeTypeName")] AdmissionPrivilegeType admissionPrivilegeType)
        {
            if (id != admissionPrivilegeType.AdmissionPrivilegeTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admissionPrivilegeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionPrivilegeTypeExists(admissionPrivilegeType.AdmissionPrivilegeTypeId))
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
            return View(admissionPrivilegeType);
        }

        // GET: AdmissionPrivilegeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admissionPrivilegeType = await _context.AdmissionPrivilegeTypes
                .SingleOrDefaultAsync(m => m.AdmissionPrivilegeTypeId == id);
            if (admissionPrivilegeType == null)
            {
                return NotFound();
            }

            return View(admissionPrivilegeType);
        }

        // POST: AdmissionPrivilegeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admissionPrivilegeType = await _context.AdmissionPrivilegeTypes.SingleOrDefaultAsync(m => m.AdmissionPrivilegeTypeId == id);
            _context.AdmissionPrivilegeTypes.Remove(admissionPrivilegeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionPrivilegeTypeExists(int id)
        {
            return _context.AdmissionPrivilegeTypes.Any(e => e.AdmissionPrivilegeTypeId == id);
        }
    }
}
