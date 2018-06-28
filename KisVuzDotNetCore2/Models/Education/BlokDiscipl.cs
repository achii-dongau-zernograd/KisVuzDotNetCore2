using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Блок дисциплин Учебного плана
    /// </summary>
    public class BlokDiscipl
    {
        public int BlokDisciplId { get; set; }
        [Display(Name = "Наименование блока дисциплин")]
        public BlokDisciplName BlokDisciplName { get; set; }
        public int BlokDisciplNameId { get; set; }
        [Display(Name = "Наименование части Блока дисциплин")]
        public List<BlokDisciplChast> BlokDisciplChast { get; set; }
        public int EduPlanId { get; set; }
        [Display(Name = "Учебный план")]
        public EduPlan EduPlan { get; set; }
    }
}