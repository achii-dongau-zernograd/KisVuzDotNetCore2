using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Интерфейс "Абитуриент"
    /// </summary>
    public interface IAbiturientRepository
    {
        /// <summary>
        /// Определяет, является ли пользователь абитуриентом
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsAbitur(string userName);

        /// <summary>
        /// Проверяет наличие у абитуриента загруженного заявления
        /// на обработку персональных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsLoadedFileApplicationForProcessingPersonalData(string userName);
        
        /// <summary>
        /// Возвращает запрос на извлечение абитуриентов
        /// </summary>
        /// <returns></returns>
        IQueryable<Abiturient> GetAbiturients();

        /// <summary>
        /// Возвращает объект "абитуриент"
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Abiturient> GetAbiturientAsync(string userName);


        /// <summary>
        /// Проверяет наличие у абитуриента загруженных
        /// документов об образовании
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsLoadedFileEducationDocuments(string userName);
                
        
        /// <summary>
        /// Удаляет аккаунт абитуриента и все связанные данные
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RemoveAbiturientAsync(string userName);

        /// <summary>
        /// Устанавливает статус документа
        /// </summary>
        /// <param name="userDocument"></param>
        /// <param name="rowStatusId"></param>
        /// <returns></returns>
        Task SetUserDocumentStatusAsync(UserDocument userDocument, RowStatusEnum rowStatusId);
        
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
        /// <param name="userName"></param>
        /// <param name="userDocumentId"></param>
        /// <returns></returns>
        Task<UserDocument> GetUserDocumentAsync(string userName, int userDocumentId);
        
        /// <summary>
        /// Устанавливает статус абитуриента
        /// </summary>
        /// <param name="abiturient"></param>
        /// <param name="abiturientStatusId"></param>
        /// <returns></returns>
        Task SetAbiturientStatusAsync(Abiturient abiturient, AbiturientStatusEnum abiturientStatusId);

        /// <summary>
        /// Удаляет документ пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userDocumentId"></param>
        /// <returns></returns>
        Task RemoveUserDocumentAsync(string userName, int userDocumentId);

        /// <summary>
        /// Проверяет наличие сведений об образовании
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsUserEducationDataExists(string userName);

        /// <summary>
        /// Возвращает запрос на выборку документов об образовании пользователя, не имеющих сведений 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<UserDocument> GetUserDocumentsWithoutUserEducationDataAsync(string userName);

        /// <summary>
        /// Возвращает запрос на выборку уже имеющихся квалификаций абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<Qualification> GetAbiturientQualifications(string userName);

        /// <summary>
        /// Проверяет наличие наспортных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsPassportDataExistsAsync(string userName);

        /// <summary>
        /// Проверяет наличие у абитуриента загруженной скан-копии паспорта
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsLoadedFilePassport(string userName);

        /// <summary>
        /// Проверяем наличие заявлений о приёме
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsApplicationForAdmissionExists(string userName);

        /// <summary>
        /// Возвращает паспортные данные указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<PassportData> GetPassportDataAsync(string userName);

        /// <summary>
        /// Добавляет сведения об образовании абитуриента
        /// </summary>
        /// <param name="userEducation"></param>
        /// <returns></returns>
        Task AddUserEducationAsync(UserEducation userEducation);

        /// <summary>
        /// Добавляет паспортные данные
        /// </summary>
        /// <param name="passportData"></param>
        Task AddPassportDataAsync(PassportData passportData);

        /// <summary>
        /// Добавляет индивидуальное достижение пользователя
        /// </summary>
        /// <param name="abiturientIndividualAchievment"></param>
        /// <returns></returns>
        Task AddAbiturientIndividualAchievment(AbiturientIndividualAchievment abiturientIndividualAchievment);

        /// <summary>
        /// Заменяет пользовательский документ
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userDocumentId"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task ReloadUserDocumentAsync(string userName, int userDocumentId, IFormFile uploadedFile);

        /// <summary>
        /// Возвращает индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievmentId"></param>
        /// <returns></returns>
        Task<AbiturientIndividualAchievment> GetAbiturientIndividualAchievmentAsync(int abiturientIndividualAchievmentId);

        /// <summary>
        /// Загружает скан-копию файла, подтверждающего индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievmentId"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateAbiturientIndividualAchievmentFileAsync(int abiturientIndividualAchievmentId, IFormFile uploadedFile);

        /// <summary>
        /// Возвращает запрос на выборку всех индивидуальных достижений абитуриентов
        /// </summary>
        /// <returns></returns>
        IQueryable<AbiturientIndividualAchievment> GetAbiturientIndividualAchievments();

        /// <summary>
        /// Регистрация существующего пользователя в качестве абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RegisterUserAsAbiturient(string userName);

        /// <summary>
        /// Добавляет новое заявление о приёме
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <returns></returns>
        Task AddApplicationForAdmission(ApplicationForAdmission applicationForAdmission);

        /// <summary>
        /// Обновляет запись об индивидуальном достижении абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievment"></param>
        /// <returns></returns>
        Task UpdateAbiturientIndividualAchievment(AbiturientIndividualAchievment abiturientIndividualAchievment, IFormFile uploadedFile);

        /// <summary>
        /// Удаляет индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievmentId"></param>
        /// <returns></returns>
        Task RemoveAbiturientIndividualAchievmentAsync(int abiturientIndividualAchievmentId);
        
        /// <summary>
        /// Возвращает количество заявлений о приёме, созданных указанным пользователем
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        int GetNumberOfApplicationForAdmissions(string userName);
    }
}
