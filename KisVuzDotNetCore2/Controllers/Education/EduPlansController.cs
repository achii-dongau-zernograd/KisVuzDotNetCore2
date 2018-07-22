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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using KisVuzDotNetCore2.Models.Files;

namespace KisVuzDotNetCore2.Controllers.Education
{
    public class EduPlansController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public EduPlansController(AppIdentityDBContext context,
        IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: EduPlans
        public async Task<IActionResult> Index()
        {
            var eduPlans = await _context.EduPlans
                .Include(e => e.EduForm)
                .Include(e => e.EduPlanPdf)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.EduSrok)
                .Include(e => e.StructKaf.StructSubvision)
                .Include(e => e.EduPlanEduYearBeginningTrainings)
                .Include(e => e.EduVidDeyatList)
                .Include(e => e.EduPlanEduYears)
                .ToListAsync();

            foreach (var eduPlan in eduPlans)
            {
                foreach (var eduYearBeginningTraining in eduPlan.EduPlanEduYearBeginningTrainings)
                {
                    eduYearBeginningTraining.EduYearBeginningTraining = await _context.EduYearBeginningTrainings.Where(y => y.EduYearBeginningTrainingId == eduYearBeginningTraining.EduYearBeginningTrainingId).SingleOrDefaultAsync();
                }

                foreach (var eduVidDeyat in eduPlan.EduVidDeyatList)
                {
                    eduVidDeyat.EduVidDeyat = await _context.EduVidDeyat.Where(v => v.EduVidDeyatId == eduVidDeyat.EduVidDeyatId).SingleOrDefaultAsync();
                }

                foreach (var eduYear in eduPlan.EduPlanEduYears)
                {
                    eduYear.EduYear = await _context.EduYears.Where(y => y.EduYearId == eduYear.EduYearId).SingleOrDefaultAsync();
                }
            }

            return View(eduPlans);
        }

        // GET: EduPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlan = await _context.EduPlans
                .Include(e => e.EduForm)
                .Include(e => e.EduPlanPdf)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.EduSrok)
                .Include(e => e.StructKaf.StructSubvision)
                .Include(e => e.EduVidDeyatList)
                .Include(e => e.EduPlanEduYearBeginningTrainings)
                .Include(e => e.EduPlanEduYears)
                .SingleOrDefaultAsync(m => m.EduPlanId == id);
            // Заполняем навигационное свойство EduVidDeyat списка видов деятельности
            foreach (var vidDeyat in eduPlan.EduVidDeyatList)
            {
                vidDeyat.EduVidDeyat = await _context.EduVidDeyat.Where(d => d.EduVidDeyatId == vidDeyat.EduVidDeyatId).FirstOrDefaultAsync();
            }

            if (eduPlan == null)
            {
                return NotFound();
            }

            foreach (var eduYearBeginningTraning in eduPlan.EduPlanEduYearBeginningTrainings)
            {
                eduYearBeginningTraning.EduYearBeginningTraining = await _context.EduYearBeginningTrainings.SingleOrDefaultAsync(y => y.EduYearBeginningTrainingId == eduYearBeginningTraning.EduYearBeginningTrainingId);
            }

            foreach (var eduYear in eduPlan.EduPlanEduYears)
            {
                eduYear.EduYear = await _context.EduYears.SingleOrDefaultAsync(y => y.EduYearId == eduYear.EduYearId);
            }

            return View(eduPlan);
        }

        // GET: EduPlans/Create
        public IActionResult Create()
        {
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName");
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName");
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgName");
            ViewData["EduSrokId"] = new SelectList(_context.EduSrok, "EduSrokId", "EduSrokName");
            ViewData["StructKafId"] = new SelectList(_context.StructKafs.Include(k => k.StructSubvision), "StructKafId", "StructSubvision.StructSubvisionName");

            List<EduVidDeyat> eduVidDeyats = _context.EduVidDeyat.ToList();
            ViewData["EduVidDeyats"] = eduVidDeyats;

            List<EduYearBeginningTraining> eduYearBeginningTrainings = _context.EduYearBeginningTrainings.ToList();
            ViewData["EduYearBeginningTrainings"] = eduYearBeginningTrainings;

            List<EduYear> eduYears = _context.EduYears.ToList();
            ViewData["EduYears"] = eduYears;

            return View();
        }

        // POST: EduPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EduPlan eduPlan, IFormFile uploadedFile, int[] EduVidDeyatIds, int[] EduYearBeginningTrainingIds, int[] EduPlanEduYearIds)
        {
            if (ModelState.IsValid && uploadedFile != null)
            {
                FileModel fileModel = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Учебный план", FileDataTypeEnum.UchebniyPlan);

                eduPlan.EduPlanPdfId = fileModel.Id;
                _context.EduPlans.Add(eduPlan);
                await _context.SaveChangesAsync();

                if (EduVidDeyatIds != null)
                {
                    var eduPlanEduVidDeyats = new List<EduPlanEduVidDeyat>();
                    foreach (var EduVidDeyatId in EduVidDeyatIds)
                    {
                        EduPlanEduVidDeyat eduPlanEduVidDeyat = new EduPlanEduVidDeyat();
                        eduPlanEduVidDeyat.EduPlanId = eduPlan.EduPlanId;
                        eduPlanEduVidDeyat.EduVidDeyatId = EduVidDeyatId;
                        eduPlanEduVidDeyats.Add(eduPlanEduVidDeyat);
                    }
                    await _context.EduPlanEduVidDeyats.AddRangeAsync(eduPlanEduVidDeyats);
                    await _context.SaveChangesAsync();
                }

                if (EduYearBeginningTrainingIds != null)
                {
                    var eduPlanEduYearBeginningTrainings = new List<EduPlanEduYearBeginningTraining>();
                    foreach (var EduYearBeginningTrainingId in EduYearBeginningTrainingIds)
                    {
                        EduPlanEduYearBeginningTraining eduPlanEduYearBeginningTraining = new EduPlanEduYearBeginningTraining();
                        eduPlanEduYearBeginningTraining.EduPlanId = eduPlan.EduPlanId;
                        eduPlanEduYearBeginningTraining.EduYearBeginningTrainingId = EduYearBeginningTrainingId;
                        eduPlanEduYearBeginningTrainings.Add(eduPlanEduYearBeginningTraining);
                    }
                    await _context.EduPlanEduYearBeginningTraining.AddRangeAsync(eduPlanEduYearBeginningTrainings);
                    await _context.SaveChangesAsync();
                }

                if (EduPlanEduYearIds != null)
                {
                    var eduPlanEduYears = new List<EduPlanEduYear>();
                    foreach (var EduPlanEduYearId in EduPlanEduYearIds)
                    {
                        EduPlanEduYear eduPlanEduYear = new EduPlanEduYear();
                        eduPlanEduYear.EduPlanId = eduPlan.EduPlanId;
                        eduPlanEduYear.EduYearId = EduPlanEduYearId;
                        eduPlanEduYears.Add(eduPlanEduYear);
                    }
                    await _context.EduPlanEduYears.AddRangeAsync(eduPlanEduYears);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction(nameof(Index));
            }
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", eduPlan.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", eduPlan.EduProfileId);
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgName", eduPlan.EduProgramPodgId);
            ViewData["EduSrokId"] = new SelectList(_context.EduSrok, "EduSrokId", "EduSrokName", eduPlan.EduSrokId);
            ViewData["StructKafId"] = new SelectList(_context.StructKafs.Include(k => k.StructSubvision), "StructKafId", "StructSubvision.StructSubvisionName", eduPlan.StructKafId);

            List<EduVidDeyat> eduVidDeyats = _context.EduVidDeyat.ToList();
            ViewData["EduVidDeyats"] = eduVidDeyats;

            List<EduYearBeginningTraining> eduYearBeginningTrainings = _context.EduYearBeginningTrainings.ToList();
            ViewData["EduYearBeginningTrainings"] = eduYearBeginningTrainings;

            List<EduYear> eduYears = _context.EduYears.ToList();
            ViewData["EduYears"] = eduYears;

            return View(eduPlan);
        }

        // GET: EduPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlan = await _context.EduPlans
                .Include(e => e.EduForm)
                .Include(p => p.EduPlanPdf)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.EduSrok)
                .Include(e => e.StructKaf.StructSubvision)
                .Include(e => e.EduPlanEduYearBeginningTrainings)
                .Include(e => e.EduVidDeyatList)
                .Include(e => e.EduPlanEduYears)
                .SingleOrDefaultAsync(m => m.EduPlanId == id);
            if (eduPlan == null)
            {
                return NotFound();
            }

            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormName", eduPlan.EduFormId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles.Include(p => p.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", eduPlan.EduProfileId);
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgName", eduPlan.EduProgramPodgId);
            ViewData["EduSrokId"] = new SelectList(_context.EduSrok, "EduSrokId", "EduSrokName", eduPlan.EduSrokId);
            ViewData["StructKafId"] = new SelectList(_context.StructKafs.Include(k => k.StructSubvision), "StructKafId", "StructSubvision.StructSubvisionName", eduPlan.StructKafId);

            List<EduVidDeyat> eduVidDeyats = _context.EduVidDeyat.ToList();
            ViewData["EduVidDeyats"] = eduVidDeyats;

            List<EduYearBeginningTraining> eduYearBeginningTrainings = _context.EduYearBeginningTrainings.ToList();
            ViewData["EduYearBeginningTrainings"] = eduYearBeginningTrainings;

            List<EduYear> eduYears = _context.EduYears.ToList();
            ViewData["EduYears"] = eduYears;

            return View(eduPlan);
        }

        // POST: EduPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EduPlan eduPlan, IFormFile uploadedFile, int[] EduVidDeyatIds, int[] EduYearBeginningTrainingIds, int[] EduPlanEduYearIds)
        {
            if (id != eduPlan.EduPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    FileModel fileModel = await Files.LoadFile(_context, _appEnvironment, uploadedFile, "Учебный план", FileDataTypeEnum.UchebniyPlan);
                    await _context.SaveChangesAsync();
                    int? fileToRemoveId = eduPlan.EduPlanPdfId;
                    eduPlan.EduPlanPdfId = fileModel.Id;
                    await _context.SaveChangesAsync();
                    Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
                }

                try
                {
                    _context.Update(eduPlan);
                    await _context.SaveChangesAsync();

                    if (EduVidDeyatIds != null)
                    {
                        _context.EduPlanEduVidDeyats.RemoveRange(_context.EduPlanEduVidDeyats.Where(v => v.EduPlanId == eduPlan.EduPlanId));
                        await _context.SaveChangesAsync();

                        var eduPlanEduVidDeyats = new List<EduPlanEduVidDeyat>();
                        foreach (var EduVidDeyatId in EduVidDeyatIds)
                        {
                            EduPlanEduVidDeyat eduPlanEduVidDeyat = new EduPlanEduVidDeyat();
                            eduPlanEduVidDeyat.EduPlanId = eduPlan.EduPlanId;
                            eduPlanEduVidDeyat.EduVidDeyatId = EduVidDeyatId;
                            eduPlanEduVidDeyats.Add(eduPlanEduVidDeyat);
                        }
                        await _context.EduPlanEduVidDeyats.AddRangeAsync(eduPlanEduVidDeyats);
                        await _context.SaveChangesAsync();
                    }

                    if (EduYearBeginningTrainingIds != null)
                    {
                        _context.EduPlanEduYearBeginningTraining.RemoveRange(_context.EduPlanEduYearBeginningTraining.Where(y => y.EduPlanId == eduPlan.EduPlanId));
                        await _context.SaveChangesAsync();

                        var eduPlanEduYearBeginningTrainings = new List<EduPlanEduYearBeginningTraining>();
                        foreach (var EduYearBeginningTrainingId in EduYearBeginningTrainingIds)
                        {
                            EduPlanEduYearBeginningTraining eduPlanEduYearBeginningTraining = new EduPlanEduYearBeginningTraining();
                            eduPlanEduYearBeginningTraining.EduPlanId = eduPlan.EduPlanId;
                            eduPlanEduYearBeginningTraining.EduYearBeginningTrainingId = EduYearBeginningTrainingId;
                            eduPlanEduYearBeginningTrainings.Add(eduPlanEduYearBeginningTraining);
                        }
                        await _context.EduPlanEduYearBeginningTraining.AddRangeAsync(eduPlanEduYearBeginningTrainings);
                        await _context.SaveChangesAsync();
                    }

                    if (EduPlanEduYearIds != null)
                    {
                        _context.EduPlanEduYears.RemoveRange(_context.EduPlanEduYears.Where(y => y.EduPlanId == eduPlan.EduPlanId));
                        await _context.SaveChangesAsync();

                        var eduPlanEduYears = new List<EduPlanEduYear>();
                        foreach (var EduPlanEduYearId in EduPlanEduYearIds)
                        {
                            EduPlanEduYear eduPlanEduYear = new EduPlanEduYear();
                            eduPlanEduYear.EduPlanId = eduPlan.EduPlanId;
                            eduPlanEduYear.EduYearId = EduPlanEduYearId;
                            eduPlanEduYears.Add(eduPlanEduYear);
                        }
                        await _context.EduPlanEduYears.AddRangeAsync(eduPlanEduYears);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EduPlanExists(eduPlan.EduPlanId))
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
            ViewData["EduFormId"] = new SelectList(_context.EduForms, "EduFormId", "EduFormId", eduPlan.EduFormId);
            ViewData["EduPlanPdfId"] = new SelectList(_context.Files, "Id", "Id", eduPlan.EduPlanPdfId);
            ViewData["EduProfileId"] = new SelectList(_context.EduProfiles, "EduProfileId", "EduProfileId", eduPlan.EduProfileId);
            ViewData["EduProgramPodgId"] = new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgId", eduPlan.EduProgramPodgId);
            ViewData["EduSrokId"] = new SelectList(_context.EduSrok, "EduSrokId", "EduSrokId", eduPlan.EduSrokId);
            ViewData["StructKafId"] = new SelectList(_context.StructKafs, "StructKafId", "StructKafId", eduPlan.StructKafId);

            return View(eduPlan);
        }

        // GET: EduPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eduPlan = await _context.EduPlans
                .Include(e => e.EduForm)
                .Include(e => e.EduPlanPdf)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.EduSrok)
                .Include(e => e.StructKaf.StructSubvision)
                .Include(e => e.EduVidDeyatList)
                .Include(e => e.EduPlanEduYearBeginningTrainings)
                .Include(e => e.EduPlanEduYears)
                .SingleOrDefaultAsync(m => m.EduPlanId == id);

            foreach (var vidDeyat in eduPlan.EduVidDeyatList)
            {
                vidDeyat.EduVidDeyat = await _context.EduVidDeyat.Where(d => d.EduVidDeyatId == vidDeyat.EduVidDeyatId).FirstOrDefaultAsync();
            }

            foreach (var eduYearBeginningTraning in eduPlan.EduPlanEduYearBeginningTrainings)
            {
                eduYearBeginningTraning.EduYearBeginningTraining = await _context.EduYearBeginningTrainings.SingleOrDefaultAsync(y => y.EduYearBeginningTrainingId == eduYearBeginningTraning.EduYearBeginningTrainingId);
            }

            foreach (var eduYear in eduPlan.EduPlanEduYears)
            {
                eduYear.EduYear = await _context.EduYears.SingleOrDefaultAsync(y => y.EduYearId == eduYear.EduYearId);
            }

            if (eduPlan == null)
            {
                return NotFound();
            }

            return View(eduPlan);

        }

        // POST: EduPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eduPlan = await _context.EduPlans.SingleOrDefaultAsync(m => m.EduPlanId == id);
            _context.EduPlans.Remove(eduPlan);

            Files.RemoveFile(_context, _appEnvironment, eduPlan?.EduPlanPdfId);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool EduPlanExists(int id)
        {
            return _context.EduPlans.Any(e => e.EduPlanId == id);
        }

        // Get: EduPlans/Preview/id
        public async Task<IActionResult> Preview(int id)
        {
            var eduPlan = await _context.EduPlans
                .Include(e => e.EduForm)
                .Include(e => e.EduPlanPdf)
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduProgramPodg)
                .Include(e => e.EduSrok)
                .Include(e => e.StructKaf.StructSubvision)
                .Include(e => e.EduVidDeyatList)
                    .ThenInclude(v => v.EduVidDeyat)
                .Include(e => e.EduPlanEduYearBeginningTrainings)
                    .ThenInclude(y => y.EduYearBeginningTraining)
                .Include(e => e.EduPlanEduYears)
                    .ThenInclude(y => y.EduYear)
                .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplName)
                .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.BlokDisciplChastName)
                .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.Kurses)
                                .ThenInclude(k => k.Semestres)
                                    .ThenInclude(s => s.SemestrName)
                .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.Kurses)
                                .ThenInclude(k => k.Semestres)
                                    .ThenInclude(s => s.SemestrVidUchebRaboti)
                                        .ThenInclude(u => u.VidUchebRabotiName)
                .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.Kurses)
                                .ThenInclude(k => k.EduKurs)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.Kurses)
                                .ThenInclude(k => k.Semestres)
                                    .ThenInclude(s => s.SemestrFormKontrolName)
                                        .ThenInclude(f => f.FormKontrolName)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d=>d.DisciplineName)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.EduAnnotations)
                                .ThenInclude(a => a.FileModel)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.RabPrograms)
                                .ThenInclude(a => a.FileModel)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.FondOcenochnihSredstvList)
                                .ThenInclude(a => a.FileModel)
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.TeacherDisciplines)
                                .ThenInclude(td => td.Teacher)
                                    .ThenInclude(t => t.AppUser)
                 .SingleOrDefaultAsync(e => e.EduPlanId == id);
                
            if(eduPlan == null)
            {
                return NotFound();
            }

            return View(eduPlan);

                 
        }
    }
}