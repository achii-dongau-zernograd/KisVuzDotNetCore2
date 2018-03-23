using KisVuzDotNetCore2.Models.Education;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    public class AppIdentityDBContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDBContext(DbContextOptions<AppIdentityDBContext> options) : base (options)
        {
            
        }

        // Education
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
        /// Сведения об образовательной организации.
        /// Структура и органы управления образовательной организацией
        /// </summary>
        public DbSet<StructOrgUprav> StructOrgUprav { get; set; }

        /// <summary>
        /// Файлы
        /// </summary>
        public DbSet<FileModel> Files { get; set; }


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

        public static async Task CreateEducationData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Уровни образования"
                if (! await context.EduLevels.AnyAsync())
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

                    await context.EduLevels.AddRangeAsync(new EduLevel[] { eduLevel1Spo,
                        eduLevel2VoBak,
                        eduLevel3VoMag,
                        eduLevel4VoSpec,
                        eduLevel5VoAsp });
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
                            EduNapravlId=1,
                            EduNapravCode = "08.02.09",
                            EduNapravName = "Монтаж, наладка и эксплуатация электрооборудования промышленных и гражданских зданий",
                            EduUgsId=1
                        };
                    EduNapravl eduNapr210205 =
                        new EduNapravl
                        {
                            EduNapravlId = 2,
                            EduNapravCode = "21.02.05",
                            EduNapravName = "Земельно-имущественные отношения",
                            EduUgsId = 2
                        };
                    EduNapravl eduNapr230203 =
                        new EduNapravl
                        {
                            EduNapravlId = 3,
                            EduNapravCode = "23.02.03",
                            EduNapravName = "Техническое обслуживание и ремонт автомобильного транспорта",
                            EduUgsId = 3
                        };
                    EduNapravl eduNapr350208 =
                        new EduNapravl
                        {
                            EduNapravlId = 4,
                            EduNapravCode = "35.02.08",
                            EduNapravName = "Электрификация и автоматизация сельского хозяйства",
                            EduUgsId = 4
                        };
                    EduNapravl eduNapr380201 =
                        new EduNapravl
                        {
                            EduNapravlId = 5,
                            EduNapravCode = "38.02.01",
                            EduNapravName = "Экономика и бухгалтерский учет (по отраслям)",
                            EduUgsId = 5
                        };
                    EduNapravl eduNapr380204 =
                        new EduNapravl
                        {
                            EduNapravlId = 6,
                            EduNapravCode = "38.02.04",
                            EduNapravName = "Коммерция (по отраслям)",
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
                            EduUgsId = 6
                        };

                    EduNapravl eduNapr130302 =
                        new EduNapravl
                        {
                            EduNapravlId = 8,
                            EduNapravCode = "13.03.02",
                            EduNapravName = "Электроэнергетика и электротехника",
                            EduUgsId = 6
                        };

                    EduNapravl eduNapr190302 =
                        new EduNapravl
                        {
                            EduNapravlId = 9,
                            EduNapravCode = "19.03.02",
                            EduNapravName = "Продукты питания из растительного сырья",
                            EduUgsId = 7
                        };

                    EduNapravl eduNapr200301 =
                        new EduNapravl
                        {
                            EduNapravlId = 10,
                            EduNapravCode = "20.03.01",
                            EduNapravName = "Техносферная безопасность",
                            EduUgsId = 8
                        };

                    EduNapravl eduNapr210302 =
                        new EduNapravl
                        {
                            EduNapravlId = 11,
                            EduNapravCode = "21.03.02",
                            EduNapravName = "Землеустройство и кадастры",
                            EduUgsId = 9
                        };

                    EduNapravl eduNapr230301 =
                        new EduNapravl
                        {
                            EduNapravlId = 12,
                            EduNapravCode = "23.03.01",
                            EduNapravName = "Технология транспортных процессов",
                            EduUgsId = 10
                        };

                    EduNapravl eduNapr230303 =
                        new EduNapravl
                        {
                            EduNapravlId = 13,
                            EduNapravCode = "23.03.03",
                            EduNapravName = "Эксплуатация транспортно-технологических машин и комплексов",
                            EduUgsId = 10
                        };

                    EduNapravl eduNapr350304 =
                        new EduNapravl
                        {
                            EduNapravlId = 14,
                            EduNapravCode = "35.03.04",
                            EduNapravName = "Агрономия",
                            EduUgsId = 11
                        };

                    EduNapravl eduNapr350306 =
                        new EduNapravl
                        {
                            EduNapravlId = 15,
                            EduNapravCode = "35.03.06",
                            EduNapravName = "Агроинженерия",
                            EduUgsId = 11
                        };

                    EduNapravl eduNapr380301 =
                        new EduNapravl
                        {
                            EduNapravlId = 16,
                            EduNapravCode = "38.03.01",
                            EduNapravName = "Экономика",
                            EduUgsId = 12
                        };

                    EduNapravl eduNapr380302 =
                        new EduNapravl
                        {
                            EduNapravlId = 17,
                            EduNapravCode = "38.03.02",
                            EduNapravName = "Менеджмент",
                            EduUgsId = 12
                        };

                    EduNapravl eduNapr380304 =
                        new EduNapravl
                        {
                            EduNapravlId = 18,
                            EduNapravCode = "38.03.04",
                            EduNapravName = "Государственное и муниципальное управление",
                            EduUgsId = 12
                        };

                    EduNapravl eduNapr440304 =
                        new EduNapravl
                        {
                            EduNapravlId = 19,
                            EduNapravCode = "44.03.04",
                            EduNapravName = "Профессиональное обучение",
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
                            EduUgsId = 14
                        };

                    EduNapravl eduNapr130402 =
                        new EduNapravl
                        {
                            EduNapravlId = 21,
                            EduNapravCode = "13.04.02",
                            EduNapravName = "Электроэнергетика и электротехника",
                            EduUgsId = 14
                        };
                                       

                    EduNapravl eduNapr230401 =
                        new EduNapravl
                        {
                            EduNapravlId = 22,
                            EduNapravCode = "23.04.01",
                            EduNapravName = "Технология транспортных процессов",
                            EduUgsId = 15
                        };

                    EduNapravl eduNapr230403 =
                        new EduNapravl
                        {
                            EduNapravlId = 23,
                            EduNapravCode = "23.04.03",
                            EduNapravName = "Эксплуатация транспортно-технологических машин и комплексов",
                            EduUgsId = 15
                        };

                    EduNapravl eduNapr350404 =
                        new EduNapravl
                        {
                            EduNapravlId = 24,
                            EduNapravCode = "35.04.04",
                            EduNapravName = "Агрономия",
                            EduUgsId = 16
                        };

                    EduNapravl eduNapr350406 =
                        new EduNapravl
                        {
                            EduNapravlId = 25,
                            EduNapravCode = "35.04.06",
                            EduNapravName = "Агроинженерия",
                            EduUgsId = 16
                        };

                    EduNapravl eduNapr380401 =
                        new EduNapravl
                        {
                            EduNapravlId = 26,
                            EduNapravCode = "38.04.01",
                            EduNapravName = "Экономика",
                            EduUgsId = 17
                        };

                    EduNapravl eduNapr380402 =
                        new EduNapravl
                        {
                            EduNapravlId = 27,
                            EduNapravCode = "38.04.02",
                            EduNapravName = "Менеджмент",
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
                            EduUgsId = 18
                        };

                    EduNapravl eduNapr380501 =
                        new EduNapravl
                        {
                            EduNapravlId = 29,
                            EduNapravCode = "38.05.01",
                            EduNapravName = "Экономическая безопасность",
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
                            EduUgsId = 20
                        };
                    EduNapravl eduNapr230601 =
                        new EduNapravl
                        {
                            EduNapravlId = 31,
                            EduNapravCode = "23.06.01",
                            EduNapravName = "Техника и технологии наземного транспорта",
                            EduUgsId = 21
                        };
                    EduNapravl eduNapr350601 =
                        new EduNapravl
                        {
                            EduNapravlId = 32,
                            EduNapravCode = "35.06.01",
                            EduNapravName = "Сельское хозяйство",
                            EduUgsId = 22
                        };
                    EduNapravl eduNapr350604 =
                        new EduNapravl
                        {
                            EduNapravlId = 33,
                            EduNapravCode = "35.06.04",
                            EduNapravName = "Технологии, средства механизации и энергетическое оборудование в сельском, лесном и рыбном хозяйстве",
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
            }

        }
    }
}
