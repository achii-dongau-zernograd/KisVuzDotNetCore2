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
    /// Инициализация таблицы "Пол"
    /// </summary>
    public class InitDatabaseGenders
    {
        /// <summary>
        /// Инициализация таблицы "Пол"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateGenders(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Пол"
                if (!await context.Genders.AnyAsync())
                {
                    var Row01 = new Gender
                    {
                        GenderId = 1,
                        GenderName = "Мужской"
                    };

                    var Row02 = new Gender
                    {
                        GenderId = 2,
                        GenderName = "Женский"
                    };                                        


                    await context.Genders.AddRangeAsync(
                        Row01,
                        Row02
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }        
    }
}
