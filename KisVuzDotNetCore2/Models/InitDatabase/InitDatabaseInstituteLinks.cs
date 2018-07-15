using System;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KisVuzDotNetCore2.Models
{
    internal class InitDatabaseInstituteLinks
    {
        internal static async Task CreateInstituteLinks(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Дисциплины"
                if (!await context.InstituteLinks.AnyAsync())
                {
                    InstituteLink InstituteLink1 = new InstituteLink
                    {
                        InstituteLinkId = 1,
                        InstituteLinkDescription = "Официальный веб-сайт",
                        LinkTypeId=(int)LinkTypesEnum.OfficialWebSite,
                        InstituteLinkLink="ачгаа.рф",
                        StructInstituteId=1
                    };

                    InstituteLink InstituteLink2 = new InstituteLink
                    {
                        InstituteLinkId = 2,
                        InstituteLinkDescription = "Ссылка на информацию, размещаемую на сайте http://bus.gov.ru",
                        LinkTypeId = (int)LinkTypesEnum.BusGovRu,
                        InstituteLinkLink = "http://bus.gov.ru/pub/agency/485942/register-info",
                        StructInstituteId = 1                        
                    };

                    await context.InstituteLinks.AddRangeAsync(
                        InstituteLink1,
                        InstituteLink2
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}