using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель "Ученая степень"
    /// </summary>
    public class AcademicDegree
    {
        public int AcademicDegreeId { get; set; }

        /// <summary>
        /// Наименование учёной степени
        /// </summary>
        [Display(Name = "Наименование учёной степени")]
        public string AcademicDegreeName { get; set; }

        /// <summary>
        /// Группа ученых степеней
        /// </summary>
        [Display(Name = "Группа ученых степеней")]
        public AcademicDegreeGroup AcademicDegreeGroup{get;set;}
        public int AcademicDegreeGroupId { get; set; }
    }
}