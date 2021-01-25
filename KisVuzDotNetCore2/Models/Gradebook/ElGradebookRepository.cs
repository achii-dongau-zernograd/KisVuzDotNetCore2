using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Репозиторий электронных журналов
    /// </summary>
    public class ElGradebookRepository : IElGradebookRepository
    {
        AppIdentityDBContext _context;
        

        public ElGradebookRepository(AppIdentityDBContext context)
        {
            _context = context;            
        }

        /// <summary>
        /// Возвращает запрос на список всех электронных журналов
        /// </summary>
        /// <returns></returns>
        public IQueryable<ElGradebook> GetElGradebooks()
        {
            IQueryable<ElGradebook> query = _context.ElGradebooks
                .Include(g => g.ElGradebookTeachers);

            return query;
        }

        public IQueryable<ElGradebook> GetElGradebooksWithStudents()
        {
            IQueryable<ElGradebook> query = GetElGradebooks()
                .Include(g => g.ElGradebookGroupStudents);

            return query;
        }

        public IQueryable<ElGradebook> GetElGradebooksWithLessons()
        {
            IQueryable<ElGradebook> query = GetElGradebooks()
                .Include(g => g.ElGradebookLessons)
                    .ThenInclude(gl => gl.ElGradebookLessonType);

            return query;
        }

        /// <summary>
        /// Возвращает запрос на список журналов согласно параметрам фильтра
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <returns></returns>
        public IQueryable<ElGradebook> GetElGradebooks(ElGradebooksFilterAndSortModel filterAndSortModel)
        {
            IQueryable<ElGradebook> query = GetElGradebooks();

            if (filterAndSortModel != null)
            {
                if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterEduYear))
                {
                    query = query.Where(g => g.EduYear == filterAndSortModel.FilterEduYear);
                }

                if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterFaculty))
                {
                    filterAndSortModel.FilterFaculty = filterAndSortModel.FilterFaculty.Trim();
                    query = query.Where(g => g.Faculty.ToLower().Contains(filterAndSortModel.FilterFaculty.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterDepartment))
                {
                    filterAndSortModel.FilterDepartment = filterAndSortModel.FilterDepartment.Trim();
                    query = query.Where(g => g.Department.ToLower().Contains(filterAndSortModel.FilterDepartment.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterDisciplineName))
                {
                    filterAndSortModel.FilterDisciplineName = filterAndSortModel.FilterDisciplineName.Trim();
                    query = query.Where(g => g.DisciplineName.ToLower().Contains(filterAndSortModel.FilterDisciplineName.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterGroupName))
                {
                    filterAndSortModel.FilterGroupName = filterAndSortModel.FilterGroupName.Trim();
                    query = query.Where(g => g.GroupName.ToLower().Contains(filterAndSortModel.FilterGroupName.ToLower()));
                }

                if (filterAndSortModel.FilterCourse > 0 && filterAndSortModel.FilterCourse <= 6)
                {
                    query = query.Where(g => g.Course == filterAndSortModel.FilterCourse);
                }
                else
                {
                    filterAndSortModel.FilterCourse = 0;
                }

                if (filterAndSortModel.FilterSemesterNumber > 0 && filterAndSortModel.FilterSemesterNumber <= 12)
                {
                    query = query.Where(g => g.SemesterNumber == filterAndSortModel.FilterSemesterNumber);
                }
                else
                {
                    filterAndSortModel.FilterSemesterNumber = 0;
                }
            }
            
            return query;
        }

        /// <summary>
        /// Возвращает список журналов пользователя согласно параметрам фильтра
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<ElGradebook> GetElGradebooks(ElGradebooksFilterAndSortModel filterAndSortModel, string userName)
        {
            if (userName == null)
                return null;

            var query = GetElGradebooks(filterAndSortModel);

            return query;
        }

        /// <summary>
        /// Возвращает электронный журнал по его идентификатору
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task<ElGradebook> GetElGradebookAsync(int elGradebookId)
        {
            var elGradebook = await GetElGradebooks().FirstOrDefaultAsync(g => g.ElGradebookId == elGradebookId);

            return elGradebook;
        }

        /// <summary>
        /// Возвращает электронный журнал со списком студентов
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task<ElGradebook> GetElGradebookWithStudentsAsync(int elGradebookId)
        {
            var elGradebook = await GetElGradebooksWithStudents()
                .FirstOrDefaultAsync(g => g.ElGradebookId == elGradebookId);

            return elGradebook;
        }

        /// <summary>
        /// Возвращает электронный журнал с заполненным списком занятий 
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task<ElGradebook> GetElGradebookWithLessonsAsync(int elGradebookId)
        {
            var elGradebook = await GetElGradebooksWithLessons()
                .FirstOrDefaultAsync(g => g.ElGradebookId == elGradebookId);

            return elGradebook;
        }

        /// <summary>
        /// Возвращает true, если userName входит в число преподавателей, закреплённых за электронным журналом
        /// </summary>
        /// <param name="elGradebook"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsGradebookTeacherAsync(ElGradebook elGradebook, string userName)
        {
            bool isGradebookTeacher = false;

            var appUserId = (await GetAppUserAsync(userName)).Id;

            if (string.IsNullOrEmpty(appUserId))
                return false;

            foreach (var elGradebookTeacher in elGradebook.ElGradebookTeachers)
            {
                if (elGradebookTeacher.UserId == appUserId)
                {
                    isGradebookTeacher = true;
                    break;
                }
            }

            return isGradebookTeacher;
        }

        

        /// <summary>
        /// Создаёт электронный журнал
        /// </summary>
        /// <param name="elGradebook"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task CreateElGradebook(ElGradebook elGradebook, string userName)
        {
            var appUser = await GetAppUserAsync(userName);
            if (appUser == null)
                throw new Exception($"Аккаунт пользователя {userName} не найден!");

            var elGradebookTeacher = new ElGradebookTeacher { UserId = appUser.Id, TeacherFio = appUser.GetFullName };
            elGradebook.ElGradebookTeachers = new List<ElGradebookTeacher>();
            elGradebook.ElGradebookTeachers.Add(elGradebookTeacher);

            _context.Add(elGradebook);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет электронный журнал
        /// </summary>
        /// <param name="elGradebook"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task UpdateElGradebook(ElGradebook elGradebook, string userName)
        {
            var appUser = await GetAppUserAsync(userName);
            if (appUser == null)
                throw new Exception($"Аккаунт пользователя {userName} не найден!");

            var entry = await GetElGradebookAsync(elGradebook.ElGradebookId);

            if(entry.Course != elGradebook.Course)
            {
                entry.Course = elGradebook.Course;
            }

            if (entry.Department != elGradebook.Department)
            {
                entry.Department = elGradebook.Department;
            }

            if (entry.DisciplineName != elGradebook.DisciplineName)
            {
                entry.DisciplineName = elGradebook.DisciplineName;
            }

            if (entry.EduYear != elGradebook.EduYear)
            {
                entry.EduYear = elGradebook.EduYear;
            }

            if (entry.Faculty != elGradebook.Faculty)
            {
                entry.Faculty = elGradebook.Faculty;
            }

            if (entry.GroupName != elGradebook.GroupName)
            {
                entry.GroupName = elGradebook.GroupName;
            }

            if (entry.SemesterNumber != elGradebook.SemesterNumber)
            {
                entry.SemesterNumber = elGradebook.SemesterNumber;
            }

            _context.Update(entry);
            await _context.SaveChangesAsync();
        }


        #region Работа с аккаунтами пользователей
        /// <summary>
        /// Возвращает объект пользователя по его userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private async Task<AppUser> GetAppUserAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        /// <summary>
        /// Добавление студентов в электронный журнал
        /// </summary>
        /// <param name="addingStudents"></param>
        /// <returns></returns>
        public async Task AddStudents(int elGradebookId, List<ElGradebookGroupStudent> addingStudents)
        {
            var elGradebook = await GetElGradebookWithStudentsAsync(elGradebookId);
            if (elGradebook == null)
                throw new Exception("Электронный журнал не найден!");

            elGradebook.ElGradebookGroupStudents.AddRange(addingStudents);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает объект студента из журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        public async Task<ElGradebookGroupStudent> GetElGradebookGroupStudentAsync(int elGradebookGroupStudentId)
        {
            var entry = await _context.ElGradebookGroupStudents
                .Include(s => s.ElGradebook)
                .FirstOrDefaultAsync(s=>s.ElGradebookGroupStudentId == elGradebookGroupStudentId);

            return entry;
        }

        /// <summary>
        /// Добавление студента в журнал
        /// </summary>
        /// <param name="elGradebookGroupStudent"></param>
        /// <returns></returns>
        public async Task AddElGradebookGroupStudent(ElGradebookGroupStudent elGradebookGroupStudent)
        {
            _context.Add(elGradebookGroupStudent);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Редактирование студента в списке группы электронного журнала
        /// </summary>
        /// <param name="elGradebookGroupStudent"></param>
        /// <returns></returns>
        public async Task UpdateElGradebookGroupStudent(ElGradebookGroupStudent elGradebookGroupStudent)
        {
            var entry = await GetElGradebookGroupStudentAsync(elGradebookGroupStudent.ElGradebookGroupStudentId);

            if (entry == null)
                throw new Exception("Студент не найден!");

            if (entry.ElGradebookGroupStudentFio != elGradebookGroupStudent.ElGradebookGroupStudentFio)
                entry.ElGradebookGroupStudentFio = elGradebookGroupStudent.ElGradebookGroupStudentFio;

            if (entry.AppUserId != elGradebookGroupStudent.AppUserId)
                entry.AppUserId = elGradebookGroupStudent.AppUserId;

            _context.Update(entry);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает УИД электронного журнала по переданному УИД студента из этого журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        public async Task<int> GetElGradebookIdByElGradebookGroupStudentIdAsync(int elGradebookGroupStudentId)
        {
            var elGradebookGroupStudent = await _context.ElGradebookGroupStudents.FirstOrDefaultAsync(s => s.ElGradebookGroupStudentId == elGradebookGroupStudentId);
            var elGradebookId = elGradebookGroupStudent.ElGradebookId;
            return elGradebookId;
        }

        /// <summary>
        /// Удаление студента из списка группы электронного журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        public async Task RemoveElGradebookGroupStudentAsync(int elGradebookGroupStudentId)
        {
            var entry = await GetElGradebookGroupStudentAsync(elGradebookGroupStudentId);

            _context.Remove(entry);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет УИД аккаунта пользователя для указанного студента из эл. журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        public async Task ElGradebookGroupStudentSetAppUserId(int elGradebookGroupStudentId, string appUserId)
        {
            var entry = await GetElGradebookGroupStudentAsync(elGradebookGroupStudentId);

            if (entry == null)
                throw new Exception("Запись о студенте в электронном журнале не найдена!");

            if(entry.AppUserId != appUserId)
            {
                entry.AppUserId = appUserId;
                _context.Update(entry);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Добавление учебного занятия в журнал
        /// </summary>
        /// <param name="elGradebook"></param>
        /// <returns></returns>
        public async Task AddElGradebookLessonAsync(ElGradebookLesson elGradebook)
        {
            if (elGradebook == null)
                throw new ArgumentNullException();
            if (elGradebook.ElGradebookId == 0)
                throw new Exception("Не указан УИД электронного журнала!");

            _context.Add(elGradebook);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает учебное занятие по его УИД
        /// </summary>
        /// <param name="elGradebookLessonId"></param>
        /// <returns></returns>
        public async Task<ElGradebookLesson> GetElGradebookLessonAsync(int elGradebookLessonId)
        {
            var entry = await _context.ElGradebookLessons
                .Include(l => l.ElGradebook)
                .FirstOrDefaultAsync(l => l.ElGradebookLessonId == elGradebookLessonId);

            return entry;
        }

        /// <summary>
        /// Обновляет учебное занятие
        /// </summary>
        /// <param name="elGradebookLesson"></param>
        /// <returns></returns>
        public async Task UpdateElGradebookLessonAsync(ElGradebookLesson elGradebookLesson)
        {
            if(elGradebookLesson == null)
                throw new Exception("elGradebookLesson = null");

            var entry = await GetElGradebookLessonAsync(elGradebookLesson.ElGradebookLessonId);
            if (entry == null)
                throw new KeyNotFoundException($"Объект типа ElGradebookLesson с ElGradebookLessonId = {elGradebookLesson.ElGradebookLessonId} не найден!");

            if (entry.Date != elGradebookLesson.Date)
                entry.Date = elGradebookLesson.Date;

            if (entry.ElGradebookLessonTypeId != elGradebookLesson.ElGradebookLessonTypeId)
                entry.ElGradebookLessonTypeId = elGradebookLesson.ElGradebookLessonTypeId;

            if (entry.HoursNumber != elGradebookLesson.HoursNumber)
                entry.HoursNumber = elGradebookLesson.HoursNumber;

            if (entry.LessonTheme != elGradebookLesson.LessonTheme)
                entry.LessonTheme = elGradebookLesson.LessonTheme;
        }


        #endregion
    }
}
