namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Электронные журнылы. Оценка за учебное занятие
    /// </summary>
    public class ElGradebookLessonMark
    {
        /// <summary>
        /// УИД оценки за учебное занятие
        /// </summary>
        public int ElGradebookLessonMarkId { get; set; }

        /// <summary>
        /// УИД учебного занятия
        /// </summary>
        public int ElGradebookLessonId { get; set; }
        /// <summary>
        /// Учебное занятие
        /// </summary>
        public ElGradebookLesson ElGradebookLesson { get; set; }


        /// <summary>
        /// УИД студента группы
        /// </summary>
        public int ElGradebookGroupStudentId { get; set; }
        /// <summary>
        /// Студент группы
        /// </summary>
        public ElGradebookGroupStudent ElGradebookGroupStudent { get; set; }


        /// <summary>
        /// УИД типа посещаемости
        /// </summary>
        public int ElGradebookLessonAttendanceTypeId { get; set; }
        /// <summary>
        /// Посещаемость
        /// </summary>
        public ElGradebookLessonAttendanceType ElGradebookLessonAttendanceType { get; set; }

        /// <summary>
        /// Количество баллов
        /// </summary>
        public int PointsNumber { get; set; }
    }
}