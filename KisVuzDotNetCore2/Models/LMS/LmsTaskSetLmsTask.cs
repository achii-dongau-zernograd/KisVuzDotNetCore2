using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Сопоставление набора заданий СДО с заданием СДО
    /// </summary>
    public class LmsTaskSetLmsTask
    {
        public int LmsTaskSetLmsTaskId { get; set; }

        /// <summary>
        /// Набор заданий СДО
        /// </summary>
        [Display(Name = "Набор заданий СДО")]
        public int LmsTaskSetId { get; set; }
        public LmsTaskSet LmsTaskSet { get; set; }

        /// <summary>
        /// Задание СДО
        /// </summary>
        [Display(Name = "Задание СДО")]
        public int LmsTaskId { get; set; }
        public LmsTask LmsTask { get; set; }

        /// <summary>
        /// Порядковый номер задания СДО
        /// </summary>
        [Display(Name = "Порядковый номер задания СДО")]
        public int? LmsTaskSetLmsTaskOrder { get; set; }        
    }
}
