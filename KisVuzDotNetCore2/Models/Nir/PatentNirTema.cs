using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Патент - Тема НИР
    /// </summary>
    public class PatentNirTema
    {
        public int PatentNirTemaId { get; set; }

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
        /// Тема НИР
        /// </summary>
        [Display(Name = "Тема НИР")]
        public NirTema NirTema { get; set; }
        /// <summary>
        /// Тема НИР
        /// </summary>
        [Display(Name = "Тема НИР")]
        public int NirTemaId { get; set; }
    }
}