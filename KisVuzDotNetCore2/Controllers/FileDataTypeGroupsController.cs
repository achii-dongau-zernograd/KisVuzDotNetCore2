using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;

namespace KisVuzDotNetCore2.Controllers
{
    public class FileDataTypeGroupsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public FileDataTypeGroupsController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: FileDataTypeGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.FileDataTypeGroups.ToListAsync());
        }

        // GET: FileDataTypeGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileDataTypeGroup = await _context.FileDataTypeGroups
                .SingleOrDefaultAsync(m => m.FileDataTypeGroupId == id);
            if (fileDataTypeGroup == null)
            {
                return NotFound();
            }

            return View(fileDataTypeGroup);
        }

        // GET: FileDataTypeGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileDataTypeGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FileDataTypeGroupId,FileDataTypeGroupName")] FileDataTypeGroup fileDataTypeGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileDataTypeGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fileDataTypeGroup);
        }

        // GET: FileDataTypeGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileDataTypeGroup = await _context.FileDataTypeGroups.SingleOrDefaultAsync(m => m.FileDataTypeGroupId == id);
            if (fileDataTypeGroup == null)
            {
                return NotFound();
            }
            return View(fileDataTypeGroup);
        }

        // POST: FileDataTypeGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FileDataTypeGroupId,FileDataTypeGroupName")] FileDataTypeGroup fileDataTypeGroup)
        {
            if (id != fileDataTypeGroup.FileDataTypeGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileDataTypeGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileDataTypeGroupExists(fileDataTypeGroup.FileDataTypeGroupId))
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
            return View(fileDataTypeGroup);
        }

        // GET: FileDataTypeGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileDataTypeGroup = await _context.FileDataTypeGroups
                .SingleOrDefaultAsync(m => m.FileDataTypeGroupId == id);
            if (fileDataTypeGroup == null)
            {
                return NotFound();
            }

            return View(fileDataTypeGroup);
        }

        // POST: FileDataTypeGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileDataTypeGroup = await _context.FileDataTypeGroups.SingleOrDefaultAsync(m => m.FileDataTypeGroupId == id);
            _context.FileDataTypeGroups.Remove(fileDataTypeGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileDataTypeGroupExists(int id)
        {
            return _context.FileDataTypeGroups.Any(e => e.FileDataTypeGroupId == id);
        }
    }
}
