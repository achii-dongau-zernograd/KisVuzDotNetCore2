using KisVuzDotNetCore2.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Репозиторий "Информация о профессионально-общественной и/или общественной аккредитации образовательной программы"
    /// </summary>
    public class EduPOAccredRepository : IEduPOAccredRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public EduPOAccredRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }


        /// <summary>
        /// Возвращает запрос на выборку информации о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        public IQueryable<EduPOAccred> GetEduPOAccreds()
        {
            var query = _context.EduPOAccreds
                .Include(d => d.EduNapravl.EduUgs.EduLevel)
                .Include(d => d.EduNapravl.EduProfiles)
                .Include(d => d.EduPOAccredPdf/*.FileDataTypeGroup*/)
                .OrderByDescending(d => d.EduNapravl.EduUgs.EduLevel.EduLevelName)
                .ThenBy(d => d.EduNapravl.EduNapravlCode);

            return query;
        }

        /// <summary>
        /// Возвращает информацию о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        public async Task<EduPOAccred> GetAsync(int id)
        {
            var entry = await GetEduPOAccreds().FirstOrDefaultAsync(a => a.EduPOAccredId == id);
            return entry;
        }

        /// <summary>
        /// Добавляет информацию о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        public async Task AddAsync(EduPOAccred eduPOAccred, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var newFileModel = await _fileModelRepository.UploadFileAsync(uploadedFile, FileDataTypeEnum.SvidetelstvoOProfObschAccreditatsii);
                eduPOAccred.EduPOAccredPdfId = newFileModel.Id;
            }

            _context.Add(eduPOAccred);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет информацию о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        public async Task RemoveAsync(int id)
        {
            var entry = await GetAsync(id);
            if (entry == null) return;

            if (entry.EduPOAccredPdfId != null)
            {
                await _fileModelRepository.RemoveFileModelAsync(entry.EduPOAccredPdf);
            }

            _context.Remove(entry);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет информацию о профессионально-общественной и/или общественной аккредитации образовательной программы
        /// </summary>
        public async Task UpdateAsync(EduPOAccred eduPOAccred, IFormFile uploadedFile)
        {
            var entry = await GetAsync(eduPOAccred.EduPOAccredId);

            if (entry == null) return;

            entry.EduNapravlId = eduPOAccred.EduNapravlId;
            entry.AccredOrgName = eduPOAccred.AccredOrgName;
            entry.AccredDateEnd = eduPOAccred.AccredDateEnd;

            if (uploadedFile != null)
            {
                if (entry.EduPOAccredPdf != null)
                {
                    await _fileModelRepository.ReloadFileAsync(entry.EduPOAccredPdf, uploadedFile);
                }
                else
                {
                    var newFileModel = await _fileModelRepository.UploadFileAsync(uploadedFile, (FileDataTypeEnum)entry.EduPOAccredPdfId);
                    entry.EduPOAccredPdfId = newFileModel.Id;
                }
            }

            _context.Update(entry);
            await _context.SaveChangesAsync();
        }
    }
}
