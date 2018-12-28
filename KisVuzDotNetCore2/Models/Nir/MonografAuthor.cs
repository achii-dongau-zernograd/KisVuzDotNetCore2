using KisVuzDotNetCore2.Models.UchPosobiya;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Монография - автор
    /// </summary>

    public class MonografAuthor
    {
        
            public int MonografAuthorId { get; set; }

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
            /// Авторская доля
            /// </summary>
            [Display(Name = "Авторская доля, о.е.")]
            public decimal AuthorPart { get; set; }
        }
    }
