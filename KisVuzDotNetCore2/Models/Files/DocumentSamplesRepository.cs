using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Files
{
    /// <summary>
    /// Репозиторий бланков и примеров заполнения документов
    /// </summary>
    public class DocumentSamplesRepository : IDocumentSamplesRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IFileModelRepository _fileModelRepository;

        public DocumentSamplesRepository(AppIdentityDBContext context,
            IFileModelRepository fileModelRepository)
        {
            _context = context;
            _fileModelRepository = fileModelRepository;
        }

        
        /// <summary>
        /// Возвращает запрос на выборку бланков и примеров заполнения документов
        /// </summary>
        /// <param name="fileDataTypes"></param>
        /// <returns></returns>
        public IQueryable<DocumentSample> GetDocumentSamples()
        {
            var query = _context.DocumentSamples
                .Include(d => d.EduProfile.EduNapravl.EduUgs.EduLevel)
                .Include(d => d.FileDataType.FileDataTypeGroup)
                .Include(d => d.FileModel);

            return query;
        }

        /// <summary>
        /// Возвращает запрос на выборку бланков и примеров заполнения документов заданных типов
        /// </summary>
        /// <param name="fileDataTypes"></param>
        /// <returns></returns>
        public IQueryable<DocumentSample> GetDocumentSamples(params FileDataTypeEnum[] fileDataTypes)
        {
            var query = GetDocumentSamples();

            int numParams = fileDataTypes.Length;

            switch (numParams)
            {
                case 1:
                    return query.Where(q => q.FileDataTypeId == (int)fileDataTypes[0]);
                case 2:
                    return query.Where(q => q.FileDataTypeId == (int)fileDataTypes[0] || q.FileDataTypeId == (int)fileDataTypes[1]);
                case 3:
                    return query.Where(q => q.FileDataTypeId == (int)fileDataTypes[0] || q.FileDataTypeId == (int)fileDataTypes[1] || q.FileDataTypeId == (int)fileDataTypes[2]);
                default:
                    throw new NotImplementedException();                    
            }            
        }

        /// <summary>
        /// Возвращает запрос на выборку бланков
        /// и примеров заполнения документов
        /// для указанного профиля заданных типов
        /// </summary>
        /// <param name="eduProfileId"></param>
        /// <param name="fileDataTypes"></param>
        /// <returns></returns>
        public IQueryable<DocumentSample> GetDocumentSamples(int eduProfileId, params FileDataTypeEnum[] fileDataTypes)
        {
            var query = GetDocumentSamples(fileDataTypes).Where(q => q.EduProfileId == eduProfileId);

            return query;
        }

        /// <summary>
        /// Возвращает объект бланка или примера
        /// заполнения документов по УИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DocumentSample> GetDocumentSampleAsync(int documentSampleId)
        {
            var documentSample = await GetDocumentSamples().SingleOrDefaultAsync(d=>d.DocumentSampleId == documentSampleId);
            return documentSample;
        }

        /// <summary>
        /// Добавляет бланк или пример заполнения
        /// </summary>
        /// <param name="documentSample"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task AddDocumentSampleAsync(DocumentSample documentSample, IFormFile uploadedFile)
        {
            if(uploadedFile != null)
            {
                var newFileModel = await _fileModelRepository.UploadFileAsync(uploadedFile, (FileDataTypeEnum)documentSample.FileDataTypeId);
                documentSample.FileModelId = newFileModel.Id;
            }

            _context.DocumentSamples.Add(documentSample);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет бланк или пример заполнения документа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveDocumentSampleAsync(int id)
        {
            var documentSample = await GetDocumentSampleAsync(id);
            if (documentSample == null) return;

            if(documentSample.FileModel != null)
            {
                await _fileModelRepository.RemoveFileModelAsync(documentSample.FileModel);
            }

            _context.DocumentSamples.Remove(documentSample);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет бланк или пример заполнения документа
        /// </summary>
        /// <param name="documentSample"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task UpdateDocumentSampleAsync(DocumentSample documentSample, IFormFile uploadedFile)
        {
            var entry = await GetDocumentSampleAsync(documentSample.DocumentSampleId);

            if (entry == null) return;

            entry.DocumentSampleName = documentSample.DocumentSampleName;
            entry.FileDataTypeId = documentSample.FileDataTypeId;
            entry.IsBlank = documentSample.IsBlank;
            entry.EduProfileId = documentSample.EduProfileId;
            entry.Link = documentSample.Link;

            if (!string.IsNullOrWhiteSpace(documentSample.Link))
            {
                if(entry.FileModel != null)
                {
                    await _fileModelRepository.RemoveFileModelAsync(entry.FileModel);
                }                
            }
            else
            {
                if(uploadedFile != null)
                {
                    if (entry.FileModel != null)
                    {
                        await _fileModelRepository.ReloadFileAsync(entry.FileModel, uploadedFile);
                    }
                    else
                    {
                        var newFileModel = await _fileModelRepository.UploadFileAsync(uploadedFile, (FileDataTypeEnum)entry.FileDataTypeId);
                        entry.FileModelId = newFileModel.Id;
                    }
                }                
            }

            _context.DocumentSamples.Update(entry);
            await _context.SaveChangesAsync();            
        }
    }
}
