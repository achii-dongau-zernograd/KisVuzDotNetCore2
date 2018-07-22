using KisVuzDotNetCore2.Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    public class EduPlanEduYear
    {
        /// <summary>
        /// УИД учебного года для учебного плана
        /// </summary>
        public int EduPlanEduYearId { get; set; }

        ///////////// Навигационные свойства //////////////
        /// <summary>
        /// Учебный план
        /// </summary>
        [Display(Name = "Учебный план")]
        public EduPlan EduPlan { get; set; }
        [Display(Name = "Учебный план")]
        public int EduPlanId { get; set; }

        /// <summary>
        /// Учебный год
        /// </summary>
        [Display(Name = "Учебный год")]
        public EduYear EduYear { get; set; }
        [Display(Name = "Учебный год")]
        public int EduYearId { get; set; }

        //////////////////////////////////////////////
        /// <summary>
        /// Преподаватели-дисциплины по учебному плану в учебном году
        /// </summary>
        public List<TeacherDiscipline> TeacherDisciplines;
    }
}