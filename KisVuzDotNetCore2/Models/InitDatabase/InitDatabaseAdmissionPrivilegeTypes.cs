using KisVuzDotNetCore2.Models.Abitur;
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
    /// Инициализация таблицы "Типы отношения к военной службе"
    /// </summary>
    public class InitDatabaseAdmissionPrivilegeTypes
    {
        /// <summary>
        /// Инициализация таблицы "Типы отношения к военной службе"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateAdmissionPrivilegeTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы отношения к военной службе"
                if (!await context.AdmissionPrivilegeTypes.AnyAsync())
                {
                    var Row01 = new AdmissionPrivilegeType
                    {
                        AdmissionPrivilegeTypeId = 1,
                        AdmissionPrivilegeTypeName = "Зачисление без вступительных испытаний"
                    };

                    var Row02 = new AdmissionPrivilegeType
                    {
                        AdmissionPrivilegeTypeId = 2,
                        AdmissionPrivilegeTypeName = "Приравнивание к лицам, успешно прошедшим вступительные испытания"
                    };

                    var Row03 = new AdmissionPrivilegeType
                    {
                        AdmissionPrivilegeTypeId = 3,
                        AdmissionPrivilegeTypeName = "Приравнивание к лицам, набравшим максимальное количество баллов по ЕГЭ"
                    };

                    var Row04 = new AdmissionPrivilegeType
                    {
                        AdmissionPrivilegeTypeId = 4,
                        AdmissionPrivilegeTypeName = "По квоте приёма лиц, имеющих особое право"
                    };

                    var Row05 = new AdmissionPrivilegeType
                    {
                        AdmissionPrivilegeTypeId = 5,
                        AdmissionPrivilegeTypeName = "Преимущественное право на поступление"
                    };

                    await context.AdmissionPrivilegeTypes.AddRangeAsync(
                        Row01, Row02, Row03, Row04, Row05
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }        
    }
}
