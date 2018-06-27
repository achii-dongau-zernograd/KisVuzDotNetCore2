using KisVuzDotNetCore2.Models.Education;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class InitDatabaseBlokDisciplChastName
    {
            /// <summary>
            /// Инициализация таблицы "Наименование части блока дисциплин Учебного плана"
            /// </summary>
            /// <param name="serviceProvider"></param>
            /// <param name="configuration"></param>
            /// <returns></returns>
            public static async Task CreateBlokDisciplChastName(IServiceProvider serviceProvider, IConfiguration configuration)
            {
                using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                    #region Инициализация таблицы "Наименование части блока дисциплин Учебного плана"
                    if (!await context.BlokDisciplChastName.AnyAsync())
                    {
                        BlokDisciplChastName BlokDisciplChastName1 = new BlokDisciplChastName
                        {
                            BlokDisciplChastNameId = 1,
                            BlokDisciplChastNameName = "Базовая"
                        };

                        BlokDisciplChastName BlokDisciplChastName2 = new BlokDisciplChastName
                        {
                            BlokDisciplChastNameId = 2,
                            BlokDisciplChastNameName = "Вариативная"
                        };

                        await context.BlokDisciplChastName.AddRangeAsync(
                            BlokDisciplChastName1,
                            BlokDisciplChastName2
                        );
                        await context.SaveChangesAsync();
                    }
                    #endregion
                }
            }
    }
}
