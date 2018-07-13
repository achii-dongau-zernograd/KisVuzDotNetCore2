using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    /// <summary>
    /// Связь между Uchposobie (учебное пособие) и Author (автор)
    /// </summary>
    public class UchPosobieAuthor
    {
      
        /// <summary>
        /// Связь между Uchposobie (учебное пособие) и Author (автор)
        /// </summary>
        public int UchPosobieAuthorId { get; set; }

        /// <summary>
        /// Автор пособия
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// Автор пособия
        /// </summary>
        [Display(Name = "Учебное пособие")]
        public Author Author { get; set; }

        /// <summary>
        /// Учебное пособие
        /// </summary>
        public int UchPosobieId { get; set; }
        /// <summary>
        /// Учебное пособие
        /// </summary>
        [Display(Name = "Учебное пособие")]
        public UchPosobie UchPosobie { get; set; }

    }
}
