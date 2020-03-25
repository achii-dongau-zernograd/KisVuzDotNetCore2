using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.UchPosobiya;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KisVuzDotNetCore2.Controllers.Eios
{
    /// <summary>
    /// Электронная информационно-образовательная среда (контроллер)
    /// </summary>
    public class EiosController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IUchPosobiyaRepository _uchPosobiyaRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public EiosController(AppIdentityDBContext context,
            IUchPosobiyaRepository uchPosobiyaRepository,
            IUserProfileRepository userProfileRepository)
        {
            _context = context;
            _uchPosobiyaRepository = uchPosobiyaRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<IActionResult> UchPlansRabProgs()
        {
            var eduLevels = await _context.EduLevels
                     .Include(l => l.EduUgses)
                        .ThenInclude(u => u.EduNapravls)
                            .ThenInclude(n => n.EduProfiles)
                                .ThenInclude(p => p.EduPlans)
                                    .ThenInclude(plan => plan.EduPlanEduYearBeginningTrainings)
                                        .ThenInclude(year => year.EduYearBeginningTraining)
                     .Include(l => l.EduUgses)
                        .ThenInclude(u => u.EduNapravls)
                            .ThenInclude(n => n.EduProfiles)
                                .ThenInclude(p => p.EduPlans)
                                    .ThenInclude(plan => plan.EduPlanPdf)
                     .Include(l => l.EduUgses)
                        .ThenInclude(u => u.EduNapravls)
                            .ThenInclude(n => n.EduProfiles)
                                .ThenInclude(p => p.EduPlans)
                                    .ThenInclude(plan => plan.EduForm)
                     .ToListAsync();

            ViewData["eduLevels"] = eduLevels;

            return View();
        }

        /// <summary>
        /// Портфолио студентов
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Portfolio()
        {
            var eduLevels = await _context.EduLevels
            .Include(l => l.EduUgses)
                .ThenInclude(u => u.EduNapravls)
                    .ThenInclude(n => n.EduProfiles)
                        .ThenInclude(p=>p.StudentGroups)
                            .ThenInclude(g=>g.Students)
                                .ThenInclude(s=>s.AppUser)
            .Include(l => l.EduUgses)
                .ThenInclude(u => u.EduNapravls)
                    .ThenInclude(n => n.EduProfiles)
                        .ThenInclude(p => p.StudentGroups)
                            .ThenInclude(g=>g.EduKurs)
            .Include(l => l.EduUgses)
                .ThenInclude(u => u.EduNapravls)
                    .ThenInclude(n => n.EduProfiles)
                        .ThenInclude(p => p.StudentGroups)
                            .ThenInclude(g => g.Kurator)                                
            .ToListAsync();

            ViewData["eduLevels"] = eduLevels;

            bool isTeacher = await _userProfileRepository.IsTeacherAsync(User.Identity.Name);
            ViewBag.IsTeacher = isTeacher;

            return View();
        }

        /// <summary>
        /// Портфолио преподавателей
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PortfolioPps()
        {
            var structFacultets = await _context.StructFacultets
                .Include(sf => sf.StructSubvision)
                .Include(sf => sf.StructKafs)
                    .ThenInclude(sk => sk.TeacherStructKafPostStavka)
                        .ThenInclude(tsk => tsk.Post)
                .Include(sf => sf.StructKafs)
                    .ThenInclude(sk => sk.TeacherStructKafPostStavka)
                        .ThenInclude(tsk => tsk.Teacher.AppUser)
                .Include(sf => sf.StructKafs)
                    .ThenInclude(sk => sk.StructSubvision)                        
            .ToListAsync();

            return View(structFacultets);
        }

        /// <summary>
        /// Каталог учебных пособий, изданных сотрудниками вуза
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UchPosobiesSobstv()
        {            
            var uchPos = await _uchPosobiyaRepository.GetUchPosobiyaAsync(null);
            return View(uchPos);
        }
    }
}