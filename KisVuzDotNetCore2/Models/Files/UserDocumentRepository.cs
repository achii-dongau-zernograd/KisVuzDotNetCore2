using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KisVuzDotNetCore2.Models.Files
{
    /// <summary>
    /// Репозиторий документов пользователей
    /// </summary>    
    public class UserDocumentRepository : IUserDocumentRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserDocumentRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository,
            IUserProfileRepository userProfileRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
            _userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех пользователей
        /// с подгруженной связью с таблицей документов пользователей
        /// (UserDocuments)
        /// </summary>
        IQueryable<AppUser> AppUsers => _context.Users
                .Include(u => u.UserDocuments);

        AppUser GetAppUser(string userName) => AppUsers
                .FirstOrDefault(u => u.UserName == userName);

        /// <summary>
        /// Загружает на сервер согласие на обработку персональных данных и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<UserDocument> CreateApplicationForProcessingPersonalDataAsync(string userName, IFormFile uploadedFile)
        {
            var userId = _userProfileRepository.GetAppUserId(userName);
            var fileModel = await _fileModelRepository.UploadApplicationForProcessingPersonalDataAsync(uploadedFile);
            if (fileModel == null)
                return null;

            var userDocument = new UserDocument
            {
                AppUserId = userId,
                FileModelId = fileModel.Id,
                FileDataTypeId = (int)FileDataTypeEnum.SoglasieNaObrabotkuPersonalnihDannih
            };

            await _context.AddAsync(userDocument);
            await _context.SaveChangesAsync();

            return userDocument;
        }

        /// <summary>
        /// Загружает на сервер документ об образовании и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <param name="typeOfEducationDocument">Тип документа об образовании</param>
        /// <returns></returns>
        public async Task<UserDocument> CreateEducationDocumentAsync(string userName,
            IFormFile uploadedFile,
            FileDataTypeEnum typeOfEducationDocument)
        {
            var userId = _userProfileRepository.GetAppUserId(userName);
            var fileModel = await _fileModelRepository.UploadEducationDocumentAsync(uploadedFile, typeOfEducationDocument);
            if (fileModel == null)
                return null;

            var userDocument = new UserDocument
            {
                AppUserId = userId,
                FileModelId = fileModel.Id,
                FileDataTypeId = (int)typeOfEducationDocument
            };

            await _context.AddAsync(userDocument);
            await _context.SaveChangesAsync();

            return userDocument;
        }

        /// <summary>
        /// Загружает на сервер паспорт и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<UserDocument> CreatePassport(string userName, IFormFile uploadedFile)
        {
            var userId = _userProfileRepository.GetAppUserId(userName);
            var fileModel = await _fileModelRepository.UploadPassportAsync(uploadedFile);
            if (fileModel == null)
                return null;

            var userDocument = new UserDocument
            {
                AppUserId = userId,
                FileModelId = fileModel.Id,
                FileDataTypeId = (int)FileDataTypeEnum.Passport                
            };

            await _context.AddAsync(userDocument);
            await _context.SaveChangesAsync();

            return userDocument;
        }

        /// <summary>
        /// Загружает на сервер карточку абитуриента и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<UserDocument> CreateAbiturientCard(string userName, IFormFile uploadedFile)
        {
            var userId = _userProfileRepository.GetAppUserId(userName);
            var fileModel = await _fileModelRepository.UploadAbiturientCardAsync(uploadedFile);
            if (fileModel == null)
                return null;

            var userDocument = new UserDocument
            {
                AppUserId = userId,
                FileModelId = fileModel.Id,
                FileDataTypeId = (int)FileDataTypeEnum.AbiturientCard
            };

            await _context.AddAsync(userDocument);
            await _context.SaveChangesAsync();

            return userDocument;
        }

        /// <summary>
        /// Загружает на сервер фотографию абитуриента и
        /// создаёт соответствующую запись в таблице UserDocuments
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<UserDocument> CreatePhoto(string userName, IFormFile uploadedFile)
        {
            var userId = _userProfileRepository.GetAppUserId(userName);
            var fileModel = await _fileModelRepository.UploadUserPhotoAsync(uploadedFile);
            if (fileModel == null)
                return null;

            var userDocument = new UserDocument
            {
                AppUserId = userId,
                FileModelId = fileModel.Id,
                FileDataTypeId = (int)FileDataTypeEnum.UserDocuments_Photo
            };

            await _context.AddAsync(userDocument);
            await _context.SaveChangesAsync();

            return userDocument;
        }

        /// <summary>
        /// Проверяет наличие у пользователя наличия документов указанного типа
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="fileDataType"></param>
        /// <returns></returns>
        public bool IsLoadedUserDocument(string userName, FileDataTypeEnum fileDataType)
        {
            var appUser = GetAppUser(userName);

            if (appUser == null)
                throw new Exception($"Пользователь {userName} не найден!");

            bool isLoadedFile = IsLoadedUserDocument(appUser, fileDataType);
            return isLoadedFile;
        }

        /// <summary>
        /// Проверяет наличие у пользователя документов указанного типа
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="fileDataType"></param>
        /// <returns></returns>
        public bool IsLoadedUserDocument(AppUser appUser, FileDataTypeEnum fileDataType)
        {
            bool isLoadedFile = appUser.UserDocuments.Any(u =>
                u.FileDataTypeId == (int)fileDataType);
            return isLoadedFile;
        }

        /// <summary>
        /// Проверяет наличие у пользователя наличия документов указанной группы типов
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="fileDataTypeGroup"></param>
        /// <returns></returns>
        public bool IsLoadedUserDocument(string userName, FileDataTypeGroupEnum fileDataTypeGroup)
        {
            var appUser = GetAppUser(userName);

            if (appUser == null)
                throw new Exception($"Пользователь {userName} не найден!");

            bool isLoadedUserDocuments = IsLoadedUserDocument(appUser, fileDataTypeGroup);
            return isLoadedUserDocuments;
        }

        /// <summary>
        /// Проверяет наличие у пользователя документов указанной группы типов
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="fileDataTypeGroup"></param>
        /// <returns></returns>
        public bool IsLoadedUserDocument(AppUser appUser, FileDataTypeGroupEnum fileDataTypeGroup)
        {
            var fileDataTypes = _fileModelRepository.GetFileDataTypes(fileDataTypeGroup);

            foreach (var fileDataType in fileDataTypes)
            {
                bool isLoadedFile = appUser.UserDocuments.Any(u =>
                    u.FileDataTypeId == fileDataType.FileDataTypeId);

                if (isLoadedFile)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Удаляет документ пользователя
        /// </summary>
        /// <param name="userDocumentId"></param>
        /// <returns></returns>
        public async Task RemoveUserDocumentAsync(int userDocumentId)
        {            
            var userDocument = await GetUserDocumentAsync(userDocumentId);
            await _fileModelRepository.RemoveUserDocumentAsync(userDocument);
        }

        /// <summary>
        /// Устанавливает замечание к документу
        /// </summary>
        /// <param name="userDocument"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task SetUserDocumentRemarkAsync(UserDocument userDocument, string remark)
        {
            userDocument.Remark = remark;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает запрос на выборку всех пользовательских документов со связями
        /// </summary>
        IQueryable<UserDocument> UserDocuments => _context.UserDocuments
            .Include(ud => ud.AppUser)
                .ThenInclude(ud => ud.Abiturient)
            .Include(ud => ud.FileModel.FileToFileTypes)
            .Include(ud => ud.FileDataType)
            .Include(ud => ud.RowStatus)
            .Include(ud => ud.UserEducations)
            .Include(ud => ud.PassportData);

        /// <summary>
        /// Возвращает запрос на выборку всех пользовательских документов со связями
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserDocument> GetUserDocuments()
        {
            return UserDocuments;
        }

        /// <summary>
        /// Возвращает документ пользователя
        /// </summary>
        /// <param name="userDocumentId"></param>
        /// <returns></returns>
        public async Task<UserDocument> GetUserDocumentAsync(int userDocumentId)
        {
            var userDocument = await UserDocuments.FirstOrDefaultAsync(ud => ud.UserDocumentId == userDocumentId);

            return userDocument;
        }

        /// <summary>
        /// Проверяет наличие у пользователя сведений об образовании
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsUserEducationDataExistsAsync(string userName)
        {
            var userEducationDocuments = await GetUserEducationDocuments(userName).ToListAsync();

            foreach (var userEducationDocument in userEducationDocuments)
            {
                if (userEducationDocument.UserEducations == null || userEducationDocument.UserEducations.Count == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Проверяет наличие у пользователя сведений об образовании
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public async Task<bool> IsUserEducationDataExistsAsync(AppUser appUser)
        {
            if(appUser.UserEducations != null)
            {
                if (appUser.UserEducations.Count > 0)
                    return true; 
                else
                    return false;
            }
            else
            {
                var isExists = await IsUserEducationDataExistsAsync(appUser.UserName);
                return isExists;
            }            
        }



        /// <summary>
        /// Возвращает документы об образовании пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private IQueryable<UserDocument> GetUserEducationDocuments(string userName)
        {
            return GetUserDocuments(FileDataTypeGroupEnum.EducationDocuments)
                .Where(ud => ud.AppUser.UserName == userName);            
        }

        /// <summary>
        /// Возвращает паспорта пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private IQueryable<UserDocument> GetUserPassports(string userName)
        {
            return GetUserDocuments(FileDataTypeEnum.Passport)
                .Where(ud => ud.AppUser.UserName == userName);
        }

        /// <summary>
        /// Возвращает документы пользователя указанного типа
        /// </summary>
        /// <param name="educationDocuments"></param>
        /// <returns></returns>
        private IQueryable<UserDocument> GetUserDocuments(FileDataTypeGroupEnum fileDataTypeGroup)
        {
            var userDocuments = UserDocuments.Where(ud => ud.FileDataType.FileDataTypeGroupId == (int)fileDataTypeGroup);
            return userDocuments;
        }

        /// <summary>
        /// Возвращает документы пользователя указанного типа
        /// </summary>
        /// <param name="educationDocuments"></param>
        /// <returns></returns>
        private IQueryable<UserDocument> GetUserDocuments(FileDataTypeEnum fileDataType)
        {
            var userDocuments = UserDocuments.Where(ud => ud.FileDataTypeId == (int)fileDataType);
            return userDocuments;
        }

        /// <summary>
        /// Возвращает запрос на выборку документов об образовании пользователя, не имеющих заполненных сведений
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<UserDocument> GetUserEducationDocumentsWithoutUserEducationDataAsync(string userName)
        {
            var query = GetUserEducationDocuments(userName).Where(ud => ud.UserEducations == null || ud.UserEducations.Count == 0);
            return query;
        }

        /// <summary>
        /// Возвращает первый объект документа пользователя,
        /// который является паспортом и не связан с объектом паспортных данных
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<UserDocument> GetUserDocumentPassportWithoutPassportDataAsync(string userName)
        {
            var userDocumentPassport = await GetUserPassports(userName)
                .Where(ud => ud.PassportData == null)
                .FirstOrDefaultAsync();

            return userDocumentPassport;
        }

        /// <summary>
        /// Заменяет пользовательский документ
        /// </summary>
        /// <param name="userDocumentId"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task ReloadUserDocumentAsync(int userDocumentId, IFormFile uploadedFile)
        {
            var userDocument = await GetUserDocumentAsync(userDocumentId);
            if (userDocument == null)
                throw new Exception("Пользовательский документ не найден!");

            await _fileModelRepository.ReloadFileAsync(userDocument.FileModel, uploadedFile);          
        }

        /// <summary>
        /// Удаляет все документы пользователя
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public async Task RemoveUserDocumentsAsync(AppUser appUser)
        {
            if (appUser == null)
                return;

            if (appUser.UserDocuments == null)
            {
                
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Обновляет документ пользователя
        /// </summary>
        /// <param name="userDocument"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateUserDocument(UserDocument userDocument, IFormFile uploadedFile)
        {
            if (userDocument == null) throw new NullReferenceException();
            var entry = await GetUserDocumentAsync(userDocument.UserDocumentId);

            entry.AppUserId      = userDocument.AppUserId;            
            entry.RowStatusId    = userDocument.RowStatusId;
            entry.Remark         = userDocument.Remark;

            if(userDocument.FileDataTypeId != entry.FileDataTypeId)
            {
                entry.FileDataTypeId = userDocument.FileDataTypeId;
                foreach (var fileToFileType in entry.FileModel.FileToFileTypes)
                {
                    fileToFileType.FileDataTypeId = userDocument.FileDataTypeId;
                }
            }
            
            if (uploadedFile != null)
            {
                if(entry.FileModelId == 0)
                {
                    var newFileModel = await _fileModelRepository.UploadFileAsync(uploadedFile, (FileDataTypeEnum)entry.FileDataTypeId);
                }
                else
                {
                    await _fileModelRepository.ReloadFileAsync(entry.FileModelId, uploadedFile);
                }
                                
            }

            _context.UserDocuments.Update(entry);

            await _context.SaveChangesAsync();
        }

        
    }
}
