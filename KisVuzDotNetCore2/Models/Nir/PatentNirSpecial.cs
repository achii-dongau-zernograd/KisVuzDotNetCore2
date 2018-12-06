using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Патент - Специальность научных работников, согласно номенклатуре
    /// </summary>
    public class PatentNirSpecial
    {
        public int PatentNirSpecialId { get; set; }

        /// <summary>
        /// Патент (свидетельство)
        /// </summary>
        [Display(Name = "Патент (свидетельство)")]
        public Patent Patent { get; set; }
        /// <summary>
        /// Статья
        /// </summary>
        [Display(Name = "Патент (свидетельство)")]
        public int PatentId { get; set; }

        /// <summary>
        /// Специальность научных работников, согласно номенклатуре
        /// </summary>
        [Display(Name = "Научная специальность")]
        public NirSpecial NirSpecial { get; set; }

        /// <summary>
        /// Специальность научных работников, согласно номенклатуре
        /// </summary>
        [Display(Name = "Научная специальность")]
        public int NirSpecialId { get; set; }
    }
}