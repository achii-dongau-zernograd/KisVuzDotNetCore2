using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Страна
    /// </summary>
    public class Country
    {
        public int CountryId { get; set; }

        /// <summary>
        /// Наименование страны
        /// </summary>
        [Display(Name = "Страна")]
        public string CountryName { get; set; }

        /// <summary>
        /// Регионы страны (области, края и пр.)
        /// </summary>
        public List<Region> Regions { get; set; }
    }
}
