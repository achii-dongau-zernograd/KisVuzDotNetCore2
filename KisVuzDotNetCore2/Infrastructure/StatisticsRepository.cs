using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Статистика использования КИС
    /// </summary>
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly AppIdentityDBContext _context;

        public StatisticsRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает количество записей в наиболее важных таблицах
        /// </summary>
        /// <returns></returns>
        public NumRowsInDataTables GetNumRowsInDataTables()
        {
            NumRowsInDataTables numRowsInDataTables = new NumRowsInDataTables();

            numRowsInDataTables.AppUsersNum = _context.Users.Count();
            numRowsInDataTables.TeachersNum = _context.Teachers.Count();
            numRowsInDataTables.StudentsNum = _context.Students.Count();
            numRowsInDataTables.StudentGroupsNum = _context.StudentGroups.Count();

            numRowsInDataTables.DisciplinesNamesNum = _context.DisciplineNames.Count();

            numRowsInDataTables.UserMessagesNum = _context.UserMessages.Count();
            numRowsInDataTables.MessagesFromAppUsersToStudentGroupsNum = _context.MessagesFromAppUsersToStudentGroups.Count();

            numRowsInDataTables.UchPosobiesNum = _context.UchPosobie.Count();

            numRowsInDataTables.ArticlesNum = _context.Articles.Count();
            numRowsInDataTables.ArticlesNotConfirmedNum = _context.Articles.Where(a => a.RowStatusId == (int)RowStatusEnum.NotConfirmed).Count();

            numRowsInDataTables.PatentsNum = _context.Patents.Count();
            numRowsInDataTables.PatentsNotConfirmedNum = _context.Patents.Where(a => a.RowStatusId == (int)RowStatusEnum.NotConfirmed).Count();

            numRowsInDataTables.MonografsNum = _context.Monografs.Count();
            numRowsInDataTables.MonografsNotConfirmedNum = _context.Monografs.Where(a => a.RowStatusId == (int)RowStatusEnum.NotConfirmed).Count();

            numRowsInDataTables.ScienceJournalsNum = _context.ScienceJournals.Count();
            numRowsInDataTables.ScienceJournalAddingClaimsNum = _context.ScienceJournalAddingClaims.Count();
            numRowsInDataTables.ScienceJournalAddingClaimsNotConfirmedNum = _context.ScienceJournalAddingClaims.Where(s => s.RowStatusId == (int)RowStatusEnum.NotConfirmed).Count();

            numRowsInDataTables.UserWorksNum = _context.UserWorks.Count();

            return numRowsInDataTables;
        }
    }
}
