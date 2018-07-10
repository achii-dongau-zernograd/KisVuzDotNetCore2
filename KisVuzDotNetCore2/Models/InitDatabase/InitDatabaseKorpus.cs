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
    public class InitDatabaseKorpus
    {
        /// <summary>
        /// Инициализация таблицы "Корпус"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateKorpus(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Корпус"
                if (!await context.Korpus.AnyAsync())
                {
                    Korpus KorpusName1 = new Korpus
                    {
                        KorpusId = 1,
                        KorpusName = "Учебный корпус 1",
                        AddressId = 1
                    };

                    Korpus KorpusName2 = new Korpus
                    {
                        KorpusId = 2,
                        KorpusName = "Учебный корпус 2",
                        AddressId = 2
                    };

                    Korpus KorpusName3 = new Korpus
                    {
                        KorpusId = 3,
                        KorpusName = "Учебный корпус 3",
                        AddressId = 3
                    };

                    Korpus KorpusName4 = new Korpus
                    {
                        KorpusId = 4,
                        KorpusName = "Административный корпус",
                        AddressId = 4
                    };

                    Korpus KorpusName5 = new Korpus
                    {
                        KorpusId = 5,
                        KorpusName = "Учебный корпус 5",
                        AddressId = 5
                    };

                    Korpus KorpusName6 = new Korpus
                    {
                        KorpusId = 6,
                        KorpusName = "Учебный корпус 6",
                        AddressId = 6
                    };

                    Korpus KorpusName7 = new Korpus
                    {
                        KorpusId = 7,
                        KorpusName = "Боксы",
                        AddressId = 7
                    };

                    await context.Korpus.AddRangeAsync(
                        KorpusName1,
                        KorpusName2,
                        KorpusName3,
                        KorpusName4,
                        KorpusName5,
                        KorpusName6,
                        KorpusName7
                    );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}

