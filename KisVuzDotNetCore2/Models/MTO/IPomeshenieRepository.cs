using KisVuzDotNetCore2.Models.Sveden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.MTO
{
    /// <summary>
    /// Интерфейс репозитория помещений образовательной организации
    /// </summary>
    public interface IPomeshenieRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку всех помещений
        /// </summary>
        /// <returns></returns>
        IQueryable<Pomeshenie> GetPomeshenies();


        /// <summary>
        /// Возвращает запрос на выборку всех учебных кабинетов
        /// </summary>
        /// <returns></returns>
        IQueryable<Pomeshenie> GetUchCabinets();

        /// <summary>
        /// Возвращает запрос на выборку всех объектов для проведения практических занятий
        /// </summary>
        /// <returns></returns>
        IQueryable<Pomeshenie> GetObjectsForPractLessons();
    }
}
