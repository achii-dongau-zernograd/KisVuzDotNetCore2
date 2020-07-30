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
        /// Возвращает запрос на выборку заданий СДО,
        /// удовлетворяющих фильтру и соответственно отсортированных
        /// </summary>
        /// <param name="lmsTasksFilterAndSortModel"></param>
        /// <returns></returns>
        public IQueryable<LmsTask> GetLmsTasks(LmsTasksFilterAndSortModel lmsTasksFilterAndSortModel)
        {
            var query = GetLmsTasks();

            if (!string.IsNullOrWhiteSpace(lmsTasksFilterAndSortModel.FilterLmsTaskId))
            {
                string filterLmsTaskId = lmsTasksFilterAndSortModel.FilterLmsTaskId.Trim();

                //var items = filterLmsTaskId.Split(',', ';'); -- Не реализовано правило выборки

                int lmsTaskId = 0;

                try
                {
                    lmsTaskId = Convert.ToInt32(filterLmsTaskId);
                }
                catch(Exception exc)
                {
                    lmsTaskId = 0;
                    lmsTasksFilterAndSortModel.FilterLmsTaskId = "";
                }

                if (lmsTaskId != 0)
                    query = query.Where(t => t.LmsTaskId == lmsTaskId);
            }

            if (!string.IsNullOrWhiteSpace(lmsTasksFilterAndSortModel.FilterAppUserId))
                query = query.Where(t => t.AppUserId == lmsTasksFilterAndSortModel.FilterAppUserId);

            if (lmsTasksFilterAndSortModel.FilterDisciplineNameId != 0)
                query = query.Where(t => t.LmsTaskDisciplineNames.Any(td => td.DisciplineNameId == lmsTasksFilterAndSortModel.FilterDisciplineNameId));

            if (lmsTasksFilterAndSortModel.FilterLmsTaskTypeId != 0)
                query = query.Where(t => t.LmsTaskTypeId == lmsTasksFilterAndSortModel.FilterLmsTaskTypeId);

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
        public async Task<LmsTask> GetLmsTaskAsync(int lmsTaskId)
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

        /// <summary>
        /// Обновляет задание СДО
        /// </summary>
        /// <param name="lmsTask"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateLmsTaskAsync(LmsTask lmsTask, IFormFile uploadedFile)
        {
            if (lmsTask == null) return;

            if (uploadedFile != null)
            {
                if(lmsTask.LmsTaskJpgId != null)
                {
                    if(lmsTask.LmsTaskJpg == null)
                    {
                        lmsTask.LmsTaskJpg = await _fileModelRepository.GetFileModelAsync(lmsTask.LmsTaskJpgId);
                    }

                    await _fileModelRepository.ReloadFileAsync(lmsTask.LmsTaskJpg, uploadedFile);
                }
                else
                {
                    FileModel newFileModel = await _fileModelRepository.UploadLmsTaskJpg(uploadedFile);
                    if (newFileModel == null) return;
                    lmsTask.LmsTaskJpgId = newFileModel.Id;
                }
                
            }

            _context.LmsTasks.Update(lmsTask);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет задание СДО
        /// </summary>
        /// <param name="lmsTask"></param>
        /// <returns></returns>
        public async Task RemoveLmsTaskAsync(LmsTask lmsTask)
        {
            if(lmsTask.LmsTaskAnswers != null)
            {
                foreach (var lmsTaskAnswer in lmsTask.LmsTaskAnswers)
                {
                    if (lmsTaskAnswer.FileModelId != null)
                    {
                        if (lmsTaskAnswer.FileModel != null)
                        {
                            await _fileModelRepository.RemoveFileModelAsync(lmsTaskAnswer.FileModel);
                        }
                        else
                        {
                            await _fileModelRepository.RemoveFileAsync(lmsTaskAnswer.FileModelId);
                        }
                    }
                }
            }


            if(lmsTask.LmsTaskJpgId != null)
            {
                if(lmsTask.LmsTaskJpg != null)
                {
                    await _fileModelRepository.RemoveFileModelAsync(lmsTask.LmsTaskJpg);
                }
                else
                {
                    await _fileModelRepository.RemoveFileAsync(lmsTask.LmsTaskJpgId);
                }
            }
            
            _context.LmsTasks.Remove(lmsTask);
            await _context.SaveChangesAsync();
        }
        
    }
}
