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
    /// <summary>
    /// Инициализация таблицы "Электронные библиотечные системы"
    /// </summary>
    public class InitDatabaseElectronBiblSystem
    {
        /// <summary>
        /// Инициализация таблицы "Электронные библиотечные системы"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateElectronBiblSystem(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Электронные библиотечные системы"
                if (!await context.ElectronBiblSystem.AnyAsync())
                {
                    ElectronBiblSystem Ebs1 = new ElectronBiblSystem
                    {
                        NameEbs = "Издательство \"Лань\"",
                        LinkEbs = "https://e.lanbook.com ",
                        NumberDogovor = "1723",
                        DateStart = new DateTime(2016, 12, 14)
                    };

                    ElectronBiblSystem Ebs2 = new ElectronBiblSystem
                    {
                        NameEbs = "ООО \"НексМедиа\"",
                        LinkEbs = "http://biblioclub.ru/index.php?page=main_ub_red ",
                        NumberDogovor = "008-01/2017",
                        DateStart = new DateTime(2017, 01, 19),
                        DateEnd = new DateTime(2018, 01, 19)
                    };

                    await context.ElectronBiblSystem.AddRangeAsync(Ebs1, Ebs2); 
                        
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
