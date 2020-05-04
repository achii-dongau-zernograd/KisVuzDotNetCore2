using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
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
    public class AbiturRepository : IAbiturRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IUserDocumentRepository _userDocumentRepository;

        public AbiturRepository(AppIdentityDBContext context,
            IUserDocumentRepository userDocumentRepository)
        {
            _context = context;
            _userDocumentRepository = userDocumentRepository;
        }               

        /// <summary>
        /// Определяет, является ли пользователь абитуриентом
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsAbitur(string userName)
        {
            bool isAbitur = _context.Users.Any(u =>
                u.UserName == userName &&
                (u.AppUserStatusId == (int)AppUserStatusEnum.NewAbitur ||
                 u.AppUserStatusId == (int)AppUserStatusEnum.ConfirmedAbitur)
            );
            return isAbitur;
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
    }
}
