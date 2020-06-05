using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Интерфейс репозитория заданий СДО
    /// </summary>
    public interface ILmsTaskRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку всех заданий СДО
        /// </summary>
        /// <returns></returns>
        IQueryable<LmsTask> GetLmsTasks();
        
        /// <summary>
        /// Добавляет новое задание в базу заданий СДО
        /// </summary>
        /// <param name="lmsTask"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task AddLmsTask(LmsTask lmsTask, IFormFile uploadedFile);
        
        /// <summary>
        /// Возвращает задание СДО
        /// </summary>
        /// <param name="lmsTaskId"></param>
        /// <returns></returns>
        Task<LmsTask> GetLmsTask(int lmsTaskId);
        
        /// <summary>
        /// Добавляет к заданию СДО дисциплину
        /// </summary>
        /// <param name="lmsTask"></param>
        /// <param name="disciplineNameId"></param>
        /// <returns></returns>
        Task AddLmsTaskDisciplineName(LmsTask lmsTask, int disciplineNameId);

        /// <summary>
        /// Добавляет к заданию СДО вариант ответа
        /// </summary>
        /// <param name="lmsTaskAnswer"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task AddLmsTaskAnswer(LmsTaskAnswer lmsTaskAnswer, IFormFile uploadedFile);
    }
}
