using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers.Files
{
    [Authorize(Roles = "Администраторы")]
    public class UserDocumentsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IUserDocumentRepository _userDocumentRepository;

        public UserDocumentsController(AppIdentityDBContext context,
            IUserDocumentRepository userDocumentRepository)
        {
            _context = context;
            _userDocumentRepository = userDocumentRepository;
        }

        // GET: UserDocuments
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.UserDocuments.Include(u => u.AppUser).Include(u => u.FileDataType).Include(u => u.FileModel);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: UserDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDocument = await _context.UserDocuments
                .Include(u => u.AppUser)
                .Include(u => u.FileDataType)
                .Include(u => u.FileModel)
                .SingleOrDefaultAsync(m => m.UserDocumentId == id);
            if (userDocument == null)
            {
                return NotFound();
            }

            return View(userDocument);
        }

        // GET: UserDocuments/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["FileDataTypeId"] = new SelectList(_context.FileDataTypes, "FileDataTypeId", "FileDataTypeId");
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id");
            return View();
        }

        // POST: UserDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserDocumentId,AppUserId,FileModelId,FileDataTypeId")] UserDocument userDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userDocument.AppUserId);
            ViewData["FileDataTypeId"] = new SelectList(_context.FileDataTypes, "FileDataTypeId", "FileDataTypeId", userDocument.FileDataTypeId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", userDocument.FileModelId);
            return View(userDocument);
        }

        // GET: UserDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDocument = await _context.UserDocuments.SingleOrDefaultAsync(m => m.UserDocumentId == id);
            if (userDocument == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userDocument.AppUserId);
            ViewData["FileDataTypeId"] = new SelectList(_context.FileDataTypes, "FileDataTypeId", "FileDataTypeId", userDocument.FileDataTypeId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", userDocument.FileModelId);
            return View(userDocument);
        }

        // POST: UserDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserDocumentId,AppUserId,FileModelId,FileDataTypeId")] UserDocument userDocument)
        {
            if (id != userDocument.UserDocumentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDocumentExists(userDocument.UserDocumentId))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userDocument.AppUserId);
            ViewData["FileDataTypeId"] = new SelectList(_context.FileDataTypes, "FileDataTypeId", "FileDataTypeId", userDocument.FileDataTypeId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", userDocument.FileModelId);
            return View(userDocument);
        }

        // GET: UserDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDocument = await _context.UserDocuments
                .Include(u => u.AppUser)
                .Include(u => u.FileDataType)
                .Include(u => u.FileModel)
                .SingleOrDefaultAsync(m => m.UserDocumentId == id);
            if (userDocument == null)
            {
                return NotFound();
            }

            return View(userDocument);
        }

        // POST: UserDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userDocumentRepository.RemoveUserDocumentAsync(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool UserDocumentExists(int id)
        {
            return _context.UserDocuments.Any(e => e.UserDocumentId == id);
        }
    }
}
