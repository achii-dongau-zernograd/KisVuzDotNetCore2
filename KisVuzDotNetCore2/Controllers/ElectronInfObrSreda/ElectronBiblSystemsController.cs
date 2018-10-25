using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using KisVuzDotNetCore2.Models.Files;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы, Библиотека")]
    public class ElectronBiblSystemsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public ElectronBiblSystemsController(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        // GET: ElectronBiblSystems
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.ElectronBiblSystem.Include(e => e.CopyDogovor);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: ElectronBiblSystems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronBiblSystem = await _context.ElectronBiblSystem
                .Include(e => e.CopyDogovor)
                .SingleOrDefaultAsync(m => m.ElectronBiblSystemId == id);
            if (electronBiblSystem == null)
            {
                return NotFound();
            }

            return View(electronBiblSystem);
        }

        // GET: ElectronBiblSystems/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: ElectronBiblSystems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElectronBiblSystemId,NameEbs,LinkEbs,NumberDogovor,DateStart,DateEnd,CopyDogovorId")] ElectronBiblSystem electronBiblSystem,
            IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if(uploadedFile!=null)
                {
                    var fileModel = await _fileModelRepository.UploadElectronBiblSystemDogovorAsync(uploadedFile);
                    if(fileModel!=null)
                    {
                        electronBiblSystem.CopyDogovorId = fileModel.Id;
                    }
                }
                _context.Add(electronBiblSystem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(electronBiblSystem);
        }

        // GET: ElectronBiblSystems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronBiblSystem = await _context.ElectronBiblSystem
                .Include(m => m.CopyDogovor)
                .SingleOrDefaultAsync(m => m.ElectronBiblSystemId == id);
            if (electronBiblSystem == null)
            {
                return NotFound();
            }
            
            return View(electronBiblSystem);
        }

        // POST: ElectronBiblSystems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ElectronBiblSystemId,NameEbs,LinkEbs,NumberDogovor,DateStart,DateEnd,CopyDogovorId")] ElectronBiblSystem electronBiblSystem,
            IFormFile uploadedFile)
        {
            if (id != electronBiblSystem.ElectronBiblSystemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(uploadedFile!=null)
                    {
                        int? fileModelIdToRemove = electronBiblSystem.CopyDogovorId;
                        var fileModel = await _fileModelRepository.UploadElectronBiblSystemDogovorAsync(uploadedFile);                        
                        electronBiblSystem.CopyDogovorId = fileModel.Id;
                        await _fileModelRepository.RemoveFileAsync(fileModelIdToRemove);
                    }
                    _context.Update(electronBiblSystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectronBiblSystemExists(electronBiblSystem.ElectronBiblSystemId))
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
            
            return View(electronBiblSystem);
        }

        // GET: ElectronBiblSystems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronBiblSystem = await _context.ElectronBiblSystem
                .Include(e => e.CopyDogovor)
                .SingleOrDefaultAsync(m => m.ElectronBiblSystemId == id);
            if (electronBiblSystem == null)
            {
                return NotFound();
            }

            return View(electronBiblSystem);
        }

        // POST: ElectronBiblSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electronBiblSystem = await _context.ElectronBiblSystem.SingleOrDefaultAsync(m => m.ElectronBiblSystemId == id);
            _context.ElectronBiblSystem.Remove(electronBiblSystem);            
            await _fileModelRepository.RemoveFileAsync(electronBiblSystem.CopyDogovorId);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Preview()
        {
            var appIdentityDBContext = _context.ElectronBiblSystem.Include(e => e.CopyDogovor);
            return View(await appIdentityDBContext.ToListAsync());
        }


        private bool ElectronBiblSystemExists(int id)
        {
            return _context.ElectronBiblSystem.Any(e => e.ElectronBiblSystemId == id);
        }
    }
}
