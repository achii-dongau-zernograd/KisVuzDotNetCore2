using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Репозиторий образовательных программ
    /// </summary>
    public class EduProgramRepository : IEduProgramRepository
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly AppIdentityDBContext _context;

        public EduProgramRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Определяет, является ли аутентифицированный
        /// пользователь лицом, ответственным за работу
        /// с образовательными программами
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsEduProgramUser(string userName)
        {
            var r = _context.Users
                .Where(u => u.UserName == userName)
                .SingleOrDefaultAsync();
                
            throw new NotImplementedException();
        }
    }
}
