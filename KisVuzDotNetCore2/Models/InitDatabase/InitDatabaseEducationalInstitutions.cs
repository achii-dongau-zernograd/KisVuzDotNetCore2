using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Struct;
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
    /// Инициализация таблицы "Образовательные организации"
    /// </summary>
    public class InitDatabaseEducationalInstitutions
    {
        /// <summary>
        /// Инициализация таблицы "Образовательные организации"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateEducationalInstitutions(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Образовательные организации"
                if (!await context.EducationalInstitutions.AnyAsync())
                {
                    var row01 = new EducationalInstitution
                    {
                        EducationalInstitutionId = 1,
                        EducationalInstitutionName = "МБОУ гимназия г.Зернограда",
                        EducationalInstitutionTypeId = (int)EducationalInstitutionTypeEnum.ObsheobrazovatelnayaOrganizaciya,
                        Location = new Location
                        {
                            Address = new Address
                            {
                                Country = "Россия",
                                PostCode = "347740",
                                PopulatedLocalityId = 1,
                                Region = "Ростовская область",
                                Settlement = "г. Зерноград",
                                Street = "ул. Советская",
                                HouseNumber = "42/11"
                            },
                            GpsCoordinate = new GpsCoordinate
                            {
                                Latitude = 46.851765m,
                                Longitude = 40.313441m
                            }
                        }
                    };

                    var row02 = new EducationalInstitution
                    {
                        EducationalInstitutionId = 2,
                        EducationalInstitutionName = "МБОУ СОШ УИОП г.Зернограда",
                        EducationalInstitutionTypeId = (int)EducationalInstitutionTypeEnum.ObsheobrazovatelnayaOrganizaciya,
                        Location = new Location
                        {
                            Address = new Address
                            {
                                Country = "Россия",
                                PostCode = "347740",
                                PopulatedLocalityId = 1,
                                Region = "Ростовская область",
                                Settlement = "г. Зерноград",
                                Street = "ул. Ленина",
                                HouseNumber = "42"
                            },
                            GpsCoordinate = new GpsCoordinate
                            {
                                Latitude = 46.853572m,
                                Longitude = 40.320881m
                            }
                        }
                    };

                    var row03 = new EducationalInstitution
                    {
                        EducationalInstitutionId = 3,
                        EducationalInstitutionName = "Азово-Черноморский инженерный институт ФГБОУ ВО Донской ГАУ",
                        EducationalInstitutionTypeId = (int)EducationalInstitutionTypeEnum.ObrazovatelnayaOrganizaciyaVisshegoObrazovaniya,
                        Location = new Location
                        {
                            Address = new Address
                            {
                                Country = "Россия",
                                PostCode = "347740",
                                PopulatedLocalityId = 1,
                                Region = "Ростовская область",
                                Settlement = "г. Зерноград",
                                Street = "ул. Ленина",
                                HouseNumber = "21"
                            },
                            GpsCoordinate = new GpsCoordinate
                            {
                                Latitude = 46.847864m,
                                Longitude = 40.309868m
                            }
                        }
                    };

                    await context.EducationalInstitutions.AddRangeAsync(
                        row01,
                        row02,
                        row03
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
