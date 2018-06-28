using KisVuzDotNetCore2.Models.Education;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class InitDatadaseBlokDisciplName
    {
        /// <summary>
        /// Инициализация таблицы "Блок дисциплин Учебного плана"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateBlokDisciplName(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Блок дисциплин Учебного плана"
                if (!await context.BlokDisciplName.AnyAsync())
                {
                    BlokDisciplName BlokDisciplName1 = new BlokDisciplName
                    {
                        BlokDisciplNameId = 1,
                        BlokDisciplNameName = "Блок 1. Дисциплины (модули)"
                    };

                    BlokDisciplName BlokDisciplName2 = new BlokDisciplName
                    {
                        BlokDisciplNameId = 2,
                        BlokDisciplNameName = "Блок 2. Практики"
                    };

                    BlokDisciplName BlokDisciplName3 = new BlokDisciplName
                    {
                        BlokDisciplNameId = 3,
                        BlokDisciplNameName = "Блок 3. Государственная итоговая аттестация"
                    };

                    BlokDisciplName BlokDisciplName4 = new BlokDisciplName
                    {
                        BlokDisciplNameId = 4,
                        BlokDisciplNameName = "Блок 4. ФТД. Факультативы"
                    };

                    await context.BlokDisciplName.AddRangeAsync(
                        BlokDisciplName1,
                        BlokDisciplName2,
                        BlokDisciplName3,
                        BlokDisciplName4
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
