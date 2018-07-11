using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель для реализации отношения М:М
    /// между моделями EduProgram и EduYear
    /// </summary>
    public class EduProgramEduYear
    {
        /// <summary>
        /// УИД
        /// </summary>
        public int EduProgramEduYearId { get; set; }

        /// <summary>
        /// Образовательная программа
        /// </summary>
        [Display(Name = "Образовательная программа")]
        public EduProgram EduProgram { get; set; }
        /// <summary>
        /// Образовательная программа
        /// </summary>
        [Display(Name = "Образовательная программа")]
        public int EduProgramId { get; set; }

        /// <summary>
        /// Учебный год
        /// </summary>
        [Display(Name = "Учебный год")]
        public EduYear EduYear { get; set; }
        /// <summary>
        /// Учебный год
        /// </summary>
        [Display(Name = "Учебный год")]
        public int EduYearId { get; set; }
    }
}