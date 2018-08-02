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
    public class FileToFileTypesController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public FileToFileTypesController(AppIdentityDBContext context)
        {
            _context = context;
        }

        // GET: FileToFileTypes
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.FileToFileTypes.Include(f => f.FileDataType).Include(f => f.FileModel);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: FileToFileTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileToFileType = await _context.FileToFileTypes
                .Include(f => f.FileDataType)
                .Include(f => f.FileModel)
                .SingleOrDefaultAsync(m => m.FileToFileTypeId == id);
            if (fileToFileType == null)
            {
                return NotFound();
            }

            return View(fileToFileType);
        }

        // GET: FileToFileTypes/Create
        public IActionResult Create()
        {
            ViewData["FileDataTypeId"] = new SelectList(_context.FileDataTypes, "FileDataTypeId", "FileDataTypeId");
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id");
            return View();
        }

        // POST: FileToFileTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FileToFileTypeId,FileModelId,FileDataTypeId")] FileToFileType fileToFileType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileToFileType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FileDataTypeId"] = new SelectList(_context.FileDataTypes, "FileDataTypeId", "FileDataTypeId", fileToFileType.FileDataTypeId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", fileToFileType.FileModelId);
            return View(fileToFileType);
        }

        // GET: FileToFileTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileToFileType = await _context.FileToFileTypes.SingleOrDefaultAsync(m => m.FileToFileTypeId == id);
            if (fileToFileType == null)
            {
                return NotFound();
            }
            ViewData["FileDataTypeId"] = new SelectList(_context.FileDataTypes, "FileDataTypeId", "FileDataTypeId", fileToFileType.FileDataTypeId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", fileToFileType.FileModelId);
            return View(fileToFileType);
        }

        // POST: FileToFileTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FileToFileTypeId,FileModelId,FileDataTypeId")] FileToFileType fileToFileType)
        {
            if (id != fileToFileType.FileToFileTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileToFileType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileToFileTypeExists(fileToFileType.FileToFileTypeId))
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
            ViewData["FileDataTypeId"] = new SelectList(_context.FileDataTypes, "FileDataTypeId", "FileDataTypeId", fileToFileType.FileDataTypeId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", fileToFileType.FileModelId);
            return View(fileToFileType);
        }

        // GET: FileToFileTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileToFileType = await _context.FileToFileTypes
                .Include(f => f.FileDataType)
                .Include(f => f.FileModel)
                .SingleOrDefaultAsync(m => m.FileToFileTypeId == id);
            if (fileToFileType == null)
            {
                return NotFound();
            }

            return View(fileToFileType);
        }

        // POST: FileToFileTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileToFileType = await _context.FileToFileTypes.SingleOrDefaultAsync(m => m.FileToFileTypeId == id);
            _context.FileToFileTypes.Remove(fileToFileType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileToFileTypeExists(int id)
        {
            return _context.FileToFileTypes.Any(e => e.FileToFileTypeId == id);
        }
    }
}
