using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Программа подготовки - Год начала подготовки"
    /// </summary>
    public class EduProgramEduYearBeginningTraining
    {
        public int EduProgramEduYearBeginningTrainingId { get; set; }

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
        /// Год начала подготовки 
        /// </summary>
        [Display(Name = "Год начала подготовки")]
        public EduYearBeginningTraining EduYearBeginningTraining { get; set; }
        /// <summary>
        /// Год начала подготовки 
        /// </summary>
        [Display(Name = "Год начала подготовки")]
        public int EduYearBeginningTrainingId { get; set; }
    }
}