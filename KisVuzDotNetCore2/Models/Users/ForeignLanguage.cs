using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Иностранный язык
    /// </summary>
    public class ForeignLanguage
    {
        public int ForeignLanguageId { get; set; }

        /// <summary>
        /// Наименование иностранного языка
        /// </summary>
        [Display(Name ="Иностранный язык")]
        public string ForeignLanguageName { get; set; }
    }
}
