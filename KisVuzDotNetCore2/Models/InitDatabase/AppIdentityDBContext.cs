﻿using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.InitDatabase;
using KisVuzDotNetCore2.Models.Priem;
using KisVuzDotNetCore2.Models.Struct;
using KisVuzDotNetCore2.Models.Sveden;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.UchPosobiya;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Students;
using KisVuzDotNetCore2.Models.Nir;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.LMS;
using KisVuzDotNetCore2.Models.Gradebook;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Класс контекста БД
    /// </summary>
    public class AppIdentityDBContext : IdentityDbContext<AppUser>
    {
        #region Конструктор
        public AppIdentityDBContext(DbContextOptions<AppIdentityDBContext> options) : base(options)
        {

        }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TextBlock>()
                .HasIndex(e=>e.TextBlockTag);
            base.OnModelCreating(builder);
        }

        #region Таблицы

        #region Электронные журнылы
        /// <summary>
        /// Электронные журнылы
        /// </summary>
        public DbSet<ElGradebook> ElGradebooks { get; set; }

        /// <summary>
        /// Электронные журнылы. Преподаватели, заполняющие электронные журнылы
        /// </summary>
        public DbSet<ElGradebookTeacher> ElGradebookTeachers { get; set; }

        /// <summary>
        /// Электронные журнылы. Студенты группы
        /// </summary>
        public DbSet<ElGradebookGroupStudent> ElGradebookGroupStudents { get; set; }

        /// <summary>
        /// Электронные журнылы. Учебные занятия
        /// </summary>
        public DbSet<ElGradebookLesson> ElGradebookLessons { get; set; }

        /// <summary>
        /// Электронные журнылы. Типы учебных занятий
        /// </summary>
        public DbSet<ElGradebookLessonType> ElGradebookLessonTypes { get; set; }

        /// <summary>
        /// Электронные журнылы. Оценки за учебные занятия
        /// </summary>
        public DbSet<ElGradebookLessonMark> ElGradebookLessonMarks { get; set; }

        /// <summary>
        /// Электронные журнылы. Типы посещаемости учебных занятий
        /// </summary>
        public DbSet<ElGradebookLessonAttendanceType> ElGradebookLessonAttendanceTypes { get; set; }
        #endregion

        #region Общие (Common)
        public DbSet<AppSetting> AppSettings { get; set; }

        /// <summary>
        /// Справочник годов
        /// </summary>
        public DbSet<Year> Years { get; set; }

        /// <summary>
        /// Формы занятости (основное, внутреннее совместительство и пр.)
        /// </summary>
        public DbSet<EmploymentForm> EmploymentForms { get; set; }

        /// <summary>
        /// Типы пользовательских сообщений
        /// </summary>
        public DbSet<UserMessageType> UserMessageTypes { get; set; }

        /// <summary>
        /// Паспортные данные
        /// </summary>
        public DbSet<PassportData> PassportDataSet { get; set; }

        /// <summary>
        /// Договоры
        /// </summary>
        public DbSet<Contract> Contracts { get; set; }

        /// <summary>
        /// Типы договоров
        /// </summary>
        public DbSet<ContractType> ContractTypes { get; set; }

        /// <summary>
        /// Оплаты
        /// </summary>
        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Текстовые блоки на веб-странице
        /// </summary>
        public DbSet<TextBlock> TextBlocks { get; set; }
        #endregion

        #region Система дистанционного образования (СДО, LMS)
        /// <summary>
        /// Группы типов событий СДО
        /// </summary>
        public DbSet<LmsEventTypeGroup> LmsEventTypeGroups { get; set; }

        /// <summary>
        /// Типы событий СДО
        /// </summary>
        public DbSet<LmsEventType> LmsEventTypes { get; set; }

        /// <summary>
        /// События СДО (экзамены, вебинары и пр.)
        /// </summary>
        public DbSet<LmsEvent> LmsEvents { get; set; }

        /// <summary>
        /// Задания СДО
        /// </summary>
        public DbSet<LmsTask> LmsTasks { get; set; }

        /// <summary>
        /// Типы заданий СДО
        /// </summary>
        public DbSet<LmsTaskType> LmsTaskTypes { get; set; }

        /// <summary>
        /// Сопоставления заданий СДО с наименованиями дисциплин
        /// </summary>
        public DbSet<LmsTaskDisciplineName> LmsTaskDisciplineNames { get; set; }

        /// <summary>
        /// Варианты ответов на задания СДО
        /// </summary>
        public DbSet<LmsTaskAnswer> LmsTaskAnswers { get; set; }

        /// <summary>
        /// Наборы заданий СДО
        /// </summary>
        public DbSet<LmsTaskSet> LmsTaskSets { get; set; }

        /// <summary>
        /// Сопоставления наборов заданий СДО с заданиями СДО
        /// </summary>
        public DbSet<LmsTaskSetLmsTask> LmsTaskSetLmsTasks { get; set; }

        /// <summary>
        /// Сопоставления наборов заданий СДО с событиями СДО
        /// </summary>
        public DbSet<LmsEventLmsTaskSet> LmsEventLmsTaskSets { get; set; }

        /// <summary>
        /// Пользователи, участвующие в мероприятиях СДО
        /// </summary>
        public DbSet<AppUserLmsEvent> AppUserLmsEvents { get; set; }

        /// <summary>
        /// Справочник ролей пользователей, участвующих в мероприятиях СДО
        /// </summary>
        public DbSet<AppUserLmsEventUserRole> AppUserLmsEventUserRoles { get; set; }

        /// <summary>
        /// Дополнительные материалы к мероприятиям СДО
        /// </summary>
        public DbSet<LmsEventAdditionalMaterial> LmsEventAdditionalMaterials { get; set; }

        /// <summary>
        /// Чат мероприятия СДО
        /// </summary>
        public DbSet<LmsEventChatMessage> LmsEventChatMessages { get; set; }

        /// <summary>
        /// Ответы пользователей на задания СДО
        /// </summary>
        public DbSet<LmsEventLmsTasksetAppUserAnswer> LmsEventLmsTasksetsAppUserAnswers { get; set; }

        /// <summary>
        /// Ответы пользователей на задания СДО, выбранные из предложенного списка ответов
        /// </summary>
        public DbSet<LmsEventLmsTasksetAppUserAnswerTaskAnswer> LmsEventLmsTasksetAppUserAnswerTaskAnswers { get; set; }
        #endregion

        #region Образовательная деятельность (Education)
        /// <summary>
        /// Обобщенные уровни образования
        /// </summary>
        public DbSet<EduLevelGroup> EduLevelGroups { get; set; }

        /// <summary>
        /// Уровни образования
        /// </summary>
        public DbSet<EduLevel> EduLevels { get; set; }

        /// <summary>
        /// Укрупнённые группы специальностей
        /// </summary>
        public DbSet<EduUgs> EduUgses { get; set; }

        /// <summary>
        /// Направления подготовки / специальности
        /// </summary>
        public DbSet<EduNapravl> EduNapravls { get; set; }

        /// <summary>
        /// Таблица, связывающая направление подготовки, форму обучения и нормативный срок обучения
        /// </summary>
        public DbSet<EduNapravlEduFormEduSrok> EduNapravlEduFormEduSroks { get; set; }

        /// <summary>
        /// Профили подготовки (направленности, специализации)
        /// </summary>
        public DbSet<EduProfile> EduProfiles { get; set; }

        /// <summary>
        /// Учебные планы
        /// </summary>
        public DbSet<EduPlan> EduPlans { get; set; }

        /// <summary>
        /// Учебные годы
        /// </summary>
        public DbSet<EduYear> EduYears { get; set; }

        /// <summary>
        /// Годы начала подготовки
        /// </summary>
        public DbSet<EduYearBeginningTraining> EduYearBeginningTrainings { get; set; }

        /// <summary>
        /// Формы обучения
        /// </summary>
        public DbSet<EduForm> EduForms { get; set; }

        /// <summary>
        /// Свидетельства о государственной аккредитации
        /// </summary>
        public DbSet<EduAccred> EduAccreds { get; set; }

        /// <summary>
        /// Численность обучающихся по реализуемым образовательным программам
        /// </summary>
        public DbSet<EduChislen> EduChislens { get; set; }

        /// <summary>
        /// Курсы
        /// </summary>
        public DbSet<EduKurs> EduKurses { get; set; }

        /// <summary>
        /// Справочник программ подготовки (академ. бакалавриат и т.д.)
        /// </summary>        
        public DbSet<EduProgramPodg> EduProgramPodg { get; set; }

        /// <summary>
        /// Виды учебной работы
        /// </summary>
        public DbSet<VidUchebRabotiName> VidUchebRabotiNames { get; set; }

        /// <summary>
        /// Виды деятельности по учебному плану
        /// </summary>
        public DbSet<EduVidDeyat> EduVidDeyat { get; set; }

        /// <summary>
        /// Блок дисциплин Учебного плана
        /// </summary>
        public DbSet<BlokDisciplName> BlokDisciplName { get; set; }

        /// <summary>
        /// Наименование части блока дисциплин Учебного плана
        /// Таблица BlokDisciplChastName
        /// </summary>
        public DbSet<BlokDisciplChastName> BlokDisciplChastName { get; set; }

        /// <summary>
        /// Срок обучения
        /// Таблица EduSrok
        /// </summary>
        public DbSet<EduSrok> EduSrok { get; set; }


        /// <summary>
        /// Квалификация
        /// Таблица EduQualification
        /// </summary>
        public DbSet<EduQualification> EduQualification { get; set; }

        /// <summary>
        /// Формы контроля
        /// </summary>
        public DbSet<FormKontrolName> FormKontrolNames { get; set; }

        /// <summary>
        /// Семестры (справочник)
        /// </summary>
        public DbSet<SemestrName> SemestrNames { get; set; }

        /// <summary>
        /// Блоки дисциплин учебного плана
        /// </summary>
        public DbSet<BlokDiscipl> BlokDiscipl { get; set; }

        /// <summary>
        /// Части блоков дисциплин учебного плана
        /// </summary>
        public DbSet<BlokDisciplChast> BlokDisciplChast { get; set; }

        /// <summary>
        /// Наименование дисциплин
        /// </summary>
        public DbSet<DisciplineName> DisciplineNames { get; set; }

        /// <summary>
        /// Добавление видов деятельности к учебному плану
        /// </summary>
        public DbSet<EduPlanEduVidDeyat> EduPlanEduVidDeyats { get; set; }

        /// <summary>
        /// Добавление годов начала подготовки к учебному плану
        /// </summary>
        public DbSet<EduPlanEduYearBeginningTraining> EduPlanEduYearBeginningTraining { get; set; }

        /// <summary>
        /// Календарные учебные графики
        /// </summary>
        public DbSet<EduShedule> EduShedules { get; set; }

        /// <summary>
        /// Образовательные программы
        /// </summary>
        public DbSet<EduProgram> EduPrograms { get; set; }

        /// <summary>
        /// Таблица для реализации отношения М:М между таблицами EduPrograms и EduForms
        /// </summary>
        public DbSet<EduProgramEduForm> EduProgramEduForms { get; set; }

        /// <summary>
        /// Таблица для реализации отношения М:М между таблицами EduPrograms и EduYears
        /// </summary>
        public DbSet<EduProgramEduYear> EduProgramEduYears { get; set; }

        /// <summary>
        /// Инициализация базы данных
        /// </summary>        
        /// <returns></returns>
        public DbSet<EduProgramEduYearBeginningTraining> EduProgramEduYearBeginningTraining { get; set; }

        /// <summary>
        /// Таблица для реализации отношения М:М между таблицами EduPlans и EduYears
        /// </summary>
        public DbSet<EduPlanEduYear> EduPlanEduYears { get; set; }

        /// <summary>
        /// Дисциплины в составе учебного плана
        /// </summary>
        public DbSet<Discipline> Disciplines { get; set; }

        /// <summary>
        /// Помещения, в которых ведутся дисциплины в составе учебного плана в учебном году
        /// </summary>
        public DbSet<DisciplinePomeshenie> DisciplinePomeshenies { get; set; }

        /// <summary>
        /// Аннотации к рабочим программам дисциплин в составе учебного плана
        /// </summary>
        public DbSet<EduAnnotation> EduAnnotations { get; set; }

        /// <summary>
        /// Рабочие программы
        /// </summary>        
        public DbSet<RabProgram> RabPrograms { get; set; }

        /// <summary>
        /// Фонды оценочных средств
        /// </summary>
        public DbSet<FondOcenochnihSredstv> FondOcenochnihSredstvs { get; set; }

        /// <summary>
        /// Курсы, на которых ведётся дисциплина в составе учебного плана
        /// </summary>
        public DbSet<Kurs> Kurses { get; set; }

        /// <summary>
        /// Семестры, в которых ведётся дисциплина
        /// </summary>
        public DbSet<Semestr> Semestres { get; set; }

        /// <summary>
        /// Таблица для реализации отношения М:М между таблицами Semestr и VidUchebRabotiName
        /// </summary>
        public DbSet<SemestrVidUchebRaboti> SemestrVidUchebRaboti { get; set; }

        /// <summary>
        /// Таблица для реализации отношения М:М между таблицами Semestr и FormKontrolName
        /// </summary>
        public DbSet<SemestrFormKontrolName> SemestrFormKontrolName { get; set; }
        #endregion

        #region Структура образовательной организации (Struct)

        /// <summary>
        /// Университеты
        /// </summary>
        public DbSet<StructUniversity> StructUniversities { get; set; }

        /// <summary>
        /// Институты
        /// </summary>
        public DbSet<StructInstitute> StructInstitutes { get; set; }

        /// <summary>
        /// Факультеты
        /// </summary>
        public DbSet<StructFacultet> StructFacultets { get; set; }

        /// <summary>
        /// Кафедры
        /// </summary>
        public DbSet<StructKaf> StructKafs { get; set; }

        /// <summary>
        /// Структурные подразделения
        /// </summary>
        public DbSet<StructSubvision> StructSubvisions { get; set; }

        /// <summary>
        /// Типы структурных подразделений
        /// </summary>
        public DbSet<StructSubvisionType> StructSubvisionTypes { get; set; }
        #endregion

        #region Общие справочники (должности, адреса, телефоны, факсы и пр.)
        /// <summary>
        /// Справочник статусов пользователей
        /// </summary>
        public DbSet<AppUserStatus> AppUserStatuses { get; set; }
        
        /// <summary>
        /// Справочник статусов записей
        /// </summary>
        public DbSet<RowStatus> RowStatuses { get; set; }

        /// <summary>
        /// Справочник должностей
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// Адреса
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Телефоны
        /// </summary>
        public DbSet<Telephone> Telephones { get; set; }

        /// <summary>
        /// Факсы
        /// </summary>
        public DbSet<Fax> Faxes { get; set; }

        /// <summary>
        /// Адреса электронной почты
        /// </summary>
        public DbSet<Email> Emails { get; set; }

        /// <summary>
        /// Ссылки на информацию об институте на сторонних веб-сайтах
        /// </summary>
        public DbSet<InstituteLink> InstituteLinks { get; set; }

        /// <summary>
        /// Справочник типов веб-ресурсов
        /// </summary>
        public DbSet<LinkType> LinkTypes { get; set; }

        /// <summary>
        /// Учебные заведения
        /// </summary>
        public DbSet<EducationalInstitution> EducationalInstitutions { get; set; }

        /// <summary>
        /// Типы учебных заведений
        /// </summary>
        public DbSet<EducationalInstitutionType> EducationalInstitutionTypes { get; set; }

        /// <summary>
        /// Расположения учебных заведений, адреса абитуриентов и прочее
        /// </summary>
        public DbSet<Location> Locations { get; set; }

        /// <summary>
        /// GPS-координаты
        /// </summary>
        public DbSet<GpsCoordinate> GpsCoordinates { get; set; }
        
        /// <summary>
        /// Страны
        /// </summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Регионы (области, края и пр.)
        /// </summary>
        public DbSet<Region> Regions { get; set; }

        /// <summary>
        /// Районы (в составе региона)
        /// </summary>
        public DbSet<District> Districts { get; set; }

        /// <summary>
        /// Населённые пункты
        /// </summary>
        public DbSet<PopulatedLocality> PopulatedLocalities { get; set; }

        /// <summary>
        /// Типы населённых пунктов
        /// </summary>
        public DbSet<PopulatedLocalityType> PopulatedLocalityTypes { get; set; }
        #endregion

        #region Сведения об образовательной организации
        /// <summary>
        /// Информация о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        public DbSet<EduPOAccred> EduPOAccreds { get; set; }

        /// <summary>
        /// Места осуществления образовательной деятельности
        /// </summary>
        public DbSet<AddressPlace> AddressPlaces { get; set; }

        /// <summary>
        /// Таблица 3. Сведения об учредителях
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<UchredLaw> UchredLaw { get; set; }

        /// <summary>
        /// Таблица 4. Сведения о филиалах
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<FilInfo> FilInfo { get; set; }

        /// <summary>
        /// Таблица 8 Информация о результатах приема по каждой профессии,
        /// специальности СПО, каждому направлению подготовки или
        /// специальности ВО с различными условиями приема
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<EduPriem> EduPriem { get; set; }

        /// <summary>
        /// Таблица 9. Информация о результатах перевода, восстановления и отчисления
        /// </summary>
        public DbSet<EduPerevod> eduPerevod { get; set; }

        /// <summary>
        /// Таблица 11. Образовательная программа (объём программы по годам)
        /// </summary>
        public DbSet<EduOPYear> EduOPYears { get; set; }

        /// <summary>
        /// Вспомогательная таблица для таблицы 11.
        /// Наименования годов обучения ("за 1 год обучения" и т.д.)
        /// </summary>
        public DbSet<EduOPEduYearName> EduOPEduYearNames { get; set; }

        /// <summary>
        /// Таблица 12. Образовательная программа (наличие практики)
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<EduPr> EduPr { get; set; }

        /// <summary>
        /// Таблица 13. Образовательная программа (направления и результаты НИР)
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<EduNir> EduNir { get; set; }

        /// <summary>
        /// Таблица 14. Информация об администрации
        /// образовательной организации
        /// </summary>
        public DbSet<Rucovodstvo> SvedenRucovodstvo { get; set; }

        /// <summary>
        /// Таблица 15. Информация о руководителях филиалов
        /// образовательной организации
        /// </summary>
        public DbSet<RucovodstvoFil> RucovodstvoFil { get; set; }

        /// <summary>
        /// Таблица 18. Наличие библиотек, объектов спорта, условия питания и охраны здоровья обучающихся
        /// </summary>
        public DbSet<PurposeLibr> PurposeLibr { get; set; }

        /// <summary>
        /// Таблица 20. Наличие общежития, интерната,
        /// количество жилых помещений в общежитии, интернате
        /// для иногородних обучающихся
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<HostelInfo> HostelInfo { get; set; }

        /// <summary>
        /// Таблица 22. Объем образовательной деятельности
        /// </summary>
        public DbSet<Volume> Volume { get; set; }

        /// <summary>
        /// Таблица "Сведения о поступлении финансовых и материальных средств и об их расходовании"
        /// </summary>
        public DbSet<VolumeFinYearPostRas> VolumeFinYearPostRas { get; set; }
        #endregion

        #region Абитуриенту
        /// <summary>
        /// Абитуриенты
        /// </summary>
        public DbSet<Abiturient> Abiturients { get; set; }

        /// <summary>
        /// Статусы абитуриентов
        /// </summary>
        public DbSet<AbiturientStatus> AbiturientStatuses { get; set; }

        /// <summary>
        /// Типы квот на выделение мест для обучения
        /// (в пределах квоты приема лиц, имеющих особое право,
        /// в пределах квоты целевого приема, основные в рамках контрольных цифр,
        /// по договорам об оказании платных образовательных услуг)
        /// </summary>
        public DbSet<QuotaType> QuotaTypes { get; set; }

        /// <summary>
        /// Сопоставления направлений подготовки и типов квот приёма
        /// </summary>
        public DbSet<EduNapravlQuotaType> EduNapravlQuotaTypes { get; set; }

        /// <summary>
        /// Сопоставления направлений подготовки и форм обучения при приёме
        /// </summary>
        public DbSet<EduNapravlEduForm> EduNapravlEduForms { get; set; }

        /// <summary>
        /// Заявления о приёме
        /// </summary>
        public DbSet<ApplicationForAdmission> ApplicationForAdmissions { get; set; }

        /// <summary>
        /// Индивидуальные достижения абитуриентов
        /// </summary>
        public DbSet<AbiturientIndividualAchievment> AbiturientIndividualAchievments { get; set; }

        /// <summary>
        /// Типы льгот при приёме
        /// </summary>
        public DbSet<AdmissionPrivilegeType> AdmissionPrivilegeTypes { get; set; }

        /// <summary>
        /// Льготы при приёме
        /// </summary>
        public DbSet<AdmissionPrivilege> AdmissionPrivileges { get; set; }

        /// <summary>
        /// Типы индивидуальных достижений абитуриентов
        /// </summary>
        public DbSet<AbiturientIndividualAchievmentType> AbiturientIndividualAchievmentTypes { get; set; }

        /// <summary>
        /// Заявления о согласии на зачислении
        /// </summary>
        public DbSet<ConsentToEnrollment> ConsentToEnrollments { get; set; }

        /// <summary>
        /// Назначение абитуриенту пользователя - консультанта
        /// </summary>
        public DbSet<AppUserAbiturientConsultant> AppUserAbiturientConsultants { get; set; }

        /// <summary>
        /// Способы подачи документов
        /// </summary>
        public DbSet<SubmittingDocumentsType> SubmittingDocumentsTypes { get; set; }

        /// <summary>
        /// Бланки регистрации абитуриентов на вступительные испытания
        /// </summary>
        public DbSet<EntranceTestRegistrationForm> EntranceTestRegistrationForms { get; set; }

        /// <summary>
        /// Протоколы вступительных испытаний
        /// </summary>
        public DbSet<EntranceTestsProtocol> EntranceTestsProtocols { get; set; }

        /// <summary>
        /// Заявления об отзыве документов абитуриентами
        /// </summary>
        public DbSet<RevocationStatement> RevocationStatements { get; set; }


        /// <summary>
        /// Таблица 24. Информация о количестве мест для приёма на обучение
        /// по различным условиям поступления (до 1 октября)
        /// </summary>
        public DbSet<priemKolMest> priemKolMest { get; set; }

        /// <summary>
        /// Таблица 25. Информация по различным условиям поступления (до 1 октября)
        /// </summary>
        public DbSet<PriemExam> PriemExam { get; set; }

        /// <summary>
        /// Таблица 27. Информация о количестве мест для приёма на обучение
        /// по различным условиям поступления (до 1 июня)
        /// </summary>
        public DbSet<priemKolTarget> priemKolTarget { get; set; }

        
        #endregion

        #region Электронная информационно-образовательная среда
        /// <summary>
        /// Электронные библиотечные системы
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<ElectronBiblSystem> ElectronBiblSystem { get; set; }

        /// <summary>
        /// База данных электронного каталога
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<ElectronCatalog> ElectronCatalog { get; set; }

        /// <summary>
        /// Электронные образовательные и информационные ресурсы
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<ElectronObrazovatInformRes> ElectronObrazovatInformRes { get; set; }

        /// <summary>
        /// Типы работ пользователя (курсовые, ВКР, эссе и пр.)
        /// </summary>
        public DbSet<UserWorkType> UserWorkTypes { get; set; }

        /// <summary>
        /// Работы пользователя (для сохранения курсовых, ВКР, эссе и пр.)
        /// </summary>
        public DbSet<UserWork> UserWorks { get; set; }

        /// <summary>
        /// Справочник оценок за работы пользователей
        /// </summary>
        public DbSet<UserWorkReviewMark> UserWorkReviewMarks { get; set; }

        /// <summary>
        /// Рецензии и оценки работ пользователей
        /// </summary>
        public DbSet<UserWorkReview> UserWorkReviews { get; set; }
        #endregion

        #region Файлы
        /// <summary>
        /// Файлы
        /// </summary>
        public DbSet<FileModel> Files { get; set; }

        /// <summary>
        /// Типы содержимого файлов
        /// </summary>
        public DbSet<FileDataType> FileDataTypes { get; set; }

        /// <summary>
        /// Таблица, связующая файлы с типами содержимого
        /// </summary>
        public DbSet<FileToFileType> FileToFileTypes { get; set; }

        /// <summary>
        /// Группы типов содержимого файлов
        /// </summary>
        public DbSet<FileDataTypeGroup> FileDataTypeGroups { get; set; }

        /// <summary>
        /// Документы пользователя
        /// </summary>
        public DbSet<UserDocument> UserDocuments { get; set; }

        /// <summary>
        /// Бланки и примеры заполнения документов
        /// </summary>
        public DbSet<DocumentSample> DocumentSamples { get; set; }
        #endregion

        #region Прием
        /// <summary>
        /// Кол-во вакантных мест для приема (перевода)
        /// </summary>
        public DbSet<Vacant> Vacants { get; set; }

        /// <summary>
        /// Информация о количестве поданных заявлений о приеме
        /// </summary>
        public DbSet<BlankNum> BlankNums { get; set; }
        #endregion

        #region Настройки пользователя
        /// <summary>
        /// Группы ученых степеней
        /// </summary>
        public DbSet<AcademicDegreeGroup> AcademicDegreeGroups { get; set; }

        /// <summary>
        /// Ученые степени
        /// </summary>
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }



        /// <summary>
        /// Ученые звания
        /// </summary>
        public DbSet<AcademicStat> AcademicStats { get; set; }

        /// <summary>
        /// Квалификации и направления подготовки
        /// </summary>
        public DbSet<Qualification> Qualifications { get; set; }

        /// <summary>
        /// Профессиональная переподготовка
        /// </summary>
        public DbSet<ProfessionalRetraining> ProfessionalRetrainings { get; set; }

        /// <summary>
        /// Профессиональная переподготовка
        /// </summary>
        public DbSet<RefresherCourse> RefresherCourses { get; set; }

        /// <summary>
        /// Преподаватели
        /// </summary>
        public DbSet<Teacher> Teachers { get; set; }

        /// <summary>
        /// Таблица для закрепления за преподавателем профилей подготовки и дисциплин
        /// </summary>
        public DbSet<TeacherEduProfileDisciplineName> TeacherEduProfileDisciplineNames { get; set; }

        /// <summary>
        /// Преподаватель - Кафедра - Должность - Ставка - Дата установления ставки
        /// </summary>
        public DbSet<TeacherStructKafPostStavka> TeacherStructKafPostStavka { get; set; }

        /// <summary>
        /// Таблица для закрепления за преподавателем дисциплин по учебному плану (по учебным годам)
        /// </summary>
        public DbSet<TeacherDiscipline> TeacherDisciplines { get; set; }

        /// <summary>
        /// Таблица "Типы достижений пользователя"
        /// </summary>
        public DbSet<UserAchievmentType> UserAchievmentTypes { get; set; }

        /// <summary>
        /// Таблица "Достижения пользователя"
        /// </summary>
        public DbSet<UserAchievment> UserAchievments { get; set; }

        /// <summary>
        /// Таблица "Образование пользователя"
        /// </summary>
        public DbSet<UserEducation> UserEducations { get; set; }

        /// <summary>
        /// Иностранные языки
        /// </summary>
        public DbSet<ForeignLanguage> ForeignLanguages { get; set; }

        /// <summary>
        /// Пользователи - Иностранные языки
        /// </summary>
        public DbSet<AppUserForeignLanguage> AppUserForeignLanguages { get; set; }

        /// <summary>
        /// Типы отношений пользователя к военной службе
        /// </summary>
        public DbSet<MilitaryServiceStatus> MilitaryServiceStatuses { get; set; }

        /// <summary>
        /// Справочник полов
        /// </summary>
        public DbSet<Gender> Genders { get; set; }

        /// <summary>
        /// Контакты членов семьи / ближайших родственников
        /// </summary>
        public DbSet<FamilyMemberContact> FamilyMemberContacts { get; set; }

        /// <summary>
        /// Наименования типов родственных связей (отец, мать и пр.)
        /// </summary>
        public DbSet<FamilyMemberType> FamilyMemberTypes { get; set; }
        #endregion

        #region Внешние ресурсы пользователей

        /// <summary>
        /// Внешние ресурсы пользователей
        /// </summary>
        public DbSet<UserAccountExternal> userAccountExternals { get; set; }

        /// <summary>
        /// Справочник Внешние ресурсы
        /// </summary>
        public DbSet<ExternalResource> ExternalResources { get; set; }

        /// <summary>
        /// Справочник Типов внешних ресурсов
        /// </summary>
        public DbSet<ExternalResourceType> ExternalResourceTypes { get; set; }

        #endregion

        #region Материально-техническое обеспечение

        /// <summary>
        /// Корпус
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<Korpus> Korpus { get; set; }

        /// <summary>
        /// Помещение
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<Pomeshenie> Pomeshenie { get; set; }

        /// <summary>
        /// Оборудование
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<Oborudovanie> Oborudovanie { get; set; }

        /// <summary>
        /// Тип помещения (справочник)
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<PomeshenieType> PomeshenieType { get; set; }

        /// <summary>
        /// Помещение - Тип помещения (таблица для связи)
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<PomeshenieTypepomesheniya> PomeshenieTypepomesheniya { get; set; }

        #endregion

        #region Трудоустройство
        /// <summary>
        /// Год выпуска
        /// </summary>
        public DbSet<GraduateYear> GraduateYear { get; set; }

        /// <summary>
        /// Выпуск (результаты защиты ВКР)
        /// </summary>
        public DbSet<EduGraduate> EduGraduate { get; set; }

        /// <summary>
        /// Количество трудоустроенных выпускников
        /// </summary>
        public DbSet<GraduateTrudoustroustvo> GraduateTrudoustroustvo { get; set; }
        #endregion

        #region Учебные пособия

        /// <summary>
        /// Авторы учебных пособий
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<Author> Author { get; set; }

        /// <summary>
        /// Таблица Учебное пособие
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<UchPosobie> UchPosobie { get; set; }

        /// <summary>
        /// Связующая таблица между UchPosobie (учебное пособие) и  DisciplineName (наименованиея дисциплин)
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<UchPosobieDisciplineName> UchPosobieDisciplineName { get; set; }

        /// <summary>
        /// Связующая таблица между UchPosobie (учебное пособие) и  EduForm (форма обучения)
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<UchPosobieEduForm> UchPosobieEduForm { get; set; }

        /// <summary>
        /// Связующая таблица между UchPosobie (учебными пособиями) и EduNapravl (направлениями)
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<UchPosobieEduNapravl> UchPosobieEduNapravl { get; set; }

        /// <summary>
        /// Справочник "Форма издания учебного пособия"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<UchPosobieFormaIzdaniya> UchPosobieFormaIzdaniya { get; set; }

        /// <summary>
        /// Справочник "Вид учебного пособия"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<UchPosobieVid> UchPosobieVid { get; set; }

        /// <summary>
        /// Связь между Uchposobie (учебное пособие) и Author (автор)
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<UchPosobieAuthor> UchPosobieAuthor { get; set; }

        #endregion

        #region НИР
        /// <summary>
        /// Тема НИР
        /// </summary>
        public DbSet<NirTema> NirTema { get; set; }

        /// <summary>
        /// Инициализация базы данных
        /// </summary>        
        public DbSet<NirTemaEduProfile> NirTemaEduProfile { get; set; }

        /// <summary>
        /// Научные статьи
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// Научные статьи - Авторы
        /// </summary>
        public DbSet<ArticleAuthor> ArticleAuthors { get; set; }

        /// <summary>
        /// Научные статьи - Темы НИР
        /// </summary>
        public DbSet<ArticleNirTema> ArticleNirTemas { get; set; }

        /// <summary>
        /// Научные журналы
        /// </summary>
        public DbSet<ScienceJournal> ScienceJournals { get; set; }

        /// <summary>
        /// Заявки на добавление научных журналов
        /// </summary>
        public DbSet<ScienceJournalAddingClaim> ScienceJournalAddingClaims { get; set; }

        /// <summary>
        /// Научные журналы - Базы цитирования
        /// </summary>
        public DbSet<ScienceJournalCitationBase> ScienceJournalCitationBases { get; set; }

        /// <summary>
        /// Базы цитирования
        /// </summary>
        public DbSet<CitationBase> CitationBases { get; set; }

        /// <summary>
        /// Статья - Специальность научных работников, согласно номенклатуре
        /// </summary>
        public DbSet<ArticleNirSpecial> ArticleNirSpecials { get; set; }

        /// <summary>
        /// Специальности научных работников, согласно номенклатуре
        /// </summary>
        public DbSet<NirSpecial> NirSpecials { get; set; }

        /// <summary>
        /// Специальности научных работников, согласно номенклатуре - Профили подготовки
        /// </summary>
        public DbSet<NirSpecialEduProfile> NirSpecialEduProfiles { get; set; }

        /// <summary>
        /// Патенты и свидетельства 
        /// </summary>
        public DbSet<Patent> Patents { get; set; }

        /// <summary>
        /// Патент - автор
        /// </summary>
        public DbSet<PatentAuthor> PatentAuthors { get; set; }

        /// <summary>
        /// Вид патента (свидетельства)
        /// </summary>
        public DbSet<PatentVid> PatentVids { get; set; }

        /// <summary>
        /// Группа вида объекта интеллектуальной собственности
        /// </summary>
        public DbSet<PatentVidGroup> PatentVidGroups { get; set; }

        /// <summary>
        /// Патент - Тема НИР
        /// </summary>
        public DbSet<PatentNirTema> PatentNirTemas { get; set; }

        /// <summary>
        /// Патент - Специальность научных работников, согласно номенклатуре
        /// </summary>
        public DbSet<PatentNirSpecial> PatentNirSpecials { get; set; }

        /// <summary>
        /// Монографии 
        /// </summary>
        public DbSet<Monograf> Monografs { get; set; }

        /// <summary>
        /// Монография - автор
        /// </summary>
        public DbSet<MonografAuthor> MonografAuthors { get; set; }

        /// <summary>
        /// Монография - Тема НИР
        /// </summary>
        public DbSet<MonografNirTema> MonografNirTemas { get; set; }

        /// <summary>
        /// Монография - Специальность научных работников, согласно номенклатуре
        /// </summary>
        public DbSet<MonografNirSpecial> MonografNirSpecials { get; set; }
        #endregion

        #region Студенты
        /// <summary>
        /// Студенты
        /// </summary>
        public DbSet<Student> Students { get; set; }
        /// <summary>
        /// Студенческие группы
        /// </summary>
        public DbSet<StudentGroup> StudentGroups { get; set; }

        /// <summary>
        /// Справочник наименований отметок по ведомости (хорошо, отлично, зачтено и пр.)
        /// </summary>
        public DbSet<VedomostStudentMarkName> VedomostStudentMarkNames { get; set; }

        /// <summary>
        /// Отметки студентов по ведомости
        /// </summary>
        public DbSet<VedomostStudentMark> VedomostStudentMarks { get; set; }

        /// <summary>
        /// Ведомости
        /// </summary>
        public DbSet<Vedomost> Vedomosti { get; set; }
        #endregion

        #region Сообщения
        /// <summary>
        /// Сообщения пользователей друг другу
        /// </summary>
        public DbSet<UserMessage> UserMessages { get; set; }

        /// <summary>
        /// Сообщения от пользователей для учебных групп
        /// </summary>
        public DbSet<MessageFromAppUserToStudentGroup> MessagesFromAppUsersToStudentGroups { get; set; }
        #endregion

        #region Методкомиссии
        /// <summary>
        /// Методкомиссии
        /// </summary>
        public DbSet<MetodKomissiya> MetodKomissii { get; set; }

        /// <summary>
        /// Методкомиссия - Профиль подготовки
        /// </summary>
        public DbSet<MetodKomissiyaEduProfile> MetodKomissiyaEduProfiles { get; set; }

        /// <summary>
        /// Преподаватель - Методкомиссия
        /// </summary>
        public DbSet<TeacherMetodKomissiya> TeacherMetodKomissiya { get; set; }
        #endregion

        #region Сотрудники
        public DbSet<AppUserStructSubvision> AppUserStructSubvisions { get; set; }
        #endregion

        #endregion
        /// <summary>
        /// Инициализация базы данных
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task InitDatabase(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            await InitDatabaseRoles.CreateRoles(serviceProvider, configuration);
            await InitDatabaseUserData.CreateAdminAccount(serviceProvider, configuration);
            await InitDatabaseUserData.CreateUserData(serviceProvider, configuration);

            await InitDatabaseEduQualification.CreateEduQualification(serviceProvider, configuration);
            await InitDatabaseRowStatuses.CreateRowStatuses(serviceProvider, configuration);
            await InitDatabaseEducationData.CreateEducationData(serviceProvider, configuration);
            await InitDatabaseStructData.CreateStructData(serviceProvider, configuration);
            await InitDatabaseFilesData.CreateFilesData(serviceProvider, configuration);
            await InitDatabaseEduChislen.CreateEduChislen(serviceProvider, configuration);            
            await InitDatabaseRucovodstvo.CreateRucovodstvo(serviceProvider, configuration);
            await InitDatabaseHostelInfo.CreateHostelInfo(serviceProvider, configuration);

            await InitDatabaseUserData.SettingAdminsProfileData(serviceProvider, configuration);
            await InitDatabaseUserData.CreateMainUsersAccounts(serviceProvider, configuration);
            await InitDatabaseUserData.CreateStudentsAccounts(serviceProvider, configuration);
            await InitDatabaseUserData.CreateOtdelKadrovAccounts(serviceProvider, configuration);
            await InitDatabaseUserData.CreateBuhgalteriyaAccounts(serviceProvider, configuration);
            await InitDatabaseUserData.CreateUchChastAccounts(serviceProvider, configuration);

            await InitDatabaseElectronBiblSystem.CreateElectronBiblSystem(serviceProvider, configuration);
            await InitDatabaseElectronCatalog.CreateElectronCatalog(serviceProvider, configuration);
            await InitDatabaseElectronObrazovatInformRes.CreateElectronObrazovatInformRes(serviceProvider, configuration);

            await InitDatabaseUchredLaw.CreateUchredLaw(serviceProvider, configuration);
            await InitDatadaseBlokDisciplName.CreateBlokDisciplName(serviceProvider, configuration);
            await InitDatabaseFormKontrolName.CreateFormKontrolName(serviceProvider, configuration);
            await InitDatabaseEduProgramPodg.CreateEduProgramPodg(serviceProvider, configuration);
            await InitDatabaseVidUchebRabotiName.CreateVidUchebRabotiNames(serviceProvider, configuration);
            await InitDatabaseEduVidDeyats.CreateEduVidDeyats(serviceProvider, configuration);
            await InitDatabaseBlokDisciplChastName.CreateBlokDisciplChastName(serviceProvider, configuration);
            await InitDatabaseEduSrok.CreateEduSrok(serviceProvider, configuration);

            await InitDatabaseKorpus.CreateKorpus(serviceProvider, configuration);
            await InitDataBasePomeshenieTypes.CreatePomeshenieType(serviceProvider, configuration);           
            await InitDatabasePomeshenie.CreatePomeshenie(serviceProvider, configuration);
            await InitDataBaseOborudovanie.CreateOborudovanie(serviceProvider, configuration);

            await InitDatabaseAuthor.CreateAuthor(serviceProvider, configuration);
            await InitDatabaseUchPosobieFormaIzdaniya.CreateUchPosobieFormaIzdaniya(serviceProvider, configuration);
            await InitDatabaseUchPosobieVid.CreateUchPosobieVid(serviceProvider, configuration);

            await InitDatabaseGraduateYear.CreateGraduateYear(serviceProvider, configuration);
            await InitDatabaseDisciplineName.CreateDisciplineName(serviceProvider, configuration);
            //await InitDatabaseEduPlans.CreateEduPlans(serviceProvider, configuration);
            //await InitDatabaseEduPlanEduVidDeyats.CreateEduPlanEduVidDeyats(serviceProvider, configuration);
            await InitDatabaseEduOPEduYearNames.CreateEduOPEduYearNames(serviceProvider, configuration);
            await InitDatabaseLinkTypes.CreateLinkTypes(serviceProvider, configuration);
            await InitDatabaseInstituteLinks.CreateInstituteLinks(serviceProvider, configuration);

            await InitDatabaseNirTema.CreateNirTema(serviceProvider, configuration);
            await InitDatabaseSemestrName.CreateSemestrName(serviceProvider, configuration);

            await InitDatabaseAppSettings.CreateAppSettings(serviceProvider, configuration);

            await InitDatabaseVedomostStudentMarkName.CreateVedomostStudentMarkName(serviceProvider, configuration);
            await InitDatabaseUserWorkType.CreateUserWorkType(serviceProvider, configuration);
            await InitDatabaseUserWorkReviewMarks.CreateUserWorkReviewMarks(serviceProvider, configuration);

            await InitDatabasePatents.CreatePatentVidGroups(serviceProvider, configuration);
            await InitDatabasePatents.CreatePatentVids(serviceProvider, configuration);

            await InitDatabaseYears.CreateYears(serviceProvider, configuration);
            await InitDatabaseEmploymentForm.CreateEmploymentForms(serviceProvider, configuration);

            await InitDatabaseCitationBases.CreateCitationBases(serviceProvider, configuration);
            await InitDatabaseNirSpecials.CreateNirSpecials(serviceProvider, configuration);

            await InitDatabaseUserMessageTypes.CreateUserMessageTypes(serviceProvider, configuration);

            await InitDatabaseAppUserStatuses.CreateAppUserStatuses(serviceProvider, configuration);

            await InitDatabaseEducationalInstitutionTypes.CreateEducationalInstitutionTypes(serviceProvider, configuration);

            await InitDatabasePopulatedLocalities.CreateCountries(serviceProvider, configuration);
            await InitDatabasePopulatedLocalities.CreateRegions(serviceProvider, configuration);
            await InitDatabasePopulatedLocalities.CreateDistricts(serviceProvider, configuration);
            await InitDatabasePopulatedLocalities.CreatePopulatedLocalityTypes(serviceProvider, configuration);
            await InitDatabasePopulatedLocalities.CreatePopulatedLocalities(serviceProvider, configuration);

            await InitDatabaseEducationalInstitutions.CreateEducationalInstitutions(serviceProvider, configuration);

            await InitDatabaseAbiturientStatuses.CreateAbiturientStatuses(serviceProvider, configuration);
            await InitDatabaseQuotaTypes.CreateQuotaTypes(serviceProvider, configuration);
            await InitDatabaseAbiturientIndividualAchievmentTypes.CreateAbiturientIndividualAchievmentTypes(serviceProvider, configuration);

            await InitDatabaseForeignLanguages.CreateForeignLanguages(serviceProvider, configuration);

            await InitDatabaseLmsEventTypes.CreateLmsEventTypeGroups(serviceProvider, configuration);
            await InitDatabaseLmsEventTypes.CreateLmsEventTypes(serviceProvider, configuration);

            await InitDatabaseGenders.CreateGenders(serviceProvider, configuration);
            await InitDatabaseFamilyMemberTypes.CreateFamilyMemberTypes(serviceProvider, configuration);
            await InitDatabaseMilitaryServiceStatuses.CreateMilitaryServiceStatuses(serviceProvider, configuration);

            await InitDatabaseAdmissionPrivilegeTypes.CreateAdmissionPrivilegeTypes(serviceProvider, configuration);

            await InitDatabaseAppUserLmsEventUserRoles.CreateAppUserLmsEventUserRoles(serviceProvider, configuration);

            await InitDatabaseContractTypes.CreateContractTypes(serviceProvider, configuration);

            await InitDatabaseLmsTaskTypes.CreateLmsTaskTypes(serviceProvider, configuration);

            await InitDatabaseSubmittingDocumentsTypes.CreateSubmittingDocumentsTypes(serviceProvider, configuration);

            await InitDatabaseExternalResources.CreateExternalResourceTypes(serviceProvider, configuration);
            await InitDatabaseExternalResources.CreateExternalResources(serviceProvider, configuration);

            await InitDatabaseElGradebooks.CreateElGradebookLessonTypes(serviceProvider, configuration);
            await InitDatabaseElGradebooks.CreateElGradebookLessonAttendanceTypes(serviceProvider, configuration);
        }
        
        
    }
}
