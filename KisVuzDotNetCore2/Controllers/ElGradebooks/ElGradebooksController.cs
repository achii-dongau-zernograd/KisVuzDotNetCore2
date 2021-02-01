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
                var data = await _elGradebookRepository
                                .GetElGradebooks(filterAndSortModel, User.Identity.Name);
                                
                return View(data);
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Просмотр перечня учебных журналов
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> View(ElGradebooksFilterAndSortModel filterAndSortModel)
        {
            ViewBag.ElGradebooksFilterAndSortModel = filterAndSortModel;
            ViewBag.EduYears = new SelectList(new List<string> { "2020-2021" }, filterAndSortModel.FilterEduYear);

            ViewBag.Faculties = new SelectList(new List<string> { "Факультет среднего профессионального образования",
                "Факультет \"Экономика и управление территориями\"",
                "Инженерно-технологический факультет",
                "Энергетический факультет"},
                filterAndSortModel.FilterFaculty);

            var departments = new List<string> {
                "Математика и биоинформатика",
                "Теплоэнергетика и техносферная безопасность",
                "Физическое воспитание и спорт",
                "Эксплуатация энергетического оборудования и электрических машин",
                "Электроэнергетика и электротехника",
                "Бухгалтерский учет, анализ и аудит",
                "Гуманитарные дисциплины и иностранные языки",
                "Землеустройство и кадастры",
                "Экономика и управление",
                "Агрономия и селекция сельскохозяйственных культур",
                "Техническая механика и физика",
                "Технический сервис в агропромышленном комплексе",
                "Технологии и средства механизации агропромышленного комплекса",
                "Тракторы, автомобили и эксплуатация автотранспортных средств"
            };
            departments.Sort();

            ViewBag.Departments = new SelectList(departments,
                filterAndSortModel.FilterDepartment);

            if (filterAndSortModel.IsRequestDataImmediately)
            {
                var data = await _elGradebookRepository
                                .GetElGradebooks(filterAndSortModel)
                                .ToListAsync();

                return View(data);
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
                GroupName = "АЭ-11",
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

            ViewBag.Departments = new SelectList(new List<string> {
                "Математика и биоинформатика",
                "Теплоэнергетика и техносферная безопасность",
                "Физическое воспитание и спорт",
                "Эксплуатация энергетического оборудования и электрических машин",
                "Электроэнергетика и электротехника",
                "Бухгалтерский учет, анализ и аудит",
                "Гуманитарные дисциплины и иностранные языки",
                "Землеустройство и кадастры",
                "Экономика и управление",
                "Агрономия и селекция сельскохозяйственных культур",
                "Техническая механика и физика",
                "Технический сервис в агропромышленном комплексе",
                "Технологии и средства механизации агропромышленного комплекса",
                "Тракторы, автомобили и эксплуатация автотранспортных средств"
            },
                elGradebook.Faculty);

            ViewBag.StudentGroups = _selectListRepository.GetSelectListStudentGroups(elGradebook.GroupId);

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
        /// <summary>
        /// Список студентов электронного журнала
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
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
        /// Привязка аккаунта студента
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ElGradebookGroupStudentsBindAccount(int elGradebookGroupStudentId, string appUserLastNameFragment)
        {
            var entry = await _elGradebookRepository.GetElGradebookGroupStudentAsync(elGradebookGroupStudentId);
            if (entry == null)
                return NotFound();


            if(!string.IsNullOrWhiteSpace(appUserLastNameFragment))
            {
                ViewBag.FindedAppUsers = _selectListRepository.GetSelectListAppUsersByFirstName(appUserLastNameFragment);
            }
            else
            {
                appUserLastNameFragment = entry.ElGradebookGroupStudentFio;
                ViewBag.FindedAppUsers = _selectListRepository.GetSelectListAppUsersByFirstName(entry.ElGradebookGroupStudentFio);
            }

            ViewBag.AppUserLastNameFragment = appUserLastNameFragment;

            return View(entry);
        }

        [HttpPost]
        public async Task<IActionResult> ElGradebookGroupStudentsBindAccountConfirmed(ElGradebookGroupStudent elGradebookGroupStudent)
        {
            await _elGradebookRepository.ElGradebookGroupStudentSetAppUserId(elGradebookGroupStudent.ElGradebookGroupStudentId, elGradebookGroupStudent.AppUserId);

            return RedirectToAction(nameof(ElGradebookGroupStudents), new { elGradebookGroupStudent.ElGradebookId });
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

            StudentGroup studentGroup;
            if (elGradebook.GroupId != 0)
            {
                studentGroup = await _studentRepository.GetStudentGroupByIdAsync(elGradebook.GroupId);
            }
            else
            {
                studentGroup = await _studentRepository.GetStudentGroupByGroupNameAsync(elGradebook.GroupName);
            }
            

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
            else
            {
                ViewBag.StudentGroupNoyFoundMessage = $"Группа {elGradebook.GroupName} не найдена!";
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

            StudentGroup studentGroup;
            if (elGradebook.GroupId != 0)
            {
                studentGroup = await _studentRepository.GetStudentGroupByIdAsync(elGradebook.GroupId);
            }
            else
            {
                studentGroup = await _studentRepository.GetStudentGroupByGroupNameAsync(elGradebook.GroupName);
            }

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

            return RedirectToAction(nameof(ElGradebookGroupStudents),
                new
                {
                    elGradebookId
                });
        }

        /// <summary>
        /// Добавление студента в журнал
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public IActionResult ElGradebookGroupStudentsAdd(int elGradebookId)
        {
            ElGradebookGroupStudent newStudent = new ElGradebookGroupStudent { ElGradebookId = elGradebookId };
            return View(newStudent);
        }

        /// <summary>
        /// Добавление студента в журнал
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ElGradebookGroupStudentsAdd(ElGradebookGroupStudent elGradebookGroupStudent)
        {
            await _elGradebookRepository.AddElGradebookGroupStudent(elGradebookGroupStudent);

            return RedirectToAction(nameof(ElGradebookGroupStudents), new { elGradebookGroupStudent.ElGradebookId });
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

        /// <summary>
        /// Удаление студента из списка группы электронного журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ElGradebookGroupStudentsRemoveStudent(int elGradebookGroupStudentId)
        {
            var entry = await _elGradebookRepository.GetElGradebookGroupStudentAsync(elGradebookGroupStudentId);
            if (entry == null)
                return NotFound();

            return View(entry);
        }

        /// <summary>
        /// Удаление студента из списка группы электронного журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ElGradebookGroupStudentsRemoveStudent(ElGradebookGroupStudent elGradebookGroupStudent)
        {            
            await _elGradebookRepository.RemoveElGradebookGroupStudentAsync(elGradebookGroupStudent.ElGradebookGroupStudentId);

            return RedirectToAction(nameof(ElGradebookGroupStudents), new { elGradebookGroupStudent.ElGradebookId });
        }
        #endregion

        #region Занятия
        /// <summary>
        /// Список занятий указанного электронного журнала
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ElGradebookLessons(int elGradebookId)
        {
            ElGradebook elGradebook = await _elGradebookRepository.GetElGradebookWithLessonsAsync(elGradebookId);
            if (elGradebook == null)
                return NotFound();

            return View(elGradebook);
        }

        /// <summary>
        /// Добавление учебного занятия в журнал
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ElGradebookLessonCreate(int elGradebookId)
        {
            ElGradebook elGradebook = await _elGradebookRepository.GetElGradebookWithLessonsAsync(elGradebookId);
            if (elGradebook == null)
                return NotFound();

            ElGradebookLesson elGradebookLesson = new ElGradebookLesson { Date = DateTime.Today };
            elGradebookLesson.ElGradebookId = elGradebook.ElGradebookId;
            elGradebookLesson.ElGradebook = elGradebook;

            ViewBag.ElGradebookLessonTypes = _selectListRepository.GetSelectListElGradebookLessonTypes();

            return View("ElGradebookLessonCreateOrUpdate", elGradebookLesson);
        }

        /// <summary>
        /// Редактирование учебного занятия в журнал
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ElGradebookLessonUpdate(int elGradebookLessonId)
        {
            ElGradebookLesson elGradebookLesson = await _elGradebookRepository.GetElGradebookLessonAsync(elGradebookLessonId);
            if (elGradebookLesson == null)
                return NotFound();

            ViewBag.ElGradebookLessonTypes = _selectListRepository.GetSelectListElGradebookLessonTypes(elGradebookLesson.ElGradebookLessonTypeId);

            return View("ElGradebookLessonCreateOrUpdate", elGradebookLesson);
        }

        /// <summary>
        /// Добавление/редактирование учебного занятия в журнал
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ElGradebookLessonCreateOrUpdate(ElGradebookLesson ElGradebookLesson)
        {
            if (ElGradebookLesson == null)
                return NotFound();

            if (ElGradebookLesson.ElGradebookLessonId == 0)
            {
                await _elGradebookRepository.AddElGradebookLessonAsync(ElGradebookLesson);
            }
            else
            {
                await _elGradebookRepository.UpdateElGradebookLessonAsync(ElGradebookLesson);
            }
                        
            return RedirectToAction(nameof(ElGradebookLessons), new { ElGradebookLesson.ElGradebookId });
        }

        //////////////////// Удаление занятия

        /// <summary>
        /// Удаление учебного занятия из журнала GET
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ElGradebookLessonRemove(int elGradebookLessonId)
        {
            ElGradebookLesson elGradebookLesson = await _elGradebookRepository.GetElGradebookLessonAsync(elGradebookLessonId);
            if (elGradebookLesson == null)
                return NotFound();
                        
            return View(elGradebookLesson);
        }

        /// <summary>
        /// Удаление учебного занятия из журнала POST
        /// </summary>
        /// <param name="ElGradebookLesson"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ElGradebookLessonRemove(ElGradebookLesson ElGradebookLesson)
        {
            if (ElGradebookLesson == null)
                return NotFound();

            await _elGradebookRepository.RemoveElGradebookLessonAsync(ElGradebookLesson);

            return RedirectToAction(nameof(ElGradebookLessons), new { ElGradebookLesson.ElGradebookId });
        }


        ///////////////// Редактирование успеваемости  студентов на учебном занятии
        public async Task<IActionResult> ElGradebookLessonMarksEdit(int elGradebookLessonId)
        {
            var elGradebookLesson = await _elGradebookRepository.GetElGradebookLessonWithLessonMarksAsync(elGradebookLessonId);
            if (elGradebookLesson == null)
                return NotFound();

            // Если список оценок пуст, заполняем его на основе имеющегося списка студентов
            if(elGradebookLesson.ElGradebookLessonMarks.Count == 0)
            {
                var elGradebookWithStudents = await _elGradebookRepository.GetElGradebookWithStudentsAsync(elGradebookLesson.ElGradebookId);

                await _elGradebookRepository.AddElGradebookLessonMarksAsync(elGradebookLesson, elGradebookWithStudents.ElGradebookGroupStudents);
            }                        

            ViewBag.AttendanceTypes = await _elGradebookRepository.GetElGradebookLessonAttendanceTypes();
            
            return View(elGradebookLesson);
        }

        [HttpPost]
        public async Task<IActionResult> ElGradebookLessonMarksEdit(int elGradebookLessonId,
            int[] elGradebookLessonMarkIds,
            int[] elGradebookLessonMarkAttendanceTypes,
            int[] elGradebookLessonMarkPointNumbers)
        {
            ElGradebookLesson elGradebookLesson = await _elGradebookRepository.UpdateElGradebookLessonMarksAsync(elGradebookLessonId,
                elGradebookLessonMarkIds, elGradebookLessonMarkAttendanceTypes, elGradebookLessonMarkPointNumbers);
            if (elGradebookLesson == null)
                return NotFound();

            return RedirectToAction(nameof(ElGradebookLessonMarksEdit), new { elGradebookLessonId });
        }
        #endregion

        #region Преподаватели
        public async Task<IActionResult> ElGradebookTeachers(int elGradebookId)
        {
            var elGradebook = await _elGradebookRepository.GetElGradebookAsync(elGradebookId);
            if (elGradebook == null)
                return NotFound();

            return View(elGradebook);
        }

        public async Task<IActionResult> ElGradebookTeacherAdd(int elGradebookId)
        {
            var elGradebook = await _elGradebookRepository.GetElGradebookAsync(elGradebookId);
            if (elGradebook == null)
                return NotFound();

            var elGradebookTeacher = new ElGradebookTeacher {
                ElGradebookId = elGradebookId,
                ElGradebook = elGradebook
            };

            ViewBag.Teachers = _selectListRepository.GetSelectListAppUsersTeachers();

            return View(elGradebookTeacher);
        }

        [HttpPost]
        public async Task<IActionResult> ElGradebookTeacherAdd(ElGradebookTeacher elGradebookTeacher)
        {
            await _elGradebookRepository.AddElGradebookTeacher(elGradebookTeacher);

            return RedirectToAction(nameof(ElGradebookTeachers), new { elGradebookTeacher.ElGradebookId });
        }


        public async Task<IActionResult> ElGradebookTeacherRemove(int elGradebookTeacherId)
        {
            var elGradebookTeacher = await _elGradebookRepository.GetElGradebookTeacherAsync(elGradebookTeacherId);

            return View(elGradebookTeacher);
        }

        [HttpPost]
        public async Task<IActionResult> ElGradebookTeacherRemove(ElGradebookTeacher elGradebookTeacher)
        {
            await _elGradebookRepository.RemoveElGradebookTeacher(elGradebookTeacher);

            return RedirectToAction(nameof(ElGradebookTeachers), new { elGradebookTeacher.ElGradebookId });
        }
        #endregion

        /// <summary>
        /// Просмотр всего журнала
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ElGradebookView(int elGradebookId)
        {
            var elGradebook = await _elGradebookRepository.GetElGradebookFullAsync(elGradebookId);
            var userId = await _elGradebookRepository.GetAppUserId(User.Identity.Name);
            ViewBag.UserId = userId;
            return View(elGradebook);
        }

        /// <summary>
        /// Просмотр успеваемости студента
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> StudentAttendanceView()
        {
            var elGradebooks = await _elGradebookRepository.GetStudentAttendance(User.Identity.Name);

            return View(elGradebooks);
        }
    }
}
