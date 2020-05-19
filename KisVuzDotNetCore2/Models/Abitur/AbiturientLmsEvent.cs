using KisVuzDotNetCore2.Models.LMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Вступительное испытание, назначенное абитуриенту
    /// </summary>
    public class AbiturientLmsEvent
    {
        public int AbiturientLmsEventId { get; set; }

        /// <summary>
        /// Абитуриент
        /// </summary>
        [Display(Name ="Абитуриент")]
        public int AbiturientId { get; set; }
        public Abiturient Abiturient { get; set; }

        /// <summary>
        /// Мероприятие СДО
        /// </summary>
        [Display(Name ="Мероприятие")]
        public int LmsEventId { get; set; }
        public LmsEvent LmsEvent { get; set; }

        /// <summary>
        /// Подтверждение участия в мероприятии
        /// </summary>
        [Display(Name = "Подтверждение участия в мероприятии")]
        public bool IsAccepted { get; set; }
    }
}
