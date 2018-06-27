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
    /// Инициализация таблицы "Программы подготовки"
    /// </summary>
    public class InitDatabaseEduProgramPodg
    {
        /// <summary>
        /// Инициализация таблицы "Программы подготовки"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEduProgramPodg(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Программы подготовки"
                if (!await context.EduProgramPodg.AnyAsync())
                {
                    EduProgramPodg EduProgramPodg1 = new EduProgramPodg
                    {
                        EduProgramPodgId = 1,
                        EduProgramPodgName = "Академический бакалавриат"
                    };

                    EduProgramPodg EduProgramPodg2 = new EduProgramPodg
                    {
                        EduProgramPodgId = 2,
                        EduProgramPodgName = "Прикладной бакалавриат"
                    };

                    EduProgramPodg EduProgramPodg3 = new EduProgramPodg
                    {
                        EduProgramPodgId = 3,
                        EduProgramPodgName = "Академическая магистратура"
                    };

                    EduProgramPodg EduProgramPodg4 = new EduProgramPodg
                    {
                        EduProgramPodgId = 4,
                        EduProgramPodgName = "Прикладная магистратура"
                    };

                    await context.EduProgramPodg.AddRangeAsync(
                        EduProgramPodg1,
                        EduProgramPodg2,
                        EduProgramPodg3,
                        EduProgramPodg4
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
