using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Интерфейс репозитория договоров
    /// </summary>
    public interface IContractRepository
    {
        /// <summary>
        /// Создаёт договор
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task CreateContractAsync(Contract contract, IFormFile uploadedFile);
        
        /// <summary>
        /// Возвращает запрос на выборку всех договоров с заполненными зависимостями
        /// </summary>
        /// <returns></returns>
        IQueryable<Contract> GetContracts();
    }
}
