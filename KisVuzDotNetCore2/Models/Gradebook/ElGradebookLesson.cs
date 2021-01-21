using System;
using System.Collections.Generic;

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
        /// Оценки за занятие
        /// </summary>
        public List<ElGradebookLessonMark> ElGradebookLessonMarks { get; set; }
    }
}