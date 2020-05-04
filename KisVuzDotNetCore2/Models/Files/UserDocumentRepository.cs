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
            var userDocument = await _context.UserDocuments
                .Include(ud => ud.FileModel)
                .SingleOrDefaultAsync(m => m.UserDocumentId == userDocumentId);
            _context.UserDocuments.Remove(userDocument);

            await _fileModelRepository.RemoveFileModelAsync(userDocument.FileModel);

            await _context.SaveChangesAsync();
        }        
    }
}
