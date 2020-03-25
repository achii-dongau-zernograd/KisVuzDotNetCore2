using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Статистика использования КИС (интерфейс)
    /// </summary>
    public interface IStatisticsRepository
    {
        NumRowsInDataTables GetNumRowsInDataTables();
    }
}
