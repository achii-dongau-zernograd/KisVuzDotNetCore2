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
    /// Инициализация таблицы "Справочник оценок за работы пользователя (курсовые, ВКР, эссе и пр.)"
    /// </summary>
    internal class InitDatabaseUserWorkReviewMarks
    {
        /// <summary>
        /// Инициализация таблицы "Справочник оценок за работы пользователя (курсовые, ВКР, эссе и пр.)"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static async Task CreateUserWorkReviewMarks(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Справочник оценок за работы пользователя (курсовые, ВКР, эссе и пр.)"
                if (!await context.UserWorkReviewMarks.AnyAsync())
                {
                    var items = new List<UserWorkReviewMark>();
                    items.Add(new UserWorkReviewMark
                    {
                        UserWorkReviewMarkId = (int)UserWorkReviewMarkEnum.Otl,
                        UserWorkReviewMarkName = "Отлично"
                    });

                    items.Add(new UserWorkReviewMark
                    {
                        UserWorkReviewMarkId = (int)UserWorkReviewMarkEnum.Hor,
                        UserWorkReviewMarkName = "Хорошо"
                    });

                    items.Add(new UserWorkReviewMark
                    {
                        UserWorkReviewMarkId = (int)UserWorkReviewMarkEnum.Udovl,
                        UserWorkReviewMarkName = "Удовлетворительно"
                    });

                    items.Add(new UserWorkReviewMark
                    {
                        UserWorkReviewMarkId = (int)UserWorkReviewMarkEnum.Neudovl,
                        UserWorkReviewMarkName = "Неудовлетворительно"
                    });                                       
                    
                    await context.UserWorkReviewMarks.AddRangeAsync(items);
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}