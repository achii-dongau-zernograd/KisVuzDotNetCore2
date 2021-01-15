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
using Microsoft.AspNetCore.Authorization;

namespace KisVuzDotNetCore2.Controllers
{
    [Authorize(Roles = "Администраторы, Учебная часть")]
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
            var eduPrograms = _context.EduPrograms
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.FileModel)
                .Include(e => e.EduProgramEduForms)
                .Include(e => e.EduProgramEduYears);
            foreach (var eduProgram in eduPrograms)
            {
                foreach (var eduProgramEduForm in eduProgram.EduProgramEduForms)
                {
                    eduProgramEduForm.EduForm = await _context.EduForms.SingleOrDefaultAsync(f=>f.EduFormId== eduProgramEduForm.EduFormId);
                }
                foreach (var eduProgramEduYear in eduProgram.EduProgramEduYears)
                {
                    eduProgramEduYear.EduYear = await _context.EduYears.SingleOrDefaultAsync(f => f.EduYearId == eduProgramEduYear.EduYearId);
                }
            }
            return View(await eduPrograms.ToListAsync());
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
                .Include(e => e.EduProgramEduForms)
                .Include(e => e.EduProgramEduYears)
                .SingleOrDefaultAsync(m => m.EduProgramId == id);
            if (eduProgram == null)
            {
                return NotFound();
            }

            foreach (var eduProgramEduForm in eduProgram.EduProgramEduForms)
            {
                eduProgramEduForm.EduForm = await _context.EduForms.SingleOrDefaultAsync(f => f.EduFormId == eduProgramEduForm.EduFormId);
            }
            foreach (var eduProgramEduYear in eduProgram.EduProgramEduYears)
            {
                eduProgramEduYear.EduYear = await _context.EduYears.SingleOrDefaultAsync(f => f.EduYearId == eduProgramEduYear.EduYearId);
            }

            return View(eduProgram);
        }

        // GET: EduPrograms/Create
        public IActionResult Create()
        {
            ViewBag.EduForms = _context.EduForms;
            ViewBag.EduYears = _context.EduYears;

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
        public async Task<IActionResult> Create([Bind("EduProgramId,EduProfileId,EduProgramPodgId,IsAdopt,UsingElAndDistEduTech,DateUtverjd")] EduProgram eduProgram,
            IFormFile uploadedFile,
            int[] eduFormIds,
            int[] eduYearIds)
        {
            if (ModelState.IsValid && uploadedFile!=null)
            {
                FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, "Образовательная программа", FileDataTypeEnum.OPOP);
                eduProgram.FileModelId = fileModel.Id;
                _context.Add(eduProgram);
                await _context.SaveChangesAsync();

                if(eduFormIds!=null)
                {
                    var eduProgramEduFormList = new List<EduProgramEduForm>();
                    foreach (int eduFormId in eduFormIds)
                    {
                        EduProgramEduForm eduProgramEduForm = new EduProgramEduForm
                        {
                            EduProgramId = eduProgram.EduProgramId,
                            EduFormId = eduFormId
                        };
                        eduProgramEduFormList.Add(eduProgramEduForm);
                    }
                    await _context.EduProgramEduForms.AddRangeAsync(eduProgramEduFormList);
                    await _context.SaveChangesAsync();
                }

                if (eduYearIds != null)
                {
                    var eduProgramEduYearList = new List<EduProgramEduYear>();
                    foreach (int eduYearId in eduYearIds)
                    {
                        EduProgramEduYear eduProgramEduYear = new EduProgramEduYear
                        {
                            EduProgramId = eduProgram.EduProgramId,
                            EduYearId = eduYearId
                        };
                        eduProgramEduYearList.Add(eduProgramEduYear);
                    }
                    await _context.EduProgramEduYears.AddRangeAsync(eduProgramEduYearList);
                    await _context.SaveChangesAsync();
                }

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

            var eduProgram = await _context.EduPrograms
                .Include(p=>p.FileModel)
                .Include(p=>p.EduProgramEduForms)
                .Include(p=>p.EduProgramEduYears)
                .SingleOrDefaultAsync(m => m.EduProgramId == id);
            if (eduProgram == null)
            {
                return NotFound();
            }
            ViewBag.EduForms = _context.EduForms;
            ViewBag.EduYears = _context.EduYears;
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
        public async Task<IActionResult> Edit(int id, [Bind("EduProgramId,EduProfileId,EduProgramPodgId,IsAdopt,UsingElAndDistEduTech,FileModelId,DateUtverjd")] EduProgram eduProgram,
            IFormFile uploadedFile,
            int[] eduFormIds,
            int[] eduYearIds)
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
                        FileModel fileModel = await KisVuzDotNetCore2.Models.Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, "Образовательная программа", FileDataTypeEnum.OPOP);
                        await _context.SaveChangesAsync();
                        int? fileToRemoveId = eduProgram.FileModelId;
                        eduProgram.FileModelId = fileModel.Id;
                        await _context.SaveChangesAsync();
                        KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                    }

                    _context.Update(eduProgram);
                    await _context.SaveChangesAsync();

                    if (eduFormIds != null)
                    {
                        _context.EduProgramEduForms.RemoveRange(_context.EduProgramEduForms.Where(f=>f.EduProgramId == eduProgram.EduProgramId));
                        await _context.SaveChangesAsync();

                        var eduProgramEduFormList = new List<EduProgramEduForm>();
                        foreach (int eduFormId in eduFormIds)
                        {
                            EduProgramEduForm eduProgramEduForm = new EduProgramEduForm
                            {
                                EduProgramId = eduProgram.EduProgramId,
                                EduFormId = eduFormId
                            };
                            eduProgramEduFormList.Add(eduProgramEduForm);
                        }
                        await _context.EduProgramEduForms.AddRangeAsync(eduProgramEduFormList);
                        await _context.SaveChangesAsync();
                    }

                    if (eduYearIds != null)
                    {
                        _context.EduProgramEduYears.RemoveRange(_context.EduProgramEduYears.Where(f => f.EduProgramId == eduProgram.EduProgramId));
                        await _context.SaveChangesAsync();

                        var eduProgramEduYearList = new List<EduProgramEduYear>();
                        foreach (int eduYearId in eduYearIds)
                        {
                            EduProgramEduYear eduProgramEduYear = new EduProgramEduYear
                            {
                                EduProgramId = eduProgram.EduProgramId,
                                EduYearId = eduYearId
                            };
                            eduProgramEduYearList.Add(eduProgramEduYear);
                        }
                        await _context.EduProgramEduYears.AddRangeAsync(eduProgramEduYearList);
                        await _context.SaveChangesAsync();
                    }
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
                .Include(e => e.EduProgramEduForms)
                .Include(e => e.EduProgramEduYears)
                .SingleOrDefaultAsync(m => m.EduProgramId == id);
            if (eduProgram == null)
            {
                return NotFound();
            }

            foreach (var eduProgramEduForm in eduProgram.EduProgramEduForms)
            {
                eduProgramEduForm.EduForm = await _context.EduForms.SingleOrDefaultAsync(f => f.EduFormId == eduProgramEduForm.EduFormId);
            }

            foreach (var eduProgramEduYear in eduProgram.EduProgramEduYears)
            {
                eduProgramEduYear.EduYear = await _context.EduYears.SingleOrDefaultAsync(f => f.EduYearId == eduProgramEduYear.EduYearId);
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
            KisVuzDotNetCore2.Models.Files.Files.RemoveFile(_context, _appEnvironment, eduProgram?.FileModelId);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduProgramExists(int id)
        {
            return _context.EduPrograms.Any(e => e.EduProgramId == id);
        }
    }
}
