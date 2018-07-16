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
    /// Инициализация таблицы "Виды учебной работы"
    /// </summary>
    public class InitDatabaseVidUchebRabotiName
    {
        /// <summary>
        /// Инициализация таблицы "Виды учебной работы"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateVidUchebRabotiNames(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Виды учебной работы"
                if (!await context.VidUchebRabotiNames.AnyAsync())
                {
                    VidUchebRabotiName VidUchebRabotiName1 = new VidUchebRabotiName
                    {
                        VidUchebRabotiNameId = 1,
                        VidUchebRabotiNameName = "Лекции"
                    };

                    VidUchebRabotiName VidUchebRabotiName2 = new VidUchebRabotiName
                    {
                        VidUchebRabotiNameId = 2,
                        VidUchebRabotiNameName = "Лабораторные работы"
                    };

                    VidUchebRabotiName VidUchebRabotiName3 = new VidUchebRabotiName
                    {
                        VidUchebRabotiNameId = 3,
                        VidUchebRabotiNameName = "Практические занятия"
                    };

                    VidUchebRabotiName VidUchebRabotiName4 = new VidUchebRabotiName
                    {
                        VidUchebRabotiNameId = 4,
                        VidUchebRabotiNameName = "Самостоятельная работа"
                    };

                    VidUchebRabotiName VidUchebRabotiName5 = new VidUchebRabotiName
                    {
                        VidUchebRabotiNameId = 5,
                        VidUchebRabotiNameName = "Контроль"
                    };

                    await context.VidUchebRabotiNames.AddRangeAsync(
                        VidUchebRabotiName1,
                        VidUchebRabotiName2,
                        VidUchebRabotiName3,
                        VidUchebRabotiName4,
                        VidUchebRabotiName5
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
