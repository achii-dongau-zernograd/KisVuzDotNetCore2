using KisVuzDotNetCore2.Models.Nir;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    public class Author
    {
        /// <summary>
        /// Справочник авторов учебных пособий
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Ф.И.О. автора 
        /// </summary>
        [Display(Name = "Ф.И.О. автора ")]
        public string AuthorName { get; set; }

        /// <summary>
        /// Аккаунт пользователя
        /// </summary>
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Список учебных пособий, принадлежащих автору
        /// </summary>
        [Display(Name = "Учебные пособия")]
        public List<UchPosobieAuthor> UchPosobieAuthors { get; set; }

        /// <summary>
        /// Список научных статей, принадлежащих автору
        /// </summary>
        [Display(Name = "Научные статьи")]
        public List<ArticleAuthor> ArticleAuthors { get; set; }

        /// <summary>
        /// Список патентов (свидетельств), принадлежащих автору
        /// </summary>
        [Display(Name = "Патенты (свидетельства)")]
        public List<PatentAuthor> PatentAuthors { get; set; }
    }
}