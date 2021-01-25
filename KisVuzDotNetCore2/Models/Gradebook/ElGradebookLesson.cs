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
        [Display(Name = "Дата занятия")]
        public DateTime Date { get; set; }

        /// <summary>
        /// УИД типа занятия
        /// </summary>
        [Display(Name = "Тип занятия")]
        public int ElGradebookLessonTypeId { get; set; }
        /// <summary>
        /// Тип занятия
        /// </summary>
        public ElGradebookLessonType ElGradebookLessonType { get; set; }

        /// <summary>
        /// Тема занятия
        /// </summary>
        [Display(Name = "Тема занятия")]
        public string LessonTheme { get; set; }

        /// <summary>
        /// Количество часов (для расчета выполненной нагрузки ППС, кафедры)
        /// </summary>
        [Range(0, 20)]
        [Display(Name = "Количество часов")]
        public double HoursNumber { get; set; } = 2;

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