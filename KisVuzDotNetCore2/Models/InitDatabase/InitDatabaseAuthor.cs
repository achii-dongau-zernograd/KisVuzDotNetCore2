using KisVuzDotNetCore2.Models.UchPosobiya;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class InitDatabaseAuthor
    {
        /// <summary>
        /// Инициализация таблицы "Авторы"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateAuthor(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Авторы"
                if (!await context.Author.AnyAsync())
                {
                    Author AuthorName1 = new Author
                    {
                        AuthorId = 1,
                        AuthorName = "Литвинов В.Н.",
                    };

                    Author AuthorName2 = new Author
                    {
                        AuthorId = 2,
                        AuthorName = "Грачева Н.Н.",                     
                    };

                    Author AuthorName3 = new Author
                    {
                        AuthorId = 3,
                        AuthorName = "Руденко Н.Б.",                      
                    };

                    await context.Author.AddRangeAsync(
                        AuthorName1,
                        AuthorName2,
                        AuthorName3                      
                    );

                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
