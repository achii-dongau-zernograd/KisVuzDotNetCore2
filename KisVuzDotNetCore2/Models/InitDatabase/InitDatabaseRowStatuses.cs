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
    /// Инициализация таблицы "Статусы записей"
    /// </summary>
    public class InitDatabaseRowStatuses
    {
        /// <summary>
        /// Инициализация таблицы "Статусы записей"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateRowStatuses(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Статусы записей"
                if (!await context.RowStatuses.AnyAsync())
                {
                    RowStatus RowStatus1 = new RowStatus
                    {
                        RowStatusId=1,
                        RowStatusName="Ожидает подтверждения"
                    };

                    RowStatus RowStatus2 = new RowStatus
                    {
                        RowStatusId = 2,
                        RowStatusName = "Подтверждено"
                    };
                    
                    await context.RowStatuses.AddRangeAsync(
                        RowStatus1,
                        RowStatus2
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
