using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{

    public class InitDatabasePatents
    {
        /// <summary>
        /// Инициализация таблицы "Группы видов патентов (свидетельств)"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreatePatentVidGroups(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Группы видов патентов (свидетельств)"
                if (!await context.PatentVidGroups.AnyAsync())
                {
                    PatentVidGroup PatentVidGroup1 = new PatentVidGroup
                    {
                        PatentVidGroupId = 1,
                        PatentVidGroupName = "Патенты"
                    };

                    PatentVidGroup PatentVidGroup2 = new PatentVidGroup
                    {
                        PatentVidGroupId = 2,
                        PatentVidGroupName = "Свидетельства"
                    };

                    await context.PatentVidGroups.AddRangeAsync(
                                    PatentVidGroup1,
                                    PatentVidGroup2
                                );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

        /// <summary>
        /// Инициализация таблицы "Виды патентов (свидетельств)"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreatePatentVids(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Виды патентов (свидетельств)"
                if (!await context.PatentVids.AnyAsync())
                {
                    PatentVid PatentVid1 = new PatentVid
                    {
                        PatentVidId = (int) FileDataTypeEnum.PatentNaIzobretenie,
                        PatentVidGroupId = 1,
                        PatentVidName = "Патент на изобретение"
                    };

                    PatentVid PatentVid2 = new PatentVid
                    {
                        PatentVidId = (int) FileDataTypeEnum.PatentNaPoleznuyuModel,
                        PatentVidGroupId = 1,
                        PatentVidName = "Патент на полезную модель"
                    };

                    PatentVid PatentVid3 = new PatentVid
                    {
                        PatentVidId = (int) FileDataTypeEnum.PatentNaSelekcionnoeDostigenie,
                        PatentVidGroupId = 1,
                        PatentVidName = "Патент на селекционное достижение"
                    };

                    PatentVid PatentVid4 = new PatentVid
                    {
                        PatentVidId = (int) FileDataTypeEnum.PatentNaPromyshlenniyObrazets,
                        PatentVidGroupId = 1,
                        PatentVidName = "Патент на промышленный образец"
                    };

                    PatentVid PatentVid5 = new PatentVid
                    {
                        PatentVidId = (int)FileDataTypeEnum.SvidetelstvoNaProgrammu,
                        PatentVidGroupId = 2,
                        PatentVidName = "Свидетельство на регистрацию программы для ЭВМ"
                    };

                    PatentVid PatentVid6 = new PatentVid
                    {
                        PatentVidId = (int)FileDataTypeEnum.SvidetelstvoNaBazuDannih,
                        PatentVidGroupId = 2,
                        PatentVidName = "Свидетельство на регистрацию базы данных"
                    };

                    await context.PatentVids.AddRangeAsync(
                                    PatentVid1,
                                    PatentVid2,
                                    PatentVid3,
                                    PatentVid4,
                                    PatentVid5,
                                    PatentVid6
                                );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}