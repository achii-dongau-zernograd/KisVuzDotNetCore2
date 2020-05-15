using KisVuzDotNetCore2.Models.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Населённый пункт
    /// </summary>
    public class PopulatedLocality
    {
        public int PopulatedLocalityId { get; set; }

        /// <summary>
        /// Наименование населённого пункта
        /// </summary>
        [Display(Name = "Наименование населённого пункта")]
        public string PopulatedLocalityName { get; set; }

        /// <summary>
        /// Тип населённого пункта
        /// </summary>
        [Display(Name = "Тип населённого пункта")]
        public int PopulatedLocalityTypeId { get; set; }
        public PopulatedLocalityType PopulatedLocalityType { get; set; }


        /// <summary>
        /// Район, в котором расположен населённый пункт
        /// </summary>
        [Display(Name = "Район")]
        public int DistrictId { get; set; }
        public District District { get; set; }



        /// <summary>
        /// Адреса, расположенные в населённом пункте
        /// </summary>
        public List<Address> Addresses { get; set; }


        public string GetPopulatedLocalityFullName
        {
            get
            {
                return $"{District?.Region?.RegionName} - {District?.DistrictName} - {PopulatedLocalityType.PopulatedLocalityTypeNameShort} {PopulatedLocalityName}";
            }
        }
    }
}
