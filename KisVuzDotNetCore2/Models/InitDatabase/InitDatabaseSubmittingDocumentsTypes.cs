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
    /// Инициализация таблицы "Способы подачи документов"
    /// </summary>
    public class InitDatabaseSubmittingDocumentsTypes
    {
        /// <summary>
        /// Инициализация таблицы "Способы подачи документов"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateSubmittingDocumentsTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Способы подачи документов"
                if (!await context.SubmittingDocumentsTypes.AnyAsync())
                {
                    var row01 = new SubmittingDocumentsType
                    {
                        SubmittingDocumentsTypeId = 1,
                        SubmittingDocumentsTypeName = "Лично"
                    };

                    var row02 = new SubmittingDocumentsType
                    {
                        SubmittingDocumentsTypeId = 2,
                        SubmittingDocumentsTypeName = "Дистанционно"
                    };

                    

                    await context.SubmittingDocumentsTypes.AddRangeAsync(
                        row01,
                        row02
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
