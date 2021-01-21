namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Электронные журналы. Тип занятия
    /// </summary>
    public class ElGradebookLessonType
    {
        /// <summary>
        /// УИД типа занятия
        /// </summary>
        public int ElGradebookLessonTypeId { get; set; }

        /// <summary>
        /// Наименование типа занятия
        /// </summary>
        public string ElGradebookLessonTypeName { get; set; }
    }
}