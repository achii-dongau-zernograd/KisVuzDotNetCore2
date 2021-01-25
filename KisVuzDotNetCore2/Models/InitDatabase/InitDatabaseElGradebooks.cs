using KisVuzDotNetCore2.Models.Gradebook;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class InitDatabaseElGradebooks
    {
        /// <summary>
        /// Инициализация таблицы "Типы учебных занятий"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateElGradebookLessonTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();
                                
                if (!await context.ElGradebookLessonTypes.AnyAsync())
                {
                    var item1 = new ElGradebookLessonType
                    {
                        ElGradebookLessonTypeId = 1,
                        ElGradebookLessonTypeName = "Лекция",
                    };

                    var item2 = new ElGradebookLessonType
                    {
                        ElGradebookLessonTypeId = 2,
                        ElGradebookLessonTypeName = "Лабораторная работа",
                    };

                    var item3 = new ElGradebookLessonType
                    {
                        ElGradebookLessonTypeId = 3,
                        ElGradebookLessonTypeName = "Практическое занятие",
                    };

                    var item4 = new ElGradebookLessonType
                    {
                        ElGradebookLessonTypeId = 4,
                        ElGradebookLessonTypeName = "Консультация",
                    };

                    var item5 = new ElGradebookLessonType
                    {
                        ElGradebookLessonTypeId = 5,
                        ElGradebookLessonTypeName = "Зачет",
                    };

                    var item6 = new ElGradebookLessonType
                    {
                        ElGradebookLessonTypeId = 6,
                        ElGradebookLessonTypeName = "Экзамен",
                    };


                    await context.AddRangeAsync(
                        item1,
                        item2,
                        item3,
                        item4,
                        item5,
                        item6
                    );

                    await context.SaveChangesAsync();
                }                
            }
        }

        /// <summary>
        /// Инициализация таблицы "Типы посещаемости учебных занятий"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateElGradebookLessonAttendanceTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                if (!await context.ElGradebookLessonAttendanceTypes.AnyAsync())
                {
                    var item1 = new ElGradebookLessonAttendanceType
                    {
                        ElGradebookLessonAttendanceTypeId = 1,
                        ElGradebookLessonAttendanceTypeName = "П",
                    };

                    var item2 = new ElGradebookLessonAttendanceType
                    {
                        ElGradebookLessonAttendanceTypeId = 2,
                        ElGradebookLessonAttendanceTypeName = "Н",
                    };

                    var item3 = new ElGradebookLessonAttendanceType
                    {
                        ElGradebookLessonAttendanceTypeId = 3,
                        ElGradebookLessonAttendanceTypeName = "У",
                    };

                    var item4 = new ElGradebookLessonAttendanceType
                    {
                        ElGradebookLessonAttendanceTypeId = 4,
                        ElGradebookLessonAttendanceTypeName = "Б",
                    };

                    await context.AddRangeAsync(
                        item1,
                        item2,
                        item3,
                        item4
                    );

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
