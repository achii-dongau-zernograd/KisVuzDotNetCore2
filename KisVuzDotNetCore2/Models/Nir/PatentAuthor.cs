using KisVuzDotNetCore2.Models.UchPosobiya;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Патент - автор
    /// </summary>
    public class PatentAuthor
    {
        public int PatentAuthorId { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        [Display(Name = "Автор")]
        public Author Author { get; set; }
        /// <summary>
        /// Автор
        /// </summary>
        [Display(Name = "Автор")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Патент (свидетельство)
        /// </summary>
        [Display(Name = "Патент (свидетельство)")]
        public Patent Patent { get; set; }
        /// <summary>
        /// Патент (свидетельство)
        /// </summary>
        [Display(Name = "Патент (свидетельство)")]
        public int PatentId { get; set; }

        /// <summary>
        /// Авторская доля
        /// </summary>
        [Display(Name = "Авторская доля, о.е.")]
        public decimal AuthorPart { get; set; }
    }
}