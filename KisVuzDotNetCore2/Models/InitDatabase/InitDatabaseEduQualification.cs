
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
    /// Инициализация таблицы "Квалификация"
    /// </summary>
    public class InitDatabaseEduQualification
    {
        /// <summary>
        /// Инициализация таблицы "Квалификация"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEduQualification(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Квалификация"
                if (!await context.EduQualification.AnyAsync())
                {
                    EduQualification EduQualification1 = new EduQualification
                    {
                        EduQualificationId = 1,
                        EduQualificationName = "Специалист"
                    };

                    EduQualification EduQualification2 = new EduQualification
                    {
                        EduQualificationId = 2,
                        EduQualificationName = "Бакалавр"
                    };

                    EduQualification EduQualification3 = new EduQualification
                    {
                        EduQualificationId = 3,
                        EduQualificationName = "Магистр"
                    };

                    EduQualification EduQualification4 = new EduQualification
                    {
                        EduQualificationId = 4,
                        EduQualificationName = "Исследователь. Преподаватель-исследователь"
                    };

                    EduQualification EduQualification5 = new EduQualification
                    {
                        EduQualificationId = 5,
                        EduQualificationName = "Техник"
                    };

                    EduQualification EduQualification6 = new EduQualification
                    {
                        EduQualificationId = 6,
                        EduQualificationName = "Специалист по земельно-имущественным отношениям"
                    };

                    EduQualification EduQualification7 = new EduQualification
                    {
                        EduQualificationId = 7,
                        EduQualificationName = "Техник-электрик"
                    };

                    EduQualification EduQualification8 = new EduQualification
                    {
                        EduQualificationId = 8,
                        EduQualificationName = "Бухгалтер"
                    };

                    EduQualification EduQualification9 = new EduQualification
                    {
                        EduQualificationId = 9,
                        EduQualificationName = "Менеджер по продажам"
                    };

                    await context.EduQualification.AddRangeAsync(
                        EduQualification1,
                        EduQualification2,
                        EduQualification3,
                        EduQualification4,
                        EduQualification5,
                        EduQualification6,
                        EduQualification7,
                        EduQualification8,
                        EduQualification9
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
