using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Интерфейс репозитория образовательных организаций
    /// </summary>
    public interface IEducationalInstitutionRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку всех образовательных учреждений
        /// </summary>
        /// <returns></returns>
        IQueryable<EducationalInstitution> GetEducationalInstitutions();
    }
}
