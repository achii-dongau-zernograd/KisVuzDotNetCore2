using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Priem;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Репозиторий заявлений о согласии на зачисление
    /// </summary>
    public class ConsentToEnrollmentRepository : IConsentToEnrollmentRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public ConsentToEnrollmentRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        /// <summary>
        /// Создание заявления о согласии на зачисление
        /// </summary>
        /// <param name="admissionPrivilege"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task CreateConsentToEnrollmentAsync(ConsentToEnrollment consentToEnrollment,
            IFormFile uploadedFile)
        {
            if (consentToEnrollment == null) return;
            if (uploadedFile == null) return;

            var fileModel = await _fileModelRepository.UploadConsentToEnrollmentFileAsync(uploadedFile);
            if (fileModel == null) return;

            consentToEnrollment.FileModelId = fileModel.Id;

            if(consentToEnrollment.RowStatusId == 0)
            {
                consentToEnrollment.RowStatusId = (int)RowStatusEnum.NotConfirmed;
            }

            

            _context.ConsentToEnrollments.Add(consentToEnrollment);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает объект заявления о согласии на зачисление
        /// </summary>
        /// <param name="consentToEnrollmentId"></param>
        /// <returns></returns>
        public async Task<ConsentToEnrollment> GetConsentToEnrollmentAsync(int consentToEnrollmentId)
        {
            var consentToEnrollment = await GetConsentToEnrollments()
                .SingleOrDefaultAsync(c => c.ConsentToEnrollmentId == consentToEnrollmentId);

            return consentToEnrollment;
        }

        /// <summary>
        /// Возвращает запрос на выборку всех заявлений о согласии на зачисление
        /// </summary>
        /// <returns></returns>
        public IQueryable<ConsentToEnrollment> GetConsentToEnrollments()
        {
            var query = _context.ConsentToEnrollments                
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
        /// Возвращает запрос на выборку всех объектов заявлений о согласии на зачисление,
        /// удовлетворяющих заданному фильтру
        /// </summary>
        /// <param name="filterAndSortModel"></param>
        /// <returns></returns>
        public IQueryable<ConsentToEnrollment> GetConsentToEnrollments(ConsentToEnrollmentsFilterAndSortModel filterAndSortModel)
        {
            var query = GetConsentToEnrollments();

            if (!string.IsNullOrWhiteSpace(filterAndSortModel.FilterLastNameFragment))
            {
                query = query.Where(a => a.ApplicationForAdmission.Abiturient.AppUser.LastName.Contains(filterAndSortModel.FilterLastNameFragment));
            }

            if (filterAndSortModel.EduFormId != 0)
            {
                query = query.Where(a => a.ApplicationForAdmission.EduFormId == filterAndSortModel.EduFormId);
            }

            if (filterAndSortModel.EduProfileId != 0)
            {
                query = query.Where(a => a.ApplicationForAdmission.EduProfileId == filterAndSortModel.EduProfileId);
            }

            if (filterAndSortModel.QuotaTypeId != 0)
            {
                query = query.Where(a => a.ApplicationForAdmission.QuotaTypeId == filterAndSortModel.QuotaTypeId);
            }

            if (filterAndSortModel.RowStatusId != 0)
            {
                query = query.Where(a => a.RowStatusId == filterAndSortModel.RowStatusId);
            }

            return query;
        }

        /// <summary>
        /// Удаляет заявление о согласии на зачисление
        /// </summary>        
        public async Task RemoveConsentToEnrollmentAsync(ConsentToEnrollment consentToEnrollment)
        {
            if (consentToEnrollment == null) return;

            if(consentToEnrollment.FileModelId != null)
            {
                if(consentToEnrollment.FileModel != null)
                {
                    await _fileModelRepository.RemoveFileModelAsync(consentToEnrollment.FileModel);
                }
                else
                {
                    await _fileModelRepository.RemoveFileAsync(consentToEnrollment.FileModelId);
                }
            }

            _context.ConsentToEnrollments.Remove(consentToEnrollment);
            await _context.SaveChangesAsync();            
        }

        /// <summary>
        /// Обновляет заявление о согласии на зачисление
        /// </summary>
        /// <param name="consentToEnrollment"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateConsentToEnrollmentAsync(ConsentToEnrollment consentToEnrollment, IFormFile uploadedFile)
        {
            if (consentToEnrollment == null) return;

            if (uploadedFile != null)//Если пользователь загрузил новый файл
            {
                if(consentToEnrollment.FileModelId == null)//Если ранее загруженный файл отсутствует, создаём новую файловую модель
                {
                    var newFileModel = await _fileModelRepository.UploadConsentToEnrollmentFileAsync(uploadedFile);
                    consentToEnrollment.FileModelId = newFileModel.Id;                    
                }
                else//Иначе заменяем имеющуюся модель на новую
                {
                    if(consentToEnrollment.FileModel == null)
                    {
                        await _fileModelRepository.ReloadFileAsync(consentToEnrollment.FileModelId, uploadedFile);
                    }
                    else
                    {
                        await _fileModelRepository.ReloadFileAsync(consentToEnrollment.FileModel, uploadedFile);
                    }
                }
            }

            _context.ConsentToEnrollments.Update(consentToEnrollment);
            await _context.SaveChangesAsync();
        }
    }
}
