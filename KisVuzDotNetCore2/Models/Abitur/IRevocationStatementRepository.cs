using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Интерфейс репозитория заявлений об отзыве документов абитуриентов
    /// </summary>
    public interface IRevocationStatementRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку всех заявлений об отзыве документов абитуриентов
        /// </summary>
        /// <returns></returns>
        IQueryable<RevocationStatement> GetRevocationStatements();

        /// <summary>
        /// Добавляет заявление об отзыве документов
        /// </summary>
        /// <param name="revocationStatement"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task CreateRevocationStatement(RevocationStatement revocationStatement, IFormFile uploadedFile);

        /// <summary>
        /// Удаляет заявление об отзыве документов абитуриента по переданному УИД заявления
        /// </summary>
        /// <param name="revocationStatementId"></param>
        /// <returns></returns>
        Task RemoveRevocationStatementAsync(int revocationStatementId);

        /// <summary>
        /// Удаляет заявление об отзыве документов абитуриента
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Task RemoveRevocationStatementAsync(RevocationStatement entry);

        /// <summary>
        /// Удаляет заявление об отзыве документов абитуриента по переданному userName и УИД заявления
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="revocationStatementId"></param>
        /// <returns></returns>
        Task RemoveRevocationStatementAsync(string userName, int revocationStatementId);

        /// <summary>
        /// Обновляет заявление об отзыве документов абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="revocationStatement"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateRevocationStatement(string userName, RevocationStatement revocationStatement, IFormFile uploadedFile);
    }
}
