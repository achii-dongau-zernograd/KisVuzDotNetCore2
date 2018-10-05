using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Users;
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
        /// Добавление нового или обновление существующего учебного плана,
        /// если образовательная программа доступна пользователю
        /// </summary>
        /// <param name="eduProgramId"></param>
        /// <param name="eduPlan"></param>
        /// <param name="uploadedFile"></param>
        /// <param name="eduVidDeyatIds"></param>
        /// <param name="eduYearBeginningTrainingIds"></param>
        /// <param name="eduPlanEduYearIds"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<EduPlan> CreateEduPlanByUserNameAsync(int eduProgramId, EduPlan eduPlan,
            IFormFile uploadedFile, int[] eduVidDeyatIds,
            int[] eduYearBeginningTrainingIds, int[] eduPlanEduYearIds,
            string userName);

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

        /// <summary>
        /// Удаляет учебный план, если он доступен пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RemoveEduPlanByUserNameAsync(int eduPlanId, string userName);

        /// <summary>
        /// Возвращает объект аннотации дисциплины.
        /// Если eduAnnotationId равно null,
        /// создаёт и возвращает новый объект
        /// </summary>
        /// <param name="eduPlanId">УИД учебного плана</param>
        /// <param name="disciplineId">УИД дисциплины</param>
        /// <param name="eduAnnotationId">УИД аннотации</param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<EduAnnotation> GetEduAnnotationByUserNameAsync(int eduPlanId, int disciplineId, int? eduAnnotationId, string userName);

        /// <summary>
        /// Возвращает объект рабочей программы дисциплины.
        /// Если rabProgramId равно null,
        /// создаёт и возвращает новый объект
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="rabProgramId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<RabProgram> GetRabProgramByUserNameAsync(int eduPlanId, int disciplineId, int? rabProgramId, string userName);

        /// <summary>
        /// Возвращает объект фонд оценочных средств дисциплины.
        /// Если fondOcenochnihSredstvId равно null,
        /// создаёт и возвращает новый объект
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="fondOcenochnihSredstvId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<FondOcenochnihSredstv> GetFondOcenochnihSredstvByUserNameAsync(int eduPlanId, int disciplineId, int? fondOcenochnihSredstvId, string userName);

        /// <summary>
        /// Обновление файла аннотации
        /// </summary>
        /// <param name="eduAnnotation"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<EduAnnotation> UpdateEduAnnotationAsync(EduAnnotation eduAnnotation, IFormFile uploadedFile);

        /// <summary>
        /// Обновление файла рабочей программы
        /// </summary>
        /// <param name="rabProgram"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<RabProgram> UpdateRabProgramAsync(RabProgram rabProgram, IFormFile uploadedFile);

        /// <summary>
        /// Обновление фонда оценочных средств
        /// </summary>
        /// <param name="fondOcenochnihSredstv"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FondOcenochnihSredstv> UpdateFondOcenochnihSredstvAsync(FondOcenochnihSredstv fondOcenochnihSredstv, IFormFile uploadedFile);

        /// <summary>
        /// Удаляет аннотацию, если она доступна пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduAnnotationId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RemoveEduAnnotationByUserNameAsync(int eduPlanId, int disciplineId, int eduAnnotationId, string userName);

        /// <summary>
        /// Удаляет рабочую программу, если она доступна пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="rabProgramId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RemoveRabProgramByUserNameAsync(int eduPlanId, int disciplineId, int rabProgramId, string userName);

        /// <summary>
        /// Удаляет фонд оценочных средств дисциплины, если она доступна пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="fondOcenochnihSredstvId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RemoveFondOcenochnihSredstvByUserNameAsync(int eduPlanId, int disciplineId, int fondOcenochnihSredstvId, string userName);

        /// <summary>
        /// Возвращает связку "Преподаватель - Дисциплина",
        /// если она доступна пользователю
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduYearId"></param>
        /// <param name="teacherDisciplineId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<TeacherDiscipline> GetTeacherDisciplineByUserNameAsync(int eduPlanId, int disciplineId, int eduYearId, int? teacherDisciplineId, string userName);

        /// <summary>
        /// Обновляет привязку "Преподаватель - Дисциплина"
        /// </summary>
        /// <param name="teacherDisciplineChanging"></param>
        /// <returns></returns>
        Task UpdateTeacherDisciplineAsync(TeacherDiscipline teacherDisciplineChanging);

        /// <summary>
        /// Удаляет привязку "Преподаватель - Дисциплина"
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduYearId"></param>
        /// <param name="teacherDisciplineId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RemoveTeacherDisciplineByUserNameAsync(int eduPlanId, int disciplineId, int eduYearId, int teacherDisciplineId, string userName);

        /// <summary>
        /// Возвращает привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduYearId"></param>
        /// <param name="disciplinePomeshenieId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<DisciplinePomeshenie> GetDisciplinePomeshenieByUserNameAsync(int eduPlanId, int disciplineId, int eduYearId, int? disciplinePomeshenieId, string userName);

        /// <summary>
        /// Обновляет привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="disciplinePomeshenieChanging"></param>
        /// <returns></returns>
        Task UpdateDisciplinePomeshenieAsync(DisciplinePomeshenie disciplinePomeshenieChanging);

        /// <summary>
        /// Удаляет привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <param name="disciplineId"></param>
        /// <param name="eduYearId"></param>
        /// <param name="disciplinePomeshenieId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RemoveDisciplinePomeshenieByUserNameAsync(int eduPlanId, int disciplineId, int eduYearId, int disciplinePomeshenieId, string userName);
    }
}
