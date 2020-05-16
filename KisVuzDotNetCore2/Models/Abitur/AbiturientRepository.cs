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
        private readonly IFileModelRepository _fileModelRepository;

        public AbiturientRepository(AppIdentityDBContext context,
            IUserDocumentRepository userDocumentRepository,
            IUserProfileRepository userProfileRepository,
            IUserEducationRepository userEducationRepository,
            IApplicationForAdmissionRepository applicationForAdmissionRepository,
            IFileModelRepository fileModelRepository
            )
        {
            _context = context;
            _userDocumentRepository = userDocumentRepository;
            _userProfileRepository = userProfileRepository;
            _userEducationRepository = userEducationRepository;
            _applicationForAdmissionRepository = applicationForAdmissionRepository;
            _fileModelRepository = fileModelRepository;
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
                // Индивидуальные достижения
                .Include(a => a.AbiturientIndividualAchievments)
                    .ThenInclude(aia => aia.AbiturientIndividualAchievmentType)
                .Include(a => a.AbiturientIndividualAchievments)
                    .ThenInclude(aia => aia.RowStatus)
                .Include(a => a.AbiturientIndividualAchievments)
                    .ThenInclude(aia => aia.FileModel)
                        .ThenInclude(fm=>fm.FileToFileTypes)
                            .ThenInclude(ftf => ftf.FileDataType)
                // Паспортные данные пользователя
                .Include(a => a.AppUser.PassportData)
                // Документы пользователя
                .Include(a => a.AppUser.UserDocuments)
                    .ThenInclude(ud => ud.RowStatus)
                .Include(a => a.AppUser.UserDocuments)
                    .ThenInclude(ud => ud.FileModel)
                .Include(a => a.AppUser.UserDocuments)
                    .ThenInclude(ud => ud.FileDataType)
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
                            .ThenInclude(ftft => ftft.FileDataType);
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
        public async Task RemoveAbiturientAsync(string userName)
        {
            var abiturient = await GetAbiturientAsync(userName);
            await _userProfileRepository.SetAppUserStatusAsync(abiturient.AppUser, AppUserStatusEnum.ToDelete);
            _context.Abiturients.Remove(abiturient);

            await _context.SaveChangesAsync();
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
            if(uploadedFile != null)
            {
                if(abiturientIndividualAchievment.FileModelId != null)
                {
                    if(abiturientIndividualAchievment.FileModel == null)
                    {
                        var entryFileModel = await _fileModelRepository.GetFileModelAsync(abiturientIndividualAchievment.FileModelId);
                        abiturientIndividualAchievment.FileModel = entryFileModel;
                    }

                    await _fileModelRepository.ReloadFileAsync(abiturientIndividualAchievment.FileModel, uploadedFile);
                }
                else
                {
                    var loadedFileModel = await _fileModelRepository.UploadIndividualAchievmentFile(uploadedFile);
                    abiturientIndividualAchievment.FileModel = loadedFileModel;
                    abiturientIndividualAchievment.FileModelId = loadedFileModel.Id;
                }
            }
            
            _context.AbiturientIndividualAchievments.Update(abiturientIndividualAchievment);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievmentId"></param>
        /// <returns></returns>
        public async Task RemoveAbiturientIndividualAchievmentAsync(int abiturientIndividualAchievmentId)
        {
            var entry = await GetAbiturientIndividualAchievmentAsync(abiturientIndividualAchievmentId);

            if(entry.FileModel != null)
                await _fileModelRepository.RemoveFileModelAsync(entry.FileModel);

            _context.AbiturientIndividualAchievments.Remove(entry);
            await _context.SaveChangesAsync();
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
    }
}
