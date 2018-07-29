using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Модель представления для создания аккаунта студента с добавлением в группу
    /// </summary>
    public class CreateStudentWithAccountModel
    {        
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
                
        [Required]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime? Birthdate { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Номер зачетной книжки")]
        public string ZachetnayaKnijkaNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        /// <summary>
        /// Группа, в которой обучается данный студент
        /// </summary>
        [Required]
        [Display(Name = "Группа")]
        public int StudentGroupId { get; set; }
    }
}
