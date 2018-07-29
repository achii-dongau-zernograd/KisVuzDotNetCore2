using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Перечисление статусов строк
    /// </summary>
    public enum RowStatusEnum
    {
        /// <summary>
        /// Не подтверждено
        /// </summary>
        NotConfirmed=1,
        /// <summary>
        /// Подтверждено
        /// </summary>
        Confirmed = 2
    }
}
