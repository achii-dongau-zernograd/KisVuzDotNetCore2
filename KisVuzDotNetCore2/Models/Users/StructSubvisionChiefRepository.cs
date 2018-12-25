using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Репозиторий руководителя структурного подразделения
    /// </summary>
    public class StructSubvisionChiefRepository : IStructSubvisionChiefRepository
    {
        AppIdentityDBContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        public StructSubvisionChiefRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Определяет, является ли пользователь руководителем структурного подразделения
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsStructSubvisionChief(string userName)
        {
            bool isStructSubvisionChief = _context.StructSubvisions
                .Include(s=>s.StructSubvisionAccountChief)
                .FirstOrDefault(s=>s.StructSubvisionAccountChief.UserName==userName) != null;
            return isStructSubvisionChief;
        }
    }
}
