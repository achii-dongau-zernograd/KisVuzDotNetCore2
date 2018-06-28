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
    public class InitDatabaseFormKontrol
    {
        /// <summary>
        /// Инициализация таблицы "Формы контроля"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateFormKontrol(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Формы контроля"
                if (!await context.FormKontrol.AnyAsync())
                {
                    FormKontrol FormKontrol1 = new FormKontrol
                    {
                        FormKontrolId = 1,
                        FormKontrolName = "Экзамен"
                    };

                    FormKontrol FormKontrol2 = new FormKontrol
                    {
                        FormKontrolId = 2,
                        FormKontrolName = "Зачет"
                    };

                    FormKontrol FormKontrol3 = new FormKontrol
                    {
                        FormKontrolId = 3,
                        FormKontrolName = "Зачет с оценкой"
                    };

                    FormKontrol FormKontrol4 = new FormKontrol
                    {
                        FormKontrolId = 4,
                        FormKontrolName = "Курсовой проект"
                    };

                    FormKontrol FormKontrol5 = new FormKontrol
                    {
                        FormKontrolId = 5,
                        FormKontrolName = "Курсовая работа"
                    };

                    FormKontrol FormKontrol6 = new FormKontrol
                    {
                        FormKontrolId = 6,
                        FormKontrolName = "Расче-графическая работа"
                    };

                    await context.FormKontrol.AddRangeAsync(
                        FormKontrol1,
                        FormKontrol2,
                        FormKontrol3,
                        FormKontrol4,
                        FormKontrol5,
                        FormKontrol6
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
