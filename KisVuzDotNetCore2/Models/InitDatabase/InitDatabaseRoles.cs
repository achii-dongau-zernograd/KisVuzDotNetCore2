using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    /// <summary>
    /// Создание ролей
    /// </summary>
    public class InitDatabaseRoles
    {
        /// <summary>
        /// Создание учётной записи администратора
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                List<string> Roles = new List<string>
                {
                    "Администраторы",
                    "Студенты",
                    "Преподаватели",
                    "Отдел кадров",
                    "Бухгалтерия",
                    "Учебная часть",
                    "Деканат энергетического факультета",
                    "Деканат инженерно-технологического факультета",
                    "Деканат факультета ЭиУТ",
                    "Деканат факультета СПО",
                    "Приёмная комиссия",
                    "Библиотека",
                    "ЗамДиректораПоВоспРаботе",
                    "ЗамДиректораПоСоцРаботе",
                    "Канцелярия",
                    "НИЧ",
                    "ЦПОРМ",
                    "Юротдел"
                };

                foreach(string role in Roles)
                {
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }                
            }
        }
    }
}
