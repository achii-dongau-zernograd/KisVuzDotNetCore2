using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель "Трудоустройство"
    /// </summary>
    public class GraduateTrudoustroustvo
    {
        /// <summary>
        /// Количество трудоустроенных выпускников
        /// </summary>
        public int GraduateTrudoustroustvoId { get; set; }

        [Display(Name = "Профиль подготовки")]
        public EduProfile EduProfile { get; set; }
        public int EduProfileId { get; set; }

        [Display(Name = "Наименование года выпуска")]
        public GraduateYear GraduateYearName { get; set; }
        public int GraduateYearId { get; set; }

        [Display(Name = "Количество трудоустроенных выпускников")]
        public int GraduateTrudoustroustvoNumber { get; set; }
    }
}