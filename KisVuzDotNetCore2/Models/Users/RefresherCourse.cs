using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Курс повышения квалификации"
    /// </summary>
    public class RefresherCourse
    {
        public int RefresherCourseId { get; set; }

        /// <summary>
        /// Регистрационный номер удостоверения
        /// </summary>
        [Display(Name = "Регистрационный номер удостоверения")]
        public string RefresherCourseRegNumber { get; set; }

        /// <summary>
        /// Наименование курса повышения квалификации
        /// </summary>
        [Display(Name = "Наименование курса повышения квалификации")]
        public string RefresherCourseName { get; set; }

        /// <summary>
        /// Количество часов
        /// </summary>
        [Display(Name = "Количество часов")]
        public int RefresherCourseHours { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [Display(Name = "Город")]
        public string RefresherCourseCity { get; set; }

        /// <summary>
        /// Учебное заведение
        /// </summary>
        [Display(Name = "Учебное заведение")]
        public string RefresherCourseInstitition { get; set; }

        /// <summary>
        /// Дата начала прохождения курса повышения квалификации
        /// </summary>
        [Display(Name = "Дата начала прохождения курса повышения квалификации")]
        [DataType(DataType.Date)]
        public DateTime RefresherCourseDateStart { get; set; }

        /// <summary>
        /// Дата окончания прохождения курса повышения квалификации
        /// </summary>
        [Display(Name = "Дата окончания прохождения курса повышения квалификации")]
        [DataType(DataType.Date)]
        public DateTime RefresherCourseDateFinish { get; set; }

        /// <summary>
        /// Дата выдачи удостоверения
        /// </summary>
        [Display(Name = "Дата выдачи удостоверения")]
        [DataType(DataType.Date)]
        public DateTime RefresherCourseDateIssue { get; set; }

        /// <summary>
        /// Скан удостоверения
        /// </summary>
        [Display(Name = "Скан удостоверения")]
        public FileModel RefresherCourseFile { get; set; }
        [Display(Name = "Скан удостоверения")]
        public int RefresherCourseFileId { get; set; }

        /// <summary>
        /// Аккаунт пользователя
        /// </summary>
        [Display(Name = "Пользователь")]
        public AppUser AppUser { get; set; }
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }


        public string GetRefresherCourseString
        {
            get
            {
                return $"Удостоверение о повышении квалификации {RefresherCourseRegNumber} " +
                    $"от {RefresherCourseDateIssue.ToString("dd.MM.yyyy")} " +
                    $"по программе {RefresherCourseName} ({RefresherCourseHours} ч), " +
                    $"{RefresherCourseCity}, {RefresherCourseDateIssue.Year} г.";
            }
        }

        /// <summary>
        /// Статус строки (подтверждено / не подтверждено)
        /// </summary>
        public RowStatus RowStatus { get; set; }
        public int? RowStatusId { get; set; }
    }
}
