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
    /// Репозиторий индивидуальных достижений абитуриентов
    /// </summary>
    public class AbiturientIndividualAchievmentRepository : IAbiturientIndividualAchievmentRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public AbiturientIndividualAchievmentRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Возвращает индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AbiturientIndividualAchievment> GetAbiturientIndividualAchievmentAsync(int id)
        {
            var achievment = await GetAbiturientIndividualAchievments().SingleOrDefaultAsync(a => a.AbiturientIndividualAchievmentId == id);

            return achievment;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех индивидуальных достижений всех абитуриентов
        /// </summary>
        /// <returns></returns>
        public IQueryable<AbiturientIndividualAchievment> GetAbiturientIndividualAchievments()
        {
            var query = _context.AbiturientIndividualAchievments
                .Include(a => a.AbiturientIndividualAchievmentType)
                .Include(a => a.Abiturient.AbiturientStatus)
                .Include(a => a.Abiturient.AppUser)
                .Include(a => a.RowStatus)
                .Include(a => a.FileModel.FileToFileTypes);
            return query;
        }

        
        /// <summary>
        /// Обновляет индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievment"></param>
        /// <returns></returns>
        public async Task Update(AbiturientIndividualAchievment abiturientIndividualAchievment,
            IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                if (abiturientIndividualAchievment.FileModelId != null)
                {
                    if (abiturientIndividualAchievment.FileModel == null)
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
        public async Task Remove(int abiturientIndividualAchievmentId)
        {
            var entry = await GetAbiturientIndividualAchievmentAsync(abiturientIndividualAchievmentId);

            if (entry.FileModel != null)
                await _fileModelRepository.RemoveFileModelAsync(entry.FileModel);

            _context.AbiturientIndividualAchievments.Remove(entry);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Создаёт индивидуальное достижение абитуриента
        /// </summary>
        /// <param name="abiturientIndividualAchievment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task Create(AbiturientIndividualAchievment abiturientIndividualAchievment, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var loadedFileModel = await _fileModelRepository.UploadIndividualAchievmentFile(uploadedFile);
                abiturientIndividualAchievment.FileModel = loadedFileModel;
                abiturientIndividualAchievment.FileModelId = loadedFileModel.Id;
            }

            _context.AbiturientIndividualAchievments.Add(abiturientIndividualAchievment);
            await _context.SaveChangesAsync();
        }
    }
}
