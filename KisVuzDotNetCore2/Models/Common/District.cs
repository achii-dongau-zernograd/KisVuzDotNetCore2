using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Район (находится в составе региона страны)
    /// </summary>
    public class District
    {
        public int DistrictId { get; set; }

        /// <summary>
        /// Наименование района
        /// </summary>
        [Display(Name = "Наименование района")]
        public string DistrictName { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        [Display(Name = "Регион")]
        public int RegionId { get; set; }
        public Region Region { get; set; }

        /// <summary>
        /// GPS-координаты геометрического центра района
        /// </summary>
        public int? GpsGeometryCenterId { get; set; }
        public GpsCoordinate GpsGeometryCenter { get; set; }

        /// <summary>
        /// Населённые пункты
        /// </summary>
        public List<PopulatedLocality> PopulatedLocalities { get; set; }
    }
}
