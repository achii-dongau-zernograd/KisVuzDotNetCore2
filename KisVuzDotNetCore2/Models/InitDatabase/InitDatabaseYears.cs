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
                    Year Year2020 = new Year
                    {
                        YearId = 2020,
                        YearName = "2020 год"
                    };
                    Year Year2019 = new Year
                    {
                        YearId = 2019,
                        YearName = "2019 год"
                    };
                    Year Year2018 = new Year
                    {
                        YearId = 2018,
                        YearName = "2018 год"
                    };
                    Year Year2017 = new Year
                    {
                        YearId = 2017,
                        YearName = "2017 год"
                    };
                    Year Year2016 = new Year
                    {
                        YearId = 2016,
                        YearName = "2016 год"
                    };
                    Year Year2015 = new Year
                    {
                        YearId = 2015,
                        YearName = "2015 год"
                    };
                    Year Year2014 = new Year
                    {
                        YearId = 2014,
                        YearName = "2014 год"
                    };
                    Year Year2013 = new Year
                    {
                        YearId = 2013,
                        YearName = "2013 год"
                    };
                    Year Year2012 = new Year
                    {
                        YearId = 2012,
                        YearName = "2012 год"
                    };
                    Year Year2011 = new Year
                    {
                        YearId = 2011,
                        YearName = "2011 год"
                    };
                    Year Year2010 = new Year
                    {
                        YearId = 2010,
                        YearName = "2010 год"
                    };
                    Year Year2009 = new Year
                    {
                        YearId = 2009,
                        YearName = "2009 год"
                    };
                    Year Year2008 = new Year
                    {
                        YearId = 2008,
                        YearName = "2008 год"
                    };
                    Year Year2007 = new Year
                    {
                        YearId = 2007,
                        YearName = "2007 год"
                    };
                    Year Year2006 = new Year
                    {
                        YearId = 2006,
                        YearName = "2006 год"
                    };
                    Year Year2005 = new Year
                    {
                        YearId = 2005,
                        YearName = "2005 год"
                    };
                    Year Year2004 = new Year
                    {
                        YearId = 2004,
                        YearName = "2004 год"
                    };
                    Year Year2003 = new Year
                    {
                        YearId = 2003,
                        YearName = "2003 год"
                    };
                    Year Year2002 = new Year
                    {
                        YearId = 2002,
                        YearName = "2002 год"
                    };
                    Year Year2001 = new Year
                    {
                        YearId = 2001,
                        YearName = "2001 год"
                    };
                    Year Year2000 = new Year
                    {
                        YearId = 2000,
                        YearName = "2000 год"
                    };

                    await context.Years.AddRangeAsync(
                        Year2000,
                        Year2001,
                        Year2002,
                        Year2003,
                        Year2004,
                        Year2005,
                        Year2006,
                        Year2007,
                        Year2008,
                        Year2009,
                        Year2010,
                        Year2011,
                        Year2012,
                        Year2013,
                        Year2014,
                        Year2015,
                        Year2016,
                        Year2017,
                        Year2018,
                        Year2019,
                        Year2020
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}