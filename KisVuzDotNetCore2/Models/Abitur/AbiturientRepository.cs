using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Репозиторий "Абитуриенты"
    /// </summary>
    public class AbiturientRepository : IAbiturientRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IUserDocumentRepository _userDocumentRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserEducationRepository _userEducationRepository;
        private readonly IApplicationForAdmissionRepository _applicationForAdmissionRepository;
        private readonly IAdmissionPrivilegeRepository _admissionPrivilegeRepository;
        private readonly IFileModelRepository _fileModelRepository;
        private readonly IConsentToEnrollmentRepository _consentToEnrollmentRepository;
        private readonly IAbiturientIndividualAchievmentRepository _abiturientIndividualAchievmentRepository;
        private readonly IContractRepository _contractRepository;

        public AbiturientRepository(AppIdentityDBContext context,
            IUserDocumentRepository userDocumentRepository,
            IUserProfileRepository userProfileRepository,
            IUserEducationRepository userEducationRepository,
            IApplicationForAdmissionRepository applicationForAdmissionRepository,
            IAdmissionPrivilegeRepository admissionPrivilegeRepository,
            IFileModelRepository fileModelRepository,
            IConsentToEnrollmentRepository consentToEnrollmentRepository,
            IAbiturientIndividualAchievmentRepository abiturientIndividualAchievmentRepository,
            IContractRepository contractRepository
            )
        {
            _context = context;
            _userDocumentRepository = userDocumentRepository;
            _userProfileRepository = userProfileRepository;
            _userEducationRepository = userEducationRepository;
            _applicationForAdmissionRepository = applicationForAdmissionRepository;
            _admissionPrivilegeRepository = admissionPrivilegeRepository;
            _fileModelRepository = fileModelRepository;
            _consentToEnrollmentRepository = consentToEnrollmentRepository;
            _abiturientIndividualAchievmentRepository = abiturientIndividualAchievmentRepository;
            _contractRepository = contractRepository;
        }

        /// <summary>
        /// Загружает скан-копию файла, подтверждающего индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievmentId"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateAbiturientIndividualAchievmentFileAsync(int abiturientIndividualAchievmentId, IFormFile uploadedFile)
        {
            var abiturientIndividualAchievment = await GetAbiturientIndividualAchievmentAsync(abiturientIndividualAchievmentId);
            var uploadedFileModel = await _fileModelRepository.UploadIndividualAchievmentFile(uploadedFile);

            if (abiturientIndividualAchievment.FileModel != null)
            {
                await _fileModelRepository.RemoveFileModelAsync(abiturientIndividualAchievment.FileModel);                
            }

            abiturientIndividualAchievment.FileModelId = uploadedFileModel.Id;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавляет индивидуальное достижение пользователя
        /// </summary>
        /// <param name="abiturientIndividualAchievment"></param>
        /// <returns></returns>
        public async Task AddAbiturientIndividualAchievment(AbiturientIndividualAchievment abiturientIndividualAchievment)
        {
            if (abiturientIndividualAchievment.RowStatusId == 0)
                abiturientIndividualAchievment.RowStatusId = (int)RowStatusEnum.NotConfirmed;

            _context.AbiturientIndividualAchievments.Add(abiturientIndividualAchievment);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавляет новое заявление о приёме
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <returns></returns>
        public async Task AddApplicationForAdmission(ApplicationForAdmission applicationForAdmission)
        {
            if (applicationForAdmission.RowStatusId == 0)
                applicationForAdmission.RowStatusId = (int)RowStatusEnum.NotConfirmed;

            _context.ApplicationForAdmissions.Add(applicationForAdmission);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавляет паспортные данные
        /// </summary>
        /// <param name="passportData"></param>
        public async Task AddPassportDataAsync(PassportData passportData)
        {
            await _userProfileRepository.AddPassportDataAsync(passportData);
        }

        /// <summary>
        /// Добавляет сведения об образовании абитуриента
        /// </summary>
        /// <param name="userEducation"></param>
        /// <returns></returns>
        public async Task AddUserEducationAsync(UserEducation userEducation)
        {
            await _userEducationRepository.AddUserEducationAsync(userEducation);
        }

        /// <summary>
        /// Возвращает объект "абитуриент"
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Abiturient> GetAbiturientAsync(string userName)
        {
            var abiturient = await GetAbiturients()                
                .FirstOrDefaultAsync(a => a.AppUser.UserName == userName);
            return abiturient;
        }

        /// <summary>
        /// Возвращает индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievmentId"></param>
        /// <returns></returns>
        public async Task<AbiturientIndividualAchievment> GetAbiturientIndividualAchievmentAsync(int abiturientIndividualAchievmentId)
        {
            var achievment = await GetAbiturientIndividualAchievments()
                .FirstOrDefaultAsync(a => a.AbiturientIndividualAchievmentId == abiturientIndividualAchievmentId);
            return achievment;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех индивидуальных достижений абитуриентов
        /// </summary>
        /// <returns></returns>
        public IQueryable<AbiturientIndividualAchievment> GetAbiturientIndividualAchievments()
        {
            var query = _context.AbiturientIndividualAchievments
                .Include(a => a.RowStatus)
                .Include(a => a.AbiturientIndividualAchievmentType)
                .Include(a => a.FileModel.FileToFileTypes)
                    .ThenInclude(ftf => ftf.FileDataType)
                .Include(a => a.Abiturient.AbiturientStatus)
                .Include(a => a.Abiturient.AppUser);
            return query;
        }

        /// <summary>
        /// Возвращает запрос на выборку уже имеющихся квалификаций абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<Qualification> GetAbiturientQualifications(string userName)
        {
            var query = _userProfileRepository.GetQualifications(userName);
            return query;
        }

        /// <summary>
        /// Возвращает запрос на извлечение всех абитуриентов
        /// </summary>
        /// <returns></returns>
        public IQueryable<Abiturient> GetAbiturients()
        {
            var abiturs = _context.Abiturients
                .Include(a => a.AbiturientStatus)
                // Консультанты абитуриента
                .Include(a => a.AppUserAbiturientConsultants)
                    .ThenInclude(aa => aa.AppUser)
                // Индивидуальные достижения
                .Include(a => a.AbiturientIndividualAchievments)
                    .ThenInclude(aia => aia.AbiturientIndividualAchievmentType)
                .Include(a => a.AbiturientIndividualAchievments)
                    .ThenInclude(aia => aia.RowStatus)
                .Include(a => a.AbiturientIndividualAchievments)
                    .ThenInclude(aia => aia.FileModel)
                        .ThenInclude(fm => fm.FileToFileTypes)
                            .ThenInclude(ftf => ftf.FileDataType)
                // Паспортные данные пользователя
                .Include(a => a.AppUser.PassportData.Address)                
                // Личные данные пользователя
                .Include(a => a.AppUser.Gender)
                .Include(a => a.AppUser.MilitaryServiceStatus)
                .Include(a => a.AppUser.FamilyMemberContacts)
                    .ThenInclude(fmc => fmc.FamilyMemberType)
                .Include(a => a.AppUser.AppUserForeignLanguages)
                    .ThenInclude(aufl => aufl.ForeignLanguage)
                // Документы пользователя
                .Include(a => a.AppUser.UserDocuments)
                    .ThenInclude(ud => ud.RowStatus)
                .Include(a => a.AppUser.UserDocuments)
                    .ThenInclude(ud => ud.FileModel)
                .Include(a => a.AppUser.UserDocuments)
                    .ThenInclude(ud => ud.FileDataType)
                // Образование пользователя
                .Include(a => a.AppUser.UserEducations)
                    .ThenInclude(ue => ue.RowStatus)
                .Include(a => a.AppUser.UserEducations)
                    .ThenInclude(ue => ue.Qualification.RowStatus)
                .Include(a => a.AppUser.UserEducations)
                    .ThenInclude(ue => ue.UserDocument.RowStatus)
                .Include(a => a.AppUser.UserEducations)
                    .ThenInclude(ue => ue.UserDocument.FileModel)
                // Заявления о приёме
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(afa => afa.EduForm)
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(afa => afa.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(afa => afa.QuotaType)
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(afa => afa.RowStatus)
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(afa => afa.FileModel)
                        .ThenInclude(fm => fm.FileToFileTypes)
                            .ThenInclude(ftft => ftft.FileDataType)
                // Льготы
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(afa => afa.AdmissionPrivileges)
                        .ThenInclude(ap => ap.RowStatus)
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(afa => afa.AdmissionPrivileges)
                        .ThenInclude(ap => ap.AdmissionPrivilegeType)
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(afa => afa.AdmissionPrivileges)
                        .ThenInclude(ap => ap.FileModel)
                            .ThenInclude(fm => fm.FileToFileTypes)
                                .ThenInclude(ftft => ftft.FileDataType)
                // Заявления о согласии на зачисление
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(afa => afa.ConsentToEnrollments)
                        .ThenInclude(ce => ce.RowStatus)
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(afa => afa.ConsentToEnrollments)
                        .ThenInclude(ce => ce.FileModel)
                            .ThenInclude(fm => fm.FileToFileTypes)
                                .ThenInclude(ftft => ftft.FileDataType)
                // Мероприятия СДО
                .Include(a => a.AppUser)
                    .ThenInclude(au => au.AppUserLmsEvents)
                        .ThenInclude(aue => aue.AppUserLmsEventUserRole)
                .Include(a => a.AppUser)
                    .ThenInclude(au => au.AppUserLmsEvents)
                        .ThenInclude(aue => aue.LmsEvent.LmsEventType.LmsEventTypeGroup)
                // Договоры
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(aa => aa.Contracts)
                        .ThenInclude(c => c.ContractType)
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(aa => aa.Contracts)
                        .ThenInclude(c => c.RowStatus)
                .Include(a => a.ApplicationForAdmissions)
                    .ThenInclude(aa => aa.Contracts)
                        .ThenInclude(c => c.FileModel.FileToFileTypes)
                ;
            return abiturs;
        }

        /// <summary>
        /// Возвращает паспортные данные указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<PassportData> GetPassportDataAsync(string userName)
        {
            var passportData = await _userProfileRepository.GetPassportDataAsync(userName);
            return passportData;
        }

        /// <summary>
        /// Возвращает документ пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userDocumentId"></param>
        /// <returns></returns>
        public async Task<UserDocument> GetUserDocumentAsync(string userName, int userDocumentId)
        {
            var userDocument = await _userDocumentRepository.GetUserDocumentAsync(userDocumentId);
            if (userDocument.AppUser.UserName != userName)
                return null;

            return userDocument;
        }

        /// <summary>
        /// Возвращает запрос на выборку документов об образовании пользователя, не имеющих заполненных сведений
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<UserDocument> GetUserDocumentsWithoutUserEducationDataAsync(string userName)
        {
            var userDocuments = _userDocumentRepository.GetUserEducationDocumentsWithoutUserEducationDataAsync(userName);

            return userDocuments;
        }

        /// <summary>
        /// Определяет, является ли пользователь абитуриентом
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsAbitur(string userName)
        {
            bool isAbitur = _context.Users
                .Include(u => u.Abiturient)
                .Any(u =>
                u.UserName == userName &&
                u.Abiturient != null
            );
            return isAbitur;
        }

        /// <summary>
        /// Проверяем наличие заявлений о приёме
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsApplicationForAdmissionExists(string userName)
        {
            bool isExists = _applicationForAdmissionRepository.IsApplicationForAdmissionExists(userName);
            return isExists;
        }

        /// <summary>
        /// Проверяет наличие заявлений о приёме
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        public bool IsApplicationForAdmissionExists(Abiturient abiturient)
        {
            if (abiturient.ApplicationForAdmissions == null)
                return false;

            if (abiturient.ApplicationForAdmissions.Count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Проверяет наличие у абитуриента загруженного заявления
        /// на обработку персональных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsLoadedFileApplicationForProcessingPersonalData(string userName)
        {
            bool isLoadedFile = _userDocumentRepository.IsLoadedUserDocument(userName, FileDataTypeEnum.SoglasieNaObrabotkuPersonalnihDannih);

            return isLoadedFile;
        }

        /// <summary>
        /// Проверяем наличие у абитуриента загруженного заявления
        /// на обработку персональных данных
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        public bool IsLoadedFileApplicationForProcessingPersonalData(Abiturient abiturient)
        {
            bool isLoadedFile = _userDocumentRepository.IsLoadedUserDocument(abiturient.AppUser, FileDataTypeEnum.SoglasieNaObrabotkuPersonalnihDannih);

            return isLoadedFile;
        }



        /// <summary>
        /// Проверяет наличие у абитуриента загруженных
        /// документов об образовании
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsLoadedFileEducationDocuments(string userName)
        {
            bool isLoadedFile = _userDocumentRepository.IsLoadedUserDocument(userName, FileDataTypeGroupEnum.EducationDocuments);

            return isLoadedFile;
        }

        /// <summary>
        /// Проверяет наличие у абитуриента загруженных
        /// документов об образовании
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        public bool IsLoadedFileEducationDocuments(Abiturient abiturient)
        {
            bool isLoadedFile = _userDocumentRepository.IsLoadedUserDocument(abiturient.AppUser, FileDataTypeGroupEnum.EducationDocuments);

            return isLoadedFile;
        }

        /// <summary>
        /// Проверяет наличие у абитуриента загруженной скан-копии паспорта
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsLoadedFilePassport(string userName)
        {
            bool isLoadedFile = _userDocumentRepository.IsLoadedUserDocument(userName, FileDataTypeEnum.Passport);

            return isLoadedFile;
        }

        /// <summary>
        /// Проверяем наличие у абитуриента загруженной скан-копии паспорта
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        public bool IsLoadedFilePassport(Abiturient abiturient)
        {
            bool isLoadedFile = _userDocumentRepository.IsLoadedUserDocument(abiturient.AppUser, FileDataTypeEnum.Passport);

            return isLoadedFile;
        }

        /// <summary>
        /// Проверяет наличие у абитуриента загруженной карточки абитуриента
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        public bool IsLoadedFileAbiturientCard(Abiturient abiturient)
        {
            bool isLoadedFile = _userDocumentRepository.IsLoadedUserDocument(abiturient.AppUser, FileDataTypeEnum.AbiturientCard);

            return isLoadedFile;
        }

        /// <summary>
        /// Проверяем наличие у абитуриента загруженной фотографии
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        public bool IsLoadedFilePhoto(Abiturient abiturient)
        {
            bool isLoadedFile = _userDocumentRepository.IsLoadedUserDocument(abiturient.AppUser, FileDataTypeEnum.UserDocuments_Photo);

            return isLoadedFile;
        }

        /// <summary>
        /// Проверяет наличие наспортных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsPassportDataExistsAsync(string userName)
        {
            bool isEntered = await _userProfileRepository.IsPassportDataExistsAsync(userName);
            return isEntered;
        }

        /// <summary>
        /// Проверяет наличие паспортных данных
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        public bool IsPassportDataExists(Abiturient abiturient)
        {
            bool isEntered =_userProfileRepository.IsPassportDataExists(abiturient.AppUser);
            return isEntered;
        }

        /// <summary>
        /// Проверяет наличие у абитуриента сведений об образовании
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsUserEducationDataExists(string userName)
        {
            bool isEntered = await _userDocumentRepository.IsUserEducationDataExistsAsync(userName);
            return isEntered;
        }

        /// <summary>
        /// Проверяет наличие у абитуриента сведений об образовании
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        public async Task<bool> IsUserEducationDataExists(Abiturient abiturient)
        {
            bool isEntered = await _userDocumentRepository.IsUserEducationDataExistsAsync(abiturient.AppUser);
            return isEntered;
        }

        /// <summary>
        /// Регистрация существующего пользователя в качестве абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RegisterUserAsAbiturient(string userName)
        {
            var appUser = _userProfileRepository.GetAppUser(userName);

            if(appUser.Abiturient == null)
            {
                appUser.Abiturient = new Abiturient
                {
                    AbiturientStatusId = (int)AbiturientStatusEnum.NewAbiturient,
                    ApplicationForAdmissions = new List<ApplicationForAdmission>(),
                    IsHostelRequired = false
                };
                await _context.SaveChangesAsync();
            }            
        }

        /// <summary>
        /// Заменяет пользовательский документ
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userDocumentId"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task ReloadUserDocumentAsync(string userName, int userDocumentId, IFormFile uploadedFile)
        {
            var userDocument = await GetUserDocumentAsync(userName, userDocumentId);
            if (userDocument == null)
                return;

            await _userDocumentRepository.ReloadUserDocumentAsync(userDocumentId, uploadedFile);                        
        }

        /// <summary>
        /// Удаляет аккаунт абитуриента и все связанные данные
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task RemoveAbiturientAsync(string userName, bool markAppUserAccountToDelete = false)
        {
            var abiturient = await GetAbiturientAsync(userName);
            if(markAppUserAccountToDelete)
            {
                await _userProfileRepository.SetAppUserStatusAsync(abiturient.AppUser, AppUserStatusEnum.ToDelete);
            }
            
            if(abiturient.AbiturientIndividualAchievments != null && abiturient.AbiturientIndividualAchievments.Count() > 0)
            {
                await RemoveAbiturientIndividualAchievmentsAsync(abiturient.AbiturientIndividualAchievments);
            }

            await _applicationForAdmissionRepository.RemoveApplicationForAdmissionsAsync(abiturient.ApplicationForAdmissions);

            _context.Abiturients.Remove(abiturient);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет индивидуальные достижения абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievments"></param>
        /// <returns></returns>
        private async Task RemoveAbiturientIndividualAchievmentsAsync(List<AbiturientIndividualAchievment> abiturientIndividualAchievments)
        {
            var list = new List<int>();
            abiturientIndividualAchievments.ForEach(a => list.Add(a.AbiturientIndividualAchievmentId));

            foreach(var item in list)
            {
                await RemoveAbiturientIndividualAchievmentAsync(item);
            }
        }

        /// <summary>
        /// Удаляет документ пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userDocumentId"></param>
        /// <returns></returns>
        public async Task RemoveUserDocumentAsync(string userName, int userDocumentId)
        {
            var userDocument = await _userDocumentRepository.GetUserDocumentAsync(userDocumentId);
            if (userDocument.AppUser.UserName != userName)
                return;

            await _userDocumentRepository.RemoveUserDocumentAsync(userDocumentId);
        }

        /// <summary>
        /// Устанавливает статус абитуриента
        /// </summary>
        /// <param name="abiturient"></param>
        /// <param name="abiturientStatus"></param>
        /// <returns></returns>
        public async Task SetAbiturientStatusAsync(Abiturient abiturient, AbiturientStatusEnum abiturientStatus)
        {
            abiturient.AbiturientStatusId = (int)abiturientStatus;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Устанавливает абитуриенту номер группы для прохождения вступительных испытаний
        /// </summary>
        /// <param name="abiturient"></param>
        /// <param name="entranceTestGroupId"></param>
        /// <returns></returns>
        public async Task SetAbiturientEntranceTestGroupIdAsync(Abiturient abiturient, int? entranceTestGroupId)
        {
            abiturient.EntranceTestGroupId = entranceTestGroupId;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Устанавливает замечание к документу
        /// </summary>
        /// <param name="userDocument"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task SetUserDocumentRemarkAsync(UserDocument userDocument, string remark)
        {
            await _userDocumentRepository.SetUserDocumentRemarkAsync(userDocument, remark);
        }

        /// <summary>
        /// Устанавливает статус документа
        /// </summary>
        /// <param name="userDocument"></param>
        /// <param name="rowStatusId"></param>
        /// <returns></returns>
        public async Task SetUserDocumentStatusAsync(UserDocument userDocument, RowStatusEnum rowStatusId)
        {
            await _userProfileRepository.SetUserDocumentStatusAsync(userDocument, rowStatusId);
        }

        /// <summary>
        /// Обновляет запись об индивидуальном достижении абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievment"></param>
        /// <returns></returns>
        public async Task UpdateAbiturientIndividualAchievment(AbiturientIndividualAchievment abiturientIndividualAchievment, IFormFile uploadedFile)
        {
            await _abiturientIndividualAchievmentRepository.Update(abiturientIndividualAchievment, uploadedFile);            
        }

        /// <summary>
        /// Удаляет индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievmentId"></param>
        /// <returns></returns>
        public async Task RemoveAbiturientIndividualAchievmentAsync(int abiturientIndividualAchievmentId)
        {
            await _abiturientIndividualAchievmentRepository.Remove(abiturientIndividualAchievmentId);
            //var entry = await GetAbiturientIndividualAchievmentAsync(abiturientIndividualAchievmentId);

            //if(entry.FileModel != null)
            //    await _fileModelRepository.RemoveFileModelAsync(entry.FileModel);

            //_context.AbiturientIndividualAchievments.Remove(entry);
            //await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает количество заявлений о приёме, созданных указанным пользователем
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetNumberOfApplicationForAdmissions(string userName)
        {
            int result = _applicationForAdmissionRepository.GetNumberOfApplicationForAdmissions(userName);
            return result;
        }

        /// <summary>
        /// Возвращает заявление о зачислении абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="applicationForAdmissionId"></param>
        /// <returns></returns>
        public async Task<ApplicationForAdmission> GetApplicationForAdmissionAsync(string userName,
            int applicationForAdmissionId)
        {
            ApplicationForAdmission applicationForAdmission = await _applicationForAdmissionRepository
                .GetApplicationForAdmissionAsync(applicationForAdmissionId);
            if (applicationForAdmission == null ||
                applicationForAdmission.Abiturient == null ||
                applicationForAdmission.Abiturient.AppUser == null ||
                applicationForAdmission.Abiturient.AppUser.UserName != userName)
                return null;

            return applicationForAdmission;
        }

        /// <summary>
        /// Обновляет заявление о зачислении
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <returns></returns>
        public async Task UpdateApplicationForAdmissionAsync(ApplicationForAdmission applicationForAdmission)
        {
            await _applicationForAdmissionRepository.UpdateApplicationForAdmissionAsync(applicationForAdmission);
        }

        /// <summary>
        /// Обновляет заявление о приёме
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateApplicationForAdmissionAsync(ApplicationForAdmission applicationForAdmission, IFormFile uploadedFile)
        {
            await _applicationForAdmissionRepository.UpdateApplicationForAdmissionAsync(applicationForAdmission, uploadedFile);
        }

        /// <summary>
        /// Удаляет заявление о приёме
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <returns></returns>
        public async Task RemoveApplicationForAdmissionAsync(string userName, int applicationForAdmissionId)
        {
            var entry = await GetApplicationForAdmissionAsync(userName, applicationForAdmissionId);

            if (entry == null) return;

            await _applicationForAdmissionRepository.RemoveApplicationForAdmissionAsync(entry);
        }


        /// <summary>
        /// Загрузка скан-копии заявления о приёме
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="applicationForAdmissionId"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task ApplicationForAdmissionFileLoadAsync(string userName, int applicationForAdmissionId, IFormFile uploadedFile)
        {
            await _applicationForAdmissionRepository.ApplicationForAdmissionFileLoadAsync(userName, applicationForAdmissionId, uploadedFile);
        }

        /// <summary>
        /// Обновляет данные абитуриента
        /// </summary>
        /// <param name="abiturient"></param>
        /// <returns></returns>
        public async Task UpdateAbiturientAsync(Abiturient abiturient)
        {
            _context.Abiturients.Update(abiturient);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Добавляет иностранный язык
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="foreignLanguageId"></param>
        /// <returns></returns>
        public async Task AddAbiturientForeignLanguage(string userName, int foreignLanguageId)
        {
            var abiturient = await GetAbiturientAsync(userName);
            if (abiturient == null) return;
            if (abiturient.AppUser.UserName != userName) return;

            await _userProfileRepository.AddAppUserForeignLanguage(abiturient.AppUser, foreignLanguageId);
        }

        /// <summary>
        /// Добавляет контакт ближайшего родственника
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="familyMemberContact"></param>
        /// <returns></returns>
        public async Task AddFamilyMemberContactAsync(string userName, FamilyMemberContact familyMemberContact)
        {
            var abiturient = await GetAbiturientAsync(userName);
            familyMemberContact.AppUserId = abiturient.AppUserId;
            await _userProfileRepository.AddFamilyMemberContact(familyMemberContact);
        }

        /// <summary>
        /// Создание льготы абитуриента при приёме
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="admissionPrivilege"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task CreateAdmissionPrivilegeAsync(string userName, AdmissionPrivilege admissionPrivilege, IFormFile uploadedFile)
        {
            var applicationForAdmission = await GetApplicationForAdmissionAsync(userName, admissionPrivilege.ApplicationForAdmissionId);
            if (applicationForAdmission == null) return;

            admissionPrivilege.RowStatusId = (int)RowStatusEnum.NotConfirmed;

            await _admissionPrivilegeRepository.CreateAdmissionPrivilegeAsync(admissionPrivilege, uploadedFile);
        }

        /// <summary>
        /// Создаёт договор абитуриента с образовательной организацией
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="contract"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task CreateContractAsync(string userName, Contract contract, IFormFile uploadedFile)
        {
            var applicationForAdmission = await GetApplicationForAdmissionAsync(userName, contract.ApplicationForAdmissionId ?? 0);
            if (applicationForAdmission == null) return;

            contract.RowStatusId = (int)RowStatusEnum.NotConfirmed;

            await _contractRepository.CreateContractAsync(contract, uploadedFile);
        }

        /// <summary>
        /// Возвращает объект льготы абитуриента при поступлении
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="abiturientAdmissionPrivilegeId"></param>
        /// <returns></returns>
        public async Task<AdmissionPrivilege> GetAdmissionPrivilegeAsync(string userName, int abiturientAdmissionPrivilegeId)
        {
            var admissionPrivilege = await _admissionPrivilegeRepository.GetAdmissionPrivilegeAsync(abiturientAdmissionPrivilegeId);
            if (admissionPrivilege == null)
                return null;

            if (admissionPrivilege.ApplicationForAdmission.Abiturient.AppUser.UserName != userName)
                return null;

            return admissionPrivilege;
        }

        /// <summary>
        /// Удаляет льготу абитуриента при поступлении
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="admissionPrivilegeId"></param>
        /// <returns></returns>
        public async Task RemoveAdmissionPrivilegeAsync(string userName, int admissionPrivilegeId)
        {
            var admissionPrivilege = await GetAdmissionPrivilegeAsync(userName, admissionPrivilegeId);
            if (admissionPrivilege == null) return;

            await _admissionPrivilegeRepository.RemoveAdmissionPrivilegeAsync(admissionPrivilege);
        }

        /// <summary>
        /// Обновляет льготу абитуриента при поступлении
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="admissionPrivilege"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateAdmissionPrivilegeAsync(string userName, AdmissionPrivilege admissionPrivilege, IFormFile uploadedFile)
        {
            if (admissionPrivilege == null) return;
            var entry = await GetAdmissionPrivilegeAsync(userName, admissionPrivilege.AdmissionPrivilegeId);
            if (entry == null) return;
                        
            entry.ApplicationForAdmissionId = admissionPrivilege.ApplicationForAdmissionId;
            entry.AdmissionPrivilegeTypeId = admissionPrivilege.AdmissionPrivilegeTypeId;
            entry.RowStatusId = (int)RowStatusEnum.ChangedByUser;
            
            await _admissionPrivilegeRepository.UpdateAdmissionPrivilegeAsync(entry, uploadedFile);
        }

        /// <summary>
        /// Изменение паспортных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passportData"></param>
        /// <returns></returns>
        public async Task ChangePassportData(string userName, PassportData passportData)
        {
            var entryPassportData = await GetPassportDataAsync(userName);
            if (entryPassportData == null) return;

            entryPassportData.PassportSeriya = passportData.PassportSeriya;
            entryPassportData.PassportNumber = passportData.PassportNumber;
            entryPassportData.MestoRojdeniya = passportData.MestoRojdeniya;
            entryPassportData.KemVidan = passportData.KemVidan;
            entryPassportData.DataVidachi = passportData.DataVidachi;
            entryPassportData.KodPodrazdeleniya = passportData.KodPodrazdeleniya;
            entryPassportData.Address.PostCode = passportData.Address.PostCode;
            entryPassportData.Address.Country = passportData.Address.Country;
            entryPassportData.Address.Region = passportData.Address.Region;
            entryPassportData.Address.Settlement = passportData.Address.Settlement;
            entryPassportData.Address.Street = passportData.Address.Street;
            entryPassportData.Address.HouseNumber = passportData.Address.HouseNumber;
            entryPassportData.Address.PopulatedLocalityId = passportData.Address.PopulatedLocalityId;

            await _userProfileRepository.UpdatePassportDataAsync(entryPassportData);
        }

        /// <summary>
        /// Создание нового заявления о согласии на зачисление
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="consentToEnrollment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task CreateConsentToEnrollmentAsync(string userName, ConsentToEnrollment consentToEnrollment, IFormFile uploadedFile)
        {
            var applicationForAdmission = await _applicationForAdmissionRepository.GetApplicationForAdmissionAsync(consentToEnrollment.ApplicationForAdmissionId);
            if (applicationForAdmission.Abiturient.AppUser.UserName != userName)
                return;

            await _consentToEnrollmentRepository.CreateConsentToEnrollmentAsync(consentToEnrollment, uploadedFile);
        }

        /// <summary>
        /// Возвращает заявление о согласии на зачисление абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="abiturientConsentToEnrollmentId"></param>
        /// <returns></returns>
        public async Task<ConsentToEnrollment> GetConsentToEnrollment(string userName, int abiturientConsentToEnrollmentId)
        {
            var consentToEnrollment = await _consentToEnrollmentRepository.GetConsentToEnrollmentAsync(abiturientConsentToEnrollmentId);
            if (consentToEnrollment == null) return null;
            if (consentToEnrollment.ApplicationForAdmission.Abiturient.AppUser.UserName != userName) return null;

            return consentToEnrollment;
        }

        /// <summary>
        /// Обновляет заявление о согласии на зачисление абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="consentToEnrollment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateConsentToEnrollment(string userName, ConsentToEnrollment consentToEnrollment, IFormFile uploadedFile)
        {
            var entry = await GetConsentToEnrollment(userName, consentToEnrollment.ConsentToEnrollmentId);
            if (entry == null) return;

            entry.ApplicationForAdmissionId = consentToEnrollment.ApplicationForAdmissionId;
            entry.Remark = consentToEnrollment.Remark;
            entry.RowStatusId = (int)RowStatusEnum.ChangedByUser;

            await _consentToEnrollmentRepository.UpdateConsentToEnrollmentAsync(entry, uploadedFile);
        }

        /// <summary>
        /// Удаляет заявление о согласии на зачисление абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="consentToEnrollmentId"></param>
        /// <returns></returns>
        public async Task RemoveConsentToEnrollment(string userName, int consentToEnrollmentId)
        {
            var entry = await GetConsentToEnrollment(userName, consentToEnrollmentId);

            if (entry == null) return;

            await _consentToEnrollmentRepository.RemoveConsentToEnrollmentAsync(entry);
        }

        #region Договоры абитуриентов
        /// <summary>
        /// Возвращает договор абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="abiturientContractId"></param>
        /// <returns></returns>
        public async Task<Contract> GetContractAsync(string userName, int abiturientContractId)
        {
            var entry = await _contractRepository.GetContractAsync(abiturientContractId);
            if (entry == null) return null;

            if (entry.AppUser.UserName != userName) return null;

            return entry;
        }

        /// <summary>
        /// Обновляет договор абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="contract"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateContractAsync(string userName, Contract contract, IFormFile uploadedFile)
        {
            if (contract == null) return;
            if (uploadedFile == null) return;

            var entry = await GetContractAsync(userName, contract.ContractId);
            if (entry == null) return;

            if (entry.AppUser.UserName != userName) return;

            entry.RowStatusId = (int)RowStatusEnum.ChangedByUser;
            await _contractRepository.UpdateContractAsync(entry, uploadedFile);
        }

        /// <summary>
        /// Удаляет договор абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task RemoveContractAsync(string userName, Contract contract)
        {
            if (contract == null) return;            

            var entry = await GetContractAsync(userName, contract.ContractId);
            if (entry == null) return;

            if (entry.AppUser.UserName != userName) return;
                        
            await _contractRepository.RemoveContractAsync(entry);
        }

        /// <summary>
        /// Назначает абитуриенту консультанта
        /// </summary>
        /// <param name="abiturient"></param>
        /// <param name="appUserIdAbiturientConsultant"></param>
        /// <returns></returns>
        public async Task SetAppUserAbiturientConsultantAsync(Abiturient abiturient, string appUserIdAbiturientConsultant)
        {
            var entry = abiturient.AppUserAbiturientConsultants.FirstOrDefault();
            if(entry == null)
            {
                abiturient.AppUserAbiturientConsultants
                    .Add(new AppUserAbiturientConsultant { AppUserId = appUserIdAbiturientConsultant });
            }
            else
            {
                entry.AppUserId = appUserIdAbiturientConsultant;
            }

            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
