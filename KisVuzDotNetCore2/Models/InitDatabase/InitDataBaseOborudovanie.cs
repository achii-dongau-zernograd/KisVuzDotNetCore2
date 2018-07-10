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
    public class InitDataBaseOborudovanie
    {

        /// <summary>
        /// Инициализация таблицы "Оборудование"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateOborudovanie(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Оборудование"
                if (!await context.Oborudovanie.AnyAsync())
                {
                    Oborudovanie OborudovanieName1 = new Oborudovanie
                    {
                        OborudovanieId = 1,
                        OborudovanieName = "Стол",
                    };

                    Oborudovanie OborudovanieName2 = new Oborudovanie
                    {
                        OborudovanieId = 2,
                        OborudovanieName = "Стул",
                    };

                    Oborudovanie OborudovanieName3 = new Oborudovanie
                    {
                        OborudovanieId = 3,
                        OborudovanieName = "Доска",
                    };

                    Oborudovanie OborudovanieName4 = new Oborudovanie
                    {
                        OborudovanieId = 4,
                        OborudovanieName = "Компьютер",
                    };

                    Oborudovanie OborudovanieName5 = new Oborudovanie
                    {
                        OborudovanieId = 5,
                        OborudovanieName = "Проектор",
                    };

                    Oborudovanie OborudovanieName6 = new Oborudovanie
                    {
                        OborudovanieId = 6,
                        OborudovanieName = "Стенд",
                    };

                    await context.Oborudovanie.AddRangeAsync(
                     OborudovanieName1,
                     OborudovanieName2,
                     OborudovanieName3,
                     OborudovanieName4,
                     OborudovanieName5,
                     OborudovanieName6
                     );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}

