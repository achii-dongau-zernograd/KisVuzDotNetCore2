using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Пользователь - иностранный язык
    /// </summary>
    public class AppUserForeignLanguage
    {
        public int AppUserForeignLanguageId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Иностранный язык
        /// </summary>
        [Display(Name = "Иностранный язык")]
        public int ForeignLanguageId { get; set; }
        public ForeignLanguage ForeignLanguage { get; set; }
    }
}
