using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Аккаунт пользователя - Структурное подразделение - Должность - Форма занятости
    /// </summary>
    public class AppUserStructSubvision
    {
        public int AppUserStructSubvisionId { get; set; }

        /// <summary>
        /// Аккаунт пользователя
        /// </summary>
        [Display(Name = "Аккаунт пользователя")]
        public AppUser AppUser { get; set; }
        /// <summary>
        /// Аккаунт пользователя
        /// </summary>
        [Display(Name = "Аккаунт пользователя")]
        public string AppUserId { get; set; }

        /// <summary>
        /// Структурное подразделение
        /// </summary>
        [Display(Name = "Структурное подразделение")]
        public StructSubvision StructSubvision { get; set; }
        /// <summary>
        /// Структурное подразделение
        /// </summary>
        [Display(Name = "Структурное подразделение")]
        public int StructSubvisionId { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [Display(Name = "Должность")]
        public Post Post { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        [Display(Name = "Должность")]
        public int PostId { get; set; }

        /// <summary>
        /// Форма занятости
        /// </summary>
        [Display(Name = "Форма занятости")]
        public EmploymentForm EmploymentForm { get; set; }
        /// <summary>
        /// Форма занятости
        /// </summary>
        [Display(Name = "Форма занятости")]
        public int EmploymentFormId { get; set; }

    }
}
