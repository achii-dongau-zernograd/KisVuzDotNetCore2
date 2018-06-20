using KisVuzDotNetCore2.Models.Education;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    /// <summary>
    /// Инициализация таблиц сведений об образовательном процессе
    /// </summary>
    public static class InitDatabaseEducationData
    {
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
                        EduKursNumber = 1,
                        EduKursName = "Первый курс"
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
                            EduAccredId = 1
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

    }
}
