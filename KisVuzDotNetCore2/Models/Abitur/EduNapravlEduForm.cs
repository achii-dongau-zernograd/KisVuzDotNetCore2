using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Сопоставление направления подготовки и формы обучения
    /// для приёма
    /// </summary>
    public class EduNapravlEduForm
    {
        public int EduNapravlEduFormId { get; set; }

        /// <summary>
        /// Направление подготовки
        /// </summary>
        [Display(Name = "Направление подготовки")]
        public int EduNapravlId { get; set; }
        public EduNapravl EduNapravl { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name = "Форма обучения")]
        public int EduFormId { get; set; }
        public EduForm EduForm { get; set; }
    }
}
