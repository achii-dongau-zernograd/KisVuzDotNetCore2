using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Интерфейс репозитория электронных журналов
    /// </summary>
    public interface IElGradebookRepository
    {
        /// <summary>
        /// Возвращает запрос на список журналов согласно параметрам фильтра
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <returns></returns>
        IQueryable<ElGradebook> GetElGradebooks(ElGradebooksFilterAndSortModel filterAndSortModel);

        /// <summary>
        /// Возвращает список журналов пользователя согласно параметрам фильтра
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<List<ElGradebook>> GetElGradebooks(ElGradebooksFilterAndSortModel filterAndSortModel, string userName);
        
        /// <summary>
        /// Возвращает электронный журнал по его идентификатору
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        Task<ElGradebook> GetElGradebookAsync(int elGradebookId);

        /// <summary>
        /// Возвращает электронный журнал с заполненным списком занятий
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        Task<ElGradebook> GetElGradebookWithLessonsAsync(int elGradebookId);

        /// <summary>
        /// Возвращает электронный журнал со всеми связанными данными
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        Task<ElGradebook> GetElGradebookFullAsync(int elGradebookId);

        /// <summary>
        /// Возвращает true, если userName входит в число преподавателей, закреплённых за электронным журналом
        /// </summary>
        /// <param name="elGradebook"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsGradebookTeacherAsync(ElGradebook elGradebook, string userName);
        
        /// <summary>
        /// Создаёт электронный журнал
        /// </summary>
        /// <param name="elGradebook"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task CreateElGradebook(ElGradebook elGradebook, string userName);

        /// <summary>
        /// Обновляет электронный журнал
        /// </summary>
        /// <param name="elGradebook"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task UpdateElGradebook(ElGradebook elGradebook, string userName);

        /// <summary>
        /// Возвращает электронный журнал со списком студентов
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        Task<ElGradebook> GetElGradebookWithStudentsAsync(int elGradebookId);
        
        /// <summary>
        /// Добавление студентов в электронный журнал
        /// </summary>
        /// <param name="addingStudents"></param>
        /// <returns></returns>
        Task AddStudents(int elGradebookId, List<ElGradebookGroupStudent> addingStudents);
        
        /// <summary>
        /// Возвращает объект студента из журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        Task<ElGradebookGroupStudent> GetElGradebookGroupStudentAsync(int elGradebookGroupStudentId);

        /// <summary>
        /// Редактирование студента в списке группы электронного журнала
        /// </summary>
        /// <param name="elGradebookGroupStudent"></param>
        /// <returns></returns>
        Task UpdateElGradebookGroupStudent(ElGradebookGroupStudent elGradebookGroupStudent);
        
        /// <summary>
        /// Возвращает УИД электронного журнала по переданному УИД студента из этого журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        Task<int> GetElGradebookIdByElGradebookGroupStudentIdAsync(int elGradebookGroupStudentId);

        /// <summary>
        /// Удаление студента из списка группы электронного журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <returns></returns>
        Task RemoveElGradebookGroupStudentAsync(int elGradebookGroupStudentId);

        /// <summary>
        /// Добавление студента в журнал
        /// </summary>
        /// <param name="elGradebookGroupStudent"></param>
        /// <returns></returns>
        Task AddElGradebookGroupStudent(ElGradebookGroupStudent elGradebookGroupStudent);

        /// <summary>
        /// Обновляет УИД аккаунта пользователя для указанного студента из эл. журнала
        /// </summary>
        /// <param name="elGradebookGroupStudentId"></param>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        Task ElGradebookGroupStudentSetAppUserId(int elGradebookGroupStudentId, string appUserId);

        /// <summary>
        /// Добавление учебного занятия в журнал
        /// </summary>
        /// <param name="elGradebook"></param>
        /// <returns></returns>
        Task AddElGradebookLessonAsync(ElGradebookLesson elGradebook);
        
        /// <summary>
        /// Возвращает учебное занятие по его УИД
        /// </summary>
        /// <param name="elGradebookLessonId"></param>
        /// <returns></returns>
        Task<ElGradebookLesson> GetElGradebookLessonAsync(int elGradebookLessonId);

        /// <summary>
        /// Возвращает учебное занятие по его УИД с заполненными отметками студентов
        /// </summary>
        /// <param name="elGradebookLessonId"></param>
        /// <returns></returns>
        Task<ElGradebookLesson> GetElGradebookLessonWithLessonMarksAsync(int elGradebookLessonId);

        /// <summary>
        /// Обновляет учебное занятие
        /// </summary>
        /// <param name="elGradebookLesson"></param>
        /// <returns></returns>
        Task UpdateElGradebookLessonAsync(ElGradebookLesson elGradebookLesson);

        /// <summary>
        /// Удаление учебного занятия из журнала
        /// </summary>
        /// <param name="elGradebookLesson"></param>
        /// <returns></returns>
        Task RemoveElGradebookLessonAsync(ElGradebookLesson elGradebookLesson);
        
        /// <summary>
        /// Добавление в занятие электронного журнала списка студентов
        /// </summary>
        /// <param name="elGradebookLesson"></param>
        /// <param name="elGradebookGroupStudents"></param>
        /// <returns></returns>
        Task AddElGradebookLessonMarksAsync(ElGradebookLesson elGradebookLesson, List<ElGradebookGroupStudent> elGradebookGroupStudents);
        
        /// <summary>
        /// Возвращает список типов посещаемости учебных занятий
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ElGradebookLessonAttendanceType>> GetElGradebookLessonAttendanceTypes();
        
        /// <summary>
        /// Обновляет успеваемость и оценки группы студентов для указанного занятия
        /// </summary>
        /// <param name="elGradebookLessonId"></param>
        /// <param name="elGradebookLessonMarkIds"></param>
        /// <param name="elGradebookLessonMarkAttendanceTypes"></param>
        /// <param name="elGradebookLessonMarkPointNumbers"></param>
        /// <returns></returns>
        Task<ElGradebookLesson> UpdateElGradebookLessonMarksAsync(int elGradebookLessonId,
            int[] elGradebookLessonMarkIds,
            int[] elGradebookLessonMarkAttendanceTypes,
            int[] elGradebookLessonMarkPointNumbers);

        /// <summary>
        /// Список журналов с результатами успеваемости пользователя с заданным userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<List<ElGradebookGroupStudent>> GetStudentAttendance(string userName);
        
        /// <summary>
        /// Возвращает преподавателя из уч. журнала по УИД
        /// </summary>
        /// <param name="elGradebookTeacherId"></param>
        /// <returns></returns>
        Task<ElGradebookTeacher> GetElGradebookTeacherAsync(int elGradebookTeacherId);
        
        /// <summary>
        /// Добавляет преподавателя к журналу
        /// </summary>
        /// <param name="elGradebookTeacher"></param>
        /// <returns></returns>
        Task AddElGradebookTeacher(ElGradebookTeacher elGradebookTeacher);
        
        /// <summary>
        /// Удаление преподавателя из журнала
        /// </summary>
        /// <param name="elGradebookTeacher"></param>
        /// <returns></returns>
        Task RemoveElGradebookTeacher(ElGradebookTeacher elGradebookTeacher);
        
        /// <summary>
        /// Возвращает Id пользователя по UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<string> GetAppUserId(string userName);
    }
}
