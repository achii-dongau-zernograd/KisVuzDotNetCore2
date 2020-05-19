using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Контакт члена семьи / ближайшего родственника
    /// </summary>
    public class FamilyMemberContact
    {
        public int FamilyMemberContactId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Тип родственной связи
        /// </summary>
        [Display(Name = "Тип родственной связи")]
        public int FamilyMemberTypeId { get; set; }
        public FamilyMemberType FamilyMemberType { get; set; }

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
        [Display(Name ="Отчество")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Место работы
        /// </summary>
        [Display(Name = "Место работы")]
        public string WorkPlace { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>        
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        public string GetFamilyMemberContactString
        {
            get
            {
                return $"{LastName} {FirstName} {Patronymic}, тел. {PhoneNumber} {(!string.IsNullOrWhiteSpace(WorkPlace) ? (" (" + WorkPlace + ")") : null)}";
            }
        }
    }
}
