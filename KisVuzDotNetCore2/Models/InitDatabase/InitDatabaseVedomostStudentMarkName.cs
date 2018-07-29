using System;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Инициализация таблицы "Наименования отметок по ведомости (справочник)"
    /// </summary>
    internal class InitDatabaseVedomostStudentMarkName
    {
        /// <summary>
        /// Инициализация таблицы "Наименования отметок по ведомости (справочник)"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static async Task CreateVedomostStudentMarkName(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Наименования отметок по ведомости (справочник)"
                if (!await context.VedomostStudentMarkNames.AnyAsync())
                {
                    VedomostStudentMarkName Mark1 = new VedomostStudentMarkName
                    {
                        VedomostStudentMarkNameId = (int)VedomostStudentMarkNameEnum.Otl,
                        VedomostStudentMarkNameName = "Отлично"
                    };

                    VedomostStudentMarkName Mark2 = new VedomostStudentMarkName
                    {
                        VedomostStudentMarkNameId = (int)VedomostStudentMarkNameEnum.Hor,
                        VedomostStudentMarkNameName = "Хорошо"
                    };

                    VedomostStudentMarkName Mark3 = new VedomostStudentMarkName
                    {
                        VedomostStudentMarkNameId = (int)VedomostStudentMarkNameEnum.Udovl,
                        VedomostStudentMarkNameName = "Удовлетворительно"
                    };

                    VedomostStudentMarkName Mark4 = new VedomostStudentMarkName
                    {
                        VedomostStudentMarkNameId = (int)VedomostStudentMarkNameEnum.Neudovl,
                        VedomostStudentMarkNameName = "Неудовлетворительно"
                    };

                    VedomostStudentMarkName Mark5 = new VedomostStudentMarkName
                    {
                        VedomostStudentMarkNameId = (int)VedomostStudentMarkNameEnum.NeYavNeAtt,
                        VedomostStudentMarkNameName = "Не явился / не аттестовано"
                    };

                    VedomostStudentMarkName Mark6 = new VedomostStudentMarkName
                    {
                        VedomostStudentMarkNameId = (int)VedomostStudentMarkNameEnum.Zachteno,
                        VedomostStudentMarkNameName = "Зачтено"
                    };

                    VedomostStudentMarkName Mark7 = new VedomostStudentMarkName
                    {
                        VedomostStudentMarkNameId = (int)VedomostStudentMarkNameEnum.NeZachteno,
                        VedomostStudentMarkNameName = "Не зачтено"
                    };

                    await context.VedomostStudentMarkNames.AddRangeAsync(
                        Mark1,
                        Mark2,
                        Mark3,
                        Mark4,
                        Mark5,
                        Mark6,
                        Mark7
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}