using KisVuzDotNetCore2.Models.Abitur;
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
    /// Инициализация таблицы "Статусы абитуриентов"
    /// </summary>
    public class InitDatabaseAbiturientStatuses
    {
        /// <summary>
        /// Инициализация таблицы "Статусы абитуриентов"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateAbiturientStatuses(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Статусы абитуриентов"
                if (!await context.AbiturientStatuses.AnyAsync())
                {
                    var row01 = new AbiturientStatus
                    {
                        AbiturientStatusId = (int)AbiturientStatusEnum.NewAbiturient,
                        AbiturientStatusName = "Новый абитуриент"
                    };

                    var row02 = new AbiturientStatus
                    {
                        AbiturientStatusId = (int)AbiturientStatusEnum.ConfirmedAbiturient,
                        AbiturientStatusName = "Подтверждённый абитуриент"
                    };

                    var row03 = new AbiturientStatus
                    {
                        AbiturientStatusId = (int)AbiturientStatusEnum.Zachislenie,
                        AbiturientStatusName = "К зачислению"
                    };

                    var row04 = new AbiturientStatus
                    {
                        AbiturientStatusId = (int)AbiturientStatusEnum.VObrabotke,
                        AbiturientStatusName = "В обработке"
                    };

                    var row05 = new AbiturientStatus
                    {
                        AbiturientStatusId = (int)AbiturientStatusEnum.KEkzamenam,
                        AbiturientStatusName = "К экзаменам"
                    };

                    await context.AbiturientStatuses.AddRangeAsync(
                        row01,
                        row02,
                        row03,
                        row04,
                        row05
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
