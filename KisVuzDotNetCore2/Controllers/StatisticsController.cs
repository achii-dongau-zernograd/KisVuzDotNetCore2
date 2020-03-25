using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace KisVuzDotNetCore2.Controllers
{
    /// <summary>
    /// Контроллер статистики использования КИС
    /// </summary>
    public class StatisticsController : Controller
    {
        IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public IActionResult Index()
        {            
            NumRowsInDataTables statistics = _statisticsRepository.GetNumRowsInDataTables();

            return View(statistics);
        }
    }
}