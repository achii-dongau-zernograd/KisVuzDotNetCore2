using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Инициализация таблицы "Типы работ пользователя (курсовые, ВКР, эссе и пр.)"
    /// </summary>
    internal class InitDatabaseUserWorkType
    {
        /// <summary>
        /// Инициализация таблицы "Типы работ пользователя (курсовые, ВКР, эссе и пр.)"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static async Task CreateUserWorkType(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы работ пользователя (курсовые, ВКР, эссе и пр.)"
                if (!await context.UserWorkTypes.AnyAsync())
                {
                    var items = new List<UserWorkType>();
                    items.Add( new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.Esse,
                        UserWorkTypeName = "Эссе"
                    });

                    items.Add(new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.Referat,
                        UserWorkTypeName = "Реферат"
                    });

                    items.Add(new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.Sochinenie,
                        UserWorkTypeName = "Сочинение"
                    });

                    items.Add(new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.Perevod,
                        UserWorkTypeName = "Перевод текста с иностранного языка / на иностранный язык"
                    });

                    items.Add(new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.OtchetLabRabota,
                        UserWorkTypeName = "Отчет по лабораторной работе"
                    });

                    items.Add(new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.OtchetPractica,
                        UserWorkTypeName = "Отчет по практике"
                    });

                    items.Add(new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.OtchetSamostRabota,
                        UserWorkTypeName = "Отчет по самостоятельной работе"
                    });

                    items.Add(new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.KursRabota,
                        UserWorkTypeName = "Курсовая работа"
                    });

                    items.Add(new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.KursProekt,
                        UserWorkTypeName = "Курсовой проект"
                    });

                    items.Add(new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.VKR,
                        UserWorkTypeName = "Выпускная квалификационная работа"
                    });

                    items.Add(new UserWorkType
                    {
                        UserWorkTypeId = (int)UserWorkTypeEnum.Other,
                        UserWorkTypeName = "Другое"
                    });

                    await context.UserWorkTypes.AddRangeAsync(items);
                    await context.SaveChangesAsync();
                }
                #endregion
            }

        }
    }
}