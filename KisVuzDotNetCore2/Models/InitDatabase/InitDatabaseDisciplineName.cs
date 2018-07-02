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
    /// Инициализация таблицы "Дисциплины"
    /// </summary>
    public class InitDatabaseDisciplineName
    {
        /// <summary>
        /// Инициализация таблицы "Дисциплины"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateDisciplineName(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Дисциплины"
                if (!await context.DisciplineNames.AnyAsync())
                {
                    DisciplineName DisciplineName1 = new DisciplineName
                    {
                        DisciplineNameId = 1,
                        DisciplineNameName = "Иностранный язык"
                    };

                    DisciplineName DisciplineName2 = new DisciplineName
                    {
                        DisciplineNameId = 2,
                        DisciplineNameName = "Философия"
                    };

                    DisciplineName DisciplineName3 = new DisciplineName
                    {
                        DisciplineNameId = 3,
                        DisciplineNameName = "История"
                    };

                    DisciplineName DisciplineName4 = new DisciplineName
                    {
                        DisciplineNameId = 4,
                        DisciplineNameName = "Математика"
                    };

                    DisciplineName DisciplineName5 = new DisciplineName
                    {
                        DisciplineNameId = 5,
                        DisciplineNameName = "Информатика"
                    };

                    DisciplineName DisciplineName6 = new DisciplineName
                    {
                        DisciplineNameId = 6,
                        DisciplineNameName = "Физика"
                    };

                    DisciplineName DisciplineName7 = new DisciplineName
                    {
                        DisciplineNameId = 7,
                        DisciplineNameName = "Химия"
                    };

                    DisciplineName DisciplineName8 = new DisciplineName
                    {
                        DisciplineNameId = 8,
                        DisciplineNameName = "Начертательная геометрия. Инженерная графика"
                    };

                    DisciplineName DisciplineName9 = new DisciplineName
                    {
                        DisciplineNameId = 9,
                        DisciplineNameName = "Физическая культура"
                    };

                    await context.DisciplineNames.AddRangeAsync(
                        DisciplineName1,
                        DisciplineName2,
                        DisciplineName3,
                        DisciplineName4,
                        DisciplineName5,
                        DisciplineName6,
                        DisciplineName7,
                        DisciplineName8,
                        DisciplineName9
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
