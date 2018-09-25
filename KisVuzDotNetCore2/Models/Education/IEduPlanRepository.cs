using System.Threading.Tasks;

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
    }
}