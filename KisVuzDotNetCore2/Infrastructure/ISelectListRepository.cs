using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Gradebook;
using KisVuzDotNetCore2.Models.LMS;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Интерфейс для работы со списками (справочниками)
    /// </summary>
    public interface ISelectListRepository
    {
        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых направлений подготовки
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListEduNapravlFullNames(int selectedId = 0);

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых направлений подготовки
        /// указанного уровня образования
        /// </summary>
        /// <param name="eduLevelId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduNapravlFullNamesOfEduLevel(int? eduLevelId, int selectedId = 0);

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых направлений подготовки
        /// указанного уровня образования
        /// за исключением 23.02.03 и 23.03.01
        /// </summary>
        /// <param name="eduLevelId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduNapravlFullNamesOfEduLevelWithout230203and230301(int? eduLevelId, int selectedId = 0);

        /// <summary>
        /// Возвращает список типов
        /// внешних ресурсов
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListExternalResourceTypes(int selectedId = 0);

        /// <summary>
        /// Возвращает список внешних ресурсов
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListExternalResources(int selectedId = 0);

        /// <summary>
        /// Возвращает список типов заданий СДО
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListLmsTaskTypes(int selectedId = 0);

        /// <summary>
        /// Возвращает список УИД дисциплин, по которым имеются задания СДО
        /// </summary>
        /// <param name="filterDisciplineNameId"></param>
        /// <returns></returns>
        SelectList GetSelectListLmsTaskDisciplineNames(int filterDisciplineNameId = 0);

        /// <summary>
        /// Возвращает список пользователей, являющихся авторами заданий СДО
        /// </summary>
        /// <param name="selectedAppUserId"></param>
        /// <returns></returns>
        SelectList GetSelectListLmsTaskAppUsers(string selectedAppUserId = "");

        /// <summary>
        /// Возвращает список пользователей, являющихся авторами
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAppUsersAuthors(string selectedId = "");

        /// <summary>
        /// Возвращает список дат, в которые проходили вступительные испытания
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <returns></returns>
        SelectList GetSelectListEntranceTestRegistrationFormDates(DateTime? selectedDate = null);

        /// <summary>
        /// Возвращает список наименований дисциплин, по которым проходили вступительные испытания
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEntranceTestRegistrationFormDisciplineNames(string selectedId = "");

        /// <summary>
        /// Возвращает список номеров групп абитуриентов для прохождения вступительных испытаний
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEntranceTestGroups(int selectedId = 0);
        
        /// <summary>
        /// Возвращает список номеров приоритетов заявлений о приёме
        /// </summary>
        /// <param name="priorityId"></param>
        /// <returns></returns>
        SelectList GetSelectListPriorities(int priorityId = 0);

        /// <summary>
        /// Возвращает список типов договоров
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListContractTypes(int selectedId = 0);
        
        /// <summary>
        /// Возвращает список типов документов, загружаемых абитуриентами,
        /// доступных для фильтрации приёмной комиссией при проверке
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAbiturientsUserDocumentTypes(int selectedId = 0);
        
        /// <summary>
        /// Возвращает список мероприятий указанного типа
        /// </summary>
        /// <param name="lmsEventTypeId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListLmsEvents(int lmsEventTypeId, int selectedId = 0);

        /// <summary>
        /// Возвращает список подтверждённых абитуриентов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAbiturientsConfirmed(int selectedId = 0);

        /// <summary>
        /// Возвращает список абитуриентов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAbiturients(int selectedId = 0);

        /// <summary>
        /// Возвращает список типов событий СДО
        /// для указанной группы типов событий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListLmsEventTypes(int lmsEventTypeGroupId, int selectedId = 0);
        
        /// <summary>
        /// Возвращает список пользователей-участников мероприятия
        /// </summary>
        /// <param name="lmsEventId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListLmsEventParticipants(int lmsEventId, int selectedId = 0);

        /// <summary>
        /// Возвращает список типов событий СДО
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListLmsEventTypes(int selectedId = 0);

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых профилей подготовки
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduProfileFullNames(int selectedId = 0);

        /// <summary>
        /// Возвращает список наименований
        /// реализуемых профилей подготовки
        /// для указанного направления
        /// </summary>
        /// <param name="eduNapravlId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduProfilesOfEduNapravl(int? eduNapravlId, int selectedId = 0);
        
        /// <summary>
        /// Возвращает список вариантов ответов для указанного задания СДО
        /// </summary>
        /// <param name="lmsTaskId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListLmsTaskAnswers(int lmsTaskId, int selectedId = 0);

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых профилей подготовки, доступных методкомиссиям
        /// </summary>
        /// <param name="metodKomissii">Перечисление методкомиссий</param>
        /// <param name="selectedId">УИД выбранного объекта</param>
        /// <returns></returns>
        SelectList GetSelectListEduProfileFullNamesOfMethodicalCommission(IEnumerable<MetodKomissiya> metodKomissii, int selectedId = 0);
        
        /// <summary>
        /// Возвращает список типов файлов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListFileDataTypes(int selectedId = 0);

        /// <summary>
        /// Возвращает список методкомиссий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListMetodKomissiyaNames(int selectedId = 0);
                       
        /// <summary>
        /// Возвращает список учебных групп
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListStudentGroups(int selectedId = 0);
        
        /// <summary>
        /// Возвращает список пользователей, являющихся подтверждёнными абитуриентов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAppUsersAbiturientsConfirmed(string selectedId = "");

        /// <summary>
        /// Возвращает список статусов пользователей
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListAppUserStatuses(int selectedId = 0);

        /// <summary>
        /// Возвращает список пользователей, назначаемых абитуриентам консультантами
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        Task<SelectList> GetSelectListAppUserAbiturientConsultantsAsync(int selectedId = 0);

        /// <summary>
        /// Возвращает список статусов абитуриентов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAbiturientStatuses(int selectedId = 0);
        
        /// <summary>
        /// Возвращает список наборов заданий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListLmsTaskSets(int selectedId = 0);

        /// <summary>
        /// Возвращает список баз цитирования
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListCitationBases(int selectedId = 0);
                
        /// <summary>
        /// Возвращает список ФИО преподавателей
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListTeacherFio(int selectedId = 0);
        
        /// <summary>
        /// Возвращает список типов пользовательских сообщений
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListUserMessageTypes(int selectedId = 0);

        /// <summary>
        /// Возвращает список форм издания
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListUchPosobieFormaIzdaniya(int selectedId = 0);

        /// <summary>
        /// Возвращает список специальностей научных работников согласно номенклатуре
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListNirSpecials(int selectedId = 0);

        /// <summary>
        /// Возвращает список статусов записей
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListRowStatuses(int selectedId = 0);

        /// <summary>
        /// Возвращает список способов подачи документов о приёме абитуриентами
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSubmittingDocumentsTypes(int? selectedId = 0);

        /// <summary>
        /// Возвращает список научных журналов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListScienceJournals(int selectedId = 0);

        /// <summary>
        /// Возвращает список тем НИР
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListNirTemas(int selectedId = 0);
        
        /// <summary>
        /// Возвращает список полов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListGenders(int selectedId = 0);
        
        /// <summary>
        /// Возвращает список типов отношений к военной службе
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListMilitaryServiceStatuses(int selectedId = 0);

        /// <summary>
        /// Возвращает список видов достижений пользователя
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListUserAchievmentTypes(int selectedId = 0);

        /// <summary>
        /// Возвращает список индивидуальных достижений абитуриента
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAbiturientIndividualAchievmentTypes(int selectedId = 0);

        /// <summary>
        /// Возвращает список годов
        /// </summary>
        /// <param name="yearId"></param>
        /// <returns></returns>
        SelectList GetSelectListYears(int? selectedId = 0);

        /// <summary>
        /// Возвращает список документов об образовании для абитуриентов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEducationDocumentsForAbiturients(int selectedId = (int)FileDataTypeEnum.AttestatOSrednemObshemObrazovanii);
        
        /// <summary>
        /// Возвращает список населённых пунктов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListPopulatedLocalities(int selectedId = 0);

        /// <summary>
        /// Возвращает список уровней образования
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduLevels(int selectedId = 0);

        /// <summary>
        /// Возвращает список иностранных языков
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListForeignLanguages(int selectedId = 0);

        /// <summary>
        /// Возвращает список видов учебных пособий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListUchPosobieVid(int selectedId = 0);

        /// <summary>
        /// Возвращает список типов родственных связей
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListFamilyMemberTypes(int selectedId = 0);

        /// <summary>
        /// Возвращает список учебных заведений
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEducationalInstitutions(int selectedId = 0);
        
        /// <summary>
        /// Возвращает список типов квот набора
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListQuotaTypes(int selectedId = 0);
        
        /// <summary>
        /// Возвращает список типов квот набора для указанного направления подготовки
        /// </summary>
        /// <param name="eduNapravlId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListQuotaTypes(int eduNapravlId, int selectedId = 0);

        /// <summary>
        /// Возвращает список квалификаций пользователя
        /// </summary>        
        SelectList GetSelectListUserQualifications(string userName, int selectedId = 0);

        /// <summary>
        /// Возвращает список форм обучения
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduForms(int selectedId = 0);

        /// <summary>
        /// Возвращает список форм обучения, доступных абитуриенту
        /// при подаче заявления о приёме
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduFormsForAbiturient(int selectedId = 0);

        /// <summary>
        /// Возвращает список форм обучения, доступных абитуриенту
        /// при подаче заявления о приёме на указанное направление
        /// </summary>
        /// <param name="EduNapravlId"></param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduFormsForAbiturient(int EduNapravlId, int selectedId = 0);

        /// <summary>
        /// Возвращает список программ подготовки
        /// </summary>
        /// <param name="eduProgramPodgId"></param>
        /// <returns></returns>
        SelectList GetSelectListEduProgramPodg(int selectedId = 0);

        /// <summary>
        /// Возвращает список сроков обучения
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListEduSrok(int selectedId = 0);

        /// <summary>
        /// Возвращает список структурных подразделений
        /// </summary>
        /// <param name="structSubvisionId"></param>
        /// <returns></returns>
        SelectList GetSelectListStructSubvisions(int? structSubvisionId = null);

        /// <summary>
        /// Возвращает список кафедр
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListStructKaf(int selectedId = 0);

        /// <summary>
        /// Возвращает полный список дисциплин
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListDisciplineNames(int? selectedId = 0);

        /// <summary>
        /// Возвращает список дисциплин,
        /// содержащих заданную строку
        /// </summary>
        /// <param name="disciplineNameSearchString"></param>
        /// <returns></returns>
        SelectList GetSelectListDisciplineNames(string disciplineNameSearchString);
        
        /// <summary>
        /// Возвращает список помещений
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListPomesheniya(int selectedId = 0);

        /// <summary>
        /// Возвращает список авторов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAuthors(int selectedId = 0);

        /// <summary>
        /// Возвращает список типов посещаемости учебных занятий (П, Н, Б, У)
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAttendanceTypes(int selectedId = 0);

        /// <summary>
        /// Возвращает список авторов
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAuthors(string authorFilter);

        /// <summary>
        /// Возвращает список пользователей по строке ФИО
        /// </summary>
        /// <param name="fio"></param>
        /// <returns></returns>
        SelectList GetSelectListAppUsersByFirstName(string fio);

        /// <summary>
        /// Возвращает список аккаунтов
        /// </summary>
        /// <returns></returns>
        SelectList GetSelectListAppUsers(string selectedId = null);

        /// <summary>
        /// Возвращает список ФИО преподавателей, УИД - AppUserId
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAppUsersTeachers(string selectedId = "");

        /// <summary>
        /// Возвращает список дисциплин
        /// </summary>
        SelectList GetSelectListDisciplines(int selectedId = 0);

        /// <summary>
        /// Возвращает список видов патентов (свидетельств)
        /// </summary>        
        SelectList GetSelectListPatentVids(int selectedId = 0);

        /// <summary>
        /// Возвращает список учебных годов
        /// </summary>        
        SelectList GetSelectListEduYears(int selectedId = 0);

        /// <summary>
        /// Возвращает список учебных годов, которые есть в списке
        /// </summary>
        /// <param name="eduYearId"></param>
        /// <param name="eduYearsList"></param>
        /// <returns></returns>
        SelectList GetSelectListEduYearsFromStringList(int selectedId, List<string> eduYearsList);

        /// <summary>
        /// Возвращает список годов начала подготовки
        /// </summary>        
        SelectList GetSelectListEduYearBeginningTrainings(int selectedId = 0);

        // Возвращает список годов начала подготовки, начиная с указанного
        SelectList GetSelectListEduYearBeginningTrainingsFrom(int selectedId, int eduYearBeginningTrainingStart);

        dynamic GetSelectListScienceJournals(object scienceJournalId);
        
        /// <summary>
        /// Возвращает список типов привилегий абитуриента при приёме
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListAdmissionPrivilegeTypes(int selectedId = 0);

        /// <summary>
        /// Возвращает список заявлений о приёме абитуриента
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListApplicationForAdmissions(int abiturientId, int selectedId = 0);

        /// <summary>
        /// Возвращает список заявлений о приёме
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListApplicationForAdmissions(int selectedId = 0);

        /// <summary>
        /// Возвращает список типов занятий (для эл. журналов)
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        SelectList GetSelectListElGradebookLessonTypes(int selectedId = 0);
        
    }
}
