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
    public class InitDatabaseSemestrName
    {
        /// <summary>
        /// Инициализация таблицы "Семестры (справочник)"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateSemestrName(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Семестры (справочник)"
                if (!await context.SemestrNames.AnyAsync())
                {
                    SemestrName SemestrName1 = new SemestrName
                    {
                        SemestrNameId = 1,
                        SemestrNameNumber = 1,
                        SemestrNameName = "Первый семестр"
                    };

                    SemestrName SemestrName2 = new SemestrName
                    {
                        SemestrNameId = 2,
                        SemestrNameNumber = 2,
                        SemestrNameName = "Второй семестр"
                    };

                    SemestrName SemestrName3 = new SemestrName
                    {
                        SemestrNameId = 3,
                        SemestrNameNumber = 3,
                        SemestrNameName = "Третий семестр"
                    };

                    SemestrName SemestrName4 = new SemestrName
                    {
                        SemestrNameId = 4,
                        SemestrNameNumber = 4,
                        SemestrNameName = "Четвертый семестр"
                    };

                    SemestrName SemestrName5 = new SemestrName
                    {
                        SemestrNameId = 5,
                        SemestrNameNumber = 5,
                        SemestrNameName = "Пятый семестр"
                    };

                    SemestrName SemestrName6 = new SemestrName
                    {
                        SemestrNameId = 6,
                        SemestrNameNumber = 6,
                        SemestrNameName = "Шестой семестр"
                    };

                    SemestrName SemestrName7 = new SemestrName
                    {
                        SemestrNameId = 7,
                        SemestrNameNumber = 7,
                        SemestrNameName = "Седьмой семестр"
                    };

                    SemestrName SemestrName8 = new SemestrName
                    {
                        SemestrNameId = 8,
                        SemestrNameNumber = 8,
                        SemestrNameName = "Восьмой семестр"
                    };

                    SemestrName SemestrName9 = new SemestrName
                    {
                        SemestrNameId = 9,
                        SemestrNameNumber = 9,
                        SemestrNameName = "Девятый семестр"
                    };

                    SemestrName SemestrName10 = new SemestrName
                    {
                        SemestrNameId = 10,
                        SemestrNameNumber = 10,
                        SemestrNameName = "Десятый семестр"
                    };

                    SemestrName SemestrName11 = new SemestrName
                    {
                        SemestrNameId = 11,
                        SemestrNameNumber = 11,
                        SemestrNameName = "Одиннадцатый семестр"
                    };

                    SemestrName SemestrName12 = new SemestrName
                    {
                        SemestrNameId = 12,
                        SemestrNameNumber = 12,
                        SemestrNameName = "Двенадцатый семестр"
                    };


                    await context.SemestrNames.AddRangeAsync(
                        SemestrName1,
                        SemestrName2,
                        SemestrName3,
                        SemestrName4,
                        SemestrName5,
                        SemestrName6,
                        SemestrName7,
                        SemestrName8,
                        SemestrName9,
                        SemestrName10,
                        SemestrName11,
                        SemestrName12
                    );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
