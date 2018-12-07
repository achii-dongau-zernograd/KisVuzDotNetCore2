using KisVuzDotNetCore2.Models.Nir;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    internal class InitDatabaseYears
    {
        /// <summary>
        /// Инициализация таблицы "Годы"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateYears(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Годы"
                if (!await context.Years.AnyAsync())
                {
                    Year Year1 = new Year
                    {
                        YearId = 2018,
                        YearName = "2018 год"
                    };
                    Year Year2 = new Year
                    {
                        YearId = 2017,
                        YearName = "2017 год"
                    };
                    Year Year3 = new Year
                    {
                        YearId = 2016,
                        YearName = "2016 год"
                    };
                    Year Year4 = new Year
                    {
                        YearId = 2015,
                        YearName = "2015 год"
                    };
                    Year Year5 = new Year
                    {
                        YearId = 2014,
                        YearName = "2014 год"
                    };
                    Year Year6 = new Year
                    {
                        YearId = 2013,
                        YearName = "2013 год"
                    };
                    Year Year7 = new Year
                    {
                        YearId = 2012,
                        YearName = "2012 год"
                    };
                    Year Year8 = new Year
                    {
                        YearId = 2011,
                        YearName = "2011 год"
                    };
                    Year Year9 = new Year
                    {
                        YearId = 2010,
                        YearName = "2010 год"
                    };

                    await context.Years.AddRangeAsync(
                        Year1,
                        Year2,
                        Year3,
                        Year4,
                        Year5,
                        Year6,
                        Year7,
                        Year8,
                        Year9
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}