using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Таблица 11. Образовательная программа (объём программы по годам)
    /// </summary>
    public class EduOPYear
    {
        public int EduOPYearId { get; set; }

        /// <summary>
        /// Профиль подготовки
        /// </summary>
        [Display(Name = "Профиль программы")]
        public EduProfile EduProfile { get; set; }
        [Display(Name = "Профиль программы")]
        public int EduProfileId { get; set; }

        /// <summary>
        /// Признак адаптированной образовательной программы
        /// </summary>
        [Display(Name ="Адаптированная образовательная программа")]
        public bool IsOVZ { get; set; }

        /// <summary>
        /// Год начала подготовки
        /// </summary>
        [Display(Name = "Год начала подготовки")]
        public EduYearBeginningTraining EduYearBeginningTraining { get;set;}
        [Display(Name = "Год начала подготовки")]
        public int EduYearBeginningTrainingId { get; set; }

        /// <summary>
        /// Наименование года обучения (за 1 год обучения и т.д.)
        /// </summary>
        [Display(Name = "Объём программы")]
        public EduOPEduYearName EduOPEduYearName { get; set; }
        [Display(Name = "Объём программы")]
        public int EduOPEduYearNameId { get; set; }

        [Display(Name = "Очная форма обучения (зачетные единицы - з.е.)")]
        public string VOchn { get; set; }

        [Display(Name = "Очно-заочная форма обучения (зачетные единицы - з.е.)")]
        public string VOchnZaochn { get; set; }

        [Display(Name = "Заочная форма обучения (зачетные единицы - з.е.)")]
        public string VZaochn { get; set; }
    }
}
