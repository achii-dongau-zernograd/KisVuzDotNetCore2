using KisVuzDotNetCore2.Models;
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

            return numRowsInDataTables;
        }
    }
}
