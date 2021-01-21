using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Интерфейс репозитория электронных журналов
    /// </summary>
    public interface IElGradebookRepository
    {
        /// <summary>
        /// Возвращает запрос на список журналов согласно параметрам фильтра
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <returns></returns>
        IQueryable<ElGradebook> GetElGradebooks(ElGradebooksFilterAndSortModel filterAndSortModel);

        /// <summary>
        /// Возвращает запрос на список журналов пользователя согласно параметрам фильтра
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<ElGradebook> GetElGradebooks(ElGradebooksFilterAndSortModel filterAndSortModel, string userName);

        
    }
}
