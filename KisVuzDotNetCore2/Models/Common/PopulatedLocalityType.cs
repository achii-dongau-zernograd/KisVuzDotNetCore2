using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Тип населённого пункта
    /// </summary>
    public class PopulatedLocalityType
    {
        public int PopulatedLocalityTypeId { get; set; }

        /// <summary>
        /// Наименование типа населённого пункта
        /// </summary>
        [Display(Name = "Тип населённого пункта")]
        public string PopulatedLocalityTypeName { get; set; }

        /// <summary>
        /// Сокращённое наименование типа населённого пункта
        /// </summary>
        [Display(Name = "Сокращённое наименование")]
        public string PopulatedLocalityTypeNameShort { get; set; }

        /// <summary>
        /// Список населённых пунктов данного типа
        /// </summary>
        public List<PopulatedLocality> PopulatedLocalities { get; set; }
    }
}
