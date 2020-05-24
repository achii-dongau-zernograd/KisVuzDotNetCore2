using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Перечисление ролей пользователей в мероприятии СДО
    /// </summary>
    public enum AppUserLmsEventUserRolesEnum
    {
        /// <summary>
        /// Участник мероприятия (абитуриенты, студенты и пр.)
        /// </summary>
        Participant = 1,
        /// <summary>
        /// Организатор мероприятия (пользователь, создавший событие)
        /// </summary>
        Organizer = 2,
        /// <summary>
        /// Консультант (пользователь для связи и консультаций)
        /// </summary>
        Consultant = 3,
        /// <summary>
        /// Пользователь, который непосредственно проводит событие
        /// </summary>
        UserWhoHostsEvent = 4
    }
}
