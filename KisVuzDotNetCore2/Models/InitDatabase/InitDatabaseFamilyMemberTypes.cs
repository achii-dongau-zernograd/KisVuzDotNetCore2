using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.LMS;
using KisVuzDotNetCore2.Models.Users;
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
    /// Инициализация таблицы "Пол"
    /// </summary>
    public class InitDatabaseFamilyMemberTypes
    {
        /// <summary>
        /// Инициализация таблицы "Типы ближайших родственных связей"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateFamilyMemberTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы ближайших родственных связей"
                if (!await context.FamilyMemberTypes.AnyAsync())
                {
                    var Row01 = new FamilyMemberType
                    {
                        FamilyMemberTypeId = 1,
                        FamilyMemberTypeName = "Отец"
                    };

                    var Row02 = new FamilyMemberType
                    {
                        FamilyMemberTypeId = 2,
                        FamilyMemberTypeName = "Мать"
                    };

                    var Row03 = new FamilyMemberType
                    {
                        FamilyMemberTypeId = 3,
                        FamilyMemberTypeName = "Жена"
                    };

                    var Row04 = new FamilyMemberType
                    {
                        FamilyMemberTypeId = 4,
                        FamilyMemberTypeName = "Муж"
                    };

                    var Row05 = new FamilyMemberType
                    {
                        FamilyMemberTypeId = 5,
                        FamilyMemberTypeName = "Другое"
                    };


                    await context.FamilyMemberTypes.AddRangeAsync(
                        Row01,
                        Row02,
                        Row03,
                        Row04,
                        Row05
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }        
    }
}
