using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Электронный журнал. Учебное занятие
    /// </summary>
    public class ElGradebookLesson
    {
        /// <summary>
        /// УИД занятия
        /// </summary>
        public int ElGradebookLessonId { get; set; }

        /// <summary>
        /// Дата занятия
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        /// <summary>
        /// УИД типа занятия
        /// </summary>
        public int ElGradebookLessonTypeId { get; set; }
        /// <summary>
        /// Тип занятия
        /// </summary>
        public ElGradebookLessonType ElGradebookLessonType { get; set; }

        /// <summary>
        /// Тема занятия
        /// </summary>
        public string LessonTheme { get; set; }

        /// <summary>
        /// УИД электронного журнала, которому принадлежит занятие
        /// </summary>
        public int ElGradebookId { get; set; }
        public ElGradebook ElGradebook { get; set; }

        /// <summary>
        /// Оценки за занятие
        /// </summary>
        public List<ElGradebookLessonMark> ElGradebookLessonMarks { get; set; }
    }
}