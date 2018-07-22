using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Модель "Преподаватель, ведущий дисциплину"
    /// (для заполнения таблицы 16 "Информация о составе педагогических работников")
    /// Реализует связь М:М между Teacher и Discipline
    /// </summary>
    public class TeacherDiscipline
    {
        /// <summary>
        /// УИД записи
        /// </summary>
        public int TeacherDisciplineId { get; set; }

        /// <summary>
        /// Учебный год (по учебному плану)
        /// </summary>
        [Display(Name = "Учебный год (по учебному плану)")]
        public EduPlanEduYear EduPlanEduYear { get; set; }
        /// <summary>
        /// Учебный год (по учебному плану)
        /// </summary>
        [Display(Name = "Учебный год (по учебному плану)")]
        public int EduPlanEduYearId { get; set; }

        /// <summary>
        /// Преподаватель, ведущий дисциплину
        /// </summary>
        [Display(Name = "Преподаватель, ведущий дисциплину")]
        public Teacher Teacher { get; set; }
        /// <summary>
        /// Преподаватель, ведущий дисциплину
        /// </summary>
        [Display(Name = "Преподаватель, ведущий дисциплину")]
        public int TeacherId { get; set; }

        /// <summary>
        /// Дисциплина
        /// </summary>
        [Display(Name ="Дисциплина")]
        public Discipline Discipline { get; set; }
        /// <summary>
        /// Дисциплина
        /// </summary>
        [Display(Name = "Дисциплина")]
        public int DisciplineId { get; set; }        
    }
}