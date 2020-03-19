using KisVuzDotNetCore2.Models.Nir;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    internal class InitDatabaseNirSpecials
    {
        /// <summary>
        /// Инициализация таблицы "Специальности научных работников"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateNirSpecials(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Специальности научных работников"
                if (!await context.NirSpecials.AnyAsync())
                {
                    var item01 = new NirSpecial
                    {
                        NirSpecialCode = "03.01.05",
                        NirSpecialName = "Физиология и биохимия растений"
                    };

                    var item02 = new NirSpecial
                    {
                        NirSpecialCode = "03.01.02",
                        NirSpecialName = "Биофизика"
                    };

                    var item03 = new NirSpecial
                    {
                        NirSpecialCode = "03.02.08",
                        NirSpecialName = "Экология (по отраслям)"
                    };

                    var item04 = new NirSpecial
                    {
                        NirSpecialCode = "03.02.13",
                        NirSpecialName = "Почвоведение"
                    };

                    var item05 = new NirSpecial
                    {
                        NirSpecialCode = "03.02.14",
                        NirSpecialName = "Биологические ресурсы"
                    };

                    var item06 = new NirSpecial
                    {
                        NirSpecialCode = "05.02.08",
                        NirSpecialName = "Технология машиностроения"
                    };

                    var item07 = new NirSpecial
                    {
                        NirSpecialCode = "05.02.22",
                        NirSpecialName = "Организация производства (по отраслям)"
                    };

                    var item08 = new NirSpecial
                    {
                        NirSpecialCode = "05.09.03",
                        NirSpecialName = "Электротехнические комплексы и системы"
                    };

                    var item09 = new NirSpecial
                    {
                        NirSpecialCode = "05.14.08",
                        NirSpecialName = "Энергоустановки на основе возобновляемых видов энергии"
                    };

                    var item10 = new NirSpecial
                    {
                        NirSpecialCode = "05.18.01",
                        NirSpecialName = "Технология обработки, хранения и переработки злаковых, бобовых культур, крупяных продуктов, плодоовощной продукции и виноградарства"
                    };

                    var item11 = new NirSpecial
                    {
                        NirSpecialCode = "05.18.07",
                        NirSpecialName = "Биотехнология пищевых продуктов и биологических активных веществ"
                    };

                    var item12 = new NirSpecial
                    {
                        NirSpecialCode = "05.18.12",
                        NirSpecialName = "Процессы и аппараты пищевых производств"
                    };

                    var item13 = new NirSpecial
                    {
                        NirSpecialCode = "05.20.01",
                        NirSpecialName = "Технологии и средства механизации сельского хозяйства"
                    };

                    var item14 = new NirSpecial
                    {
                        NirSpecialCode = "05.20.02",
                        NirSpecialName = "Электротехнологии и электрооборудование в сельском хозяйстве"
                    };

                    var item15 = new NirSpecial
                    {
                        NirSpecialCode = "05.20.03",
                        NirSpecialName = "Технологии и средства технического обслуживания в сельском хозяйстве"
                    };

                    var item16 = new NirSpecial
                    {
                        NirSpecialCode = "05.22.08",
                        NirSpecialName = "Управление процессами перевозок"
                    };

                    var item17 = new NirSpecial
                    {
                        NirSpecialCode = "05.22.10",
                        NirSpecialName = "Эксплуатация автомобильного транспорта"
                    };

                    var item18 = new NirSpecial
                    {
                        NirSpecialCode = "05.26.01",
                        NirSpecialName = "Охрана труда (по отраслям)"
                    };

                    var item19 = new NirSpecial
                    {
                        NirSpecialCode = "06.01.01",
                        NirSpecialName = "Общее земледелие"
                    };

                    var item20 = new NirSpecial
                    {
                        NirSpecialCode = "06.01.04",
                        NirSpecialName = "Агрохимия"
                    };

                    var item21 = new NirSpecial
                    {
                        NirSpecialCode = "06.01.05",
                        NirSpecialName = "Селекция и семеноводство сельскохозяйственных растений"
                    };

                    var item22 = new NirSpecial
                    {
                        NirSpecialCode = "06.01.07",
                        NirSpecialName = "Защита растений"
                    };

                    var item23 = new NirSpecial
                    {
                        NirSpecialCode = "07.00.02",
                        NirSpecialName = "Отечественная история"
                    };

                    var item24 = new NirSpecial
                    {
                        NirSpecialCode = "07.00.10",
                        NirSpecialName = "История науки и техники"
                    };

                    var item25 = new NirSpecial
                    {
                        NirSpecialCode = "08.00.01",
                        NirSpecialName = "Экономическая теория"
                    };

                    var item26 = new NirSpecial
                    {
                        NirSpecialCode = "08.00.05",
                        NirSpecialName = "Экономика и управление народным хозяйством (по отраслям и сферам деятельности)"
                    };

                    var item27 = new NirSpecial
                    {
                        NirSpecialCode = "08.00.10",
                        NirSpecialName = "Финансы, денежное обращение и кредит"
                    };

                    var item28 = new NirSpecial
                    {
                        NirSpecialCode = "08.00.12",
                        NirSpecialName = "Бухгалтерский учет, статистика"
                    };

                    var item29 = new NirSpecial
                    {
                        NirSpecialCode = "09.00.01",
                        NirSpecialName = "Онтология и теория познания"
                    };

                    var item30 = new NirSpecial
                    {
                        NirSpecialCode = "09.00.11",
                        NirSpecialName = "Социальная философия"
                    };

                    var item31 = new NirSpecial
                    {
                        NirSpecialCode = "13.00.01",
                        NirSpecialName = "Общая педагогика, история педагогики и образования"
                    };

                    var item32 = new NirSpecial
                    {
                        NirSpecialCode = "13.00.08",
                        NirSpecialName = "Теория и методика профессионального образования"
                    };

                    var item33 = new NirSpecial
                    {
                        NirSpecialCode = "05.25.05",
                        NirSpecialName = "Информационные системы и процессы"
                    };

                    var item34 = new NirSpecial
                    {
                        NirSpecialCode = "05.13.18",
                        NirSpecialName = "Математическое моделирование, численные методы и комплексы программ"
                    };


                    await context.NirSpecials.AddRangeAsync(
                        item01,
                        item02,
                        item03,
                        item04,
                        item05,
                        item06,
                        item07,
                        item08,
                        item09,
                        item10,
                        item11,
                        item12,
                        item13,
                        item14,
                        item15,
                        item16,
                        item17,
                        item18,
                        item19,
                        item20,
                        item21,
                        item22,
                        item23,
                        item24,
                        item25,
                        item26,
                        item27,
                        item28,
                        item29,
                        item30,
                        item31,
                        item32,
                        item33,
                        item34                        
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}