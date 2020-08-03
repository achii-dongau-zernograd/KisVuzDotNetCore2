using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Репозиторий заявлений об отзыве документов абитуриентов
    /// </summary>
    public class RevocationStatementRepository : IRevocationStatementRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public RevocationStatementRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }


        /// <summary>
        /// Возвращает запрос на выборку всех заявлений об отзыве документов абитуриентов
        /// </summary>
        /// <returns></returns>
        public IQueryable<RevocationStatement> GetRevocationStatements()
        {
            var query = _context.RevocationStatements
                .Include(r => r.ApplicationForAdmission.Abiturient.AppUser)
                .Include(r => r.ApplicationForAdmission.EduForm)
                .Include(r => r.ApplicationForAdmission.QuotaType)
                .Include(r => r.FileModel.FileToFileTypes)
                .Include(r => r.RowStatus);

            return query;
        }

        /// <summary>
        /// Возвращает заявление об отзыве документов абитуриента по переданному УИД заявления
        /// </summary>
        /// <param name="revocationStatementId"></param>
        /// <returns></returns>
        private async Task<RevocationStatement> GetRevocationStatementAsync(int revocationStatementId)
        {
            var entry = await GetRevocationStatements().FirstOrDefaultAsync(rs => rs.RevocationStatementId == revocationStatementId);

            return entry;
        }

        /// <summary>
        /// Добавляет заявление об отзыве документов
        /// </summary>
        /// <param name="revocationStatement"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task CreateRevocationStatement(RevocationStatement revocationStatement, IFormFile uploadedFile)
        {
            if(uploadedFile != null)
            {
                var newFileModel = await _fileModelRepository.UploadRevocationStatementAsync(uploadedFile);

                if (newFileModel != null)
                    revocationStatement.FileModelId = newFileModel.Id;
            }

            _context.RevocationStatements.Add(revocationStatement);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет заявление об отзыве документов абитуриента по переданному УИД заявления
        /// </summary>
        /// <param name="revocationStatementId"></param>
        /// <returns></returns>
        public async Task RemoveRevocationStatementAsync(int revocationStatementId)
        {
            var entry = await GetRevocationStatementAsync(revocationStatementId);

            await RemoveRevocationStatementAsync(entry);
        }

        /// <summary>
        /// Удаляет заявление об отзыве документов абитуриента
        /// </summary>
        /// <param name="revocationStatement"></param>
        /// <returns></returns>
        public async Task RemoveRevocationStatementAsync(RevocationStatement revocationStatement)
        {
            _context.RevocationStatements.Remove(revocationStatement);

            if (revocationStatement.FileModel != null)
            {
                await _fileModelRepository.RemoveFileModelAsync(revocationStatement.FileModel);                
            }
                        
            await _context.SaveChangesAsync();
        }

        

        /// <summary>
        /// Удаляет заявление об отзыве документов абитуриента по переданному userName и УИД заявления
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="revocationStatementId"></param>
        /// <returns></returns>
        public async Task RemoveRevocationStatementAsync(string userName, int revocationStatementId)
        {
            var entry = await GetRevocationStatementAsync(revocationStatementId);
            if (entry == null ||
                entry.ApplicationForAdmission.Abiturient.AppUser.UserName != userName)
                return;

            await RemoveRevocationStatementAsync(entry);
        }

        /// <summary>
        /// Обновляет заявление об отзыве документов абитуриента
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="revocationStatement"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateRevocationStatement(string userName, RevocationStatement revocationStatement, IFormFile uploadedFile)
        {
            var entry = await GetRevocationStatementAsync(revocationStatement.RevocationStatementId);

            if (entry == null || entry.ApplicationForAdmission.Abiturient.AppUser.UserName != userName)
                return;

            if (entry.ApplicationForAdmissionId != revocationStatement.ApplicationForAdmissionId)
                entry.ApplicationForAdmissionId = revocationStatement.ApplicationForAdmissionId;

            if (entry.Date != revocationStatement.Date)
                entry.Date = revocationStatement.Date;

            if (entry.RejectionReason != revocationStatement.RejectionReason)
                entry.RejectionReason = revocationStatement.RejectionReason;

            if (entry.GeneratedPdfDocumentPath != revocationStatement.GeneratedPdfDocumentPath)
                entry.GeneratedPdfDocumentPath = revocationStatement.GeneratedPdfDocumentPath;

            if (entry.Remark != revocationStatement.Remark)
                entry.Remark = revocationStatement.Remark;

            if (entry.RowStatusId != revocationStatement.RowStatusId)
                entry.RowStatusId = revocationStatement.RowStatusId;

            await _context.SaveChangesAsync();

            if (uploadedFile != null)
            {
                if(entry.FileModel != null)
                {
                    await _fileModelRepository.ReloadFileAsync(entry.FileModel, uploadedFile);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newFileModel = await _fileModelRepository.UploadRevocationStatementAsync(uploadedFile);
                    entry.FileModel = newFileModel;
                    entry.FileModelId = newFileModel.Id;

                    await _context.SaveChangesAsync();
                }                                
            }

            //_context.RevocationStatements.Update(entry);

            //await _context.SaveChangesAsync();
        }

    }
}
