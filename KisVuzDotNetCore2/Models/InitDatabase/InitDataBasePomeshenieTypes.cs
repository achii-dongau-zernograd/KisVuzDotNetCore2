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
    public class InitDataBasePomeshenieTypes
    {
        /// <summary>
        /// Инициализация таблицы "Корпус"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreatePomeshenieType(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Тип помещения (справочник)"
                if (!await context.PomeshenieType.AnyAsync())
                {
                    PomeshenieType PomeshenieTypeName1 = new PomeshenieType
                    {
                        PomeshenieTypeId = 1,
                        PomeshenieTypeName = "Учебная аудитория",
                    };

                    PomeshenieType PomeshenieTypeName2 = new PomeshenieType
                    {
                        PomeshenieTypeId = 2,
                        PomeshenieTypeName = "Лекционная аудитория",
                    };

                    PomeshenieType PomeshenieTypeName3 = new PomeshenieType
                    {
                        PomeshenieTypeId = 3,
                        PomeshenieTypeName = "Аудитория для самостоятельной работы студентов",
                    };

                    await context.PomeshenieType.AddRangeAsync( 
                     PomeshenieTypeName1,
                     PomeshenieTypeName2,
                     PomeshenieTypeName3
                     );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}

