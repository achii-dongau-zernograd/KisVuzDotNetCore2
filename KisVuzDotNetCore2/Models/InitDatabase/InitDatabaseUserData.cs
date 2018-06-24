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
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();

                #region Заполнение данных первого пользователя
                if (await context.Users.AnyAsync(u=>u.Email == configuration["Data:AdminUser:Email"]))
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == configuration["Data:AdminUser:Email"]);
                    if(user!=null && user.LastName==null)
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
                        
                        await userManager.UpdateAsync(user);

                        Qualification qualification1 =
                            new Qualification
                            {
                                AppUserId = user.Id,
                                NapravlName = "Электрификация и автоматизация сельского хозяйства",
                                QualificationName = "Инженер-электрик",
                                RowStatusId = 2
                            };
                        await context.Qualifications.AddAsync(qualification1);
                        await context.SaveChangesAsync();
                    }
                }
                #endregion                
            }
        }

        /// <summary>
        /// Создание учётных записей основных пользователей
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateMainUsersAccounts(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                                
                string role = configuration["Data:AdminUser:Role"];

                List<AppUser> appUsers = new List<AppUser>();
                AppUser user1 = new AppUser
                {
                    UserName = "GrachevaNN",
                    Email = "GrachevaNN@example.com",
                    PasswordHash= "secret",
                    AcademicDegreeId=1,
                    AcademicStatId=1,
                    Birthdate = new DateTime(1970,12,30),
                    DateStartWorking = new DateTime(1995, 09, 01),
                    DateStartWorkingSpec = new DateTime(1995, 09, 01),
                    EduLevelGroupId=2,
                    FirstName="Наталья",
                    LastName="Грачева",
                    Patronymic="Николаевна",
                    PhoneNumber="8928123456",
                    Qualifications=new List<Qualification>
                    {
                        new Qualification
                        {
                            NapravlName="Прикладная математика",
                            QualificationName="Математик",
                            RowStatusId=2
                        }
                    }
                };

                AppUser user2 = new AppUser
                {
                    UserName = "RudenkoNB",
                    Email = "RudenkoNB@example.com",
                    PasswordHash = "secret",
                    AcademicDegreeId = 1,
                    AcademicStatId = 1,
                    Birthdate = new DateTime(1970, 11, 20),
                    DateStartWorking = new DateTime(1996, 09, 01),
                    DateStartWorkingSpec = new DateTime(1997, 05, 5),
                    EduLevelGroupId = 2,
                    FirstName = "Нелли",
                    LastName = "Руденко",
                    Patronymic = "Борисовна",
                    PhoneNumber = "8928234567",
                    Qualifications = new List<Qualification>
                    {
                        new Qualification
                        {
                            NapravlName="Прикладная математика",
                            QualificationName="Математик",
                            RowStatusId=2
                        }
                    }
                };

                appUsers.AddRange(new List<AppUser>{ user1, user2 });

                foreach(AppUser user in appUsers)
                {
                    if (await userManager.FindByNameAsync(user.UserName) == null)
                    {
                        if (await roleManager.FindByNameAsync(role) == null)
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }

                        IdentityResult result = await userManager.CreateAsync(user, user.PasswordHash);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, role);
                        }
                    }
                }

                
            }
        }


        /// <summary>
        /// Создание учётных записей студентов
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateStudentsAccounts(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                string role = "Студенты";

                List<AppUser> appUsers = new List<AppUser>();
                AppUser user1 = new AppUser
                {
                    UserName = "student1",
                    Email = "student1@example.com",
                    PasswordHash = "secret",                    
                    Birthdate = new DateTime(2000, 12, 30),                    
                    EduLevelGroupId = 1,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович",
                    PhoneNumber = "8928111111",                    
                };

                AppUser user2 = new AppUser
                {
                    UserName = "student2",
                    Email = "student2@example.com",
                    PasswordHash = "secret",
                    Birthdate = new DateTime(2000, 12, 30),
                    EduLevelGroupId = 1,
                    FirstName = "Петр",
                    LastName = "Петров",
                    Patronymic = "Петрович",
                    PhoneNumber = "8928222222",
                };

                appUsers.AddRange(new List<AppUser> { user1, user2 });

                foreach (AppUser user in appUsers)
                {
                    if (await userManager.FindByNameAsync(user.UserName) == null)
                    {
                        if (await roleManager.FindByNameAsync(role) == null)
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }

                        IdentityResult result = await userManager.CreateAsync(user, user.PasswordHash);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, role);
                        }
                    }
                }


            }
        }


        /// <summary>
        /// Создание учётных записей отдела кадров
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateOtdelKadrovAccounts(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                string role = "Отдел кадров";

                List<AppUser> appUsers = new List<AppUser>();
                AppUser user1 = new AppUser
                {
                    UserName = "kadri1",
                    Email = "kadri1@example.com",
                    PasswordHash = "secret",
                    Birthdate = new DateTime(2000, 12, 30),
                    EduLevelGroupId = 1,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович",
                    PhoneNumber = "8928111111",
                };
                               

                appUsers.AddRange(new List<AppUser> { user1 });

                foreach (AppUser user in appUsers)
                {
                    if (await userManager.FindByNameAsync(user.UserName) == null)
                    {
                        if (await roleManager.FindByNameAsync(role) == null)
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }

                        IdentityResult result = await userManager.CreateAsync(user, user.PasswordHash);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, role);
                        }
                    }
                }


            }
        }


    }
}
