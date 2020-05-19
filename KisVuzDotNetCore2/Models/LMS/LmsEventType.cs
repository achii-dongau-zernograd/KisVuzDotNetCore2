using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Тип мероприятия системы дистанционного образования
    /// </summary>
    public class LmsEventType
    {
        public int LmsEventTypeId { get; set; }

        /// <summary>
        /// Наименование типа мероприятия системы дистанционного образования
        /// </summary>
        [Display(Name ="Наименование типа мероприятия")]
        public string LmsEventTypeName { get; set; }

        [Display(Name = "Группа типов мероприятий СДО")]
        public int LmsEventTypeGroupId { get; set; }
        public LmsEventTypeGroup LmsEventTypeGroup { get; set; }
    }
}
