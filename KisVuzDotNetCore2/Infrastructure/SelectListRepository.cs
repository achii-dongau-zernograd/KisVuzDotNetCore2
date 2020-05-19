﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Struct;
using KisVuzDotNetCore2.Models.Users;
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

        /// <summary>
        /// Внедрение зависимостей
        /// </summary>
        /// <param name="context"></param>
        public SelectListRepository(AppIdentityDBContext context,
            IUserProfileRepository userProfileRepository,
            IEducationalInstitutionRepository educationalInstitutionRepository,
            IPopulatedLocalityRepository populatedLocalityRepository)
        {
            _context = context;
            _userProfileRepository = userProfileRepository;
            _educationalInstitutionRepository = educationalInstitutionRepository;
            _populatedLocalityRepository = populatedLocalityRepository;
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
                .Where(fdt => fdt.FileDataTypeId == (int)FileDataTypeEnum.AttestatOSrednemObshemObrazovanii ||
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
    }
}
