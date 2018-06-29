using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Добавление года начала подготовки в учебный план"
    /// </summary>
    public class EduPlanEduYearBeginningTraining
    {
        public int EduPlanEduYearBeginningTrainingId { get; set; }

        [Display(Name = "Учебный план")]
        public EduPlan EduPlan { get; set; }
        [Display(Name = "Учебный план")]
        public int EduPlanId { get; set; }

        [Display(Name = "Год начала подготовки")]
        public EduYearBeginningTraining EduYearBeginningTraining { get; set; }
        [Display(Name = "Год начала подготовки")]
        public int EduYearBeginningTrainingId { get; set; }
    }
}
