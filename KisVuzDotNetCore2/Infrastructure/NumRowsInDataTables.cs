using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Число записей в таблицах
    /// </summary>
    public class NumRowsInDataTables
    {
        /// <summary>
        /// Количество пользователей, зарегистрированных в системе
        /// </summary>
        public int AppUsersNum { get; internal set; }
    }
}
