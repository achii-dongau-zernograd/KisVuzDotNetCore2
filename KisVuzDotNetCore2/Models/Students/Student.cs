using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Модель "Студент"
    /// </summary>
    public class Student
    {
        /// <summary>
        /// УИД студента
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// ФИО студента для составления ведомостей
        /// </summary>
        [Display(Name = "ФИО студента")]
        public string StudentFio { get; set; }

        /// <summary>
        /// Номер зачетной книжки
        /// </summary>
        [Display(Name = "Номер зачетной книжки")]
        public string ZachetnayaKnijkaNumber { get; set; }

        #region Аккаунт студента
        /// <summary>
        /// Аккаунт студента
        /// </summary>
        [Display(Name = "Аккаунт студента")]
        public AppUser AppUser { get; set; }
        /// <summary>
        /// Аккаунт студента
        /// </summary>
        [Display(Name = "Аккаунт студента")]
        public string AppUserId { get; set; }
        #endregion

        #region Группа, в которой обучается данный студент
        /// <summary>
        /// Группа, в которой обучается данный студент
        /// </summary>
        [Display(Name ="Группа")]
        public StudentGroup StudentGroup { get; set; }
        /// <summary>
        /// Группа, в которой обучается данный студент
        /// </summary>
        [Display(Name = "Группа")]
        public int StudentGroupId { get; set; }
        #endregion

        /// <summary>
        /// Оценки студента
        /// </summary>
        public List<VedomostStudentMark> VedomostStudentMarks { get; set; }

        /// <summary>
        /// .pdf файл с результатами освоения образовательной программы
        /// </summary>
        [Display(Name = ".pdf файл с результатами освоения образовательной программы")]
        public FileModel RezultOsvoenObrazovatProgr { get; set; }
        [Display(Name = ".pdf файл с результатами освоения образовательной программы")]
        public int? RezultOsvoenObrazovatProgrId { get; set; }
    }
}