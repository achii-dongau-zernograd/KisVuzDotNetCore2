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
    /// Инициализация справочника типов заданий СДО
    /// </summary>
    public class InitDatabaseLmsTaskTypes
    {
        /// <summary>
        /// Инициализация справочника типов заданий СДО
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateLmsTaskTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы заданий СДО"
                if (!await context.LmsTaskTypes.AnyAsync())
                {
                    var Row01 = new LmsTaskType
                    {
                        LmsTaskTypeId = (int)LmsTaskTypesEnum.OneCorrectAnswer,
                        LmsTaskTypeName = "Один правильный ответ"
                    };

                    var Row02 = new LmsTaskType
                    {
                        LmsTaskTypeId = (int)LmsTaskTypesEnum.SeveralCorrectAnswers,
                        LmsTaskTypeName = "Несколько правильных ответов"
                    };

                    var Row03 = new LmsTaskType
                    {
                        LmsTaskTypeId = (int)LmsTaskTypesEnum.InputText,
                        LmsTaskTypeName = "Ввод текста"
                    };

                    var Row04 = new LmsTaskType
                    {
                        LmsTaskTypeId = (int)LmsTaskTypesEnum.InputNumber,
                        LmsTaskTypeName = "Ввод числа"
                    };

                    var Row05 = new LmsTaskType
                    {
                        LmsTaskTypeId = (int)LmsTaskTypesEnum.FileUpload,
                        LmsTaskTypeName = "Загрузка файла со скан-копией решения"
                    };

                    await context.LmsTaskTypes.AddRangeAsync(
                        Row01, Row02, Row03, Row04, Row05
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }        
    }
}
