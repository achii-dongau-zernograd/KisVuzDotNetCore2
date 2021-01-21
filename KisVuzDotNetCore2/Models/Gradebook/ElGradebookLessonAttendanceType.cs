namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Электронные журналы. Тип посещаемости занятий
    /// </summary>
    public class ElGradebookLessonAttendanceType
    {
        /// <summary>
        /// УИД типа посещаемости
        /// </summary>
        public int ElGradebookLessonAttendanceTypeId { get; set; }

        /// <summary>
        /// Наименование типа посещаемости
        /// </summary>
        public string ElGradebookLessonAttendanceTypeName { get; set; }
    }
}