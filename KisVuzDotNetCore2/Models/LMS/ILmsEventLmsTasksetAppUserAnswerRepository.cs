using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Интерфейс репозитория ответов пользователей на наборы заданий СДО
    /// </summary>
    public interface ILmsEventLmsTasksetAppUserAnswerRepository
    {
        /// <summary>
        /// Возвращает вопрос из списка заданий мероприятия, на который пользователь ещё не отвечал
        /// </summary>        
        Task<LmsEventLmsTasksetAppUserAnswer> GetLmsTaskNotAnsweredAsync(string userName, int lmsEventId);

        /// <summary>
        /// Возвращает ответы, которые дал указанный пользователь на набор ответов мероприятия СДО
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="lmsEventLmsTaskSetId"></param>
        /// <returns></returns>
        IQueryable<LmsEventLmsTasksetAppUserAnswer> GetLmsEventLmsTaskSetAppUserAnswers(string userName, int lmsEventLmsTaskSetId);

        /// <summary>
        /// Возвращает запрос на выборку всех ответов пльзователей
        /// </summary>
        /// <returns></returns>
        IQueryable<LmsEventLmsTasksetAppUserAnswer> GetLmsEventLmsTaskSetAppUserAnswers();
        
        /// <summary>
        /// Добавляет ответ пользователя
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="lmsEventId">УИД мероприятия</param>
        /// <param name="lmsEventLmsTasksetAppUserAnswer">Бланк объекта ответа</param>
        /// <param name="choosedAnswers">Идентификаторы выбранных пользователем ответов из списка ответов</param>
        /// <param name="uploadedFile">Загруженный пользователем файл</param>
        /// <returns></returns>
        Task AddAppUserAnswerAsync(string userName, int lmsEventId, LmsEventLmsTasksetAppUserAnswer lmsEventLmsTasksetAppUserAnswer, int[] choosedAnswers, IFormFile uploadedFile);
        
        /// <summary>
        /// Возвращает объект события СДО
        /// </summary>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        Task<LmsEvent> GetLmsEvent(int lmsEventId);

        /// <summary>
        /// Возвращает запрос на выборку всех ответов,
        /// данных пользователем на все вопросы,
        /// заданные на мероприятии СДО
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        IQueryable<LmsEventLmsTasksetAppUserAnswer> GetLmsEventAppUserAnswers(string userName, int lmsEventId);

        /// <summary>
        /// Устанавливает оценку на ответ пользователя и возвращает объект-ответ
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswerId">УИД ответа пользователя</param>
        /// <param name="numberOfPoints">Оценка</param>
        /// <returns></returns>
        Task<LmsEventLmsTasksetAppUserAnswer> SetNumberOfPointsAsync(int lmsEventLmsTasksetAppUserAnswerId, int numberOfPoints);

        /// <summary>
        /// Возвращает запрос на выборку всех заданий мероприятия СДО
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        Task<List<LmsTask>> GetLmsEventTasksAsync(int lmsEventId);

        /// <summary>
        /// Возвращает заполненный объект ответа пользователя по указанному УИД
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswerId"></param>
        /// <returns></returns>
        Task<LmsEventLmsTasksetAppUserAnswer> GetLmsEventAppUserAnswerAsync(int lmsEventLmsTasksetAppUserAnswerId);
        
        /// <summary>
        /// Добавляет ответ пользователя, выбранный из набора вариантов ответов
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswerTaskAnswer"></param>
        /// <returns></returns>
        Task AddLmsEventLmsTasksetAppUserAnswerTaskAnswerAsync(LmsEventLmsTasksetAppUserAnswerTaskAnswer lmsEventLmsTasksetAppUserAnswerTaskAnswer);

        /// <summary>
        /// Возвращает ответ пользователя, выбранный из набора вариантов ответов
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswerTaskAnswerId"></param>
        /// <returns></returns>
        Task<LmsEventLmsTasksetAppUserAnswerTaskAnswer> GetLmsEventLmsTasksetAppUserAnswerTaskAnswerAsync(int lmsEventLmsTasksetAppUserAnswerTaskAnswerId);

        /// <summary>
        /// Удаление ответа пользователя, выбранного из списка
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswerTaskAnswerId"></param>
        /// <returns></returns>
        Task RemoveLmsEventLmsTasksetAppUserAnswerTaskAnswerAsync(int lmsEventLmsTasksetAppUserAnswerTaskAnswerId);
        
        /// <summary>
        /// Обновление ответа пользователя на задание СДО
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswer"></param>
        /// <returns></returns>
        Task UpdateLmsEventLmsTasksetAppUserAnswerAsync(LmsEventLmsTasksetAppUserAnswer lmsEventLmsTasksetAppUserAnswer);
    }
}
