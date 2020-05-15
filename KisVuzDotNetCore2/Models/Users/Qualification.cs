using KisVuzDotNetCore2.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Квалификация
    /// </summary>
    public class Qualification
    {
        public int QualificationId { get; set; }

        /// <summary>
        /// Наименование направления подготовки (специальности)
        /// </summary>
        [Display(Name = "Наименование направления подготовки (специальности)")]
        public string NapravlName { get; set; }

        /// <summary> 
        /// Наименование квалификации (инженер и пр.)
        /// </summary>
        [Display(Name ="Квалификация")]
        public string QualificationName { get; set; }

        public string QualificationFullName
        {
            get
            {
                return NapravlName + " - " + QualificationName;
            }
        }

        /// <summary>
        /// Аккаунт пользователя
        /// </summary>
        [Display(Name = "Пользователь")]
        public AppUser AppUser { get; set; }
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }

        public RowStatus RowStatus { get; set; }
        public int? RowStatusId { get; set; }

        /// <summary>
        /// Образование пользователя
        /// </summary>
        public List<UserEducation> UserEducations { get; set; }
    }
}