using KisVuzDotNetCore2.Models.Education;
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
        #region Таблицы

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
        /// Таблица EduProgramPodg
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<EduProgramPodg> EduProgramPodg { get; set; }

        /// <summary>
        /// Виды учебной работы
        /// </summary>
        public DbSet<VidUchebRaboti> VidUchebRaboti { get; set; }

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
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<FormKontrol> FormKontrol { get; set; }

        /// <summary>
        /// Семестр
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<Semestr> Semestr { get; set; }

        /// <summary>
        /// Наименование дисциплин
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<DisciplineName> DisciplineNames { get; set; }

        /// <summary>
        /// Добавление видов деятельности к учебному плану
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<EduPlanEduVidDeyat> EduPlanEduVidDeyats { get; set; }

        /// <summary>
        /// Добавление годов начала подготовки к учебному плану
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<EduPlanEduYearBeginningTraining> EduPlanEduYearBeginningTraining { get; set; }
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
        #endregion

        #region Сведения об образовательной организации
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
        #endregion

        #region Абитуриенту
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

            await InitDatabaseElectronBiblSystem.CreateElectronBiblSystem(serviceProvider, configuration);
            await InitDatabaseElectronCatalog.CreateElectronCatalog(serviceProvider, configuration);
            await InitDatabaseElectronObrazovatInformRes.CreateElectronObrazovatInformRes(serviceProvider, configuration);

            await InitDatabaseUchredLaw.CreateUchredLaw(serviceProvider, configuration);
            await InitDatadaseBlokDisciplName.CreateBlokDisciplName(serviceProvider, configuration);
            await InitDatabaseFormKontrol.CreateFormKontrol(serviceProvider, configuration);
            await InitDatabaseEduProgramPodg.CreateEduProgramPodg(serviceProvider, configuration);
            await InitDatabaseVidUchebRaboti.CreateVidUchebRaboti(serviceProvider, configuration);
            await InitDatabaseEduVidDeyats.CreateEduVidDeyats(serviceProvider, configuration);
            await InitDatabaseBlokDisciplChastName.CreateBlokDisciplChastName(serviceProvider, configuration);
            await InitDatabaseEduSrok.CreateEduSrok(serviceProvider, configuration);
            await InitDatabaseEduPlans.CreateEduPlans(serviceProvider, configuration);

            await InitDatabaseGraduateYear.CreateGraduateYear(serviceProvider, configuration);
            await InitDatabaseDisciplineName.CreateDisciplineName(serviceProvider, configuration);
            await InitDatabaseEduPlanEduVidDeyats.CreateEduPlanEduVidDeyats(serviceProvider, configuration);
            await InitDatabaseEduOPEduYearNames.CreateEduOPEduYearNames(serviceProvider, configuration);

        }        

        /// <summary>
        /// Инициализация базы данных
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<KisVuzDotNetCore2.Models.Education.BlokDiscipl> BlokDiscipl { get; set; }

        /// <summary>
        /// Инициализация базы данных
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public DbSet<KisVuzDotNetCore2.Models.Education.BlokDisciplChast> BlokDisciplChast { get; set; }
                
    }
}
