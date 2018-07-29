using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models;
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

        public EiosController(AppIdentityDBContext context)
        {
            _context = context;
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
            .ToListAsync();

            ViewData["eduLevels"] = eduLevels;

            return View();
        }
    }
}