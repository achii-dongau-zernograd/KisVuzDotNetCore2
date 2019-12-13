using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    public class EduPlanRepository : IEduPlanRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IFileModelRepository _fileModelRepository;

        public EduPlanRepository(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Добавление нового или обновление существующего учебного плана
        /// </summary>
        /// <param name="eduPlan"></param>
        /// <param name="uploadedFile"></param>
        /// <param name="eduVidDeyatIds"></param>
        /// <param name="eduYearBeginningTrainingIds"></param>
        /// <param name="eduPlanEduYearIds"></param>
        /// <returns></returns>
        public async Task<EduPlan> CreateEduPlan(EduPlan eduPlan,
            IFormFile uploadedFile,
            int[] eduVidDeyatIds,
            int[] eduYearBeginningTrainingIds,
            int[] eduPlanEduYearIds)
        {            
            if (eduPlan.EduPlanId == 0)
            {
                _context.Add(eduPlan);
            }
            else
            {
                EduPlan eduPlanDbEntry = await GetEduPlanAsync(eduPlan.EduPlanId);
                eduPlanDbEntry.EduFormId = eduPlan.EduFormId;
                eduPlanDbEntry.EduSrokId = eduPlan.EduSrokId;
                eduPlanDbEntry.StructKafId = eduPlan.StructKafId;
                eduPlanDbEntry.ProtokolNumber = eduPlan.ProtokolNumber;
                eduPlanDbEntry.ProtokolDate = eduPlan.ProtokolDate;
                eduPlanDbEntry.UtverjdDate = eduPlan.UtverjdDate;
                _context.Update(eduPlanDbEntry);
                eduPlan = eduPlanDbEntry;
            }

            await _context.SaveChangesAsync();

            if (uploadedFile != null)
            {
                FileModel fileModel = await Files.Files.LoadFile(_context, _appEnvironment, uploadedFile, "Учебный план", FileDataTypeEnum.UchebniyPlan);
                await _context.SaveChangesAsync();
                int? fileToRemoveId = eduPlan.EduPlanPdfId;
                eduPlan.EduPlanPdfId = fileModel.Id;
                await _context.SaveChangesAsync();
                Files.Files.RemoveFile(_context, _appEnvironment, fileToRemoveId);
            }

            try
            {
                if (eduVidDeyatIds != null)
                {
                    _context.EduPlanEduVidDeyats.RemoveRange(_context.EduPlanEduVidDeyats.Where(v => v.EduPlanId == eduPlan.EduPlanId));
                    await _context.SaveChangesAsync();

                    var eduPlanEduVidDeyats = new List<EduPlanEduVidDeyat>();
                    foreach (var EduVidDeyatId in eduVidDeyatIds)
                    {
                        EduPlanEduVidDeyat eduPlanEduVidDeyat = new EduPlanEduVidDeyat();
                        eduPlanEduVidDeyat.EduPlanId = eduPlan.EduPlanId;
                        eduPlanEduVidDeyat.EduVidDeyatId = EduVidDeyatId;
                        eduPlanEduVidDeyats.Add(eduPlanEduVidDeyat);
                    }
                    await _context.EduPlanEduVidDeyats.AddRangeAsync(eduPlanEduVidDeyats);
                    await _context.SaveChangesAsync();
                }

                if (eduYearBeginningTrainingIds != null)
                {
                    _context.EduPlanEduYearBeginningTraining.RemoveRange(_context.EduPlanEduYearBeginningTraining.Where(y => y.EduPlanId == eduPlan.EduPlanId));
                    await _context.SaveChangesAsync();

                    var eduPlanEduYearBeginningTrainings = new List<EduPlanEduYearBeginningTraining>();
                    foreach (var EduYearBeginningTrainingId in eduYearBeginningTrainingIds)
                    {
                        EduPlanEduYearBeginningTraining eduPlanEduYearBeginningTraining = new EduPlanEduYearBeginningTraining();
                        eduPlanEduYearBeginningTraining.EduPlanId = eduPlan.EduPlanId;
                        eduPlanEduYearBeginningTraining.EduYearBeginningTrainingId = EduYearBeginningTrainingId;
                        eduPlanEduYearBeginningTrainings.Add(eduPlanEduYearBeginningTraining);
                    }
                    await _context.EduPlanEduYearBeginningTraining.AddRangeAsync(eduPlanEduYearBeginningTrainings);
                    await _context.SaveChangesAsync();
                }

                if (eduPlanEduYearIds != null)
                {
                    foreach (var eduYearIdByUser in eduPlanEduYearIds)
                    {
                        bool isNeedAdd = true;
                        foreach (var item in eduPlan.EduPlanEduYears)
                        {
                            if (item.EduYearId == eduYearIdByUser) isNeedAdd = false;
                        }

                        if(isNeedAdd)
                        {
                            eduPlan.EduPlanEduYears.Add(new EduPlanEduYear { EduYearId = eduYearIdByUser, EduPlanId = eduPlan.EduPlanId });
                            await _context.SaveChangesAsync();
                        }
                    }

                    var eduPlanEduYearEntriesToRemove = new List<EduPlanEduYear>();
                    foreach (var eduPlanEduYearEntry in eduPlan.EduPlanEduYears)
                    {
                        bool isNeedRemove = true;
                        foreach (var eduYearIdByUser in eduPlanEduYearIds)
                        {
                            if (eduYearIdByUser == eduPlanEduYearEntry.EduYearId) isNeedRemove = false;
                        }

                        if(isNeedRemove)
                        {
                            eduPlanEduYearEntriesToRemove.Add(eduPlanEduYearEntry);
                        }
                    }
                    foreach (var removingItem in eduPlanEduYearEntriesToRemove)
                    {
                        eduPlan.EduPlanEduYears.Remove(removingItem);
                    }
                    await _context.SaveChangesAsync();                    
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return eduPlan;
        }

        /// <summary>
        /// Создаёт структуру учебного плана
        /// </summary>
        /// <param name="eduPlan"></param>
        /// <returns></returns>
        public async Task CreateEduPlanStructureAsync(EduPlan eduPlan)
        {
            int eduLevelId = eduPlan.EduProfile.EduNapravl.EduUgs.EduLevelId;
            
            // Создаём список, содержащий блоки дисциплин
            if (eduPlan.BlokDiscipl == null)
            {
                eduPlan.BlokDiscipl = new List<BlokDiscipl>();
                await _context.SaveChangesAsync();
            }

            // Заполняем блоки дисциплин
            if (eduPlan.BlokDiscipl.Count == 0)
            {
                foreach (var blokDisciplName in _context.BlokDisciplName.OrderBy(b=>b.BlokDisciplNameName))
                {
                    BlokDiscipl blokDiscipl = new BlokDiscipl();
                    blokDiscipl.BlokDisciplNameId = blokDisciplName.BlokDisciplNameId;

                    bool isAddBlockDisc = false;
                    if (blokDiscipl.BlokDisciplNameId == 1) isAddBlockDisc = true;
                    if (blokDiscipl.BlokDisciplNameId == 2) isAddBlockDisc = true;
                    if (eduLevelId == 3 || eduLevelId == 4 || eduLevelId == 5)
                    {
                        if (blokDiscipl.BlokDisciplNameId == 3) isAddBlockDisc = true;
                        if (blokDiscipl.BlokDisciplNameId == 4) isAddBlockDisc = true;
                    }
                    else if(eduLevelId == 6)
                    {
                        if (blokDiscipl.BlokDisciplNameId == 9) isAddBlockDisc = true;
                        if (blokDiscipl.BlokDisciplNameId == 10) isAddBlockDisc = true;
                        if (blokDiscipl.BlokDisciplNameId == 11) isAddBlockDisc = true;
                    }

                    if (isAddBlockDisc)
                    {
                        eduPlan.BlokDiscipl.Add(blokDiscipl);
                    }
                }

                await _context.SaveChangesAsync();
            }

            foreach (var blokDiscipl in eduPlan.BlokDiscipl)
            {
                if(blokDiscipl.BlokDisciplChast==null)
                {
                    blokDiscipl.BlokDisciplChast = new List<BlokDisciplChast>();
                    await _context.SaveChangesAsync();
                }

                if(blokDiscipl.BlokDisciplChast.Count==0)
                {
                    foreach (var blokDisciplChastName in _context.BlokDisciplChastName)
                    {
                        BlokDisciplChast blokDisciplChast = new BlokDisciplChast();
                        blokDisciplChast.BlokDisciplId = blokDiscipl.BlokDisciplId;
                        blokDisciplChast.BlokDisciplChastNameId = blokDisciplChastName.BlokDisciplChastNameId;

                        blokDiscipl.BlokDisciplChast.Add(blokDisciplChast);
                    }
                    
                }
            }
            await _context.SaveChangesAsync();



            return;
        }

        /// <summary>
        /// Возвращает объект дисциплины из учебного плана
        /// </summary>
        /// <param name="eduPlan"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public Discipline GetDiscipline(EduPlan eduPlan, int disciplineId)
        {
            Discipline discipline=null;
            eduPlan.BlokDiscipl
                .ForEach(b => b.BlokDisciplChast
                    .ForEach(c => c.Disciplines
                        .ForEach(d => { if (d.DisciplineId == disciplineId) { discipline = d; } })));
            return discipline;
        }

        /// <summary>
        /// Возвращает учебный план со всеми связанными данными
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <returns></returns>
        public async Task<EduPlan> GetEduPlanAsync(int eduPlanId)
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
                            .ThenInclude(d => d.DisciplineName)
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
                 .Include(e => e.BlokDiscipl)
                    .ThenInclude(b => b.BlokDisciplChast)
                        .ThenInclude(c => c.Disciplines)
                            .ThenInclude(d => d.DisciplinePomeshenies)
                                .ThenInclude(dp => dp.Pomeshenie)
                                    .ThenInclude(p => p.Korpus)
                 .SingleOrDefaultAsync(e => e.EduPlanId == eduPlanId);

            return eduPlan;
        }

        /// <summary>
        /// Удаляет объект дисциплины
        /// </summary>
        /// <param name="discipline"></param>
        /// <returns></returns>
        public async Task RemoveDiscipline(Discipline discipline)
        {
            _context.Disciplines.Remove(discipline);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет аннотацию
        /// </summary>
        /// <param name="eduAnnotation"></param>
        /// <returns></returns>
        public async Task RemoveEduAnnotationAsync(EduAnnotation eduAnnotation)
        {
            _context.EduAnnotations.Remove(eduAnnotation);            
            await _fileModelRepository.RemoveFileAsync(eduAnnotation.FileModelId);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет рабочую программу
        /// </summary>
        /// <param name="rabProgram"></param>
        /// <returns></returns>
        public async Task RemoveRabProgramAsync(RabProgram rabProgram)
        {
            _context.RabPrograms.Remove(rabProgram);
            await _fileModelRepository.RemoveFileAsync(rabProgram.FileModelId);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет фонд оценочных средств
        /// </summary>
        /// <param name="fondOcenochnihSredstv"></param>
        /// <returns></returns>
        public async Task RemoveFondOcenochnihSredstvAsync(FondOcenochnihSredstv fondOcenochnihSredstv)
        {
            _context.FondOcenochnihSredstvs.Remove(fondOcenochnihSredstv);
            await _fileModelRepository.RemoveFileAsync(fondOcenochnihSredstv.FileModelId);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет учебный план
        /// </summary>
        /// <param name="eduPlan"></param>
        /// <returns></returns>
        public async Task RemoveEduPlanAsync(EduPlan eduPlan)
        {            
            if(eduPlan!=null)
            {
                await _fileModelRepository.RemoveFileAsync(eduPlan.EduPlanPdfId);
                _context.EduPlans.Remove(eduPlan);
                await _context.SaveChangesAsync();                
            }
        }

        /// <summary>
        /// Добавляет к аннотации загруженный файл
        /// </summary>
        /// <param name="eduAnnotation"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<EduAnnotation> UpdateEduAnnotationAsync(EduAnnotation eduAnnotation, IFormFile uploadedFile)
        {
            if (eduAnnotation == null || uploadedFile == null) return null;
            FileModel fileModel = await _fileModelRepository.UploadEduAnnotationAsync(uploadedFile);

            if(eduAnnotation.FileModelId!=0)
            {
                await _fileModelRepository.RemoveFileAsync(eduAnnotation.FileModelId);
            }

            eduAnnotation.FileModel = fileModel;
            eduAnnotation.FileModelId = fileModel.Id;

            if(eduAnnotation.EduAnnotationId==0)
            {
                await _context.EduAnnotations.AddAsync(eduAnnotation);
            }

            await _context.SaveChangesAsync();
            return eduAnnotation;
        }

        /// <summary>
        /// Добавляет к рабочей программе загруженный файл
        /// </summary>
        /// <param name="rabProgram"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<RabProgram> UpdateRabProgramAsync(RabProgram rabProgram, IFormFile uploadedFile)
        {
            if (rabProgram == null || uploadedFile == null) return null;
            FileModel fileModel = await _fileModelRepository.UploadRabProgramAsync(uploadedFile);

            if (rabProgram.FileModelId != 0)
            {
                await _fileModelRepository.RemoveFileAsync(rabProgram.FileModelId);
            }

            rabProgram.FileModel = fileModel;
            rabProgram.FileModelId = fileModel.Id;

            if (rabProgram.RabProgramId == 0)
            {
                await _context.RabPrograms.AddAsync(rabProgram);
            }

            await _context.SaveChangesAsync();
            return rabProgram;
        }

        /// <summary>
        /// Добавляет к фонду оценочных средств загруженный файл
        /// </summary>
        /// <param name="fondOcenochnihSredstv"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<FondOcenochnihSredstv> UpdateFondOcenochnihSredstvAsync(FondOcenochnihSredstv fondOcenochnihSredstv, IFormFile uploadedFile)
        {
            if (fondOcenochnihSredstv == null || uploadedFile == null) return null;
            FileModel fileModel = await _fileModelRepository.UploadFondOcenochnihSredstvAsync(uploadedFile);

            if (fondOcenochnihSredstv.FileModelId != 0)
            {
                await _fileModelRepository.RemoveFileAsync(fondOcenochnihSredstv.FileModelId);
            }

            fondOcenochnihSredstv.FileModel = fileModel;
            fondOcenochnihSredstv.FileModelId = fileModel.Id;

            if (fondOcenochnihSredstv.FondOcenochnihSredstvId == 0)
            {
                await _context.FondOcenochnihSredstvs.AddAsync(fondOcenochnihSredstv);
            }

            await _context.SaveChangesAsync();
            return fondOcenochnihSredstv;
        }

        /// <summary>
        /// Возвращает связку "Преподаватель - Дисциплина"
        /// </summary>
        /// <param name="discipline"></param>
        /// <param name="teacherDisciplineId"></param>
        /// <returns></returns>
        public TeacherDiscipline GetTeacherDisciplineByDisciplineAndTeacherDisciplineId(Discipline discipline, int? teacherDisciplineId)
        {
            if (discipline == null || teacherDisciplineId == null) return null;
            TeacherDiscipline teacherDiscipline = discipline.TeacherDisciplines.SingleOrDefault(td => td.TeacherDisciplineId == teacherDisciplineId);

            return teacherDiscipline;
        }

        /// <summary>
        /// Обновляет привязку "Преподаватель - Дисциплина"
        /// </summary>
        /// <param name="teacherDisciplineChanging"></param>
        /// <returns></returns>
        public async Task UpdateEduAnnotationAsync(TeacherDiscipline teacherDisciplineChanging)
        {
            _context.TeacherDisciplines.Update(teacherDisciplineChanging);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет привязку "Преподаватель - Дисциплина"
        /// </summary>
        /// <param name="teacherDiscipline"></param>
        /// <returns></returns>
        public async Task RemoveTeacherDisciplineAsync(TeacherDiscipline teacherDiscipline)
        {
            if (teacherDiscipline == null) return;
            _context.TeacherDisciplines.Remove(teacherDiscipline);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="discipline"></param>
        /// <param name="disciplinePomeshenieId"></param>
        /// <returns></returns>
        public DisciplinePomeshenie GetDisciplinePomeshenieByDisciplineAndDisciplinePomeshenieId(Discipline discipline, int? disciplinePomeshenieId)
        {
            if (discipline == null || disciplinePomeshenieId == null) return null;
            DisciplinePomeshenie disciplinePomeshenie = discipline.DisciplinePomeshenies.SingleOrDefault(dp => dp.DisciplinePomeshenieId == disciplinePomeshenieId);

            return disciplinePomeshenie;
        }

        /// <summary>
        /// Обновляет привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="disciplinePomeshenieChanging"></param>
        /// <returns></returns>
        public async Task UpdateDisciplinePomeshenieAsync(DisciplinePomeshenie disciplinePomeshenieChanging)
        {
            if (disciplinePomeshenieChanging == null) return;
            _context.DisciplinePomeshenies.Update(disciplinePomeshenieChanging);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="disciplinePomeshenie"></param>
        /// <returns></returns>
        public async Task RemoveDisciplinePomeshenieAsync(DisciplinePomeshenie disciplinePomeshenie)
        {
            if (disciplinePomeshenie == null) return;
            _context.DisciplinePomeshenies.Remove(disciplinePomeshenie);
            await _context.SaveChangesAsync();
        }
    }
}