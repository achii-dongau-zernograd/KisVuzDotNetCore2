using KisVuzDotNetCore2.Models.Education;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Репозиторий методкомиссий
    /// </summary>
    public class MetodKomissiyaRepository : IMetodKomissiyaRepository
    {
        private readonly AppIdentityDBContext _context;

        public MetodKomissiyaRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает набор образовательных программ,
        /// к которым имеет доступ пользователь
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IEnumerable<EduProgram> GetEduProgramsByUserName(string userName)
        {
            // Поиск аккаунта пользователя
            var appUser = _context.Users
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm=>tm.MetodKomissiya)
                            .ThenInclude(m=>m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp=>mp.EduProfile)
                                    .ThenInclude(p=>p.EduPrograms)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile)
                                    .ThenInclude(p => p.EduPrograms)
                                        .ThenInclude(ep=>ep.EduProgramPodg)
                .Include(u => u.Teachers)
                    .ThenInclude(t => t.TeacherMetodKomissii)
                        .ThenInclude(tm => tm.MetodKomissiya)
                            .ThenInclude(m => m.MetodKomissiyaEduProfiles)
                                .ThenInclude(mp => mp.EduProfile.EduNapravl.EduUgs.EduLevel)                                    
                .Where(u => u.UserName == userName)
                .SingleOrDefault();

            var eduPrograms = new List<EduProgram>();

            foreach (var teacher in appUser.Teachers)
            {
                foreach (var teacherMetodKomissiya in teacher.TeacherMetodKomissii)
                {
                    foreach (var metodKomissiyaEduProfiles in teacherMetodKomissiya.MetodKomissiya.MetodKomissiyaEduProfiles)
                    {
                        foreach (var eduProgram in metodKomissiyaEduProfiles.EduProfile.EduPrograms)
                        {
                            eduPrograms.Add(eduProgram);
                        }
                    }
                }
            }

            return eduPrograms;
        }

        /// <summary>
        /// Определяет, является ли аутентифицированный
        /// пользователь членом методкомиссии
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsMetodKomissiyaMember(string userName)
        {
            // Поиск аккаунта пользователя
            var appUser = _context.Users
                .Include(u=>u.Teachers)
                    .ThenInclude(t=>t.TeacherMetodKomissii)
                .Where(u => u.UserName == userName)
                .SingleOrDefault();
            if (appUser == null) return false;

            foreach (var teacher in appUser.Teachers)
            {
                if(teacher.TeacherMetodKomissii.Any())
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
