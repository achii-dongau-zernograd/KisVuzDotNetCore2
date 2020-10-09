using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Users;
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
    /// Инициализация таблиц "Типы внешних ресурсов" и "Внешние ресурсы"
    /// </summary>
    public class InitDatabaseExternalResources
    {
        /// <summary>
        /// Инициализация таблицы "Типы внешних ресурсов"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateExternalResourceTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы внешних ресурсов"
                if (!await context.ExternalResourceTypes.AnyAsync())
                {
                    var row01 = new ExternalResourceType
                    {
                        ExternalResourceTypeId = (int)ExternalResourceTypeEnum.SocialNetworks,
                        ExternalResourceTypeName = "Социальные сети"
                    };

                    var row02 = new ExternalResourceType
                    {
                        ExternalResourceTypeId = (int)ExternalResourceTypeEnum.ScienceNetworks,
                        ExternalResourceTypeName = "Научные сети"
                    };

                    var row03 = new ExternalResourceType
                    {
                        ExternalResourceTypeId = (int)ExternalResourceTypeEnum.Messengers,
                        ExternalResourceTypeName = "Мессенджеры"
                    };

                    var row04 = new ExternalResourceType
                    {
                        ExternalResourceTypeId = (int)ExternalResourceTypeEnum.CitationBases,
                        ExternalResourceTypeName = "Базы цитирования"
                    };


                    await context.ExternalResourceTypes.AddRangeAsync(
                        row01,
                        row02,
                        row03,
                        row04
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

        /// <summary>
        /// Инициализация таблицы "Внешние ресурсы"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateExternalResources(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Внешние ресурсы"
                if (!await context.ExternalResources.AnyAsync())
                {
                    var row01 = new ExternalResource
                    {
                        ExternalResourceId = (int)ExternalResourceEnum.SocialNetworks_VKontakte,
                        ExternalResourceTypeId = (int)ExternalResourceTypeEnum.SocialNetworks,
                        ExternalResourceName = "ВКонтакте"
                    };

                    var row02 = new ExternalResource
                    {
                        ExternalResourceId = (int)ExternalResourceEnum.SocialNetworks_ORCID,
                        ExternalResourceTypeId = (int)ExternalResourceTypeEnum.ScienceNetworks,
                        ExternalResourceName = "ORCID"
                    };

                    var row03 = new ExternalResource
                    {
                        ExternalResourceId = (int)ExternalResourceEnum.SocialNetworks_Mendeley,
                        ExternalResourceTypeId = (int)ExternalResourceTypeEnum.ScienceNetworks,
                        ExternalResourceName = "Mendeley"
                    };

                    var row04 = new ExternalResource
                    {
                        ExternalResourceId = (int)ExternalResourceEnum.Messengers_Skype,
                        ExternalResourceTypeId = (int)ExternalResourceTypeEnum.Messengers,
                        ExternalResourceName = "Skype"
                    };

                    var row05 = new ExternalResource
                    {
                        ExternalResourceId = (int)ExternalResourceEnum.CitationBases_Elibrary,
                        ExternalResourceTypeId = (int)ExternalResourceTypeEnum.CitationBases,
                        ExternalResourceName = "РИНЦ"
                    };

                    var row06 = new ExternalResource
                    {
                        ExternalResourceId = (int)ExternalResourceEnum.CitationBases_Scopus,
                        ExternalResourceTypeId = (int)ExternalResourceTypeEnum.CitationBases,
                        ExternalResourceName = "Scopus"
                    };


                    await context.ExternalResources.AddRangeAsync(
                        row01,
                        row02,
                        row03,
                        row04,
                        row05,
                        row06
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
