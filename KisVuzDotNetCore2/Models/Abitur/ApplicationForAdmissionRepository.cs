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
    /// Репозиторий заявлений о приёме
    /// </summary>
    public class ApplicationForAdmissionRepository : IApplicationForAdmissionRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;
        public ApplicationForAdmissionRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Проверяет наличие заявлений о приёме для указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsApplicationForAdmissionExists(string userName)
        {

            var applicationsForAdmission = GetApplicationForAdmissions(userName);
            if (applicationsForAdmission != null && applicationsForAdmission.Count() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех заявлений о приёме указанного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<ApplicationForAdmission> GetApplicationForAdmissions(string userName)
        {
            var query = GetApplicationForAdmissions()
                .Where(a => a.Abiturient.AppUser.UserName == userName);

            return query;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех заявлений о приёме
        /// </summary>
        /// <returns></returns>
        public IQueryable<ApplicationForAdmission> GetApplicationForAdmissions()
        {
            var query = _context.ApplicationForAdmissions
                .Include(afa => afa.Abiturient.AppUser)
                .Include(afa => afa.EduForm)
                .Include(afa => afa.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(afa => afa.QuotaType)
                .Include(afa => afa.RowStatus)
                // Льготы
                .Include(afa => afa.AdmissionPrivileges)
                    .ThenInclude(ap => ap.AdmissionPrivilegeType)
                .Include(afa => afa.AdmissionPrivileges)
                    .ThenInclude(ap => ap.RowStatus)
                .Include(afa => afa.AdmissionPrivileges)
                    .ThenInclude(ap => ap.FileModel)
                        .ThenInclude(fm => fm.FileToFileTypes)
                            .ThenInclude(ftft => ftft.FileDataType)
                                .ThenInclude(fdt => fdt.FileDataTypeGroup)
                // Файл заявления о приёме
                .Include(afa => afa.FileModel)
                    .ThenInclude(fm => fm.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileDataType)
                            .ThenInclude(fdt => fdt.FileDataTypeGroup);

            return query;
        }

        /// <summary>
        /// Возвращает количество заявлений о приёме, созданных указанным пользователем
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetNumberOfApplicationForAdmissions(string userName)
        {
            int result = GetApplicationForAdmissions(userName).Count();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationForAdmissionId"></param>
        /// <returns></returns>
        public async Task<ApplicationForAdmission> GetApplicationForAdmissionAsync(int applicationForAdmissionId)
        {
            var applicationForAdmission = await GetApplicationForAdmissions()
                .FirstOrDefaultAsync(a => a.ApplicationForAdmissionId == applicationForAdmissionId);

            return applicationForAdmission;
        }

        /// <summary>
        /// Обновляет заявление о зачислении
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <returns></returns>
        public async Task UpdateApplicationForAdmissionAsync(ApplicationForAdmission applicationForAdmission)
        {
            _context.ApplicationForAdmissions.Update(applicationForAdmission);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет заявление о приёме
        /// </summary>
        /// <param name="applicationForAdmission"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateApplicationForAdmissionAsync(ApplicationForAdmission applicationForAdmission, IFormFile uploadedFile)
        {
            if(uploadedFile != null)
            {
                if(applicationForAdmission.FileModelId == null)
                {
                    var newFileModel = await _fileModelRepository.UploadApplicationForAdmissionFileAsync(uploadedFile);
                    applicationForAdmission.FileModelId = newFileModel.Id;
                }
                else
                {
                    if (applicationForAdmission.FileModel == null)
                        applicationForAdmission.FileModel = await _fileModelRepository.GetFileModelAsync(applicationForAdmission.FileModelId);

                    await _fileModelRepository.ReloadFileAsync(applicationForAdmission.FileModel, uploadedFile);
                }
            }

            await UpdateApplicationForAdmissionAsync(applicationForAdmission);            
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
            var applicationForAdmission = await GetApplicationForAdmissionAsync(applicationForAdmissionId);
            if (applicationForAdmission == null)
                return;

            if (applicationForAdmission.FileModelId != null)
                return;

            if (applicationForAdmission.Abiturient.AppUser.UserName != userName)
                return;

            var fileModel = await _fileModelRepository.UploadApplicationForAdmissionFileAsync(uploadedFile);

            applicationForAdmission.FileModelId = fileModel.Id;
            await _context.SaveChangesAsync();            
        }

        /// <summary>
        /// Удаляет заявления о приёме
        /// </summary>
        /// <param name="applicationForAdmissions"></param>
        /// <returns></returns>
        public async Task RemoveApplicationForAdmissionsAsync(List<ApplicationForAdmission> applicationForAdmissions)
        {
            if (applicationForAdmissions != null && applicationForAdmissions.Count() > 0)
            {
                foreach (var applicationForAdmission in applicationForAdmissions)
                {
                    if (applicationForAdmission.FileModelId != null)
                    {
                        await _fileModelRepository.RemoveFileModelAsync(applicationForAdmission.FileModel);
                    }
                }

                _context.ApplicationForAdmissions.RemoveRange(applicationForAdmissions);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Удаляет заявление о приёме
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public async Task RemoveApplicationForAdmissionAsync(ApplicationForAdmission entry)
        {
            if (entry.FileModelId != null)
            {
                await _fileModelRepository.RemoveFileModelAsync(entry.FileModel);
            }

            _context.ApplicationForAdmissions.Remove(entry);
            await _context.SaveChangesAsync();
        }
    }
}
