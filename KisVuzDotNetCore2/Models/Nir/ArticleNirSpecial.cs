using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Статья - Специальность научных работников, согласно номенклатуре
    /// </summary>
    public class ArticleNirSpecial
    {
        public int ArticleNirSpecialId { get; set; }

        /// <summary>
        /// Научная статья
        /// </summary>
        [Display(Name = "Статья")]
        public Article Article { get; set; }

        /// <summary>
        /// Научная статья
        /// </summary>
        [Display(Name ="Статья")]
        public int ArticleId { get; set; }

        /// <summary>
        /// Специальность научных работников, согласно номенклатуре
        /// </summary>
        [Display(Name ="Научная специальность" )]
        public NirSpecial NirSpecial { get; set; }

        /// <summary>
        /// Специальность научных работников, согласно номенклатуре
        /// </summary>
        [Display(Name = "Научная специальность")]
        public int NirSpecialId { get; set; }
    }
}