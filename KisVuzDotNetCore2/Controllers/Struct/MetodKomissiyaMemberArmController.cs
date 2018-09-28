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

        public async Task<IActionResult> EduPlanCreateOrEdit(int EduProgramId, int? EduPlanId)
        {
            var eduProgram = await _metodKomissiyaRepository.GetEduProgramByUserNameAsync(EduProgramId, User.Identity.Name);
            if (eduProgram == null) return NotFound();

            var eduPlan = new EduPlan();
            if (EduPlanId!=null)
            {
                eduPlan = await _metodKomissiyaRepository.GetEduPlanByUserNameAsync((int)EduPlanId,User.Identity.Name);
                ViewData["EduPlanId"] = EduPlanId;
            }
            
            ViewData["EduFormId"] = _selectListRepository.GetSelectListEduForms();
            ViewData["EduProfileId"] =  eduProgram.EduProfileId;
            ViewData["EduProgramId"] = eduProgram.EduProgramId;
            ViewData["EduProgramPodgId"] = eduProgram.EduProgramPodgId;
            ViewData["EduSrokId"] = _selectListRepository.GetSelectListEduSrok();
            ViewData["StructKafId"] = _selectListRepository.GetSelectListStructKaf();

            List<EduVidDeyat> eduVidDeyats = _context.EduVidDeyat.ToList();
            ViewData["EduVidDeyats"] = eduVidDeyats;

            List<EduYearBeginningTraining> eduYearBeginningTrainings = _context.EduYearBeginningTrainings.ToList();
            ViewData["EduYearBeginningTrainings"] = eduYearBeginningTrainings;

            List<EduYear> eduYears = _context.EduYears.ToList();
            ViewData["EduYears"] = eduYears;
                                    

            return View("EduPlanCreateOrEdit", eduPlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EduPlanCreateOrEdit(EduPlan eduPlan,
            int EduProgramId,
            IFormFile uploadedFile,
            int[] EduVidDeyatIds,
            int[] EduYearBeginningTrainingIds,
            int[] EduPlanEduYearIds)
        {
            if (ModelState.IsValid)
            {
                EduPlan eduPlanRepositoryEntry = await _metodKomissiyaRepository
                    .CreateEduPlanByUserNameAsync(EduProgramId,
                        eduPlan, uploadedFile, EduVidDeyatIds,
                        EduYearBeginningTrainingIds, EduPlanEduYearIds,
                        User.Identity.Name);
                return RedirectToAction(nameof(EduPrograms));
            }

            var eduProgram = await _metodKomissiyaRepository.GetEduProgramByUserNameAsync(EduProgramId, User.Identity.Name);
            if (eduProgram == null) return NotFound();

            ViewData["EduFormId"] = _selectListRepository.GetSelectListEduForms();
            ViewData["EduProfileId"] = eduProgram.EduProfileId;
            ViewData["EduProgramPodgId"] = eduProgram.EduProgramPodgId;
            ViewData["EduSrokId"] = _selectListRepository.GetSelectListEduSrok();
            ViewData["StructKafId"] = _selectListRepository.GetSelectListStructKaf();

            List<EduVidDeyat> eduVidDeyats = _context.EduVidDeyat.ToList();
            ViewData["EduVidDeyats"] = eduVidDeyats;

            List<EduYearBeginningTraining> eduYearBeginningTrainings = _context.EduYearBeginningTrainings.ToList();
            ViewData["EduYearBeginningTrainings"] = eduYearBeginningTrainings;

            List<EduYear> eduYears = _context.EduYears.ToList();
            ViewData["EduYears"] = eduYears;

            return View(eduPlan);
        }

        /// <summary>
        /// Удаляет учебный план
        /// </summary>
        /// <param name="EduProgramId"></param>
        /// <param name="EduPlanId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EduPlanRemove(int EduPlanId)
        {                        
            var eduPlan = await _metodKomissiyaRepository.GetEduPlanByUserNameAsync(EduPlanId, User.Identity.Name);
            if (eduPlan == null) return NotFound();
                       
            return View(eduPlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EduPlanRemoveConfirmed(int EduPlanId)
        {
            await _metodKomissiyaRepository.RemoveEduPlanByUserNameAsync(EduPlanId, User.Identity.Name);
            return RedirectToAction(nameof(EduPrograms));
        }

        #region Аннотации
        /// <summary>
        /// Добавление / редактирование аннотации учебной дисциплины
        /// </summary>
        /// <param name="EduPlanId"></param>
        /// <param name="DisciplineId"></param>
        /// <param name="EduAnnotationId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EduAnnotationCreateOrEdit(int EduPlanId, int DisciplineId, int? EduAnnotationId)
        {
            EduAnnotation eduAnnotation = await _metodKomissiyaRepository.GetEduAnnotationByUserNameAsync(EduPlanId, DisciplineId, EduAnnotationId, User.Identity.Name);
            
            if (eduAnnotation == null)
            {
                return NotFound();
            }
            ViewBag.EduPlanId = EduPlanId;
            return View(eduAnnotation);
        }

        [HttpPost]
        public async Task<IActionResult> EduAnnotationCreateOrEdit(int EduPlanId, int DisciplineId, int? EduAnnotationId, IFormFile uploadedFile)
        {
            EduAnnotation eduAnnotation = await _metodKomissiyaRepository.GetEduAnnotationByUserNameAsync(EduPlanId, DisciplineId, EduAnnotationId, User.Identity.Name);

            if (eduAnnotation != null)
            {
                await _metodKomissiyaRepository.UpdateEduAnnotationAsync(eduAnnotation, uploadedFile);
            }
            
            return RedirectToAction(nameof(EduPlanPreview), new { id = EduPlanId });
        }

        /// <summary>
        /// Удаление аннотации учебной дисциплины
        /// </summary>
        /// <param name="EduPlanId"></param>
        /// <param name="DisciplineId"></param>
        /// <param name="EduAnnotationId"></param>
        /// <returns></returns>
        public async Task<IActionResult> EduAnnotationRemove(int EduPlanId, int DisciplineId, int EduAnnotationId)
        {
            EduAnnotation eduAnnotation = await _metodKomissiyaRepository.GetEduAnnotationByUserNameAsync(EduPlanId, DisciplineId, EduAnnotationId, User.Identity.Name);

            if (eduAnnotation == null)
            {
                return NotFound();
            }
            
            return View(eduAnnotation);
        }

        [HttpPost]
        public async Task<IActionResult> EduAnnotationRemoveConfirmed(int EduPlanId, int DisciplineId, int EduAnnotationId)
        {
            await _metodKomissiyaRepository.RemoveEduAnnotationByUserNameAsync(EduPlanId, DisciplineId, EduAnnotationId, User.Identity.Name);

            return RedirectToAction(nameof(EduPlanPreview), new { id = EduPlanId });
        }
        #endregion

        #region Рабочие программы
        /// <summary>
        /// Добавление / редактирование аннотации учебной дисциплины
        /// </summary>
        /// <param name="EduPlanId"></param>
        /// <param name="DisciplineId"></param>
        /// <param name="RabProgramId"></param>
        /// <returns></returns>
        public async Task<IActionResult> RabProgramCreateOrEdit(int EduPlanId, int DisciplineId, int? RabProgramId)
        {
            RabProgram rabProgram = await _metodKomissiyaRepository.GetRabProgramByUserNameAsync(EduPlanId, DisciplineId, RabProgramId, User.Identity.Name);

            if (rabProgram == null)
            {
                return NotFound();
            }
            ViewBag.EduPlanId = EduPlanId;
            return View(rabProgram);
        }

        [HttpPost]
        public async Task<IActionResult> RabProgramCreateOrEdit(int EduPlanId, int DisciplineId, int? RabProgramId, IFormFile uploadedFile)
        {
            RabProgram rabProgram = await _metodKomissiyaRepository.GetRabProgramByUserNameAsync(EduPlanId, DisciplineId, RabProgramId, User.Identity.Name);

            if (rabProgram != null)
            {
                await _metodKomissiyaRepository.UpdateRabProgramAsync(rabProgram, uploadedFile);
            }

            return RedirectToAction(nameof(EduPlanPreview), new { id = EduPlanId });
        }

        /// <summary>
        /// Удаление аннотации учебной дисциплины
        /// </summary>
        /// <param name="EduPlanId"></param>
        /// <param name="DisciplineId"></param>
        /// <param name="RabProgramId"></param>
        /// <returns></returns>
        public async Task<IActionResult> RabProgramRemove(int EduPlanId, int DisciplineId, int RabProgramId)
        {
            RabProgram rabProgram = await _metodKomissiyaRepository.GetRabProgramByUserNameAsync(EduPlanId, DisciplineId, RabProgramId, User.Identity.Name);

            if (rabProgram == null)
            {
                return NotFound();
            }

            return View(rabProgram);
        }

        [HttpPost]
        public async Task<IActionResult> RabProgramRemoveConfirmed(int EduPlanId, int DisciplineId, int RabProgramId)
        {
            await _metodKomissiyaRepository.RemoveRabProgramByUserNameAsync(EduPlanId, DisciplineId, RabProgramId, User.Identity.Name);
            return RedirectToAction(nameof(EduPlanPreview), new { id = EduPlanId });
        }
        #endregion

        #region Фонды оценочных средств
        /// <summary>
        /// Добавление / редактирование фонда оценочных средств учебной дисциплины
        /// </summary>
        /// <param name="EduPlanId"></param>
        /// <param name="DisciplineId"></param>
        /// <param name="FondOcenochnihSredstvId"></param>
        /// <returns></returns>
        public async Task<IActionResult> FondOcenochnihSredstvCreateOrEdit(int EduPlanId,
            int DisciplineId, int? FondOcenochnihSredstvId)
        {
            FondOcenochnihSredstv fondOcenochnihSredstv = await _metodKomissiyaRepository.
                GetFondOcenochnihSredstvByUserNameAsync(EduPlanId, DisciplineId, FondOcenochnihSredstvId, User.Identity.Name);

            if (fondOcenochnihSredstv == null)
            {
                return NotFound();
            }
            ViewBag.EduPlanId = EduPlanId;
            return View(fondOcenochnihSredstv);
        }

        [HttpPost]
        public async Task<IActionResult> FondOcenochnihSredstvCreateOrEdit(int EduPlanId,
            int DisciplineId, int? FondOcenochnihSredstvId, IFormFile uploadedFile)
        {
            FondOcenochnihSredstv fondOcenochnihSredstv = await _metodKomissiyaRepository
                .GetFondOcenochnihSredstvByUserNameAsync(EduPlanId, DisciplineId, FondOcenochnihSredstvId, User.Identity.Name);

            if (fondOcenochnihSredstv != null)
            {
                await _metodKomissiyaRepository.UpdateFondOcenochnihSredstvAsync(fondOcenochnihSredstv, uploadedFile);
            }

            return RedirectToAction(nameof(EduPlanPreview), new { id = EduPlanId });
        }

        /// <summary>
        /// Удаление аннотации учебной дисциплины
        /// </summary>
        /// <param name="EduPlanId"></param>
        /// <param name="DisciplineId"></param>
        /// <param name="FondOcenochnihSredstvId"></param>
        /// <returns></returns>
        public async Task<IActionResult> FondOcenochnihSredstvRemove(int EduPlanId, int DisciplineId, int FondOcenochnihSredstvId)
        {
            FondOcenochnihSredstv fondOcenochnihSredstv = await _metodKomissiyaRepository.GetFondOcenochnihSredstvByUserNameAsync(EduPlanId, DisciplineId, FondOcenochnihSredstvId, User.Identity.Name);

            if (fondOcenochnihSredstv == null)
            {
                return NotFound();
            }

            return View(fondOcenochnihSredstv);
        }

        [HttpPost]
        public async Task<IActionResult> FondOcenochnihSredstvRemoveConfirmed(int EduPlanId, int DisciplineId, int FondOcenochnihSredstvId)
        {
            await _metodKomissiyaRepository.RemoveFondOcenochnihSredstvByUserNameAsync(EduPlanId, DisciplineId, FondOcenochnihSredstvId, User.Identity.Name);
            return RedirectToAction(nameof(EduPlanPreview), new { id = EduPlanId });
        }
        #endregion

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