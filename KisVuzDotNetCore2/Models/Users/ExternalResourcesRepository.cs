using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    public class ExternalResourcesRepository : IExternalResourcesRepository
    {
        private AppIdentityDBContext _context;
        public ExternalResourcesRepository(AppIdentityDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет новый объект типа ExternalResource
        /// </summary>
        /// <param name="externalResource"></param>
        public void AddExternalResource(ExternalResource externalResource)
        {
            _context.ExternalResources.Add(externalResource);
            _context.SaveChanges();
        }

        public ExternalResource GetExternalResource(int id)
        {
            var entry = GetExternalResources().FirstOrDefault(e => e.ExternalResourceId == id);
            return entry;
        }

        /// <summary>
        /// Возвращает запрос на выборку
        /// всех объектов типа ExternalREsource
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExternalResource> GetExternalResources()
        {
            return _context.ExternalResources.Include(e => e.ExternalResourceType);
        }        
    }
}
