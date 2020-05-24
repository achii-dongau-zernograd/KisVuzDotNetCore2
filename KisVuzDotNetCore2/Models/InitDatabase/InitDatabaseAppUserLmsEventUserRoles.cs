using KisVuzDotNetCore2.Models.Abitur;
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
    /// Инициализация справочника ролей пользователей, участвующих в мероприятии СДО
    /// </summary>
    public class InitDatabaseAppUserLmsEventUserRoles
    {
        /// <summary>
        /// Инициализация справочника ролей пользователей, участвующих в мероприятии СДО
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateAppUserLmsEventUserRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы отношения к военной службе"
                if (!await context.AppUserLmsEventUserRoles.AnyAsync())
                {
                    var Row01 = new AppUserLmsEventUserRole
                    {
                        AppUserLmsEventUserRoleId = (int)AppUserLmsEventUserRolesEnum.Participant,
                        AppUserLmsEventUserRoleName = "Участник мероприятия"
                    };

                    var Row02 = new AppUserLmsEventUserRole
                    {
                        AppUserLmsEventUserRoleId = (int)AppUserLmsEventUserRolesEnum.Organizer,
                        AppUserLmsEventUserRoleName = "Организатор мероприятия"
                    };

                    var Row03 = new AppUserLmsEventUserRole
                    {
                        AppUserLmsEventUserRoleId = (int)AppUserLmsEventUserRolesEnum.Consultant,
                        AppUserLmsEventUserRoleName = "Консультант"
                    };

                    var Row04 = new AppUserLmsEventUserRole
                    {
                        AppUserLmsEventUserRoleId = (int)AppUserLmsEventUserRolesEnum.UserWhoHostsEvent,
                        AppUserLmsEventUserRoleName = "Пользователь, который непосредственно проводит событие"
                    };

                    await context.AppUserLmsEventUserRoles.AddRangeAsync(
                        Row01, Row02, Row03, Row04
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }        
    }
}
