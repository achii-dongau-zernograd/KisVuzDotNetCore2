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
        /// Учебный план
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EduPlanPreview(int id)
        {
            var eduPlan = await _metodKomissiyaRepository.GetEduPlanByUserNameAsync(id, User.Identity.Name);
            return View(eduPlan);
        }

        /// <summary>
        /// Формирует базовую структуру учебного плана
        /// (если она ещё не создана)
        /// </summary>
        /// <param name="EduPlanId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEduPlanStructure(int EduPlanId)
        {
            await _metodKomissiyaRepository.CreateEduPlanStructureByUserNameAsync(EduPlanId, User.Identity.Name);            

            return RedirectToAction(nameof(EduPlanPreview),new { id = EduPlanId });
        }

        /// <summary>
        /// Создание дисциплины в заданном разделе
        /// </summary>
        /// <returns></returns>        
        public IActionResult EduPlanCreateDiscipline(int EduPlanId,
            int BlokDisciplChastId)
        {
            ViewBag.EduPlanId = EduPlanId;
            ViewBag.BlokDisciplChastId = BlokDisciplChastId;            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EduPlanCreateDiscipline(int EduPlanId,
            int BlokDisciplChastId,
            string DisciplineNameSearchString,
            string DisciplineNameAdding,
            Discipline discipline)
        {
            if(discipline.DisciplineNameId!=0)
            {
                await _metodKomissiyaRepository.CreateEduPlanDisciplineByUserNameAsync(EduPlanId, BlokDisciplChastId, discipline, User.Identity.Name);
                return RedirectToAction(nameof(EduPlanPreview), new { id = EduPlanId });
            }

            if(DisciplineNameAdding!=null)
            {
                await _metodKomissiyaRepository.CreateDisciplineNameByUserNameAsync(DisciplineNameAdding, User.Identity.Name);
                DisciplineNameSearchString = DisciplineNameAdding;
            }

            ViewBag.EduPlanId = EduPlanId;
            ViewBag.BlokDisciplChastId = BlokDisciplChastId;
            ViewBag.DisciplineNameSearchString = DisciplineNameSearchString;
            ViewBag.Disciplines = _selectListRepository
                .GetSelectListDisciplineNames(DisciplineNameSearchString);            

            return View();
        }

        /// <summary>
        /// Создание дисциплины в заданном разделе
        /// </summary>
        /// <returns></returns>        
        public async Task<IActionResult> EduPlanRemoveDiscipline(int EduPlanId,
            int DisciplineId)
        {
            Discipline discipline = await _metodKomissiyaRepository.GetDisciplineByUserNameAsync(EduPlanId, DisciplineId, User.Identity.Name);
            if (discipline == null) return NotFound();

            ViewBag.EduPlanId = EduPlanId;

            return View(discipline);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EduPlanRemoveDisciplineConfirmed(int EduPlanId,
            int DisciplineId)
        {
            await _metodKomissiyaRepository.RemoveDisciplineByUserNameAsync(EduPlanId, DisciplineId, User.Identity.Name);                       
            return RedirectToAction(nameof(EduPlanPreview), new { id = EduPlanId });
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
            ViewBag.EduForms = _context.EduForms.OrderBy(f => f.EduFormName);
            ViewBag.EduYears = _context.EduYears.OrderBy(y => y.EduYearName);
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