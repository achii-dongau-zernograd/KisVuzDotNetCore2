using KisVuzDotNetCore2.Models.Education;
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
    /// Инициализация таблицы "Виды деятельности по учебному плану"
    /// </summary>
    public class InitDatabaseEduVidDeyats
    {
        /// <summary>
        /// Инициализация таблицы "Виды деятельности по учебному плану"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEduVidDeyats(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Виды деятельности по учебному плану"
                if (!await context.EduVidDeyat.AnyAsync())
                {
                    EduVidDeyat EduVidDeyat1 = new EduVidDeyat
                    {
                        EduVidDeyatId = 1,
                        EduVidDeyatName = "Монтажно-наладочная"
                    };

                    EduVidDeyat EduVidDeyat2 = new EduVidDeyat
                    {
                        EduVidDeyatId = 2,
                        EduVidDeyatName = "Научно-исследовательская"
                    };

                    EduVidDeyat EduVidDeyat3 = new EduVidDeyat
                    {
                        EduVidDeyatId = 3,
                        EduVidDeyatName = "Организационно-управленческая"
                    };

                    EduVidDeyat EduVidDeyat4 = new EduVidDeyat
                    {
                        EduVidDeyatId = 4,
                        EduVidDeyatName = "Проектно-конструкторская"
                    };

                    EduVidDeyat EduVidDeyat5 = new EduVidDeyat
                    {
                        EduVidDeyatId = 5,
                        EduVidDeyatName = "Производственно-технологическая"
                    };

                    EduVidDeyat EduVidDeyat6 = new EduVidDeyat
                    {
                        EduVidDeyatId = 6,
                        EduVidDeyatName = "Расчетно-проектная и проектно-конструкторская"
                    };

                    EduVidDeyat EduVidDeyat7 = new EduVidDeyat
                    {
                        EduVidDeyatId = 7,
                        EduVidDeyatName = "Сервисно-эксплуатационная"
                    };

                    EduVidDeyat EduVidDeyat8 = new EduVidDeyat
                    {
                        EduVidDeyatId = 8,
                        EduVidDeyatName = "Экспертная, надзорная и инспекционно-аудиторская"
                    };

                    await context.EduVidDeyat.AddRangeAsync(
                        EduVidDeyat1,
                        EduVidDeyat2,
                        EduVidDeyat3,
                        EduVidDeyat4,
                        EduVidDeyat5,
                        EduVidDeyat6,
                        EduVidDeyat7,
                        EduVidDeyat8
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
