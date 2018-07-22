using KisVuzDotNetCore2.Models.Common;
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
    /// Инициализация таблицы "Настройки приложения"
    /// </summary>
    public class InitDatabaseAppSettings
    {
        /// <summary>
        /// Инициализация таблицы "Настройки приложения"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateAppSettings(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Настройки приложения"
                if (!await context.AppSettings.AnyAsync())
                {                    
                    AppSetting CurrentEduYear = new AppSetting
                    {
                        AppSettingId = (int)AppSettingTypesEnum.CurrentEduYear,
                        AppSettingName = "Текущий учебный год",
                        AppSettingValue = 2
                    };
                                        

                    await context.AppSettings.AddRangeAsync(
                        CurrentEduYear
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
