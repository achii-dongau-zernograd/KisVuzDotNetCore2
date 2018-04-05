using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Тип содержимого файла
    /// </summary>
    public class FileDataType
    {
        public int FileDataTypeId { get; set; }

        /// <summary>
        /// Наименование типа содержимого файла
        /// </summary>
        [Display(Name = "Наименование типа содержимого файла")]
        public string FileDataTypeName { get; set; }

        /// <summary>
        /// itemprop
        /// </summary>
        public string Itemprop { get; set; }


        /// <summary>
        /// Назначение файлу набора типов данных
        /// </summary>
        public List<FileToFileType> FileToFileTypes { get; set; }

        [Display(Name = "Группа типов содержимого")]
        public int FileDataTypeGroupId { get; set; }
        [Display(Name = "Группа типов содержимого")]
        public FileDataTypeGroup FileDataTypeGroup { get; set; }
    }
}