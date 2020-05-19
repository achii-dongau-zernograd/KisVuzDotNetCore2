using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.LMS;
using KisVuzDotNetCore2.Models.Users;
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
    /// Инициализация таблицы "Типы отношения к военной службе"
    /// </summary>
    public class InitDatabaseMilitaryServiceStatuses
    {
        /// <summary>
        /// Инициализация таблицы "Типы отношения к военной службе"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateMilitaryServiceStatuses(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы отношения к военной службе"
                if (!await context.MilitaryServiceStatuses.AnyAsync())
                {
                    var Row01 = new MilitaryServiceStatus
                    {
                        MilitaryServiceStatusId = 1,
                        MilitaryServiceStatusName = "Военнослужащий"
                    };

                    var Row02 = new MilitaryServiceStatus
                    {
                        MilitaryServiceStatusId = 2,
                        MilitaryServiceStatusName = "Военнообязанный"
                    };

                    var Row03 = new MilitaryServiceStatus
                    {
                        MilitaryServiceStatusId = 3,
                        MilitaryServiceStatusName = "Невоеннообязанный"
                    };

                    var Row04 = new MilitaryServiceStatus
                    {
                        MilitaryServiceStatusId = 4,
                        MilitaryServiceStatusName = "Призывник"
                    };

                    await context.MilitaryServiceStatuses.AddRangeAsync(
                        Row01,
                        Row02,
                        Row03,
                        Row04
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }        
    }
}
