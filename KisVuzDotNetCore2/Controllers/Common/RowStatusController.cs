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
    [Authorize(Roles = "Администраторы")]
    public class RowStatusController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public RowStatusController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: RowStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.RowStatuses.ToListAsync());
        }

        // GET: RowStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rowStatus = await _context.RowStatuses
                .SingleOrDefaultAsync(m => m.RowStatusId == id);
            if (rowStatus == null)
            {
                return NotFound();
            }

            return View(rowStatus);
        }

        // GET: RowStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RowStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RowStatusId,RowStatusName")] RowStatus rowStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rowStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rowStatus);
        }

        // GET: RowStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rowStatus = await _context.RowStatuses.SingleOrDefaultAsync(m => m.RowStatusId == id);
            if (rowStatus == null)
            {
                return NotFound();
            }
            return View(rowStatus);
        }

        // POST: RowStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RowStatusId,RowStatusName")] RowStatus rowStatus)
        {
            if (id != rowStatus.RowStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rowStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RowStatusExists(rowStatus.RowStatusId))
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
            return View(rowStatus);
        }

        // GET: RowStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rowStatus = await _context.RowStatuses
                .SingleOrDefaultAsync(m => m.RowStatusId == id);
            if (rowStatus == null)
            {
                return NotFound();
            }

            return View(rowStatus);
        }

        // POST: RowStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rowStatus = await _context.RowStatuses.SingleOrDefaultAsync(m => m.RowStatusId == id);
            _context.RowStatuses.Remove(rowStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RowStatusExists(int id)
        {
            return _context.RowStatuses.Any(e => e.RowStatusId == id);
        }
    }
}
