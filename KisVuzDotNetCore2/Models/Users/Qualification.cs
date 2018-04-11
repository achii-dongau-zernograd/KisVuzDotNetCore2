using System;
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

        /// <summary>
        /// Аккаунт пользователя
        /// </summary>
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}