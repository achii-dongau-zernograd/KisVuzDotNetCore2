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
        /// Загружает на сервер документ об образовании и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <param name="typeOfEducationDocument">Тип документа об образовании</param>
        /// <returns></returns>
        Task<UserDocument> CreateEducationDocumentAsync(string userName, IFormFile uploadedFile, FileDataTypeEnum typeOfEducationDocument);
        
        /// <summary>
        /// Возвращает запрос на выборку всех пользовательских документов
        /// </summary>
        /// <returns></returns>
        IQueryable<UserDocument> GetUserDocuments();

        /// <summary>
        /// Загружает на сервер паспорт и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<UserDocument> CreatePassport(string userName, IFormFile uploadedFile);

        /// <summary>
        /// Загружает на сервер карточку абитуриента и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<UserDocument> CreateAbiturientCard(string userName, IFormFile uploadedFile);
        
        /// <summary>
        /// Загружает на сервер фотографию абитуриента и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<UserDocument> CreatePhoto(string userName, IFormFile uploadedFile);

        /// <summary>
        /// Загружает на сервер скан-копию СНИЛС и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<UserDocument> CreateSNILS(string userName, IFormFile uploadedFile);

        /// <summary>
        /// Удаляет документ пользователя
        /// </summary>
        /// <param name="userDocumentId">УИД документа пользователя</param>
        /// <returns></returns>
        Task RemoveUserDocumentAsync(int userDocumentId);
        
        /// <summary>
        /// Проверяет наличие у пользователя документов указанного типа
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="fileDataType"></param>
        /// <returns></returns>
        bool IsLoadedUserDocument(string userName, FileDataTypeEnum fileDataType);

        /// <summary>
        /// Проверяет наличие у пользователя документов указанного типа
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="fileDataType"></param>
        /// <returns></returns>
        bool IsLoadedUserDocument(AppUser appUser, FileDataTypeEnum fileDataType);
        
        /// <summary>
        /// Обновляет документ пользователя
        /// </summary>
        /// <param name="userDocument"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateUserDocument(UserDocument userDocument, IFormFile uploadedFile);

        /// <summary>
        /// Проверяет наличие у пользователя документов указанной группы типов
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="fileDataTypeGroup"></param>
        /// <returns></returns>
        bool IsLoadedUserDocument(string userName, FileDataTypeGroupEnum fileDataTypeGroup);

        /// <summary>
        /// Проверяет наличие у пользователя документов указанной группы типов
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="fileDataTypeGroup"></param>
        /// <returns></returns>
        bool IsLoadedUserDocument(AppUser appUser, FileDataTypeGroupEnum fileDataTypeGroup);

        

        /// <summary>
        /// Устанавливает замечание к документу
        /// </summary>
        /// <param name="userDocument"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task SetUserDocumentRemarkAsync(UserDocument userDocument, string remark);
        
        /// <summary>
        /// Возвращает документ пользователя
        /// </summary>
        /// <param name="userDocumentId"></param>
        /// <returns></returns>
        Task<UserDocument> GetUserDocumentAsync(int userDocumentId);
                
        /// <summary>
        /// Проверяет наличие у пользователя сведений об образовании
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsUserEducationDataExistsAsync(string userName);

        /// <summary>
        /// Проверяет наличие у пользователя сведений об образовании
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        Task<bool> IsUserEducationDataExistsAsync(AppUser appUser);
        
        /// <summary>
        /// Возвращает запрос на выборку документов об образовании пользователя, не имеющих заполненных сведений
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<UserDocument> GetUserEducationDocumentsWithoutUserEducationDataAsync(string userName);
                
        /// <summary>
        /// Возвращает первый объект документа пользователя,
        /// который является паспортом и не связан с объектом паспортных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<UserDocument> GetUserDocumentPassportWithoutPassportDataAsync(string userName);

        /// <summary>
        /// Заменяет пользовательский документ
        /// </summary>
        /// <param name="userDocumentId"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task ReloadUserDocumentAsync(int userDocumentId, IFormFile uploadedFile);
        
        /// <summary>
        /// Удаляет все документы пользователя
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        Task RemoveUserDocumentsAsync(AppUser appUser);
        
    }
}
