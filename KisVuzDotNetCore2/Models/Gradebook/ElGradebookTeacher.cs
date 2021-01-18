namespace KisVuzDotNetCore2.Models.Gradebook
{
    /// <summary>
    /// Преподаватель
    /// </summary>
    public class ElGradebookTeacher
    {
        /// <summary>
        /// УИД преподавателя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ФИО преподавателя
        /// </summary>
        public string TeacherFio { get; set; }

        /// <summary>
        /// УИД аккаунта преподавателя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// УИД журнала
        /// </summary>
        public int ElGradebookId { get; set; }
        /// <summary>
        /// Журнал
        /// </summary>
        public ElGradebook ElGradebook { get; set; }
    }
}