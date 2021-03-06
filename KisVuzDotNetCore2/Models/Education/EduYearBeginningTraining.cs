﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Год начала подготовки"
    /// </summary>
    public class EduYearBeginningTraining
    {
        /// <summary>
        /// УИД года начала подготовки
        /// </summary>
        public int EduYearBeginningTrainingId { get; set; }

        /// <summary>
        /// Наименование года начала подготовки
        /// </summary>
        [Display(Name = "Наименование года начала подготовки")]
        public string EduYearBeginningTrainingName { get; set; }

        /// <summary>
        /// Набор привязок годов начала подготовки к учебным планам
        /// </summary>
        public List<EduPlanEduYearBeginningTraining> EduPlanEduYearBeginningTrainings { get; set; }
    }
}