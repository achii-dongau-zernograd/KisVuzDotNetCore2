using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Пользователь - Мероприятие СДО
    /// </summary>
    public class AppUserLmsEvent
    {
        public int AppUserLmsEventId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Мероприятие СДО
        /// </summary>
        [Display(Name = "Мероприятие СДО")]
        public int LmsEventId { get; set; }
        public LmsEvent LmsEvent { get; set; }

        /// <summary>
        /// Роль пользователя в мероприятии СДО
        /// </summary>
        [Display(Name = "Роль пользователя в мероприятии СДО")]
        public int AppUserLmsEventUserRoleId { get; set; }
        public AppUserLmsEventUserRole AppUserLmsEventUserRole { get; set; }
    }
}
