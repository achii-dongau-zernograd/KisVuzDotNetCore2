using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Репозиторий заданий СДО
    /// </summary>
    public class LmsTaskRepository : ILmsTaskRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public LmsTaskRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }
        
        /// <summary>
        /// Возвращает запрос на выборку всех заданий СДО
        /// </summary>
        /// <returns></returns>
        public IQueryable<LmsTask> GetLmsTasks()
        {
            var query = _context.LmsTasks
                .Include(t => t.AppUser)
                .Include(t => t.LmsTaskType)
                .Include(t => t.LmsTaskJpg.FileToFileTypes)
                .Include(t => t.LmsTaskDisciplineNames)
                    .ThenInclude(td => td.DisciplineName)
                .Include(t => t.LmsTaskAnswers)
                    .ThenInclude(ta=>ta.FileModel.FileToFileTypes);

            return query;
        }

        /// <summary>
        /// Добавляет новое задание в базу заданий СДО
        /// </summary>
        /// <param name="lmsTask"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task AddLmsTask(LmsTask lmsTask, IFormFile uploadedFile)
        {
            if (lmsTask == null) return;

            if(uploadedFile != null)
            {
                FileModel newFileModel = await _fileModelRepository.UploadLmsTaskJpg(uploadedFile);
                if (newFileModel == null) return;
                lmsTask.LmsTaskJpgId = newFileModel.Id;
            }

            _context.LmsTasks.Add(lmsTask);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает задание СДО
        /// </summary>
        /// <param name="lmsTaskId"></param>
        /// <returns></returns>
        public async Task<LmsTask> GetLmsTask(int lmsTaskId)
        {
            if (lmsTaskId == 0) return null;

            var lmsTask = await GetLmsTasks().FirstOrDefaultAsync(t => t.LmsTaskId == lmsTaskId);

            return lmsTask;
        }

        /// <summary>
        /// Добавляет к заданию СДО дисциплину
        /// </summary>
        /// <param name="lmsTask"></param>
        /// <param name="disciplineNameId"></param>
        /// <returns></returns>
        public async Task AddLmsTaskDisciplineName(LmsTask lmsTask, int disciplineNameId)
        {
            foreach (var lmsTaskDisciplineName in lmsTask.LmsTaskDisciplineNames)
            {
                if (lmsTaskDisciplineName.DisciplineNameId == disciplineNameId) return;
            }

            var newLmsTaskDisciplineName = new LmsTaskDisciplineName
            {
                DisciplineNameId = disciplineNameId
            };
            newLmsTaskDisciplineName.DisciplineName = await _context.DisciplineNames.SingleOrDefaultAsync(dn => dn.DisciplineNameId == disciplineNameId);

            lmsTask.LmsTaskDisciplineNames.Add(newLmsTaskDisciplineName);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавляет к заданию СДО вариант ответа
        /// </summary>
        /// <param name="lmsTaskAnswer"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task AddLmsTaskAnswer(LmsTaskAnswer lmsTaskAnswer, IFormFile uploadedFile)
        {
            if(uploadedFile != null)
            {
                var newFileModel = await _fileModelRepository.UploadLmsTaskAnswerJpg(uploadedFile);

                if(newFileModel != null)
                {
                    lmsTaskAnswer.FileModel = newFileModel;
                    lmsTaskAnswer.FileModelId = newFileModel.Id;
                }
            }

            _context.LmsTaskAnswers.Add(lmsTaskAnswer);
            await _context.SaveChangesAsync();
        }
    }
}
