using System;
using System.Collections.Generic;
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
        public string RefresherCourseRegNumber { get; set; }

        /// <summary>
        /// Наименование курса повышения квалификации
        /// </summary>
        public string RefresherCourseName { get; set; }

        /// <summary>
        /// Количество часов
        /// </summary>
        public int RefresherCourseHours { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string RefresherCourseCity { get; set; }

        /// <summary>
        /// Учебное заведение
        /// </summary>
        public string RefresherCourseInstitition { get; set; }

        /// <summary>
        /// Дата начала прохождения курса повышения квалификации
        /// </summary>
        public DateTime RefresherCourseDateStart { get; set; }

        /// <summary>
        /// Дата окончания прохождения курса повышения квалификации
        /// </summary>
        public DateTime RefresherCourseDateFinish { get; set; }

        /// <summary>
        /// Дата выдачи удостоверения
        /// </summary>
        public DateTime RefresherCourseDateIssue { get; set; }

        /// <summary>
        /// Скан удостоверения
        /// </summary>
        public FileModel RefresherCourseFile { get; set; }
        public int RefresherCourseFileId { get; set; }

        /// <summary>
        /// Аккаунт пользователя
        /// </summary>
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }


        public string GetRefresherCourseString
        {
            get
            {
                return $"Удостоверение о повышении квалификации {RefresherCourseRegNumber} " +
                    $"от {RefresherCourseDateIssue} " +
                    $"по программе {RefresherCourseName} ({RefresherCourseHours} ч)," +
                    $"{RefresherCourseCity}, {RefresherCourseDateIssue.Year} г.";
            }
        }
    }
}
