using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Регион (область, край и пр.)
    /// </summary>
    public class Region
    {
        public int RegionId { get; set; }

        /// <summary>
        /// Наименование региона
        /// </summary>
        public string RegionName { get; set; }


        /// <summary>
        /// Страна
        /// </summary>
        [Display(Name = "Страна")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        /// <summary>
        /// Районы
        /// </summary>
        [Display(Name = "Районы")]
        public List<District> Districts { get; set; }
    }
}
