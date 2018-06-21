using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class InitDatabaseElectronCatalog
    {
        /// <summary>
        /// Инициализация таблицы "База данных электронного каталога"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateElectronCatalog(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "База данных электронного каталога"
                if (!await context.ElectronCatalog.AnyAsync())
                {
                    ElectronCatalog Ec1 = new ElectronCatalog
                    {
                       NameEc = "Электронный каталог \"MarcSQL\"",
                       DescriptionEc = "Электронный каталог \"MarcSQL\" содержит информацию о учебниках, учебных изданиях и другой литературе, поступившей в библиотеку института, начиная с 1999 года. Электронный каталог удобен для поиска информации."
                    };

                    await context.ElectronCatalog.AddAsync(Ec1);

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
