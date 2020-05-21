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
    public class EduNapravlQuotaTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public EduNapravlQuotaTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: EduNapravlQuotaTypes
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduNapravlQuotaTypes
                .Include(e => e.EduNapravl)
                .Include(e => e.QuotaType)
                .OrderBy(e => e.EduNapravl.GetEduNapravlName);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduNapravlQuotaTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravlQuotaType = await _context.EduNapravlQuotaTypes
                .Include(e => e.EduNapravl)
                .Include(e => e.QuotaType)
                .SingleOrDefaultAsync(m => m.EduNapravlQuotaTypeId == id);
            if (eduNapravlQuotaType == null)
            {
                return NotFound();
            }

            return View(eduNapravlQuotaType);
        }

        // GET: EduNapravlQuotaTypes/Create
        public IActionResult Create()
        {
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "GetEduNapravlName");
            ViewData["QuotaTypeId"] = new SelectList(_context.QuotaTypes, "QuotaTypeId", "QuotaTypeName");
            return View();
        }

        // POST: EduNapravlQuotaTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduNapravlQuotaTypeId,EduNapravlId,QuotaTypeId")] EduNapravlQuotaType eduNapravlQuotaType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eduNapravlQuotaType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "GetEduNapravlName", eduNapravlQuotaType.EduNapravlId);
            ViewData["QuotaTypeId"] = new SelectList(_context.QuotaTypes, "QuotaTypeId", "QuotaTypeName", eduNapravlQuotaType.QuotaTypeId);
            return View(eduNapravlQuotaType);
        }

        // GET: EduNapravlQuotaTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravlQuotaType = await _context.EduNapravlQuotaTypes.SingleOrDefaultAsync(m => m.EduNapravlQuotaTypeId == id);
            if (eduNapravlQuotaType == null)
            {
                return NotFound();
            }
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "GetEduNapravlName", eduNapravlQuotaType.EduNapravlId);
            ViewData["QuotaTypeId"] = new SelectList(_context.QuotaTypes, "QuotaTypeId", "QuotaTypeName", eduNapravlQuotaType.QuotaTypeId);
            return View(eduNapravlQuotaType);
        }

        // POST: EduNapravlQuotaTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduNapravlQuotaTypeId,EduNapravlId,QuotaTypeId")] EduNapravlQuotaType eduNapravlQuotaType)
        {
            if (id != eduNapravlQuotaType.EduNapravlQuotaTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eduNapravlQuotaType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduNapravlQuotaTypeExists(eduNapravlQuotaType.EduNapravlQuotaTypeId))
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
            ViewData["EduNapravlId"] = new SelectList(_context.EduNapravls, "EduNapravlId", "GetEduNapravlName", eduNapravlQuotaType.EduNapravlId);
            ViewData["QuotaTypeId"] = new SelectList(_context.QuotaTypes, "QuotaTypeId", "QuotaTypeName", eduNapravlQuotaType.QuotaTypeId);
            return View(eduNapravlQuotaType);
        }

        // GET: EduNapravlQuotaTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduNapravlQuotaType = await _context.EduNapravlQuotaTypes
                .Include(e => e.EduNapravl)
                .Include(e => e.QuotaType)
                .SingleOrDefaultAsync(m => m.EduNapravlQuotaTypeId == id);
            if (eduNapravlQuotaType == null)
            {
                return NotFound();
            }

            return View(eduNapravlQuotaType);
        }

        // POST: EduNapravlQuotaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduNapravlQuotaType = await _context.EduNapravlQuotaTypes.SingleOrDefaultAsync(m => m.EduNapravlQuotaTypeId == id);
            _context.EduNapravlQuotaTypes.Remove(eduNapravlQuotaType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduNapravlQuotaTypeExists(int id)
        {
            return _context.EduNapravlQuotaTypes.Any(e => e.EduNapravlQuotaTypeId == id);
        }
    }
}
