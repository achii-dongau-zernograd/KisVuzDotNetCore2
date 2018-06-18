using Microsoft.AspNetCore.Identity;
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
    /// Инициализация таблиц с данными пользователей
    /// </summary>
    public static class InitDatabaseUserData
    {
        /// <summary>
        /// Создание учётной записи администратора
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                string username = configuration["Data:AdminUser:Name"];
                string email = configuration["Data:AdminUser:Email"];
                string password = configuration["Data:AdminUser:Password"];
                string role = configuration["Data:AdminUser:Role"];

                if (await userManager.FindByNameAsync(username) == null)
                {
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                    AppUser user = new AppUser
                    {
                        UserName = username,
                        Email = email
                    };
                    IdentityResult result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }

        /// <summary>
        /// Инициализация таблиц, связанных с пользователем
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateUserData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Группы ученых степеней"
                if (!await context.AcademicDegreeGroups.AnyAsync())
                {
                    AcademicDegreeGroup AcademicDegreeGroup1 = new AcademicDegreeGroup
                    {
                        AcademicDegreeGroupId = 1,
                        AcademicDegreeGroupName = "Кандидаты наук"
                    };

                    AcademicDegreeGroup AcademicDegreeGroup2 = new AcademicDegreeGroup
                    {
                        AcademicDegreeGroupId = 2,
                        AcademicDegreeGroupName = "Доктора наук"
                    };

                    await context.AcademicDegreeGroups.AddRangeAsync(
                        AcademicDegreeGroup1,
                        AcademicDegreeGroup2
                    );
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Ученые степени"
                if (!await context.AcademicDegrees.AnyAsync())
                {
                    AcademicDegree AcademicDegree1 = new AcademicDegree
                    {
                        AcademicDegreeId = 1,
                        AcademicDegreeGroupId = 1,
                        AcademicDegreeName = "Кандидат технических наук"
                    };

                    AcademicDegree AcademicDegree2 = new AcademicDegree
                    {
                        AcademicDegreeId = 2,
                        AcademicDegreeGroupId = 1,
                        AcademicDegreeName = "Кандидат сельскохозяйственных наук"
                    };

                    AcademicDegree AcademicDegree3 = new AcademicDegree
                    {
                        AcademicDegreeId = 3,
                        AcademicDegreeGroupId = 2,
                        AcademicDegreeName = "Доктор технических наук"
                    };

                    AcademicDegree AcademicDegree4 = new AcademicDegree
                    {
                        AcademicDegreeId = 4,
                        AcademicDegreeGroupId = 2,
                        AcademicDegreeName = "Доктор сельскохозяйственных наук"
                    };

                    await context.AcademicDegrees.AddRangeAsync(
                        AcademicDegree1,
                        AcademicDegree2,
                        AcademicDegree3,
                        AcademicDegree4
                    );
                    await context.SaveChangesAsync();
                }
                #endregion

                #region Инициализация таблицы "Ученые звания"
                if (!await context.AcademicStats.AnyAsync())
                {
                    AcademicStat AcademicStat1 = new AcademicStat
                    {
                        AcademicStatId = 1,
                        AcademicStatName = "Доцент"
                    };

                    AcademicStat AcademicStat2 = new AcademicStat
                    {
                        AcademicStatId = 2,
                        AcademicStatName = "Профессор"
                    };

                    await context.AcademicStats.AddRangeAsync(
                        AcademicStat1,
                        AcademicStat2
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

        /// <summary>
        /// Заполнение профилей пользователей
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task SettingAdminsProfileData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Группы ученых степеней"
                if (await context.Users.AnyAsync(u=>u.Email == configuration["Data:AdminUser:Email"]))
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == configuration["Data:AdminUser:Email"]);
                    if(user.LastName==null)
                    {
                        user.AcademicDegreeId = 1;
                        user.AcademicStatId = 1;
                        user.Birthdate = new DateTime(1983, 4, 5);
                        user.DateStartWorking = new DateTime(2005, 9, 1);
                        user.DateStartWorkingSpec = new DateTime(2005, 9, 1);
                        user.EduLevelGroupId = 2;
                        user.FirstName = "Владимир";
                        user.LastName = "Литвинов";
                        user.Patronymic = "Николаевич";
                        user.PhoneNumber = "89185172138";
                    }                    

                    await context.SaveChangesAsync();
                }
                #endregion                
            }
        }

    }
}
