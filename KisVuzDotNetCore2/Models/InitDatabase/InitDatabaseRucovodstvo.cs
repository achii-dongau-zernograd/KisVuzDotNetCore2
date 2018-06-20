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
    /// <summary>
    /// Инициализация таблицы "Руководство"
    /// </summary>
    public static class InitDatabaseRucovodstvo
    {
        /// <summary>
        /// Инициализация таблицы "Руководство"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateRucovodstvo(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Руководство"
                if (!await context.SvedenRucovodstvo.AnyAsync())
                {
                    Rucovodstvo SvedenRucovodstvo1 = new Rucovodstvo
                    {
                        Fio = "Серегин Александр Анатольевич",
                        Post = "Директор",
                        Telephone = "8(86359) 41-7-43",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo2 = new Rucovodstvo
                    {
                        Fio = "Глечикова Наталья Александровна",
                        Post = "И.о. зам. директора по учебной работе",
                        Telephone = "8(86359) 42-1-76",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo3 = new Rucovodstvo
                    {
                        Fio = "Юдаев Игорь Викторович",
                        Post = "Зам. директора по научной работе",
                        Telephone = "8(86359) 41-1-61",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo4 = new Rucovodstvo
                    {
                        Fio = "Джанибеков Казбек Алиевич",
                        Post = "Зам. директора по административно-хозяйственной работе",
                        Telephone = "8(86359) 42-7-81",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo5 = new Rucovodstvo
                    {
                        Fio = "Асатурян Сергей Вартанович",
                        Post = "И.о. зам. директора по воспитательной работе",
                        Telephone = "8(86359) 43-3-49",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo6 = new Rucovodstvo
                    {
                        Fio = "Кабанов Александр Николаевич",
                        Post = "И.о. зам. директора по социальной работе",
                        Telephone = "8(86359) 34-6-12",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo7 = new Rucovodstvo
                    {
                        Fio = "Бондаренко Анатолий Михайлович",
                        Post = "И.о. зам. директора по связям с общественностью",
                        Telephone = "8(86359) 41-3-65",
                        Email = "achgaa@achgaa.ru"
                    };

                    Rucovodstvo SvedenRucovodstvo8 = new Rucovodstvo
                    {
                        Fio = "Меркулов Александр Филиппович",
                        Post = "Директор агротехнологического центра",
                        Telephone = "8(86359) 34-7-32",
                        Email = "achgaa@achgaa.ru"
                    };
                    await context.SvedenRucovodstvo.AddRangeAsync(
                        SvedenRucovodstvo1,
                        SvedenRucovodstvo2,
                        SvedenRucovodstvo3,
                        SvedenRucovodstvo4,
                        SvedenRucovodstvo5,
                        SvedenRucovodstvo6,
                        SvedenRucovodstvo7,
                        SvedenRucovodstvo8
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
