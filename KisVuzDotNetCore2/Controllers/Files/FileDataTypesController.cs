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
    public class FileDataTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public FileDataTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: FileDataTypes
        public async Task<IActionResult> Index()
        {
            //var appIdentityDBContext = _context.FileDataTypes.Include(f => f.FileDataTypeGroup);
            //return View(await appIdentityDBContext.ToListAsync());
            var fileDataTypeGroups = await _context.FileDataTypeGroups.Include(g => g.FileDataTypes).ToListAsync();
            return View(fileDataTypeGroups);
        }

        // GET: FileDataTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileDataType = await _context.FileDataTypes
                .Include(f => f.FileDataTypeGroup)
                .SingleOrDefaultAsync(m => m.FileDataTypeId == id);
            if (fileDataType == null)
            {
                return NotFound();
            }

            return View(fileDataType);
        }

        // GET: FileDataTypes/Create
        public IActionResult Create()
        {
            ViewData["FileDataTypeGroups"] = new SelectList(_context.FileDataTypeGroups, "FileDataTypeGroupId", "FileDataTypeGroupName");
            return View();
        }

        // POST: FileDataTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FileDataType fileDataType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileDataType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FileDataTypeGroups"] = new SelectList(_context.FileDataTypeGroups, "FileDataTypeGroupId", "FileDataTypeGroupName", fileDataType.FileDataTypeGroupId);
            return View(fileDataType);
        }

        // GET: FileDataTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileDataType = await _context.FileDataTypes.SingleOrDefaultAsync(m => m.FileDataTypeId == id);
            if (fileDataType == null)
            {
                return NotFound();
            }
            ViewData["FileDataTypeGroups"] = new SelectList(_context.FileDataTypeGroups, "FileDataTypeGroupId", "FileDataTypeGroupName", fileDataType.FileDataTypeGroupId);
            return View(fileDataType);
        }

        // POST: FileDataTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FileDataType fileDataType)
        {
            if (id != fileDataType.FileDataTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileDataType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileDataTypeExists(fileDataType.FileDataTypeId))
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
            ViewData["FileDataTypeGroups"] = new SelectList(_context.FileDataTypeGroups, "FileDataTypeGroupId", "FileDataTypeGroupName", fileDataType.FileDataTypeGroupId);
            return View(fileDataType);
        }

        // GET: FileDataTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileDataType = await _context.FileDataTypes
                .Include(f => f.FileDataTypeGroup)
                .SingleOrDefaultAsync(m => m.FileDataTypeId == id);
            if (fileDataType == null)
            {
                return NotFound();
            }

            return View(fileDataType);
        }

        // POST: FileDataTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileDataType = await _context.FileDataTypes.SingleOrDefaultAsync(m => m.FileDataTypeId == id);
            _context.FileDataTypes.Remove(fileDataType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileDataTypeExists(int id)
        {
            return _context.FileDataTypes.Any(e => e.FileDataTypeId == id);
        }
    }
}
