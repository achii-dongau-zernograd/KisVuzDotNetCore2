using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Authorization;
using KisVuzDotNetCore2.Models.Files;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы")]
    public class StructSubvisionsController : Controller
    {
        private readonly AppIdentityDBContext _context;

        public StructSubvisionsController(AppIdentityDBContext context)
        {
            _context = context;
        }


        private async Task<FileDataType> GetFileDataTypePolojenOStructPodrazd()
        {
            // Получаем список положений о структурных подразделениях
            var fileDataType = await _context.FileDataTypes
                .Include(fdt => fdt.FileToFileTypes)
                    .ThenInclude(ftft => ftft.FileModel)
                .SingleOrDefaultAsync(fdt => fdt.FileDataTypeId == (int)FileDataTypeEnum.PolojenOStructPodrazd);
            return fileDataType;
        }
        

        // GET: StructSubvisions
        public async Task<IActionResult> Index()
        {
            ViewData["StructSubvisionTypes"] = await _context.StructSubvisionTypes.ToListAsync();
            var appIdentityDBContext = _context.StructSubvisions
                .Include(s => s.StructSubvisionAdress)
                .Include(s => s.StructSubvisionPostChief)
                .Include(s => s.StructSubvisionType)
                .Include(s => s.StructStandingOrder);
            return View(await appIdentityDBContext.ToListAsync());            
        }

        // GET: StructSubvisions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structSubvision = await _context.StructSubvisions
                .Include(s => s.StructSubvisionAdress)
                .Include(s => s.StructSubvisionPostChief)
                .Include(s => s.StructSubvisionType)
                .SingleOrDefaultAsync(m => m.StructSubvisionId == id);
            if (structSubvision == null)
            {
                return NotFound();
            }

            return View(structSubvision);
        }

        // GET: StructSubvisions/Create
        public async Task<IActionResult> Create()
        {
            ViewData["StructSubvisionAdressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress");
            ViewData["StructSubvisionPostChiefId"] = new SelectList(_context.Posts, "PostId", "PostName");
            ViewData["StructSubvisionTypes"] = new SelectList(_context.StructSubvisionTypes, "StructSubvisionTypeId", "StructSubvisionTypeName");            
            ViewData["files"] = new SelectList((await GetFileDataTypePolojenOStructPodrazd()).FileToFileTypes, "FileModelId", "FileModel.Name");
            return View();
        }

        // POST: StructSubvisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StructSubvision structSubvision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(structSubvision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StructSubvisionAdressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress", structSubvision.StructSubvisionAdressId);
            ViewData["StructSubvisionPostChiefId"] = new SelectList(_context.Posts, "PostId", "PostName", structSubvision.StructSubvisionPostChiefId);
            ViewData["StructSubvisionTypes"] = new SelectList(_context.StructSubvisionTypes, "StructSubvisionTypeId", "StructSubvisionTypeName", structSubvision.StructSubvisionTypeId);
            ViewData["files"] = new SelectList((await GetFileDataTypePolojenOStructPodrazd()).FileToFileTypes, "FileModelId", "FileModel.Name");
            return View(structSubvision);
        }

        // GET: StructSubvisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structSubvision = await _context.StructSubvisions.SingleOrDefaultAsync(m => m.StructSubvisionId == id);
            if (structSubvision == null)
            {
                return NotFound();
            }
            ViewData["StructSubvisionAdressId"] = new SelectList(_context.Addresses, "AddressId", "GetAddress", structSubvision.StructSubvisionAdressId);
            ViewData["StructSubvisionPostChiefId"] = new SelectList(_context.Posts, "PostId", "PostName", structSubvision.StructSubvisionPostChiefId);
            ViewData["StructSubvisionTypes"] = new SelectList(_context.StructSubvisionTypes, "StructSubvisionTypeId", "StructSubvisionTypeName", structSubvision.StructSubvisionTypeId);
            ViewData["files"] = new SelectList((await GetFileDataTypePolojenOStructPodrazd()).FileToFileTypes, "FileModelId", "FileModel.Name");
            return View(structSubvision);
        }

        // POST: StructSubvisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StructSubvision structSubvision)
        {
            if (id != structSubvision.StructSubvisionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(structSubvision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StructSubvisionExists(structSubvision.StructSubvisionId))
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
            ViewData["StructSubvisionAdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", structSubvision.StructSubvisionAdressId);
            ViewData["StructSubvisionPostChiefId"] = new SelectList(_context.Posts, "PostId", "PostId", structSubvision.StructSubvisionPostChiefId);
            ViewData["StructSubvisionTypes"] = new SelectList(_context.StructSubvisionTypes, "StructSubvisionTypeId", "StructSubvisionTypeId", structSubvision.StructSubvisionTypeId);
            ViewData["files"] = new SelectList((await GetFileDataTypePolojenOStructPodrazd()).FileToFileTypes, "FileModelId", "FileModel.Name");
            return View(structSubvision);
        }

        // GET: StructSubvisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var structSubvision = await _context.StructSubvisions
                .Include(s => s.StructSubvisionAdress)
                .Include(s => s.StructSubvisionPostChief)
                .Include(s => s.StructSubvisionType)
                .SingleOrDefaultAsync(m => m.StructSubvisionId == id);
            if (structSubvision == null)
            {
                return NotFound();
            }

            return View(structSubvision);
        }

        // POST: StructSubvisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var structSubvision = await _context.StructSubvisions.SingleOrDefaultAsync(m => m.StructSubvisionId == id);
            _context.StructSubvisions.Remove(structSubvision);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StructSubvisionExists(int id)
        {
            return _context.StructSubvisions.Any(e => e.StructSubvisionId == id);
        }
    }
}
