using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    public class AppUserCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// Модель представления для регистрации нового абитуриента
    /// </summary>
    public class AppUserCreateModelAbitur
    {
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime? Birthdate { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Адрес электронной почты (Email)")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль (не менее 6 символов)")]
        public string Password { get; set; }        
    }

    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }

    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }

    public class UserProfileModificationModel
    {
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime? Birthdate { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Фотография пользователя")]
        public byte[] AppUserPhoto { get; set; }

        [Display(Name = "Фотография пользователя")]
        public IFormFile AppUserPhotoFile { get; set; }

        [Display(Name = "Уровень образования")]
        public int? EduLevelGroupId { get; set; }

        [Display(Name = "Ученая степень")]
        public int? AcademicDegreeId { get; set; }

        [Display(Name = "Ученое звание")]
        public int? AcademicStatId { get; set; }

        /// <summary>
        /// Дата отсчета общего стажа работы
        /// (дата начала трудовой деятельности)
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата начала трудовой деятельности")]
        public DateTime? DateStartWorking { get; set; }

        /// <summary>
        /// Дата отсчета стажа работы по специальности
        /// (дата начала трудовой деятельности по специальности)
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата начала трудовой деятельности по специальности")]
        public DateTime? DateStartWorkingSpec { get; set; }
    }
}
