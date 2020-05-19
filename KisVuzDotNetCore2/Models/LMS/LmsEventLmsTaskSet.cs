using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Сопоставление набора заданий СДО с событием СДО
    /// </summary>
    public class LmsEventLmsTaskSet
    {
        public int LmsEventLmsTaskSetId { get; set; }

        /// <summary>
        /// Событие СДО
        /// </summary>
        [Display(Name = "Событие СДО")]
        public int LmsEventId { get; set; }
        public LmsEvent LmsEvent { get; set; }

        /// <summary>
        /// Набор заданий СДО
        /// </summary>
        [Display(Name = "Набор заданий СДО")]
        public int LmsTaskSetId { get; set; }
        public LmsTaskSet LmsTaskSet { get;set;}
    }
}
