using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class InitDatabaseGraduateYear
    {
        /// <summary>
        /// Инициализация таблицы "Год выпуска"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateGraduateYear(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Срок Обучения"
                if (!await context.GraduateYear.AnyAsync())
                {
                    GraduateYear GraduateYear1 = new GraduateYear
                    {
                        GraduateYearId = 1,
                        GraduateYearName = "2016 год"
                    };

                    GraduateYear GraduateYear2 = new GraduateYear
                    {
                        GraduateYearId = 2,
                        GraduateYearName = "2017 год"
                    };

                    GraduateYear GraduateYear3 = new GraduateYear
                    {
                        GraduateYearId = 3,
                        GraduateYearName = "2018 год"
                    };

                    await context.GraduateYear.AddRangeAsync(
                        GraduateYear1,
                        GraduateYear2,
                        GraduateYear3
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
