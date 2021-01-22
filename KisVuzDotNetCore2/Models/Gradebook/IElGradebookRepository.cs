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
        /// Возвращает запрос на список журналов пользователя согласно параметрам фильтра
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<ElGradebook> GetElGradebooks(ElGradebooksFilterAndSortModel filterAndSortModel, string userName);
        
        /// <summary>
        /// Возвращает электронный журнал по его идентификатору
        /// </summary>
        /// <param name="elGradebookId"></param>
        /// <returns></returns>
        Task<ElGradebook> GetElGradebookAsync(int elGradebookId);

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
    }
}
