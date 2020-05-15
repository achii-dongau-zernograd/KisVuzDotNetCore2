using KisVuzDotNetCore2.Models.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Расположение (адрес и координаты)
    /// </summary>
    public class Location
    {
        public int LocationId { get; set; }

        /// <summary>
        /// GPS-координаты
        /// </summary>
        [Display(Name = "GPS-координаты")]
        public int? GpsCoordinateId { get; set; }
        public GpsCoordinate GpsCoordinate { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Display(Name = "Адрес")]
        public int? AddressId { get; set; }
        public Address Address { get; set; }

        /// <summary>
        /// Образовательные организации
        /// </summary>
        public List<EducationalInstitution> EducationalInstitutions { get; set; }
    }
}
