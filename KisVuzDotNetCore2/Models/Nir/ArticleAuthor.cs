using KisVuzDotNetCore2.Models.UchPosobiya;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Статья - автор
    /// </summary>
    public class ArticleAuthor
    {
        public int ArticleAuthorId { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        [Display(Name ="Автор")]
        public Author Author { get; set; }
        /// <summary>
        /// Автор
        /// </summary>
        [Display(Name = "Автор")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Статья
        /// </summary>
        [Display(Name = "Статья")]
        public Article Article { get; set; }
        /// <summary>
        /// Статья
        /// </summary>
        [Display(Name = "Статья")]
        public int ArticleId { get; set; }

        /// <summary>
        /// Авторская доля
        /// </summary>
        [Display(Name = "Авторская доля, о.е.")]
        public decimal AuthorPart { get; set; }
    }
}