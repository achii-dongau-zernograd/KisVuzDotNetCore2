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
    /// Инициализация таблицы "Учебные планы"
    /// </summary>
    public class InitDatabaseEduPlans
    {
        /// <summary>
        /// Инициализация таблицы "Учебные планы"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEduPlans(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Учебные планы"
                if (!await context.EduPlans.AnyAsync())
                {
                    EduPlan EduPlan1 = new EduPlan
                    {
                        ProtokolNumber = "4",
                        ProtokolDate = new DateTime(2017, 09, 21),
                        UtverjdDate = new DateTime(2017, 09, 30),
                        EduFormId = 1,
                        EduProfileId = 1,
                        EduProgramPodgId = 1,
                        EduSrokId = 1,
                        StructKafId = 1                     

                    };

                    await context.EduPlans.AddRangeAsync(
                        EduPlan1

                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
