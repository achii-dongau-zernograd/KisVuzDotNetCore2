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

        /// <summary>
        /// Возвращает список полных наименований
        /// реализуемых профилей подготовки
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListEduProfileFullNames(int selectedId = 0)
        {
            return new SelectList(_context.EduProfiles.Include(n => n.EduNapravl.EduUgs.EduLevel), "EduProfileId", "GetEduProfileFullName", selectedId);
        }

        /// <summary>
        /// Возвращает список методкомиссий
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListMetodKomissiyaNames(int selectedId = 0)
        {
            return new SelectList(_context.MetodKomissii, "MetodKomissiyaId", "MetodKomissiyaName", selectedId);
        }

        /// <summary>
        /// Возвращает список ФИО преподавателей
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public SelectList GetSelectListTeacherFio(int selectedId = 0)
        {
            return new SelectList(_context.Teachers, "TeacherId", "TeacherFio", selectedId);
        }
    }
}
