using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.LMS
{
    /// <summary>
    /// Роль пользователя в мероприятии СДО
    /// </summary>
    public class AppUserLmsEventUserRole
    {
        public int AppUserLmsEventUserRoleId { get; set; }

        /// <summary>
        /// Наименование роли пользователя в мероприятии СДО
        /// </summary>
        [Display(Name = "Наименование роли пользователя в мероприятии СДО")]
        public string AppUserLmsEventUserRoleName { get; set; }
    }
}