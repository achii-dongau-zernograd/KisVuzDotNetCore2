﻿using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.MTO;
using KisVuzDotNetCore2.Models.Struct;
using KisVuzDotNetCore2.Models.Sveden;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers
{
    /// <summary>
    /// Контроллер, реализующий отображение раздела
    /// "Сведения об образовательной организации"
    /// </summary>
    public class SvedenController : Controller
    {
        ISelectListRepository _selectListRepository;
        IHostingEnvironment _appEnvironment;
        private readonly AppIdentityDBContext _context;
        private readonly IEduNapravlRepository _eduNapravlRepository;
        private readonly IPomeshenieRepository _pomeshenieRepository;
        private readonly IAddressPlacesRepository _addressPlacesRepository;
        private readonly IEduPOAccredRepository _eduPOAccredRepository;
        private readonly ITextBlockRepository _textBlockRepository;

        public SvedenController(IHostingEnvironment appEnvironment,
            AppIdentityDBContext context,
            ISelectListRepository selectListRepository,
            IEduNapravlRepository eduNapravlRepository,
            IPomeshenieRepository pomeshenieRepository,
            IAddressPlacesRepository addressPlacesRepository,
            IEduPOAccredRepository eduPOAccredRepository,
            ITextBlockRepository textBlockRepository)
        {
            _appEnvironment = appEnvironment;
            _context = context;
            _selectListRepository = selectListRepository;
            _eduNapravlRepository = eduNapravlRepository;
            _pomeshenieRepository = pomeshenieRepository;
            _addressPlacesRepository = addressPlacesRepository;
            _eduPOAccredRepository = eduPOAccredRepository;
            _textBlockRepository = textBlockRepository;
        }

        /// <summary>
        /// Подраздел "Основные сведения"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Common()
        {
            #region Таблица 2 "Основные сведения"            
            StructInstitute institute = _context
                .StructInstitutes
                .Include(a=>a.Address)
                .Include(a=>a.Telephones)
                .Include(a => a.Faxes)
                .Include(a => a.Emailes)
                .SingleOrDefault(i=>i.StructInstituteId==1);
            if(institute==null)
            {
                return NotFound();
            }

            ViewData["StructInstituteName"] = institute.StructInstituteName;
            ViewData["StructInstituteFullName"] = institute.StructInstituteFullName;
            ViewData["DateOfCreation"] = String.Format("{0:dd.MM.yyyy}", institute.DateOfCreation);
            ViewData["Address"] = institute.Address.GetAddress;
            ViewData["ExistenceOfFilials"] = institute.ExistenceOfFilials;
            ViewData["WorkingRegime"] = institute.WorkingRegime;
            ViewData["WorkingSchedule"] = institute.WorkingSchedule;
            ViewData["Telephones"] = institute.Telephones;
            ViewData["Faxes"] = institute.Faxes;
            ViewData["Emailes"] = institute.Emailes;
            #endregion

            #region Сведения о лицензии на осуществление образовательной деятельности (выписке из реестра лицензий на осуществление образовательной деятельности)                        
            var licenses = await _context.FileDataTypes                
                    .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                            .ThenInclude(fm => fm.SignList)
                .Where(g => g.Itemprop == "licenseDocLink")                
                .ToListAsync();
            ViewData["licenses"] = licenses;
            #endregion

            #region Сведения о наличии государственной аккредитации образовательной деятельности
            var accreds = await _context.FileDataTypes
                    .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                            .ThenInclude(fm => fm.SignList)
                .Where(g => g.Itemprop == "accreditationDocLink")
                .ToListAsync();
            ViewData["accreds"] = accreds;
            #endregion

            #region Таблица 3 "Сведения об учредителях"  

            var t3uchred = await _context.UchredLaw
                .ToListAsync();
            ViewData["t3uchred"] = t3uchred;

            
            #endregion

            #region Таблица 4 "Сведения о филиалах"  

            var t4filInfo = await _context.FilInfo
                .ToListAsync();
            ViewData["t4filInfo"] = t4filInfo;
            #endregion

            #region Места осуществления образовательной деятельности 

            var addressPlaces = await _addressPlacesRepository.GetAddressPlaces()
                .ToListAsync();
            ViewData["addressPlaces"] = addressPlaces;

            #endregion

            return View();
        }



        /// <summary>
        /// Подраздел "Структура и органы управления
        /// образовательной организацией"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Struct()
        {
            List<StructSubvisionType> structSubvisionTypes = await _context
                .StructSubvisionTypes
                .ToListAsync();
            List<StructSubvision> structSubvisions = await _context
                .StructSubvisions
                .Include(a => a.StructSubvisionAdress)
                .Include(a => a.StructSubvisionEmail)
                .Include(a => a.StructStandingOrder)
                .Include(a => a.StructSubvisionType)
                .Include(a => a.StructSubvisionPostChief)
                .ToListAsync();
            ViewData["StructSubvisions"] = structSubvisions;
            ViewData["StructSubvisionTypes"] = structSubvisionTypes;

            #region Таблица 4 "Сведения о филиалах"  

            var filInfo = await _context.FilInfo
                .ToListAsync();
            ViewData["filInfo"] = filInfo;            
            #endregion

            return View();
        }

        /// <summary>
        /// Подраздел "Документы"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Document()
        {
            var docs = await _context.FileDataTypeGroups.Where(g => g.FileDataTypeGroupName == "Сведения об образовательной организации - Документы")
                .Include(g => g.FileDataTypes)
                    .ThenInclude(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft=>ftft.FileModel)
                            .ThenInclude(fm => fm.SignList)
                .FirstOrDefaultAsync();
            //.ToListAsync();

            // Дополнительная информация
            var polojeniya = await _context.FileDataTypeGroups
                .Include(g => g.FileDataTypes)
                    .ThenInclude(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                            .ThenInclude(fm => fm.SignList)
                .SingleOrDefaultAsync(g => g.FileDataTypeGroupName == "Положения");
            ViewData["polojeniya"] = polojeniya;

            return View(docs);
        }

        /// <summary>
        /// Подраздел "Образование"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Education(string openedSpoiler)
        {
            ViewBag.OpenedSpoiler = openedSpoiler;

            #region Копия лицензии с приложениями
            var license = await _context.FileDataTypes.Where(fdt => fdt.FileDataTypeName == "Лицензия на осуществление образовательной деятельности")
                .Include(fdt => fdt.FileDataTypeGroup)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .ToListAsync();
            ViewData["license"] = license;
            #endregion

            #region Таблица 9. Информация о реализуемых уровнях образования, о формах обучения, нормативных сроках обучения, сроке действия государственной аккредитации образовательной программы (при наличии государственной аккредитации), о языках, на которых осуществляется образование(обучение)
            if (string.IsNullOrEmpty(openedSpoiler) || openedSpoiler == "eduAccred")
            {
                var t9eduAccred = await _eduNapravlRepository.GetEduNapravlEduFormEduSroksAsync();
                var eduProfiles = await _context.EduProfiles.ToListAsync();
                ViewData["t9eduAccred"] = t9eduAccred;
            }
            #endregion

            #region Информация о профессионально-общественной аккредитации образовательной программы
            if (string.IsNullOrEmpty(openedSpoiler) || openedSpoiler == "eduPOAccred")
            {
                var eduPOAccred = await _eduPOAccredRepository.GetEduPOAccreds().ToListAsync();                
                ViewData["eduPOAccred"] = eduPOAccred;
            }
            #endregion

            var EduLanguages = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.EduLanguages)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["EduLanguages"] = EduLanguages;

            var EduChislen = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.EduChislen)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["EduChislen"] = EduChislen;

            var EduPriem = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.EduPriem)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["EduPriem"] = EduPriem;

            var EduPerevod = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.EduPerevod)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["EduPerevod"] = EduPerevod;


            #region Таблица 7. Информация о численности обучающихся
            if (string.IsNullOrEmpty(openedSpoiler) || openedSpoiler == "eduChislen")
            {
                var t7eduChislen = await _context.EduChislens
                .Include(c => c.EduProfile)
                    .ThenInclude(n => n.EduNapravl)
                    .ThenInclude(u => u.EduUgs)
                    .ThenInclude(l => l.EduLevel)
                .Include(f => f.EduForm)
                .OrderBy(c => c.EduProfile.EduNapravl.EduUgs.EduLevelId)
                .ThenBy(c => c.EduProfile.EduNapravl.EduNapravlCode)
                .ToListAsync();
                ViewData["t7eduChislen"] = t7eduChislen;
            }
            #endregion

            #region Таблица 8. Информация о результатах приема
            if (string.IsNullOrEmpty(openedSpoiler) || openedSpoiler == "eduPriem")
            {
                var t8eduPriem = await _context.EduPriem
                .Include(e => e.EduNapravl)
                    .ThenInclude(n => n.EduUgs)
                    .ThenInclude(u => u.EduLevel)
                .Include(e => e.EduForm)
                .ToListAsync();
                ViewData["t8eduPriem"] = t8eduPriem;
            }
            #endregion

            #region Таблица 9. Информация о результатах перевода, восстановления и отчисления            
            if (string.IsNullOrEmpty(openedSpoiler) || openedSpoiler == "eduPerevod")
            {
                var t9eduPerevod = await _context.eduPerevod
                .Include(c => c.EduNapravl)
                    .ThenInclude(u => u.EduUgs)
                        .ThenInclude(l => l.EduLevel)
                .Include(f => f.EduForm)
                .ToListAsync();
                ViewData["t9eduPerevod"] = t9eduPerevod;
            }
            #endregion

            #region Таблица 10. Информация по образовательным программам
            if (string.IsNullOrEmpty(openedSpoiler) || openedSpoiler == "eduPrograms")
            {
                var eduPrograms = await _context.EduPrograms
                .Include(p => p.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(p => p.EduProfile.EduPlans)
                .Include(p => p.EduProgramEduForms)
                    .ThenInclude(pf => pf.EduForm)
                .Include(p => p.EduProgramEduYearBeginningTrainings)
                    .ThenInclude(py => py.EduYearBeginningTraining)
                .Include(p => p.EduProgramEduYears)
                    .ThenInclude(py => py.EduYear)
                .Include(p => p.EduProgramPodg)
                .Include(p => p.FileModel).ToListAsync();
                ViewData["t10eduPrograms"] = eduPrograms;

                var eduShedules = await _context.EduShedules
                    .Include(s => s.EduForm)
                    .Include(s => s.EduProfile.EduNapravl.EduUgs.EduLevel)
                    .Include(s => s.FileModel)
                    .Include(s => s.EduYear)
                    .ToListAsync();
                ViewData["eduShedules"] = eduShedules;

                var eduPlans = await _context.EduPlans
                    .Include(p => p.EduForm)
                    .Include(p => p.EduPlanEduYears)
                    .Include(p => p.EduPlanEduYearBeginningTrainings)
                        .ThenInclude(e => e.EduYearBeginningTraining)
                    .Include(p => p.EduPlanPdf)
                    .Include(p => p.EduProfile.EduNapravl.EduUgs.EduLevel)
                    .Include(p => p.EduProgramPodg)
                    .Include(p => p.EduSrok)
                    .Include(e => e.BlokDiscipl)
                        .ThenInclude(b => b.BlokDisciplChast)
                            .ThenInclude(c => c.Disciplines)
                                .ThenInclude(d => d.EduAnnotations)
                                    .ThenInclude(a => a.FileModel)
                    .Include(e => e.BlokDiscipl)
                        .ThenInclude(b => b.BlokDisciplChast)
                            .ThenInclude(c => c.Disciplines)
                                .ThenInclude(d => d.DisciplineName)
                    .ToListAsync();
                ViewData["eduPlans"] = eduPlans;
            }
            #endregion

            //#region Таблица 11. Образовательная программа (объём программы по годам)

            //var t11eduOPYears = await _context.EduOPYears
            //    .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)                   
            //    .Include(e => e.EduYearBeginningTraining)
            //    .Include(e=>e.EduOPEduYearName)
            //    .ToListAsync();
            //ViewData["t11eduOPYears"] = t11eduOPYears;
            //#endregion

            //#region Таблица 11. Информация о реализуемых образовательных программах, в том числе о реализуемых адаптированных образовательных программах, а также об использовании при реализации указанных образовательных программ электронного обучения и дистанционных образовательных технологиий
            //        var t11eduObrProg = await _context.EduPr
            //    .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
            //    .Include(e => e.EduYearBeginningTraining.EduPlanEduYearBeginningTrainings)   
            //        .ThenInclude(e=>e.EduPlan.EduPlanEduYears)
            //            .ThenInclude(e=>e.EduYear)
            //    .ToListAsync();
            //ViewData["t11eduObrProg"] = t11eduObrProg;
            //#endregion

            #region Таблица 13. Образовательная программа (направления и результаты научной (научно-исследовательской) деятельности
            if (string.IsNullOrEmpty(openedSpoiler) || openedSpoiler == "eduNir")
            {
                var t13eduNir = _context.EduNir.Include(n => n.EduProfile.EduNapravl.EduUgs.EduLevel);
                ViewData["t13eduNir"] = t13eduNir;
            }
            #endregion

            #region Информация о трудоустройстве выпускников
            if (string.IsNullOrEmpty(openedSpoiler) || openedSpoiler == "graduateJob")
            {
                // Годы выпуска
                var GraduateYear = await _context.GraduateYear.ToListAsync();
                ViewData["GraduateYear"] = GraduateYear;

                // Количество выпускников
                var EduGraduate = await _context.EduGraduate.Include(g => g.EduProfile.EduNapravl.EduUgs.EduLevel).ToListAsync();
                ViewData["EduGraduate"] = EduGraduate;

                // Количество трудоустроенных выпускников
                var GraduateTrudoustroustvo = await _context.GraduateTrudoustroustvo.Include(g => g.EduProfile.EduNapravl.EduUgs.EduLevel).ToListAsync();
                ViewData["GraduateTrudoustroustvo"] = GraduateTrudoustroustvo;
            }
            
            #endregion

            return View();
        }

        /// <summary>
        /// Подраздел "Образовательные стандарты"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> EduStandarts()
        {
            List<EduLevel> eduLevelsNapravls = await _context.EduLevels
                .Include(l => l.EduUgses)
                    .ThenInclude(u => u.EduNapravls)
                    .ToListAsync();
            return View(eduLevelsNapravls);
        }

        /// <summary>
        /// Подраздел "Руководство"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Managers()
        {
            var t14rucovodstvo = await _context.SvedenRucovodstvo
                .Include(r => r.AppUser)
                .OrderBy(r => r.OrderNumber)
                .ToListAsync();
            ViewData["t14rucovodstvo"] = t14rucovodstvo;

            var t15rucovodstvoFil = await _context.RucovodstvoFil
                .ToListAsync();
            ViewData["t15rucovodstvoFil"] = t15rucovodstvoFil;                      

            return View();
        }

        /// <summary>
        /// Подраздел "Руководство. Педагогический
        /// (научно-педагогический) состав"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Employees()
        {
            var t14rucovodstvo = await _context.SvedenRucovodstvo
                .Include(r=>r.AppUser)
                .OrderBy(r=>r.OrderNumber)
                .ToListAsync();
            ViewData["t14rucovodstvo"] = t14rucovodstvo;

            var t15rucovodstvoFil = await _context.RucovodstvoFil
                .ToListAsync();
            ViewData["t15rucovodstvoFil"] = t15rucovodstvoFil;

            #region Таблица 16-1. Пофамильный перечень педагогических (научно-педагогических) работников
            var teacher1 = await _context.Teachers
                .Include(t => t.TeacherStructKafPostStavka)
                    .ThenInclude(p => p.Post)
                .Include(t => t.TeacherStructKafPostStavka)
                    .ThenInclude(p => p.StructKaf.StructSubvision)
                .Include(t => t.TeacherStructKafPostStavka)
                    .ThenInclude(p => p.EmploymentForm)
                .Include(a => a.AppUser)
                    .ThenInclude(e => e.EduLevelGroup)
                .Include(a => a.AppUser)
                    .ThenInclude(aa => aa.AcademicDegree)
                .Include(a => a.AppUser)
                    .ThenInclude(aa => aa.AcademicStat)
                .Include(a => a.AppUser)
                    .ThenInclude(q => q.Qualifications)
                .Include(a => a.AppUser)
                    .ThenInclude(pr => pr.ProfessionalRetrainings)
                .Include(a => a.AppUser)
                    .ThenInclude(r => r.RefresherCourses)
                .Include(e => e.TeacherEduProfileDisciplineNames)
                    .ThenInclude(ed => ed.DisciplineName)
                .Include(e => e.TeacherEduProfileDisciplineNames)
                    .ThenInclude(ep => ep.EduProfile)
                        .ThenInclude(p => p.EduPlans)
                            .ThenInclude(f => f.EduForm)
                .Include(e => e.TeacherEduProfileDisciplineNames)
                    .ThenInclude(ep => ep.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Where(t => t.IsIdle != true)                
                .ToListAsync();
            ViewData["teacher1"] = teacher1;
            #endregion
            
            return View();
        }

        /// <summary>
        /// Преподаватели, ведущие занятия по направлениям подготовки
        /// </summary>
        /// <param name="EduProfileId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EmployeesOfEduProfile(int EduProfileId,
            int EduYearBeginningTrainingId,
            int EduYearId,
            bool needFetchDataFromDb)
        {
            if(needFetchDataFromDb)
            {
                var eduPlansOfSelectedEduProfile = await _context.EduPlans
                .Include(p => p.EduPlanEduYearBeginningTrainings)
                .Include(p => p.EduPlanEduYears)
                .Where(p => p.EduProfileId == EduProfileId)
                .ToListAsync();

                var eduPlansOfSelectedEduProfileAndYearBeginningTraining = new List<EduPlan>();
                foreach (var plan in eduPlansOfSelectedEduProfile)
                {
                    foreach (var eduPlanEduYearBeginningTraining in plan.EduPlanEduYearBeginningTrainings)
                    {
                        if (eduPlanEduYearBeginningTraining.EduYearBeginningTrainingId == EduYearBeginningTrainingId)
                        {
                            eduPlansOfSelectedEduProfileAndYearBeginningTraining.Add(plan);
                        }
                    }
                }

                var eduPlansFiltered = new List<EduPlan>();
                foreach (var plan in eduPlansOfSelectedEduProfileAndYearBeginningTraining)
                {
                    foreach (var eduPlanEduYears in plan.EduPlanEduYears)
                    {
                        if (eduPlanEduYears.EduYearId == EduYearId)
                        {
                            eduPlansFiltered.Add(plan);
                        }
                    }
                }


                #region Перечень педагогических (научно-педагогических) работников
                var teacher1 = await _context.Teachers
                    .Include(t => t.TeacherStructKafPostStavka)
                        .ThenInclude(p => p.Post)
                    .Include(t => t.TeacherStructKafPostStavka)
                        .ThenInclude(p => p.StructKaf.StructSubvision)
                    .Include(t => t.TeacherStructKafPostStavka)
                        .ThenInclude(p => p.EmploymentForm)
                    .Include(a => a.AppUser)
                        .ThenInclude(e => e.EduLevelGroup)
                    .Include(a => a.AppUser)
                        .ThenInclude(aa => aa.AcademicDegree)
                    .Include(a => a.AppUser)
                        .ThenInclude(aa => aa.AcademicStat)
                    .Include(a => a.AppUser)
                        .ThenInclude(q => q.Qualifications)
                    .Include(a => a.AppUser)
                        .ThenInclude(pr => pr.ProfessionalRetrainings)
                    .Include(a => a.AppUser)
                        .ThenInclude(r => r.RefresherCourses)
                    .Include(e => e.TeacherEduProfileDisciplineNames)
                        .ThenInclude(ed => ed.DisciplineName)
                    .Include(e => e.TeacherEduProfileDisciplineNames)
                        .ThenInclude(ep => ep.EduProfile)
                            .ThenInclude(p => p.EduPlans)
                                .ThenInclude(f => f.EduForm)
                    .Include(e => e.TeacherEduProfileDisciplineNames)
                        .ThenInclude(ep => ep.EduProfile)
                            .ThenInclude(p => p.EduNapravl)
                    .ToListAsync();
                ViewData["teacher1"] = teacher1;
                #endregion

                #region Таблица 16. Перечень педагогических (научно-педагогических) работников, задействованных в реализации образовательных программ                       

                var eduLevels = await _context.EduLevels
                    .Include(u => u.EduUgses)
                        .ThenInclude(en => en.EduNapravls)
                            .ThenInclude(p => p.EduProfiles)
                                .ThenInclude(pp => pp.EduPlans)
                                    .ThenInclude(py => py.EduPlanEduYears)
                                        .ThenInclude(t => t.TeacherDisciplines)
                                            .ThenInclude(tt => tt.Teacher)
                                                .ThenInclude(a => a.AppUser)
                    .Include(u => u.EduUgses)
                        .ThenInclude(en => en.EduNapravls)
                            .ThenInclude(p => p.EduProfiles)
                                .ThenInclude(pp => pp.EduPlans)
                                    .ThenInclude(py => py.EduPlanEduYears)
                                        .ThenInclude(t => t.TeacherDisciplines)
                                            .ThenInclude(tt => tt.Teacher)
                                                .ThenInclude(ttt => ttt.TeacherStructKafPostStavka)
                                                    .ThenInclude(tp => tp.StructKaf.StructSubvision)
                    .Include(u => u.EduUgses)
                        .ThenInclude(en => en.EduNapravls)
                            .ThenInclude(p => p.EduProfiles)
                                .ThenInclude(pp => pp.EduPlans)
                                    .ThenInclude(py => py.EduPlanEduYears)
                                        .ThenInclude(t => t.TeacherDisciplines)
                                            .ThenInclude(tt => tt.Discipline)
                                                .ThenInclude(d => d.DisciplineName)
                     .Include(u => u.EduUgses)
                        .ThenInclude(en => en.EduNapravls)
                            .ThenInclude(p => p.EduProfiles)
                                .ThenInclude(pp => pp.EduPlans)
                                    .ThenInclude(f => f.EduForm)
                    .Include(u => u.EduUgses)
                        .ThenInclude(en => en.EduNapravls)
                            .ThenInclude(p => p.EduProfiles)
                                .ThenInclude(pp => pp.EduPlans)
                                    .ThenInclude(f => f.EduProgramPodg)
                    .Include(u => u.EduUgses)
                        .ThenInclude(en => en.EduNapravls)
                            .ThenInclude(p => p.EduProfiles)
                                .ThenInclude(pp => pp.EduPlans)
                                    .ThenInclude(f => f.EduPlanEduYearBeginningTrainings)
                                        .ThenInclude(f => f.EduYearBeginningTraining)
                    .Include(u => u.EduUgses)
                        .ThenInclude(en => en.EduNapravls)
                            .ThenInclude(p => p.EduProfiles)
                                .ThenInclude(pp => pp.EduPlans)
                                    .ThenInclude(f => f.EduPlanEduYears)
                                        .ThenInclude(f => f.EduYear)
                    .ToListAsync();

                ViewData["eduLevels"] = eduLevels;
                //ViewData["eduLevels"] = eduPlansFiltered; 
            }
                        
            #endregion

            ViewBag.EduProfiles = _selectListRepository.GetSelectListEduProfileFullNames(EduProfileId);
            //ViewBag.EduYearBeginningTrainings = _selectListRepository.GetSelectListEduYearBeginningTrainings(EduYearBeginningTrainingId);
            ViewBag.EduYearBeginningTrainings = _selectListRepository.GetSelectListEduYearBeginningTrainingsFrom(EduYearBeginningTrainingId, 3);
            //ViewBag.EduYears = _selectListRepository.GetSelectListEduYears(EduYearId);

            var eduYearsList = new List<string> { "2015-", "2016-", "2017-", "2018-", "2019-", "2020-", };
            ViewBag.EduYears = _selectListRepository.GetSelectListEduYearsFromStringList(EduYearId, eduYearsList);

            ViewBag.EduProfileId = EduProfileId;
            ViewBag.EduYearBeginningTrainingId = EduYearBeginningTrainingId;
            ViewBag.EduYearId = EduYearId;

            return View();
        }

        /// <summary>
        /// Подраздел "Материально-техническое обеспечение
        /// и оснащенность образовательного процесса"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Objects()
        {
            #region Таблица 17. Сведения о наличии оборудованных учебных кабинетов
            ViewData["UchCabinets"] = await _pomeshenieRepository.GetUchCabinets().ToListAsync();
            ViewData["ObjectsForPractLessons"] = await _pomeshenieRepository.GetObjectsForPractLessons().ToListAsync();

            //int currentEduYear = (await _context.AppSettings.SingleOrDefaultAsync(s => s.AppSettingId == (int)AppSettingTypesEnum.CurrentEduYear)).AppSettingValue;
            //ViewData["currentEduYear"] = currentEduYear;

            //var disciplinePomeshenie = await _context.DisciplinePomeshenies
            //    .Include(p => p.Discipline.DisciplineName)
            //    .Include(p => p.EduPlanEduYear.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
            //    .Include(p => p.Pomeshenie.Korpus)
            //    .Include(p => p.Pomeshenie.OborudovanieList)
            //    .Where(p => p.EduPlanEduYear.EduYearId == currentEduYear).ToListAsync();

            //ViewData["disciplinePomeshenie"] = disciplinePomeshenie;

            //var eduLevelsToPomeshenie = _context.EduLevels
            //    .Include(l => l.EduUgses)
            //        .ThenInclude(u => u.EduNapravls)
            //            .ThenInclude(n => n.EduProfiles)
            //                .ThenInclude(p => p.EduPlans)
            //                    .ThenInclude(ep => ep.EduPlanEduYears)
            //                        .ThenInclude(epey => epey.DisciplinePomeshenies)
            //                            .ThenInclude(dp => dp.Pomeshenie)
            //                                .ThenInclude(p=>p.OborudovanieList)
            //   .Include(l => l.EduUgses)
            //        .ThenInclude(u => u.EduNapravls)
            //            .ThenInclude(n => n.EduProfiles)
            //                .ThenInclude(p => p.EduPlans)
            //                    .ThenInclude(ep => ep.EduPlanEduYears)
            //                        .ThenInclude(epey => epey.DisciplinePomeshenies)
            //                            .ThenInclude(dp => dp.Discipline.DisciplineName)
            //   .Include(l => l.EduUgses)
            //        .ThenInclude(u => u.EduNapravls)
            //            .ThenInclude(n => n.EduProfiles)
            //                .ThenInclude(p => p.EduPlans)
            //                    .ThenInclude(ep => ep.EduForm)
            //   .Include(l => l.EduUgses)
            //        .ThenInclude(u => u.EduNapravls)
            //            .ThenInclude(n => n.EduProfiles)
            //                .ThenInclude(p => p.EduPlans)
            //                    .ThenInclude(ep => ep.EduProgramPodg)
            //   .Include(l => l.EduUgses)
            //        .ThenInclude(u => u.EduNapravls)
            //            .ThenInclude(n => n.EduProfiles)
            //                .ThenInclude(p => p.EduPlans)
            //                    .ThenInclude(ep => ep.EduPlanEduYearBeginningTrainings)
            //                        .ThenInclude(epey => epey.EduYearBeginningTraining)
            //   .Include(l => l.EduUgses)
            //        .ThenInclude(u => u.EduNapravls)
            //            .ThenInclude(n => n.EduProfiles)
            //                .ThenInclude(p => p.EduPlans)
            //                    .ThenInclude(ep => ep.EduPlanEduYears)
            //                        .ThenInclude(epey => epey.EduYear)
            //   .ToList();

            //ViewData["eduLevelsToPomeshenie"] = eduLevelsToPomeshenie;
            #endregion

            int NumEbs = _context.ElectronBiblSystem.Count();
            int NumEbsSobstv= _context.ElectronBiblSystem.Where(ebs=>ebs.NumberDogovor==string.Empty).Count();
            int NumEbsDogovor = NumEbs - NumEbsSobstv;
            ViewData["NumEbs"] = NumEbs;
            ViewData["NumEbsSobstv"] = NumEbsSobstv;
            ViewData["NumEbsDogovor"] = NumEbsDogovor;
            int NumEc = _context.ElectronCatalog.Count();
            ViewData["NumEc"] = NumEc;
            int NumEoisOwn = _context.ElectronObrazovatInformRes.Where(e=>e.IsSobstv==true).Count();
            ViewData["NumEoisOwn"] = NumEoisOwn;
            int NumEoisSide = _context.ElectronObrazovatInformRes.Where(e=>e.IsSobstv==false).Count();
            ViewData["NumEoisSide"] = NumEoisSide;

            var PurposeLibrs = await _context.PurposeLibr.ToListAsync();
            ViewData["PurposeLibrs"] = PurposeLibrs;

            var hostelInfo = await _context.HostelInfo.ToListAsync();
            ViewData["hostelInfo"] = hostelInfo;


            // Информация об обеспечении беспрепятственного доступа в здания образовательной организации
            // Текст
            var InfObObespBesprDostupaVZdaniyaObrOrg_TextBlocks = await _textBlockRepository.GetTextBlocks("InfObObespBesprDostupaVZdaniyaObrOrg").ToListAsync();
            ViewData["InfObObespBesprDostupaVZdaniyaObrOrg_TextBlocks"] = InfObObespBesprDostupaVZdaniyaObrOrg_TextBlocks;
            // Файлы
            var InfObObespBesprDostupaVZdaniyaObrOrg = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.InfObObespBesprDostupaVZdaniyaObrOrg)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["InfObObespBesprDostupaVZdaniyaObrOrg"] = InfObObespBesprDostupaVZdaniyaObrOrg;

            // Информация о наличии специальных технических средств обучения коллективного и индивидуального пользования
            var InfONalichiiSpecTehnSredstvObucheniyaKollektivnIIndividPolzovaniya = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.InfONalichiiSpecTehnSredstvObucheniyaKollektivnIIndividPolzovaniya)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["InfONalichiiSpecTehnSredstvObucheniyaKollektivnIIndividPolzovaniya"] = InfONalichiiSpecTehnSredstvObucheniyaKollektivnIIndividPolzovaniya;

            // Сведения о формировании платы за проживание в общежитии
            var InfOFormirovaniiPlatiZaProjivanieVObsch = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.InfOFormirovaniiPlatiZaProjivanieVObsch)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["InfOFormirovaniiPlatiZaProjivanieVObsch"] = InfOFormirovaniiPlatiZaProjivanieVObsch;


            return View();
        }

        /// <summary>
        /// Подраздел "Стипендии и иные виды материальной поддержки"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Grants()
        {
            var StipendiiFederalGrant = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.StipendiiFederalGrant)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["StipendiiFederalGrant"] = StipendiiFederalGrant;

            var StipendiiLocalGrant = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.StipendiiLocalGrant)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["StipendiiLocalGrant"] = StipendiiLocalGrant;


            // Годы выпуска
            var GraduateYear = await _context.GraduateYear.ToListAsync();
            ViewData["GraduateYear"] = GraduateYear;

            // Количество выпускников
            var EduGraduate = await _context.EduGraduate.Include(g=>g.EduProfile.EduNapravl.EduUgs.EduLevel).ToListAsync();
            ViewData["EduGraduate"] = EduGraduate;

            // Количество трудоустроенных выпускников
            var GraduateTrudoustroustvo = await _context.GraduateTrudoustroustvo.Include(g => g.EduProfile.EduNapravl.EduUgs.EduLevel).ToListAsync();
            ViewData["GraduateTrudoustroustvo"] = GraduateTrudoustroustvo;


            var t20hostelInfo = await _context.HostelInfo
                .ToListAsync();
            ViewData["t20hostelInfo"] = t20hostelInfo;

            var InfOFormirovaniiPlatiZaProjivanieVObsch = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.InfOFormirovaniiPlatiZaProjivanieVObsch)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["InfOFormirovaniiPlatiZaProjivanieVObsch"] = InfOFormirovaniiPlatiZaProjivanieVObsch;

            return View();
        }

        /// <summary>
        /// Подраздел "Платные образовательные услуги"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Paid_edu()
        {
            var ObrazecDogovoraObOkazaniiPlatnihObrazovatelnihUslug = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.ObrazecDogovoraObOkazaniiPlatnihObrazovatelnihUslug)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["ObrazecDogovoraObOkazaniiPlatnihObrazovatelnihUslug"] = ObrazecDogovoraObOkazaniiPlatnihObrazovatelnihUslug;

            var DocumentObUtverjdeniiStoimostiObucheniya = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.DocumentObUtverjdeniiStoimostiObucheniya)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["DocumentObUtverjdeniiStoimostiObucheniya"] = DocumentObUtverjdeniiStoimostiObucheniya;

            var PoryadokOkazaniyaPlatnihObrazovatelnihUslug = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.PoryadokOkazaniyaPlatnihObrazovatelnihUslug)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["PoryadokOkazaniyaPlatnihObrazovatelnihUslug"] = PoryadokOkazaniyaPlatnihObrazovatelnihUslug;

            return View();
        }

        /// <summary>
        /// Подраздел "Финансово-хозяйственная деятельность"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Budget()
        {
            var t22volume = await _context.Volume
                .ToListAsync();
            ViewData["t22volume"] = t22volume;

            var volumeFinYearPostRas = await _context.VolumeFinYearPostRas
                .OrderBy(v => v.FinYear)
                .ToListAsync();
            ViewData["volumeFinYearPostRas"] = volumeFinYearPostRas;

            var InfOPostupleniiIRashodovaniiFinIMaterialnihSredstv = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.InfOPostupleniiIRashodovaniiFinIMaterialnihSredstv)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["InfOPostupleniiIRashodovaniiFinIMaterialnihSredstv"] = InfOPostupleniiIRashodovaniiFinIMaterialnihSredstv;

            var planFinansovoHozyaystvennoyDeyatelnosti = await _context.FileDataTypes
                .Where(t => t.FileDataTypeId == (int)FileDataTypeEnum.PlanFinansovoHozyaystvennoyDeyatelnosti)
                .Include(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .FirstOrDefaultAsync();
            ViewData["planFinansovoHozyaystvennoyDeyatelnosti"] = planFinansovoHozyaystvennoyDeyatelnosti;

            var busGovRuLink = await _context.InstituteLinks.FirstOrDefaultAsync(l => l.StructInstituteId == 1 && l.LinkTypeId == (int)LinkTypesEnum.BusGovRu);
            ViewData["busGovRuLink"] = busGovRuLink;

            return View();
        }

   

        /// <summary>
        /// Подраздел "Вакантные места для приёма (перевода)"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Vacant()
        {
            var t23vacant = await _context.Vacants
                .Include(l => l.EduNapravl.EduUgs.EduLevel)
                .Include(l => l.EduNapravl)
                .Include(n => n.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(l => l.EduKurs)
                .Include(l => l.EduForm)
                .ToListAsync();
            ViewData["t23vacant"] = t23vacant;
            return View();
        }


        /// <summary>
        /// Подраздел "Доступная среда"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Ovz()
        {
            var PurposeLibrs = await _context.PurposeLibr
                //.Where(pl=>pl.itemprop.Contains("Ovz"))
                .Where(pl => !pl.PrisposoblOvz.Contains("недоступно"))
                .ToListAsync();
            ViewData["PurposeLibrs"] = PurposeLibrs;

            return View();
        }


        /// <summary>
        /// Подраздел "Международное сотрудничество"
        /// </summary>
        /// <returns></returns>
        public IActionResult Inter()
        {

            return View();
        }


        /// <summary>
        /// Подраздел "Организация питания в образовательной организации"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Catering()
        {
            var MealsAndHealth = await _context.PurposeLibr
                .Where(p => p.itemprop == "meals" || p.itemprop == "mealsOvz" || p.itemprop == "health" || p.itemprop == "healthOvz")
                .ToListAsync();
            ViewData["MealsAndHealth"] = MealsAndHealth;

            return View();
        }



        /// <summary>
        /// Подраздел "Шаблон представления информации по различным условиям поступления"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PriemExam()
        {               
            var t25PriemExam=await _context.PriemExam
                 .Include(l => l.EduNapravl)
             .ToListAsync();
            ViewData["t25PriemExam"] = t25PriemExam;
            return View();
        }


        /// <summary>
        /// Подраздел "Шаблона представления информации по различным условиям"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> HostelInfo()
        {
            var t20HostelInfo= await _context.HostelInfo.ToListAsync();
            ViewData["t20HostelInfo"] = t20HostelInfo;            
       
            return View();
        }


        /// <summary>
        /// Подраздел "Информация о количестве мест для приема на обучение по различным условиям поступления"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> priemKolMest()
        {
            var t24priemKolMest = await _context.priemKolMest
                .Include(l => l.EduNapravl)
                .ToListAsync();
            ViewData["t24priemKolMest"] = t24priemKolMest;

            return View();
        }

    }
}
