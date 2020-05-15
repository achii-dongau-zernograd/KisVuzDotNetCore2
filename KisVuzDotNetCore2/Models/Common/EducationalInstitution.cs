using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Образовательное учреждение
    /// </summary>
    public class EducationalInstitution
    {
        public int EducationalInstitutionId { get; set; }

        /// <summary>
        /// Официальное наименование учебного заведения
        /// </summary>
        public string EducationalInstitutionName { get; set; }

        //////////////////////////////////////////////////////
        /// <summary>
        /// Тип учебного заведения
        /// </summary>
        public int EducationalInstitutionTypeId { get; set; }

        /// <summary>
        /// Тип учебного заведения
        /// </summary>
        public EducationalInstitutionType EducationalInstitutionType { get; set; }
        ////////////////////////////////////////////////////////

        /// <summary>
        /// Расположение
        /// </summary>
        public int? LocationId { get; set; }
        /// <summary>
        /// Расположение
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Возвращает наименование учебного заведения с местом расположения
        /// </summary>
        public string GetEducationalInstitutionNameAndSettlement
        {
            get
            {
                string result = EducationalInstitutionName;
                if (Location != null &&
                    Location.Address != null &&
                    Location.Address.PopulatedLocality != null &&
                    !string.IsNullOrWhiteSpace(Location.Address.PopulatedLocality.PopulatedLocalityName))
                {
                    result += $" ({Location.Address.PopulatedLocality.PopulatedLocalityType.PopulatedLocalityTypeNameShort} {Location.Address.PopulatedLocality.PopulatedLocalityName})";
                }
                return result;
            }
        }
    }
}
