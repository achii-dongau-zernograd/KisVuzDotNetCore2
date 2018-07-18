using KisVuzDotNetCore2.Models.UchPosobiya;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class InitDatabaseUchPosobieVid
    {
        /// <summary>
        /// Инициализация таблицы "Вид учебного пособия"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateUchPosobieVid(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Вид учебного пособия"
                if (!await context.UchPosobieVid.AnyAsync())
                {
                    UchPosobieVid UchPosobieVidName1 = new UchPosobieVid
                    {
                        UchPosobieVidId = 1,
                        UchPosobieVidName = "Учебное пособие",
                    };

                    UchPosobieVid UchPosobieVidName2 = new UchPosobieVid
                    {
                        UchPosobieVidId = 2,
                        UchPosobieVidName = "Курс лекций",
                    };

                    UchPosobieVid UchPosobieVidName3 = new UchPosobieVid
                    {
                        UchPosobieVidId =3,
                        UchPosobieVidName = "Лабораторный практикум",
                    };

                    UchPosobieVid UchPosobieVidName4 = new UchPosobieVid
                    {
                        UchPosobieVidId = 4,
                        UchPosobieVidName = "Методические указания",
                    };

                    await context.UchPosobieVid.AddRangeAsync(
                        UchPosobieVidName1,
                        UchPosobieVidName2,
                        UchPosobieVidName3,
                        UchPosobieVidName4
                    );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
