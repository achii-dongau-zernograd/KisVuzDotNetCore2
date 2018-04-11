using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель "Ученое звание"
    /// </summary>
    public class AcademicStat
    {
        public int AcademicStatId { get; set; }

        /// <summary>
        /// Наименование учёного звания
        /// </summary>
        [Display(Name = "Наименование учёного звания")]
        public string AcademicStatName { get; set; }
    }
}