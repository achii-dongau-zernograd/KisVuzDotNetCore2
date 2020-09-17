using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Репозиторий методкомиссий
    /// </summary>
    public class MetodKomissiyaRepository : IMetodKomissiyaRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IEduPlanRepository _eduPlanRepository;

        public MetodKomissiyaRepository(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment,
            IEduPlanRepository eduPlanRepository)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _eduPlanRepository = eduPlanRepository;
        }

        /// <summary>
        /// Возвращает образовательную программу по УИД,
        /// если она доступна пользователю
        /// </summary>
        /// <param name="eduProgramId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<EduProgram> GetEduProgramByUserNameAsync(int eduProgramId, string userName)
        {
            var eduPrograms = await GetEduProgramsByUserNameAsync(userName);
            if (eduPrograms.Count() == 0) return null;

            var eduProgram = eduPrograms.SingleOrDefault(p => p.EduProgramId == eduProgramId);
            return eduProgram;
        }

        /// <summary>
        /// Возвращает заполненный объект пользователя по переданному имени
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AppUser> GetAppUserAsync(string userName)
        {
            // Поиск аккаунта пользователя
            var appUser = await _context.Users
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                                        .ThenInclude(ep => ep.EduProgramPodg)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                                        .ThenInclude(ep => ep.EduProgramEduForms)
                                            .ThenInclude(epef=>epef.EduForm)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                                        .ThenInclude(ep => ep.EduProgramEduYears)
                                            .ThenInclude(epey => epey.EduYear)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                                        .ThenInclude(ep => ep.EduProgramEduYearBeginningTrainings)
                                            .ThenInclude(epey => epey.EduYearBeginningTraining)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                                        .ThenInclude(ep => ep.FileModel)                                            
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPlans)
                                        .ThenInclude( plan => plan.EduForm)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPlans)
                                        .ThenInclude(plan => plan.EduPlanEduYears)
                                            .ThenInclude(py => py.EduYear)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPlans)
                                        .ThenInclude(plan => plan.EduPlanEduYearBeginningTrainings)
                                            .ThenInclude(py => py.EduYearBeginningTraining)
                .SingleOrDefaultAsync(u => u.UserName == userName);

            return appUser;
        }


        /// <summary>
        /// Возвращает набор образовательных программ,
        /// к которым имеет доступ пользователь
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EduProgram>> GetEduProgramsByUserNameAsync(string userName)
        {
            // Поиск аккаунта пользователя
            var appUser = await GetAppUserAsync(userName);

            var eduPrograms = new List<EduProgram>();

            foreach (var teacher in appUser.Teachers)
            {
                foreach (var teacherMetodKomissiya in teacher.TeacherMetodKomissii)
                {
                    foreach (var metodKomissiyaEduProfiles in teacherMetodKomissiya.MetodKomissiya.MetodKomissiyaEduProfiles)
                    {
                        foreach (var eduProgram in metodKomissiyaEduProfiles.EduProfile.EduPrograms)
                        {
                            eduPrograms.Add(eduProgram);
                        }
                    }
                }
            }

            return eduPrograms;
        }

        /// <summary>
        /// Определяет, является ли аутентифицированный
        /// пользователь членом методкомиссии
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsMetodKomissiyaMember(string userName)
        {
            // Поиск аккаунта пользователя
            var appUser = _context.Users
                .Include(u=>u.Teachers)
                    .ThenInclude(t=>t.TeacherMetodKomissii)
                .Where(u => u.UserName == userName)
                .SingleOrDefault();
            if (appUser == null) return false;

            foreach (var teacher in appUser.Teachers)
            {
                if(teacher.TeacherMetodKomissii.Any())
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Обновляет образовательную программу, если она доступна пользователю
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="eduProgram"></param>
        /// <param name="uploadedFile"></param>
        /// <param name="eduFormIds"></param>
        /// <param name="eduYearIds"></param>
        public async Task UpdateEduProgramByUserNameAsync(string userName, EduProgram eduProgram, IFormFile uploadedFile, int[] eduFormIds, int[] eduYearBeginningTrainingIds, int[] eduYearIds)
        {
            ///////
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
                    _context.EduProgramEduForms.RemoveRange(_context.EduProgramEduForms.Where(f => f.EduProgramId == eduProgram.EduProgramId));
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

                if (eduYearBeginningTrainingIds != null)
                {
                    _context.EduProgramEduYearBeginningTraining.RemoveRange(_context.EduProgramEduYearBeginningTraining.Where(f => f.EduProgramId == eduProgram.EduProgramId));
                    await _context.SaveChangesAsync();

                    var eduProgramEduYearBeginningTrainingList = new List<EduProgramEduYearBeginningTraining>();
                    foreach (int eduYearBeginningTrainingId in eduYearBeginningTrainingIds)
                    {
                        EduProgramEduYearBeginningTraining eduProgramEduYearBeginningTraining = new EduProgramEduYearBeginningTraining
                        {
                            EduProgramId = eduProgram.EduProgramId,
                            EduYearBeginningTrainingId = eduYearBeginningTrainingId
                        };
                        eduProgramEduYearBeginningTrainingList.Add(eduProgramEduYearBeginningTraining);
                    }
                    await _context.EduProgramEduYearBeginningTraining.AddRangeAsync(eduProgramEduYearBeginningTrainingList);
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
                throw;
            }
            ///////
        }

        /// <summary>
        /// Возвращает перечисление методкомиссий,
        /// в которые входит пользователь
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MetodKomissiya>> GetMetodKomissiiByUserNameAsync(string userName)
        {
            var appUser = await GetAppUserAsync(userName);
            var metodKomissii = new List<MetodKomissiya>();
            appUser.Teachers
                .ForEach(t=>t.TeacherMetodKomissii
                    .ForEach(tm => metodKomissii.Add(tm.MetodKomissiya) ));

            return metodKomissii;
        }

        /// <summary>
        /// Удаляет образовательную программу,
        /// если она доступна пользователю
        /// </summary>
        /// <param name="eduProgram"></param>
        /// <param name="userName"></param>
        public async Task RemoveEduProgramByUserNameAsync(EduProgram eduProgram, string userName)
        {
            var p = await GetEduProgramByUserNameAsync(eduProgram.EduProgramId, userName);

            if(p!=null && p.Equals(eduProgram))
            {
                _context.EduPrograms.Remove(eduProgram);
                Files.Files.RemoveFile(_context, _appEnvironment, eduProgram?.FileModelId);
                await _context.SaveChangesAsync();
            }            
        }

        /// <summary>
        /// Возвращает учебный план, если он доступен пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<EduPlan> GetEduPlanByUserNameAsync(int eduPlanId, string userName)
        {
            var appUser = await GetAppUserAsync(userName);
            var eduPlansOfUser = new List<EduPlan>();
            appUser.Teachers
                .ForEach(t => t.TeacherMetodKomissii
                    .ForEach(tm => tm.MetodKomissiya.MetodKomissiyaEduProfiles
                        .ForEach(mp => mp.EduProfile.EduPlans
                            .ForEach(ep => eduPlansOfUser.Add(ep)))));
            var eduPlan = eduPlansOfUser.SingleOrDefault(ep => ep.EduPlanId == eduPlanId);
            if (eduPlan == null)
            {
                return null;
            }

            EduPlan eduPlanWithFullData = await _eduPlanRepository.GetEduPlanAsync(eduPlan.EduPlanId);

            return eduPlanWithFullData;
        }

        /// <summary>
        /// Создаёт структуру учебного плана,
        /// если он доступен пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task CreateEduPlanStructureByUserNameAsync(int eduPlanId, string userName)
        {
            var eduPlan = await GetEduPlanByUserNameAsync(eduPlanId, userName);

            if(eduPlan!=null)
            {
                await _eduPlanRepository.CreateEduPlanStructureAsync(eduPlan);
            }
        }

        /// <summary>
        /// Добавляет дисциплину в справочник наименований дисциплин
        /// </summary>
        /// <param name="disciplineName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task CreateDisciplineNameByUserNameAsync(string disciplineName, string userName)
        {
            var metodKomissii = await GetMetodKomissiiByUserNameAsync(userName);
            if(metodKomissii.Count() > 0)
            {
                if(_context.DisciplineNames.Where(n => n.DisciplineNameName.ToLower() == disciplineName.ToLower()).Count()>0)
                {
                    return;
                }

                disciplineName = disciplineName.Trim();
                var disciplineNameNewObject = new DisciplineName { DisciplineNameName = disciplineName };
                await _context.DisciplineNames.AddAsync(disciplineNameNewObject);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="discipline"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task CreateEduPlanDisciplineByUserNameAsync(int eduPlanId, int blokDisciplChastId, Discipline discipline, string userName)
        {
            var eduPlan = await GetEduPlanByUserNameAsync(eduPlanId,userName);
            if (eduPlan == null) return;
            
            await _context.Disciplines.AddAsync(discipline);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает объект дисциплины,
        /// если учебный план доступен пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Discipline> GetDisciplineByUserNameAsync(int eduPlanId, int disciplineId, string userName)
        {
            var eduPlan = await GetEduPlanByUserNameAsync(eduPlanId, userName);
            if (eduPlan == null) return null;

            var discipline = _eduPlanRepository.GetDiscipline(eduPlan, disciplineId);
            return discipline;
        }

        /// <summary>
        /// Удаляет объект дисциплины,
        /// если учебный план доступен пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveDisciplineByUserNameAsync(int eduPlanId, int disciplineId, string userName)
        {
            var discipline = await GetDisciplineByUserNameAsync(eduPlanId, disciplineId, userName);
            if (discipline == null) return;
            
            await _eduPlanRepository.RemoveDiscipline(discipline);
            
        }

        /// <summary>
        /// Добавление нового или обновление существующего учебного плана,
        /// если образовательная программа доступна пользователю
        /// </summary>
        /// <param name="eduProgramId"></param>
        /// <param name="eduPlan"></param>
        /// <param name="uploadedFile"></param>
        /// <param name="eduVidDeyatIds"></param>
        /// <param name="eduYearBeginningTrainingIds"></param>
        /// <param name="eduPlanEduYearIds"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<EduPlan> CreateEduPlanByUserNameAsync(int eduProgramId,
            EduPlan eduPlan,
            IFormFile uploadedFile,
            int[] eduVidDeyatIds,
            int[] eduYearBeginningTrainingIds,
            int[] eduPlanEduYearIds,
            string userName)
        {
            var eduProgram = await GetEduProgramByUserNameAsync(eduProgramId, userName);
            if (eduProgram == null) return null;

            EduPlan eduPlanDbEntry = await _eduPlanRepository.CreateEduPlan(eduPlan,
                uploadedFile,
                eduVidDeyatIds,
                eduYearBeginningTrainingIds,
                eduPlanEduYearIds);

            return eduPlanDbEntry;
        }

        /// <summary>
        /// Удаляет учебный план, если он доступен пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveEduPlanByUserNameAsync(int eduPlanId, string userName)
        {
            var eduPlan = await GetEduPlanByUserNameAsync(eduPlanId, userName);
            if(eduPlan!=null)
            {
                await _eduPlanRepository.RemoveEduPlanAsync(eduPlan);
            }
        }

        /// <summary>
        /// Возвращает объект аннотации дисциплины.
        /// Если eduAnnotationId равно null,
        /// создаёт и возвращает новый объект
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduAnnotationId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<EduAnnotation> GetEduAnnotationByUserNameAsync(int eduPlanId, int disciplineId, int? eduAnnotationId, string userName)
        {
            var discipline = await GetDisciplineByUserNameAsync(eduPlanId, disciplineId, userName);
            if (discipline == null) return null;

            EduAnnotation eduAnnotation = new EduAnnotation();
            eduAnnotation.Discipline = discipline;
            eduAnnotation.DisciplineId = discipline.DisciplineId;
            if (eduAnnotationId == null || eduAnnotationId == 0) return eduAnnotation;

            eduAnnotation = discipline.EduAnnotations.FirstOrDefault(a => a.EduAnnotationId == eduAnnotationId);
            if (eduAnnotation == null) return null;
                        
            return eduAnnotation;
        }

        /// <summary>
        /// Возвращает объект рабочей программы дисциплины.
        /// Если rabProgramId равно null,
        /// создаёт и возвращает новый объект
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="rabProgramId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<RabProgram> GetRabProgramByUserNameAsync(int eduPlanId, int disciplineId, int? rabProgramId, string userName)
        {
            var discipline = await GetDisciplineByUserNameAsync(eduPlanId, disciplineId, userName);
            if (discipline == null) return null;

            RabProgram rabProgram = new RabProgram();
            rabProgram.Discipline = discipline;
            rabProgram.DisciplineId = discipline.DisciplineId;
            if (rabProgramId == null || rabProgramId == 0) return rabProgram;

            rabProgram = discipline.RabPrograms.FirstOrDefault(a => a.RabProgramId == rabProgramId);
            if (rabProgram == null) return null;

            return rabProgram;
        }

        /// <summary>
        /// Возвращает фонд оценочных средств дисциплины.
        /// Если fondOcenochnihSredstvId равно null,
        /// создаёт и возвращает новый объект
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="fondOcenochnihSredstvId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<FondOcenochnihSredstv> GetFondOcenochnihSredstvByUserNameAsync(int eduPlanId, int disciplineId, int? fondOcenochnihSredstvId, string userName)
        {
            var discipline = await GetDisciplineByUserNameAsync(eduPlanId, disciplineId, userName);
            if (discipline == null) return null;

            FondOcenochnihSredstv fondOcenochnihSredstv = new FondOcenochnihSredstv();
            fondOcenochnihSredstv.Discipline = discipline;
            fondOcenochnihSredstv.DisciplineId = discipline.DisciplineId;
            if (fondOcenochnihSredstvId == null || fondOcenochnihSredstvId == 0) return fondOcenochnihSredstv;

            fondOcenochnihSredstv = discipline.FondOcenochnihSredstvList.FirstOrDefault(a => a.FondOcenochnihSredstvId == fondOcenochnihSredstvId);
            if (fondOcenochnihSredstv == null) return null;

            return fondOcenochnihSredstv;
        }

        /// <summary>
        /// Возвращает связку "Преподаватель - Дисциплина",
        /// если она доступна пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduYearId"></param>
        /// <param name="teacherDisciplineId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<TeacherDiscipline> GetTeacherDisciplineByUserNameAsync(int eduPlanId,
            int disciplineId, int eduYearId, int? teacherDisciplineId, string userName)
        {
            var eduPlan = await GetEduPlanByUserNameAsync(eduPlanId, userName);
            if (eduPlan == null) return null;
            var eduPlanEduYear = eduPlan.EduPlanEduYears.FirstOrDefault(y => y.EduYearId == eduYearId);

            var discipline = await GetDisciplineByUserNameAsync(eduPlanId, disciplineId, userName);
            if (discipline == null) return null;

            TeacherDiscipline teacherDiscipline = _eduPlanRepository
                .GetTeacherDisciplineByDisciplineAndTeacherDisciplineId(discipline, teacherDisciplineId);

            if (teacherDiscipline == null)
            {
                teacherDiscipline = new TeacherDiscipline
                {
                    TeacherDisciplineId = teacherDisciplineId ?? 0,
                    Discipline = discipline,
                    DisciplineId = disciplineId,
                    EduPlanEduYear = eduPlanEduYear,
                    EduPlanEduYearId = eduPlanEduYear.EduPlanEduYearId
                };
            }

            return teacherDiscipline;
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

            eduAnnotation = await _eduPlanRepository.UpdateEduAnnotationAsync(eduAnnotation, uploadedFile);

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
            rabProgram = await _eduPlanRepository.UpdateRabProgramAsync(rabProgram, uploadedFile);
            return rabProgram;
        }

        /// <summary>
        /// Обновление файла листа переутверждения рабочей программы
        /// </summary>
        /// <param name="rabProgram"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<RabProgram> UpdateRabProgramListPereutverjdeniyaAsync(RabProgram rabProgram, IFormFile uploadedFile)
        {
            if (rabProgram == null || uploadedFile == null) return null;
            rabProgram = await _eduPlanRepository.UpdateRabProgramListPereutverjdeniyaAsync(rabProgram, uploadedFile);
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
            fondOcenochnihSredstv = await _eduPlanRepository.UpdateFondOcenochnihSredstvAsync(fondOcenochnihSredstv, uploadedFile);
            return fondOcenochnihSredstv;
        }

        /// <summary>
        /// Обновление листа переутверждения фонда оценочных средств
        /// </summary>
        /// <param name="fondOcenochnihSredstv"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<FondOcenochnihSredstv> UpdateFondOcenochnihSredstvListPereutverjdeniyaAsync(FondOcenochnihSredstv fondOcenochnihSredstv, IFormFile uploadedFile)
        {
            if (fondOcenochnihSredstv == null || uploadedFile == null) return null;
            fondOcenochnihSredstv = await _eduPlanRepository.UpdateFondOcenochnihSredstvListPereutverjdeniyaAsync(fondOcenochnihSredstv, uploadedFile);
            return fondOcenochnihSredstv;
        }

        /// <summary>
        /// Удаляет аннотацию, если она доступна пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduAnnotationId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveEduAnnotationByUserNameAsync(int eduPlanId, int disciplineId, int eduAnnotationId, string userName)
        {
            EduAnnotation eduAnnotation = await GetEduAnnotationByUserNameAsync(eduPlanId, disciplineId, eduAnnotationId, userName);
            if (eduAnnotation == null) return;

            await _eduPlanRepository.RemoveEduAnnotationAsync(eduAnnotation);
        }

        /// <summary>
        /// Удаляет рабочую программу, если она доступна пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="rabProgramId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveRabProgramByUserNameAsync(int eduPlanId, int disciplineId, int rabProgramId, string userName)
        {
            RabProgram rabProgram = await GetRabProgramByUserNameAsync(eduPlanId, disciplineId, rabProgramId, userName);
            if (rabProgram == null) return;

            await _eduPlanRepository.RemoveRabProgramAsync(rabProgram);
        }

        /// <summary>
        /// Удаляет лист переутверждения рабочей программы, если она доступна пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="rabProgramId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveRabProgramListPereutverjdeniyaByUserNameAsync(int eduPlanId, int disciplineId, int rabProgramId, string userName)
        {
            RabProgram rabProgram = await GetRabProgramByUserNameAsync(eduPlanId, disciplineId, rabProgramId, userName);
            if (rabProgram == null) return;

            await _eduPlanRepository.RemoveRabProgramListPereutverjdeniyaAsync(rabProgram);
        }

        /// <summary>
        /// Удаляет фонд оценочных средств, если он доступен пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="fondOcenochnihSredstvId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveFondOcenochnihSredstvByUserNameAsync(int eduPlanId, int disciplineId, int fondOcenochnihSredstvId, string userName)
        {
            FondOcenochnihSredstv fondOcenochnihSredstv = await GetFondOcenochnihSredstvByUserNameAsync(eduPlanId, disciplineId, fondOcenochnihSredstvId, userName);
            if (fondOcenochnihSredstv == null) return;

            await _eduPlanRepository.RemoveFondOcenochnihSredstvAsync(fondOcenochnihSredstv);
        }

        /// <summary>
        /// Удаляет лист переутверждения фонда оценочных средств дисциплины, если она доступна пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="fondOcenochnihSredstvId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveFondOcenochnihSredstvListPereutverjdeniyaByUserNameAsync(int eduPlanId, int disciplineId, int fondOcenochnihSredstvId, string userName)
        {
            FondOcenochnihSredstv fondOcenochnihSredstv = await GetFondOcenochnihSredstvByUserNameAsync(eduPlanId, disciplineId, fondOcenochnihSredstvId, userName);
            if (fondOcenochnihSredstv == null) return;

            await _eduPlanRepository.RemoveFondOcenochnihSredstvListPereutverjdeniyaAsync(fondOcenochnihSredstv);
        }

        /// <summary>
        /// Обновляет привязку "Преподаватель - Дисциплина"
        /// </summary>
        /// <param name="teacherDisciplineChanging"></param>
        /// <returns></returns>
        public async Task UpdateTeacherDisciplineAsync(TeacherDiscipline teacherDisciplineChanging)
        {
            await _eduPlanRepository.UpdateEduAnnotationAsync(teacherDisciplineChanging);
        }

        /// <summary>
        /// Удаляет привязку "Преподаватель - Дисциплина"
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduYearId"></param>
        /// <param name="teacherDisciplineId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveTeacherDisciplineByUserNameAsync(int eduPlanId, int disciplineId, int eduYearId, int teacherDisciplineId, string userName)
        {
            TeacherDiscipline teacherDiscipline = await GetTeacherDisciplineByUserNameAsync(eduPlanId, disciplineId, eduYearId, teacherDisciplineId, userName);
            if (teacherDiscipline == null || teacherDiscipline.TeacherDisciplineId == 0) return;

            await _eduPlanRepository.RemoveTeacherDisciplineAsync(teacherDiscipline);
        }

        /// <summary>
        /// Возвращает привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduYearId"></param>
        /// <param name="disciplinePomeshenieId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<DisciplinePomeshenie> GetDisciplinePomeshenieByUserNameAsync(int eduPlanId, int disciplineId, int eduYearId, int? disciplinePomeshenieId, string userName)
        {
            var eduPlan = await GetEduPlanByUserNameAsync(eduPlanId, userName);
            if (eduPlan == null) return null;
            var eduPlanEduYear = eduPlan.EduPlanEduYears.FirstOrDefault(y => y.EduYearId == eduYearId);

            var discipline = await GetDisciplineByUserNameAsync(eduPlanId, disciplineId, userName);
            if (discipline == null) return null;

            DisciplinePomeshenie disciplinePomeshenie = _eduPlanRepository
                .GetDisciplinePomeshenieByDisciplineAndDisciplinePomeshenieId(discipline, disciplinePomeshenieId);

            if (disciplinePomeshenie == null)
            {
                disciplinePomeshenie = new DisciplinePomeshenie
                {
                    DisciplinePomeshenieId = disciplinePomeshenieId ?? 0,
                    Discipline = discipline,
                    DisciplineId = disciplineId,
                    EduPlanEduYear = eduPlanEduYear,
                    EduPlanEduYearId = eduPlanEduYear.EduPlanEduYearId
                };
            }

            return disciplinePomeshenie;
        }

        /// <summary>
        /// Обновляет привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="disciplinePomeshenieChanging"></param>
        /// <returns></returns>
        public async Task UpdateDisciplinePomeshenieAsync(DisciplinePomeshenie disciplinePomeshenieChanging)
        {
            await _eduPlanRepository.UpdateDisciplinePomeshenieAsync(disciplinePomeshenieChanging);
        }

        /// <summary>
        /// Удаляет привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduYearId"></param>
        /// <param name="disciplinePomeshenieId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveDisciplinePomeshenieByUserNameAsync(int eduPlanId,
            int disciplineId, int eduYearId, int disciplinePomeshenieId, string userName)
        {
            DisciplinePomeshenie disciplinePomeshenie = await GetDisciplinePomeshenieByUserNameAsync(eduPlanId, disciplineId, eduYearId, disciplinePomeshenieId, userName);
            if (disciplinePomeshenie == null || disciplinePomeshenie.DisciplinePomeshenieId == 0) return;

            await _eduPlanRepository.RemoveDisciplinePomeshenieAsync(disciplinePomeshenie);
        }

        
    }
}
