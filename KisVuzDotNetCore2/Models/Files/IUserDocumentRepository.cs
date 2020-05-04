using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KisVuzDotNetCore2.Models.Files
{
    /// <summary>
    /// Интерфейс документов пользователей
    /// </summary>
    public interface IUserDocumentRepository
    {
        /// <summary>
        /// Загружает на сервер согласие на обработку персональных данных и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<UserDocument> CreateApplicationForProcessingPersonalDataAsync(string userName, IFormFile uploadedFile);

        /// <summary>
        /// Удаляет документ пользователя
        /// </summary>
        /// <param name="userDocumentId">УИД документа пользователя</param>
        /// <returns></returns>
        Task RemoveUserDocumentAsync(int userDocumentId);

        /// <summary>
        /// Проверяет наличие у пользователя наличия документов указанного типа
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="fileDataType"></param>
        /// <returns></returns>
        bool IsLoadedUserDocument(string userName, FileDataTypeEnum fileDataType);

        /// <summary>
        /// Проверяет наличие у пользователя наличия документов указанной группы типов
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="fileDataTypeGroup"></param>
        /// <returns></returns>
        bool IsLoadedUserDocument(string userName, FileDataTypeGroupEnum fileDataTypeGroup);
    }
}
