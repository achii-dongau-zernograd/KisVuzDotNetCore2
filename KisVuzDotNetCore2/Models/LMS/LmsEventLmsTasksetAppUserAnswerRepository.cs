using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Репозиторий ответов пользователей на наборы заданий СДО
    /// </summary>
    public class LmsEventLmsTasksetAppUserAnswerRepository : ILmsEventLmsTasksetAppUserAnswerRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly ILmsEventRepository _lmsEventRepository;
        private readonly ILmsTaskSetRepository _lmsTaskSetRepository;
        private readonly IFileModelRepository _fileModelRepository;

        public LmsEventLmsTasksetAppUserAnswerRepository(AppIdentityDBContext context,
            ILmsEventRepository lmsEventRepository,
            ILmsTaskSetRepository lmsTaskSetRepository,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _lmsEventRepository = lmsEventRepository;
            _lmsTaskSetRepository = lmsTaskSetRepository;
            _fileModelRepository = fileModelRepository;
        }

        public IQueryable<LmsEventLmsTasksetAppUserAnswer> GetLmsEventLmsTaskSetAppUserAnswers()
        {
            var query = _context.LmsEventLmsTasksetsAppUserAnswers
                .Include(a => a.AppUser)
                .Include(a => a.AnswerAsFile.FileToFileTypes)
                .Include(a => a.LmsEventLmsTasksetAppUserAnswerTaskAnswers)
                    .ThenInclude(ta => ta.LmsTaskAnswer.FileModel.FileToFileTypes)
                .Include(a => a.LmsTask.LmsTaskType)
                .Include(a => a.LmsTask.LmsTaskJpg.FileToFileTypes)
                .Include(a => a.LmsTask.LmsTaskAnswers)
                    .ThenInclude(ta => ta.FileModel.FileToFileTypes);

            return query;
        }


        /// <summary>
        /// Возвращает вопрос из списка заданий мероприятия, на который пользователь ещё не отвечал
        /// </summary>        
        public async Task<LmsEventLmsTasksetAppUserAnswer> GetLmsTaskNotAnsweredAsync(string userName, int lmsEventId)
        {
            // Получаем мероприятие
            var lmsEvent = await _lmsEventRepository.GetLmsEventAsync(lmsEventId);
            if (lmsEvent == null) return null;

            // Удостоверяемся, что пользователь является участником события
            var appUser = lmsEvent.AppUserLmsEvents
                .FirstOrDefault(ae => ae.AppUser.UserName == userName &&
                           ae.AppUserLmsEventUserRoleId == (int)AppUserLmsEventUserRolesEnum.Participant)
                .AppUser;
            if (appUser == null) return null;

            //if (! lmsEvent.AppUserLmsEvents
            //    .Any(ae => ae.AppUser.UserName == userName &&
            //               ae.AppUserLmsEventUserRoleId == (int)AppUserLmsEventUserRolesEnum.Participant))
            //    return null;

            // Удостоверяемся, что мероприятие проходит в данный момент
            if (!lmsEvent.IsLmsEventStarted)
                return null;

            foreach (var lmsEventLmsTaskSet in lmsEvent.LmsEventLmsTaskSets)
            {
                if(lmsEventLmsTaskSet.LmsTaskSetId != 0 && lmsEventLmsTaskSet.LmsTaskSet.LmsTaskSetLmsTasks == null)
                {
                    // Загружаем набор заданий
                    var lmsTaskSet = await _lmsTaskSetRepository.GetLmsTaskSetAsync(lmsEventLmsTaskSet.LmsTaskSetId);
                    lmsEventLmsTaskSet.LmsTaskSet = lmsTaskSet;
                }

                // Загружаем уже полученные ответы пользователя на вопросы данного мероприятия
                var appUserAnswers = await GetLmsEventLmsTaskSetAppUserAnswers(userName, lmsEventLmsTaskSet.LmsEventLmsTaskSetId).ToListAsync();

                foreach (var lmsTaskSetLmsTask in lmsEventLmsTaskSet.LmsTaskSet.LmsTaskSetLmsTasks)
                {

                    // Проверяем, отвечал ли уже пользователь на данный вопрос
                    bool isAppUserAnswered = false;
                    foreach (var appUserAnswer in appUserAnswers)
                    {
                        if (appUserAnswer.LmsTaskId == lmsTaskSetLmsTask.LmsTaskId)
                        {
                            isAppUserAnswered = true;
                            break;
                        }
                    }

                    if(! isAppUserAnswered)
                    {
                        // Если мы до сюда дошли, значит пользователь не отвечал на текущий вопрос
                        // Формируем объект будущего ответа для передачи в представление задания
                        var newAppUserAnswer = new LmsEventLmsTasksetAppUserAnswer
                        {
                            AppUserId = appUser.Id,
                            LmsEventLmsTaskSetId = lmsEventLmsTaskSet.LmsEventLmsTaskSetId,
                            LmsTaskId = lmsTaskSetLmsTask.LmsTaskId,
                            LmsTask = lmsTaskSetLmsTask.LmsTask
                        };

                        return newAppUserAnswer;
                    }                    
                }
            }

            return null;
        }

        /// <summary>
        /// Возвращает ответы, которые дал указанный пользователь на набор ответов мероприятия СДО
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="lmsEventLmsTaskSetId"></param>
        /// <returns></returns>
        public IQueryable<LmsEventLmsTasksetAppUserAnswer> GetLmsEventLmsTaskSetAppUserAnswers(string userName, int lmsEventLmsTaskSetId)
        {
            var answers = GetLmsEventLmsTaskSetAppUserAnswers()
               .Where(a => a.AppUser.UserName == userName &&
                           a.LmsEventLmsTaskSetId == lmsEventLmsTaskSetId);

            return answers;
        }

        /// <summary>
        /// Добавляет ответ пользователя
        /// </summary>        
        /// <param name="lmsEventLmsTasksetAppUserAnswer">Бланк объекта ответа</param>
        /// <param name="choosedAnswers">Идентификаторы выбранных пользователем ответов из списка ответов</param>
        /// <param name="uploadedFile">Загруженный пользователем файл</param>
        /// <returns></returns>
        public async Task AddAppUserAnswerAsync(LmsEventLmsTasksetAppUserAnswer lmsEventLmsTasksetAppUserAnswer,
            int[] choosedAnswers,
            IFormFile uploadedFile)
        {
            
            // Обрабатываем ответы пользователя на тестовые задания и текстовый ввод
            if( (choosedAnswers != null && choosedAnswers.Count() > 0) ||
                ! string.IsNullOrWhiteSpace(lmsEventLmsTasksetAppUserAnswer.AnswerAsText) )
            {
                // Загружаем задание с вариантами ответов
                var lmsTask = await _lmsTaskSetRepository.GetLmsTaskAsync(lmsEventLmsTasksetAppUserAnswer.LmsTaskId);
                if(lmsTask != null && lmsTask.LmsTaskAnswers != null && lmsTask.LmsTaskAnswers.Count > 0)
                {
                    int numberOfPoints = 0;
                    var correctTaskAnswers = lmsTask.LmsTaskAnswers.Where(ta => ta.IsCorrect);

                    // Обрабатываем случаи, когда пользователь выбирал ответы
                    if(choosedAnswers != null && choosedAnswers.Count() > 0 && correctTaskAnswers.Count() == choosedAnswers.Count())
                    {
                        bool isAllCorrectAnswersChoosed = true;
                        foreach (var correctTaskAnswer in correctTaskAnswers)
                        {
                            if(! choosedAnswers.Contains(correctTaskAnswer.LmsTaskAnswerId))
                            {
                                isAllCorrectAnswersChoosed = false;
                                break;
                            }
                        }

                        // Если все ответы правильные, назначаем балл по значению из настроек задания
                        if (isAllCorrectAnswersChoosed)
                            numberOfPoints = lmsTask.NumberOfPoints;

                        lmsEventLmsTasksetAppUserAnswer.NumberOfPoints = numberOfPoints;
                    }

                    // Обрабатываем случай, когда пользователь вводил ответ в текстовое поле
                    if (!string.IsNullOrWhiteSpace(lmsEventLmsTasksetAppUserAnswer.AnswerAsText) && correctTaskAnswers.Count() > 0)
                    {
                        // Приводим строки к общему виду и сравниваем
                        string sUserAnswer = lmsEventLmsTasksetAppUserAnswer.AnswerAsText.Trim().ToLower().Replace(',','.');                        

                        foreach (var correctTaskAnswer in correctTaskAnswers)
                        {
                            string sCorrectAnswer = correctTaskAnswer.LmsTaskAnswerText.Trim().ToLower().Replace(',', '.');

                            if(sUserAnswer == sCorrectAnswer)
                            {
                                lmsEventLmsTasksetAppUserAnswer.NumberOfPoints = lmsTask.NumberOfPoints;
                            }

                            // Обработка вопроса типа "ввод числа"
                            if(lmsTask.LmsTaskTypeId == (int)LmsTaskTypesEnum.InputNumber)
                            {
                                double dUserAnswer = 0;
                                double dCorrectAnswer = 0;
                                bool isUserAnswerParsed    = double.TryParse(sUserAnswer,    out dUserAnswer);
                                bool isCorrectAnswerParsed = double.TryParse(sCorrectAnswer, out dCorrectAnswer);
                                if(isUserAnswerParsed && isCorrectAnswerParsed)
                                {
                                    double stdErr = (dUserAnswer - dCorrectAnswer) * 100 / dCorrectAnswer;
                                    if(stdErr < 0.5) // Допустимое отклонение - 0.5%
                                    {
                                        lmsEventLmsTasksetAppUserAnswer.NumberOfPoints = lmsTask.NumberOfPoints;
                                    }
                                    else
                                    {
                                        lmsEventLmsTasksetAppUserAnswer.NumberOfPoints = 0;
                                    }
                                }
                            }
                        }
                    }    

                }


                // Сохраняем ответы пользователя
                if (lmsEventLmsTasksetAppUserAnswer.LmsEventLmsTasksetAppUserAnswerTaskAnswers == null)
                    lmsEventLmsTasksetAppUserAnswer.LmsEventLmsTasksetAppUserAnswerTaskAnswers = new List<LmsEventLmsTasksetAppUserAnswerTaskAnswer>();
                foreach (var choosedAnswer in choosedAnswers)
                {
                    var newChoosedAnswerRecord = new LmsEventLmsTasksetAppUserAnswerTaskAnswer
                    {
                        LmsTaskAnswerId = choosedAnswer
                    };
                    lmsEventLmsTasksetAppUserAnswer.LmsEventLmsTasksetAppUserAnswerTaskAnswers.Add(newChoosedAnswerRecord);
                }
            }

            // Сохраняем ответ пользователя в виде скан-копии решения
            if (uploadedFile != null)
            {
                var newFileModel = await _fileModelRepository.UploadLmsAppUserAnswer(uploadedFile);
                if (newFileModel != null)
                {
                    lmsEventLmsTasksetAppUserAnswer.AnswerAsFile = newFileModel;
                    lmsEventLmsTasksetAppUserAnswer.AnswerAsFileId = newFileModel.Id;
                }
            }                       

            _context.LmsEventLmsTasksetsAppUserAnswers.Add(lmsEventLmsTasksetAppUserAnswer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавляет ответ пользователя
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="lmsEventId">УИД мероприятия</param>
        /// <param name="lmsEventLmsTasksetAppUserAnswer">Бланк объекта ответа</param>
        /// <param name="choosedAnswers">Идентификаторы выбранных пользователем ответов из списка ответов</param>
        /// <param name="uploadedFile">Загруженный пользователем файл</param>
        /// <returns></returns>
        public async Task AddAppUserAnswerAsync(string userName,
            int lmsEventId,
            LmsEventLmsTasksetAppUserAnswer lmsEventLmsTasksetAppUserAnswer,
            int[] choosedAnswers,
            IFormFile uploadedFile)
        {
            var lmsEvent = await _lmsEventRepository.GetLmsEventAsync(lmsEventId);

            var appUser = lmsEvent.AppUserLmsEvents
                .FirstOrDefault(ae => ae.AppUser.UserName == userName &&
                           ae.AppUserLmsEventUserRoleId == (int)AppUserLmsEventUserRolesEnum.Participant)
                .AppUser;
            if (appUser == null) return;

            if (lmsEventLmsTasksetAppUserAnswer.AppUserId != appUser.Id)
                return;
                        

            await AddAppUserAnswerAsync(lmsEventLmsTasksetAppUserAnswer, choosedAnswers, uploadedFile);
        }

        /// <summary>
        /// Возвращает запрос на выборку всех ответов,
        /// данных пользователем на все вопросы,
        /// заданные на мероприятии СДО
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        public IQueryable<LmsEventLmsTasksetAppUserAnswer> GetLmsEventAppUserAnswers(string userName, int lmsEventId)
        {
            var query = GetLmsEventLmsTaskSetAppUserAnswers()
                .Where(a => a.AppUser.UserName == userName && a.LmsEventLmsTaskSet.LmsEventId == lmsEventId);

            return query;
        }

        /// <summary>
        /// Возвращает все задания мероприятия СДО
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        public async Task<List<LmsTask>> GetLmsEventTasksAsync(int lmsEventId)
        {
            var lmsTasks = await _lmsEventRepository.GetLmsEventTasks(lmsEventId);
            return lmsTasks;
        }

        /// <summary>
        /// Устанавливает оценку на ответ пользователя и возвращает объект-ответ
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswerId"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public async Task<LmsEventLmsTasksetAppUserAnswer> SetNumberOfPointsAsync(int lmsEventLmsTasksetAppUserAnswerId, int numberOfPoints)
        {
            var answer = await GetLmsEventAppUserAnswerAsync(lmsEventLmsTasksetAppUserAnswerId);
            answer.NumberOfPoints = numberOfPoints;

            await _context.SaveChangesAsync();

            return answer;
        }

        /// <summary>
        /// Возвращает заполненный объект ответа пользователя по указанному УИД
        /// </summary>
        /// <param name="lmsEventLmsTasksetAppUserAnswerId"></param>
        /// <returns></returns>
        public async Task<LmsEventLmsTasksetAppUserAnswer> GetLmsEventAppUserAnswerAsync(int lmsEventLmsTasksetAppUserAnswerId)
        {
            var answer = await GetLmsEventLmsTaskSetAppUserAnswers()
                .SingleOrDefaultAsync(a => a.LmsEventLmsTasksetAppUserAnswerId == lmsEventLmsTasksetAppUserAnswerId);

            return answer;
        }
    }
}
