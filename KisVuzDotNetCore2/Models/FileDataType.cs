using System.Collections.Generic;

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
        public string FileDataTypeName { get; set; }

        /// <summary>
        /// Назначение файлу набора типов данных
        /// </summary>
        public List<FileToFileType> FileToFileTypes { get; set; }
    }
}