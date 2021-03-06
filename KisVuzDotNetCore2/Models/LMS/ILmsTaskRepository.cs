﻿using Microsoft.AspNetCore.Http;
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
        /// Возвращает запрос на выборку заданий СДО,
        /// удовлетворяющих фильтру и соответственно отсортированных 
        /// </summary>
        /// <param name="lmsTasksFilterAndSortModel"></param>
        /// <returns></returns>
        IQueryable<LmsTask> GetLmsTasks(LmsTasksFilterAndSortModel lmsTasksFilterAndSortModel);

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
        Task<LmsTask> GetLmsTaskAsync(int lmsTaskId);
        
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
        
        /// <summary>
        /// Обновляет задание СДО
        /// </summary>
        /// <param name="lmsTask"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateLmsTaskAsync(LmsTask lmsTask, IFormFile uploadedFile);
        
        /// <summary>
        /// Удаляет задание СДО
        /// </summary>
        /// <param name="lmsTask"></param>
        /// <returns></returns>
        Task RemoveLmsTaskAsync(LmsTask lmsTask);
        
        /// <summary>
        /// Возвращает вариант ответа по переданному УИД
        /// </summary>
        /// <param name="lmsTaskAnswerId"></param>
        /// <returns></returns>
        Task<LmsTaskAnswer> GetLmsTaskAnswer(int lmsTaskAnswerId);
        
        /// <summary>
        /// Обновляет вариант ответа
        /// </summary>
        /// <param name="lmsTaskAnswer"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateLmsTaskAnswer(LmsTaskAnswer lmsTaskAnswer, IFormFile uploadedFile);
        
        /// <summary>
        /// Удаляет вариант ответа
        /// </summary>
        /// <param name="lmsTaskAnswerId"></param>
        /// <returns></returns>
        Task RemoveLmsTaskAnswer(int lmsTaskAnswerId);
    }
}
