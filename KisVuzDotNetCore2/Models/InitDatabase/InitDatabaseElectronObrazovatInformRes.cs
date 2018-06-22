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
    public class InitDatabaseElectronObrazovatInformRes
    {
        /// <summary>
        /// Инициализация таблицы "Электронные образовательные и информационные ресурсы"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>

        public static async Task CreateElectronObrazovatInformRes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Электронные образовательные и информационные ресурсы"
                if (!await context.ElectronObrazovatInformRes.AnyAsync())
                {
                    ElectronObrazovatInformRes Eoir1 = new ElectronObrazovatInformRes
                    {
                        NameRes = "Электронный тренажер по алгоритмизации",
                        LinkRes = "",
                        Res = null,
                        IsSobstv = true,
                        DescriptionRes = "Электронный тренажер предназначен для обучения студентов 1 курса по дисциплине \"Информатика\", раздел \"Алгоритмизация и программирование\"."
                    };

                    ElectronObrazovatInformRes Eoir2 = new ElectronObrazovatInformRes
                    {
                        NameRes = "Введение в Linux",
                        LinkRes = "https://stepik.org/course/73/",
                        Res = null,
                        IsSobstv = false,
                        DescriptionRes = "Электронный учебный курс \"Введение в Linux\" знакомит с операционной системой Linux и ее базовыми возможностями."
                    };

                    ElectronObrazovatInformRes Eoir3 = new ElectronObrazovatInformRes
                    {
                        NameRes = "Программирование на Python",
                        LinkRes = "https://stepik.org/course/67/",
                        Res = null,
                        IsSobstv = false,
                        DescriptionRes = "Электронный учебный курс \"Программирование на Python\" знакомит с базовыми понятиями программирования."
                    };

                    await context.ElectronObrazovatInformRes.AddRangeAsync(Eoir1, Eoir2, Eoir3);

                    await context.SaveChangesAsync();
                }
                #endregion
            }

        }
    }
}
