using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Модель записи таблицы 12.
    /// Образовательная программа (наличие практики)
    /// </summary>
    public class EduPr
    {
        public int EduPrId { get; set; }

        [Display(Name ="Профиль программы")]
        public EduProfile EduProfile { get; set; }
        [Display(Name = "Профиль программы")]
        public int EduProfileId { get; set; }

        /// <summary>
        /// Признак адаптированной образовательной программы
        /// </summary>
        [Display(Name = "Адаптированная образовательная программа")]
        public bool IsOVZ { get; set; }

        [Display(Name ="Год начала подготовки")]
        public EduYearBeginningTraining EduYearBeginningTraining { get; set; }
        [Display(Name = "Год начала подготовки")]
        public int EduYearBeginningTrainingId { get; set; }

        [Display(Name ="Форма обучения")]
        public EduForm EduForm { get; set; }
        [Display(Name = "Форма обучения")]
        public int EduFormId { get; set; }

        [Display(Name = "Учебная практика, з.е.")]
        public string EduPrUchebn { get; set; }

        [Display(Name = "Производственная практика, з.е.")]
        public string EduPrProizv { get; set; }

        [Display(Name = "Преддипломная практика для выполнения выпускной квалификационной работы, з.е.")]
        public string EduPrPreddiplomn { get; set; }
    }
}
