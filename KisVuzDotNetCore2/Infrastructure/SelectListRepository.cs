using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Struct;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Репозиторий списков (справочников)
    /// </summary>
    public class SelectListRepository : ISelectListRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IEducationalInstitutionRepository _educationalInstitutionRepository;
        private readonly IPopulatedLocalityRepository _populatedLocalityRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Внедрение зависимостей
        /// </summary>
        /// <param name="context"></param>
        public SelectListRepository(AppIdentityDBContext context,
            IUserProfileRepository userProfileRepository,
            IEducationalInstitutionRepository educationalInstitutionRepository,
            IPopulatedLocalityRepository populatedLocalityRepository,
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userProfileRepository = userProfileRepository;
            _educationalInstitutionRepository = educationalInstitutionRepository;
            _populatedLocalityRepository = populatedLocalityRepository;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Возвращает список пользователей по строке ФИО
        /// </summary>
        /// <param name="fio"></param>
        /// <returns></returns>
        public SelectList GetSelectListAppUsersByFirstName(string fio)
        {
            string lastName       = "";
            string other          = "";
            string firstNameAbbr  = "";
            string patronymicAbbr = "";
            List<AppUser> findedAppUsers;

            string[] lastNameAndOtherList = fio.Split(' ');
            if(lastNameAndOtherList.Count() > 1)
            {
                lastName = lastNameAndOtherList[0];
                other    = lastNameAndOtherList[1];

                string[] abbreviatures = other.Split('.', StringSplitOptions.RemoveEmptyEntries);
                int abbreviaturesNum = abbreviatures.Count();

                if(abbreviaturesNum > 0)
                {
                    firstNameAbbr  = abbreviatures[0];                    
                }
                if (abbreviaturesNum > 1)
                {
                    patronymicAbbr = abbreviatures[1];
                }

                findedAppUsers = _context.Users.Where(u => u.LastName.ToLower().Contains(lastName)).ToList();

                if (!string.IsNullOrWhiteSpace(firstNameAbbr))
                    findedAppUsers = findedAppUsers.Where(u => u.FirstName.Contains(firstNameAbbr)).ToList();
                if (!string.IsNullOrWhiteSpace(patronymicAbbr))
                    findedAppUsers = findedAppUsers.Where(u => u.Patronymic.Contains(patronymicAbbr)).ToList();
            }
            else
            {
                findedAppUsers = _context.Users.Where(u => u.LastName == fio).ToList();
            }                       

            return new SelectList(findedAppUsers, "Id", "GetFullName");
        }

        /// <summary>
        /// Возвращает список авторов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAuthors(int selectedId = 0)
        {
            return new SelectList(_context.Author.Include(a=>a.AppUser).OrderBy(a=>a.AuthorName), "AuthorId", "AuthorName");
        }

        /// <summary>
        /// Возвращает отфильтрованный список авторов
        /// </summary>
        /// <param name="authorFilter"></param>
        /// <returns></returns>
        public SelectList GetSelectListAuthors(string authorFilter)
        {
            return new SelectList(_context.Author
                .Include(a => a.AppUser)
                .Where(a => string.IsNullOrWhiteSpace(authorFilter) ? true : a.AuthorName.Contains(authorFilter))
                .OrderBy(a => a.AuthorName), "AuthorId", "AuthorName");
        }

        /// <summary>
        /// Возвращает список наименований учебных дисциплоин
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListDisciplineNames(int? selectedId = 0)
        {
            return new SelectList(_context.DisciplineNames
                .OrderBy(dn => dn.DisciplineNameName),
                "DisciplineNameId",
                "DisciplineNameName",
                selectedId);
        }

        /// <summary>
        /// Возвращает список дисциплин,
        /// содержащих заданную строку
        /// </summary>
        /// <param name="disciplineNameSearchString"></param>
        /// <returns></returns>
        public SelectList GetSelectListDisciplineNames(string disciplineNameSearchString="")
        {
            if (String.IsNullOrWhiteSpace(disciplineNameSearchString)) return null;
            var disciplineNames = _context.DisciplineNames
                .Where(n => n.DisciplineNameName.ToLower().Contains(disciplineNameSearchString.ToLower()))
                .ToList();
            if (disciplineNames.Count == 0) return null;

            return new SelectList(disciplineNames, "DisciplineNameId", "DisciplineNameName");
        }

        /// <summary>
        /// Возвращает список дисциплин
        /// </summary>        
        public SelectList GetSelectListDisciplines(int selectedId = 0)
        {
            return new SelectList(_context.DisciplineNames.OrderBy(d=>d.DisciplineNameName), "DisciplineNameId", "DisciplineNameName", selectedId);
        }

        /// <summary>
        /// Возвращает список форм обучения
        /// </summary>
        /// <returns></returns>
        public SelectList GetSelectListEduForms(int selectedId = 0)
        {
            return new SelectList(_context.EduForms, "EduFormId", "EduFormName", selectedId);
        }

        /// <summary>
        /// Возвращает список форм обучения, доступных абитуриенту
        /// при подаче заявления о приёме
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduFormsForAbiturient(int selectedId = 0)
        {
            var data = _context.EduForms.Where(f => f.EduFormId == 1 || f.EduFormId == 3);
            return new SelectList(data, "EduFormId", "EduFormName", selectedId);
        }

        /// <summary>
        /// Возвращает список форм обучения, доступных абитуриенту
        /// при подаче заявления о приёме на указанное направление подготовки
        /// </summary>
        /// <param name="EduNapravlId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduFormsForAbiturient(int EduNapravlId, int selectedId = 0)
        {
            var data = _context                
                .EduNapravlEduForms
                .Include(enef => enef.EduForm)
                .Where(enef => enef.EduNapravlId == EduNapravlId);
            return new SelectList(data, "EduFormId", "EduForm.EduFormName", selectedId);
        }

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых направлений подготовки
        /// </summary>
        /// <returns></returns>
        public SelectList GetSelectListEduNapravlFullNames(int selectedId = 0)
        {            
            return new SelectList(_context.EduNapravls.Include(n=>n.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName", selectedId);                       
        }

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых направлений подготовки
        /// указанного уровня образования
        /// </summary>
        /// <param name="eduLevelId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduNapravlFullNamesOfEduLevel(int? eduLevelId, int selectedId = 0)
        {
            var data = _context.EduNapravls
                .Include(n => n.EduUgs.EduLevel)
                .Where(n => n.EduUgs.EduLevelId == eduLevelId);

            return new SelectList(data, "EduNapravlId", "GetEduNapravlName", selectedId);
        }

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых профилей подготовки
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduProfileFullNames(int selectedId = 0)
        {
            return new SelectList(_context.EduProfiles.Include(n => n.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список наименований
        /// реализуемых профилей подготовки
        /// для указанного направления
        /// </summary>
        /// <param name="eduNapravlId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduProfilesOfEduNapravl(int? eduNapravlId, int selectedId = 0)
        {
            var data = _context.EduProfiles
                .Include(p => p.EduNapravl.EduUgs.EduLevel)
                .Where(p => p.EduNapravlId == eduNapravlId);

            if(eduNapravlId == 15)// Фильтрация профилей для приёмной комиссии
            {
                data = data.Where(n => n.EduProfileName.Contains("Электрооборудование") || n.EduProfileName.Contains("Экономика") || n.EduProfileName.Contains("Технические системы") );                
            }

            return new SelectList(data, "EduProfileId", "EduProfileName", selectedId);
        }

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых профилей подготовки, доступных методкомиссиям
        /// </summary>
        /// <param name="metodKomissii"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduProfileFullNamesOfMethodicalCommission(IEnumerable<MetodKomissiya> metodKomissii, int selectedId = 0)
        {
            var eduProfiles = new List<EduProfile>();

            foreach (var metodKomissiya in metodKomissii)
            {
                foreach (var metodKomissiyaEduProfile in metodKomissiya.MetodKomissiyaEduProfiles)
                {
                    eduProfiles.Add(metodKomissiyaEduProfile.EduProfile);
                }
            }

            return new SelectList(eduProfiles, "EduProfileId", "GetEduProfileFullName", selectedId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAppUsers(string selectedId)
        {
            return new SelectList(_context.Users.OrderBy(u => u.GetFullName), "Id", "GetFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список
        /// программ подготовки
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduProgramPodg(int selectedId)
        {
            return new SelectList(_context.EduProgramPodg, "EduProgramPodgId", "EduProgramPodgName", selectedId);
        }

        /// <summary>
        /// Возвращает список сроков обучения
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduSrok(int selectedId = 0)
        {
            return new SelectList(_context.EduSrok, "EduSrokId", "EduSrokName", selectedId);
        }

        /// <summary>
        /// Возвращает список методкомиссий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListMetodKomissiyaNames(int selectedId = 0)
        {
            return new SelectList(_context.MetodKomissii, "MetodKomissiyaId", "MetodKomissiyaName", selectedId);
        }

        /// <summary>
        /// Возвращает список помещений
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListPomesheniya(int selectedId = 0)
        {
            return new SelectList(_context.Pomeshenie.OrderBy(p=>p.PomeshenieName), "PomeshenieId", "PomeshenieName", selectedId);
        }

        /// <summary>
        /// Возвращает список кафедр
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListStructKaf(int selectedId = 0)
        {
            return new SelectList(_context.StructKafs.Include(s=>s.StructSubvision), "StructKafId", "StructSubvision.StructSubvisionName", selectedId);
        }

        /// <summary>
        /// Возвращает список ФИО преподавателей
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListTeacherFio(int selectedId = 0)
        {
            return new SelectList(_context.Teachers.OrderBy(t=>t.TeacherFio), "TeacherId", "TeacherFio", selectedId);
        }

        /// <summary>
        /// Возвращает список форм издания
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListUchPosobieFormaIzdaniya(int selectedId = 0)
        {
            return new SelectList(_context.UchPosobieFormaIzdaniya.OrderBy(f => f.UchPosobieFormaIzdaniyaName),
                "UchPosobieFormaIzdaniyaId", "UchPosobieFormaIzdaniyaName", selectedId);
        }

        /// <summary>
        /// Возвращает список видов учебных пособий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListUchPosobieVid(int selectedId = 0)
        {
            return new SelectList(_context.UchPosobieVid.OrderBy(t => t.UchPosobieVidName),
                "UchPosobieVidId", "UchPosobieVidName", selectedId);
        }

        /// <summary>
        /// Возвращает список структурных подразделений
        /// </summary>
        /// <param name="structSubvisionId"></param>
        /// <returns></returns>
        public SelectList GetSelectListStructSubvisions(int? structSubvisionId = null)
        {
            return new SelectList(_context.StructSubvisions.OrderBy(t => t.StructSubvisionName),
                "StructSubvisionId", "StructSubvisionName", structSubvisionId);
        }

        /// <summary>
        /// Возвращает список научных журналов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListScienceJournals(int selectedId = 0)
        {
            return new SelectList(_context.ScienceJournals.OrderBy(t => t.ScienceJournalName),
                "ScienceJournalId", "ScienceJournalName", selectedId);
        }

        /// <summary>
        /// Возвращает список специальностей научных работников согласно номенклатуре
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListNirSpecials(int selectedId = 0)
        {
            return new SelectList(_context.NirSpecials.OrderBy(t => t.NirSpecialCode),
                "NirSpecialId", "NirSpecialName", selectedId);
        }

        /// <summary>
        /// Возвращает список годов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListYears(int? selectedId = 0)
        {
            return new SelectList(_context.Years.OrderByDescending(y => y.YearName),
                "YearId", "YearName", selectedId);
        }

        /// <summary>
        /// Возвращает список тем НИР
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListNirTemas(int selectedId = 0)
        {
            return new SelectList(_context.NirTema.OrderBy(n => n.NirTemaName),
                "NirTemaId", "NirTemaName", selectedId);
        }

        /// <summary>
        /// Возвращает список видов патентов (свидетельств)
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListPatentVids(int selectedId = 0)
        {
            return new SelectList(_context.PatentVids.OrderBy(p => p.PatentVidName),
                "PatentVidId", "PatentVidName", selectedId);
        }

        /// <summary>
        /// Возвращает список баз цитирования
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListCitationBases(int selectedId = 0)
        {
            return new SelectList(_context.CitationBases.OrderBy(c => c.CitationBaseName),
                "CitationBaseId", "CitationBaseName", selectedId);
        }

        /// <summary>
        /// Возвращает список видов достижений пользователя
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListUserAchievmentTypes(int selectedId = 0)
        {
            return new SelectList(_context.UserAchievmentTypes.OrderBy(a => a.UserAchievmentTypeName),
                "UserAchievmentTypeId", "UserAchievmentTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список учебных годов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduYears(int selectedId = 0)
        {
            return new SelectList(_context.EduYears.OrderBy(ey => ey.EduYearName),
                 "EduYearId", "EduYearName", selectedId);
        }

        /// <summary>
        /// Возвращает список годов начала подготовки
        /// </summary>        
        public SelectList GetSelectListEduYearBeginningTrainings(int selectedId = 0)
        {
            return new SelectList(_context.EduYearBeginningTrainings.OrderBy(ey => ey.EduYearBeginningTrainingName),
                 "EduYearBeginningTrainingId", "EduYearBeginningTrainingName", selectedId);
        }

        /// <summary>
        /// Возвращает список учебных групп
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListStudentGroups(int selectedId = 0)
        {
            return new SelectList(_context.StudentGroups
                .Include(sg => sg.EduKurs)
                .OrderBy(sg => sg.StudentGroupName),
                 "StudentGroupId", "StudentGroupName", selectedId);
        }

        /// <summary>
        /// Возвращает список типов пользовательских сообщений
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListUserMessageTypes(int selectedId = 0)
        {
            return new SelectList(_context.UserMessageTypes,
                 "UserMessageTypeId", "UserMessageTypeName", selectedId);
        }

        public dynamic GetSelectListScienceJournals(object scienceJournalId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает список статусов пользователей
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAppUserStatuses(int selectedId = 0)
        {
            return new SelectList(_context.AppUserStatuses,
                 "AppUserStatusId", "AppUserStatusName", selectedId);
        }

        /// <summary>
        /// Возвращает список статусов абитуриентов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAbiturientStatuses(int selectedId = 0)
        {
            return new SelectList(_context.AbiturientStatuses,
                 "AbiturientStatusId", "AbiturientStatusName", selectedId);
        }

        /// <summary>
        /// Возвращает список статусов записей
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListRowStatuses(int selectedId = 0)
        {
            return new SelectList(_context.RowStatuses,
                 "RowStatusId", "RowStatusName", selectedId);
        }

        /// <summary>
        /// Возвращает список документов об образовании для абитуриентов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEducationDocumentsForAbiturients(int selectedId = (int)FileDataTypeEnum.AttestatOSrednemObshemObrazovanii)
        {
            var dataTypes = _context.FileDataTypes.Where(fdt=>fdt.FileDataTypeGroupId == (int)FileDataTypeGroupEnum.EducationDocuments);
            var dataTypesForAbiturients = dataTypes
                .Where(fdt => fdt.FileDataTypeId == (int)FileDataTypeEnum.AttestatObOsnovnomObshemObrazovanii ||
                              fdt.FileDataTypeId == (int)FileDataTypeEnum.AttestatOSrednemObshemObrazovanii ||
                              fdt.FileDataTypeId == (int)FileDataTypeEnum.DiplomSPO ||
                              fdt.FileDataTypeId == (int)FileDataTypeEnum.DiplomVO)
                .OrderBy(fdt => fdt.FileDataTypeName);

            return new SelectList(dataTypesForAbiturients,
                 "FileDataTypeId", "FileDataTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список квалификаций пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListUserQualifications(string userName, int selectedId = 0)
        {
            var userQualifications = _userProfileRepository.GetQualifications(userName).ToList();
            return new SelectList(userQualifications,
                 "QualificationId", "QualificationFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список учебных заведений
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEducationalInstitutions(int selectedId = 0)
        {
            var educationalInstitutions = _educationalInstitutionRepository.GetEducationalInstitutions().ToList();
            return new SelectList(educationalInstitutions,
                 "EducationalInstitutionId", "GetEducationalInstitutionNameAndSettlement", selectedId);
        }

        /// <summary>
        /// Возвращает список населённых пунктов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListPopulatedLocalities(int selectedId = 0)
        {
            var populatedLocalities = _populatedLocalityRepository.GetPopulatedLocalities()
                .OrderBy(pl => pl.District.Region.Country.CountryName)
                .ThenBy(pl => pl.District.Region.RegionName)
                .ThenBy(pl => pl.District.DistrictName)
                .ThenBy(pl => pl.PopulatedLocalityName)
                .ToList();
            return new SelectList(populatedLocalities,
                 "PopulatedLocalityId", "GetPopulatedLocalityFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список типов квот набора
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListQuotaTypes(int selectedId = 0)
        {
            var data = _context.QuotaTypes.OrderBy(q => q.QuotaTypeName);
            return new SelectList(data,
                 "QuotaTypeId", "QuotaTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список типов квот набора для указанного направления подготовки
        /// </summary>
        /// <param name="eduNapravlId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListQuotaTypes(int eduNapravlId, int selectedId = 0)
        {
            var data = _context.EduNapravlQuotaTypes
                .Include(q => q.QuotaType)
                .Where(q => q.EduNapravlId == eduNapravlId)
                .OrderBy(q => q.QuotaType.QuotaTypeName);
            return new SelectList(data,
                 "QuotaTypeId", "QuotaType.QuotaTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список уровней образования
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduLevels(int selectedId = 0)
        {
            var data = _context.EduLevels.OrderBy(l => l.EduLevelId);
            return new SelectList(data,
                 "EduLevelId", "EduLevelName", selectedId);
        }

        /// <summary>
        /// Возвращает список индивидуальных достижений абитуриента
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAbiturientIndividualAchievmentTypes(int selectedId = 0)
        {
            var data = _context.AbiturientIndividualAchievmentTypes.OrderBy(t => t.AbiturientIndividualAchievmentTypeName);
            return new SelectList(data,
                 "AbiturientIndividualAchievmentTypeId", "AbiturientIndividualAchievmentTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список полов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListGenders(int selectedId = 0)
        {
            var data = _context.Genders;
            return new SelectList(data,
                 "GenderId", "GenderName", selectedId);
        }

        /// <summary>
        /// Возвращает список типов отношений к военной службе
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListMilitaryServiceStatuses(int selectedId = 0)
        {
            var data = _context.MilitaryServiceStatuses.OrderByDescending(m=>m.MilitaryServiceStatusName);
            return new SelectList(data,
                 "MilitaryServiceStatusId", "MilitaryServiceStatusName", selectedId);
        }

        /// <summary>
        /// Возвращает список иностранных языков
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListForeignLanguages(int selectedId = 0)
        {
            var data = _context.ForeignLanguages.OrderBy(m => m.ForeignLanguageName);
            return new SelectList(data,
                 "ForeignLanguageId", "ForeignLanguageName", selectedId);
        }

        /// <summary>
        /// Возвращает список типов родственных связей
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListFamilyMemberTypes(int selectedId = 0)
        {
            var data = _context.FamilyMemberTypes;
            return new SelectList(data,
                 "FamilyMemberTypeId", "FamilyMemberTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список типов привилегий абитуриента при приёме
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAdmissionPrivilegeTypes(int selectedId = 0)
        {
            var data = _context.AdmissionPrivilegeTypes;
            return new SelectList(data,
                 "AdmissionPrivilegeTypeId", "AdmissionPrivilegeTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список заявлений о приёме
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListApplicationForAdmissions(int selectedId = 0)
        {
            var data = _context.ApplicationForAdmissions
                .Include(a => a.Abiturient.AppUser)
                .Include(a => a.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(a => a.EduForm)
                .Include(a => a.QuotaType)
                .OrderBy(a => a.Abiturient.AppUser.GetFullName)
                .ThenBy(a => a.EduProfile.GetEduProfileFullName);
            return new SelectList(data,
                 "ApplicationForAdmissionId", "ApplicationForAdmissionFullNameWithAppUserInfo", selectedId);
        }

        /// <summary>
        /// Возвращает список заявлений о приёме абитуриента
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListApplicationForAdmissions(int abiturientId, int selectedId = 0)
        {
            var data = _context.ApplicationForAdmissions
                .Include(a => a.Abiturient.AppUser)
                .Include(a => a.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(a => a.EduForm)
                .Include(a => a.QuotaType)
                .OrderBy(a => a.Abiturient.AppUser.GetFullName)
                .ThenBy(a => a.EduProfile.GetEduProfileFullName)
                .Where(a => a.AbiturientId == abiturientId);
            return new SelectList(data,
                 "ApplicationForAdmissionId", "ApplicationForAdmissionFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список типов документов, загружаемых абитуриентами,
        /// доступных для фильтрации приёмной комиссией при проверке
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAbiturientsUserDocumentTypes(int selectedId = 0)
        {
            var data = _context.FileDataTypes
                .Where(fdt => fdt.FileDataTypeGroupId == (int)FileDataTypeGroupEnum.UserDocuments ||
                    fdt.FileDataTypeId == (int)FileDataTypeEnum.AbiturientCard ||
                    fdt.FileDataTypeId == (int) FileDataTypeEnum.AttestatObOsnovnomObshemObrazovanii ||
                    fdt.FileDataTypeId == (int)FileDataTypeEnum.AttestatOSrednemObshemObrazovanii ||
                    fdt.FileDataTypeId == (int)FileDataTypeEnum.DiplomSPO ||
                    fdt.FileDataTypeId == (int)FileDataTypeEnum.DiplomVO)
                .OrderBy(fdt => fdt.FileDataTypeName);
            return new SelectList(data,
                 "FileDataTypeId", "FileDataTypeName", selectedId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListFileDataTypes(int selectedId = 0)
        {
            var data = _context.FileDataTypes.OrderBy(fdt => fdt.FileDataTypeName);
            return new SelectList(data,
                 "FileDataTypeId", "FileDataTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список типов мероприятий СДО
        /// для указанной группы типов мероприятий СДО
        /// </summary>
        /// <param name="lmsEventTypeGroupId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListLmsEventTypes(int lmsEventTypeGroupId, int selectedId = 0)
        {
            var data = _context.LmsEventTypes
                .Where(t => t.LmsEventTypeGroupId == lmsEventTypeGroupId)
                .OrderBy(t => t.LmsEventTypeName);

            return new SelectList(data,
                 "LmsEventTypeId", "LmsEventTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список типов событий СДО
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListLmsEventTypes(int selectedId = 0)
        {
            var data = _context.LmsEventTypes
                .Include(t => t.LmsEventTypeGroup)
                .OrderBy(t => t.LmsEventTypeFullName);

            return new SelectList(data,
                 "LmsEventTypeId", "LmsEventTypeFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список абитуриентов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAppUsersAbiturientsConfirmed(string selectedId = "")
        {
            var data = _context.Abiturients
                .Include(a => a.AppUser)
                //.Where(a => a.AbiturientStatusId == (int) AbiturientStatusEnum.ConfirmedAbiturient)
                .OrderBy(a => a.AbiturientFioBirthdayEmail);

            return new SelectList(data,
                 "AppUser.Id", "AbiturientFioBirthdayEmail", selectedId);
        }

        /// <summary>
        /// Возвращает список подтверждённых абитуриентов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAbiturientsConfirmed(int selectedId = 0)
        {
            var data = _context.Abiturients
                .Include(a => a.AppUser)
                .Where(a => a.AbiturientStatusId == (int)AbiturientStatusEnum.ConfirmedAbiturient);

            return new SelectList(data,
                 "AbiturientId", "AbiturientFioBirthdayEmail", selectedId);
        }

        /// <summary>
        /// Возвращает список абитуриентов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAbiturients(int selectedId = 0)
        {
            var data = _context.Abiturients
                .Include(a => a.AppUser)
                .OrderBy(a => a.AppUser.GetFullName);

            return new SelectList(data,
                 "AbiturientId", "AbiturientFioBirthdayEmail", selectedId);
        }

        /// <summary>
        /// Возвращает список типов договоров
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListContractTypes(int selectedId = 0)
        {
            var data = _context.ContractTypes;

            return new SelectList(data,
                 "ContractTypeId", "ContractTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список пользователей, являющихся авторами
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListAppUsersAuthors(string selectedId = "")
        {
            var data = _context.Author
                .Include(a => a.AppUser)
                .Where(a => a.AppUserId != null);

            return new SelectList(data,
                 "AppUserId", "AppUser.GetFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список типов заданий СДО
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListLmsTaskTypes(int selectedId = 0)
        {
            var data = _context.LmsTaskTypes;

            return new SelectList(data,
                 "LmsTaskTypeId", "LmsTaskTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список наборов заданий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListLmsTaskSets(int selectedId = 0)
        {
            var data = _context.LmsTaskSets.OrderBy(ts => ts.LmsTaskSetDescription);

            return new SelectList(data,
                 "LmsTaskSetId", "LmsTaskSetDescription", selectedId);
        }

        /// <summary>
        /// Возвращает список мероприятий указанного типа
        /// </summary>
        /// <param name="lmsEventTypeId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListLmsEvents(int lmsEventTypeId, int selectedId = 0)
        {
            var data = _context.LmsEvents
                .Where(e => e.LmsEventTypeId == lmsEventTypeId)
                .OrderByDescending(e => e.DateTimeStart);

            return new SelectList(data,
                 "LmsEventId", "GetFullDescription", selectedId);
        }

        /// <summary>
        /// Возвращает список пользователей-участников мероприятия
        /// </summary>
        /// <param name="lmsEventId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListLmsEventParticipants(int lmsEventId, int selectedId = 0)
        {
            var data = _context.AppUserLmsEvents
                .Include(e => e.AppUser)
                .Where(e => e.LmsEventId == lmsEventId)
                .OrderBy(e => e.AppUser.GetFullName);

            return new SelectList(data,
                 "AppUser.UserName", "AppUser.GetFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список номеров групп абитуриентов для прохождения вступительных испытаний
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEntranceTestGroups(int selectedId = 0)
        {
            var maxGroupId = _context.Abiturients.Max(a => a.EntranceTestGroupId);

            if(maxGroupId == null)
            {
                return null;
            }
            else
            {
                return new SelectList(Enumerable.Range(1, (int)maxGroupId), selectedId);
            }
            
        }

        /// <summary>
        /// Возвращает список пользователей, назначаемых абитуриентам консультантами
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public async Task<SelectList> GetSelectListAppUserAbiturientConsultantsAsync(int selectedId = 0)
        {            
            var u1 = await _userManager.GetUsersInRoleAsync("Приёмная комиссия");
            var u2 = await _userManager.GetUsersInRoleAsync("Приёмная комиссия (консультанты)");
            var data = new List<AppUser>(u1);

            foreach (var appUser in u2)
            {
                if (!data.Contains(appUser))
                    data.Add(appUser);
            }
            
            return new SelectList(data.OrderBy(u => u.GetFullName),
                 "Id", "GetFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список номеров приоритетов заявлений о приёме
        /// </summary>
        /// <param name="priorityId"></param>
        /// <returns></returns>
        public SelectList GetSelectListPriorities(int selectedId = 0)
        {
            var priorityId = _context.ApplicationForAdmissions.Max(a => a.PriorityId);

            if (priorityId == 0)
            {
                return null;
            }
            else
            {
                return new SelectList(Enumerable.Range(1, priorityId), selectedId);
            }
        }

        /// <summary>
        /// Возвращает список способов подачи документов о приёме абитуриентами
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSubmittingDocumentsTypes(int? selectedId = 0)
        {
            var data = _context.SubmittingDocumentsTypes;

            return new SelectList(data,
                 "SubmittingDocumentsTypeId", "SubmittingDocumentsTypeName", selectedId);
        }

        /// <summary>
        /// Возвращает список пользователей, являющихся авторами заданий СДО
        /// </summary>
        /// <param name="selectedAppUserId"></param>
        /// <returns></returns>
        public SelectList GetSelectListLmsTaskAppUsers(string selectedAppUserId = "")
        {
            var data = _context.LmsTasks
                .Include(l=>l.AppUser)
                .Select(l => new { l.AppUserId, l.AppUser})
                .Distinct();

            return new SelectList(data,
                 "AppUserId", "AppUser.GetFullName", selectedAppUserId);
        }

        /// <summary>
        /// Возвращает список УИД дисциплин, по которым имеются задания СДО
        /// </summary>
        /// <param name="filterDisciplineNameId"></param>
        /// <returns></returns>
        public SelectList GetSelectListLmsTaskDisciplineNames(int filterDisciplineNameId = 0)
        {
            var data = _context.LmsTaskDisciplineNames
                .Include(l => l.DisciplineName)
                .Select(l => new { l.DisciplineNameId, l.DisciplineName.DisciplineNameName })
                .Distinct();

            return new SelectList(data,
                 "DisciplineNameId", "DisciplineNameName", filterDisciplineNameId);
        }
    }
}
