using KisVuzDotNetCore2.Models.Education;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Интерфейс методкомиссий
    /// </summary>
    public interface IMetodKomissiyaRepository
    {
        /// <summary>
        /// Определяет, является ли аутентифицированный
        /// пользователь членом методкомиссии
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsMetodKomissiyaMember(string userName);

        /// <summary>
        /// Возвращает набор образовательных программ,
        /// к которым имеет доступ пользователь
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IEnumerable<EduProgram>> GetEduProgramsByUserNameAsync(string userName);

        /// <summary>
        /// Возвращает образовательную программу по УИД,
        /// если она доступна пользователю
        /// </summary>
        /// <param name="eduProgramId">УИД образовательной программы</param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<EduProgram> GetEduProgramByUserNameAsync(int eduProgramId, string userName);

        /// <summary>
        /// Обновляет образовательную программу,
        /// если она доступна пользователю
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="eduProgram"></param>
        /// <param name="uploadedFile"></param>
        /// <param name="eduFormIds"></param>
        /// <param name="eduYearIds"></param>
        Task UpdateEduProgramByUserNameAsync(string userName, EduProgram eduProgram, IFormFile uploadedFile, int[] eduFormIds, int[] eduYearIds);
        
        /// <summary>
        /// Возвращает перечисление методкомиссий,
        /// в которые входит пользователь
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IEnumerable<MetodKomissiya>> GetMetodKomissiiByUserNameAsync(string userName);

        /// <summary>
        /// Создаёт структуру учебного плана,
        /// если он доступен пользователю
        /// </summary>
        /// <param name="eduPlanId">УИД учебного плана</param>
        /// <param name="userName">Имя пользователя</param>
        /// <returns></returns>
        Task CreateEduPlanStructureByUserNameAsync(int eduPlanId, string userName);

        /// <summary>
        /// Удаляет образовательную программу,
        /// если она доступна пользователю
        /// </summary>
        /// <param name="eduProgram"></param>
        /// <param name="userName"></param>
        Task RemoveEduProgramByUserNameAsync(EduProgram eduProgram, string userName);

        /// <summary>
        /// Возвращает учебный план,
        /// если он доступен пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<EduPlan> GetEduPlanByUserNameAsync(int eduPlanId, string userName);

        /// <summary>
        /// Добавляет дисциплину в учебный план,
        /// если он доступен пользователю
        /// </summary>        
        Task CreateEduPlanDisciplineByUserNameAsync(int eduPlanId, int blokDisciplChastId, Discipline discipline, string userName);

        /// <summary>
        /// Добавляет дисциплину в справочник наименований дисциплин
        /// </summary>
        /// <param name="disciplineName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task CreateDisciplineNameByUserNameAsync(string disciplineName, string userName);

        /// <summary>
        /// Возвращает объект дисциплины,
        /// если учебный план доступен пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Discipline> GetDisciplineByUserNameAsync(int eduPlanId, int disciplineId, string userName);

        /// <summary>
        /// Удаляет объект дисциплины,
        /// если учебный план доступен пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RemoveDisciplineByUserNameAsync(int eduPlanId, int disciplineId, string userName);
    }
}
