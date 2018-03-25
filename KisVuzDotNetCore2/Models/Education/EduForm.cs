using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Форма обучения"
    /// </summary>
    public class EduForm
    {
        public int EduFormId { get; set; }

        [Display(Name = "Форма обучения")]
        public string EduFormName { get; set; }
    }
}
