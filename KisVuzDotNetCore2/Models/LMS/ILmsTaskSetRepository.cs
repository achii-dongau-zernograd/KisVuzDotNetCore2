using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Интерфейс репозитория наборов вопросов
    /// </summary>
    public interface ILmsTaskSetRepository
    {
        /// <summary>
        /// Возвращает наборы заданий
        /// </summary>
        /// <returns></returns>
        IQueryable<LmsTaskSet> GetLmsTaskSets();
        
        /// <summary>
        /// Добавляет набор заданий
        /// </summary>
        /// <param name="lmsTaskSet"></param>
        /// <returns></returns>
        Task AddLmsTaskSet(LmsTaskSet lmsTaskSet);

        /// <summary>
        /// Возвращает набор заданий
        /// </summary>
        /// <param name="lmsTaskSetId"></param>
        /// <returns></returns>
        Task<LmsTaskSet> GetLmsTaskSetAsync(int lmsTaskSetId);
        
        /// <summary>
        /// Добавляет задание в набор
        /// </summary>
        /// <param name="lmsTaskSetLmsTask"></param>
        /// <returns></returns>
        Task AddLmsTaskSetLmsTaskAsync(LmsTaskSetLmsTask lmsTaskSetLmsTask);
        
        /// <summary>
        /// Возвращает задание
        /// </summary>
        /// <param name="lmsTaskId"></param>
        /// <returns></returns>
        Task<LmsTask> GetLmsTaskAsync(int lmsTaskId);
    }
}
