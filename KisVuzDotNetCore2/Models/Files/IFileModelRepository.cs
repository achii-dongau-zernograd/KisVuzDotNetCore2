﻿using System.Threading.Tasks;
using KisVuzDotNetCore2.Models.Nir;
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
        /// Удаляет объект файл и файл на диске
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        Task RemoveFileModelAsync(FileModel fileModel);

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

        /// <summary>
        /// Загружает учебное пособие
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadUchPosobieAsync(IFormFile formFile, FileDataTypeEnum fileDataTypeEnum);

        /// <summary>
        /// Загружает договор с электронной библиотечной системой
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        Task<FileModel> UploadElectronBiblSystemDogovorAsync(IFormFile uploadedFile);

        /// <summary>
        /// Загружает файл научной статьи
        /// </summary>
        /// <param name="article">Научная статья</param>
        /// <param name="uploadedFile">Загружаемый файл</param>
        /// <returns></returns>
        Task<FileModel> UploadArticleAsync(Article article, IFormFile uploadedFile);
    }
}