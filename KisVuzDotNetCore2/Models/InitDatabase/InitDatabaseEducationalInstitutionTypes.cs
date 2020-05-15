using KisVuzDotNetCore2.Models.Common;
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
    /// Инициализация таблицы "Типы образовательных организаций"
    /// </summary>
    public class InitDatabaseEducationalInstitutionTypes
    {
        /// <summary>
        /// Инициализация таблицы "Типы образовательных организаций"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEducationalInstitutionTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы образовательных организаций"
                if (!await context.EducationalInstitutionTypes.AnyAsync())
                {
                    EducationalInstitutionType EducationalInstitutionType1 = new EducationalInstitutionType
                    {
                        EducationalInstitutionTypeId = (int)EducationalInstitutionTypeEnum.ObsheobrazovatelnayaOrganizaciya,
                        EducationalInstitutionTypeName = "Общеобразовательная организация"
                    };

                    EducationalInstitutionType EducationalInstitutionType2 = new EducationalInstitutionType
                    {
                        EducationalInstitutionTypeId = (int)EducationalInstitutionTypeEnum.ProfessionalnayaObrazovatelnayaOrganizaciya,
                        EducationalInstitutionTypeName = "Профессиональная образовательная организация"
                    };

                    EducationalInstitutionType EducationalInstitutionType3 = new EducationalInstitutionType
                    {
                        EducationalInstitutionTypeId = (int)EducationalInstitutionTypeEnum.ObrazovatelnayaOrganizaciyaVisshegoObrazovaniya,
                        EducationalInstitutionTypeName = "Образовательная организация высшего образования"
                    };

                    await context.EducationalInstitutionTypes.AddRangeAsync(
                        EducationalInstitutionType1,
                        EducationalInstitutionType2,
                        EducationalInstitutionType3
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
