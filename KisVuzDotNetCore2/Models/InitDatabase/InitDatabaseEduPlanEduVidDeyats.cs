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
    /// Инициализация таблицы "Таблица для связи EduPlan и EduVidDeyat"
    /// </summary>
    public class InitDatabaseEduPlanEduVidDeyats
    {
        /// <summary>
        /// Инициализация таблицы "Таблица для связи EduPlan и EduVidDeyat"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEduPlanEduVidDeyats(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Таблица для связи EduPlan и EduVidDeyat"
                if (!await context.EduPlanEduVidDeyats.AnyAsync())
                {
                    EduPlanEduVidDeyat EduPlanEduVidDeyat1 = new EduPlanEduVidDeyat
                    {
                        EduPlanEduVidDeyatId = 1,
                        EduPlanId = 1,
                        EduVidDeyatId = 1
                    };

                    

                    await context.EduPlanEduVidDeyats.AddRangeAsync(
                        EduPlanEduVidDeyat1
                        
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
