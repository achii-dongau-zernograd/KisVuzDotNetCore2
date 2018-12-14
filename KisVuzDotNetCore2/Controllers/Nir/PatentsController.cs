using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Nir;

namespace KisVuzDotNetCore2.Controllers.Nir
{
    public class PatentsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public PatentsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: Patents
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.Patents.Include(p => p.FileModel).Include(p => p.PatentVid).Include(p => p.RowStatus).Include(p => p.Year);
            return View(await appIdentityDBContext.ToListAsync());
        }
        
        // GET: Patents/Create
        public IActionResult Create()
        {
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Name");
            ViewData["PatentVidId"] = new SelectList(_context.PatentVids, "PatentVidId", "PatentVidName");
            ViewData["RowStatusId"] = new SelectList(_context.RowStatuses, "RowStatusId", "RowStatusName");
            ViewData["YearId"] = new SelectList(_context.Years, "YearId", "YearName");
            return View();
        }

        // POST: Patents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatentId,PatentName,PatentNumber,YearId,IsZarubejn,PatentOwner,IsACHII,PatentVidId,FileModelId,RowStatusId")] Patent patent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", patent.FileModelId);
            ViewData["PatentVidId"] = new SelectList(_context.PatentVids, "PatentVidId", "PatentVidName", patent.PatentVidId);
            ViewData["RowStatusId"] = new SelectList(_context.RowStatuses, "RowStatusId", "RowStatusName", patent.RowStatusId);
            ViewData["YearId"] = new SelectList(_context.Years, "YearId", "YearId", patent.YearId);
            return View(patent);
        }

        // GET: Patents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patent = await _context.Patents.SingleOrDefaultAsync(m => m.PatentId == id);
            if (patent == null)
            {
                return NotFound();
            }
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", patent.FileModelId);
            ViewData["PatentVidId"] = new SelectList(_context.PatentVids, "PatentVidId", "PatentVidName", patent.PatentVidId);
            ViewData["RowStatusId"] = new SelectList(_context.RowStatuses, "RowStatusId", "RowStatusName", patent.RowStatusId);
            ViewData["YearId"] = new SelectList(_context.Years, "YearId", "YearName", patent.YearId);
            return View(patent);
        }

        // POST: Patents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatentId,PatentName,PatentNumber,YearId,IsZarubejn,PatentOwner,IsACHII,PatentVidId,FileModelId,RowStatusId")] Patent patent)
        {
            if (id != patent.PatentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatentExists(patent.PatentId))
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
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", patent.FileModelId);
            ViewData["PatentVidId"] = new SelectList(_context.PatentVids, "PatentVidId", "PatentVidName", patent.PatentVidId);
            ViewData["RowStatusId"] = new SelectList(_context.RowStatuses, "RowStatusId", "RowStatusName", patent.RowStatusId);
            ViewData["YearId"] = new SelectList(_context.Years, "YearId", "YearName", patent.YearId);
            return View(patent);
        }

        // GET: Patents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patent = await _context.Patents
                .Include(p => p.FileModel)
                .Include(p => p.PatentVid)
                .Include(p => p.RowStatus)
                .Include(p => p.Year)
                .SingleOrDefaultAsync(m => m.PatentId == id);
            if (patent == null)
            {
                return NotFound();
            }

            return View(patent);
        }

        // POST: Patents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patent = await _context.Patents.SingleOrDefaultAsync(m => m.PatentId == id);
            _context.Patents.Remove(patent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatentExists(int id)
        {
            return _context.Patents.Any(e => e.PatentId == id);
        }
    }
}
