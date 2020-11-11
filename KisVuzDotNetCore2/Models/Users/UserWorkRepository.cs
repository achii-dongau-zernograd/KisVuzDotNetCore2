using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Репозиторий работ пользователей
    /// </summary>
    public class UserWorkRepository : IUserWorkRepository
    {
        private readonly AppIdentityDBContext _context;

        public UserWorkRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает количество работ пользователей, загруженное до указанной даты
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public async Task<int> GetNumUserWorksUploadedToDate(DateTime dateTime)
        {
            var num = _context.UserWorks
                .Include(uw => uw.FileModel)
                .Where(uw => uw.FileModelId != null)
                .Where(uw => uw.FileModel.UploadDate <= dateTime)
                .Count();

            return num;
        }
    }
}
