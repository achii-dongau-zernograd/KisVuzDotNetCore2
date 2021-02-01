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

        public IQueryable<ElGradebook> GetElGradebooksFull()
        {
            IQueryable<ElGradebook> query = GetElGradebooksWithLessons()
                .Include(g => g.ElGradebookGroupStudents)
                .Include(g => g.ElGradebookLessons)
                    .ThenInclude(gl => gl.ElGradebookLessonMarks)
                        .ThenInclude(glm => glm.ElGradebookLessonAttendanceType);

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
        public async Task<List<ElGradebook>> GetElGradebooks(ElGradebooksFilterAndSortModel filterAndSortModel, string userName)
        {
            if (userName == null)
                return null;

            var appUser = await GetAppUserAsync(userName);
            if (appUser== null)
                return null;

            var query = GetElGradebooks(filterAndSortModel);

            var cmp = new ElGradebookTeacherComparer();
            var st = new ElGradebookTeacher();
            st.UserId = appUser.Id;            

            query = query.Where(g => g.ElGradebookTeachers.Contains(st, cmp));
            query = query.OrderByDescending(g => g.GroupName);

            var result = query.ToList();

            return result;
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
        /// Возвращает электронный журнал со всеми связанными данными
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task<ElGradebook> GetElGradebookFullAsync(int elGradebookId)
        {
            var elGradebook = await GetElGradebooksFull()
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

            if (entry.GroupId != elGradebook.GroupId)
            {
                entry.GroupId = elGradebook.GroupId;
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
        /// Возвращает запрос на выборку всех занятий
        /// </summary>
        /// <returns></returns>
        public IQueryable<ElGradebookLesson> GetElGradebookLessons()
        {
            var query = _context.ElGradebookLessons
                .Include(l => l.ElGradebook)
                .Include(l => l.ElGradebookLessonType);

            return query;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех занятий
        /// </summary>
        /// <returns></returns>
        public IQueryable<ElGradebookLesson> GetElGradebookLessonsWithLessonMarks()
        {
            var query = GetElGradebookLessons()
                .Include(l => l.ElGradebookLessonMarks)
                    .ThenInclude(lm => lm.ElGradebookLessonAttendanceType)
                .Include(l => l.ElGradebookLessonMarks)
                    .ThenInclude(lm => lm.ElGradebookGroupStudent);

            return query;
        }

        /// <summary>
        /// Возвращает учебное занятие по его УИД
        /// </summary>
        /// <param name="elGradebookLessonId"></param>
        /// <returns></returns>
        public async Task<ElGradebookLesson> GetElGradebookLessonAsync(int elGradebookLessonId)
        {
            var entry = await GetElGradebookLessons()
                .FirstOrDefaultAsync(l => l.ElGradebookLessonId == elGradebookLessonId);

            return entry;
        }

        /// <summary>
        /// Возвращает учебное занятие по его УИД с заполненными отметками студентов
        /// </summary>
        /// <param name="elGradebookLessonId"></param>
        /// <returns></returns>
        public async Task<ElGradebookLesson> GetElGradebookLessonWithLessonMarksAsync(int elGradebookLessonId)
        {
            var entry = await GetElGradebookLessonsWithLessonMarks()
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

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление учебного занятия из журнала
        /// </summary>
        /// <param name="elGradebookLesson"></param>
        /// <returns></returns>
        public async Task RemoveElGradebookLessonAsync(ElGradebookLesson elGradebookLesson)
        {
            _context.Remove(elGradebookLesson);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавление в занятие электронного журнала списка студентов
        /// </summary>
        /// <param name="elGradebookLesson"></param>
        /// <param name="elGradebookGroupStudents"></param>
        /// <returns></returns>
        public async Task AddElGradebookLessonMarksAsync(ElGradebookLesson elGradebookLesson, List<ElGradebookGroupStudent> elGradebookGroupStudents)
        {
            foreach (var student in elGradebookGroupStudents)
            {
                elGradebookLesson.ElGradebookLessonMarks.Add(
                    new ElGradebookLessonMark
                    {
                        ElGradebookGroupStudentId = student.ElGradebookGroupStudentId,
                        ElGradebookLessonAttendanceTypeId = 1
                    }
                );
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список типов посещаемости учебных занятий
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ElGradebookLessonAttendanceType>> GetElGradebookLessonAttendanceTypes()
        {
            return await _context.ElGradebookLessonAttendanceTypes.ToListAsync();
        }

        /// <summary>
        /// Обновляет успеваемость и оценки группы студентов для указанного занятия
        /// </summary>
        /// <param name="elGradebookLessonId"></param>
        /// <param name="elGradebookLessonMarkIds"></param>
        /// <param name="elGradebookLessonMarkAttendanceTypes"></param>
        /// <param name="elGradebookLessonMarkPointNumbers"></param>
        /// <returns></returns>
        public async Task<ElGradebookLesson> UpdateElGradebookLessonMarksAsync(int elGradebookLessonId, int[] elGradebookLessonMarkIds,
            int[] elGradebookLessonMarkAttendanceTypes, int[] elGradebookLessonMarkPointNumbers)
        {
            var elGradebookLesson = await GetElGradebookLessonWithLessonMarksAsync(elGradebookLessonId);

            foreach (var elGradebookLessonMark in elGradebookLesson.ElGradebookLessonMarks)
            {
                for (int i = 0; i < elGradebookLessonMarkIds.Length; i++)
                {
                    if (elGradebookLessonMark.ElGradebookLessonMarkId == elGradebookLessonMarkIds[i])
                    {
                        if (elGradebookLessonMark.ElGradebookLessonAttendanceTypeId != elGradebookLessonMarkAttendanceTypes[i])
                            elGradebookLessonMark.ElGradebookLessonAttendanceTypeId = elGradebookLessonMarkAttendanceTypes[i];

                        // Сохраняем баллы только для случаев присутствия или отработки, иначе ставим 0
                        if (elGradebookLessonMark.ElGradebookLessonAttendanceTypeId == 1 || elGradebookLessonMark.ElGradebookLessonAttendanceTypeId == 5)
                        {
                            if (elGradebookLessonMark.PointsNumber != elGradebookLessonMarkPointNumbers[i])
                                elGradebookLessonMark.PointsNumber = elGradebookLessonMarkPointNumbers[i];
                        }
                        else
                        {
                            if (elGradebookLessonMark.PointsNumber != 0)
                                elGradebookLessonMark.PointsNumber = 0;
                        }

                        
                    }
                }                
            }

            await _context.SaveChangesAsync();

            return elGradebookLesson;
        }
        #endregion

        /// <summary>
        /// Список журналов с результатами успеваемости пользователя с заданным userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<List<ElGradebookGroupStudent>> GetStudentAttendance(string userName)
        {
            var appUser = await GetAppUserAsync(userName);

            var elGradebookGroupStudents = await _context.ElGradebookGroupStudents
                .Include(s => s.ElGradebookLessonMarks)
                    .ThenInclude(sm => sm.ElGradebookLessonAttendanceType)
                .Include(s => s.ElGradebook.ElGradebookLessons)
                    .ThenInclude(sgl => sgl.ElGradebookLessonType)
                .Include(s => s.ElGradebook.ElGradebookTeachers)                
                //.Include(s => s.ElGradebook.ElGradebookLessons)
                //    .ThenInclude(sgl => sgl.ElGradebookLessonMarks)
                //        .ThenInclude(sglm => sglm.ElGradebookLessonAttendanceType)
                //.Include(s => s.ElGradebook.ElGradebookLessons)
                //    .ThenInclude(sgl => sgl.ElGradebookLessonMarks)
                //        .ThenInclude(sglm => sglm.ElGradebookGroupStudent)
                .Where(s => s.AppUserId == appUser.Id)
                .ToListAsync();

            foreach (var elGradebookGroupStudent in elGradebookGroupStudents)
            {
                var elGradebook = elGradebookGroupStudent.ElGradebook;

                foreach (var elGradebookLesson in elGradebook.ElGradebookLessons)
                {
                    elGradebookLesson.ElGradebookLessonMarks = new List<ElGradebookLessonMark>();
                    var lessonMark = elGradebookGroupStudent.ElGradebookLessonMarks.FirstOrDefault(m=>m.ElGradebookLessonId == elGradebookLesson.ElGradebookLessonId);
                    if(lessonMark != null)
                        elGradebookLesson.ElGradebookLessonMarks.Add(lessonMark);
                }

            }



            return elGradebookGroupStudents;
        }

        /// <summary>
        /// Возвращает преподавателя из уч. журнала по УИД
        /// </summary>
        /// <param name="elGradebookTeacherId"></param>
        /// <returns></returns>
        public async Task<ElGradebookTeacher> GetElGradebookTeacherAsync(int elGradebookTeacherId)
        {
            var entry = await _context.ElGradebookTeachers
                .Include(t => t.ElGradebook.ElGradebookTeachers)
                .FirstOrDefaultAsync(t => t.ElGradebookTeacherId == elGradebookTeacherId);

            return entry;
        }

        /// <summary>
        /// Добавляет преподавателя к журналу
        /// </summary>
        /// <param name="elGradebookTeacher"></param>
        /// <returns></returns>
        public async Task AddElGradebookTeacher(ElGradebookTeacher elGradebookTeacher)
        {
            if (elGradebookTeacher == null)
                throw new NullReferenceException();

            if (string.IsNullOrEmpty(elGradebookTeacher.TeacherFio))
            {
                var appUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == elGradebookTeacher.UserId);
                if(appUser == null)
                    throw new NullReferenceException($"Пользователь {elGradebookTeacher.UserId} не найден!");
                elGradebookTeacher.TeacherFio = appUser.GetFullName;
            }

            var elGradebook = await GetElGradebookAsync(elGradebookTeacher.ElGradebookId);
            var cmp = new ElGradebookTeacherComparer();
            if (elGradebook.ElGradebookTeachers.Contains(elGradebookTeacher, cmp))
                return;

            _context.Add(elGradebookTeacher);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление преподавателя из журнала
        /// </summary>
        /// <param name="elGradebookTeacher"></param>
        /// <returns></returns>
        public async Task RemoveElGradebookTeacher(ElGradebookTeacher elGradebookTeacher)
        {
            var entry = await GetElGradebookTeacherAsync(elGradebookTeacher.ElGradebookTeacherId);
            if(entry == null || entry.ElGradebook.ElGradebookTeachers.Count == 1)//Последнего преподавателя не удаляем!
            {
                return;
            }
            _context.Remove(entry);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает Id пользователя по UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<string> GetAppUserId(string userName)
        {
            AppUser appUser = await GetAppUserAsync(userName);
            return appUser.Id;
        }

        /// <summary>
        /// Удаляет электронный журнал со всеми данными
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        public async Task RemoveElGradebookAsync(int elGradebookId)
        {
            var entry = await GetElGradebookFullAsync(elGradebookId);
            _context.Remove(entry);
            await _context.SaveChangesAsync();
        }
    }
}
