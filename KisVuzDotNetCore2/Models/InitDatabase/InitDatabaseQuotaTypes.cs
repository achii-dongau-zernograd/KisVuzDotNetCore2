using KisVuzDotNetCore2.Models.Abitur;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    internal class InitDatabaseQuotaTypes
    {
        /// <summary>
        /// Инициализация таблицы "Типы квот для приёма абитуриентов"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateQuotaTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы квот"
                if (!await context.QuotaTypes.AnyAsync())
                {
                    var row01 = new QuotaType
                    {
                        QuotaTypeId = (int)QuotaTypesEnum.OsoboePravo,
                        QuotaTypeName = "Места в пределах квоты приёма лиц, имеющих особое право"
                    };

                    var row02 = new QuotaType
                    {
                        QuotaTypeId = (int)QuotaTypesEnum.CelevoyPriem,
                        QuotaTypeName = "Места в пределах квоты целевого приёма"
                    };

                    var row03 = new QuotaType
                    {
                        QuotaTypeId = (int)QuotaTypesEnum.KontrolnieCifri,
                        QuotaTypeName = "Основные места в рамках контрольных цифр"
                    };

                    var row04 = new QuotaType
                    {
                        QuotaTypeId = (int)QuotaTypesEnum.DogovorObOkazaniiPlatnihObrazovatUslug,
                        QuotaTypeName = "Места по договорам об оказании платных образовательных услуг"
                    };

                    await context.QuotaTypes.AddRangeAsync(
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
    }
}