using KisVuzDotNetCore2.Models.Common;
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
    /// Инициализация таблицы "Статусы пользователей"
    /// </summary>
    public class InitDatabaseAppUserStatuses
    {
        /// <summary>
        /// Инициализация таблицы "Статусы пользователей"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateAppUserStatuses(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Статусы пользователей"
                if (!await context.AppUserStatuses.AnyAsync())
                {
                    var AppUserStatus1 = new AppUserStatus
                    {
                        AppUserStatusId = (int)AppUserStatusEnum.NewUser,
                        AppUserStatusName = "Новый пользователь"
                    };

                    var AppUserStatus2 = new AppUserStatus
                    {
                        AppUserStatusId = (int)AppUserStatusEnum.ConfirmedUser,
                        AppUserStatusName = "Подтверждённый пользователь"
                    };

                    var AppUserStatus3 = new AppUserStatus
                    {
                        AppUserStatusId = (int)AppUserStatusEnum.ToDelete,
                        AppUserStatusName = "Пользователь, помеченный к удалению"
                    };

                    await context.AppUserStatuses.AddRangeAsync(
                        AppUserStatus1,
                        AppUserStatus2,
                        AppUserStatus3
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
