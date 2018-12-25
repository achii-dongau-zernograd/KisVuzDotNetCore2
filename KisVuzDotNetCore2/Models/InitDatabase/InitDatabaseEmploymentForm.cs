using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Nir;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Инициализация таблицы форм занятости
    /// </summary>
    public static class InitDatabaseEmploymentForm
    {
        /// <summary>
        /// Инициализация таблицы "Формы занятости"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEmploymentForms(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Формы занятости"
                if (!await context.EmploymentForms.AnyAsync())
                {
                    EmploymentForm EmploymentForm1 = new EmploymentForm
                    {
                        EmploymentFormId = 1,
                        EmploymentFormName = "основное"
                    };
                    EmploymentForm EmploymentForm2 = new EmploymentForm
                    {
                        EmploymentFormId = 2,
                        EmploymentFormName = "внутреннее совместительство"
                    };
                    EmploymentForm EmploymentForm3 = new EmploymentForm
                    {
                        EmploymentFormId = 3,
                        EmploymentFormName = "внешнее совместительство"
                    };
                    EmploymentForm EmploymentForm4 = new EmploymentForm
                    {
                        EmploymentFormId = 4,
                        EmploymentFormName = "работа по договору ГПХ"
                    };

                    await context.EmploymentForms.AddRangeAsync(
                        EmploymentForm1,
                        EmploymentForm2,
                        EmploymentForm3,
                        EmploymentForm4
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}