using Microsoft.AspNetCore.Identity;
using System;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель пользователя приложения
    /// </summary>
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthdate { get; set; }
        public byte[] AppUserPhoto { get; set; }
    }
}
