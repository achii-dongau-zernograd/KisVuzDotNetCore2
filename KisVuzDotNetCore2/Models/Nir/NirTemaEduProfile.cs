using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель связи TemaNir и EduProfiles
    /// </summary>
    public class NirTemaEduProfile
    {
        /// <summary>
        /// Идентификатор темы TemaNir и EduProfiles
        /// </summary>
        public int NirTemaEduProfileId { get; set; }

        /// <summary>
        /// Тема НИР
        /// </summary>
        [Display(Name = "Тема НИР")]
        public NirTema NirTema { get; set; }
        public int NirTemaId { get; set; }

        /// <summary>
        /// Профиль подготовки
        /// </summary>
        [Display(Name ="Профиль подготовки")]
        public EduProfile EduProfile { get; set; }
        public int EduProfileId { get; set; }
    }
}
