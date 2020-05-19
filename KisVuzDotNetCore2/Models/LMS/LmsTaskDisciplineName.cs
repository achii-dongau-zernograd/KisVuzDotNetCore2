using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Сопоставление задание в СДО с наименованием дисциплины
    /// </summary>
    public class LmsTaskDisciplineName
    {
        public int LmsTaskDisciplineNameId { get; set; }

        /// <summary>
        /// Задание в СДО
        /// </summary>
        [Display(Name ="Задание в СДО")]
        public int LmsTaskId { get; set; }
        public LmsTask LmsTask { get; set; }

        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        [Display(Name = "Задание в СДО")]
        public int DisciplineNameId { get; set; }
        public DisciplineName DisciplineName { get; set; }
    }
}
