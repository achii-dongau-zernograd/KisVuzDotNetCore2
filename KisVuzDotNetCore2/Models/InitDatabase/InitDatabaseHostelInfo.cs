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
    public static class InitDatabaseHostelInfo
    {
        /// <summary>
        /// Инициализация таблицы "Наличие общежития, интерната, количество жилых помещений в общежитии, интернате для иногородних обучающихся"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateHostelInfo(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Наличие общежития, интерната, количество жилых помещений в общежитии, интернате для иногородних обучающихся"
                if (!await context.HostelInfo.AnyAsync())
                {
                    HostelInfo SvedenHostelInfo1 = new HostelInfo
                    {
                        NameIndicator = "Количество общежитий, интернатов",
                        Itemprop = "hostelNum",
                        Value = "5",
                        Link = "http://ачгаа.рф/sveden/grants"
                    };

                    HostelInfo SvedenHostelInfo2 = new HostelInfo
                    {
                        NameIndicator = "Количество общежитий, интернатов для использования инвалидами и лицами с ограниченными возможностями здоровья",
                        Itemprop = "hostelNumOvz",
                        Value = "",
                        Link = ""
                    };

                    HostelInfo SvedenHostelInfo3 = new HostelInfo
                    {
                        NameIndicator = "Общая площадь общежитий, интернатов, м.кв.",
                        Itemprop = "",
                        Value = "17656,1",
                        Link = ""
                    };

                    HostelInfo SvedenHostelInfo4 = new HostelInfo
                    {
                        NameIndicator = "Общая площадь общежитий, интернатов для использования инвалидами и лицами с ограниченными возможностями здоровья, м.кв.",
                        Itemprop = "",
                        Value = "",
                        Link = ""
                    };

                    HostelInfo SvedenHostelInfo5 = new HostelInfo
                    {
                        NameIndicator = "Жилая площадь общежитий, интернатов, м.кв.",
                        Itemprop = "",
                        Value = "10537,1",
                        Link = ""
                    };

                    HostelInfo SvedenHostelInfo6 = new HostelInfo
                    {
                        NameIndicator = "Жилая площадь общежитий, интернатов для использования инвалидами и лицами с ограниченными возможностями здоровья, м.кв.",
                        Itemprop = "",
                        Value = "",
                        Link = ""
                    };

                    HostelInfo SvedenHostelInfo7 = new HostelInfo
                    {
                        NameIndicator = "Количество мест в общежитиях, в интернатах",
                        Itemprop = "",
                        Value = "1295",
                        Link = ""
                    };

                    HostelInfo SvedenHostelInfo8 = new HostelInfo
                    {
                        NameIndicator = "Количество мест в общежитиях, в интернатах для использования инвалидами и лицами с ограниченными возможностями здоровья",
                        Itemprop = "",
                        Value = "",
                        Link = ""
                    };

                    HostelInfo SvedenHostelInfo9 = new HostelInfo
                    {
                        NameIndicator = "Обеспеченность общежитий, интернатов 100% мягким и жестким инвентарем по установленным стандартным нормам",
                        Itemprop = "",
                        Value = "100 %",
                        Link = ""
                    };

                    HostelInfo SvedenHostelInfo10 = new HostelInfo
                    {
                        NameIndicator = "Обеспеченность общежитий, интернатов 100% мягким и жестким инвентарем по установленным стандартным нормам для использования инвалидами и лицами с ограниченными возможностями здоровья",
                        Itemprop = "",
                        Value = "",
                        Link = ""
                    };

                    HostelInfo SvedenHostelInfo11 = new HostelInfo
                    {
                        NameIndicator = "Наличие питания (включая буфеты, столовые) (да/нет) в общежитиях, в интернатах",
                        Itemprop = "",
                        Value = "Да",
                        Link = ""
                    };

                    HostelInfo SvedenHostelInfo12 = new HostelInfo
                    {
                        NameIndicator = "Наличие питания (включая буфеты, столовые) (да/нет) в общежитиях, в интернатах для использования инвалидами и лицами с ограниченными возможностями здоровья",
                        Itemprop = "",
                        Value = "Да",
                        Link = ""
                    };
                    await context.HostelInfo.AddRangeAsync(
                        SvedenHostelInfo1,
                        SvedenHostelInfo2,
                        SvedenHostelInfo3,
                        SvedenHostelInfo4,
                        SvedenHostelInfo5,
                        SvedenHostelInfo6,
                        SvedenHostelInfo7,
                        SvedenHostelInfo8,
                        SvedenHostelInfo9,
                        SvedenHostelInfo10,
                        SvedenHostelInfo11,
                        SvedenHostelInfo12
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

    }
}
