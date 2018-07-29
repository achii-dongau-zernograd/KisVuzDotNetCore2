using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Работа пользователя (реферат, курсовой проект и пр.)"
    /// </summary>
    public class UserWork
    {
        /// <summary>
        /// УИД работы пользователя
        /// </summary>
        public int UserWorkId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name ="Пользователь")]
        public AppUser AppUser { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }

        /// <summary>
        /// Дата размещения работы
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата размещения работы")]
        public DateTime DatePosting { get; set; } = DateTime.Now;

        /// <summary>
        /// Наименование работы пользователя
        /// </summary>
        [Display(Name = "Наименование работы")]
        public string UserWorkName { get; set; }

        /// <summary>
        /// Описание работы пользователя
        /// </summary>
        [Display(Name = "Описание")]
        public string UserWorkDescription { get; set; }

        /// <summary>
        /// Тип работы пользователя
        /// </summary>
        [Display(Name = "Тип работы пользователя")]
        public UserWorkType UserWorkType { get; set; }
        /// <summary>
        /// Тип работы пользователя
        /// </summary>
        [Display(Name = "Тип работы пользователя")]
        public int UserWorkTypeId { get; set; }

        /// <summary>
        /// Файл работы пользователя
        /// </summary>
        [Display(Name ="Файл работы пользователя")]
        public FileModel FileModel { get; set; }
        /// <summary>
        /// Файл работы пользователя
        /// </summary>
        [Display(Name = "Файл работы пользователя")]
        public int? FileModelId { get; set; }

        /// <summary>
        /// Рецензии и оценки
        /// </summary>
        [Display(Name ="Рецензии и оценки")]
        public List<UserWorkReview> UserWorkReviews { get; set; }
    }
}
