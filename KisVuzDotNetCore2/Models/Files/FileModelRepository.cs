using KisVuzDotNetCore2.Models.Nir;
using KisVuzDotNetCore2.Models.Students;
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
                    await RemoveFileModelAsync(fileModel);
                }
            }            
        }

        /// <summary>
        /// Удаляет объект FileModel и файл на диске
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        public async Task RemoveFileModelAsync(FileModel fileModel)
        {
            if (fileModel == null) return;
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
        /// Загружает файл рабочей программы на диск
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadRabProgramAsync(IFormFile uploadedFile)
        {
            return await UploadFileAsync(uploadedFile, "Рабочая программа", FileDataTypeEnum.RabProgrammaDisciplini);
        }

        /// <summary>
        /// Загружает файл фонда оценочнгых средств на диск
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadFondOcenochnihSredstvAsync(IFormFile uploadedFile)
        {
            return await UploadFileAsync(uploadedFile, "Фонд оценочных средств", FileDataTypeEnum.FondOcenochnihSredstv);
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

            string fileName = /*DateTime.Now.ToString("yyyy-MM-dd-") +*/
                Guid.NewGuid().ToString() +
                Path.GetExtension(uploadedFile.FileName);
            // путь к папке files
            string[] paths = { _appEnvironment.WebRootPath, "files", fileName };

            string pathToDirectory = Path.Combine(paths[0],paths[1]);
            if (!Directory.Exists(pathToDirectory))
                Directory.CreateDirectory(pathToDirectory);

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

        /// <summary>
        /// Загружает учебное пособие
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadUchPosobieAsync(IFormFile uploadedFile,
            FileDataTypeEnum fileDataTypeEnum = FileDataTypeEnum.UchebnoePosobie)
        {            
            return await UploadFileAsync(uploadedFile, "Учебное пособие", fileDataTypeEnum);
        }

        /// <summary>
        /// Загружает договор с электронной-библиотечной системой
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadElectronBiblSystemDogovorAsync(IFormFile uploadedFile)
        {
            return await UploadFileAsync(uploadedFile, "Договор с ЭБС", FileDataTypeEnum.DogovorElBiblSystem);
        }

        /// <summary>
        /// Загружает файл научной статьи (с заменой ранее загруженного)
        /// </summary>
        /// <param name="article">Научная статья</param>
        /// <param name="uploadFile">Загружаемый файл</param>
        /// <returns></returns>
        public async Task<FileModel> UploadArticleAsync(Article article,IFormFile uploadedFile)
        {
            if (article == null || uploadedFile == null) return null;

            FileModel newFileModel = await UploadFileAsync(uploadedFile, "Научная статья", FileDataTypeEnum.Article);

            if (newFileModel != null)
            {
                int? existingFileModelId = article.FileModelId;
                article.FileModelId = newFileModel.Id;
                article.FileModel = newFileModel;
                await _context.SaveChangesAsync();
                // Проверка наличия ранее загруженного файла
                if (existingFileModelId > 0)
                {
                    await RemoveFileAsync(existingFileModelId);
                    await _context.SaveChangesAsync();
                }                 
            }
            else
            {
                return null;
            }

            return article.FileModel;
        }

        /// <summary>
        /// Загружает файл патента (свидетельства) (с заменой ранее загруженного)
        /// </summary>
        /// <param name="patent"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadPatentAsync(Patent patent, IFormFile uploadedFile)
        {
            if (patent == null || uploadedFile == null) return null;

            FileModel newFileModel = await UploadFileAsync(uploadedFile, "Патент (свидетельство)", (FileDataTypeEnum)patent.PatentVidId);

            if (newFileModel != null)
            {
                int? existingFileModelId = patent.FileModelId;
                patent.FileModelId = newFileModel.Id;
                patent.FileModel = newFileModel;
                await _context.SaveChangesAsync();
                // Проверка наличия ранее загруженного файла
                if (existingFileModelId > 0)
                {
                    await RemoveFileAsync(existingFileModelId);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                return null;
            }

            return patent.FileModel;
        }

        /// <summary>
        /// Загружает файл монографии (с заменой ранее загруженного)
        /// </summary>
        /// <param name="patent"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadMonografAsync(Monograf monograf, IFormFile uploadFile)
        {
            if (monograf == null || uploadFile == null) return null;

            FileModel newFileModel = await UploadFileAsync(uploadFile, "Монография", FileDataTypeEnum.Monografiya);

            if (newFileModel != null)
            {
                int? existingFileModelId = monograf.FileModelId;
                monograf.FileModelId = newFileModel.Id;
                monograf.FileModel = newFileModel;
                await _context.SaveChangesAsync();
                // Проверка наличия ранее загруженного файла
                if (existingFileModelId > 0)
                {
                    await RemoveFileAsync(existingFileModelId);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                return null;
            }

            return monograf.FileModel;
        }

        /// <summary>
        /// Загружает файл результатов освоения образовательной программы
        /// </summary>
        /// <param name="student"></param>
        /// <param name="uploadFile"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadRezultOsvoenObrazovatProgrAsync(Student student, IFormFile uploadedFile)
        {
            if (student == null || uploadedFile == null) return null;

            FileModel newFileModel = await UploadFileAsync(uploadedFile, "Результаты освоения образовательной программы", FileDataTypeEnum.RezultOsvoenObrazovatProgr);
            await _context.SaveChangesAsync();

            if (newFileModel != null)
            {
                int? existingFileModelId = student.RezultOsvoenObrazovatProgrId;
                student.RezultOsvoenObrazovatProgrId = newFileModel.Id;
                student.RezultOsvoenObrazovatProgr = newFileModel;
                await _context.SaveChangesAsync();
                // Проверка наличия ранее загруженного файла
                if (existingFileModelId > 0)
                {
                    //await RemoveFileAsync(existingFileModelId);
                    //await _context.SaveChangesAsync();
                }
            }
            else
            {
                return null;
            }

            return student.RezultOsvoenObrazovatProgr;
        }

        /// <summary>
        /// Загружает файл согласия на обработку персональных данных
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadApplicationForProcessingPersonalDataAsync(IFormFile uploadedFile)
        {
            var fileModel = await UploadFileAsync(uploadedFile,
                "Согласие на обработку персональных данных",
                FileDataTypeEnum.SoglasieNaObrabotkuPersonalnihDannih);
            return fileModel;
        }

        /// <summary>
        /// Возвращает типы файлов для заданной группы
        /// </summary>
        /// <param name="fileDataTypeGroup"></param>
        /// <returns></returns>
        public IQueryable<FileDataType> GetFileDataTypes(FileDataTypeGroupEnum fileDataTypeGroup)
        {
            var fileDataTypes = _context.FileDataTypes.Where(f => f.FileDataTypeGroupId == (int)fileDataTypeGroup);

            return fileDataTypes;
        }
    }
}
