using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Nir;
using KisVuzDotNetCore2.Models.Students;
using Microsoft.AspNetCore.Http;

namespace KisVuzDotNetCore2.Models.Files
{
    public interface IFileModelRepository
    {
        /// <summary>
        /// Удаляет объект файл и файл на диске
        /// </summary>
        /// <param name="fileModelId"></param>
        Task RemoveFileAsync(int? fileModelId);

        /// <summary>
        /// Удаляет объект файл и файл на диске
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        Task RemoveFileModelAsync(FileModel fileModel);

        /// <summary>
        /// Загружает файл аннотации на диск
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadEduAnnotationAsync(IFormFile uploadedFile);
        
        /// <summary>
        /// Загружает файл скан-копии подтверждения платежа
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadPaymentAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл рабочей программы на диск
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadRabProgramAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл листа переутверждения рабочей программы
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadRabProgramListPereutverjdeniyaAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает заявление о согласии на зачисление
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadConsentToEnrollmentFileAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает заявление об отзыве документов
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadRevocationStatementAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл, подтверждающий льготу абитуриента при приёме
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadAdmissionPrivilegeFileAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает jpg-файл нового задания в базу заданий СДО
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadLmsTaskJpg(IFormFile uploadedFile);

        /// <summary>
        /// Загружает jpg-файл иллюстрации к варианту ответа на задание СДО
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadLmsTaskAnswerJpg(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл фонда оценочных средств на диск
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadFondOcenochnihSredstvAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл листа переутверждения фонда оценочных средств на диск
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadFondOcenochnihSredstvListPereutverjdeniyaAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает учебное пособие
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadUchPosobieAsync(IFormFile formFile, FileDataTypeEnum fileDataTypeEnum);

        /// <summary>
        /// Загружает договор с электронной библиотечной системой
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadElectronBiblSystemDogovorAsync(IFormFile uploadedFile);

        /// <summary>
        /// Возвращает количество файлов в папке files
        /// </summary>
        /// <returns></returns>
        Task<int> GetNumFilesInFileSystemAsync();

        /// <summary>
        /// Количество записей в таблице files базы данных
        /// </summary>
        /// <returns></returns>
        Task<int> GetNumFilesInDatabase();

        /// <summary>
        /// Загружает файл научной статьи
        /// </summary>
        /// <param name="article">Научная статья</param>
        /// <param name="uploadedFile">Загружаемый файл</param>
        /// <returns></returns>
        Task<FileModel> UploadArticleAsync(Article article, IFormFile uploadedFile);

        /// <summary>
        /// Загружайт скан-копию файла, подтверждающего индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadIndividualAchievmentFile(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл патента (свидетельства)
        /// </summary>
        /// <param name="patent">Патент (свидетельство)</param>
        /// <param name="uploadedFile">Загружаемый файл</param>
        /// <returns></returns>
        Task<FileModel> UploadPatentAsync(Patent patent, IFormFile uploadedFile);
        
        /// <summary>
        /// Загружает файл монографии
        /// </summary>
        /// <param name="monograf">Монография</param>
        /// <param name="uploadedFile">Загружаемый файл</param>
        /// <returns></returns>
        Task<FileModel> UploadMonografAsync(Monograf monograf, IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл документа об образовании
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <param name="typeOfEducationDocument"></param>
        /// <returns></returns>
        Task<FileModel> UploadEducationDocumentAsync(IFormFile uploadedFile, FileDataTypeEnum typeOfEducationDocument);

        /// <summary>
        /// Загружает файл согласия на обработку персональных данных
        /// </summary>
        /// <param name="uploadedFile">Загружаемый файл</param>
        /// <returns></returns>
        Task<FileModel> UploadApplicationForProcessingPersonalDataAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл результатов освоения образовательной программы
        /// </summary>
        /// <param name="student"></param>
        /// <param name="uploadFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadRezultOsvoenObrazovatProgrAsync(Student student, IFormFile uploadedFile);
        
        /// <summary>
        /// Загружает файл паспорта
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadPassportAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает на сервер скан-копию СНИЛС
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadSNILSAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл карточки абитуриента
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadAbiturientCardAsync(IFormFile uploadedFile);

        /// <summary>
        /// Возвращает типы файлов указанной группы
        /// </summary>
        /// <param name="fileDataTypeGroup"></param>
        /// <returns></returns>
        IQueryable<FileDataType> GetFileDataTypes(FileDataTypeGroupEnum fileDataTypeGroup);

        /// <summary>
        /// Загружает на сервер фотографию абитуриента
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadUserPhotoAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загрузка скан-копии заявления о приёме
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadApplicationForAdmissionFileAsync(IFormFile uploadedFile);

        /// <summary>
        /// Файл с решением задания, загруженный пользователем
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadLmsAppUserAnswer(IFormFile uploadedFile);

        /// <summary>
        /// Удаляет документ пользователя
        /// </summary>
        /// <param name="userDocument"></param>
        /// <returns></returns>
        Task RemoveUserDocumentAsync(UserDocument userDocument);

        /// <summary>
        /// Удаляет набор документов пользователя
        /// </summary>
        /// <param name="userDocuments"></param>
        /// <returns></returns>
        Task RemoveUserDocumentsAsync(List<UserDocument> userDocuments);

        /// <summary>
        /// Заменяет файл на диске
        /// </summary>
        /// <param name="fileModel"></param>
        /// <param name="uploadedFile"></param>
        Task ReloadFileAsync(FileModel fileModel, IFormFile uploadedFile);

        /// <summary>
        /// Заменяет файл на диске
        /// </summary>
        /// <param name="fileModelId"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task ReloadFileAsync(int? fileModelId, IFormFile uploadedFile);

        /// <summary>
        /// Возвращает объект файловой модели
        /// </summary>
        /// <param name="fileModelId"></param>
        /// <returns></returns>
        Task<FileModel> GetFileModelAsync(int? fileModelId);
        
        /// <summary>
        /// Загружает на сервер файл указанного типа
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <param name="fileDataType"></param>
        /// <returns></returns>
        Task<FileModel> UploadFileAsync(IFormFile uploadedFile, FileDataTypeEnum fileDataType);

        
    }
}