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
                        PomeshenieId = 3,
                        OborudovanieName = "Стол",
                        OborudovanieCount = 8
                    };

                    Oborudovanie OborudovanieName2 = new Oborudovanie
                    {
                        OborudovanieId = 2,
                        PomeshenieId=3,
                        OborudovanieName = "Стул",
                        OborudovanieCount=16
                    };

                    Oborudovanie OborudovanieName3 = new Oborudovanie
                    {
                        OborudovanieId = 3,
                        PomeshenieId = 3,
                        OborudovanieName = "Доска",
                        OborudovanieCount = 1
                    };

                    Oborudovanie OborudovanieName4 = new Oborudovanie
                    {
                        OborudovanieId = 4,
                        PomeshenieId = 3,
                        OborudovanieName = "Компьютер",
                        OborudovanieCount = 6
                    };

                    Oborudovanie OborudovanieName5 = new Oborudovanie
                    {
                        OborudovanieId = 5,
                        PomeshenieId = 3,
                        OborudovanieName = "Проектор",
                        OborudovanieCount = 1
                    };

                    Oborudovanie OborudovanieName6 = new Oborudovanie
                    {
                        OborudovanieId = 6,
                        PomeshenieId = 5,
                        OborudovanieName = "Стенд",
                        OborudovanieCount =4
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

