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
    /// Инициализация таблиц, связанных с численностью обучающихся
    /// </summary>
    public static class InitDatabaseEduChislen
    {
        /// <summary>
        /// Инициализация данных, связанных с численность обучающихся
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEduChislen(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Численность обучающихся"
                if (!await context.EduChislens.AnyAsync())
                {
                    foreach (var profile in context.EduProfiles)
                    {
                        foreach (var form in context.EduForms)
                        {
                            EduChislen eduChislen = new EduChislen();
                            eduChislen.EduProfileId = profile.EduProfileId;
                            eduChislen.EduFormId = form.EduFormId;

                            await context.EduChislens.AddAsync(eduChislen);
                        }
                    }

                    await context.SaveChangesAsync();
                }
                #endregion

            }
        }

    }
}
