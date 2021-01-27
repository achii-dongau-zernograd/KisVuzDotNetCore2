using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Электронные журналы. Студент группы
    /// </summary>
    public class ElGradebookGroupStudent
    {
        /// <summary>
        /// УИД студента группы
        /// </summary>
        public int ElGradebookGroupStudentId { get; set; }

        /// <summary>
        /// ФИО студента группы
        /// </summary>
        [Display(Name = "ФИО студента группы")]
        public string ElGradebookGroupStudentFio { get; set; }

        /// <summary>
        /// УИД аккаунта студента группы
        /// </summary>
        [Display(Name = "УИД аккаунта студента группы")]
        public string AppUserId { get; set; }

        /// <summary>
        /// УИД журнала
        /// </summary>
        public int ElGradebookId { get; set; }
        /// <summary>
        /// Журнал
        /// </summary>
        public ElGradebook ElGradebook { get; set; }

        /// <summary>
        /// Посещаемость и оценки за учебные занятия
        /// </summary>
        public List<ElGradebookLessonMark> ElGradebookLessonMarks { get; set; }
    }
}