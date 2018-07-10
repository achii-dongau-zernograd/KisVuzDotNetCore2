using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class InitDatabasePomeshenie
    {
        /// <summary>
        /// Инициализация таблицы "Помещение"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreatePomeshenie(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Помещение"
                if (!await context.Pomeshenie.AnyAsync())
                {
                    Pomeshenie PomeshenieName1 = new Pomeshenie
                    {
                        PomeshenieId = 1,
                        KorpusId=5,
                        PomeshenieName = "5-201",
                    };

                    Pomeshenie PomeshenieName2 = new Pomeshenie
                    {
                        PomeshenieId = 2,
                        KorpusId = 5,
                        PomeshenieName = "5-211",
                    };

                    Pomeshenie PomeshenieName3 = new Pomeshenie
                    {
                        PomeshenieId = 3,
                        KorpusId = 5,
                        PomeshenieName = "5-221",
                    };

                    Pomeshenie PomeshenieName4 = new Pomeshenie
                    {
                        PomeshenieId = 4,
                        KorpusId = 5,
                        PomeshenieName = "5-115",
                    };

                    Pomeshenie PomeshenieName5 = new Pomeshenie
                    {
                        PomeshenieId = 5,
                        KorpusId = 5,
                        PomeshenieName = "5-203",
                    };

                    Pomeshenie PomeshenieName6 = new Pomeshenie
                    {
                        PomeshenieId = 6,
                        KorpusId = 5,
                        PomeshenieName = "5-306",
                    };

                    await context.Pomeshenie.AddRangeAsync(
                     PomeshenieName1,
                     PomeshenieName2,
                     PomeshenieName3,
                     PomeshenieName4,
                     PomeshenieName5,
                     PomeshenieName6
                     );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
