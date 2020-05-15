using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Перечисление типов образовательных организаций
    /// </summary>
    public enum EducationalInstitutionTypeEnum
    {
        /// <summary>
        /// Общеобразовательная организация
        /// </summary>
        ObsheobrazovatelnayaOrganizaciya = 1,
        /// <summary>
        /// Профессиональная образовательная организация
        /// </summary>
        ProfessionalnayaObrazovatelnayaOrganizaciya = 2,
        /// <summary>
        /// Образовательная организация высшего образования
        /// </summary>
        ObrazovatelnayaOrganizaciyaVisshegoObrazovaniya = 3
    }
}
