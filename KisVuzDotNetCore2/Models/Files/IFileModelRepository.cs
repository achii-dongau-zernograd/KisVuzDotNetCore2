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
    }
}