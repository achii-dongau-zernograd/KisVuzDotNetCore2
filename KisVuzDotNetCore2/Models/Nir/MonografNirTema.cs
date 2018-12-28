using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Монография - Тема НИР
    /// </summary>
    /// 
    public class MonografNirTema
    {
        public int MonografNirTemaId { get; set; }

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