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
using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Hosting;

namespace KisVuzDotNetCore2.Controllers
{
    public class EduShedulesController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public EduShedulesController(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: EduShedules
        public async Task<IActionResult> Index()
        {
            var appIdentityDBContext = _context.EduShedules
                .Include(e => e.EduForm)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduYear)
                .Include(e => e.FileModel);
            return View(await appIdentityDBContext.ToListAsync());
        }

        // GET: EduShedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduShedule = await _context.EduShedules
                .Include(e => e.EduForm)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduYear)
                .Include(e => e.FileModel)
                .SingleOrDefaultAsync(m => m.EduSheduleId == id);
            if (eduShedule == null)
            {
                return NotFound();
            }

            return View(eduShedule);
        }

        // GET: EduShedules/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName");
            //ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id");
            return View();
        }

        // POST: EduShedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EduSheduleId,EduYearId,EduProfileId,EduFormId")] EduShedule eduShedule, IFormFile uploadedFile)
        {
            if (ModelState.IsValid && uploadedFile!=null)
            {
                FileModel fileModel = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Календарный учебный график", FileDataTypeEnum.KalendarniyUchebniyGraphik);
                eduShedule.FileModelId = fileModel.Id;
                _context.Add(eduShedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", eduShedule.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", eduShedule.EduProfileId);
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", eduShedule.EduYearId);
            //ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", eduShedule.FileModelId);
            return View(eduShedule);
        }

        // GET: EduShedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduShedule = await _context.EduShedules
                .Include(s=>s.FileModel)
                .SingleOrDefaultAsync(m => m.EduSheduleId == id);
            if (eduShedule == null)
            {
                return NotFound();
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", eduShedule.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p=>p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", eduShedule.EduProfileId);
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearName", eduShedule.EduYearId);
            //ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", eduShedule.FileModelId);
            return View(eduShedule);
        }

        // POST: EduShedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EduSheduleId,EduYearId,EduProfileId,EduFormId,FileModelId")] EduShedule eduShedule, IFormFile uploadedFile)
        {
            if (id != eduShedule.EduSheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    FileModel fileModel = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Календарный учебный график", FileDataTypeEnum.KalendarniyUchebniyGraphik);
                    await _context.SaveChangesAsync();
                    int? fileToRemoveId = eduShedule.FileModelId;
                    eduShedule.FileModelId = fileModel.Id;
                    await _context.SaveChangesAsync();
                    Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                }

                try
                {
                    _context.Update(eduShedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduSheduleExists(eduShedule.EduSheduleId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduShedule.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", eduShedule.EduProfileId);
            ViewData["EduYearId"] = new SelectList(_context.EduYears, "EduYearId", "EduYearId", eduShedule.EduYearId);
            ViewData["FileModelId"] = new SelectList(_context.Files, "Id", "Id", eduShedule.FileModelId);
            return View(eduShedule);
        }

        // GET: EduShedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduShedule = await _context.EduShedules
                .Include(e => e.EduForm)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduYear)
                .Include(e => e.FileModel)
                .SingleOrDefaultAsync(m => m.EduSheduleId == id);
            if (eduShedule == null)
            {
                return NotFound();
            }

            return View(eduShedule);
        }

        // POST: EduShedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduShedule = await _context.EduShedules.SingleOrDefaultAsync(m => m.EduSheduleId == id);
            _context.EduShedules.Remove(eduShedule);

            Files.RemoveFile(_context, _appEnvironment, eduShedule?.FileModelId);
                        
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EduSheduleExists(int id)
        {
            return _context.EduShedules.Any(e => e.EduSheduleId == id);
        }
    }
}
