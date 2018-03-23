using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Уровень образования"
    /// </summary>
    public class EduLevel
    {
        /// <summary>
        /// Идентификатор уровня образования
        /// </summary>
        [Display(Name = "Идентификатор уровня образования")]
        public int EduLevelId { get; set; }

        /// <summary>
        /// Наименование уровня образования
        /// </summary>
        [Display(Name = "Уровень образования")]
        public string EduLevelName { get; set; }

        /// <summary>
        /// Список УГС
        /// </summary>        
        public List<EduUgs> EduUgses = new List<EduUgs>();
    }    
}
