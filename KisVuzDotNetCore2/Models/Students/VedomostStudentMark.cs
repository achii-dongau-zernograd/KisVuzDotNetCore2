using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Students
{
    /// <summary>
    /// Модель "Отметка студента по ведомости"
    /// </summary>
    public class VedomostStudentMark
    {
        /// <summary>
        /// УИД отметки
        /// </summary>
        public int VedomostStudentMarkId { get; set; }

        /// <summary>
        /// Ведомость
        /// </summary>
        [Display(Name ="Ведомость")]
        public Vedomost Vedomost { get; set; }
        /// <summary>
        /// Ведомость
        /// </summary>
        [Display(Name = "Ведомость")]
        public int VedomostId { get; set; }

        /// <summary>
        /// Студент
        /// </summary>
        [Display(Name ="Студент")]
        public Student Student { get; set; }
        /// <summary>
        /// Студент
        /// </summary>
        [Display(Name = "Студент")]
        public int StudentId { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        [Display(Name = "Оценка")]
        public VedomostStudentMarkName VedomostStudentMarkName { get; set; }
        /// <summary>
        /// Оценка
        /// </summary>
        [Display(Name = "Оценка")]
        public int VedomostStudentMarkNameId { get; set; }
    }
}