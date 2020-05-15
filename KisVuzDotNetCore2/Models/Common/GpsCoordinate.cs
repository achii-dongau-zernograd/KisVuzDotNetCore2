using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Координаты GPS
    /// </summary>
    public class GpsCoordinate
    {
        public int GpsCoordinateId { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Места, расположенные по данным координатам
        /// </summary>
        public List<Location> Locations { get; set; }
    }
}
