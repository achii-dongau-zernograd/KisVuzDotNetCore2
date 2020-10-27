using KisVuzDotNetCore2.Models.Sveden;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.MTO
{
    public class PomeshenieRepository : IPomeshenieRepository
    {
        private readonly AppIdentityDBContext _context;

        public PomeshenieRepository(AppIdentityDBContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Возвращает запрос на выборку всех помещений
        /// </summary>
        /// <returns></returns>
        public IQueryable<Pomeshenie> GetPomeshenies()
        {
            var query = _context.Pomeshenie
                .Include(p => p.PomeshenieTypes)
                    .ThenInclude(pt => pt.PomeshenieType)
                .Include(p => p.OborudovanieList)
                .Include(p => p.Korpus.KorpusAddress)
                .Include(p => p.StructSubvision);

            return query;
        }


        /// <summary>
        /// Возвращает запрос на выборку всех объектов для проведения практических занятий
        /// </summary>
        /// <returns></returns>
        public IQueryable<Pomeshenie> GetObjectsForPractLessons()
        {
            var query = GetPomeshenies();
            return query;
        }

        

        /// <summary>
        /// Возвращает запрос на выборку всех учебных кабинетов
        /// </summary>
        /// <returns></returns>
        public IQueryable<Pomeshenie> GetUchCabinets()
        {
            var query = GetPomeshenies();
            return query;
        }
    }
}
