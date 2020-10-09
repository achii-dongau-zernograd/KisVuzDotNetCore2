using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Интерфейс внешних ресурсов пользователя
    /// </summary>
    public interface IExternalResourcesRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку всех объектов типа ExternalResource
        /// </summary>
        /// <returns></returns>
        IQueryable<ExternalResource> GetExternalResources();
                
        /// <summary>
        /// Добавляет новый объект типа ExternalResource
        /// </summary>
        /// <param name="externalResource"></param>
        void AddExternalResource(ExternalResource externalResource);
        ExternalResource GetExternalResource(int id);                
    }
}
