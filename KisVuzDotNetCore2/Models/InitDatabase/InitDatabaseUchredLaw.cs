using KisVuzDotNetCore2.Models.Education;
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
    public class InitDatabaseUchredLaw
    {
        /// <summary>
        /// Инициализация таблицы "Сведения об учредителях"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateUchredLaw(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Сведения об учредителях"
                if (!await context.UchredLaw.AnyAsync())
                {
                    UchredLaw UchredLawName1 = new UchredLaw
                    {
                        UchredLawId = 1,
                        NameUchred = "Министерство сельского хозяйства Российской Федерации (Минсельхоз России)",
                         FullnameUchred = "Министр сельского хозяйства РФ - Ткачев Александр Николаевич",
                         AddressUchred = "107139, г. Москва, Орликов переулок, д. 1/1",
                        TelUchred = "+7 (495) 607-80-00",
                        mailUchred = "info@mcx.ru",
                        WebsiteUchred = "http://mcx.ru/"
                    };


                    await context.UchredLaw.AddRangeAsync(UchredLawName1);
                        
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
