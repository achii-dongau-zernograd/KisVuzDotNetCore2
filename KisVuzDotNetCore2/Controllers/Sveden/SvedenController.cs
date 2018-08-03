using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Struct;
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
        IHostingEnvironment _appEnvironment;
        private readonly AppIdentityDBContext _context;

        public SvedenController(IHostingEnvironment appEnvironment, AppIdentityDBContext context)
        {
            _appEnvironment = appEnvironment;
            _context = context;
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

            ViewData["DateOfCreation"] = String.Format("{0:dd.MM.yyyy}", institute.DateOfCreation);
            ViewData["Address"] = institute.Address.GetAddress;
            ViewData["ExistenceOfFilials"] = institute.ExistenceOfFilials;
            ViewData["WorkingRegime"] = institute.WorkingRegime;
            ViewData["WorkingSchedule"] = institute.WorkingSchedule;
            ViewData["Telephones"] = institute.Telephones;
            ViewData["Faxes"] = institute.Faxes;
            ViewData["Emailes"] = institute.Emailes;
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
            return View();
            #endregion

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
                .FirstOrDefaultAsync();
            //.ToListAsync();

            // Дополнительная информация
            var polojeniya = await _context.FileDataTypeGroups
                .Include(g => g.FileDataTypes)
                    .ThenInclude(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileModel)
                .SingleOrDefaultAsync(g => g.FileDataTypeGroupName == "Положения");
            ViewData["polojeniya"] = polojeniya;

            return View(docs);
        }

        /// <summary>
        /// Подраздел "Образование"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Education()
        {
            #region Таблица 6. Инфоормация о сроке действия государственной аккредитации            
            var t6eduAccred = await _context.EduNapravls                
                .Include(l => l.EduUgs.EduAccred.EduAccredFile)
                .Include(l => l.EduUgs)
                    .ThenInclude(ugs => ugs.EduLevel)                
                .ToListAsync();
            ViewData["t6eduAccred"] = t6eduAccred;
            #endregion

            #region Таблица 7. Инфоормация о численности обучающихся            
            var t7eduChislen = await _context.EduChislens
                .Include(c => c.EduProfile)
                    .ThenInclude(n => n.EduNapravl)
                    .ThenInclude(u => u.EduUgs)
                    .ThenInclude(l => l.EduLevel)
                .Include (f =>f.EduForm)
                .ToListAsync();
            ViewData["t7eduChislen"] = t7eduChislen;
            #endregion

            #region Таблица 8. Инфоормация о результатах приема            
            var t8eduPriem = await _context.EduPriem
                .Include(e => e.EduNapravl)
                    .ThenInclude(n => n.EduUgs)
                    .ThenInclude(u => u.EduLevel)
                .Include(e => e.EduForm)
                .ToListAsync();
            ViewData["t8eduPriem"] = t8eduPriem;
            #endregion

            #region Таблица 9. Инфоормация о результатах перевода, восстановления и отчисления            
            var t9eduPerevod = await _context.eduPerevod
                .Include(c => c.EduNapravl)                    
                    .ThenInclude(u => u.EduUgs)
                        .ThenInclude(l => l.EduLevel)
                .Include(f => f.EduForm)
                .ToListAsync();
            ViewData["t9eduPerevod"] = t9eduPerevod;
            #endregion

            #region Таблица 10. Инфоормация по образовательным программам
            var eduPrograms = await _context.EduPrograms
                .Include(p => p.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(p => p.EduProgramEduForms)
                .Include(p => p.EduProgramEduYears)
                .Include(p => p.EduProgramPodg)
                .Include(p => p.FileModel).ToListAsync();
            foreach (var eduProgram in eduPrograms)
            {
                foreach (var eduProgramEduForm in eduProgram.EduProgramEduForms)
                {
                    eduProgramEduForm.EduForm = await _context.EduForms.SingleOrDefaultAsync(f => f.EduFormId == eduProgramEduForm.EduFormId);
                }
                foreach (var eduProgramEduYear in eduProgram.EduProgramEduYears)
                {
                    eduProgramEduYear.EduYear = await _context.EduYears.SingleOrDefaultAsync(f => f.EduYearId == eduProgramEduYear.EduYearId);
                }
            }
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
                            .ThenInclude(d=>d.DisciplineName)
                .ToListAsync();
            ViewData["eduPlans"] = eduPlans;                        
            #endregion

            #region Таблица 11. Образовательная программа (объём программы по годам)
            var t11eduOPYears = await _context.EduOPYears
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)                   
                .Include(e => e.EduYearBeginningTraining)
                .Include(e=>e.EduOPEduYearName)
                .ToListAsync();
            ViewData["t11eduOPYears"] = t11eduOPYears;
            #endregion

            #region Таблица 12. Образовательная программа (наличие практики)
            var t12eduPr = await _context.EduPr
                .Include(e => e.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(e => e.EduYearBeginningTraining)                
                .ToListAsync();
            ViewData["t12eduPr"] = t12eduPr;
            #endregion

            #region Таблица 13. Образовательная программа (направления и результаты научной (научно-исследовательской) деятельности
            var t13eduNir = _context.EduNir.Include(n=>n.EduNapravl.EduUgs.EduLevel);
            ViewData["t13eduNir"] = t13eduNir;
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
        /// Подраздел "Руководство. Педагогический
        /// (научно-педагогический) состав"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Employees()
        {
            var t14rucovodstvo = await _context.SvedenRucovodstvo
                .ToListAsync();
            ViewData["t14rucovodstvo"] = t14rucovodstvo;

            var t15rucovodstvoFil = await _context.RucovodstvoFil
                .ToListAsync();
            ViewData["t15rucovodstvoFil"] = t15rucovodstvoFil;

            #region Таблица 16. Информация о составе педагогических (научно-педагогических) работников образовательной организации
            // Перечень образовательных программ, для которых имеются сведения о распределении дисциплин по преподавателям---
            var eduProfiles = await _context.EduProfiles
                .Include(p=>p.EduNapravl.EduUgs.EduLevel)
                .ToListAsync();//Add filter by teacher-disc data---
            ViewData["eduProfiles"] = eduProfiles;


            //////////
            // Очищаем таблицу TeacherEduProfileDisciplineNames
            _context.TeacherEduProfileDisciplineNames.RemoveRange(_context.TeacherEduProfileDisciplineNames);
            await _context.SaveChangesAsync();

            int currentEduYearId = (await _context.AppSettings.SingleOrDefaultAsync(s=>s.AppSettingId==(int)AppSettingTypesEnum.CurrentEduYear)).AppSettingValue;
            // Группируем TeacherDisciplines по коду профиля EduProfileId
            var teacherDisciplinesGroupedByEduProfileId = await _context.TeacherDisciplines
                .Include(td => td.Discipline.DisciplineName)
                .Include(td => td.EduPlanEduYear.EduPlan)
                .Where(td=>td.EduPlanEduYear.EduYearId== currentEduYearId)//Фильтр по учебному году 2 - 2018-2019
                .GroupBy(td=>td.EduPlanEduYear.EduPlan.EduProfileId)
                .ToListAsync();
            
            foreach (var teacherDisciplinesGroupedByEduProfileIdItem in teacherDisciplinesGroupedByEduProfileId)
            {
                // Группируем по коду преподавателя
                foreach(var teacherIdG in teacherDisciplinesGroupedByEduProfileIdItem.GroupBy(g=>g.TeacherId))
                {
                    foreach (var teacherDiscipline in teacherIdG)
                    {                        
                        var teacherEduProfileDisciplineName = new TeacherEduProfileDisciplineName();
                        teacherEduProfileDisciplineName.EduProfileId = teacherDisciplinesGroupedByEduProfileIdItem.Key;
                        teacherEduProfileDisciplineName.TeacherId = teacherIdG.Key;
                        teacherEduProfileDisciplineName.DisciplineNameId = teacherDiscipline.Discipline.DisciplineNameId;

                        var findedItem = await _context.TeacherEduProfileDisciplineNames.SingleOrDefaultAsync(tepd => 
                        tepd.EduProfileId == teacherEduProfileDisciplineName.EduProfileId && 
                        tepd.TeacherId == teacherEduProfileDisciplineName.TeacherId &&
                        tepd.DisciplineNameId == teacherEduProfileDisciplineName.DisciplineNameId);
                        if (findedItem==null)
                        {
                            await _context.TeacherEduProfileDisciplineNames.AddAsync(teacherEduProfileDisciplineName);
                            await _context.SaveChangesAsync();
                        }
                    }                    
                }                
            }
            
            //////////

            var teacherEduProfileDisciplineNames = await _context.TeacherEduProfileDisciplineNames
                .Include(tpd => tpd.Teacher.AppUser.EduLevelGroup)
                .Include(tpd => tpd.Teacher.AppUser.Qualifications)
                .Include(tpd => tpd.Teacher.AppUser.AcademicDegree)
                .Include(tpd => tpd.Teacher.AppUser.AcademicStat)
                .Include(tpd => tpd.Teacher.AppUser.ProfessionalRetrainings)
                .Include(tpd => tpd.Teacher.AppUser.RefresherCourses)
                .Include(tdp => tdp.Teacher.TeacherStructKafPostStavka)
                .Include(tpd => tpd.DisciplineName)
                .ToListAsync();
            ViewData["teacherEduProfileDisciplineNames"] = teacherEduProfileDisciplineNames;
            
            var posts = await _context.Posts.ToListAsync();
            ViewData["posts"] = posts;            
            #endregion

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

            int currentEduYear = (await _context.AppSettings.SingleOrDefaultAsync(s => s.AppSettingId == (int)AppSettingTypesEnum.CurrentEduYear)).AppSettingValue;

            var disciplinePomeshenie = await _context.DisciplinePomeshenies
                .Include(p => p.Discipline.DisciplineName)
                .Include(p => p.EduPlanEduYear.EduPlan.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(p => p.Pomeshenie.Korpus)
                .Include(p => p.Pomeshenie.OborudovanieList)
                .Where(p => p.EduPlanEduYear.EduYearId == currentEduYear).ToListAsync();

            ViewData["disciplinePomeshenie"] = disciplinePomeshenie;

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
                .Include(l => l.EduKurs)
                .Include(l => l.EduForm)
                .ToListAsync();
            ViewData["t23vacant"] = t23vacant;
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
        /// Подраздел "Шаблона представления информации по различныфм условиям"
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
