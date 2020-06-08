using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Репозиторий наборов вопросов
    /// </summary>
    public class LmsTaskSetRepository : ILmsTaskSetRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly ILmsTaskRepository _lmsTaskRepository;

        public LmsTaskSetRepository(AppIdentityDBContext context,
            ILmsTaskRepository lmsTaskRepository)
        {
            _context = context;
            _lmsTaskRepository = lmsTaskRepository;
        }
               

        /// <summary>
        /// Возвращает наборы заданий
        /// </summary>
        /// <returns></returns>
        public IQueryable<LmsTaskSet> GetLmsTaskSets()
        {
            var lmsTaskSets = _context.LmsTaskSets
                .Include(s => s.LmsEventLmsTaskSets)
                    .ThenInclude(es => es.LmsEvent.LmsEventType)
                .Include(s => s.LmsTaskSetLmsTasks)
                    .ThenInclude(st => st.LmsTask.LmsTaskType)
                .Include(s => s.LmsTaskSetLmsTasks)
                    .ThenInclude(st => st.LmsTask.LmsTaskJpg.FileToFileTypes)
                .Include(s => s.LmsTaskSetLmsTasks)
                    .ThenInclude(st => st.LmsTask)
                        .ThenInclude(t => t.LmsTaskDisciplineNames)
                            .ThenInclude(td => td.DisciplineName)
                .Include(s => s.LmsTaskSetLmsTasks)
                    .ThenInclude(st => st.LmsTask)
                        .ThenInclude(t=>t.LmsTaskAnswers)
                            .ThenInclude(ta=>ta.FileModel.FileToFileTypes);

            return lmsTaskSets;
        }

        /// <summary>
        /// Добавляет набор заданий
        /// </summary>
        /// <param name="lmsTaskSet"></param>
        /// <returns></returns>
        public async Task AddLmsTaskSet(LmsTaskSet lmsTaskSet)
        {
            if (lmsTaskSet == null) return;

            _context.LmsTaskSets.Add(lmsTaskSet);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает набор заданий
        /// </summary>
        /// <param name="lmsTaskSetId"></param>
        /// <returns></returns>
        public async Task<LmsTaskSet> GetLmsTaskSetAsync(int lmsTaskSetId)
        {
            var lmsTaskSet = await GetLmsTaskSets().SingleOrDefaultAsync(ts => ts.LmsTaskSetId == lmsTaskSetId);

            return lmsTaskSet;
        }

        /// <summary>
        /// Добавляет задание в набор
        /// </summary>
        /// <param name="lmsTaskSetLmsTask"></param>
        /// <returns></returns>
        public async Task AddLmsTaskSetLmsTaskAsync(LmsTaskSetLmsTask lmsTaskSetLmsTask)
        {
            var entries = _context.LmsTaskSetLmsTasks.Where(t => t.LmsTaskId == lmsTaskSetLmsTask.LmsTaskId && t.LmsTaskSetId == lmsTaskSetLmsTask.LmsTaskSetId);

            if(entries == null || entries.Count() == 0)
            {
                _context.LmsTaskSetLmsTasks.Add(lmsTaskSetLmsTask);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Возвращает задание
        /// </summary>
        /// <param name="lmsTaskId"></param>
        /// <returns></returns>
        public async Task<LmsTask> GetLmsTaskAsync(int lmsTaskId)
        {
            var lmsTask = await _lmsTaskRepository.GetLmsTaskAsync(lmsTaskId);
            return lmsTask;
        }
    }
}
