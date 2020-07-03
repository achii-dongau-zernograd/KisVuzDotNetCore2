using KisVuzDotNetCore2.Models.Priem;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Интерфейс заявления о согласии на зачисление
    /// </summary>
    public interface IConsentToEnrollmentRepository
    {
        /// <summary>
        /// Создание заявления о согласии на зачисление
        /// </summary>
        /// <param name="consentToEnrollment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task CreateConsentToEnrollmentAsync(ConsentToEnrollment consentToEnrollment, IFormFile uploadedFile);

        /// <summary>
        /// Возвращает объект заявления о согласии на зачисление
        /// </summary>
        /// <param name="consentToEnrollmentId"></param>
        /// <returns></returns>
        Task<ConsentToEnrollment> GetConsentToEnrollmentAsync(int consentToEnrollmentId);

        /// <summary>
        /// Возвращает запрос на выборку всех объектов заявлений о согласии на зачисление
        /// </summary>
        /// <returns></returns>
        IQueryable<ConsentToEnrollment> GetConsentToEnrollments();

        /// <summary>
        /// Возвращает запрос на выборку всех объектов заявлений о согласии на зачисление,
        /// удовлетворяющих заданному фильтру
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <returns></returns>
        IQueryable<ConsentToEnrollment> GetConsentToEnrollments(ConsentToEnrollmentsFilterAndSortModel filterAndSortModel);

        /// <summary>
        /// Удаляет заявление о согласии на зачисление
        /// </summary>
        /// <param name="consentToEnrollment"></param>
        /// <returns></returns>
        Task RemoveConsentToEnrollmentAsync(ConsentToEnrollment consentToEnrollment);

        /// <summary>
        /// Обновляет заявление о согласии на зачисление
        /// </summary>
        /// <param name="consentToEnrollment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateConsentToEnrollmentAsync(ConsentToEnrollment consentToEnrollment, IFormFile uploadedFile);
    }
}
