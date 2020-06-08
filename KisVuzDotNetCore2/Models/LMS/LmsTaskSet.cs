using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Набор заданий СДО
    /// </summary>
    public class LmsTaskSet
    {
        public int LmsTaskSetId { get; set; }

        [Display(Name = "Описание набора заданий СДО")]
        public string LmsTaskSetDescription { get; set; }

        /// <summary>
        /// Сопоставления данного набора заданий СДО с заданиями СДО
        /// </summary>
        public List<LmsTaskSetLmsTask> LmsTaskSetLmsTasks { get; set; }

        /// <summary>
        /// Сопоставления данного набора заданий СДО с событиями СДО
        /// </summary>
        public List<LmsEventLmsTaskSet> LmsEventLmsTaskSets { get; set; }
    }
}
