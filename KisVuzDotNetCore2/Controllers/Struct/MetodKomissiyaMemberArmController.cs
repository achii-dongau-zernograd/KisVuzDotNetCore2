using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Struct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KisVuzDotNetCore2.Controllers.Struct
{
    /// <summary>
    /// Контроллер АРМ члена методкомиссии
    /// </summary>
    public class MetodKomissiyaMemberArmController : Controller
    {
        private readonly AppIdentityDBContext _context;
        private readonly IMetodKomissiyaRepository _metodKomissiyaRepository;
        private readonly ISelectListRepository _selectListRepository;

        public MetodKomissiyaMemberArmController(AppIdentityDBContext context,
            IMetodKomissiyaRepository metodKomissiyaRepository,
            ISelectListRepository selectListRepository)
        {
            _context = context;
            _metodKomissiyaRepository = metodKomissiyaRepository;
            _selectListRepository = selectListRepository;
        }

        /// <summary>
        /// Список образовательны программ, доступных пользователю
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> EduPrograms()
        {
            var eduPrograms = await _metodKomissiyaRepository.GetEduProgramsByUserNameAsync(User.Identity.Name);
            return View(eduPrograms);
        }

        /// <summary>
        /// Создание образовательной программы, доступной методкомиссии
        /// </summary>
        /// <param name="EduProgramId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EduProgramCreate()
        {            
            ViewBag.EduForms = _context.EduForms;
            ViewBag.EduYears = _context.EduYears;
            IEnumerable<MetodKomissiya> metodKomissiiOfUser = await _metodKomissiyaRepository.GetMetodKomissiiByUserNameAsync(User.Identity.Name);
            ViewData["EduProfileId"] = _selectListRepository.GetSelectListEduProfileFullNamesOfMethodicalCommission(metodKomissiiOfUser);
            ViewData["EduProgramPodgId"] = _selectListRepository.GetSelectListEduProgramPodg();

            return View("EduProgramEdit", new EduProgram() );
        }

        /// <summary>
        /// Редактирование образовательной программы, доступной пользователю
        /// </summary>
        /// <param name="EduProgramId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EduProgramEdit(int EduProgramId)
        {
            var eduProgram = await _metodKomissiyaRepository.GetEduProgramByUserNameAsync(EduProgramId, User.Identity.Name);
            ViewBag.EduForms = _context.EduForms;
            ViewBag.EduYears = _context.EduYears;
            IEnumerable<MetodKomissiya> metodKomissiiOfUser = await _metodKomissiyaRepository.GetMetodKomissiiByUserNameAsync(User.Identity.Name);
            ViewData["EduProfileId"] = _selectListRepository.GetSelectListEduProfileFullNamesOfMethodicalCommission(metodKomissiiOfUser, eduProgram.EduProfileId);
            ViewData["EduProgramPodgId"] = _selectListRepository.GetSelectListEduProgramPodg(eduProgram.EduProgramPodgId);

            return View(eduProgram);
        }

        // POST: EduPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EduProgramEdit([Bind("EduProgramId,EduProfileId,EduProgramPodgId,IsAdopt,UsingElAndDistEduTech,FileModelId,DateUtverjd")] EduProgram eduProgram,
            IFormFile uploadedFile,
            int[] eduFormIds,
            int[] eduYearIds)
        {
            if (ModelState.IsValid)
            {
                await _metodKomissiyaRepository.UpdateEduProgramByUserNameAsync(User.Identity.Name, eduProgram, uploadedFile, eduFormIds, eduYearIds);                                

                return RedirectToAction(nameof(EduPrograms));
            }
            ViewData["EduProfileId"] = _selectListRepository.GetSelectListEduProfileFullNames(eduProgram.EduProfileId);
            ViewData["EduProgramPodgId"] = _selectListRepository.GetSelectListEduProgramPodg(eduProgram.EduProgramPodgId);

            return View(eduProgram);
        }


        // GET: EduPrograms/Delete/5
        public async Task<IActionResult> EduProgramDelete(int EduProgramId)
        {
            var eduProgram = await _metodKomissiyaRepository.GetEduProgramByUserNameAsync(EduProgramId, User.Identity.Name);
            if (eduProgram == null)
            {
                return NotFound();
            }            

            return View(eduProgram);
        }

        // POST: EduPrograms/Delete/5
        [HttpPost, ActionName("EduProgramDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EduProgramDeleteConfirmed(int EduProgramId)
        {
            var eduProgram = await _metodKomissiyaRepository.GetEduProgramByUserNameAsync(EduProgramId, User.Identity.Name);

            if(eduProgram!=null)
            {
                await _metodKomissiyaRepository.RemoveEduProgramByUserNameAsync(eduProgram, User.Identity.Name);
            }                       
            
            return RedirectToAction(nameof(EduPrograms));
        }

        private bool EduProgramExists(int id)
        {
            return _context.EduPrograms.Any(e => e.EduProgramId == id);
        }
    }
}