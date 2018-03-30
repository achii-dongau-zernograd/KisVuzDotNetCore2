 using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Struct;
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
    public class AppIdentityDBContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDBContext(DbContextOptions<AppIdentityDBContext> options) : base (options)
        {
            
        }

        #region Таблицы
        //////////////////// Education ////////////////////
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


        //////////// Struct /////////////

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

        /// <summary>
        /// Справочник должностей
        /// </summary>
        public DbSet<Post> Posts { get; set; }





        ///////////////////////////////////
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

        ////////// Сведения об образовательной организации ////////

        /// <summary>
        /// Сведения об образовательной организации.
        /// Структура и органы управления образовательной организацией
        /// </summary>
        public DbSet<StructOrgUprav> StructOrgUprav { get; set; }



        ////////////////////////////////////////////////////////////

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
            await CreateEducationData(serviceProvider, configuration);
            await CreateStructData(serviceProvider, configuration);
            await CreateFilesData(serviceProvider, configuration);
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
                        eduLevel5VoAsp );
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
                        };
                    EduUgs eduUgs21 =
                        new EduUgs
                        {
                            EduUgsId = 2,
                            EduUgsCode = "21.00.00",
                            EduUgsName = "Прикладная геология, горное дело, нефтегазовое дело и геодезия",
                            EduLevelId = 2,
                        };

                    EduUgs eduUgs23 =
                        new EduUgs
                        {
                            EduUgsId = 3,
                            EduUgsCode = "23.00.00",
                            EduUgsName = "Техника и технологии наземного транспорта",
                            EduLevelId = 2,
                        };

                    EduUgs eduUgs35 =
                        new EduUgs
                        {
                            EduUgsId = 4,
                            EduUgsCode = "35.00.00",
                            EduUgsName = "Сельское, лесное и рыбное хозяйство",
                            EduLevelId = 2,
                        };

                    EduUgs eduUgs38 =
                        new EduUgs
                        {
                            EduUgsId = 5,
                            EduUgsCode = "38.00.00",
                            EduUgsName = "Экономика и управление",
                            EduLevelId = 2,
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
                        };
                    EduUgs eduUgs19VoBak =
                        new EduUgs
                        {
                            EduUgsId = 7,
                            EduUgsCode = "19.00.00",
                            EduUgsName = "Промышленная экология и биотехнологии",
                            EduLevelId = 3,
                        };
                    EduUgs eduUgs20VoBak =
                        new EduUgs
                        {
                            EduUgsId = 8,
                            EduUgsCode = "20.00.00",
                            EduUgsName = "Техносферная безопасность и природообустройство",
                            EduLevelId = 3,
                        };
                    EduUgs eduUgs21VoBak =
                        new EduUgs
                        {
                            EduUgsId = 9,
                            EduUgsCode = "21.00.00",
                            EduUgsName = "Прикладная геология, горное дело, нефтегазовое дело и геодезия",
                            EduLevelId = 3,
                        };
                    EduUgs eduUgs23VoBak =
                        new EduUgs
                        {
                            EduUgsId = 10,
                            EduUgsCode = "23.00.00",
                            EduUgsName = "Техника и технологии наземного транспорта",
                            EduLevelId = 3,
                        };
                    EduUgs eduUgs35VoBak =
                        new EduUgs
                        {
                            EduUgsId = 11,
                            EduUgsCode = "35.00.00",
                            EduUgsName = "Сельское, лесное и рыбное хозяйство",
                            EduLevelId = 3,
                        };
                    EduUgs eduUgs38VoBak =
                        new EduUgs
                        {
                            EduUgsId = 12,
                            EduUgsCode = "38.00.00",
                            EduUgsName = "Экономика и управление",
                            EduLevelId = 3,
                        };
                    EduUgs eduUgs44VoBak =
                        new EduUgs
                        {
                            EduUgsId = 13,
                            EduUgsCode = "44.00.00",
                            EduUgsName = "Образование и педагогические науки",
                            EduLevelId = 3,
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
                        };
                    EduUgs eduUgs23VoMag =
                        new EduUgs
                        {
                            EduUgsId = 15,
                            EduUgsCode = "23.00.00",
                            EduUgsName = "Техника и технологии наземного транспорта",
                            EduLevelId = 4,
                        };
                    EduUgs eduUgs35VoMag =
                        new EduUgs
                        {
                            EduUgsId = 16,
                            EduUgsCode = "35.00.00",
                            EduUgsName = "Сельское, лесное и рыбное хозяйство",
                            EduLevelId = 4,
                        };
                    EduUgs eduUgs38VoMag =
                        new EduUgs
                        {
                            EduUgsId = 17,
                            EduUgsCode = "38.00.00",
                            EduUgsName = "Экономика и управление",
                            EduLevelId = 4,
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
                        };
                    EduUgs eduUgs38VoSpec =
                        new EduUgs
                        {
                            EduUgsId = 19,
                            EduUgsCode = "38.00.00",
                            EduUgsName = "Экономика и управление",
                            EduLevelId = 5,
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
                        };
                    EduUgs eduUgs23VoAsp =
                        new EduUgs
                        {
                            EduUgsId = 21,
                            EduUgsCode = "23.00.00",
                            EduUgsName = "Техника и технологии наземного транспорта",
                            EduLevelId = 6,
                        };
                    EduUgs eduUgs35VoAsp =
                        new EduUgs
                        {
                            EduUgsId = 22,
                            EduUgsCode = "35.00.00",
                            EduUgsName = "Сельское, лесное и рыбное хозяйство",
                            EduLevelId = 6,
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
                            EduNapravCode = "08.02.09",
                            EduNapravName = "Монтаж, наладка и эксплуатация электрооборудования промышленных и гражданских зданий",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/db/mo/Data/d_14/m519.pdf",
                            EduUgsId = 1
                        };
                    EduNapravl eduNapr210205 =
                        new EduNapravl
                        {
                            EduNapravlId = 2,
                            EduNapravCode = "21.02.05",
                            EduNapravName = "Земельно-имущественные отношения",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/db/mo/Data/d_14/m486.pdf",
                            EduUgsId = 2
                        };
                    EduNapravl eduNapr230203 =
                        new EduNapravl
                        {
                            EduNapravlId = 3,
                            EduNapravCode = "23.02.03",
                            EduNapravName = "Техническое обслуживание и ремонт автомобильного транспорта",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/db/mo/Data/d_14/m383.pdf",
                            EduUgsId = 3
                        };
                    EduNapravl eduNapr350208 =
                        new EduNapravl
                        {
                            EduNapravlId = 4,
                            EduNapravCode = "35.02.08",
                            EduNapravName = "Электрификация и автоматизация сельского хозяйства",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/db/mo/Data/d_14/m457.pdf",
                            EduUgsId = 4
                        };
                    EduNapravl eduNapr380201 =
                        new EduNapravl
                        {
                            EduNapravlId = 5,
                            EduNapravCode = "38.02.01",
                            EduNapravName = "Экономика и бухгалтерский учет (по отраслям)",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/db/mo/Data/d_14/m832.pdf",
                            EduUgsId = 5
                        };
                    EduNapravl eduNapr380204 =
                        new EduNapravl
                        {
                            EduNapravlId = 6,
                            EduNapravCode = "38.02.04",
                            EduNapravName = "Коммерция (по отраслям)",
                            EduNapravlStandartDocLink = @"http://www.edu.ru/file/docs/2014/05/58859.pdf",
                            EduUgsId = 5
                        };
                    #endregion

                    #region Направления ВО-бакалавриат
                    EduNapravl eduNapr130301 =
                        new EduNapravl
                        {
                            EduNapravlId = 7,
                            EduNapravCode = "13.03.01",
                            EduNapravName = "Теплоэнергетика и теплотехника",
                            EduNapravlStandartDocLink= @"http://fgosvo.ru/fgosvo/downloads/474/?f=%2Fuploadfiles%2Ffgosvob%2F130301.pdf",
                            EduUgsId = 6
                        };

                    EduNapravl eduNapr130302 =
                        new EduNapravl
                        {
                            EduNapravlId = 8,
                            EduNapravCode = "13.03.02",
                            EduNapravName = "Электроэнергетика и электротехника",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/436/?f=%2Fuploadfiles%2Ffgosvob%2F130302.pdf",
                            EduUgsId = 6
                        };

                    EduNapravl eduNapr190302 =
                        new EduNapravl
                        {
                            EduNapravlId = 9,
                            EduNapravCode = "19.03.02",
                            EduNapravName = "Продукты питания из растительного сырья",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/329/?f=%2Fuploadfiles%2Ffgosvob%2F190302.pdf",
                            EduUgsId = 7
                        };

                    EduNapravl eduNapr200301 =
                        new EduNapravl
                        {
                            EduNapravlId = 10,
                            EduNapravCode = "20.03.01",
                            EduNapravName = "Техносферная безопасность",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/560/?f=%2Fuploadfiles%2Ffgosvob%2F200301.pdf",
                            EduUgsId = 8
                        };

                    EduNapravl eduNapr210302 =
                        new EduNapravl
                        {
                            EduNapravlId = 11,
                            EduNapravCode = "21.03.02",
                            EduNapravName = "Землеустройство и кадастры",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/472/?f=%2Fuploadfiles%2Ffgosvob%2F210302.pdf",
                            EduUgsId = 9
                        };

                    EduNapravl eduNapr230301 =
                        new EduNapravl
                        {
                            EduNapravlId = 12,
                            EduNapravCode = "23.03.01",
                            EduNapravName = "Технология транспортных процессов",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/935/?f=%2Fuploadfiles%2Ffgosvob%2F230301.pdf",
                            EduUgsId = 10
                        };

                    EduNapravl eduNapr230303 =
                        new EduNapravl
                        {
                            EduNapravlId = 13,
                            EduNapravCode = "23.03.03",
                            EduNapravName = "Эксплуатация транспортно-технологических машин и комплексов",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/535/?f=%2Fuploadfiles%2Ffgosvob%2F230303.pdf",
                            EduUgsId = 10
                        };

                    EduNapravl eduNapr350304 =
                        new EduNapravl
                        {
                            EduNapravlId = 14,
                            EduNapravCode = "35.03.04",
                            EduNapravName = "Агрономия",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/516/?f=%2Fuploadfiles%2Ffgosvob%2F350304.pdf",
                            EduUgsId = 11
                        };

                    EduNapravl eduNapr350306 =
                        new EduNapravl
                        {
                            EduNapravlId = 15,
                            EduNapravCode = "35.03.06",
                            EduNapravName = "Агроинженерия",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/486/?f=%2Fuploadfiles%2Ffgosvob%2F350306.pdf",
                            EduUgsId = 11
                        };

                    EduNapravl eduNapr380301 =
                        new EduNapravl
                        {
                            EduNapravlId = 16,
                            EduNapravCode = "38.03.01",
                            EduNapravName = "Экономика",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/498/?f=%2Fuploadfiles%2Ffgosvob%2F380301.pdf",
                            EduUgsId = 12
                        };

                    EduNapravl eduNapr380302 =
                        new EduNapravl
                        {
                            EduNapravlId = 17,
                            EduNapravCode = "38.03.02",
                            EduNapravName = "Менеджмент",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/545/?f=%2Fuploadfiles%2Ffgosvob%2F380302.pdf",
                            EduUgsId = 12
                        };

                    EduNapravl eduNapr380304 =
                        new EduNapravl
                        {
                            EduNapravlId = 18,
                            EduNapravCode = "38.03.04",
                            EduNapravName = "Государственное и муниципальное управление",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/298/?f=%2Fuploadfiles%2Ffgosvob%2F380304_gosmunupr.pdf",
                            EduUgsId = 12
                        };

                    EduNapravl eduNapr440304 =
                        new EduNapravl
                        {
                            EduNapravlId = 19,
                            EduNapravCode = "44.03.04",
                            EduNapravName = "Профессиональное обучение",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/473/?f=%2Fuploadfiles%2Ffgosvob%2F440304.pdf",
                            EduUgsId = 13
                        };

                    #endregion

                    #region Направления ВО-магистратура
                    EduNapravl eduNapr130401 =
                        new EduNapravl
                        {
                            EduNapravlId = 20,
                            EduNapravCode = "13.04.01",
                            EduNapravName = "Теплоэнергетика и теплотехника",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/205/?f=%2Fuploadfiles%2Ffgosvom%2F130401.pdf",
                            EduUgsId = 14
                        };

                    EduNapravl eduNapr130402 =
                        new EduNapravl
                        {
                            EduNapravlId = 21,
                            EduNapravCode = "13.04.02",
                            EduNapravName = "Электроэнергетика и электротехника",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/206/?f=%2Fuploadfiles%2Ffgosvom%2F130402_Elektroenergetika.pdf",
                            EduUgsId = 14
                        };


                    EduNapravl eduNapr230401 =
                        new EduNapravl
                        {
                            EduNapravlId = 22,
                            EduNapravCode = "23.04.01",
                            EduNapravName = "Технология транспортных процессов",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/379/?f=%2Fuploadfiles%2Ffgosvom%2F230401.pdf",
                            EduUgsId = 15
                        };

                    EduNapravl eduNapr230403 =
                        new EduNapravl
                        {
                            EduNapravlId = 23,
                            EduNapravCode = "23.04.03",
                            EduNapravName = "Эксплуатация транспортно-технологических машин и комплексов",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/299/?f=%2Fuploadfiles%2Ffgosvom%2F230403.pdf",
                            EduUgsId = 15
                        };

                    EduNapravl eduNapr350404 =
                        new EduNapravl
                        {
                            EduNapravlId = 24,
                            EduNapravCode = "35.04.04",
                            EduNapravName = "Агрономия",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/412/?f=%2Fuploadfiles%2Ffgosvom%2F350404.pdf",
                            EduUgsId = 16
                        };

                    EduNapravl eduNapr350406 =
                        new EduNapravl
                        {
                            EduNapravlId = 25,
                            EduNapravCode = "35.04.06",
                            EduNapravName = "Агроинженерия",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/461/?f=%2Fuploadfiles%2Ffgosvom%2F350406.pdf",
                            EduUgsId = 16
                        };

                    EduNapravl eduNapr380401 =
                        new EduNapravl
                        {
                            EduNapravlId = 26,
                            EduNapravCode = "38.04.01",
                            EduNapravName = "Экономика",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/386/?f=%2Fuploadfiles%2Ffgosvom%2F380401.pdf",
                            EduUgsId = 17
                        };

                    EduNapravl eduNapr380402 =
                        new EduNapravl
                        {
                            EduNapravlId = 27,
                            EduNapravCode = "38.04.02",
                            EduNapravName = "Менеджмент",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/361/?f=%2Fuploadfiles%2Ffgosvom%2F380402.pdf",
                            EduUgsId = 17
                        };
                    #endregion

                    #region Направления ВО-специалитет 
                    EduNapravl eduNapr230501 =
                        new EduNapravl
                        {
                            EduNapravlId = 28,
                            EduNapravCode = "23.05.01",
                            EduNapravName = "Наземные транспортно–технологические средства",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/747/?f=%2Fuploadfiles%2Ffgosvospec%2F230501.pdf",
                            EduUgsId = 18
                        };

                    EduNapravl eduNapr380501 =
                        new EduNapravl
                        {
                            EduNapravlId = 29,
                            EduNapravCode = "38.05.01",
                            EduNapravName = "Экономическая безопасность",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/946/?f=%2Fuploadfiles%2Ffgosvospec%2F380501.pdf",
                            EduUgsId = 19
                        };
                    #endregion

                    #region Направления ВО-подготовка кадров высшей квалификации
                    EduNapravl eduNapr060601 =
                        new EduNapravl
                        {
                            EduNapravlId = 30,
                            EduNapravCode = "06.06.01",
                            EduNapravName = "Биологические науки",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/99/?f=%2Fuploadfiles%2Ffgosvoaspism%2F060601.pdf",
                            EduUgsId = 20
                        };
                    EduNapravl eduNapr230601 =
                        new EduNapravl
                        {
                            EduNapravlId = 31,
                            EduNapravCode = "23.06.01",
                            EduNapravName = "Техника и технологии наземного транспорта",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/113/?f=%2Fuploadfiles%2Ffgosvoaspism%2F230601.pdf",
                            EduUgsId = 21
                        };
                    EduNapravl eduNapr350601 =
                        new EduNapravl
                        {
                            EduNapravlId = 32,
                            EduNapravCode = "35.06.01",
                            EduNapravName = "Сельское хозяйство",
                            EduNapravlStandartDocLink = @"http://fgosvo.ru/fgosvo/downloads/119/?f=%2Fuploadfiles%2Ffgosvoaspism%2F350601.pdf",
                            EduUgsId = 22
                        };
                    EduNapravl eduNapr350604 =
                        new EduNapravl
                        {
                            EduNapravlId = 33,
                            EduNapravCode = "35.06.04",
                            EduNapravName = "Технологии, средства механизации и энергетическое оборудование в сельском, лесном и рыбном хозяйстве",
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
                    };

                    Address addressLenina21 = new Address
                    {
                        AddressId = 2,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "21"
                    };

                    Address addressSovetskaya28 = new Address
                    {
                        AddressId = 3,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "28"
                    };

                    Address addressLenina21_2 = new Address
                    {
                        AddressId = 4,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "21/2"
                    };

                    Address addressSovetskaya15_4 = new Address
                    {
                        AddressId = 5,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "15/4"
                    };

                    Address addressLenina19_a = new Address
                    {
                        AddressId = 6,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Ленина",
                        HouseNumber = "19a"
                    };

                    Address addressSovetskaya17_21 = new Address
                    {
                        AddressId = 7,
                        PostCode = "347740",
                        Country = "Россия",
                        Region = "Ростовская область",
                        Settlement = "г. Зерноград",
                        Street = "ул. Советская",
                        HouseNumber = "17/21"
                    };

                    await context.Addresses.AddRangeAsync(
                        addressPers, 
                        addressLenina21, 
                        addressSovetskaya28, 
                        addressLenina21_2, 
                        addressSovetskaya15_4, 
                        addressLenina19_a,
                        addressSovetskaya17_21);
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
                    };

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
                        Faxes=new List<Fax> { new Fax { FaxValue = "(886359) 43-3-80", FaxComment = "Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ" } },
                        Emailes = new List<Email> {new Email { EmailValue = "achgaa@achgaa.ru", EmailComment = "Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ"} },
                        Telephones =new List<Telephone>
                        {
                            new Telephone{TelephoneNumber="(886359) 41-8-31", TelephoneComment="Приемная комиссия"},
                            new Telephone{TelephoneNumber="(886359) 43-3-80", TelephoneComment="Канцелярия"},
                            new Telephone{TelephoneNumber="(886359) 42-6-78", TelephoneComment="Факультет  «Инженерно-технологический» "},
                            new Telephone{TelephoneNumber="(886359) 41-6-56", TelephoneComment="Факультет «Энергетический»"},
                            new Telephone{TelephoneNumber="(886359) 42-1-98", TelephoneComment="Факультет «Экономика и управление территориями»"},
                            new Telephone{TelephoneNumber="(886359) 35-9-96", TelephoneComment="Факультет «Среднее профессиональное образование»"},
                        }
                    };

                    await context.StructInstitutes.AddAsync(institute);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Типы структурных подразделений"
                if (!await context.StructSubvisionTypes.AnyAsync())
                {
                    StructSubvisionType structSubvisionType1 = new StructSubvisionType
                    {
                        StructSubvisionTypeId=1,
                        StructSubvisionTypeName="Органы управления образовательной организации"
                    };

                    StructSubvisionType structSubvisionType2 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 2,
                        StructSubvisionTypeName = "Структурные подразделения образовательной организации"
                    };

                    StructSubvisionType structSubvisionType3 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 3,
                        StructSubvisionTypeName = "Факультеты"
                    };

                    StructSubvisionType structSubvisionType4 = new StructSubvisionType
                    {
                        StructSubvisionTypeId = 4,
                        StructSubvisionTypeName = "Кафедры"
                    };

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
                        PostName = "Ассистент"
                    };

                    Post post2 = new Post
                    {
                        PostId = 2,
                        PostName = "Старший преподаватель"
                    };

                    Post post3 = new Post
                    {
                        PostId = 3,
                        PostName = "Доцент"
                    };

                    Post post4 = new Post
                    {
                        PostId = 4,
                        PostName = "Профессор"
                    };

                    Post post5 = new Post
                    {
                        PostId = 5,
                        PostName = "Заведующий кафедрой"
                    };

                    Post post6 = new Post
                    {
                        PostId = 6,
                        PostName = "Декан"
                    };

                    Post post7 = new Post
                    {
                        PostId = 7,
                        PostName = "Начальник"
                    };

                    Post post8 = new Post
                    {
                        PostId = 8,
                        PostName = "Заместитель директора"
                    };

                    Post post9 = new Post
                    {
                        PostId = 9,
                        PostName = "Директор"
                    };

                    Post post10 = new Post
                    {
                        PostId = 10,
                        PostName = "Главный бухгалтер"
                    };

                    await context.Posts.AddRangeAsync(post1, post2, post3, post4, post5, post6, post7, post8, post9, post10);
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Структурные подразделения"
                if (!await context.StructSubvisions.AnyAsync())
                {
                    StructSubvision structSubvision1 = new StructSubvision
                    {
                        StructSubvisionId = 1,
                        StructSubvisionName = "Отдел финансового планирования и бухгалтерского учета",
                        StructSubvisionFioChief = "Поваляева Елена Петровна",
                        StructSubvisionPostChiefId = 10,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite ="",
                        StructSubvisionEmail = new Email { EmailValue= "achgaa@itog.biz", EmailComment= "Отдел финансового планирования и бухгалтерского учета" },
                        StructSubvisionTypeId=1
                    };

                    StructSubvision structSubvisionSpo = new StructSubvision
                    {
                        StructSubvisionId = 2,
                        StructSubvisionName = "Факультет среднего профессионального образования",
                        StructSubvisionFioChief = "Черемисин Юрий Михайлович",
                        StructSubvisionPostChiefId = 6,
                        StructSubvisionAdressId = 3,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "dekspo@mail.ru", EmailComment = "Факультет среднего профессионального образования" },
                        StructSubvisionTypeId = 3
                    };

                    StructSubvision structSubvisionInjTech = new StructSubvision
                    {
                        StructSubvisionId = 3,
                        StructSubvisionName = "Инженерно-технологический факультет",
                        StructSubvisionFioChief = "Глобин Андрей Николаевич",
                        StructSubvisionPostChiefId = 6,
                        StructSubvisionAdressId = 3,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "atfachgaa@mail.ru", EmailComment = "Инженерно-технологический факультет" },
                        StructSubvisionTypeId = 3
                    };

                    StructSubvision structSubvisionEnerg = new StructSubvision
                    {
                        StructSubvisionId = 4,
                        StructSubvisionName = "Энергетический факультет",
                        StructSubvisionFioChief = "Степанчук Геннадий Владимирович",
                        StructSubvisionPostChiefId = 6,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "achgaa@itog.biz", EmailComment = "Энергетический факультет" },
                        StructSubvisionTypeId = 3
                    };

                    StructSubvision structSubvisionEconom = new StructSubvision
                    {
                        StructSubvisionId = 5,
                        StructSubvisionName = "Факультет \"Экономика и управление территориями\"",
                        StructSubvisionFioChief = "Рева Алла Федоровна",
                        StructSubvisionPostChiefId = 6,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "achgaa@itog.biz", EmailComment = "Факультет \"Экономика и управление территориями\"" },
                        StructSubvisionTypeId = 3
                    };

                    StructSubvision structSubvisionKafTiIuS = new StructSubvision
                    {
                        StructSubvisionId = 6,
                        StructSubvisionName = "Кафедра \"Теплоэнергетика и информационно-управляющие системы\"",
                        StructSubvisionFioChief = "Литвинов Владимир Николаевич",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 5,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "itius@achgaa.ru", EmailComment = "Кафедра \"Теплоэнергетика и информационно-управляющие системы\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionKafTbiF = new StructSubvision
                    {
                        StructSubvisionId = 7,
                        StructSubvisionName = "Кафедра \"Техносферная безопасность и физика\"",
                        StructSubvisionFioChief = "Шабанов Николай Иванович",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "itius@achgaa.ru", EmailComment = "Кафедра \"Техносферная безопасность и физика\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionKafFiS = new StructSubvision
                    {
                        StructSubvisionId = 8,
                        StructSubvisionName = "Кафедра \"Физвоспитание и спорт\"",
                        StructSubvisionFioChief = "Пятикопов Сергей Михайлович",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 6,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "sport@achgaa.ru", EmailComment = "Кафедра \"Физвоспитание и спорт\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionKafEEOiEM = new StructSubvision
                    {
                        StructSubvisionId = 9,
                        StructSubvisionName = "Кафедра \"Эксплуатация энергетического оборудования и электрических машин\"",
                        StructSubvisionFioChief = "Таранов Михаил Алексеевич",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "expl_el_mach@achgaa.ru", EmailComment = "Кафедра \"Эксплуатация энергетического оборудования и электрических машин\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionKafEEiET = new StructSubvision
                    {
                        StructSubvisionId = 10,
                        StructSubvisionName = "Кафедра \"Электроэнергетика и электротехника\"",
                        StructSubvisionFioChief = "Забродина Ольга Борисовна",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "toeеs@achgaa.ru", EmailComment = "Кафедра \"Электроэнергетика и электротехника\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionKafBUAiA = new StructSubvision
                    {
                        StructSubvisionId = 11,
                        StructSubvisionName = "Кафедра \"Бухгалтерский учет, анализ и аудит\"",
                        StructSubvisionFioChief = "Чумакова Наталья Валерьевна",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 3,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "account@achgaa.ru", EmailComment = "Кафедра \"Бухгалтерский учет, анализ и аудит\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionKafGDiInYaz = new StructSubvision
                    {
                        StructSubvisionId = 12,
                        StructSubvisionName = "Кафедра \"Гуманитарные дисциплины и иностранные языки\"",
                        StructSubvisionFioChief = "Глушко Ирина Васильевна",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 3,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "kppiinjz@achgaa.ru", EmailComment = "Кафедра \"Гуманитарные дисциплины и иностранные языки\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionKafZUiK = new StructSubvision
                    {
                        StructSubvisionId = 13,
                        StructSubvisionName = "Кафедра \"Землеустройство и кадастры\"",
                        StructSubvisionFioChief = "Бондаренко Анатолий Михайлович",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "kppiinjz@achgaa.ru", EmailComment = "Кафедра \"Землеустройство и кадастры\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionEiU = new StructSubvision
                    {
                        StructSubvisionId = 14,
                        StructSubvisionName = "Кафедра \"Экономика и управление\"",
                        StructSubvisionFioChief = "Рева Алла Федоровна",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 3,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "ekonom_i_upravlenie@achgaa.ru", EmailComment = "Кафедра \"Экономика и управление\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionAiSsk = new StructSubvision
                    {
                        StructSubvisionId = 15,
                        StructSubvisionName = "Кафедра \"Агрономия и селекция с/х культур\"",
                        StructSubvisionFioChief = "Хронюк Василий Борисович",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 2,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "tr@achgaa.ru", EmailComment = "Кафедра \"Агрономия и селекция с/х культур\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionVMiM = new StructSubvision
                    {
                        StructSubvisionId = 16,
                        StructSubvisionName = "Кафедра \"Высшая математика и механика\"",
                        StructSubvisionFioChief = "Степовой Дмитрий Владимирович",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 5,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "tr@achgaa.ru", EmailComment = "Кафедра \"Высшая математика и механика\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionTSinAPK = new StructSubvision
                    {
                        StructSubvisionId = 17,
                        StructSubvisionName = "Кафедра \"Технический сервис в агропромышленном комплексе\"",
                        StructSubvisionFioChief = "Никитченко Сергей Леонидович",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 7,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "kaf-ts@achgaa.ru", EmailComment = "Кафедра \"Технический сервис в агропромышленном комплексе\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionTiSmAPK = new StructSubvision
                    {
                        StructSubvisionId = 18,
                        StructSubvisionName = "Кафедра \"Технологии и средства механизации агропромышленного комплекса\"",
                        StructSubvisionFioChief = "Толстоухова Татьяна Николаевна",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 3,
                        StructSubvisionSite = "",
                        StructSubvisionEmail = new Email { EmailValue = "mtppshp@achgaa.ru", EmailComment = "Кафедра \"Технологии и средства механизации агропромышленного комплекса\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionTiA = new StructSubvision
                    {
                        StructSubvisionId = 19,
                        StructSubvisionName = "Кафедра \"Тракторы и автомобили\"",
                        StructSubvisionFioChief = "Нагорский Леонид Алексеевич",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 7,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "mtppshp@achgaa.ru", EmailComment = "Кафедра \"Тракторы и автомобили\"" },
                        StructSubvisionTypeId = 4
                    };

                    StructSubvision structSubvisionEAiTTP = new StructSubvision
                    {
                        StructSubvisionId = 20,
                        StructSubvisionName = "Кафедра \"Эксплуатация автомобилей и технологии транспортных процессов\"",
                        StructSubvisionFioChief = "Щиров Владимир Николаевич",
                        StructSubvisionPostChiefId = 5,
                        StructSubvisionAdressId = 5,
                        StructSubvisionSite = "",
                        //StructSubvisionEmail = new Email { EmailValue = "mtppshp@achgaa.ru", EmailComment = "Кафедра \"Эксплуатация автомобилей и технологии транспортных процессов\"" },
                        StructSubvisionTypeId = 4
                    };

                    await context.StructSubvisions.AddRangeAsync(structSubvision1,
                        structSubvisionSpo,
                        structSubvisionInjTech,
                        structSubvisionEnerg,
                        structSubvisionEconom,
                        structSubvisionKafTiIuS,
                        structSubvisionKafTbiF,
                        structSubvisionKafFiS,
                        structSubvisionKafEEOiEM,
                        structSubvisionKafEEiET,
                        structSubvisionKafBUAiA,
                        structSubvisionKafGDiInYaz,
                        structSubvisionKafZUiK,
                        structSubvisionEiU,
                        structSubvisionAiSsk,
                        structSubvisionVMiM,
                        structSubvisionTSinAPK,
                        structSubvisionTiSmAPK,
                        structSubvisionTiA,
                        structSubvisionEAiTTP);
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
                        StructSubvisionId = 2
                    };

                    StructFacultet facultetInjTech = new StructFacultet
                    {
                        StructFacultetId = 2,
                        StructInstituteId = 1,
                        StructSubvisionId = 3
                    };

                    StructFacultet facultetEnerg = new StructFacultet
                    {
                        StructFacultetId = 3,                        
                        StructInstituteId = 1,
                        StructSubvisionId = 4
                    };

                    StructFacultet facultetEconom = new StructFacultet
                    {
                        StructFacultetId = 4,                        
                        StructInstituteId = 1,
                        StructSubvisionId = 5
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
                        StructSubvisionId = 6
                    };

                    StructKaf kaf2 = new StructKaf
                    {
                        StructKafId = 2,
                        StructFacultetId = 3,
                        StructSubvisionId = 7
                    };

                    StructKaf kaf3 = new StructKaf
                    {
                        StructKafId = 3,
                        StructFacultetId = 3,
                        StructSubvisionId = 8
                    };

                    StructKaf kaf4 = new StructKaf
                    {
                        StructKafId = 4,
                        StructFacultetId = 3,
                        StructSubvisionId = 9
                    };

                    StructKaf kaf5 = new StructKaf
                    {
                        StructKafId = 5,
                        StructFacultetId = 3,
                        StructSubvisionId = 10
                    };

                    StructKaf kaf6 = new StructKaf
                    {
                        StructKafId = 6,
                        StructFacultetId = 4,
                        StructSubvisionId = 11
                    };

                    StructKaf kaf7 = new StructKaf
                    {
                        StructKafId = 7,
                        StructFacultetId = 4,
                        StructSubvisionId = 12
                    };

                    StructKaf kaf8 = new StructKaf
                    {
                        StructKafId = 8,
                        StructFacultetId = 4,
                        StructSubvisionId = 13
                    };

                    StructKaf kaf9 = new StructKaf
                    {
                        StructKafId = 9,
                        StructFacultetId = 4,
                        StructSubvisionId = 14
                    };

                    StructKaf kaf10 = new StructKaf
                    {
                        StructKafId = 10,
                        StructFacultetId = 4,
                        StructSubvisionId = 15
                    };

                    StructKaf kaf11 = new StructKaf
                    {
                        StructKafId = 11,
                        StructFacultetId = 2,
                        StructSubvisionId = 16
                    };

                    StructKaf kaf12 = new StructKaf
                    {
                        StructKafId = 12,
                        StructFacultetId = 2,
                        StructSubvisionId = 17
                    };

                    StructKaf kaf13 = new StructKaf
                    {
                        StructKafId = 13,
                        StructFacultetId = 2,
                        StructSubvisionId = 18
                    };

                    StructKaf kaf14 = new StructKaf
                    {
                        StructKafId = 14,
                        StructFacultetId = 2,
                        StructSubvisionId = 19
                    };

                    StructKaf kaf15 = new StructKaf
                    {
                        StructKafId = 15,
                        StructFacultetId = 2,
                        StructSubvisionId = 20
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

                #region Инициализация таблицы "Типы содержимого файла"
                if (!await context.FileDataTypes.AnyAsync())
                {
                    FileDataType fileDataType1 = new FileDataType
                    {
                        FileDataTypeId=1,
                        FileDataTypeName="Положения о структурных подразделениях"
                    };
                                        

                    await context.FileDataTypes.AddRangeAsync(
                        fileDataType1
                        );
                    await context.SaveChangesAsync();
                }
                #endregion                                
            }
        }
    }
}
