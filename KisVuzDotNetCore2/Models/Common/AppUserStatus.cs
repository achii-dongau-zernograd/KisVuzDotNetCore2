using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    // Статус пользователя
    public class AppUserStatus
    {
        [Display(Name = "Идентификатор статуса пользователя")]
        public int AppUserStatusId { get; set; }
        [Display(Name = "Статус записи пользователя")]
        public string AppUserStatusName { get; set; }
    }
}