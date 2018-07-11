using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель для реализации отношения М:М
    /// между моделями EduProgram и EduForm
    /// </summary>
    public class EduProgramEduForm
    {
        /// <summary>
        /// УИД модели EduProgramEduForm
        /// </summary>
        public int EduProgramEduFormId { get; set; }

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
        /// Форма обучения
        /// </summary>
        [Display(Name ="Форма обучения")]
        public EduForm EduForm { get; set; }
        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name = "Форма обучения")]
        public int EduFormId { get; set; }
    }
}