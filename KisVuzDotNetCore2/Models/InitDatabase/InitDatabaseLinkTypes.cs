using System;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Инициализация таблицы типов веб-ресурсов
    /// </summary>
    public class InitDatabaseLinkTypes
    {
        public static async Task CreateLinkTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Дисциплины"
                if (!await context.LinkTypes.AnyAsync())
                {
                    LinkType LinkType1 = new LinkType
                    {
                        LinkTypeId = (int)LinkTypesEnum.OfficialWebSite,
                        LinkTypeName = "Официальный веб-сайт"
                    };

                    LinkType LinkType2 = new LinkType
                    {
                        LinkTypeId = (int)LinkTypesEnum.BusGovRu,
                        LinkTypeName = "Ссылка на информацию, размещаемую на сайте http://bus.gov.ru"
                    };
                                        
                    await context.LinkTypes.AddRangeAsync(
                        LinkType1,
                        LinkType2                       
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}