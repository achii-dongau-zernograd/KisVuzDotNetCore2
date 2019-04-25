using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.UchPosobiya;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KisVuzDotNetCore2.Controllers.UchPosobiya
{
    [Authorize(Roles ="Администраторы, Библиотека")]
    public class LibraryUchPosobiyaController : Controller
    {
        IUchPosobiyaRepository _uchPosobiyaRepository;
        ISelectListRepository _selectListRepository;

        public LibraryUchPosobiyaController(IUchPosobiyaRepository uchPosobiyaRepository,
            ISelectListRepository selectListRepository)
        {
            _uchPosobiyaRepository = uchPosobiyaRepository;
            _selectListRepository = selectListRepository;
        }

        /// <summary>
        /// Список учебных пособий
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(UchPosobieFilterModel uchPosobieFilterModel)
        {
            IEnumerable<UchPosobie> uchPosobiya = await _uchPosobiyaRepository.GetUchPosobiyaAsync(uchPosobieFilterModel);

            var uchPosobiyaViewModel = new UchPosobiyaViewModel
            {
                UchPosobiya = uchPosobiya,
                UchPosobieFilterModel = uchPosobieFilterModel
            };

            return View(uchPosobiyaViewModel);
        }

        /// <summary>
        /// Добавление / редактирование учебного пособия
        /// </summary>
        /// <param name="uchPosobieEditViewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> UchPosobieCreateOrEdit(int id, string filter)
        {
            ViewBag.filter = filter;

            UchPosobie uchPosobie = await _uchPosobiyaRepository.GetUchPosobieByIdAsync(id);
            ViewBag.UchPosobieVidList = _selectListRepository.GetSelectListUchPosobieVid();
            ViewBag.UchPosobieFormaIzdaniyaList = _selectListRepository.GetSelectListUchPosobieFormaIzdaniya();
            return View(new UchPosobieEditViewModel { UchPosobie = uchPosobie });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UchPosobieCreateOrEdit(UchPosobieEditViewModel uchPosobieEditViewModel, UchPosobieFilterModel uchPosobieFilterModel)
        {
            if(uchPosobieEditViewModel != null)
            {
                await _uchPosobiyaRepository.UpdateUchPosobie(uchPosobieEditViewModel);
            }

            return RedirectToAction(nameof(Index), uchPosobieFilterModel);
        }

        /// <summary>
        /// Удаление учебного пособия
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> UchPosobieDelete(int id)
        {
            var uchPosobie = await _uchPosobiyaRepository.GetUchPosobieByIdAsync(id);
            if (uchPosobie.UchPosobieId == 0) return NotFound();

            return View(uchPosobie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UchPosobieDeleteConfirmed(int UchPosobieId)
        {
            var uchPosobie = await _uchPosobiyaRepository.GetUchPosobieByIdAsync(UchPosobieId);
            if (uchPosobie.UchPosobieId != 0)
            {
                await _uchPosobiyaRepository.RemoveUchPosobieAsync(uchPosobie);
            }

            return RedirectToAction(nameof(Index));
        }

        #region Авторы
        /// <summary>
        /// Добавление / редактирование автора учебного пособия
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UchPosobieAuthorId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UchPosobieAuthorCreateOrEdit(int UchPosobieId, int UchPosobieAuthorId)
        {
            // Находим учебное пособие по УИД
            var uchPosobie = await _uchPosobiyaRepository.GetUchPosobieByIdAsync(UchPosobieId);
            if (uchPosobie == null || uchPosobie.UchPosobieId == 0) return NotFound();

            var uchPosobieAuthor = new UchPosobieAuthor();
            uchPosobieAuthor.UchPosobie = uchPosobie;

            // Если UchPosobieAuthorId=0 - новая запись
            // иначе - редактирование
            if (UchPosobieAuthorId!=0)
            {
                uchPosobieAuthor = uchPosobie.UchPosobieAuthors.SingleOrDefault(ua => ua.UchPosobieAuthorId == UchPosobieAuthorId);
                if (uchPosobieAuthor == null) return NotFound();
            }

            ViewBag.Authors = _selectListRepository.GetSelectListAuthors();

            return View(uchPosobieAuthor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UchPosobieAuthorCreateOrEdit(UchPosobieAuthor uchPosobieAuthor)
        {
            if (uchPosobieAuthor == null) return NotFound();

            if (uchPosobieAuthor.AuthorId == 0)
            {
                return RedirectToAction(nameof(AddAuthor), new { uchPosobieAuthor.UchPosobieId, uchPosobieAuthor.UchPosobieAuthorId });
            }
            else
            {                
                await _uchPosobiyaRepository.UpdateUchPosobieAuthorAsync(uchPosobieAuthor);
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Добавление автора в справочник
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddAuthor(UchPosobieAuthor uchPosobieAuthor, string fio, string selectedAppUserId)
        {
            if(!string.IsNullOrWhiteSpace(selectedAppUserId))
            {
                Author author = await _uchPosobiyaRepository.CreateAuthorByAppUserIdAsync(selectedAppUserId);
                uchPosobieAuthor.AuthorId = author.AuthorId;
                await _uchPosobiyaRepository.UpdateUchPosobieAuthorAsync(uchPosobieAuthor);
                return RedirectToAction(nameof(Index));                
            }

            if(!string.IsNullOrWhiteSpace(fio))
            {
                ViewBag.FindedUsers = _selectListRepository.GetSelectListAppUsersByFirstName(fio);
            }
            ViewBag.fio = fio;
            return View(uchPosobieAuthor);
        }

        /// <summary>
        /// Удаляет привязку автора к учебному пособию
        /// </summary>
        /// <param name="UchPosobieId"></param>
        /// <param name="UchPosobieAuthorId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UchPosobieAuthorRemove(int UchPosobieAuthorId)
        {
            var uchPosobieAuthor = await _uchPosobiyaRepository.GetUchPosobieAuthorByIdAsync(UchPosobieAuthorId);
            return View(uchPosobieAuthor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UchPosobieAuthorRemoveConfirmed(int UchPosobieAuthorId)
        {
            if(UchPosobieAuthorId!=0)
            {
                await _uchPosobiyaRepository.RemoveUchPosobieAuthorByIdAsync(UchPosobieAuthorId);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Учебное пособие - Дисциплина
        /// <summary>
        /// Добавление / редактирование
        /// привязки "Учебное пособие - Дисциплина"
        /// </summary>
        /// <param name="UchPosobieId"></param>
        /// <param name="UchPosobieDisciplineNameId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UchPosobieDisciplineNameCreateOrEdit(int UchPosobieId,
            int UchPosobieDisciplineNameId)
        {
            // Находим учебное пособие по УИД
            var uchPosobie = await _uchPosobiyaRepository.GetUchPosobieByIdAsync(UchPosobieId);
            if (uchPosobie == null || uchPosobie.UchPosobieId == 0) return NotFound();

            var uchPosobieDisciplineName = new UchPosobieDisciplineName();
            uchPosobieDisciplineName.UchPosobie = uchPosobie;

            // Если UchPosobieAuthorId=0 - новая запись
            // иначе - редактирование
            if (UchPosobieDisciplineNameId != 0)
            {
                uchPosobieDisciplineName = uchPosobie.UchPosobieDisciplineNames
                    .SingleOrDefault(ud => ud.UchPosobieDisciplineNameId == UchPosobieDisciplineNameId);
                if (uchPosobieDisciplineName == null) return NotFound();
            }
                        
            ViewBag.Disciplines = _selectListRepository.GetSelectListDisciplines(uchPosobieDisciplineName.DisciplineNameId);

            return View(uchPosobieDisciplineName);
        }

        [HttpPost]
        public async Task<IActionResult> UchPosobieDisciplineNameCreateOrEdit(UchPosobieDisciplineName uchPosobieDisciplineName)
        {
            await _uchPosobiyaRepository.UpdateUchPosobieDisciplineNameAsync(uchPosobieDisciplineName);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Удаление привязки "Учебное пособие - Дисциплина"
        /// </summary>
        /// <param name="UchPosobieDisciplineNameId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UchPosobieDisciplineNameRemove(int UchPosobieDisciplineNameId)
        {
            var uchPosobieDisciplineName = await _uchPosobiyaRepository.GetUchPosobieDisciplineNameByIdAsync(UchPosobieDisciplineNameId);
            if (uchPosobieDisciplineName == null) return NotFound();
            return View(uchPosobieDisciplineName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UchPosobieDisciplineNameRemoveConfirmed(int UchPosobieDisciplineNameId)
        {
            var uchPosobieDisciplineName = await _uchPosobiyaRepository.GetUchPosobieDisciplineNameByIdAsync(UchPosobieDisciplineNameId);
            if (uchPosobieDisciplineName != null)
            {
                await _uchPosobiyaRepository.RemoveUchPosobieDisciplineNameAsync(uchPosobieDisciplineName);
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Учебное пособие - Направление
        /// <summary>
        /// Добавление / редактирование
        /// привязки "Учебное пособие - Направление"
        /// </summary>
        /// <param name="UchPosobieId"></param>
        /// <param name="UchPosobieDisciplineNameId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UchPosobieEduNapravlCreateOrEdit(int UchPosobieId,
            int UchPosobieEduNapravlId)
        {
            // Находим учебное пособие по УИД
            var uchPosobie = await _uchPosobiyaRepository.GetUchPosobieByIdAsync(UchPosobieId);
            if (uchPosobie == null || uchPosobie.UchPosobieId == 0) return NotFound();

            var uchPosobieEduNapravl = new UchPosobieEduNapravl();
            uchPosobieEduNapravl.UchPosobie = uchPosobie;

            // Если UchPosobieEduNapravlId=0 - новая запись
            // иначе - редактирование
            if (UchPosobieEduNapravlId != 0)
            {
                uchPosobieEduNapravl = uchPosobie.EduNapravls
                    .SingleOrDefault(ud => ud.UchPosobieEduNapravlId == UchPosobieEduNapravlId);
                if (uchPosobieEduNapravl == null) return NotFound();
            }

            ViewBag.EduNapravls = _selectListRepository.GetSelectListEduNapravlFullNames(uchPosobieEduNapravl.EduNapravlId);

            return View(uchPosobieEduNapravl);
        }

        [HttpPost]
        public async Task<IActionResult> UchPosobieEduNapravlCreateOrEdit(UchPosobieEduNapravl uchPosobieEduNapravl)
        {
            await _uchPosobiyaRepository.UpdateUchPosobieEduNapravlAsync(uchPosobieEduNapravl);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Удаление привязки "Учебное пособие - Направление"
        /// </summary>
        /// <param name="UchPosobieEduNapravlId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UchPosobieEduNapravlRemove(int UchPosobieEduNapravlId)
        {
            var uchPosobieEduNapravl = await _uchPosobiyaRepository.GetUchPosobieEduNapravlByIdAsync(UchPosobieEduNapravlId);
            if (uchPosobieEduNapravl == null) return NotFound();
            return View(uchPosobieEduNapravl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UchPosobieEduNapravlRemoveConfirmed(int UchPosobieEduNapravlId)
        {
            var uchPosobieEduNapravl = await _uchPosobiyaRepository.GetUchPosobieEduNapravlByIdAsync(UchPosobieEduNapravlId);
            if (uchPosobieEduNapravl != null)
            {
                await _uchPosobiyaRepository.RemoveUchPosobieEduNapravlAsync(uchPosobieEduNapravl);
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Учебное пособие - Формы обучения
        /// <summary>
        /// Редактирование привязки
        /// "Учебное пособие - Форма обучения"
        /// </summary>
        /// <param name="UchPosobieId"></param>        
        /// <returns></returns>
        public async Task<IActionResult> UchPosobieEduFormCreateOrEdit(int UchPosobieId,
            int UchPosobieEduFormId)
        {
            // Находим учебное пособие по УИД
            var uchPosobie = await _uchPosobiyaRepository.GetUchPosobieByIdAsync(UchPosobieId);
            if (uchPosobie == null || uchPosobie.UchPosobieId == 0) return NotFound();

            var uchPosobieEduForm = new UchPosobieEduForm();
            uchPosobieEduForm.UchPosobie = uchPosobie;

            // Если UchPosobieEduFormId=0 - новая запись
            // иначе - редактирование
            if (UchPosobieEduFormId != 0)
            {
                uchPosobieEduForm = uchPosobie.EduForms
                    .SingleOrDefault(uf => uf.UchPosobieEduFormId == UchPosobieEduFormId);
                if (uchPosobieEduForm == null) return NotFound();
            }

            ViewBag.EduForms = _selectListRepository.GetSelectListEduForms(uchPosobieEduForm.EduFormId);

            return View(uchPosobieEduForm);
        }

        [HttpPost]
        public async Task<IActionResult> UchPosobieEduFormCreateOrEdit(UchPosobieEduForm uchPosobieEduForm)
        {
            await _uchPosobiyaRepository.UpdateUchPosobieEduFormAsync(uchPosobieEduForm);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Удаление привязки "Учебное пособие - Форма обучения"
        /// </summary>
        /// <param name="UchPosobieEduFormId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UchPosobieEduFormRemove(int UchPosobieEduFormId)
        {
            var uchPosobieEduForm = await _uchPosobiyaRepository.GetUchPosobieEduFormByIdAsync(UchPosobieEduFormId);
            if (uchPosobieEduForm == null) return NotFound();
            return View(uchPosobieEduForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UchPosobieEduFormRemoveConfirmed(int UchPosobieEduFormId)
        {
            var uchPosobieEduForm = await _uchPosobiyaRepository.GetUchPosobieEduFormByIdAsync(UchPosobieEduFormId);
            if (uchPosobieEduForm != null)
            {
                await _uchPosobiyaRepository.RemoveUchPosobieEduFormAsync(uchPosobieEduForm);
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}