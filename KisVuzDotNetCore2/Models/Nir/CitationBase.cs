using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// База цитирования
    /// </summary>
    public class CitationBase
    {
        public int CitationBaseId { get; set; }
        
        /// <summary>
        /// Наименование базы цитирования
        /// </summary>
        [Display(Name = "Наименование базы цитирования")]
        public string CitationBaseName { get; set; }
    }
}