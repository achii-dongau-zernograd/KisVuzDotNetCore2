using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Репозиторий образовательных организаций
    /// </summary>
    public class EducationalInstitutionRepository : IEducationalInstitutionRepository
    {
        private readonly AppIdentityDBContext _context;

        public EducationalInstitutionRepository(AppIdentityDBContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Возвращает запрос на выборку всех образовательных учреждений
        /// </summary>
        /// <returns></returns>
        public IQueryable<EducationalInstitution> GetEducationalInstitutions()
        {
            var query = _context.EducationalInstitutions
                .Include(i => i.EducationalInstitutionType)
                .Include(i => i.Location)
                    .ThenInclude(l => l.GpsCoordinate)
                .Include(i => i.Location)
                    .ThenInclude(l => l.Address)
                        .ThenInclude(a => a.PopulatedLocality)
                            .ThenInclude(pl => pl.PopulatedLocalityType)
                .Include(i => i.Location)
                    .ThenInclude(l => l.Address)
                        .ThenInclude(a => a.PopulatedLocality)
                            .ThenInclude(pl => pl.District)
                                .ThenInclude(d => d.GpsGeometryCenter)
                .Include(i => i.Location)
                    .ThenInclude(l => l.Address.PopulatedLocality.District.Region.Country)
                .OrderBy(i => i.EducationalInstitutionName);

            return query;                                                   
        }
    }
}
