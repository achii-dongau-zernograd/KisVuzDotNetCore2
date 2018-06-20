using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    public class HostelInfo
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "Идентификатор")]
        public int Id { get; set; }

        /// <summary>
        /// Наименование показателя
        /// </summary>
        [Display(Name = "Наименование показателя")]
        public string NameIndicator { get; set; }

        /// <summary>
        /// Itemprop
        /// </summary>
        [Display(Name = "Itemprop")]
        public string Itemprop { get; set; }

        /// <summary>
        /// Значение
        /// </summary>        
        [Display(Name = "Значение")]
        public string Value { get; set; }

        /// <summary>
        /// Ссылка
        /// </summary>        
        [Display(Name = "Ссылка")]
        public string Link { get; set; }

    }
}
