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
        /// Возвращает задание
        /// </summary>
        /// <param name="lmsTaskId"></param>
        /// <returns></returns>
        Task<LmsTask> GetLmsTaskAsync(int lmsTaskId);

        /// <summary>
        /// Обновление сущности "Набор заданий"
        /// </summary>
        /// <param name="lmsTaskSet"></param>
        /// <returns></returns>
        Task UpdateLmsTaskSet(LmsTaskSet lmsTaskSet);

        /// <summary>
        /// Удаление сущности "Набор заданий" и привязок "Набор заданий" - "Задание"
        /// </summary>
        /// <param name="lmsTaskSet"></param>
        /// <returns></returns>
        Task RemoveLmsTaskSet(LmsTaskSet lmsTaskSet);


        /// <summary>
        /// Добавляет задание в набор
        /// </summary>
        /// <param name="lmsTaskSetLmsTask"></param>
        /// <returns></returns>
        Task AddLmsTaskSetLmsTaskAsync(LmsTaskSetLmsTask lmsTaskSetLmsTask);

        /// <summary>
        /// Удаляет задание из набора
        /// </summary>
        /// <param name="lmsTaskSetLmsTask"></param>
        /// <returns></returns>
        Task RemoveLmsTaskSetLmsTaskAsync(LmsTaskSetLmsTask lmsTaskSetLmsTask);
        
        /// <summary>
        /// Обновление последовательности заданий
        /// </summary>
        /// <param name="lmsTaskSetLmsTaskId"></param>
        /// <param name="lmsTaskSetLmsTaskOrder"></param>
        /// <returns></returns>
        Task UpdateLmsTaskSetLmsTasksOrderAsync(int[] lmsTaskSetLmsTaskId, int?[] lmsTaskSetLmsTaskOrder);
    }
}
