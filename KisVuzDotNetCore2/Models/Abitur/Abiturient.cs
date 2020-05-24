using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Абитуриент
    /// </summary>
    public class Abiturient
    {
        public int AbiturientId { get; set; }

        [Display(Name ="Пользователь")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Необходимость общежития
        /// </summary>
        [Display(Name = "Необходимость общежития")]
        public bool IsHostelRequired { get; set; } = false;

        /// <summary>
        /// Статус абитуриента
        /// </summary>
        [Display(Name ="Статус абитуриента")]
        public int AbiturientStatusId { get; set; }
        public AbiturientStatus AbiturientStatus { get; set; }

        /// <summary>
        /// Заявления о приёме
        /// </summary>
        public List<ApplicationForAdmission> ApplicationForAdmissions { get; set; }

        /// <summary>
        /// Индивидуальные достижения абитуриента
        /// </summary>
        public List<AbiturientIndividualAchievment> AbiturientIndividualAchievments { get; set; }
        
        /// <summary>
        /// Возвращает строку описания абитуриента, включающую
        /// ФИО, дату рождения и email
        /// </summary>
        public string AbiturientFioBirthdayEmail
        {
            get
            {
                return $"{AppUser?.GetFullName ?? " - "}, {(AppUser?.Birthdate != null ? ((DateTime)AppUser?.Birthdate).ToString("d") : " - ")}, {AppUser?.Email ?? " - "}";
            }
        }
    }
}
