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
    public class InitDatabaseEduSrok
    {
        /// <summary>
        /// Инициализация таблицы "Срок Обучения"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEduSrok(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Срок Обучения"
                if (!await context.EduSrok.AnyAsync())
                {
                    EduSrok EduSrok1 = new EduSrok
                    {
                        EduSrokId = 1,
                        EduSrokName = "1 год 10 месяцев"
                    };

                    EduSrok EduSrok2 = new EduSrok
                    {
                        EduSrokId = 2,
                        EduSrokName = "2 года"
                    };

                    EduSrok EduSrok3 = new EduSrok
                    {
                        EduSrokId = 3,
                        EduSrokName = "2 года 6 месяцев"
                    };

                    EduSrok EduSrok4 = new EduSrok
                    {
                        EduSrokId = 4,
                        EduSrokName = "2 года 10 месяцев"
                    };

                    EduSrok EduSrok5 = new EduSrok
                    {
                        EduSrokId = 5,
                        EduSrokName = "3 года"
                    };

                    EduSrok EduSrok6 = new EduSrok
                    {
                        EduSrokId = 6,
                        EduSrokName = "3 года 10 месяцев"
                    };

                    EduSrok EduSrok7 = new EduSrok
                    {
                        EduSrokId = 7,
                        EduSrokName = "4 года"
                    };

                    EduSrok EduSrok8 = new EduSrok
                    {
                        EduSrokId = 8,
                        EduSrokName = "5 лет"
                    };  

                    await context.EduSrok.AddRangeAsync(
                        EduSrok1,
                        EduSrok2,
                        EduSrok3,
                        EduSrok4,
                        EduSrok5,
                        EduSrok6,
                        EduSrok7,
                        EduSrok8
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
