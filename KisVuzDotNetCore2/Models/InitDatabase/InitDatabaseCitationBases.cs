using KisVuzDotNetCore2.Models.Nir;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    internal class InitDatabaseCitationBases
    {
        /// <summary>
        /// Инициализация таблицы "Базы цитирования"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateCitationBases(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Базы цитирования"
                if (!await context.CitationBases.AnyAsync())
                {
                    var citationBase1 = new CitationBase
                    {
                        CitationBaseId = 1,
                        CitationBaseName = "РИНЦ"
                    };
                    var citationBase2 = new CitationBase
                    {
                        CitationBaseId = 2,
                        CitationBaseName = "Web of Science"
                    };
                    var citationBase3 = new CitationBase
                    {
                        CitationBaseId = 3,
                        CitationBaseName = "Scopus"
                    };
                    var citationBase4 = new CitationBase
                    {
                        CitationBaseId = 4,
                        CitationBaseName = "AGRIS"
                    };

                    await context.CitationBases.AddRangeAsync(
                        citationBase1,
                        citationBase2,
                        citationBase3,
                        citationBase4
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}