using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Тип задания в системе дистанционного образования
    /// </summary>
    public class LmsTaskType
    {
        public int LmsTaskTypeId { get; set; }

        /// <summary>
        /// Наименование типа задания СДО
        /// </summary>
        [Display(Name ="Наименование типа задания")]
        public string LmsTaskTypeName { get; set; }

        /// <summary>
        /// Список заданий СДО данного типа
        /// </summary>
        public List<LmsTask> LmsTasks { get; set; }
    }
}
