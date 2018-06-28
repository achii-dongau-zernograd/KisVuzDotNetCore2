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
    public class InitDatabaseVidUchebRaboti
    {
        /// <summary>
        /// Инициализация таблицы "Виды учебной работы"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateVidUchebRaboti(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Виды учебной работы"
                if (!await context.VidUchebRaboti.AnyAsync())
                {
                    VidUchebRaboti VidUchebRaboti1 = new VidUchebRaboti
                    {
                        VidUchebRabotiId = 1,
                        VidUchebRabotiName = "Лекции"
                    };

                    VidUchebRaboti VidUchebRaboti2 = new VidUchebRaboti
                    {
                        VidUchebRabotiId = 2,
                        VidUchebRabotiName = "Лабораторные работы"
                    };

                    VidUchebRaboti VidUchebRaboti3 = new VidUchebRaboti
                    {
                        VidUchebRabotiId = 3,
                        VidUchebRabotiName = "Практические занятия"
                    };

                    VidUchebRaboti VidUchebRaboti4 = new VidUchebRaboti
                    {
                        VidUchebRabotiId = 4,
                        VidUchebRabotiName = "Самостоятельная работа"
                    };

                    VidUchebRaboti VidUchebRaboti5 = new VidUchebRaboti
                    {
                        VidUchebRabotiId = 5,
                        VidUchebRabotiName = "Контроль"
                    };

                    await context.VidUchebRaboti.AddRangeAsync(
                        VidUchebRaboti1,
                        VidUchebRaboti2,
                        VidUchebRaboti3,
                        VidUchebRaboti4,
                        VidUchebRaboti5
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
