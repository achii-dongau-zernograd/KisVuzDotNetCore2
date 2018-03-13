using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель пользователя приложения
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [DataType(DataType.Date)]        
        [Display(Name = "Дата рождения")]
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Фотография пользователя
        /// </summary>
        public byte[] AppUserPhoto { get; set; }
    }
}
