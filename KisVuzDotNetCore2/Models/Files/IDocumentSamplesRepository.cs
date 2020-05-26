using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Files
{
    /// <summary>
    /// Интерфейс репозитория бланков и примеров заполнения документов
    /// </summary>
    public interface IDocumentSamplesRepository
    {
        /// <summary>
        /// Возвращает запрос на выборку бланков
        /// и примеров заполнения документов
        /// заданных типов
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <param name="soglasieNaObrabotkuPersonalnihDannih"></param>
        /// <returns></returns>
        IQueryable<DocumentSample> GetDocumentSamples(params FileDataTypeEnum[] fileDataTypes);

        /// <summary>
        /// Возвращает запрос на выборку бланков
        /// и примеров заполнения документов
        /// для указанного профиля заданных типов
        /// </summary>
        /// <param name="eduProfileId"></param>
        /// <param name="applicationForAdmission"></param>
        /// <returns></returns>
        IQueryable<DocumentSample> GetDocumentSamples(int eduProfileId, params FileDataTypeEnum[] fileDataTypes);

        /// <summary>
        /// Возвращает объект бланка или примера
        /// заполнения документов по УИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DocumentSample> GetDocumentSampleAsync(int documentSampleId);

        /// <summary>
        /// Добавляет бланк или пример заполнения
        /// </summary>
        /// <param name="documentSample"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task AddDocumentSampleAsync(DocumentSample documentSample, IFormFile uploadedFile);
        
        /// <summary>
        /// Удаляет бланк или пример заполнения документа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveDocumentSampleAsync(int id);
        
        /// <summary>
        /// Обновляет бланк или пример заполнения документа
        /// </summary>
        /// <param name="documentSample"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateDocumentSampleAsync(DocumentSample documentSample, IFormFile uploadedFile);
    }
}
