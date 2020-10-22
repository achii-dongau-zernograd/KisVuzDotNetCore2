using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Репозиторий направлений подготовки
    /// </summary>
    public class EduNapravlRepository : IEduNapravlRepository
    {
        private readonly AppIdentityDBContext _context;

        public EduNapravlRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        IOrderedQueryable<EduNapravlEduFormEduSrok> GetEduNapravlEduFormEduSroks => _context.EduNapravlEduFormEduSroks
            .Include(e => e.EduForm)
            .Include(e => e.EduNapravl.EduUgs.EduLevel)
            .Include(e => e.EduNapravl.EduUgs.EduAccred)
            .Include(e => e.EduSrok)            
            .OrderBy(e => e.EduNapravl.EduNapravlCode);

        

        /// <summary>
        /// Возвращает набор записей, связывающих направления подготовки, формы обучения и нормативный срок обучения
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EduNapravlEduFormEduSrok>> GetEduNapravlEduFormEduSroksAsync()
        {            
            return await GetEduNapravlEduFormEduSroks.ToListAsync();
        }

        /// <summary>
        /// Возвращает запись, связывающую направление подготовки, форму обучения и нормативный срок обучения, по переданному УИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EduNapravlEduFormEduSrok> GetEduNapravlEduFormEduSrokAsync(int? id)
        {
            return await GetEduNapravlEduFormEduSroks
                .SingleOrDefaultAsync(m => m.EduNapravlEduFormEduSrokId == id);
        }

        /// <summary>
        /// Удаляет запись, связывающую направление подготовки, форму обучения и нормативный срок обучения
        /// </summary>
        /// <param name="eduNapravlEduFormEduSrok"></param>
        /// <returns></returns>
        public async Task RemoveEduNapravlEduFormEduSrokAsync(EduNapravlEduFormEduSrok eduNapravlEduFormEduSrok)
        {
            _context.EduNapravlEduFormEduSroks.Remove(eduNapravlEduFormEduSrok);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавляет запись, связывающую направление подготовки, форму обучения и нормативный срок обучения
        /// </summary>
        /// <param name="eduNapravlEduFormEduSrok"></param>
        /// <returns></returns>
        public async Task AddEduNapravlEduFormEduSrokAsync(EduNapravlEduFormEduSrok eduNapravlEduFormEduSrok)
        {
            _context.Add(eduNapravlEduFormEduSrok);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет запись, связывающую направление подготовки, форму обучения и нормативный срок обучения
        /// </summary>
        /// <param name="eduNapravlEduFormEduSrok"></param>
        /// <returns></returns>
        public async Task UpdateEduNapravlEduFormEduSrokAsync(EduNapravlEduFormEduSrok eduNapravlEduFormEduSrok)
        {
            _context.Update(eduNapravlEduFormEduSrok);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Определяет, существует ли запись с указанным УИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsEduNapravlEduFormEduSrokExists(int id)
        {
            return _context.EduNapravlEduFormEduSroks.Any(e => e.EduNapravlEduFormEduSrokId == id);
        }
    }
}
