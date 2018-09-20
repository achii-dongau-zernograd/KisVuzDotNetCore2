using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KisVuzDotNetCore2.Infrastructure
{
    /// <summary>
    /// Репозиторий списков (справочников)
    /// </summary>
    public class SelectListRepository : ISelectListRepository
    {
        private readonly AppIdentityDBContext _context;

        public SelectListRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых направлений подготовки
        /// </summary>
        /// <returns></returns>
        public SelectList GetSelectListEduNapravlFullNames(int selectedId = 0)
        {            
            return new SelectList(_context.EduNapravls.Include(n=>n.EduUgs.EduLevel), "EduNapravlId", "GetEduNapravlFullName", selectedId);                       
        }
    }
}
