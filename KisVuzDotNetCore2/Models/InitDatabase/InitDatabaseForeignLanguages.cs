using KisVuzDotNetCore2.Models.Common;
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
    /// Инициализация таблицы "Иностранные языки"
    /// </summary>
    public class InitDatabaseForeignLanguages
    {
        /// <summary>
        /// Инициализация таблицы "Иностранные языки"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateForeignLanguages(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Иностранные языки"
                if (!await context.ForeignLanguages.AnyAsync())
                {
                    var Row01 = new ForeignLanguage
                    {
                        ForeignLanguageId = 1,
                        ForeignLanguageName = "Английский"
                    };

                    var Row02 = new ForeignLanguage
                    {
                        ForeignLanguageId = 2,
                        ForeignLanguageName = "Немецкий"
                    };

                    var Row03 = new ForeignLanguage
                    {
                        ForeignLanguageId = 3,
                        ForeignLanguageName = "Французский"
                    };

                    await context.ForeignLanguages.AddRangeAsync(
                        Row01,
                        Row02,
                        Row03
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
