using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models.Files;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    /// <summary>
    /// Репозиторий протоколов вступительных испытаний
    /// </summary>
    public class EntranceTestsProtocolsRepository : IEntranceTestsProtocolsRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IUserDocumentRepository _userDocumentRepository;
        private readonly IPdfDocumentGenerator _pdfDocumentGenerator;

        public EntranceTestsProtocolsRepository(AppIdentityDBContext context,
            IUserDocumentRepository userDocumentRepository,
            IPdfDocumentGenerator pdfDocumentGenerator)
        {
            _context = context;
            _userDocumentRepository = userDocumentRepository;
            _pdfDocumentGenerator = pdfDocumentGenerator;
        }

                

        public IQueryable<EntranceTestsProtocol> GetEntranceTestsProtocols()
        {
            var query = _context.EntranceTestsProtocols
                .Include(e => e.Abiturient.AppUser)
                .Include(e => e.Abiturient.EntranceTestRegistrationForms);

            return query;
        }

        /// <summary>
        /// Возвращает протокол вступительных испытаний
        /// </summary>
        /// <param name="entranceTestsProtocolId"></param>
        /// <returns></returns>
        public async Task<EntranceTestsProtocol> GetEntranceTestsProtocol(int entranceTestsProtocolId)
        {
            var entry = await GetEntranceTestsProtocols().FirstOrDefaultAsync(e => e.EntranceTestsProtocolId == entranceTestsProtocolId);

            return entry;
        }


        /// <summary>
        /// Создаёт pdf-файл протокола и возвращает объект протокола вступительных испытаний
        /// </summary>
        /// <param name="entranceTestsProtocolId"></param>
        /// <returns></returns>
        public async Task<EntranceTestsProtocol> GeneratePdf(int entranceTestsProtocolId)
        {
            var entry = await GetEntranceTestsProtocol(entranceTestsProtocolId);
            var userPhoto = await _userDocumentRepository.GetUserDocuments().Where(ud => ud.FileDataTypeId == (int)FileDataTypeEnum.UserDocuments_Photo).FirstOrDefaultAsync();

            string newFileName = _pdfDocumentGenerator.GenerateEntranceTestsProtocol(entry, userPhoto.FileModel.Path);

            

            return entry;
        }
    }
}
