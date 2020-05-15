using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Интерфейс репозитория населённых пунктов
    /// </summary>
    public interface IPopulatedLocalityRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку населённых пунктов
        /// </summary>
        /// <returns></returns>
        IQueryable<PopulatedLocality> GetPopulatedLocalities();
    }
}
