using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Репозиторий населённых пунктов
    /// </summary>
    public class PopulatedLocalityRepository : IPopulatedLocalityRepository
    {
        public readonly AppIdentityDBContext _context;
        
        public PopulatedLocalityRepository(AppIdentityDBContext context)
        {
            _context = context;
        }       

        /// <summary>
        /// Возвращает запрос на выборку населённых пунктов
        /// </summary>
        /// <returns></returns>
        public IQueryable<PopulatedLocality> GetPopulatedLocalities()
        {
            var query = _context.PopulatedLocalities
                .Include(pl => pl.PopulatedLocalityType)
                .Include(pl => pl.District.GpsGeometryCenter)
                .Include(pl => pl.District.Region.Country);

            return query;
        }
    }
}
