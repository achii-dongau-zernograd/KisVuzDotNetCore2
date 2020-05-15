using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Тип образовательной организации
    /// </summary>
    public class EducationalInstitutionType
    {
        public int EducationalInstitutionTypeId { get; set; }
        
        /// <summary>
        /// Наименование типа образовательной организации
        /// </summary>
        public string EducationalInstitutionTypeName { get; set; }

        /// <summary>
        /// Образовательные организации
        /// </summary>
        public List<EducationalInstitution> EducationalInstitutions { get; set; }
    }
}
