using KisVuzDotNetCore2.Models.Education;
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
        /// Таблица 14. Информация об администрации
        /// образовательной организации
        /// </summary>
        public DbSet<Rucovodstvo> SvedenRucovodstvo { get; set; }
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

        #endregion

        /// <summary>
        /// Инициализация базы данных
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task InitDatabase(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            await CreateAdminAccount(serviceProvider, configuration);
            await CreateUserData(serviceProvider, configuration);
            await CreateEducationData(serviceProvider, configuration);
            await CreateStructData(serviceProvider, configuration);
            await CreateFilesData(serviceProvider, configuration);
            await CreateEduChislen(serviceProvider, configuration);
            await CreateRucovodstvo(serviceProvider, configuration);
        }

        /// <summary>
        /// Создание учётной записи администратора
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                string username = configuration["Data:AdminUser:Name"];
                string email = configuration["Data:AdminUser:Email"];
                string password = configuration["Data:AdminUser:Password"];
                string role = configuration["Data:AdminUser:Role"];

                if (await userManager.FindByNameAsync(username) == null)
                {
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                    AppUser user = new AppUser
                    {
                        UserName = username,
                        Email = email
                    };
                    IdentityResult result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }

        /// <summary>
        /// Инициализация таблиц, связанных с пользователем
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateUserData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Группы ученых степеней"
                if (!await context.AcademicDegreeGroups.AnyAsync())
                {
                    AcademicDegreeGroup AcademicDegreeGroup1 = new AcademicDegreeGroup
                    {
                        AcademicDegreeGroupId=1,
                        AcademicDegreeGroupName="Кандидаты наук"
                    };

                    AcademicDegreeGroup AcademicDegreeGroup2 = new AcademicDegreeGroup
                    {
                        AcademicDegreeGroupId = 2,
                        AcademicDegreeGroupName = "Доктора наук"
                    };

                    await context.AcademicDegreeGroups.AddRangeAsync(
                        AcademicDegreeGroup1,
                        AcademicDegreeGroup2
                    );
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Ученые степени"
                if (!await context.AcademicDegrees.AnyAsync())
                {
                    AcademicDegree AcademicDegree1 = new AcademicDegree
                    {
                        AcademicDegreeId = 1,
                        AcademicDegreeGroupId = 1,
                        AcademicDegreeName = "Кандидат технических наук"
                    };

                    AcademicDegree AcademicDegree2 = new AcademicDegree
                    {
                        AcademicDegreeId = 2,
                        AcademicDegreeGroupId = 1,
                        AcademicDegreeName = "Кандидат сельскохозяйственных наук"
                    };

                    AcademicDegree AcademicDegree3 = new AcademicDegree
                    {
                        AcademicDegreeId = 3,
                        AcademicDegreeGroupId = 2,
                        AcademicDegreeName = "Доктор технических наук"
                    };

                    AcademicDegree AcademicDegree4 = new AcademicDegree
                    {
                        AcademicDegreeId = 4,
                        AcademicDegreeGroupId = 2,
                        AcademicDegreeName = "Доктор сельскохозяйственных наук"
                    };

                    await context.AcademicDegrees.AddRangeAsync(
                        AcademicDegree1,
                        AcademicDegree2,
                        AcademicDegree3,
                        AcademicDegree4
                    );
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Ученые звания"
                if (!await context.AcademicStats.AnyAsync())
                {
                    AcademicStat AcademicStat1 = new AcademicStat
                    {
                        AcademicStatId = 1,
                        AcademicStatName = "Доцент"
                    };

                    AcademicStat AcademicStat2 = new AcademicStat
                    {
                        AcademicStatId = 2,
                        AcademicStatName = "Профессор"
                    };

                    await context.AcademicStats.AddRangeAsync(
                        AcademicStat1,
                        AcademicStat2
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }


        /// <summary>
        /// Заполнение сведений об образовательном процессе
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEducationData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Свидетельства о гос. аккредитации"
                if (!await context.EduAccreds.AnyAsync())
                {
                    EduAccred svid1 = new EduAccred
                    {
                        EduAccredId = 1,
                        EduAccredDate = new DateTime(2016, 5, 6),
                        EduAccredDateExpiration = new DateTime(2020, 10, 3),
                        EduAccredNumber = "1922"
                    };

                    await context.EduAccreds.AddRangeAsync(svid1);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Курсы"
                if (!await context.EduKurses.AnyAsync())
                {
                    EduKurs EduKurs1 = new EduKurs
                    {
                        EduKursNumber=1,
                        EduKursName="Первый курс"
                    };

                    EduKurs EduKurs2 = new EduKurs
                    {
                        EduKursNumber = 2,
                        EduKursName = "Второй курс"
                    };

                    EduKurs EduKurs3 = new EduKurs
                    {
                        EduKursNumber = 3,
                        EduKursName = "Третий курс"
                    };

                    EduKurs EduKurs4 = new EduKurs
                    {
                        EduKursNumber = 4,
                        EduKursName = "Четвертый курс"
                    };

                    EduKurs EduKurs5 = new EduKurs
                    {
                        EduKursNumber = 5,
                        EduKursName = "Пятый курс"
                    };

                    await context.EduKurses.AddRangeAsync(EduKurs1, EduKurs2, EduKurs3, EduKurs4, EduKurs5);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Обобщенные уровни образования"
                if (!await context.EduLevelGroups.AnyAsync())
                {
                    EduLevelGroup eduLevelGroup1Spo = new EduLevelGroup
                    {
                        EduLevelGroupId = 1,
                        EduLevelGroupName = "Среднее профессиональное образование"
                    };

                    EduLevelGroup eduLevelGroup2Vo = new EduLevelGroup
                    {
                        EduLevelGroupId = 2,
                        EduLevelGroupName = "Высшее образование"
                    };

                    await context.EduLevelGroups.AddRangeAsync(eduLevelGroup1Spo,
                        eduLevelGroup2Vo);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Уровни образования"
                if (!await context.EduLevels.AnyAsync())
                {
                    EduLevel eduLevel1Spo = new EduLevel
                    {
                        EduLevelId = 2,
                        EduLevelName = "Среднее профессиональное образование"
                    };

                    EduLevel eduLevel2VoBak = new EduLevel
                    {
                        EduLevelId = 3,
                        EduLevelName = "Высшее образование - бакалавриат"
                    };

                    EduLevel eduLevel3VoMag = new EduLevel
                    {
                        EduLevelId = 4,
                        EduLevelName = "Высшее образование - магистратура"
                    };

                    EduLevel eduLevel4VoSpec = new EduLevel
                    {
                        EduLevelId = 5,
                        EduLevelName = "Высшее образование - специалитет"
                    };

                    EduLevel eduLevel5VoAsp = new EduLevel
                    {
                        EduLevelId = 6,
                        EduLevelName = "Высшее образование - подготовка кадров высшей квалификации"
                    };

                    await context.EduLevels.AddRangeAsync(eduLevel1Spo,
                        eduLevel2VoBak,
                        eduLevel3VoMag,
                        eduLevel4VoSpec,
                        eduLevel5VoAsp);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Укрупнённые группы специальностей"
                if (!await context.EduUgses.AnyAsync())
                {
                    #region УГС СПО
                    EduUgs eduUgs08 =
                        new EduUgs
                        {
                            EduUgsId = 1,
                            EduUgsCode = "08.00.00",
                            EduUgsName = "Техника и технологии строительства",
                            EduLevelId = 2,
                            EduAccredId=1
                        };
                    EduUgs eduUgs21 =
                        new EduUgs
                        {
                            EduUgsId = 2,
                            EduUgsCode = "21.00.00",
                            EduUgsName = "Прикладная геология, горное дело, нефтегазовое дело и геодезия",
                            EduLevelId = 2,
                            EduAccredId = 1
                        };

                    EduUgs eduUgs23 =
                        new EduUgs
                        {
                            EduUgsId = 3,
                            EduUgsCode = "23.00.00",
                            EduUgsName = "Техника и технологии наземного транспорта",
                            EduLevelId = 2,
                            EduAccredId = 1
                        };

                    EduUgs eduUgs35 =
                        new EduUgs
                        {
                            EduUgsId = 4,
                            EduUgsCode = "35.00.00",
                            EduUgsName = "Сельское, лесное и рыбное хозяйство",
                            EduLevelId = 2,
                            EduAccredId = 1
                        };

                    EduUgs eduUgs38 =
                        new EduUgs
                        {
                            EduUgsId = 5,
                            EduUgsCode = "38.00.00",
                            EduUgsName = "Экономика и управление",
                            EduLevelId = 2,
                            EduAccredId = 1
                        };
                    #endregion

                    #region УГС ВО-бакалавриат
                    EduUgs eduUgs13VoBak =
                        new EduUgs
                        {
                            EduUgsId = 6,
                            EduUgsCode = "13.00.00",
                            EduUgsName = "Электро- и теплоэнергетика",
                            EduLevelId = 3,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs19VoBak =
                        new EduUgs
                        {
                            EduUgsId = 7,
                            EduUgsCode = "19.00.00",
                            EduUgsName = "Промышленная экология и биотехнологии",
                            EduLevelId = 3,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs20VoBak =
                        new EduUgs
                        {
                            EduUgsId = 8,
                            EduUgsCode = "20.00.00",
                            EduUgsName = "Техносферная безопасность и природообустройство",
                            EduLevelId = 3,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs21VoBak =
                        new EduUgs
                        {
                            EduUgsId = 9,
                            EduUgsCode = "21.00.00",
                            EduUgsName = "Прикладная геология, горное дело, нефтегазовое дело и геодезия",
                            EduLevelId = 3,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs23VoBak =
                        new EduUgs
                        {
                            EduUgsId = 10,
                            EduUgsCode = "23.00.00",
                            EduUgsName = "Техника и технологии наземного транспорта",
                            EduLevelId = 3,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs35VoBak =
                        new EduUgs
                        {
                            EduUgsId = 11,
                            EduUgsCode = "35.00.00",
                            EduUgsName = "Сельское, лесное и рыбное хозяйство",
                            EduLevelId = 3,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs38VoBak =
                        new EduUgs
                        {
                            EduUgsId = 12,
                            EduUgsCode = "38.00.00",
                            EduUgsName = "Экономика и управление",
                            EduLevelId = 3,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs44VoBak =
                        new EduUgs
                        {
                            EduUgsId = 13,
                            EduUgsCode = "44.00.00",
                            EduUgsName = "Образование и педагогические науки",
                            EduLevelId = 3,
                            EduAccredId = 1
                        };
                    #endregion

                    #region УГС ВО-магистратура
                    EduUgs eduUgs13VoMag =
                        new EduUgs
                        {
                            EduUgsId = 14,
                            EduUgsCode = "13.00.00",
                            EduUgsName = "Электро- и теплоэнергетика",
                            EduLevelId = 4,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs23VoMag =
                        new EduUgs
                        {
                            EduUgsId = 15,
                            EduUgsCode = "23.00.00",
                            EduUgsName = "Техника и технологии наземного транспорта",
                            EduLevelId = 4,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs35VoMag =
                        new EduUgs
                        {
                            EduUgsId = 16,
                            EduUgsCode = "35.00.00",
                            EduUgsName = "Сельское, лесное и рыбное хозяйство",
                            EduLevelId = 4,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs38VoMag =
                        new EduUgs
                        {
                            EduUgsId = 17,
                            EduUgsCode = "38.00.00",
                            EduUgsName = "Экономика и управление",
                            EduLevelId = 4,
                            EduAccredId = 1
                        };
                    #endregion

                    #region УГС ВО-специалитет                    
                    EduUgs eduUgs23VoSpec =
                        new EduUgs
                        {
                            EduUgsId = 18,
                            EduUgsCode = "23.00.00",
                            EduUgsName = "Техника и технологии наземного транспорта",
                            EduLevelId = 5,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs38VoSpec =
                        new EduUgs
                        {
                            EduUgsId = 19,
                            EduUgsCode = "38.00.00",
                            EduUgsName = "Экономика и управление",
                            EduLevelId = 5,
                            EduAccredId = 1
                        };
                    #endregion

                    #region УГС ВО-подготовка кадров высшей квалификации
                    EduUgs eduUgs06VoAsp =
                        new EduUgs
                        {
                            EduUgsId = 20,
                            EduUgsCode = "06.00.00",
                            EduUgsName = "Биологические науки",
                            EduLevelId = 6,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs23VoAsp =
                        new EduUgs
                        {
                            EduUgsId = 21,
                            EduUgsCode = "23.00.00",
                            EduUgsName = "Техника и технологии наземного транспорта",
                            EduLevelId = 6,
                            EduAccredId = 1
                        };
                    EduUgs eduUgs35VoAsp =
                        new EduUgs
                        {
                            EduUgsId = 22,
                            EduUgsCode = "35.00.00",
                            EduUgsName = "Сельское, лесное и рыбное хозяйство",
                            EduLevelId = 6,
                            EduAccredId = 1
                        };
                    #endregion

                    await context.EduUgses.AddRangeAsync(new EduUgs[] { eduUgs08,
                        eduUgs21,
                        eduUgs23,
                        eduUgs35,
                        eduUgs38,
                        eduUgs13VoBak,
                        eduUgs19VoBak,
                        eduUgs20VoBak,
                        eduUgs21VoBak,
                        eduUgs23VoBak,
                        eduUgs35VoBak,
                        eduUgs38VoBak,
                        eduUgs44VoBak,
                        eduUgs13VoMag,
                        eduUgs23VoMag,
                        eduUgs35VoMag,
                        eduUgs38VoMag,
                        eduUgs23VoSpec,
                        eduUgs38VoSpec,
                        eduUgs06VoAsp,
                        eduUgs23VoAsp,
                        eduUgs35VoAsp
                    });
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Направления подготовки / специальности"
                if (!await context.EduNapravls.AnyAsync())
                {
                    #region Направления СПО
                    EduNapravl eduNapr080209 =
                        new EduNapravl
                        {
                            EduNapravlId = 1,
                            EduNapravlCode = "08.02.09",
                            EduNapravlName = "Монтаж, наладка и эксплуатация электрооборудования промышленных и гражданских зданий",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/db/mo/Data/d_14/m519.pdf",
                            EduUgsId = 1
                        };
                    EduNapravl eduNapr210205 =
                        new EduNapravl
                        {
                            EduNapravlId = 2,
                            EduNapravlCode = "21.02.05",
                            EduNapravlName = "Земельно-имущественные отношения",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/db/mo/Data/d_14/m486.pdf",
                            EduUgsId = 2
                        };
                    EduNapravl eduNapr230203 =
                        new EduNapravl
                        {
                            EduNapravlId = 3,
                            EduNapravlCode = "23.02.03",
                            EduNapravlName = "Техническое обслуживание и ремонт автомобильного транспорта",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/db/mo/Data/d_14/m383.pdf",
                            EduUgsId = 3
                        };
                    EduNapravl eduNapr350208 =
                        new EduNapravl
                        {
                            EduNapravlId = 4,
                            EduNapravlCode = "35.02.08",
                            EduNapravlName = "Электрификация и автоматизация сельского хозяйства",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/db/mo/Data/d_14/m457.pdf",
                            EduUgsId = 4
                        };
                    EduNapravl eduNapr380201 =
                        new EduNapravl
                        {
                            EduNapravlId = 5,
                            EduNapravlCode = "38.02.01",
                            EduNapravlName = "Экономика и бухгалтерский учет (по отраслям)",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/db/mo/Data/d_14/m832.pdf",
                            EduUgsId = 5
                        };
                    EduNapravl eduNapr380204 =
                        new EduNapravl
                        {
                            EduNapravlId = 6,
                            EduNapravlCode = "38.02.04",
                            EduNapravlName = "Коммерция (по отраслям)",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/file/docs/2014/05/58859.pdf",
                            EduUgsId = 5
                        };
                    #endregion

                    #region Направления ВО-бакалавриат
                    EduNapravl eduNapr130301 =
                        new EduNapravl
                        {
                            EduNapravlId = 7,
                            EduNapravlCode = "13.03.01",
                            EduNapravlName = "Теплоэнергетика и теплотехника",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/474/?f=%2Fuploadfiles%2Ffgosvob%2F130301.pdf",
                            EduUgsId = 6
                        };

                    EduNapravl eduNapr130302 =
                        new EduNapravl
                        {
                            EduNapravlId = 8,
                            EduNapravlCode = "13.03.02",
                            EduNapravlName = "Электроэнергетика и электротехника",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/436/?f=%2Fuploadfiles%2Ffgosvob%2F130302.pdf",
                            EduUgsId = 6
                        };

                    EduNapravl eduNapr190302 =
                        new EduNapravl
                        {
                            EduNapravlId = 9,
                            EduNapravlCode = "19.03.02",
                            EduNapravlName = "Продукты питания из растительного сырья",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/329/?f=%2Fuploadfiles%2Ffgosvob%2F190302.pdf",
                            EduUgsId = 7
                        };

                    EduNapravl eduNapr200301 =
                        new EduNapravl
                        {
                            EduNapravlId = 10,
                            EduNapravlCode = "20.03.01",
                            EduNapravlName = "Техносферная безопасность",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/560/?f=%2Fuploadfiles%2Ffgosvob%2F200301.pdf",
                            EduUgsId = 8
                        };

                    EduNapravl eduNapr210302 =
                        new EduNapravl
                        {
                            EduNapravlId = 11,
                            EduNapravlCode = "21.03.02",
                            EduNapravlName = "Землеустройство и кадастры",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/472/?f=%2Fuploadfiles%2Ffgosvob%2F210302.pdf",
                            EduUgsId = 9
                        };

                    EduNapravl eduNapr230301 =
                        new EduNapravl
                        {
                            EduNapravlId = 12,
                            EduNapravlCode = "23.03.01",
                            EduNapravlName = "Технология транспортных процессов",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/935/?f=%2Fuploadfiles%2Ffgosvob%2F230301.pdf",
                            EduUgsId = 10
                        };

                    EduNapravl eduNapr230303 =
                        new EduNapravl
                        {
                            EduNapravlId = 13,
                            EduNapravlCode = "23.03.03",
                            EduNapravlName = "Эксплуатация транспортно-технологических машин и комплексов",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/535/?f=%2Fuploadfiles%2Ffgosvob%2F230303.pdf",
                            EduUgsId = 10
                        };

                    EduNapravl eduNapr350304 =
                        new EduNapravl
                        {
                            EduNapravlId = 14,
                            EduNapravlCode = "35.03.04",
                            EduNapravlName = "Агрономия",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/516/?f=%2Fuploadfiles%2Ffgosvob%2F350304.pdf",
                            EduUgsId = 11
                        };

                    EduNapravl eduNapr350306 =
                        new EduNapravl
                        {
                            EduNapravlId = 15,
                            EduNapravlCode = "35.03.06",
                            EduNapravlName = "Агроинженерия",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/486/?f=%2Fuploadfiles%2Ffgosvob%2F350306.pdf",
                            EduUgsId = 11
                        };

                    EduNapravl eduNapr380301 =
                        new EduNapravl
                        {
                            EduNapravlId = 16,
                            EduNapravlCode = "38.03.01",
                            EduNapravlName = "Экономика",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/498/?f=%2Fuploadfiles%2Ffgosvob%2F380301.pdf",
                            EduUgsId = 12
                        };

                    EduNapravl eduNapr380302 =
                        new EduNapravl
                        {
                            EduNapravlId = 17,
                            EduNapravlCode = "38.03.02",
                            EduNapravlName = "Менеджмент",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/545/?f=%2Fuploadfiles%2Ffgosvob%2F380302.pdf",
                            EduUgsId = 12
                        };

                    EduNapravl eduNapr380304 =
                        new EduNapravl
                        {
                            EduNapravlId = 18,
                            EduNapravlCode = "38.03.04",
                            EduNapravlName = "Государственное и муниципальное управление",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/298/?f=%2Fuploadfiles%2Ffgosvob%2F380304_gosmunupr.pdf",
                            EduUgsId = 12
                        };

                    EduNapravl eduNapr440304 =
                        new EduNapravl
                        {
                            EduNapravlId = 19,
                            EduNapravlCode = "44.03.04",
                            EduNapravlName = "Профессиональное обучение",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/473/?f=%2Fuploadfiles%2Ffgosvob%2F440304.pdf",
                            EduUgsId = 13
                        };

                    #endregion

                    #region Направления ВО-магистратура
                    EduNapravl eduNapr130401 =
                        new EduNapravl
                        {
                            EduNapravlId = 20,
                            EduNapravlCode = "13.04.01",
                            EduNapravlName = "Теплоэнергетика и теплотехника",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/205/?f=%2Fuploadfiles%2Ffgosvom%2F130401.pdf",
                            EduUgsId = 14
                        };

                    EduNapravl eduNapr130402 =
                        new EduNapravl
                        {
                            EduNapravlId = 21,
                            EduNapravlCode = "13.04.02",
                            EduNapravlName = "Электроэнергетика и электротехника",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/206/?f=%2Fuploadfiles%2Ffgosvom%2F130402_Elektroenergetika.pdf",
                            EduUgsId = 14
                        };


                    EduNapravl eduNapr230401 =
                        new EduNapravl
                        {
                            EduNapravlId = 22,
                            EduNapravlCode = "23.04.01",
                            EduNapravlName = "Технология транспортных процессов",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/379/?f=%2Fuploadfiles%2Ffgosvom%2F230401.pdf",
                            EduUgsId = 15
                        };

                    EduNapravl eduNapr230403 =
                        new EduNapravl
                        {
                            EduNapravlId = 23,
                            EduNapravlCode = "23.04.03",
                            EduNapravlName = "Эксплуатация транспортно-технологических машин и комплексов",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/299/?f=%2Fuploadfiles%2Ffgosvom%2F230403.pdf",
                            EduUgsId = 15
                        };

                    EduNapravl eduNapr350404 =
                        new EduNapravl
                        {
                            EduNapravlId = 24,
                            EduNapravlCode = "35.04.04",
                            EduNapravlName = "Агрономия",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/412/?f=%2Fuploadfiles%2Ffgosvom%2F350404.pdf",
                            EduUgsId = 16
                        };

                    EduNapravl eduNapr350406 =
                        new EduNapravl
                        {
                            EduNapravlId = 25,
                            EduNapravlCode = "35.04.06",
                            EduNapravlName = "Агроинженерия",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/461/?f=%2Fuploadfiles%2Ffgosvom%2F350406.pdf",
                            EduUgsId = 16
                        };

                    EduNapravl eduNapr380401 =
                        new EduNapravl
                        {
                            EduNapravlId = 26,
                            EduNapravlCode = "38.04.01",
                            EduNapravlName = "Экономика",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/386/?f=%2Fuploadfiles%2Ffgosvom%2F380401.pdf",
                            EduUgsId = 17
                        };

                    EduNapravl eduNapr380402 =
                        new EduNapravl
                        {
                            EduNapravlId = 27,
                            EduNapravlCode = "38.04.02",
                            EduNapravlName = "Менеджмент",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/361/?f=%2Fuploadfiles%2Ffgosvom%2F380402.pdf",
                            EduUgsId = 17
                        };
                    #endregion

                    #region Направления ВО-специалитет 
                    EduNapravl eduNapr230501 =
                        new EduNapravl
                        {
                            EduNapravlId = 28,
                            EduNapravlCode = "23.05.01",
                            EduNapravlName = "Наземные транспортно–технологические средства",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/747/?f=%2Fuploadfiles%2Ffgosvospec%2F230501.pdf",
                            EduUgsId = 18
                        };

                    EduNapravl eduNapr380501 =
                        new EduNapravl
                        {
                            EduNapravlId = 29,
                            EduNapravlCode = "38.05.01",
                            EduNapravlName = "Экономическая безопасность",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/946/?f=%2Fuploadfiles%2Ffgosvospec%2F380501.pdf",
                            EduUgsId = 19
                        };
                    #endregion

                    #region Направления ВО-подготовка кадров высшей квалификации
                    EduNapravl eduNapr060601 =
                        new EduNapravl
                        {
                            EduNapravlId = 30,
                            EduNapravlCode = "06.06.01",
                            EduNapravlName = "Биологические науки",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/99/?f=%2Fuploadfiles%2Ffgosvoaspism%2F060601.pdf",
                            EduUgsId = 20
                        };
                    EduNapravl eduNapr230601 =
                        new EduNapravl
                        {
                            EduNapravlId = 31,
                            EduNapravlCode = "23.06.01",
                            EduNapravlName = "Техника и технологии наземного транспорта",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/113/?f=%2Fuploadfiles%2Ffgosvoaspism%2F230601.pdf",
                            EduUgsId = 21
                        };
                    EduNapravl eduNapr350601 =
                        new EduNapravl
                        {
                            EduNapravlId = 32,
                            EduNapravlCode = "35.06.01",
                            EduNapravlName = "Сельское хозяйство",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/119/?f=%2Fuploadfiles%2Ffgosvoaspism%2F350601.pdf",
                            EduUgsId = 22
                        };
                    EduNapravl eduNapr350604 =
                        new EduNapravl
                        {
                            EduNapravlId = 33,
                            EduNapravlCode = "35.06.04",
                            EduNapravlName = "Технологии, средства механизации и энергетическое оборудование в сельском, лесном и рыбном хозяйстве",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/120/?f=%2Fuploadfiles%2Ffgosvoaspism%2F350604.pdf",
                            EduUgsId = 22
                        };
                    #endregion

                    await context.EduNapravls.AddRangeAsync(new EduNapravl[] { eduNapr080209,eduNapr210205,eduNapr230203,
                        eduNapr350208,eduNapr380201,eduNapr380204,
                        eduNapr130301,eduNapr130302,eduNapr190302,eduNapr200301,eduNapr210302,eduNapr230301,
                        eduNapr230303,eduNapr350304,eduNapr350306,eduNapr380301,eduNapr380302,eduNapr380304,eduNapr440304,
                        eduNapr130401,eduNapr130402,eduNapr230401,eduNapr230403,
                        eduNapr350404,eduNapr350406,eduNapr380401,eduNapr380402,
                        eduNapr230501,eduNapr380501,
                        eduNapr060601,eduNapr230601,eduNapr350601,eduNapr350604
                    });
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Профили подготовки / направленности / специализации"
                if (!await context.EduProfiles.AnyAsync())
                {
                    #region Направленности СПО
                    EduProfile eduProfile080209_9 =
                        new EduProfile
                        {
                            EduProfileId = 1,
                            EduProfileName = "На базе 9 классов",
                            EduNapravlId = 1
                        };
                    EduProfile eduProfile080209_11 =
                        new EduProfile
                        {
                            EduProfileId = 2,
                            EduProfileName = "На базе 11 классов",
                            EduNapravlId = 1
                        };
                    EduProfile eduProfile210205_9 =
                        new EduProfile
                        {
                            EduProfileId = 3,
                            EduProfileName = "На базе 9 классов",
                            EduNapravlId = 2
                        };
                    EduProfile eduProfile210205_11 =
                        new EduProfile
                        {
                            EduProfileId = 4,
                            EduProfileName = "На базе 11 классов",
                            EduNapravlId = 2
                        };
                    EduProfile eduProfile230203_9 =
                        new EduProfile
                        {
                            EduProfileId = 5,
                            EduProfileName = "На базе 9 классов",
                            EduNapravlId = 3
                        };
                    EduProfile eduProfile230203_11 =
                        new EduProfile
                        {
                            EduProfileId = 6,
                            EduProfileName = "На базе 11 классов",
                            EduNapravlId = 3
                        };
                    EduProfile eduProfile350208_9 =
                        new EduProfile
                        {
                            EduProfileId = 7,
                            EduProfileName = "На базе 9 классов",
                            EduNapravlId = 4
                        };
                    EduProfile eduProfile350208_11 =
                        new EduProfile
                        {
                            EduProfileId = 8,
                            EduProfileName = "На базе 11 классов",
                            EduNapravlId = 4
                        };
                    EduProfile eduProfile380201_9 =
                        new EduProfile
                        {
                            EduProfileId = 9,
                            EduProfileName = "На базе 9 классов",
                            EduNapravlId = 5
                        };
                    EduProfile eduProfile380201_11 =
                        new EduProfile
                        {
                            EduProfileId = 10,
                            EduProfileName = "На базе 11 классов",
                            EduNapravlId = 5
                        };
                    EduProfile eduProfile380204_9 =
                        new EduProfile
                        {
                            EduProfileId = 11,
                            EduProfileName = "На базе 9 классов",
                            EduNapravlId = 6
                        };
                    EduProfile eduProfile380204_11 =
                        new EduProfile
                        {
                            EduProfileId = 12,
                            EduProfileName = "На базе 11 классов",
                            EduNapravlId = 6
                        };
                    #endregion

                    #region Профили ВО-бакалавриат
                    EduProfile eduProfile130301 =
                        new EduProfile
                        {
                            EduProfileId = 13,
                            EduProfileName = "Энергообеспечение предприятий",
                            EduNapravlId = 7
                        };

                    EduProfile eduProfile130302 =
                        new EduProfile
                        {
                            EduProfileId = 14,
                            EduProfileName = "Электроснабжение",
                            EduNapravlId = 8
                        };

                    EduProfile eduProfile190302_1 =
                        new EduProfile
                        {
                            EduProfileId = 15,
                            EduProfileName = "Технология хранения и переработки зерна",
                            EduNapravlId = 9
                        };

                    EduProfile eduProfile190302_2 =
                        new EduProfile
                        {
                            EduProfileId = 16,
                            EduProfileName = "Технология продуктов общественного питания",
                            EduNapravlId = 9
                        };

                    EduProfile eduProfile200301 =
                        new EduProfile
                        {
                            EduProfileId = 17,
                            EduProfileName = "Безопасность технологических процессов и производств",
                            EduNapravlId = 10
                        };

                    EduProfile eduProfile210302 =
                        new EduProfile
                        {
                            EduProfileId = 18,
                            EduProfileName = "Эемлеустройство",
                            EduNapravlId = 11
                        };

                    EduProfile eduProfile230301_1 =
                        new EduProfile
                        {
                            EduProfileId = 19,
                            EduProfileName = "Организация перевозок и управления на автомобильном транспорте",
                            EduNapravlId = 12
                        };

                    EduProfile eduProfile230301_2 =
                        new EduProfile
                        {
                            EduProfileId = 20,
                            EduProfileName = "Транспортная логистика",
                            EduNapravlId = 12
                        };

                    EduProfile eduProfile230303_1 =
                        new EduProfile
                        {
                            EduProfileId = 21,
                            EduProfileName = "Автомобили и автомобильное хозяйство",
                            EduNapravlId = 13
                        };

                    EduProfile eduProfile230303_2 =
                        new EduProfile
                        {
                            EduProfileId = 22,
                            EduProfileName = "Автомобильный сервис",
                            EduNapravlId = 13
                        };

                    EduProfile eduProfile350304 =
                        new EduProfile
                        {
                            EduProfileId = 23,
                            EduProfileName = "Селекция и генетика сельскохозяйственных культур",
                            EduNapravlId = 14
                        };

                    EduProfile eduProfile350306_1 =
                        new EduProfile
                        {
                            EduProfileId = 24,
                            EduProfileName = "Электрооборудование и электротехнологии",
                            EduNapravlId = 15
                        };

                    EduProfile eduProfile350306_2 =
                        new EduProfile
                        {
                            EduProfileId = 25,
                            EduProfileName = "Техническая эксплуатация транспортных средств",
                            EduNapravlId = 15
                        };

                    EduProfile eduProfile350306_3 =
                        new EduProfile
                        {
                            EduProfileId = 26,
                            EduProfileName = "Экономика и управление производством",
                            EduNapravlId = 15
                        };

                    EduProfile eduProfile350306_4 =
                        new EduProfile
                        {
                            EduProfileId = 27,
                            EduProfileName = "Технические системы в агробизнесе",
                            EduNapravlId = 15
                        };

                    EduProfile eduProfile350306_5 =
                        new EduProfile
                        {
                            EduProfileId = 28,
                            EduProfileName = "Технический сервис в АПК",
                            EduNapravlId = 15
                        };

                    EduProfile eduProfile350306_6 =
                        new EduProfile
                        {
                            EduProfileId = 29,
                            EduProfileName = "Технологическое оборудование для хранения и переработки сельскохозяйственной продукции",
                            EduNapravlId = 15
                        };

                    EduProfile eduProfile380301 =
                        new EduProfile
                        {
                            EduProfileId = 30,
                            EduProfileName = "Бухгалтерский учет, анализ и аудит",
                            EduNapravlId = 16
                        };

                    EduProfile eduProfile380302 =
                        new EduProfile
                        {
                            EduProfileId = 31,
                            EduProfileName = "Производственный менеджмент",
                            EduNapravlId = 17
                        };

                    EduProfile eduProfile380304 =
                        new EduProfile
                        {
                            EduProfileId = 32,
                            EduProfileName = "Муниципальное управление",
                            EduNapravlId = 18
                        };

                    EduProfile eduProfile440304 =
                        new EduProfile
                        {
                            EduProfileId = 33,
                            EduProfileName = "Экономика и управление",
                            EduNapravlId = 19
                        };
                    #endregion

                    #region Специализации ВО-специалитет
                    EduProfile eduProfile230501 =
                        new EduProfile
                        {
                            EduProfileId = 34,
                            EduProfileName = "Автомобили и тракторы",
                            EduNapravlId = 28
                        };
                    EduProfile eduProfile380501 =
                        new EduProfile
                        {
                            EduProfileId = 35,
                            EduProfileName = "Экономико-правовое обеспечение экономической безопасности",
                            EduNapravlId = 29
                        };
                    #endregion

                    #region Направленность ВО-магистратура
                    EduProfile eduProfile130401 =
                        new EduProfile
                        {
                            EduProfileId = 36,
                            EduProfileName = "Энергообеспечение предприятий",
                            EduNapravlId = 20
                        };

                    EduProfile eduProfile130402 =
                        new EduProfile
                        {
                            EduProfileId = 37,
                            EduProfileName = "Электроснабжение",
                            EduNapravlId = 21
                        };

                    EduProfile eduProfile230401_1 =
                        new EduProfile
                        {
                            EduProfileId = 38,
                            EduProfileName = "Организация перевозок и управления на автомобильном транспорте",
                            EduNapravlId = 22
                        };

                    EduProfile eduProfile230401_2 =
                        new EduProfile
                        {
                            EduProfileId = 39,
                            EduProfileName = "Транспортная логистика",
                            EduNapravlId = 22
                        };

                    EduProfile eduProfile230403_1 =
                        new EduProfile
                        {
                            EduProfileId = 40,
                            EduProfileName = "Автомобили и автомобильное хозяйство",
                            EduNapravlId = 23
                        };

                    EduProfile eduProfile230403_2 =
                        new EduProfile
                        {
                            EduProfileId = 41,
                            EduProfileName = "Автомобильный сервис",
                            EduNapravlId = 23
                        };

                    EduProfile eduProfile350404 =
                        new EduProfile
                        {
                            EduProfileId = 42,
                            EduProfileName = "Селекция, генетика и семеноводство сельскохозяйственных культур",
                            EduNapravlId = 24
                        };

                    EduProfile eduProfile350406_1 =
                        new EduProfile
                        {
                            EduProfileId = 43,
                            EduProfileName = "Технологии и технические средства производства сельскохозяйственной продукции",
                            EduNapravlId = 25
                        };

                    EduProfile eduProfile350406_2 =
                        new EduProfile
                        {
                            EduProfileId = 44,
                            EduProfileName = "Надежность технических средств в агропромышленном комплексе",
                            EduNapravlId = 25
                        };

                    EduProfile eduProfile350406_3 =
                        new EduProfile
                        {
                            EduProfileId = 45,
                            EduProfileName = "Электрооборудование и электротехнологии",
                            EduNapravlId = 25
                        };

                    EduProfile eduProfile380401 =
                        new EduProfile
                        {
                            EduProfileId = 46,
                            EduProfileName = "Бухгалтерский учет и налогообложение",
                            EduNapravlId = 26
                        };

                    EduProfile eduProfile380402 =
                        new EduProfile
                        {
                            EduProfileId = 47,
                            EduProfileName = "Стратегический менеджмент",
                            EduNapravlId = 27
                        };
                    #endregion

                    #region Специализации ВО-подготовка кадров высшей квалификации
                    EduProfile eduProfile060601 =
                        new EduProfile
                        {
                            EduProfileId = 48,
                            EduProfileName = "Физиология и биохимия растений",
                            EduNapravlId = 30
                        };
                    EduProfile eduProfile230601 =
                        new EduProfile
                        {
                            EduProfileId = 49,
                            EduProfileName = "Эксплуатация автомобильного транспорта",
                            EduNapravlId = 31
                        };
                    EduProfile eduProfile350601 =
                        new EduProfile
                        {
                            EduProfileId = 50,
                            EduProfileName = "Селекция и семеноводство сельскохозяйственных растений",
                            EduNapravlId = 32
                        };
                    EduProfile eduProfile350604_1 =
                        new EduProfile
                        {
                            EduProfileId = 51,
                            EduProfileName = "Технологии и средства механизации сельского хозяйства",
                            EduNapravlId = 33
                        };
                    EduProfile eduProfile350604_2 =
                        new EduProfile
                        {
                            EduProfileId = 52,
                            EduProfileName = "Электротехнологии и электрооборудование в сельском хозяйстве",
                            EduNapravlId = 33
                        };
                    EduProfile eduProfile350604_3 =
                        new EduProfile
                        {
                            EduProfileId = 53,
                            EduProfileName = "Технологии и средства технического обслуживания в сельском хозяйстве",
                            EduNapravlId = 33
                        };
                    #endregion

                    await context.EduProfiles.AddRangeAsync(new EduProfile[] {
                        eduProfile080209_9, eduProfile080209_11,
                        eduProfile210205_9, eduProfile210205_11,
                        eduProfile230203_9, eduProfile230203_11,
                        eduProfile350208_9, eduProfile350208_11,
                        eduProfile380201_9, eduProfile380201_11,
                        eduProfile380204_9, eduProfile380204_11,

                        eduProfile130301,
                        eduProfile130302,
                        eduProfile190302_1, eduProfile190302_2,
                        eduProfile200301,
                        eduProfile210302,
                        eduProfile230301_1, eduProfile230301_2,
                        eduProfile230303_1, eduProfile230303_2,
                        eduProfile350304,
                        eduProfile350306_1, eduProfile350306_2, eduProfile350306_3, eduProfile350306_4, eduProfile350306_5, eduProfile350306_6,
                        eduProfile380301,
                        eduProfile380302,
                        eduProfile380304,
                        eduProfile440304,

                        eduProfile380501,
                        eduProfile230501,

                        eduProfile130401,
                        eduProfile130402,
                        eduProfile230401_1, eduProfile230401_2,
                        eduProfile230403_1, eduProfile230403_2,
                        eduProfile350404,
                        eduProfile350406_1, eduProfile350406_2, eduProfile350406_3,
                        eduProfile380401,
                        eduProfile380402,

                        eduProfile060601,
                        eduProfile230601,
                        eduProfile350601,
                        eduProfile350604_1, eduProfile350604_2, eduProfile350604_3
                    });
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Формы обучения"
                if (!await context.EduForms.AnyAsync())
                {
                    EduForm eduFormOchn = new EduForm
                    {
                        EduFormId = 1,
                        EduFormName = "Очная"
                    };
                    EduForm eduFormOchnUsk = new EduForm
                    {
                        EduFormId = 2,
                        EduFormName = "Очная ускоренная"
                    };
                    EduForm eduFormZaochn = new EduForm
                    {
                        EduFormId = 3,
                        EduFormName = "Заочная"
                    };
                    EduForm eduFormZaochnUsk = new EduForm
                    {
                        EduFormId = 4,
                        EduFormName = "Заочная ускоренная"
                    };
                    EduForm eduFormOchnZaoch = new EduForm
                    {
                        EduFormId = 5,
                        EduFormName = "Очно-заочная"
                    };

                    await context.EduForms.AddRangeAsync(new EduForm[] { eduFormOchn,
                        eduFormOchnUsk,eduFormZaochn,eduFormZaochnUsk,eduFormOchnZaoch
                    });
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Годы обучения"
                if (!await context.EduYears.AnyAsync())
                {
                    EduYear eduYear1718 = new EduYear
                    {
                        EduYearId = 1,
                        EduYearName = "2017-2018"
                    };

                    await context.EduYears.AddRangeAsync(new EduYear[] { eduYear1718 });
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Годы начала подготовки"
                if (!await context.EduYearBeginningTrainings.AnyAsync())
                {
                    EduYearBeginningTraining eduYear13 = new EduYearBeginningTraining
                    {
                        EduYearBeginningTrainingId = 1,
                        EduYearBeginningTrainingName = "2013"
                    };

                    EduYearBeginningTraining eduYear14 = new EduYearBeginningTraining
                    {
                        EduYearBeginningTrainingId = 2,
                        EduYearBeginningTrainingName = "2014"
                    };

                    EduYearBeginningTraining eduYear15 = new EduYearBeginningTraining
                    {
                        EduYearBeginningTrainingId = 3,
                        EduYearBeginningTrainingName = "2015"
                    };

                    EduYearBeginningTraining eduYear16 = new EduYearBeginningTraining
                    {
                        EduYearBeginningTrainingId = 4,
                        EduYearBeginningTrainingName = "2016"
                    };

                    EduYearBeginningTraining eduYear17 = new EduYearBeginningTraining
                    {
                        EduYearBeginningTrainingId = 5,
                        EduYearBeginningTrainingName = "2017"
                    };

                    await context.EduYearBeginningTrainings.AddRangeAsync(new EduYearBeginningTraining[] { eduYear13,
                        eduYear14, eduYear15, eduYear16, eduYear17 });
                    await context.SaveChangesAsync();
                }
                #endregion                                
            }
        }

        /// <summary>
        /// Инициализация структуры образовательной организации
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateStructData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Адреса"
                if (!await context.Addresses.AnyAsync())
                {
                    Address addressPers = new Address
                    {
                        AddressId = 1,
                        PostCode = "346493",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "Октябрьский район, п. Персиановский",
                        Street = "ул. Кривошлыкова",
                        HouseNumber = "24"
                    };          //1
                    Address addressLenina19 = new Address
                    {
                        AddressId = 2,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "19"
                    };      //2
                    Address addressLenina19_a = new Address
                    {
                        AddressId = 3,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "19a"
                    };    //3
                    Address addressLenina21 = new Address
                    {
                        AddressId = 4,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "21"
                    };      //4
                    Address addressLenina25_29 = new Address
                    {
                        AddressId = 5,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "25/29"
                    };   //5
                    Address addressSovetskaya15_4 = new Address
                    {
                        AddressId = 6,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "15/4"
                    };//6
                    Address addressSovetskaya17 = new Address
                    {
                        AddressId = 7,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "17"
                    };  //7
                    Address addressSovetskaya17_21 = new Address
                    {
                        AddressId = 8,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "17/21"
                    };//8
                    Address addressSovetskaya19 = new Address
                    {
                        AddressId = 9,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "19"
                    };  //9
                    Address addressSovetskaya21_a = new Address
                    {
                        AddressId = 10,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "21a"
                    };//10
                    Address addressSovetskaya23 = new Address
                    {
                        AddressId = 11,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "23"
                    };  //11                    
                    Address addressSovetskaya28_30 = new Address
                    {
                        AddressId = 12,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "28/30"
                    };//12
                    Address addressSelercionniy34 = new Address
                    {
                        AddressId = 13,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "пер. Селекционный",
                        HouseNumber = "34"
                    };  //13
                    Address addressTelmana36 = new Address
                    {
                        AddressId = 14,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Тельмана",
                        HouseNumber = "36"
                    };      //14
                    Address addressTelmana61_2a = new Address
                    {
                        AddressId = 15,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Тельмана",
                        HouseNumber = "61/2a"
                    };  //15
                    Address addressSalsk = new Address
                    {
                        AddressId = 16,
                        PostCode = "347603",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "Сальский район, п. Конный завод им.Буденного",
                        Street = "База отдыха, строение",
                        HouseNumber = "2"
                    };          //16
                    await context.Addresses.AddRangeAsync(
                        addressPers,
                        addressLenina19,
                        addressLenina19_a,
                        addressLenina21,
                        addressLenina25_29,
                        addressSovetskaya15_4,
                        addressSovetskaya17,
                        addressSovetskaya17_21,
                        addressSovetskaya19,
                        addressSovetskaya21_a,
                        addressSovetskaya23,
                        addressSovetskaya28_30,
                        addressSelercionniy34,
                        addressTelmana36,
                        addressTelmana61_2a,
                        addressSalsk
                        );
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Университеты"
                if (!await context.StructUniversities.AnyAsync())
                {
                    StructUniversity university = new StructUniversity
                    {
                        StructUniversityId = 1,
                        StructUniversityName = "ФГБОУ ВО Донской ГАУ",
                        DateOfCreation = new DateTime(1993, 07, 05),
                        ExistenceOfFilials = true,
                        WorkingRegime = "Режим работы: вход в учебные корпуса и общежития — по пропускам и студенческим билетам",
                        WorkingSchedule = "График работы: понедельник-пятница с 8.00 до 17.00. Выходной — суббота, воскресенье",
                        AddressId = 1
                    };//ФГБОУ ВО Донской ГАУ

                    await context.StructUniversities.AddAsync(university);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Институты"
                if (!await context.StructInstitutes.AnyAsync())
                {
                    StructInstitute institute = new StructInstitute
                    {
                        StructInstituteId = 1,
                        StructInstituteName = "Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ",
                        DateOfCreation = new DateTime(2014, 09, 15),
                        ExistenceOfFilials = false,
                        WorkingRegime = "Режим работы: вход в учебные корпуса и общежития — по пропускам и студенческим билетам",
                        WorkingSchedule = "График работы: понедельник-пятница с 8.00 до 17.00. Выходной — суббота, воскресенье",
                        UniversityId = 1,
                        AddressId = 4,
                        Faxes = new List<Fax> { new Fax { FaxValue = "(886359) 43-3-80", FaxComment = "Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ" } },
                        Emailes = new List<Email> { new Email { EmailValue = "achgaa@achgaa.ru", EmailComment = "Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ" } },
                        Telephones = new List<Telephone>
                        {
                            new Telephone{TelephoneNumber="(886359) 41-8-31", TelephoneComment="Приемная комиссия"},
                            new Telephone{TelephoneNumber="(886359) 43-3-80", TelephoneComment="Канцелярия"},
                            new Telephone{TelephoneNumber="(886359) 42-6-78", TelephoneComment="Факультет  «Инженерно-технологический» "},
                            new Telephone{TelephoneNumber="(886359) 41-6-56", TelephoneComment="Факультет «Энергетический»"},
                            new Telephone{TelephoneNumber="(886359) 42-1-98", TelephoneComment="Факультет «Экономика и управление территориями»"},
                            new Telephone{TelephoneNumber="(886359) 35-9-96", TelephoneComment="Факультет «Среднее профессиональное образование»"},
                        }
                    };// Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ

                    await context.StructInstitutes.AddAsync(institute);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Типы структурных подразделений"
                if (!await context.StructSubvisionTypes.AnyAsync())
                {
                    StructSubvisionType structSubvisionType1 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 1,
                        StructSubvisionTypeName = "Органы управления образовательной организации"
                    };// Органы управления образовательной организации

                    StructSubvisionType structSubvisionType2 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 2,
                        StructSubvisionTypeName = "Структурные подразделения образовательной организации"
                    };// Структурные подразделения образовательной организации

                    StructSubvisionType structSubvisionType3 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 3,
                        StructSubvisionTypeName = "Факультеты"
                    };// Факультеты

                    StructSubvisionType structSubvisionType4 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 4,
                        StructSubvisionTypeName = "Кафедры"
                    };// Кафедры

                    await context.StructSubvisionTypes.AddRangeAsync(structSubvisionType1, structSubvisionType2, structSubvisionType3, structSubvisionType4);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Должности"
                if (!await context.Posts.AnyAsync())
                {                    
                    Post post1 = new Post
                    {
                        PostId = 1,
                        PostName = "Директор"
                    };//Директор
                    Post post2 = new Post
                    {
                        PostId = 2,
                        PostName = "Исполняющий обязанности заместителя директора по учебной работе"
                    };//Исполняющий обязанности заместителя директора по учебной работе
                    Post post3 = new Post
                    {
                        PostId = 3,
                        PostName = "Заместитель директора по научной работе"
                    };//Заместитель директора по научной работе               
                    Post post4 = new Post
                    {
                        PostId = 4,
                        PostName = "Исполняющий обязанности заместителя директора по социальной работе"
                    };//Исполняющий обязанности заместителя директора по социальной работе
                    Post post5 = new Post
                    {
                        PostId = 5,
                        PostName = "Исполняющий обязанности заместителя директора по воспитательной работе"
                    };//Исполняющий обязанности заместителя директора по воспитательной работе
                    Post post6 = new Post
                    {
                        PostId = 6,
                        PostName = "Заместитель директора по административно-хозяйственной работе"
                    };//Заместитель директора по административно-хозяйственной работе
                    Post post7 = new Post
                    {
                        PostId = 7,
                        PostName = "Исполняющий обязанности заместителя директора по связям с общественностью"
                    };//Исполняющий обязанности заместителя директора по связям с общественностью
                    Post post8 = new Post
                    {
                        PostId = 8,
                        PostName = "Главный бухгалтер"
                    };//Главный бухгалтер
                    Post post9 = new Post
                    {
                        PostId = 9,
                        PostName = "Начальник"
                    };//Начальник
                    Post post10 = new Post
                    {
                        PostId = 10,
                        PostName = "Заведующий архивом"
                    };//Заведующий архивом
                    Post post11 = new Post
                    {
                        PostId = 11,
                        PostName = "Ответственный секретарь приемной комиссии"
                    };//Ответственный секретарь приемной комиссии
                    Post post12 = new Post
                    {
                        PostId = 12,
                        PostName = "Инструктор ГО"
                    };//Инструктор ГО
                    Post post13 = new Post
                    {
                        PostId = 13,
                        PostName = "Декан"
                    };//Декан
                    Post post14 = new Post
                    {
                        PostId = 14,
                        PostName = "Исполняющий обязанности декана"
                    };//Исполняющий обязанности декана
                    Post post15 = new Post
                    {
                        PostId = 15,
                        PostName = "Заведующий кафедрой"
                    };//Заведующий кафедрой
                    Post post16 = new Post
                    {
                        PostId = 16,
                        PostName = "Исполняющий обязанности заведующего кафедрой"
                    };//Исполняющий обязанности заведующего кафедрой
                    Post post17 = new Post
                    {
                        PostId = 17,
                        PostName = "Заведующий научно-технической библиотекой"
                    };//Заведующий научно-технической библиотекой
                    Post post18 = new Post
                    {
                        PostId = 18,
                        PostName = "Заведующий"
                    };//Заведующий
                    Post post19 = new Post
                    {
                        PostId = 19,
                        PostName = "Заведующий студенческим общежитием №1"
                    };//Заведующий студенческим общежитием №1
                    Post post20 = new Post
                    {
                        PostId = 20,
                        PostName = "Заведующий студенческим общежитием №2"
                    };//Заведующий студенческим общежитием №2
                    Post post21 = new Post
                    {
                        PostId = 21,
                        PostName = "Заведующий студенческим общежитием №3"
                    };//Заведующий студенческим общежитием №3
                    Post post22 = new Post
                    {
                        PostId = 22,
                        PostName = "Заведующий студенческим общежитием №4"
                    };//Заведующий студенческим общежитием №4
                    Post post23 = new Post
                    {
                        PostId = 23,
                        PostName = "Заведующий студенческим общежитием №5"
                    };//Заведующий студенческим общежитием №5
                    Post post24 = new Post
                    {
                        PostId = 24,
                        PostName = "Главный инженер"
                    };//Главный инженер
                    Post post25 = new Post
                    {
                        PostId = 25,
                        PostName = "Комендант учебного корпуса №1"
                    };//Комендант учебного корпуса №1
                    Post post26 = new Post
                    {
                        PostId = 26,
                        PostName = "Комендант учебного корпуса №2"
                    };//Комендант учебного корпуса №2
                    Post post27 = new Post
                    {
                        PostId = 27,
                        PostName = "Комендант учебного корпуса №3"
                    };//Комендант учебного корпуса №3
                    Post post28 = new Post
                    {
                        PostId = 28,
                        PostName = "Комендант административного корпуса"
                    };//Комендант административного корпуса
                    Post post29 = new Post
                    {
                        PostId = 29,
                        PostName = "Комендант учебного корпуса №5"
                    };//Комендант учебного корпуса №5
                    Post post30 = new Post
                    {
                        PostId = 30,
                        PostName = "Комендант учебного корпуса №6"
                    };//Комендант учебного корпуса №6
                    Post post31 = new Post
                    {
                        PostId = 31,
                        PostName = "Комендант учебного корпуса №7"
                    };//Комендант учебного корпуса №7                    
                    Post post32 = new Post
                    {
                        PostId = 32,
                        PostName = "Ассистент"
                    };//Ассистент
                    Post post33 = new Post
                    {
                        PostId = 33,
                        PostName = "Старший преподаватель"
                    };//Старший преподаватель
                    Post post34 = new Post
                    {
                        PostId = 34,
                        PostName = "Доцент"
                    };//Доцент
                    Post post35 = new Post
                    {
                        PostId = 35,
                        PostName = "Профессор"
                    };//Профессор
                    await context.Posts.AddRangeAsync(
                        post1, post2, post3, post4, post5, post6, post7, post8, post9, post10, 
                        post11, post12, post13, post14, post15, post16, post17, post18, post19, post20,
                        post21, post22, post23, post24, post25, post26, post27, post28, post29, post30,
                        post31, post32, post33, post34, post35);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Структурные подразделения"
                if (!await context.StructSubvisions.AnyAsync())
                {
                    StructSubvision structSubvision1 = new StructSubvision
                    {
                        StructSubvisionId = 1,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Серёгин Александр Анатольевич",
                        StructSubvisionPostChiefId = 1,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "achgaa@achgaa.ru", EmailComment = "Дирекция" },
                        StructSubvisionTypeId = 1
                    };//Директор
                    StructSubvision structSubvision2 = new StructSubvision
                    {
                        StructSubvisionId = 2,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Глечикова Наталья Александровна",
                        StructSubvisionPostChiefId = 2,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Исполняющий обязанности заместителя директора по учебной работе" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по учебной работе
                    StructSubvision structSubvision3 = new StructSubvision
                    {
                        StructSubvisionId = 3,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Юдаев Игорь Викторович",
                        StructSubvisionPostChiefId = 3,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                       //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Заместитель директора по научной работе" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по научной работе
                    StructSubvision structSubvision4 = new StructSubvision
                    {
                        StructSubvisionId = 4,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Кабанов Александр Николаевич",
                        StructSubvisionPostChiefId = 4,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Исполняющий обязанности заместителя директора по социальной работе" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по социальной работе
                    StructSubvision structSubvision5 = new StructSubvision
                    {
                        StructSubvisionId = 5,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Асатурян Сергей Вартанович",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Исполняющий обязанности заместителя директора по воспитательной работе" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по воспитательной работе
                    StructSubvision structSubvision6 = new StructSubvision
                    {
                        StructSubvisionId = 6,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Джанибеков Казбек Азрет-Алиевич",
                        StructSubvisionPostChiefId = 6,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Заместитель директора по административно-хозяйственной работе" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по административно-хозяйственной работе
                    StructSubvision structSubvision7 = new StructSubvision
                    {
                        StructSubvisionId = 7,
                        StructSubvisionName = "Дирекция",
                        StructSubvisionFioChief = "Бондаренко Анатолий Михайлович",
                        StructSubvisionPostChiefId = 7,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Исполняющий обязанности заместителя директора по связям с общественностью" },
                        StructSubvisionTypeId = 1
                    };//Заместитель директора по связям с общественностью
                    StructSubvision structSubvision8 = new StructSubvision
                    {
                        StructSubvisionId = 8,
                        StructSubvisionName = "Отдел финансового планирования и бухгалтерского учета",
                        StructSubvisionFioChief = "Поваляева Елена Петровна",
                        StructSubvisionPostChiefId = 8,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "achgaa@itog.biz", EmailComment = "Отдел финансового планирования и бухгалтерского учета" },
                        StructSubvisionTypeId = 2
                    };//Отдел финансового планирования и бухгалтерского учета
                    StructSubvision structSubvision9 = new StructSubvision
                    {
                        StructSubvisionId = 9,
                        StructSubvisionName = "Отдел кадрового и документационного обеспечения",
                        StructSubvisionFioChief = "Головина Наталья Юрьевна",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "kadr@achgaa.ru", EmailComment = "Отдел кадрового и документационного обеспечения" },
                        StructSubvisionTypeId = 2
                    };//Отдел кадрового и документационного обеспечения
                    StructSubvision structSubvision10 = new StructSubvision
                    {
                        StructSubvisionId = 10,
                        StructSubvisionName = "Архив",
                        StructSubvisionFioChief = "Голодная Татьяна Николаевна",
                        StructSubvisionPostChiefId = 10,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Архив" },
                        StructSubvisionTypeId = 2
                    };//Архив
                    StructSubvision structSubvision11 = new StructSubvision
                    {
                        StructSubvisionId = 11,
                        StructSubvisionName = "Приемная комиссия",
                        StructSubvisionFioChief = "Должиков Валерий Викторович",
                        StructSubvisionPostChiefId = 11,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "priem@achgaa.ru", EmailComment = "Приемная комиссия" },
                        StructSubvisionTypeId = 2
                    };//Приемная комиссия
                    StructSubvision structSubvision12 = new StructSubvision
                    {
                        StructSubvisionId = 12,
                        StructSubvisionName = "Отдел информационных технологий и издательской деятельности",
                        StructSubvisionFioChief = "Лопатин Андрей Дмитриевич",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Отдел информационных технологий и издательской деятельности" },
                        StructSubvisionTypeId = 2
                    };//Отдел информационных технологий и издательской деятельности
                    StructSubvision structSubvision13 = new StructSubvision
                    {
                        StructSubvisionId = 13,
                        StructSubvisionName = "Штаб гражданской обороны и чрезвычайных ситуаций",
                        StructSubvisionFioChief = "Мангуш Антон Петрович",
                        StructSubvisionPostChiefId = 12,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Штаб гражданской обороны и чрезвычайных ситуаций" },
                        StructSubvisionTypeId = 2
                    };//Штаб гражданской обороны и чрезвычайных ситуаций
                    StructSubvision structSubvision14 = new StructSubvision
                    {
                        StructSubvisionId = 14,
                        StructSubvisionName = "Учебный отдел",
                        StructSubvisionFioChief = "Лашина Тамара Ашотовна",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "uotdel@achgaa.ru", EmailComment = "Учебный отдел" },
                        StructSubvisionTypeId = 2
                    };//Учебный отдел
                    StructSubvision structSubvisionSpo = new StructSubvision
                    {
                        StructSubvisionId = 15,
                        StructSubvisionName = "Факультет среднего профессионального образования",
                        StructSubvisionFioChief = "Черемисин Юрий Михайлович",
                        StructSubvisionPostChiefId = 13,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "dekspo@mail.ru", EmailComment = "Факультет среднего профессионального образования" },
                        StructSubvisionTypeId = 3
                    };//Факультет среднего профессионального образования
                    StructSubvision structSubvisionInjTech = new StructSubvision
                    {
                        StructSubvisionId = 16,
                        StructSubvisionName = "Инженерно-технологический факультет",
                        StructSubvisionFioChief = "Глобин Андрей Николаевич",
                        StructSubvisionPostChiefId = 13,
                        StructSubvisionAdressId =11,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "atfachgaa@mail.ru", EmailComment = "Инженерно-технологический факультет" },
                        StructSubvisionTypeId = 3
                    };//Инженерно-технологический факультет
                    StructSubvision structSubvisionEnerg = new StructSubvision
                    {
                        StructSubvisionId = 17,
                        StructSubvisionName = "Энергетический факультет",
                        StructSubvisionFioChief = "Степанчук Геннадий Владимирович",
                        StructSubvisionPostChiefId = 13,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Энергетический факультет" },
                        StructSubvisionTypeId = 3
                    };//Энергетический факультет
                    StructSubvision structSubvisionEconom = new StructSubvision
                    {
                        StructSubvisionId = 18,
                        StructSubvisionName = "Факультет \"Экономика и управление территориями\"",
                        StructSubvisionFioChief = "Рева Алла Федоровна",
                        StructSubvisionPostChiefId = 13,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "achgaa@itog.biz", EmailComment = "Факультет \"Экономика и управление территориями\"" },
                        StructSubvisionTypeId = 3
                    };//Факультет \"Экономика и управление территориями\"
                    StructSubvision structSubvisionKafTiIuS = new StructSubvision
                    {
                        StructSubvisionId = 19,
                        StructSubvisionName = "Кафедра \"Теплоэнергетика и информационно-управляющие системы\"",
                        StructSubvisionFioChief = "Литвинов Владимир Николаевич",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "itius@achgaa.ru", EmailComment = "Кафедра \"Теплоэнергетика и информационно-управляющие системы\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Теплоэнергетика и информационно-управляющие системы\"
                    StructSubvision structSubvisionKafTbiF = new StructSubvision
                    {
                        StructSubvisionId = 20,
                        StructSubvisionName = "Кафедра \"Техносферная безопасность и физика\"",
                        StructSubvisionFioChief = "Шабанов Николай Иванович",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Кафедра \"Техносферная безопасность и физика\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Техносферная безопасность и физика\"
                    StructSubvision structSubvisionKafFiS = new StructSubvision
                    {
                        StructSubvisionId = 21,
                        StructSubvisionName = "Кафедра \"Физвоспитание и спорт\"",
                        StructSubvisionFioChief = "Пятикопов Сергей Михайлович",
                        StructSubvisionPostChiefId = 16,
                        StructSubvisionAdressId = 3,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "sport@achgaa.ru", EmailComment = "Кафедра \"Физвоспитание и спорт\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Физвоспитание и спорт\"
                    StructSubvision structSubvisionKafEEOiEM = new StructSubvision
                    {
                        StructSubvisionId = 22,
                        StructSubvisionName = "Кафедра \"Эксплуатация энергетического оборудования и электрических машин\"",
                        StructSubvisionFioChief = "Таранов Михаил Алексеевич",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "expl_el_mach@achgaa.ru", EmailComment = "Кафедра \"Эксплуатация энергетического оборудования и электрических машин\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Эксплуатация энергетического оборудования и электрических машин\"
                    StructSubvision structSubvisionKafEEiET = new StructSubvision
                    {
                        StructSubvisionId = 23,
                        StructSubvisionName = "Кафедра \"Электроэнергетика и электротехника\"",
                        StructSubvisionFioChief = "Забродина Ольга Борисовна",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "toeеs@achgaa.ru", EmailComment = "Кафедра \"Электроэнергетика и электротехника\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Электроэнергетика и электротехника\"
                    StructSubvision structSubvisionKafBUAiA = new StructSubvision
                    {
                        StructSubvisionId = 24,
                        StructSubvisionName = "Кафедра \"Бухгалтерский учет, анализ и аудит\"",
                        StructSubvisionFioChief = "Чумакова Наталья Валерьевна",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "account@achgaa.ru", EmailComment = "Кафедра \"Бухгалтерский учет, анализ и аудит\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Бухгалтерский учет, анализ и аудит\"
                    StructSubvision structSubvisionKafGDiInYaz = new StructSubvision
                    {
                        StructSubvisionId = 25,
                        StructSubvisionName = "Кафедра \"Гуманитарные дисциплины и иностранные языки\"",
                        StructSubvisionFioChief = "Глушко Ирина Васильевна",
                        StructSubvisionPostChiefId = 16,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "kppiinjz@achgaa.ru", EmailComment = "Кафедра \"Гуманитарные дисциплины и иностранные языки\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Гуманитарные дисциплины и иностранные языки\"
                    StructSubvision structSubvisionKafZUiK = new StructSubvision
                    {
                        StructSubvisionId = 26,
                        StructSubvisionName = "Кафедра \"Землеустройство и кадастры\"",
                        StructSubvisionFioChief = "Бондаренко Анатолий Михайлович",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Кафедра \"Землеустройство и кадастры\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Землеустройство и кадастры\"
                    StructSubvision structSubvisionEiU = new StructSubvision
                    {
                        StructSubvisionId = 27,
                        StructSubvisionName = "Кафедра \"Экономика и управление\"",
                        StructSubvisionFioChief = "Рева Алла Федоровна",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "ekonom_i_upravlenie@achgaa.ru", EmailComment = "Кафедра \"Экономика и управление\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Экономика и управление\"
                    StructSubvision structSubvisionAiSsk = new StructSubvision
                    {
                        StructSubvisionId = 28,
                        StructSubvisionName = "Кафедра \"Агрономия и селекция с/х культур\"",
                        StructSubvisionFioChief = "Хронюк Василий Борисович",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "tr@achgaa.ru", EmailComment = "Кафедра \"Агрономия и селекция с/х культур\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Агрономия и селекция с/х культур\"
                    StructSubvision structSubvisionVMiM = new StructSubvision
                    {
                        StructSubvisionId = 29,
                        StructSubvisionName = "Кафедра \"Высшая математика и механика\"",
                        StructSubvisionFioChief = "Степовой Дмитрий Владимирович",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Кафедра \"Высшая математика и механика\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Высшая математика и механика\"
                    StructSubvision structSubvisionTSinAPK = new StructSubvision
                    {
                        StructSubvisionId = 30,
                        StructSubvisionName = "Кафедра \"Технический сервис в агропромышленном комплексе\"",
                        StructSubvisionFioChief = "Никитченко Сергей Леонидович",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 8,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "kaf-ts@achgaa.ru", EmailComment = "Кафедра \"Технический сервис в агропромышленном комплексе\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Технический сервис в агропромышленном комплексе\"
                    StructSubvision structSubvisionTiSmAPK = new StructSubvision
                    {
                        StructSubvisionId = 31,
                        StructSubvisionName = "Кафедра \"Технологии и средства механизации агропромышленного комплекса\"",
                        StructSubvisionFioChief = "Толстоухова Татьяна Николаевна",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "mtppshp@achgaa.ru", EmailComment = "Кафедра \"Технологии и средства механизации агропромышленного комплекса\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Технологии и средства механизации агропромышленного комплекса\"
                    StructSubvision structSubvisionTiA = new StructSubvision
                    {
                        StructSubvisionId = 32,
                        StructSubvisionName = "Кафедра \"Тракторы и автомобили\"",
                        StructSubvisionFioChief = "Нагорский Леонид Алексеевич",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 8,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Кафедра \"Тракторы и автомобили\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Тракторы и автомобили\"
                    StructSubvision structSubvisionEAiTTP = new StructSubvision
                    {
                        StructSubvisionId = 33,
                        StructSubvisionName = "Кафедра \"Эксплуатация автомобилей и технологии транспортных процессов\"",
                        StructSubvisionFioChief = "Щиров Владимир Николаевич",
                        StructSubvisionPostChiefId = 15,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Кафедра \"Эксплуатация автомобилей и технологии транспортных процессов\"" },
                        StructSubvisionTypeId = 4
                    };//Кафедра \"Эксплуатация автомобилей и технологии транспортных процессов\"
                    StructSubvision structSubvision15 = new StructSubvision
                    {
                        StructSubvisionId = 34,
                        StructSubvisionName = "Библиотека",
                        StructSubvisionFioChief = "Мирошникова Людмила Федоровна",
                        StructSubvisionPostChiefId = 17,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "biblioteka@achgaa.ru", EmailComment = "Библиотека" },
                        StructSubvisionTypeId = 2
                    };//Библиотека
                    StructSubvision structSubvision16 = new StructSubvision
                    {
                        StructSubvisionId = 35,
                        StructSubvisionName = "Учебный центр прикладных квалификаций",
                        StructSubvisionFioChief = "Иванов Павел Александрович",
                        StructSubvisionPostChiefId = 18,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный центр прикладных квалификаций" },
                        StructSubvisionTypeId = 2
                    };//Учебный центр прикладных квалификаций
                    StructSubvision structSubvision17 = new StructSubvision
                    {
                        StructSubvisionId = 36,
                        StructSubvisionName = "Агротехнологический центр",
                        StructSubvisionFioChief = "Меркулов Александр Филиппович",
                        StructSubvisionPostChiefId = 1,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Агротехнологический центр" },
                        StructSubvisionTypeId = 2
                    };//Агротехнологический центр
                    StructSubvision structSubvision18 = new StructSubvision
                    {
                        StructSubvisionId = 37,
                        StructSubvisionName = "Центр инжиниринга и трансфера",
                        StructSubvisionFioChief = "Хижняк Владимир Иванович",
                        StructSubvisionPostChiefId = 1,
                        StructSubvisionAdressId = 7,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Центр инжиниринга и трансфера" },
                        StructSubvisionTypeId = 2
                    };//Центр инжиниринга и трансфера
                    StructSubvision structSubvision19 = new StructSubvision
                    {
                        StructSubvisionId = 38,
                        StructSubvisionName = "Учебно-научно-производственная агротехнологическая лаборатория",
                        StructSubvisionFioChief = "Кувшинова Елена Константиновна",
                        StructSubvisionPostChiefId = 18,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебно-научно-производственная агротехнологическая лаборатория" },
                        StructSubvisionTypeId = 2
                    };//Учебно-научно-производственная агротехнологическая лаборатория
                    StructSubvision structSubvision20 = new StructSubvision
                    {
                        StructSubvisionId = 39,
                        StructSubvisionName = "Центр профессиональной ориентации и работы с молодежью",
                        StructSubvisionFioChief = "Черноусов Иван Николаевич",
                        StructSubvisionPostChiefId = 18,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Центрпрофессиональ ной ориентации и работы с молодежью" },
                        StructSubvisionTypeId = 2
                    };//Центр профессиональной ориентации и работы с молодежью
                    StructSubvision structSubvision21 = new StructSubvision
                    {
                        StructSubvisionId = 40,
                        StructSubvisionName = "Комбинат студенческого питания",
                        StructSubvisionFioChief = "Айнушов Адиль Искендер оглы",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 13,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Комбинат студенческого питания" },
                        StructSubvisionTypeId = 2
                    };//Комбинат студенческого питания
                    StructSubvision structSubvision22 = new StructSubvision
                    {
                        StructSubvisionId = 41,
                        StructSubvisionName = "База отдыха",
                        StructSubvisionFioChief = "Рогозин Сергей Федорович",
                        StructSubvisionPostChiefId = 18,
                        StructSubvisionAdressId = 16,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "База отдыха" },
                        StructSubvisionTypeId = 2
                    };//База отдыха
                    StructSubvision structSubvision23 = new StructSubvision
                    {
                        StructSubvisionId = 42,
                        StructSubvisionName = "Студенческий городок. Общежитие №1",
                        StructSubvisionFioChief = "Шевченко Светлана Георгиевна",
                        StructSubvisionPostChiefId = 19,
                        StructSubvisionAdressId = 9,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Студенческий городок. Общежитие №1" },
                        StructSubvisionTypeId = 2
                    };//Студенческий городок. Общежитие №1
                    StructSubvision structSubvision24 = new StructSubvision
                    {
                        StructSubvisionId = 43,
                        StructSubvisionName = "Студенческий городок. Общежитие №2",
                        StructSubvisionFioChief = "Мирошникова лариса Алексеевна",
                        StructSubvisionPostChiefId = 20,
                        StructSubvisionAdressId = 11,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Студенческий городок. Общежитие №2" },
                        StructSubvisionTypeId = 2
                    };//Студенческий городок. Общежитие №2
                    StructSubvision structSubvision25 = new StructSubvision
                    {
                        StructSubvisionId = 44,
                        StructSubvisionName = "Студенческий городок. Общежитие №3",
                        StructSubvisionFioChief = "Ляхова Татьяна Михайловна",
                        StructSubvisionPostChiefId = 21,
                        StructSubvisionAdressId = 5,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Студенческий городок. Общежитие №3" },
                        StructSubvisionTypeId = 2
                    };//Студенческий городок. Общежитие №3
                    StructSubvision structSubvision26 = new StructSubvision
                    {
                        StructSubvisionId = 45,
                        StructSubvisionName = "Студенческий городок. Общежитие №4",
                        StructSubvisionFioChief = "Кунец Светлана Сергеевна",
                        StructSubvisionPostChiefId = 22,
                        StructSubvisionAdressId = 14,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Студенческий городок. Общежитие №4" },
                        StructSubvisionTypeId = 2
                    };//Студенческий городок. Общежитие №4
                    StructSubvision structSubvision27 = new StructSubvision
                    {
                        StructSubvisionId = 46,
                        StructSubvisionName = "Студенческий городок. Общежитие №5",
                        StructSubvisionFioChief = "Корсунова Вера Николаевна",
                        StructSubvisionPostChiefId = 23,
                        StructSubvisionAdressId = 15,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Студенческий городок. Общежитие №5" },
                        StructSubvisionTypeId = 2
                    };//Студенческий городок. Общежитие №5
                    StructSubvision structSubvision28 = new StructSubvision
                    {
                        StructSubvisionId = 47,
                        StructSubvisionName = "Инженерная служба",
                        StructSubvisionFioChief = "Коршунов Александр Иванович",
                        StructSubvisionPostChiefId = 24,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Инженерная служба" },
                        StructSubvisionTypeId = 2
                    };//Инженерная служба
                    StructSubvision structSubvision29 = new StructSubvision
                    {
                        StructSubvisionId = 48,
                        StructSubvisionName = "Ремонтностроительный участок",
                        StructSubvisionFioChief = "Дубограй Татьяна Гавриловна",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 7,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Ремонтностроительный участок" },
                        StructSubvisionTypeId = 2
                    };//Ремонтностроительный участок
                    StructSubvision structSubvision30 = new StructSubvision
                    {
                        StructSubvisionId = 49,
                        StructSubvisionName = "Хозяйственный отдел",
                        StructSubvisionFioChief = "Чичиланов Илья Иванович",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 7,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Хозяйственный отдел" },
                        StructSubvisionTypeId = 2
                    };//Хозяйственный отдел
                    StructSubvision structSubvision31 = new StructSubvision
                    {
                        StructSubvisionId = 50,
                        StructSubvisionName = "Учебный корпус №1",
                        StructSubvisionFioChief = "Бондаренко Татьяна Анатольевна",
                        StructSubvisionPostChiefId = 25,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №1" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №1
                    StructSubvision structSubvision32 = new StructSubvision
                    {
                        StructSubvisionId = 51,
                        StructSubvisionName = "Учебный корпус №2",
                        StructSubvisionFioChief = "Бондаренко Татьяна Анатольевна",
                        StructSubvisionPostChiefId = 26,
                        StructSubvisionAdressId = 12,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №2" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №2
                    StructSubvision structSubvision33 = new StructSubvision
                    {
                        StructSubvisionId = 52,
                        StructSubvisionName = "Учебный корпус №3",
                        StructSubvisionFioChief = "Левченко Наталья Петровна",
                        StructSubvisionPostChiefId = 27,
                        StructSubvisionAdressId = 8,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №3" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №3
                    StructSubvision structSubvision34 = new StructSubvision
                    {
                        StructSubvisionId = 53,
                        StructSubvisionName = "Административный корпус",
                        StructSubvisionFioChief = "Левченко Наталья Петровна",
                        StructSubvisionPostChiefId = 28,
                        StructSubvisionAdressId = 4,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Административный корпус" },
                        StructSubvisionTypeId = 2
                    };//Административный корпус
                    StructSubvision structSubvision35 = new StructSubvision
                    {
                        StructSubvisionId = 54,
                        StructSubvisionName = "Учебный корпус №5",
                        StructSubvisionFioChief = "Щербакова Павлина Григорьевна",
                        StructSubvisionPostChiefId = 29,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №5" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №5
                    StructSubvision structSubvision36 = new StructSubvision
                    {
                        StructSubvisionId = 55,
                        StructSubvisionName = "Учебный корпус №6",
                        StructSubvisionFioChief = "Щербакова Павлина Григорьевна",
                        StructSubvisionPostChiefId = 30,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №6" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №6
                    StructSubvision structSubvision37 = new StructSubvision
                    {
                        StructSubvisionId = 56,
                        StructSubvisionName = "Учебный корпус №7",
                        StructSubvisionFioChief = "Щербакова Павлина Григорьевна",
                        StructSubvisionPostChiefId = 31,
                        StructSubvisionAdressId = 7,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Учебный корпус №7" },
                        StructSubvisionTypeId = 2
                    };//Учебный корпус №7
                    StructSubvision structSubvision38 = new StructSubvision
                    {
                        StructSubvisionId = 57,
                        StructSubvisionName = "Автогараж",
                        StructSubvisionFioChief = "Мусенко Владимир Александрович",
                        StructSubvisionPostChiefId = 9,
                        StructSubvisionAdressId = 10,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "", EmailComment = "Автогараж" },
                        StructSubvisionTypeId = 2
                    };//Автогараж
                    await context.StructSubvisions.AddRangeAsync(structSubvision1, structSubvision2,
                        structSubvision3, structSubvision4, structSubvision5, structSubvision6,
                        structSubvision7, structSubvision8, structSubvision9, structSubvision10,
                        structSubvision11, structSubvision12, structSubvision13, structSubvision14,
                        structSubvision15, structSubvision16, structSubvision17, structSubvision18,
                        structSubvision19, structSubvision20, structSubvision21, structSubvision22,
                        structSubvision23, structSubvision24, structSubvision25, structSubvision26,
                        structSubvision27, structSubvision28, structSubvision29, structSubvision30,
                        structSubvision31, structSubvision32, structSubvision33, structSubvision34,
                        structSubvision35, structSubvision36, structSubvision37, structSubvision38,

                        structSubvisionSpo, structSubvisionInjTech, structSubvisionEnerg, structSubvisionEconom,

                        structSubvisionKafTiIuS, structSubvisionKafTbiF, structSubvisionKafFiS,
                        structSubvisionKafEEOiEM, structSubvisionKafEEiET, structSubvisionKafBUAiA,
                        structSubvisionKafGDiInYaz, structSubvisionKafZUiK, structSubvisionEiU,
                        structSubvisionAiSsk, structSubvisionVMiM, structSubvisionTSinAPK, 
                        structSubvisionTiSmAPK, structSubvisionTiA, structSubvisionEAiTTP);

                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Факультеты"
                if (!await context.StructFacultets.AnyAsync())
                {
                    StructFacultet facultetSpo = new StructFacultet
                    {
                        StructFacultetId = 1,
                        StructInstituteId = 1,
                        StructSubvisionId = 15
                    };

                    StructFacultet facultetInjTech = new StructFacultet
                    {
                        StructFacultetId = 2,
                        StructInstituteId = 1,
                        StructSubvisionId = 16
                    };

                    StructFacultet facultetEnerg = new StructFacultet
                    {
                        StructFacultetId = 3,
                        StructInstituteId = 1,
                        StructSubvisionId = 17
                    };

                    StructFacultet facultetEconom = new StructFacultet
                    {
                        StructFacultetId = 4,
                        StructInstituteId = 1,
                        StructSubvisionId = 18
                    };

                    await context.StructFacultets.AddRangeAsync(new StructFacultet[] { facultetSpo,
                        facultetInjTech, facultetEnerg, facultetEconom
                    });
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Кафедры"
                if (!await context.StructKafs.AnyAsync())
                {
                    StructKaf kaf1 = new StructKaf
                    {
                        StructKafId = 1,
                        StructFacultetId = 3,
                        StructSubvisionId = 19
                    };

                    StructKaf kaf2 = new StructKaf
                    {
                        StructKafId = 2,
                        StructFacultetId = 3,
                        StructSubvisionId = 20
                    };

                    StructKaf kaf3 = new StructKaf
                    {
                        StructKafId = 3,
                        StructFacultetId = 3,
                        StructSubvisionId = 21
                    };

                    StructKaf kaf4 = new StructKaf
                    {
                        StructKafId = 4,
                        StructFacultetId = 3,
                        StructSubvisionId = 22
                    };

                    StructKaf kaf5 = new StructKaf
                    {
                        StructKafId = 5,
                        StructFacultetId = 3,
                        StructSubvisionId = 23
                    };

                    StructKaf kaf6 = new StructKaf
                    {
                        StructKafId = 6,
                        StructFacultetId = 4,
                        StructSubvisionId = 24
                    };

                    StructKaf kaf7 = new StructKaf
                    {
                        StructKafId = 7,
                        StructFacultetId = 4,
                        StructSubvisionId = 25
                    };

                    StructKaf kaf8 = new StructKaf
                    {
                        StructKafId = 8,
                        StructFacultetId = 4,
                        StructSubvisionId = 26
                    };

                    StructKaf kaf9 = new StructKaf
                    {
                        StructKafId = 9,
                        StructFacultetId = 4,
                        StructSubvisionId = 27
                    };

                    StructKaf kaf10 = new StructKaf
                    {
                        StructKafId = 10,
                        StructFacultetId = 4,
                        StructSubvisionId = 28
                    };

                    StructKaf kaf11 = new StructKaf
                    {
                        StructKafId = 11,
                        StructFacultetId = 2,
                        StructSubvisionId = 29
                    };

                    StructKaf kaf12 = new StructKaf
                    {
                        StructKafId = 12,
                        StructFacultetId = 2,
                        StructSubvisionId = 30
                    };

                    StructKaf kaf13 = new StructKaf
                    {
                        StructKafId = 13,
                        StructFacultetId = 2,
                        StructSubvisionId = 31
                    };

                    StructKaf kaf14 = new StructKaf
                    {
                        StructKafId = 14,
                        StructFacultetId = 2,
                        StructSubvisionId = 32
                    };

                    StructKaf kaf15 = new StructKaf
                    {
                        StructKafId = 15,
                        StructFacultetId = 2,
                        StructSubvisionId = 33
                    };


                    await context.StructKafs.AddRangeAsync(
                        kaf1,
                        kaf2,
                        kaf3,
                        kaf4,
                        kaf5,
                        kaf6,
                        kaf7,
                        kaf8,
                        kaf9,
                        kaf10,
                        kaf11,
                        kaf12,
                        kaf13,
                        kaf14,
                        kaf15);
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

        /// <summary>
        /// Инициализация данных, связанных с файловыми операциями
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateFilesData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Группы типов содержимого файла"
                if (!await context.FileDataTypeGroups.AnyAsync())
                {
                    FileDataTypeGroup fileDataTypeGroup1 = new FileDataTypeGroup
                    {
                        FileDataTypeGroupId = 1,
                        FileDataTypeGroupName = "Положения"
                    };

                    FileDataTypeGroup fileDataTypeGroup2 = new FileDataTypeGroup
                    {
                        FileDataTypeGroupId = 2,
                        FileDataTypeGroupName = "Локальные нормативные акты, предусмотренные частью 2 статьи 30 федерального закона \"Об образовании в РФ\""
                    };

                    FileDataTypeGroup fileDataTypeGroup3 = new FileDataTypeGroup
                    {
                        FileDataTypeGroupId = 3,
                        FileDataTypeGroupName = "Сведения об образовательной организации - Документы"
                    };

                    FileDataTypeGroup fileDataTypeGroup4 = new FileDataTypeGroup
                    {
                        FileDataTypeGroupId = 4,
                        FileDataTypeGroupName = "Документы об образовании"
                    };

                    await context.FileDataTypeGroups.AddRangeAsync(
                        fileDataTypeGroup1,
                        fileDataTypeGroup2,
                        fileDataTypeGroup3,
                        fileDataTypeGroup4
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
                
                #region Инициализация таблицы "Типы содержимого файла"
                if (!await context.FileDataTypes.AnyAsync())
                {
                    FileDataType fileDataType1 = new FileDataType
                    {
                        FileDataTypeId = 1,
                        FileDataTypeName = "Положения о структурных подразделениях",
                        FileDataTypeGroupId = 1
                    };

                    FileDataType fileDataType2 = new FileDataType
                    {
                        FileDataTypeId = 2,
                        FileDataTypeName = "Положения об образовательной деятельности",
                        FileDataTypeGroupId = 1
                    };

                    FileDataType fileDataType3 = new FileDataType
                    {
                        FileDataTypeId = 3,
                        FileDataTypeName = "Устав образовательной организации",
                        Itemprop="ustavDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType4 = new FileDataType
                    {
                        FileDataTypeId = 4,
                        FileDataTypeName = "Лицензия на осуществление образовательной деятельности",
                        Itemprop = "licenseDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType5 = new FileDataType
                    {
                        FileDataTypeId = 5,
                        FileDataTypeName = "Свидетельство о государственной аккредитации (с приложениями)",
                        Itemprop = "accreditationDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType6 = new FileDataType
                    {
                        FileDataTypeId = 6,
                        FileDataTypeName = "План финансово-хозяйственной деятельности образовательной организации",
                        Itemprop = "finPlanDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType7 = new FileDataType
                    {
                        FileDataTypeId = 7,
                        FileDataTypeName = "Локальные нормативные акты, регламентирующие правила приема обучающихся",
                        Itemprop = "priemDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType8 = new FileDataType
                    {
                        FileDataTypeId = 8,
                        FileDataTypeName = "Локальные нормативные акты, регламентирующие режим занятий обучающихся",
                        Itemprop = "modeDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType9 = new FileDataType
                    {
                        FileDataTypeId = 9,
                        FileDataTypeName = "Локальные нормативные акты, регламентирующие формы, периодичность и порядок текущего контроля успеваемости и промежуточной аттестации обучающихся",
                        Itemprop = "tek_KontrolDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType10 = new FileDataType
                    {
                        FileDataTypeId = 10,
                        FileDataTypeName = "Локальные нормативные акты, регламентирующие порядок и основания перевода, отчисления и восстановления обучающихся",
                        Itemprop = "perevodDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType11 = new FileDataType
                    {
                        FileDataTypeId = 11,
                        FileDataTypeName = "Локальные нормативные акты, регламентирующие порядок оформления возникновения, приостановления и прекращения отношений между образовательной организацией, обучающимися и (или) родителями (законными представителями) несовершеннолетних обучающихся",
                        Itemprop = "vozDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType12 = new FileDataType
                    {
                        FileDataTypeId = 12,
                        FileDataTypeName = "Правила внутреннего распорядка обучающихся",
                        Itemprop = "localActStud",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType13 = new FileDataType
                    {
                        FileDataTypeId = 13,
                        FileDataTypeName = "Правила внутреннего трудового распорядка",
                        Itemprop = "localActOrder",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType14 = new FileDataType
                    {
                        FileDataTypeId = 14,
                        FileDataTypeName = "Коллективный договор",
                        Itemprop = "localActCollec",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType15 = new FileDataType
                    {
                        FileDataTypeId = 15,
                        FileDataTypeName = "Отчет о результатах самообследования",
                        Itemprop = "reportEduDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType16 = new FileDataType
                    {
                        FileDataTypeId = 16,
                        FileDataTypeName = "Документ о порядке оказания платных образовательных услуг",
                        Itemprop = "paidEduDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType17 = new FileDataType
                    {
                        FileDataTypeId = 17,
                        FileDataTypeName = "Образец договора об оказании платных образовательных услуг",
                        Itemprop = "paidEduDogDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType18 = new FileDataType
                    {
                        FileDataTypeId = 18,
                        FileDataTypeName = "Документ об утверждении стоимости обучения по каждой образовательной программе",
                        Itemprop = "paidEduStoimDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType19 = new FileDataType
                    {
                        FileDataTypeId = 19,
                        FileDataTypeName = "Предписания органов, осуществляющих государственный контроль (надзор) в сфере образования",
                        Itemprop = "prescriptionDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType20 = new FileDataType
                    {
                        FileDataTypeId = 20,
                        FileDataTypeName = "Отчеты об исполнении предписаний органов, осуществляющих государственный контроль (надзор) в сфере образования",
                        Itemprop = "prescriptionOtchetDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType21 = new FileDataType
                    {
                        FileDataTypeId = 21,
                        FileDataTypeName = "Удостоверение о повышении квалификации",
                        Itemprop = "",
                        FileDataTypeGroupId = 4
                    };

                    FileDataType fileDataType22 = new FileDataType
                    {
                        FileDataTypeId = 22,
                        FileDataTypeName = "Удостоверение",
                        Itemprop = "",
                        FileDataTypeGroupId = 4
                    };

                    FileDataType fileDataType23 = new FileDataType
                    {
                        FileDataTypeId = 23,
                        FileDataTypeName = "Свидетельство",
                        Itemprop = "",
                        FileDataTypeGroupId = 4
                    };

                    FileDataType fileDataType24 = new FileDataType
                    {
                        FileDataTypeId = 24,
                        FileDataTypeName = "Диплом о профессиональной переподготовке",
                        Itemprop = "",
                        FileDataTypeGroupId = 4
                    };

                    FileDataType fileDataType25 = new FileDataType
                    {
                        FileDataTypeId = 25,
                        FileDataTypeName = "Диплом о среднем профессиональном образовании",
                        Itemprop = "",
                        FileDataTypeGroupId = 4
                    };

                    FileDataType fileDataType26 = new FileDataType
                    {
                        FileDataTypeId = 26,
                        FileDataTypeName = "Диплом о высшем образовании",
                        Itemprop = "",
                        FileDataTypeGroupId = 4
                    };

                    FileDataType fileDataType27 = new FileDataType
                    {
                        FileDataTypeId = 27,
                        FileDataTypeName = "Диплом кандидата наук",
                        Itemprop = "",
                        FileDataTypeGroupId = 4
                    };

                    FileDataType fileDataType28 = new FileDataType
                    {
                        FileDataTypeId = 28,
                        FileDataTypeName = "Диплом доктора наук",
                        Itemprop = "",
                        FileDataTypeGroupId = 4
                    };

                    FileDataType fileDataType29 = new FileDataType
                    {
                        FileDataTypeId = 29,
                        FileDataTypeName = "Аттестат доцента",
                        Itemprop = "",
                        FileDataTypeGroupId = 4
                    };

                    FileDataType fileDataType30 = new FileDataType
                    {
                        FileDataTypeId = 30,
                        FileDataTypeName = "Аттестат профессора",
                        Itemprop = "",
                        FileDataTypeGroupId = 4
                    };

                    await context.FileDataTypes.AddRangeAsync(
                        fileDataType1,
                        fileDataType2,
                        fileDataType3,
                        fileDataType4,
                        fileDataType5,
                        fileDataType6,
                        fileDataType7,
                        fileDataType8,
                        fileDataType9,
                        fileDataType10,
                        fileDataType11,
                        fileDataType12,
                        fileDataType13,
                        fileDataType14,
                        fileDataType15,
                        fileDataType16,
                        fileDataType17,
                        fileDataType18,
                        fileDataType19,
                        fileDataType20,
                        fileDataType21,
                        fileDataType22,
                        fileDataType23,
                        fileDataType24,
                        fileDataType25,
                        fileDataType26,
                        fileDataType27,
                        fileDataType28,
                        fileDataType29,
                        fileDataType30
                        );
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Заполнение тестовыми данными таблицы "Файлы"
                if (!await context.Files.AnyAsync())
                {
                    FileModel fileDataTypeGroup1 = new FileModel
                    {                        
                        Name="Описание файла",
                        FileToFileTypes=new List<FileToFileType> { new FileToFileType { FileDataTypeId=3} }
                    };
                                        

                    await context.Files.AddRangeAsync(
                        fileDataTypeGroup1
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

        /// <summary>
        /// Инициализация данных, связанных с численность обучающихся
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEduChislen(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Численность обучающихся"
                if (!await context.EduChislens.AnyAsync())
                {
                    foreach (var profile in context.EduProfiles)
                    {
                        foreach (var form in context.EduForms)
                        {
                            EduChislen eduChislen = new EduChislen();
                            eduChislen.EduProfileId = profile.EduProfileId;
                            eduChislen.EduFormId = form.EduFormId;

                            await context.EduChislens.AddAsync(eduChislen);
                        }
                    }

                    await context.SaveChangesAsync();
                }
                #endregion

            }
        }

        /// <summary>
        /// Инициализация таблицы "Руководство"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateRucovodstvo(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Руководство"
                if (!await context.SvedenRucovodstvo.AnyAsync())
                {
                    Rucovodstvo SvedenRucovodstvo1 = new Rucovodstvo
                    {
                       Fio= "Серегин Александр Анатольевич",
                       Post= "Директор",
                       Telephone= "8(86359) 41-7-43",
                       Email= "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo2 = new Rucovodstvo
                    {
                        Fio = "Глечикова Наталья Александровна",
                        Post = "И.о. зам. директора по учебной работе",
                        Telephone = "8(86359) 42-1-76",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo3 = new Rucovodstvo
                    {
                        Fio = "Юдаев Игорь Викторович",
                        Post = "Зам. директора по научной работе",
                        Telephone = "8(86359) 41-1-61",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo4 = new Rucovodstvo
                    {
                        Fio = "Джанибеков Казбек Алиевич",
                        Post = "Зам. директора по административно-хозяйственной работе",
                        Telephone = "8(86359) 42-7-81",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo5 = new Rucovodstvo
                    {
                        Fio = "Асатурян Сергей Вартанович",
                        Post = "И.о. зам. директора по воспитательной работе",
                        Telephone = "8(86359) 43-3-49",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo6 = new Rucovodstvo
                    {
                        Fio = "Кабанов Александр Николаевич",
                        Post = "И.о. зам. директора по социальной работе",
                        Telephone = "8(86359) 34-6-12",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo7 = new Rucovodstvo
                    {
                        Fio = "Бондаренко Анатолий Михайлович",
                        Post = "И.о. зам. директора по связям с общественностью",
                        Telephone = "8(86359) 41-3-65",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo8 = new Rucovodstvo
                    {
                        Fio = "Меркулов Александр Филиппович",
                        Post = "Директор агротехнологического центра",
                        Telephone = "8(86359) 34-7-32",
                        Email = "achgaa@achgaa.ru"
                    };
                    await context.SvedenRucovodstvo.AddRangeAsync(
                        SvedenRucovodstvo1,
                        SvedenRucovodstvo2,
                        SvedenRucovodstvo3,
                        SvedenRucovodstvo4,
                        SvedenRucovodstvo5,
                        SvedenRucovodstvo6,
                        SvedenRucovodstvo7,
                        SvedenRucovodstvo8
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
