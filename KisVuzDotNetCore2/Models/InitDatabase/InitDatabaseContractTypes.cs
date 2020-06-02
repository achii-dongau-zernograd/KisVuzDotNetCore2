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
    /// Инициализация справочника типов договоров
    /// </summary>
    public class InitDatabaseContractTypes
    {
        /// <summary>
        /// Инициализация справочника типов договоров
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateContractTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы договоров"
                if (!await context.ContractTypes.AnyAsync())
                {
                    var Row01 = new ContractType
                    {
                        ContractTypeId = (int)ContractTypesEnum.ContractTargetTraining,
                        ContractTypeName = "Договор о целевом обучении"
                    };

                    var Row02 = new ContractType
                    {
                        ContractTypeId = (int)ContractTypesEnum.ContractPaidTraining,
                        ContractTypeName = "Договор об оказании платных образовательных услуг"
                    };

                    await context.ContractTypes.AddRangeAsync(
                        Row01, Row02
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }        
    }
}
