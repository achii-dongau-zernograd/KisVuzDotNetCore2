using KisVuzDotNetCore2.Models.Files;
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
    /// Инициализация таблиц, связанных с файловыми операциями
    /// </summary>
    public static class InitDatabaseFilesData
    {
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

                    FileDataTypeGroup fileDataTypeGroup5 = new FileDataTypeGroup
                    {
                        FileDataTypeGroupId = 5,
                        FileDataTypeGroupName = "Учебно-программная документация"
                    };

                    FileDataTypeGroup fileDataTypeGroup6 = new FileDataTypeGroup
                    {
                        FileDataTypeGroupId = 6,
                        FileDataTypeGroupName = "Учебно-методическое обеспечение"
                    };

                    FileDataTypeGroup fileDataTypeGroup7 = new FileDataTypeGroup
                    {
                        FileDataTypeGroupId = 7,
                        FileDataTypeGroupName = "Сведения об образовательной организации - Финансово-хозяйственная деятельность"
                    };

                    FileDataTypeGroup fileDataTypeGroup8 = new FileDataTypeGroup
                    {
                        FileDataTypeGroupId = 8,
                        FileDataTypeGroupName = "Сведения об образовательной организации - Стипендии и иные виды материальной поддержки"
                    };

                    FileDataTypeGroup fileDataTypeGroup9 = new FileDataTypeGroup
                    {
                        FileDataTypeGroupId = 9,
                        FileDataTypeGroupName = "Работы пользователей"
                    };

                    await context.FileDataTypeGroups.AddRangeAsync(
                        fileDataTypeGroup1,
                        fileDataTypeGroup2,
                        fileDataTypeGroup3,
                        fileDataTypeGroup4,
                        fileDataTypeGroup5,
                        fileDataTypeGroup6,
                        fileDataTypeGroup7,
                        fileDataTypeGroup8,
                        fileDataTypeGroup9
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
                        FileDataTypeId = (int)FileDataTypeEnum.UstavObrazovatelnoyOrganizatsii,
                        FileDataTypeName = "Устав образовательной организации",
                        Itemprop = "ustavDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType4 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.LicenziyaNaOsushestvlenieObrazovatelnoyDeyatelnosti,
                        FileDataTypeName = "Лицензия на осуществление образовательной деятельности",
                        Itemprop = "licenseDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType5 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.SvidetelstvoOGosudarstvennoyAccreditatsii,
                        FileDataTypeName = "Свидетельство о государственной аккредитации (с приложениями)",
                        Itemprop = "accreditationDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType6 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.PlanFinansovoHozyaystvennoyDeyatelnosti,
                        FileDataTypeName = "План финансово-хозяйственной деятельности образовательной организации",
                        Itemprop = "finPlanDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType7 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.LocalnieNormativnieActiReglamentiruyushiePravilaPriemaObuchaushihsya,
                        FileDataTypeName = "Локальные нормативные акты, регламентирующие правила приема обучающихся",
                        Itemprop = "priemDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType8 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.LocalnieNormativnieActiReglamentiruyushieRejimZanyatiyObuchaushihsya,
                        FileDataTypeName = "Локальные нормативные акты, регламентирующие режим занятий обучающихся",
                        Itemprop = "modeDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType9 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.LocalnieNormativnieActiReglamentiruyushieTekushiyKontrolIPromAttestObuchaushihsya,
                        FileDataTypeName = "Локальные нормативные акты, регламентирующие формы, периодичность и порядок текущего контроля успеваемости и промежуточной аттестации обучающихся",
                        Itemprop = "tek_KontrolDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType10 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.LocalnieNormativnieActiReglamentiruyushiePerevodOtchislenieIVosstanovlenieObuchaushihsya,
                        FileDataTypeName = "Локальные нормативные акты, регламентирующие порядок и основания перевода, отчисления и восстановления обучающихся",
                        Itemprop = "perevodDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType11 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.LocalnieNormativnieActiReglamentiruyushieOtnosheniyaSNesovershennoletnimiObuchayushimisya,
                        FileDataTypeName = "Локальные нормативные акты, регламентирующие порядок оформления возникновения, приостановления и прекращения отношений между образовательной организацией, обучающимися и (или) родителями (законными представителями) несовершеннолетних обучающихся",
                        Itemprop = "vozDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType12 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.PravilaVnutrennegoRasporyadkaObuchaushihsya,
                        FileDataTypeName = "Правила внутреннего распорядка обучающихся",
                        Itemprop = "localActStud",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType13 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.PravilaVnutrennegoTrudovogoRasporyadka,
                        FileDataTypeName = "Правила внутреннего трудового распорядка",
                        Itemprop = "localActOrder",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType14 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.KollektivniyDogovor,
                        FileDataTypeName = "Коллективный договор",
                        Itemprop = "localActCollec",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType15 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.OtchetORezultatahSamoobsledovaniya,
                        FileDataTypeName = "Отчет о результатах самообследования",
                        Itemprop = "reportEduDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType16 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.PoryadokOkazaniyaPlatnihObrazovatelnihUslug,
                        FileDataTypeName = "Документ о порядке оказания платных образовательных услуг",
                        Itemprop = "paidEduDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType17 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.ObrazecDogovoraObOkazaniiPlatnihObrazovatelnihUslug,
                        FileDataTypeName = "Образец договора об оказании платных образовательных услуг",
                        Itemprop = "paidEduDogDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType18 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.DocumentObUtverjdeniiStoimostiObucheniya,
                        FileDataTypeName = "Документ об утверждении стоимости обучения по каждой образовательной программе",
                        Itemprop = "paidEduStoimDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType19 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.PredpisaniyaOrganovNadzora,
                        FileDataTypeName = "Предписания органов, осуществляющих государственный контроль (надзор) в сфере образования",
                        Itemprop = "prescriptionDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType20 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.OtchetiObIspolneniiPredpisaniyOrganovNadzora,
                        FileDataTypeName = "Отчеты об исполнении предписаний органов, осуществляющих государственный контроль (надзор) в сфере образования",
                        Itemprop = "prescriptionOtchetDocLink",
                        FileDataTypeGroupId = 3
                    };

                    FileDataType fileDataType21 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.UdostoverenieOPovisheniiKvalifikacii,
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
                        FileDataTypeId = (int)FileDataTypeEnum.DiplomOProfessionalnoyPerepodgotovke,
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

                    FileDataType fileDataType31 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.UchebniyPlan,
                        FileDataTypeName = "Учебный план",
                        Itemprop = "",
                        FileDataTypeGroupId = 5
                    };

                    FileDataType fileDataType32 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.OPOP,
                        FileDataTypeName = "Основная профессиональная образовательная программа",
                        Itemprop = "",
                        FileDataTypeGroupId = 5
                    };

                    FileDataType fileDataType33 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.KalendarniyUchebniyGraphik,
                        FileDataTypeName = "Календарный учебный график",
                        Itemprop = "",
                        FileDataTypeGroupId = 5
                    };

                    FileDataType fileDataType34 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.RabProgrammaDisciplini,
                        FileDataTypeName = "Рабочая программа",
                        Itemprop = "",
                        FileDataTypeGroupId = 5
                    };

                    FileDataType fileDataType35 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.Annotation,
                        FileDataTypeName = "Аннотация рабочей программы",
                        Itemprop = "",
                        FileDataTypeGroupId = 5
                    };                                     

                    FileDataType fileDataType36 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.UchebnoePosobie,
                        FileDataTypeName = "Учебное пособие",
                        Itemprop = "",
                        FileDataTypeGroupId = 6
                    };

                    FileDataType fileDataType37 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.KursLekcii,
                        FileDataTypeName = "Курс лекций",
                        Itemprop = "",
                        FileDataTypeGroupId = 6
                    };

                    FileDataType fileDataType38 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.LaboratorniiPraktikum,
                        FileDataTypeName = "Лабораторный практикум",
                        Itemprop = "",
                        FileDataTypeGroupId = 6
                    };

                    FileDataType fileDataType39 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.MetodicheskieUkazaniya,
                        FileDataTypeName = "Методические указания",
                        Itemprop = "",
                        FileDataTypeGroupId = 6
                    };

                    FileDataType fileDataType40 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.InfOPostupleniiIRashodovaniiFinIMaterialnihSredstv,
                        FileDataTypeName = "Информация о поступлении и расходовании финансовых и материальных средств",
                        Itemprop = "finRec",
                        FileDataTypeGroupId = 7
                    };

                    FileDataType fileDataType41 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.StipendiiFederalGrant,
                        FileDataTypeName = "Федеральный нормативный акт, регламентирующий наличие и условия предоставления стипендии",
                        Itemprop = "federalGrant",
                        FileDataTypeGroupId = 8
                    };

                    FileDataType fileDataType42 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.StipendiiLocalGrant,
                        FileDataTypeName = "Локальный нормативный акт, регламентирующий наличие и условия предоставления стипендии",
                        Itemprop = "localGrant",
                        FileDataTypeGroupId = 8
                    };

                    FileDataType fileDataType43 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.FondOcenochnihSredstv,
                        FileDataTypeName = "Фонд оценочных средств",
                        Itemprop = "",
                        FileDataTypeGroupId = 5
                    };

                    FileDataType fileDataType44 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.UserWork,
                        FileDataTypeName = "Работы пользователей",
                        Itemprop = "",
                        FileDataTypeGroupId = 9
                    };

                    FileDataType fileDataType45 = new FileDataType
                    {
                        FileDataTypeId = (int)FileDataTypeEnum.UserWorkRecenziya,
                        FileDataTypeName = "Рецензия на работу пользователя",
                        Itemprop = "",
                        FileDataTypeGroupId = 9
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
                        fileDataType30,
                        fileDataType31,
                        fileDataType32,
                        fileDataType33,
                        fileDataType34,
                        fileDataType35,
                        fileDataType36,
                        fileDataType37,
                        fileDataType38,
                        fileDataType39,
                        fileDataType40,
                        fileDataType41,
                        fileDataType42,
                        fileDataType43,
                        fileDataType44,
                        fileDataType45
                        );
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Заполнение тестовыми данными таблицы "Файлы"
                if (!await context.Files.AnyAsync())
                {
                    FileModel fileDataTypeGroup1 = new FileModel
                    {
                        Name = "Описание файла",
                        FileToFileTypes = new List<FileToFileType> { new FileToFileType { FileDataTypeId = 3 } }
                    };


                    await context.Files.AddRangeAsync(
                        fileDataTypeGroup1
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

    }
}
