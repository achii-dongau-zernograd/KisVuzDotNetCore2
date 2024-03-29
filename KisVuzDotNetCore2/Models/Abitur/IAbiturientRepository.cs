﻿using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Http;
using System;
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
        /// Проверяем наличие у абитуриента загруженного заявления
        /// на обработку персональных данных
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        bool IsLoadedFileApplicationForProcessingPersonalData(Abiturient abiturient);

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
        /// Возвращает объект "абитуриент"
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Abiturient> GetAbiturientAsync(int abiturientId);

        /// <summary>
        /// Проверка наличия бланка регистрации на мероприятие
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="lmsEventId"></param>
        /// <returns></returns>
        Task<bool> IsEntranceTestRegistrationFormExistsAsync(string userName, int lmsEventId);


        /// <summary>
        /// Проверяет наличие у абитуриента загруженных
        /// документов об образовании
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsLoadedFileEducationDocuments(string userName);

        /// <summary>
        /// Проверяет наличие у абитуриента загруженных
        /// документов об образовании
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        bool IsLoadedFileEducationDocuments(Abiturient abiturient);

        /// <summary>
        /// Проверяет наличие у абитуриента загруженной карточки абитуриента
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        bool IsLoadedFileAbiturientCard(Abiturient abiturient);

        /// <summary>
        /// Удаляет личный кабинет абитуриента и все связанные данные
        /// с возможностью пометки аккаунта к удалению
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task RemoveAbiturientAsync(string userName, bool MarkAppUserAccountToDelete = false);

        /// <summary>
        /// Удаляет личный кабинет абитуриента и все связанные данные
        /// с возможностью пометки аккаунта к удалению
        /// </summary>
        Task RemoveAbiturientAsync(int abiturientId, bool MarkAppUserAccountToDelete = false);

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
        /// Добавляет бланк регистрации абитуриента на вступительное испытание
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="entranceTestRegistrationForm"></param>
        /// <returns></returns>
        Task CreateEntranceTestRegistrationFormAsync(string userName, EntranceTestRegistrationForm entranceTestRegistrationForm);

        /// <summary>
        /// Устанавливает статус абитуриента
        /// </summary>
        /// <param name="abiturient"></param>
        /// <param name="abiturientStatusId"></param>
        /// <returns></returns>
        Task SetAbiturientStatusAsync(Abiturient abiturient, AbiturientStatusEnum abiturientStatusId);
                
        /// <summary>
        /// Устанавливает способ подачи документов
        /// </summary>
        /// <param name="abiturient"></param>
        /// <param name="submittingDocumentsTypeId"></param>
        /// <returns></returns>
        Task SetAbiturientSubmittingDocumentsTypeAsync(Abiturient abiturient, int submittingDocumentsTypeId);

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
        /// Проверяет наличие сведений об образовании
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        Task<bool> IsUserEducationDataExists(Abiturient abiturient);
                
        /// <summary>
        /// Возвращает запрос на выборку документов об образовании пользователя, не имеющих сведений 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<UserDocument> GetUserDocumentsWithoutUserEducationDataAsync(string userName);

        /// <summary>
        /// Устанавливает абитуриенту номер группы для прохождения вступительных испытаний
        /// </summary>
        /// <param name="abiturient"></param>
        /// <param name="entranceTestGroupId"></param>
        /// <returns></returns>
        Task SetAbiturientEntranceTestGroupIdAsync(Abiturient abiturient, int? entranceTestGroupId);

        /// <summary>
        /// Возвращает запрос на выборку уже имеющихся квалификаций абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<Qualification> GetAbiturientQualifications(string userName);

        /// <summary>
        /// Устанавливает дату и время регистрации абитуриента
        /// </summary>
        /// <param name="abiturient"></param>
        /// <param name="abiturRegisterDateTime"></param>
        /// <returns></returns>
        Task SetAbiturientRegisterDateTimeAsync(Abiturient abiturient, DateTime abiturRegisterDateTime);

        /// <summary>
        /// Проверяет наличие наспортных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsPassportDataExistsAsync(string userName);

        /// <summary>
        /// Проверяет наличие паспортных данных
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        bool IsPassportDataExists(Abiturient abiturient);

        /// <summary>
        /// Проверяет наличие у абитуриента загруженной скан-копии паспорта
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsLoadedFilePassport(string userName);
                
        /// <summary>
        /// Назначает абитуриенту консультанта
        /// </summary>
        /// <param name="abiturient"></param>
        /// <param name="appUserAbiturientConsultantId"></param>
        /// <returns></returns>
        Task SetAppUserAbiturientConsultantAsync(Abiturient abiturient, string appUserAbiturientConsultantId);

        /// <summary>
        /// Проверяем наличие у абитуриента загруженной скан-копии паспорта
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        bool IsLoadedFilePassport(Abiturient abiturient);

        /// <summary>
        /// Проверяем наличие у абитуриента загруженной фотографии
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        bool IsLoadedFilePhoto(Abiturient abiturient);

        /// <summary>
        /// Проверяем наличие у абитуриента загруженной скан-копии СНИЛС
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        bool IsLoadedFileSNILS(Abiturient abiturient);

        /// <summary>
        /// Обновляет данные абитуриента
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        Task UpdateAbiturientAsync(Abiturient abiturient);

        /// <summary>
        /// Проверяет наличие заявлений о приёме
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsApplicationForAdmissionExists(string userName);

        /// <summary>
        /// Проверяет наличие заявлений о приёме
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        bool IsApplicationForAdmissionExists(Abiturient abiturient);

        /// <summary>
        /// Возвращает паспортные данные указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<PassportData> GetPassportDataAsync(string userName);

        /// <summary>
        /// Добавляет иностранный язык
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="foreignLanguageId"></param>
        /// <returns></returns>
        Task AddAbiturientForeignLanguage(string userName, int foreignLanguageId);

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
        /// Добавляет контакт ближайшего родственника
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="familyMemberContact"></param>
        /// <returns></returns>
        Task AddFamilyMemberContactAsync(string userName, FamilyMemberContact familyMemberContact);

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
        /// Изменяет флаг наличия оригинала документа об образовании
        /// </summary>
        /// <param name="abiturient"></param>
        /// <param name="IsEduDocumentOriginal"></param>
        /// <returns></returns>
        Task SetIsEduDocumentOriginalAsync(Abiturient abiturient, bool IsEduDocumentOriginal);

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

        /// <summary>
        /// Возвращает заявление о приёме пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="applicationForAdmissionId"></param>
        /// <returns></returns>
        Task<ApplicationForAdmission> GetApplicationForAdmissionAsync(string userName, int applicationForAdmissionId);

        /// <summary>
        /// Обновляет заявление о приёме
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <returns></returns>
        Task UpdateApplicationForAdmissionAsync(ApplicationForAdmission applicationForAdmission);

        /// <summary>
        /// Обновляет заявление о приёме
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateApplicationForAdmissionAsync(ApplicationForAdmission applicationForAdmission, IFormFile uploadedFile);

        /// <summary>
        /// Удаляет заявление о приёме
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <returns></returns>
        Task RemoveApplicationForAdmissionAsync(string userName, int applicationForAdmissionId);

        /// <summary>
        /// Загрузка скан-копии заявления о приёме
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="applicationForAdmissionId"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task ApplicationForAdmissionFileLoadAsync(string userName, int applicationForAdmissionId, IFormFile uploadedFile);
        
        /// <summary>
        /// Создание льготы абитуриента при приёме
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="admissionPrivilege"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task CreateAdmissionPrivilegeAsync(string userName, AdmissionPrivilege admissionPrivilege, IFormFile uploadedFile);

        /// <summary>
        /// Создаёт договор абитуриента с образовательной организацией
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="contract"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task CreateContractAsync(string userName, Contract contract, IFormFile uploadedFile);

        /// <summary>
        /// Возвращает объект льготы абитуриента при поступлении
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="abiturientAdmissionPrivilegeId"></param>
        /// <returns></returns>
        Task<AdmissionPrivilege> GetAdmissionPrivilegeAsync(string userName, int abiturientAdmissionPrivilegeId);

        /// <summary>
        /// Удаляет льготу абитуриента при поступлении
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="admissionPrivilegeId"></param>
        /// <returns></returns>
        Task RemoveAdmissionPrivilegeAsync(string userName, int admissionPrivilegeId);

        /// <summary>
        /// Обновляет льготу абитуриента при поступлении
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="admissionPrivilege"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateAdmissionPrivilegeAsync(string userName, AdmissionPrivilege admissionPrivilege, IFormFile uploadedFile);

        /// <summary>
        /// Изменение паспортных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passportData"></param>
        /// <returns></returns>
        Task ChangePassportData(string userName, PassportData passportData);

        /// <summary>
        /// Создание нового заявления о согласии на зачисление
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="consentToEnrollment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task CreateConsentToEnrollmentAsync(string userName, ConsentToEnrollment consentToEnrollment, IFormFile uploadedFile);

        /// <summary>
        /// Возвращает заявление о согласии на зачисление абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="abiturientConsentToEnrollmentId"></param>
        /// <returns></returns>
        Task<ConsentToEnrollment> GetConsentToEnrollment(string userName, int abiturientConsentToEnrollmentId);

        /// <summary>
        /// Обновляет заявление о согласии на зачисление абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="consentToEnrollment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateConsentToEnrollment(string userName, ConsentToEnrollment consentToEnrollment, IFormFile uploadedFile);

        /// <summary>
        /// Удаляет заявление о согласии на зачисление абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="consentToEnrollmentId"></param>
        /// <returns></returns>
        Task RemoveConsentToEnrollment(string userName, int consentToEnrollmentId);
        
        /// <summary>
        /// Возвращает договор абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="abiturientContractId"></param>
        /// <returns></returns>
        Task<Contract> GetContractAsync(string userName, int abiturientContractId);
        
        /// <summary>
        /// Обновляет договор абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="contract"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task UpdateContractAsync(string userName, Contract contract, IFormFile uploadedFile);
        
        /// <summary>
        /// Удаляет договор абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task RemoveContractAsync(string userName, Contract contract);

        /// <summary>
        /// Добавляет сведения об оплате по договору
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="payment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task CreateContractPaymentAsync(string userName, Payment payment, IFormFile uploadedFile);
        
        /// <summary>
        /// Добавляет заявление об отзыве документов
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="revocationStatement"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task CreateRevocationStatement(string userName, RevocationStatement revocationStatement, IFormFile uploadedFile);


        /// <summary>
        /// Возвращает заявление об отзыве документов абитуриента по переданному УИД заявления
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="revocationStatementId"></param>
        /// <returns></returns>
        Task<RevocationStatement> GetRevocationStatementAsync(string userName, int revocationStatementId);

        /// <summary>
        /// Удаляет заявление об отзыве документов абитуриента по переданному УИД заявления
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
