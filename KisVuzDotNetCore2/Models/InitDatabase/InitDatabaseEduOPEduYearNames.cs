using KisVuzDotNetCore2.Models.Sveden;
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
    /// Инициализация вспомогательной таблицы выбора годов обучения для табл. 11
    /// </summary>
    public class InitDatabaseEduOPEduYearNames
    {
        /// <summary>
        /// Инициализация вспомогательной таблицы выбора годов обучения для табл. 11
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEduOPEduYearNames(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Статусы записей"
                if (!await context.EduOPEduYearNames.AnyAsync())
                {
                    EduOPEduYearName EduOPEduYearName1 = new EduOPEduYearName
                    {
                        EduOPEduYearNameId = 1,
                        EduOPEduYearNameName = "за 1 год обучения"
                    };

                    EduOPEduYearName EduOPEduYearName2 = new EduOPEduYearName
                    {
                        EduOPEduYearNameId = 2,
                        EduOPEduYearNameName = "за 2 год обучения"
                    };

                    EduOPEduYearName EduOPEduYearName3 = new EduOPEduYearName
                    {
                        EduOPEduYearNameId = 3,
                        EduOPEduYearNameName = "за 3 год обучения"
                    };

                    EduOPEduYearName EduOPEduYearName4 = new EduOPEduYearName
                    {
                        EduOPEduYearNameId = 4,
                        EduOPEduYearNameName = "за 4 год обучения"
                    };

                    EduOPEduYearName EduOPEduYearName5 = new EduOPEduYearName
                    {
                        EduOPEduYearNameId = 5,
                        EduOPEduYearNameName = "за 5 год обучения"
                    };


                    await context.EduOPEduYearNames.AddRangeAsync(
                        EduOPEduYearName1,
                        EduOPEduYearName2,
                        EduOPEduYearName3,
                        EduOPEduYearName4,
                        EduOPEduYearName5
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
