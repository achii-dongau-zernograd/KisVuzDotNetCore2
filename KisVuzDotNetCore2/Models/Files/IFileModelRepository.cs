using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KisVuzDotNetCore2.Models.Files
{
    public interface IFileModelRepository
    {
        /// <summary>
        /// Удаляет объект файл и файл на диске
        /// </summary>
        /// <param name="fileModelId"></param>
        Task RemoveFileAsync(int? fileModelId);

        /// <summary>
        /// Загружает файл аннотации на диск
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadEduAnnotationAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл рабочей программы на диск
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadRabProgramAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл фонда оценочных средств на диск
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadFondOcenochnihSredstvAsync(IFormFile uploadedFile);
    }
}