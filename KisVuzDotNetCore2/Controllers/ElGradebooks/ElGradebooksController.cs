using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Gradebook;
using KisVuzDotNetCore2.Models.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Controllers.ElGradebooks
{
    /// <summary>
    /// Контроллер для работы с зарегистрированными абитуриентами
    /// </summary>
    [Authorize(Roles = "Администраторы, Преподаватели")]
    public class ElGradebooksController : Controller
    {
        IElGradebookRepository _elGradebookRepository;
        ISelectListRepository _selectListRepository;
        IStudentRepository _studentRepository;

        public ElGradebooksController(IElGradebookRepository elGradebookRepository,
            ISelectListRepository selectListRepository,
            IStudentRepository studentRepository)
        {
            _elGradebookRepository = elGradebookRepository;
            _selectListRepository = selectListRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> Index(ElGradebooksFilterAndSortModel filterAndSortModel)
        {
            ViewBag.ElGradebooksFilterAndSortModel = filterAndSortModel;
            ViewBag.EduYears = new SelectList(new List<string> { "2020-2021" }, filterAndSortModel.FilterEduYear);
                        
            
            if (filterAndSortModel.IsRequestDataImmediately)
            {
                var data = _elGradebookRepository
                                .GetElGradebooks(filterAndSortModel, User.Identity.Name);

                data = data.OrderByDescending(g => g.GroupName);
                return View(await data.ToListAsync());
            }
            else
            {
                return View();
            }
        }

        #region Добавление или редактирование электронного журнала
        public async Task<IActionResult> ElGradebookCreateOrUpdate(int elGradebookId)
        {
            ElGradebook elGradebook = new ElGradebook { EduYear = "2020-2021",
                DisciplineName = "Информатика",
                Course = 1,
                SemesterNumber = 1,
                GroupName = "АЭ11",
                Faculty = "Энергетический факультет",
                Department = "Математика и биоинформатика"
            };
            if (elGradebookId != 0)
            {
                elGradebook = await _elGradebookRepository.GetElGradebookAsync(elGradebookId);
                if (elGradebook == null)
                    return NotFound();
                                
                bool isGradebookTeacher = await _elGradebookRepository.IsGradebookTeacherAsync(elGradebook, User.Identity.Name);
                if (!isGradebookTeacher)
                    return NotFound();
            }

            ViewBag.Faculties = new SelectList(new List<string> { "Факультет среднего профессионального образования",
                "Факультет \"Экономика и управление территориями\"",
                "Инженерно-технологический факультет",
                "Энергетический факультет"},
                elGradebook.Faculty);

            return View(elGradebook);
        }

        [HttpPost]
        public async Task<IActionResult> ElGradebookCreateOrUpdate(ElGradebook elGradebook)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(ElGradebookCreateOrUpdate), elGradebook);
            }


            if(elGradebook.ElGradebookId == 0)
            {
                await _elGradebookRepository.CreateElGradebook(elGradebook, User.Identity.Name);
            }
            else
            {
                await _elGradebookRepository.UpdateElGradebook(elGradebook, User.Identity.Name);
            }
            
            return RedirectToAction(nameof(Index),
                new { FilterEduYear = elGradebook.EduYear,
                    FilterGroupName = elGradebook.GroupName,
                    IsRequestDataImmediately = true });
        }
        #endregion

        #region Список студентов
        public async Task<IActionResult> ElGradebookGroupStudents(int elGradebookId)
        {
            if (elGradebookId == 0)
                return NotFound();

            var elGradebook = await _elGradebookRepository.GetElGradebookWithStudentsAsync(elGradebookId);
            if (elGradebook == null)
                return NotFound();

            return View(elGradebook);
        }

        /// <summary>
        /// Добавление группы студентов
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ElGradebookGroupStudentsAddGroup(int elGradebookId)
        {
            if (elGradebookId == 0)
                return NotFound();

            var elGradebook = await _elGradebookRepository.GetElGradebookWithStudentsAsync(elGradebookId);
            if (elGradebook == null)
                return NotFound();

            var studentGroup = await _studentRepository.GetStudentGroupByGroupNameAsync(elGradebook.GroupName);

            elGradebook.ElGradebookGroupStudents = new List<ElGradebookGroupStudent>();
            if (studentGroup != null)
            {                
                foreach (var student in studentGroup.Students)
                {
                    elGradebook.ElGradebookGroupStudents.Add(new ElGradebookGroupStudent {
                    ElGradebookGroupStudentFio = student.StudentFio,
                    AppUserId = student.AppUserId });                
                }
            }
            
            return View(elGradebook);
        }

        [HttpPost]
        public async Task<IActionResult> ElGradebookGroupStudentsAddGroupConfirmed(int elGradebookId)
        {
            if (elGradebookId == 0)
                return NotFound();

            var elGradebook = await _elGradebookRepository.GetElGradebookWithStudentsAsync(elGradebookId);
            if (elGradebook == null)
                return NotFound();

            var studentGroup = await _studentRepository.GetStudentGroupByGroupNameAsync(elGradebook.GroupName);
            var addingStudents = new List<ElGradebookGroupStudent>();
            foreach (var student in studentGroup.Students)
            {
                addingStudents.Add(new ElGradebookGroupStudent
                {
                    ElGradebookGroupStudentFio = student.StudentFio,
                    AppUserId = student.AppUserId
                });
            }

            await _elGradebookRepository.AddStudents(elGradebookId, addingStudents);

            return RedirectToAction(nameof(Index),
                new
                {
                    FilterEduYear = elGradebook.EduYear,
                    FilterGroupName = elGradebook.GroupName,
                    IsRequestDataImmediately = true
                });
        }


        /// <summary>
        /// Редактирование студента в списке группы электронного журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ElGradebookGroupStudentsEditStudent(int elGradebookGroupStudentId)
        {
            ElGradebookGroupStudent entry = await _elGradebookRepository.GetElGradebookGroupStudentAsync(elGradebookGroupStudentId);

            if (entry == null)
                return NotFound();

            return View(entry);
        }

        /// <summary>
        /// Редактирование студента в списке группы электронного журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ElGradebookGroupStudentsEditStudent(ElGradebookGroupStudent elGradebookGroupStudent)
        {
            await _elGradebookRepository.UpdateElGradebookGroupStudent(elGradebookGroupStudent);

            return RedirectToAction(nameof(ElGradebookGroupStudents), new { elGradebookGroupStudent.ElGradebookId });
        }


        #endregion
    }
}
