using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Http;

namespace KisVuzDotNetCore2.Models.Education
{
    public interface IEduPlanRepository
    {
        /// <summary>
        /// Возвращает учебный план со всеми связанными данными
        /// </summary>
        /// <param name="eduPlanId"></param>
        /// <returns></returns>
        Task<EduPlan> GetEduPlanAsync(int eduPlanId);

        /// <summary>
        /// Создаёт структуру учебного плана
        /// </summary>
        /// <param name="eduPlan"></param>
        /// <returns></returns>
        Task CreateEduPlanStructureAsync(EduPlan eduPlan);

        /// <summary>
        /// Возвращает дисциплину из учебного плана
        /// </summary>
        /// <param name="eduPlan"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        Discipline GetDiscipline(EduPlan eduPlan, int disciplineId);

        /// <summary>
        /// Удаляет объект дисциплины
        /// </summary>
        /// <param name="discipline"></param>
        /// <returns></returns>
        Task RemoveDiscipline(Discipline discipline);

        /// <summary>
        /// Добавление нового или обновление существующего учебного плана
        /// </summary>
        /// <param name="eduPlan"></param>
        /// <param name="uploadedFile"></param>
        /// <param name="eduVidDeyatIds"></param>
        /// <param name="eduYearBeginningTrainingIds"></param>
        /// <param name="eduPlanEduYearIds"></param>
        /// <returns></returns>
        Task<EduPlan> CreateEduPlan(EduPlan eduPlan,
            IFormFile uploadedFile,
            int[] eduVidDeyatIds,
            int[] eduYearBeginningTrainingIds,
            int[] eduPlanEduYearIds);

        /// <summary>
        /// Удаление учебного плана
        /// </summary>
        /// <param name="eduPlanId">УИД учебного плана</param>
        /// <returns></returns>
        Task RemoveEduPlanAsync(int eduPlanId);

        /// <summary>
        /// Удаление учебного плана
        /// </summary>
        /// <param name="eduPlan"></param>
        /// <returns></returns>
        Task RemoveEduPlanAsync(EduPlan eduPlan);

        /// <summary>
        /// Добавляет к аннотации загруженный файл
        /// </summary>
        /// <param name="eduAnnotation"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<EduAnnotation> UpdateEduAnnotationAsync(EduAnnotation eduAnnotation, IFormFile uploadedFile);

        /// <summary>
        /// Добавляет к рабочей программе загруженный файл
        /// </summary>
        /// <param name="rabProgram"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<RabProgram> UpdateRabProgramAsync(RabProgram rabProgram, IFormFile uploadedFile);

        /// <summary>
        /// Обновление файла листа переутверждения рабочей программы
        /// </summary>
        /// <param name="rabProgram"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<RabProgram> UpdateRabProgramListPereutverjdeniyaAsync(RabProgram rabProgram, IFormFile uploadedFile);

        /// <summary>
        /// Добавляет к фонду оценочных средств загруженный файл
        /// </summary>
        /// <param name="fondOcenochnihSredstv"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FondOcenochnihSredstv> UpdateFondOcenochnihSredstvAsync(FondOcenochnihSredstv fondOcenochnihSredstv, IFormFile uploadedFile);

        /// <summary>
        /// Обновление листа переутверждения фонда оценочных средств
        /// </summary>
        /// <param name="fondOcenochnihSredstv"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FondOcenochnihSredstv> UpdateFondOcenochnihSredstvListPereutverjdeniyaAsync(FondOcenochnihSredstv fondOcenochnihSredstv, IFormFile uploadedFile);

        /// <summary>
        /// Удаляет аннотацию
        /// </summary>
        /// <param name="eduAnnotation"></param>
        /// <returns></returns>
        Task RemoveEduAnnotationAsync(EduAnnotation eduAnnotation);

        /// <summary>
        /// Удаляет рабочую программу
        /// </summary>
        /// <param name="rabProgram"></param>
        /// <returns></returns>
        Task RemoveRabProgramAsync(RabProgram rabProgram);

        /// <summary>
        /// Удаляет лист переутверждения рабочей программы
        /// </summary>
        /// <param name="rabProgram"></param>
        /// <returns></returns>
        Task RemoveRabProgramListPereutverjdeniyaAsync(RabProgram rabProgram);

        /// <summary>
        /// Удаляет фонд оценочных средств
        /// </summary>
        /// <param name="fondOcenochnihSredstv"></param>
        /// <returns></returns>
        Task RemoveFondOcenochnihSredstvAsync(FondOcenochnihSredstv fondOcenochnihSredstv);

        /// <summary>
        /// Удаляет лист переутверждения фонда оценочных средств дисциплины
        /// </summary>
        /// <param name="fondOcenochnihSredstv"></param>
        /// <returns></returns>
        Task RemoveFondOcenochnihSredstvListPereutverjdeniyaAsync(FondOcenochnihSredstv fondOcenochnihSredstv);

        /// <summary>
        /// Возвращает привязку "Преподаватель - Дисциплина"
        /// </summary>
        /// <param name="discipline"></param>
        /// <param name="teacherDisciplineId"></param>
        /// <returns></returns>
        TeacherDiscipline GetTeacherDisciplineByDisciplineAndTeacherDisciplineId(Discipline discipline, int? teacherDisciplineId);

        /// <summary>
        /// Обновляет привязку "Преподаватель - Дисциплина"
        /// </summary>
        /// <param name="teacherDisciplineChanging"></param>
        /// <returns></returns>
        Task UpdateEduAnnotationAsync(TeacherDiscipline teacherDisciplineChanging);

        /// <summary>
        /// Удаляет привязку "Преподаватель - Дисциплина"
        /// </summary>
        /// <param name="teacherDiscipline"></param>
        /// <returns></returns>
        Task RemoveTeacherDisciplineAsync(TeacherDiscipline teacherDiscipline);

        /// <summary>
        /// Возвращает привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="discipline"></param>
        /// <param name="disciplinePomeshenieId"></param>
        /// <returns></returns>
        DisciplinePomeshenie GetDisciplinePomeshenieByDisciplineAndDisciplinePomeshenieId(Discipline discipline, int? disciplinePomeshenieId);

        /// <summary>
        /// Обновляет привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="disciplinePomeshenieChanging"></param>
        /// <returns></returns>
        Task UpdateDisciplinePomeshenieAsync(DisciplinePomeshenie disciplinePomeshenieChanging);

        /// <summary>
        /// Удаляет привязку "Дисциплина - Помещение"
        /// </summary>
        /// <param name="disciplinePomeshenie"></param>
        /// <returns></returns>
        Task RemoveDisciplinePomeshenieAsync(DisciplinePomeshenie disciplinePomeshenie);
        
    }
}