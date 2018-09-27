using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Files
{
    /// <summary>
    /// Ре
    /// </summary>
    public class FileModelRepository : IFileModelRepository
    {
        private readonly AppIdentityDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public FileModelRepository(AppIdentityDBContext context,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Удаляет объект FileModel и файл на диске
        /// </summary>
        /// <param name="fileModelId"></param>
        public async Task RemoveFileAsync(int? fileModelId)
        {
            if (fileModelId != null)
            {
                var fileModel = await _context.Files.FirstOrDefaultAsync(f => f.Id == fileModelId);
                if (fileModel != null)
                {
                    _context.Files.Remove(fileModel);

                    if (!string.IsNullOrEmpty(fileModel.Path))
                    {
                        string PathToFile = Path.Combine(_appEnvironment.WebRootPath, fileModel.Path);

                        if (System.IO.File.Exists(PathToFile))
                        {
                            System.IO.File.Delete(PathToFile);
                        }
                    }
                }
            }            
        }

        /// <summary>
        /// Загружает файл аннотации на диск
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadEduAnnotationAsync(IFormFile uploadedFile)
        {
            return await UploadFileAsync(uploadedFile, "Аннотация", FileDataTypeEnum.Annotation);
        }

        /// <summary>
        /// Создаёт объект FileModel и загружает его в папку www/files
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_appEnvironment"></param>
        /// <param name="uploadedFile"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadFileAsync(IFormFile uploadedFile, string name, FileDataTypeEnum fileDataTypeEnum)
        {
            FileModel fileModel = new FileModel();
            fileModel.ContentType = uploadedFile.ContentType;

            fileModel.Name = name;

            string fileName = DateTime.Now.ToString("yyyy-MM-dd-") +
                Guid.NewGuid().ToString() +
                Path.GetExtension(uploadedFile.FileName);
            // путь к папке files
            string[] paths = { _appEnvironment.WebRootPath, "files", fileName };
            string path = Path.Combine(paths);
            // сохраняем файл в папку files в каталоге wwwroot
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            fileModel.FileName = Path.GetFileName(uploadedFile.FileName);
            fileModel.Path = Path.Combine("files", fileName); ;

            if (Path.GetExtension(uploadedFile.FileName) == ".pdf")
            {
                fileModel.ContentType = "application/pdf";
            }

            _context.Files.Add(fileModel);
            await _context.SaveChangesAsync();


            FileToFileType ftf = new FileToFileType
            {
                FileDataTypeId = (int)fileDataTypeEnum,
                FileModelId = fileModel.Id
            };
            _context.FileToFileTypes.Add(ftf);
            await _context.SaveChangesAsync();

            return fileModel;
        }
    }
}
