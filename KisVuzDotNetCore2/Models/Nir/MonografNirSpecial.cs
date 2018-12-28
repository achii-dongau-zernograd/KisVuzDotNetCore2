using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Монография - Специальность научных работников, согласно номенклатуре
    /// </summary>

    public class MonografNirSpecial
    {
        public int MonografNirSpecialId { get; set; }

        /// <summary>
        /// Монография
        /// </summary>
        [Display(Name = "Монография")]
        public Monograf Monograf { get; set; }
        /// <summary>
        /// Монография
        /// </summary>
        [Display(Name = "Монография")]
        public int MonografId { get; set; }

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