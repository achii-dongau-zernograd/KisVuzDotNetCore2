using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Группа типов мероприятий системы дистанционного образования
    /// </summary>
    public class LmsEventTypeGroup
    {
        public int LmsEventTypeGroupId { get; set; }

        /// <summary>
        /// Наименование группы типов мероприятий СДО
        /// </summary>
        [Display(Name = "Наименование группы типов мероприятий СДО")]
        public string LmsEventTypeGroupName { get; set; }

        /// <summary>
        /// Типы мероприятий СДО, входящие в данную группу
        /// </summary>
        public List<LmsEventType> LmsEventTypes { get; set; }
    }
}
