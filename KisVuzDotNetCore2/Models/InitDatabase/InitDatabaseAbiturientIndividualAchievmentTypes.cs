using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    internal class InitDatabaseAbiturientIndividualAchievmentTypes
    {
        /// <summary>
        /// Инициализация таблицы "Типы индивидуальных достижений абитуриента"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateAbiturientIndividualAchievmentTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы индивидуальных достижений абитуриента"
                if (!await context.AbiturientIndividualAchievmentTypes.AnyAsync())
                {
                    var row25 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 25,
                        AbiturientIndividualAchievmentTypeName = "Диплом СПО с отличием",
                        Point = 5
                    };

                    var row26 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 26,
                        AbiturientIndividualAchievmentTypeName = "Знак ГТО",
                        Point = 5
                    };

                    var row27 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 27,
                        AbiturientIndividualAchievmentTypeName = "Аттестат с отличием (золото)",
                        Point = 5
                    };

                    var row28 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 28,
                        AbiturientIndividualAchievmentTypeName = "Аттестат с отличием (серебро)",
                        Point = 5
                    };

                    var row29 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 29,
                        AbiturientIndividualAchievmentTypeName = "Диплом ВО с отличием",
                        Point = 1
                    };

                    var row30 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 30,
                        AbiturientIndividualAchievmentTypeName = "Победитель всероссийского этапа ВСО",
                        Point = 5
                    };

                    var row31 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 31,
                        AbiturientIndividualAchievmentTypeName = "2 место всероссийского этапа ВСО",
                        Point = 4
                    };

                    var row32 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 32,
                        AbiturientIndividualAchievmentTypeName = "3 место всероссийского этапа ВСО",
                        Point = 3
                    };

                    var row33 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 33,
                        AbiturientIndividualAchievmentTypeName = "Участие в ВСО по направлению подготовки",
                        Point = 2
                    };

                    var row34 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 34,
                        AbiturientIndividualAchievmentTypeName = "Участие в ВСО не по направлению подготовки",
                        Point = 1
                    };

                    var row35 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 35,
                        AbiturientIndividualAchievmentTypeName = "Патент на изобретение, ТУ и ТО",
                        Point = 1
                    };

                    //var row36 = new AbiturientIndividualAchievmentType
                    //{
                    //    AbiturientIndividualAchievmentTypeId = 36,
                    //    AbiturientIndividualAchievmentTypeName = "Диплом СПО с отличием",
                    //    Point = 5
                    //};

                    var row37 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 37,
                        AbiturientIndividualAchievmentTypeName = "Внешний грант на проведение научных исследований",
                        Point = 2
                    };

                    var row38 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 38,
                        AbiturientIndividualAchievmentTypeName = "Статьи в ВАК РФ",
                        Point = 2
                    };

                    var row39 = new AbiturientIndividualAchievmentType
                    {
                        AbiturientIndividualAchievmentTypeId = 39,
                        AbiturientIndividualAchievmentTypeName = "Статьи в РИНЦ",
                        Point = 1
                    };

                    //var row40 = new AbiturientIndividualAchievmentType
                    //{
                    //    AbiturientIndividualAchievmentTypeId = 40,
                    //    AbiturientIndividualAchievmentTypeName = "Знак ГТО",
                    //    Point = 5
                    //};

                    await context.AbiturientIndividualAchievmentTypes.AddRangeAsync(
                                                           row25, row26, row27, row28, row29,
                        row30, row31, row32, row33, row34, row35, /*row36,*/ row37, row38, row39//,
                        //row40
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}