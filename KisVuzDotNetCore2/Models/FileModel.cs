using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Модель файла
    /// </summary>
    public class FileModel
    {
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        [Display(Name = "Наименование файла")]
        public string Name { get; set; }

        /// <summary>
        /// Имя файла с расширением
        /// </summary>
        [Display(Name = "Имя файла")]
        public string FileName { get; set; }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        [Display(Name = "Путь к файлу")]
        public string Path { get; set; }

        /// <summary>
        /// Дата загрузки на сервер
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата загрузки на сервер")]
        public DateTime UploadDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Тип файла
        /// </summary>
        [Display(Name = "Тип файла")]
        public string ContentType { get; set; } = "application/octet-stream";

        /// <summary>
        /// Назначение файлу набора типов данных
        /// </summary>
        public List<FileToFileType> FileToFileTypes { get; set; }
    }
}
