﻿using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers
{
    /// <summary>
    /// Контроллер, реализующий отображение раздела
    /// "Сведеия об образовательной организации"
    /// </summary>
    public class Sveden : Controller
    {
        IHostingEnvironment _appEnvironment;
        private readonly AppIdentityDBContext _context;

        public Sveden(IHostingEnvironment appEnvironment, AppIdentityDBContext context)
        {
            _appEnvironment = appEnvironment;
            _context = context;
        }

        /// <summary>
        /// Подраздел "Основные сведения"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Common()
        {
            #region Таблица 2 "Основные сведения"            
            StructInstitute institute = _context
                .StructInstitutes
                .Include(a=>a.Address)
                .Include(a=>a.Telephones)
                .Include(a => a.Faxes)
                .Include(a => a.Emailes)
                .SingleOrDefault(i=>i.StructInstituteId==1);
            if(institute==null)
            {
                return NotFound();
            }

            ViewData["DateOfCreation"] = String.Format("{0:dd.MM.yyyy}", institute.DateOfCreation);
            ViewData["Address"] = institute.Address.GetAddress;
            ViewData["ExistenceOfFilials"] = institute.ExistenceOfFilials;
            ViewData["WorkingRegime"] = institute.WorkingRegime;
            ViewData["WorkingSchedule"] = institute.WorkingSchedule;
            ViewData["Telephones"] = institute.Telephones;
            ViewData["Faxes"] = institute.Faxes;
            ViewData["Emailes"] = institute.Emailes;

            #endregion                       

            return View();
        }

        /// <summary>
        /// Подраздел "Структура и органы управления
        /// образовательной организацией"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Struct()
        {
            List<StructSubvisionType> structSubvisionTypes = await _context
                .StructSubvisionTypes
                .ToListAsync();
            List<StructSubvision> structSubvisions = await _context
                .StructSubvisions
                .Include(a => a.StructSubvisionAdress)
                .Include(a => a.StructSubvisionEmail)
                .Include(a => a.StructStandingOrder)
                .Include(a => a.StructSubvisionType)
                .Include(a => a.StructSubvisionPostChief)
                .ToListAsync();
            ViewData["StructSubvisions"] = structSubvisions;
            ViewData["StructSubvisionTypes"] = structSubvisionTypes;
            return View();
        }

        /// <summary>
        /// Подраздел "Документы"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Document()
        {
            var docs = await _context.FileDataTypeGroups.Where(g => g.FileDataTypeGroupName == "Сведения об образовательной организации - Документы")
                .Include(g => g.FileDataTypes)
                    .ThenInclude(fdt => fdt.FileToFileTypes)
                        .ThenInclude(ftft=>ftft.FileModel)
                .FirstOrDefaultAsync();
                //.ToListAsync();
            return View(docs);
        }

        /// <summary>
        /// Подраздел "Образование"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Education()
        {
            var t6eduAccred = await _context.EduNapravls
                .Include(l => l.EduUgs)
                    .ThenInclude(ugs => ugs.EduAccred)
                .Include(l => l.EduUgs)
                    .ThenInclude(ugs => ugs.EduLevel)                
                .ToListAsync();
            ViewData["t6eduAccred"] = t6eduAccred;

            var t7eduChislen = await _context.EduChislens
                .Include(c => c.EduProfile)
                    .ThenInclude(n => n.EduNapravl)
                    .ThenInclude(u => u.EduUgs)
                    .ThenInclude(l => l.EduLevel)
                .Include (f =>f.EduForm)
                .ToListAsync();
            ViewData["t7eduChislen"] = t7eduChislen;              

            return View();
        }

        /// <summary>
        /// Подраздел "Образовательные стандарты"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> EduStandarts()
        {
            List<EduLevel> eduLevelsNapravls = await _context.EduLevels
                .Include(l => l.EduUgses)
                    .ThenInclude(u => u.EduNapravls)
                    .ToListAsync();
            return View(eduLevelsNapravls);
        }

        /// <summary>
        /// Подраздел "Руководство. Педагогический
        /// (научно-педагогический) состав"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Employees()
        {
            return View();
        }

        /// <summary>
        /// Подраздел "Материально-техническое обеспечение
        /// и оснащенность образовательного процесса"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Objects()
        {
            return View();
        }

        /// <summary>
        /// Подраздел "Стипендии и иные виды материальной поддержки"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Grants()
        {
            return View();
        }

        /// <summary>
        /// Подраздел "Платные образовательные услуги"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Paid_edu()
        {
            return View();
        }

        /// <summary>
        /// Подраздел "Финансово-хозяйственная деятельность"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Budget()
        {
            return View();
        }

        /// <summary>
        /// Подраздел "Вакантные места для приёма (перевода)"
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Vacant()
        {
            return View();
        }
    }
}