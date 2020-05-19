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
    /// Инициализация таблицы "Типы событий СДО"
    /// </summary>
    public class InitDatabaseLmsEventTypes
    {
        /// <summary>
        /// Инициализация таблицы "Группы типов событий СДО"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateLmsEventTypeGroups(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Группы типов событий СДО"
                if (!await context.LmsEventTypeGroups.AnyAsync())
                {
                    var Row01 = new LmsEventTypeGroup
                    {
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.Priem,
                        LmsEventTypeGroupName = "События, организовываемые в процессе проведения приёмной кампании"
                    };

                    var Row02 = new LmsEventTypeGroup
                    {
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.StudyingProccess,
                        LmsEventTypeGroupName = "События, организовываемые в процессе обучения"
                    };

                    var Row03 = new LmsEventTypeGroup
                    {
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.CurrentCertification,
                        LmsEventTypeGroupName = "События, организовываемые в процессе текущей аттестации"
                    };

                    var Row04 = new LmsEventTypeGroup
                    {
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.IntermediateCertification,
                        LmsEventTypeGroupName = "События, организовываемые в процессе промежуточной аттестации"
                    };

                    var Row05 = new LmsEventTypeGroup
                    {
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.StateFinalCertification,
                        LmsEventTypeGroupName = "События, организовываемые в процессе государственной итоговой аттестации (ГИА)"
                    };


                    await context.LmsEventTypeGroups.AddRangeAsync(
                        Row01,
                        Row02,
                        Row03,
                        Row04,
                        Row05
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }

        /// <summary>
        /// Инициализация таблицы "Типы событий СДО"
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task CreateLmsEventTypes(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                AppIdentityDBContext context = serviceScope.ServiceProvider.GetService<AppIdentityDBContext>();

                #region Инициализация таблицы "Типы событий СДО"
                if (!await context.LmsEventTypes.AnyAsync())
                {
                    /////////////////////////////////////// Приём ///////////////////////////
                    var Row11 = new LmsEventType
                    {
                        LmsEventTypeId = (int)LmsEventTypesEnum.Priem_EntranceTest,
                        LmsEventTypeName = "Вступительное испытание",
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.Priem                        
                    };

                    ////////////////////////////////// Учебный процесс ///////////////////////

                    var Row21 = new LmsEventType
                    {
                        LmsEventTypeId = (int)LmsEventTypesEnum.StudyingProccess_Lecture,
                        LmsEventTypeName = "Лекция",
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.StudyingProccess
                    };

                    var Row22 = new LmsEventType
                    {
                        LmsEventTypeId = (int)LmsEventTypesEnum.StudyingProccess_PracticalLesson,
                        LmsEventTypeName = "Практическое занятие",
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.StudyingProccess
                    };

                    var Row23 = new LmsEventType
                    {
                        LmsEventTypeId = (int)LmsEventTypesEnum.StudyingProccess_LaboratoryWork,
                        LmsEventTypeName = "Лабораторная работа",
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.StudyingProccess
                    };

                    ////////////////////////////////// Текущая аттестация ///////////////////////
                    var Row31 = new LmsEventType
                    {
                        LmsEventTypeId = (int)LmsEventTypesEnum.CurrentCertification_Test,
                        LmsEventTypeName = "Текущая аттестация - тест",
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.CurrentCertification
                    };

                    /////////////////////////////// Промежуточная аттестация ///////////////////////
                    var Row41 = new LmsEventType
                    {
                        LmsEventTypeId = (int)LmsEventTypesEnum.IntermediateCertification_Midterm,
                        LmsEventTypeName = "Промежуточная аттестация - зачет",
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.IntermediateCertification
                    };
                    var Row42 = new LmsEventType
                    {
                        LmsEventTypeId = (int)LmsEventTypesEnum.IntermediateCertification_MidtermWithMark,
                        LmsEventTypeName = "Промежуточная аттестация - зачет с оценкой",
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.IntermediateCertification
                    };
                    var Row43 = new LmsEventType
                    {
                        LmsEventTypeId = (int)LmsEventTypesEnum.IntermediateCertification_Exam,
                        LmsEventTypeName = "Промежуточная аттестация - экзамен",
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.IntermediateCertification
                    };
                    /////////////////////////// Государственная итоговая аттестация ///////////////////////
                    var Row51 = new LmsEventType
                    {
                        LmsEventTypeId = (int)LmsEventTypesEnum.StateFinalCertification_FinalQualifyingWorkDefense,
                        LmsEventTypeName = "Государственная итоговая аттестация - защита ВКР",
                        LmsEventTypeGroupId = (int)LmsEventTypeGroupsEnum.StateFinalCertification
                    };


                    await context.LmsEventTypes.AddRangeAsync(
                        Row11,
                        Row21, Row22, Row23,
                        Row31,
                        Row41, Row42, Row43,
                        Row51
                    );
                    await context.SaveChangesAsync();
                }
                #endregion
            }
        }
    }
}
