using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Событие (мероприятие), организовываемое
    /// с помощью системы дистанционного образования
    /// </summary>
    public class LmsEvent
    {
        public int LmsEventId { get; set; }

        public DateTime DateTimeStart { get; set; }

        /// <summary>
        /// Длительность мероприятия (по умолчанию 2 часа)
        /// </summary>
        public TimeSpan Duration { get; set; } = new TimeSpan(2, 0, 0);

        /// <summary>
        /// Дата и время окончания мероприятия
        /// </summary>
        public DateTime DateTimeEnd
        {
            get
            {
                return DateTimeStart + Duration;
            }
        }

        /// <summary>
        /// Тип мероприятия системы дистанционного образования
        /// </summary>
        [Display(Name ="Тип мероприятия")]
        public int LmsEventTypeId { get; set; }
        public LmsEventType LmsEventType { get; set; }

    }
}
