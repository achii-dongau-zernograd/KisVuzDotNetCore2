using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Статья - Тема НИР
    /// </summary>
    public class ArticleNirTema
    {
        public int ArticleNirTemaId { get; set; }

        /// <summary>
        /// Статья
        /// </summary>
        [Display(Name ="Статья")]
        public Article Article { get; set; }
        /// <summary>
        /// Статья
        /// </summary>
        [Display(Name = "Статья")]
        public int ArticleId { get; set; }

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