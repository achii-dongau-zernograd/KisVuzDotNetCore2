using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Достижение пользователя"
    /// </summary>
    public class UserAchievment
    {
        /// <summary>
        /// УИД достижения пользователя
        /// </summary>
        public int UserAchievmentId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public AppUser AppUser { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }

        /// <summary>
        /// Дата достижения
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Описание достижения пользователя
        /// </summary>
        [Display(Name = "Описание")]
        public string Description { get; set; }

        /// <summary>
        /// Тип достижения
        /// </summary>
        [Display(Name = "Тип достижения")]
        public UserAchievmentType UserAchievmentType { get; set; }
        /// <summary>
        /// Тип достижения
        /// </summary>
        [Display(Name = "Тип достижения")]
        public int UserAchievmentTypeId { get; set; }
    }
}
