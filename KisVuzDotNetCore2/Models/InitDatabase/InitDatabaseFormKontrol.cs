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
    /// Инициализация таблицы "Формы контроля"
    /// </summary>
    public class InitDatabaseFormKontrolName
    {
        /// <summary>
        /// Инициализация таблицы "Формы контроля"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateFormKontrolName(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Формы контроля"
                if (!await context.FormKontrolNames.AnyAsync())
                {
                    FormKontrolName FormKontrolName1 = new FormKontrolName
                    {
                        FormKontrolNameId = 1,
                        FormKontrolNameName = "Экзамен"
                    };

                    FormKontrolName FormKontrolName2 = new FormKontrolName
                    {
                        FormKontrolNameId = 2,
                        FormKontrolNameName = "Зачет"
                    };

                    FormKontrolName FormKontrolName3 = new FormKontrolName
                    {
                        FormKontrolNameId = 3,
                        FormKontrolNameName = "Зачет с оценкой"
                    };

                    FormKontrolName FormKontrolName4 = new FormKontrolName
                    {
                        FormKontrolNameId = 4,
                        FormKontrolNameName = "Курсовой проект"
                    };

                    FormKontrolName FormKontrolName5 = new FormKontrolName
                    {
                        FormKontrolNameId = 5,
                        FormKontrolNameName = "Курсовая работа"
                    };

                    FormKontrolName FormKontrolName6 = new FormKontrolName
                    {
                        FormKontrolNameId = 6,
                        FormKontrolNameName = "Расчетно-графическая работа"
                    };

                    await context.FormKontrolNames.AddRangeAsync(
                        FormKontrolName1,
                        FormKontrolName2,
                        FormKontrolName3,
                        FormKontrolName4,
                        FormKontrolName5,
                        FormKontrolName6
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
