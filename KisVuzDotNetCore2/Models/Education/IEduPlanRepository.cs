using System.Threading.Tasks;
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
        /// Удаляет аннотацию
        /// </summary>
        /// <param name="eduAnnotation"></param>
        /// <returns></returns>
        Task RemoveEduAnnotationAsync(EduAnnotation eduAnnotation);
    }
}