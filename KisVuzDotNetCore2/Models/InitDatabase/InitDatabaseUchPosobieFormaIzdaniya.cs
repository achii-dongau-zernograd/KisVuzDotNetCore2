using KisVuzDotNetCore2.Models.UchPosobiya;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class InitDatabaseUchPosobieFormaIzdaniya
    {
        /// <summary>
        /// Инициализация таблицы "Форма издания учебного пособия"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateUchPosobieFormaIzdaniya(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Форма издания учебного пособия"
                if (!await context.UchPosobieFormaIzdaniya.AnyAsync())
                {
                    UchPosobieFormaIzdaniya UchPosobieFormaIzdaniyaName1 = new UchPosobieFormaIzdaniya
                    {
                        UchPosobieFormaIzdaniyaId = 1,
                        UchPosobieFormaIzdaniyaName = "Печатная",
                    };

                    UchPosobieFormaIzdaniya UchPosobieFormaIzdaniyaName2 = new UchPosobieFormaIzdaniya
                    {
                        UchPosobieFormaIzdaniyaId = 2,
                        UchPosobieFormaIzdaniyaName = "Электронная",
                    };

                  
                    await context.UchPosobieFormaIzdaniya.AddRangeAsync(
                        UchPosobieFormaIzdaniyaName1,
                        UchPosobieFormaIzdaniyaName2
                    );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
