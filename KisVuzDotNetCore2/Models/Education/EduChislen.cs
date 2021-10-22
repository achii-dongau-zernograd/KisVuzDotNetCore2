using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Численность обучающихся"
    /// </summary>
    public class EduChislen
    {   
        public EduProfile EduProfile { get; set; }
        [Display(Name = "Наименование профиля подготовки (направленности, специальности)")]
        public int EduProfileId { get; set; }

        public EduForm EduForm { get; set; }
        [Display(Name = "Форма обучения")]
        public int EduFormId { get; set; }

        public int EduChislenId { get; set; }

        [Display(Name="бюджетных ассигнований федерального бюджета")]
        public int NumberBFpriem { get; set; }

        [Display(Name = "бюджетов субъектов Российской Федерации")]
        public int NumberBRpriem { get; set; }

        [Display(Name = "местных бюджетов")]
        public int NumberBMpriem { get; set; }

        [Display(Name = "средств физических и (или) юридических лиц")]
        public int NumberPpriem { get; set; }

        [Display(Name = "Численность обучающихся, являющихся иностранными гражданами")]
        public int? NumberInostr { get; set; }

        /// <summary>
        /// Общая численность обучающихся
        /// </summary>
        public int NumberAll {
            get
            {
                return NumberBFpriem + NumberBMpriem + NumberPpriem + NumberInostr??0;
            }
        }
    }
}
