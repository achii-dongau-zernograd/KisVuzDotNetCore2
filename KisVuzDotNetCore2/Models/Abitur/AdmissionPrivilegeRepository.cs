using KisVuzDotNetCore2.Models.Common;
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
    /// Льгота абитуриента при поступлении
    /// </summary>
    public class AdmissionPrivilegeRepository : IAdmissionPrivilegeRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public AdmissionPrivilegeRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Создание льготы абитуриента при приёме
        /// </summary>
        /// <param name="admissionPrivilege"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task CreateAdmissionPrivilegeAsync(AdmissionPrivilege admissionPrivilege,
            IFormFile uploadedFile)
        {
            if (admissionPrivilege == null) return;
            if (uploadedFile == null) return;

            var fileModel = await _fileModelRepository.UploadAdmissionPrivilegeFileAsync(uploadedFile);
            if (fileModel == null) return;

            admissionPrivilege.FileModelId = fileModel.Id;
            
            _context.AdmissionPrivileges.Add(admissionPrivilege);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает объект льготы абитуриента при поступлении
        /// </summary>
        /// <param name="admissionPrivilegeId"></param>
        /// <returns></returns>
        public async Task<AdmissionPrivilege> GetAdmissionPrivilegeAsync(int admissionPrivilegeId)
        {
            var admissionPrivilege = await GetAdmissionPrivileges()
                .SingleOrDefaultAsync(ap => ap.AdmissionPrivilegeId == admissionPrivilegeId);

            return admissionPrivilege;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех льгот абитуриентов при поступлении
        /// </summary>
        /// <returns></returns>
        public IQueryable<AdmissionPrivilege> GetAdmissionPrivileges()
        {
            var query = _context.AdmissionPrivileges
                .Include(ap => ap.AdmissionPrivilegeType)
                .Include(ap => ap.RowStatus)
                .Include(ap => ap.FileModel)
                    .ThenInclude(fm => fm.FileToFileTypes)
                        .ThenInclude(ftft => ftft.FileDataType)
                            .ThenInclude(fdt => fdt.FileDataTypeGroup)
                .Include(ap => ap.ApplicationForAdmission)
                    .ThenInclude(afa => afa.Abiturient.AppUser)
                .Include(ap => ap.ApplicationForAdmission)
                    .ThenInclude(afa => afa.EduForm)
                .Include(ap => ap.ApplicationForAdmission)
                    .ThenInclude(afa => afa.QuotaType)
                .Include(ap => ap.ApplicationForAdmission)
                    .ThenInclude(afa => afa.EduProfile.EduNapravl.EduUgs.EduLevel);

            return query;
        }

        /// <summary>
        /// Удаляет льготу абитуриента при поступлении
        /// </summary>
        /// <param name="admissionPrivilegeId"></param>
        /// <returns></returns>
        public async Task RemoveAdmissionPrivilegeAsync(AdmissionPrivilege admissionPrivilege)
        {
            if (admissionPrivilege == null) return;

            if(admissionPrivilege.FileModelId != null)
            {
                if(admissionPrivilege.FileModel != null)
                {
                    await _fileModelRepository.RemoveFileModelAsync(admissionPrivilege.FileModel);
                }
                else
                {
                    await _fileModelRepository.RemoveFileAsync(admissionPrivilege.FileModelId);
                }
            }

            _context.AdmissionPrivileges.Remove(admissionPrivilege);
            await _context.SaveChangesAsync();            
        }

        /// <summary>
        /// Обновляет льготу абитуриента при поступлении
        /// </summary>
        /// <param name="admissionPrivilege"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateAdmissionPrivilegeAsync(AdmissionPrivilege admissionPrivilege, IFormFile uploadedFile)
        {
            if (admissionPrivilege == null) return;

            //var entry = await GetAdmissionPrivilegeAsync(admissionPrivilege.AdmissionPrivilegeId);
            //entry.AdmissionPrivilegeTypeId = admissionPrivilege.AdmissionPrivilegeTypeId;
            //entry.ApplicationForAdmissionId = admissionPrivilege.ApplicationForAdmissionId;
            ////entry.FileModelId = admissionPrivilege.FileModelId;
            //entry.Remark = admissionPrivilege.Remark;
            //entry.RowStatusId = admissionPrivilege.RowStatusId;

            if (uploadedFile != null)//Если пользователь загрузил новый файл
            {
                if(admissionPrivilege.FileModelId == null)//Если ранее загруженный файл отсутствует, создаём новую файловую модель
                {
                    var newFileModel = await _fileModelRepository.UploadAdmissionPrivilegeFileAsync(uploadedFile);
                    admissionPrivilege.FileModelId = newFileModel.Id;
                }
                else//Иначе заменяем имеющуюся модель на новую
                {
                    if(admissionPrivilege.FileModel == null)
                    {
                        await _fileModelRepository.ReloadFileAsync(admissionPrivilege.FileModelId, uploadedFile);
                    }
                    else
                    {
                        await _fileModelRepository.ReloadFileAsync(admissionPrivilege.FileModel, uploadedFile);
                    }
                }
            }

            _context.AdmissionPrivileges.Update(admissionPrivilege);
            await _context.SaveChangesAsync();
        }
    }
}
