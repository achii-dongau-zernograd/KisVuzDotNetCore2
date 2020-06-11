using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Интерфейс репозитория обработки платежей
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Добавляет платёж
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task AddPaymentAsync(Payment payment, IFormFile uploadedFile);
    }
}
