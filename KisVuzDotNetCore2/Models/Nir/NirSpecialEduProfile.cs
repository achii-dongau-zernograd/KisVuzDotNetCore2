using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Модель "Специальность научных работников, согласно номенклатуре - Профиль подготовки"
    /// </summary>
    public class NirSpecialEduProfile
    {
        public int NirSpecialEduProfileId { get; set; }

        /// <summary>
        /// Специальность научных работников, согласно номенклатуре
        /// </summary>
        [Display(Name = "Специальность научных работников, согласно номенклатуре")]
        public NirSpecial NirSpecial { get; set; }
        /// <summary>
        /// Специальность научных работников, согласно номенклатуре
        /// </summary>
        [Display(Name = "Специальность научных работников, согласно номенклатуре")]
        public int NirSpecialId { get; set; }

        /// <summary>
        /// Профиль подготовки
        /// </summary>
        [Display(Name = "Профиль подготовки")]
        public EduProfile EduProfile { get; set; }
        /// <summary>
        /// Профиль подготовки
        /// </summary>
        [Display(Name = "Профиль подготовки")]
        public int EduProfileId { get; set; }
    }
}