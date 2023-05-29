using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Репозиторий профилей подготовки
    /// </summary>
    public class EduProfileRepository : IEduProfileRepository
    {
        private readonly AppIdentityDBContext _context;

        public EduProfileRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        IOrderedQueryable<EduProfile> GetEduProfiles => _context.EduProfiles
            .Include(e => e.EduNapravl.EduUgs.EduLevel)                       
            .OrderBy(e => e.EduNapravl.EduNapravlCode);

               

        /// <summary>
        /// Возвращает профиль подготовки по переданному УИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EduProfile> GetEduProfileAsync(int? id)
        {
            return await GetEduProfiles
                .SingleOrDefaultAsync(m => m.EduProfileId == id);
        }

        
    }
}
