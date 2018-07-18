using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Таблица для связи EduPlan и EduVidDeyat
    /// </summary>
    public class EduPlanEduVidDeyat
    {
        public int EduPlanEduVidDeyatId { get; set; }
        
        /// <summary>
        /// Учебный план
        /// </summary>
        [Display(Name = "Учебный план")]
        public EduPlan EduPlan { get; set; }
        /// <summary>
        /// Учебный план
        /// </summary>
        [Display(Name = "Учебный план")]
        public int EduPlanId { get; set; }

        /// <summary>
        /// Вид деятельности
        /// </summary>
        [Display(Name = "Вид деятельности")]
        public EduVidDeyat EduVidDeyat { get; set; }
        /// <summary>
        /// Вид деятельности
        /// </summary>
        [Display(Name = "Вид деятельности")]
        public int EduVidDeyatId { get; set; }
    }
}
