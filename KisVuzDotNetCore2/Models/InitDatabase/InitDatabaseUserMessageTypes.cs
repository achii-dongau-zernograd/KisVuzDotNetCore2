using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    internal class InitDatabaseUserMessageTypes
    {
        /// <summary>
        /// Инициализация таблицы "Типы пользовательских сообщений"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateUserMessageTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы пользовательских сообщений"
                if (!await context.UserMessageTypes.AnyAsync())
                {
                    var mt01 = new UserMessageType
                    {
                        UserMessageTypeId = 1,
                        UserMessageTypeName = "Простое сообщение"
                    };

                    var mt02 = new UserMessageType
                    {
                        UserMessageTypeId = 2,
                        UserMessageTypeName = "Информирование"
                    };

                    var mt03 = new UserMessageType
                    {
                        UserMessageTypeId = 3,
                        UserMessageTypeName = "Задание по дисциплине"
                    };

                    await context.UserMessageTypes.AddRangeAsync(
                        mt01,
                        mt02,
                        mt03
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}