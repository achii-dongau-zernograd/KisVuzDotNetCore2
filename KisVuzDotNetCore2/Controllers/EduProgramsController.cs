using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using KisVuzDotNetCore2.Models.Files;

namespace KisVuzDotNetCore2.Controllers
{
    public class EduProgramsController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public EduProgramsController(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: EduPrograms
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduPrograms
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.FileModel);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgram = await _context.EduPrograms
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.FileModel)
                .SingleOrDefaultAsync(m => m.EduProgramId == id);
            if (eduProgram == null)
            {
                return NotFound();
            }

            return View(eduProgram);
        }

        // GET: EduPrograms/Create
        public IActionResult Create()
        {
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgName");
            //ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id");
            return View();
        }

        // POST: EduPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduProgramId,EduProfileId,EduProgramPodgId,IsAdopt,UsingElAndDistEduTech,DateUtverjd")] EduProgram eduProgram, IFormFile uploadedFile)
        {
            if (ModelState.IsValid && uploadedFile!=null)
            {
                FileModel fileModel = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Образовательная программа", FileDataTypeEnum.OPOP);
                eduProgram.FileModelId = fileModel.Id;
                _context.Add(eduProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", eduProgram.EduProfileId);
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgName", eduProgram.EduProgramPodgId);
            //ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", eduProgram.FileModelId);
            return View(eduProgram);
        }

        // GET: EduPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgram = await _context.EduPrograms.Include(p=>p.FileModel).SingleOrDefaultAsync(m => m.EduProgramId == id);
            if (eduProgram == null)
            {
                return NotFound();
            }
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", eduProgram.EduProfileId);
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgName", eduProgram.EduProgramPodgId);
            //ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", eduProgram.FileModelId);
            return View(eduProgram);
        }

        // POST: EduPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduProgramId,EduProfileId,EduProgramPodgId,IsAdopt,UsingElAndDistEduTech,FileModelId,DateUtverjd")] EduProgram eduProgram, IFormFile uploadedFile)
        {
            if (id != eduProgram.EduProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                        FileModel fileModel = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Образовательная программа", FileDataTypeEnum.OPOP);
                        await _context.SaveChangesAsync();
                        int? fileToRemoveId = eduProgram.FileModelId;
                        eduProgram.FileModelId = fileModel.Id;
                        await _context.SaveChangesAsync();
                        Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                    }

                    _context.Update(eduProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduProgramExists(eduProgram.EduProgramId))
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
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", eduProgram.EduProfileId);
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgName", eduProgram.EduProgramPodgId);
            //ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", eduProgram.FileModelId);
            return View(eduProgram);
        }

        // GET: EduPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduProgram = await _context.EduPrograms
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.FileModel)
                .SingleOrDefaultAsync(m => m.EduProgramId == id);
            if (eduProgram == null)
            {
                return NotFound();
            }

            return View(eduProgram);
        }

        // POST: EduPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduProgram = await _context.EduPrograms.SingleOrDefaultAsync(m => m.EduProgramId == id);
            _context.EduPrograms.Remove(eduProgram);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduProgramExists(int id)
        {
            return _context.EduPrograms.Any(e => e.EduProgramId == id);
        }
    }
}
