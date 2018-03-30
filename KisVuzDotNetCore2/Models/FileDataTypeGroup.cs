using System.Collections.Generic;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Группа типов данных файлов
    /// </summary>
    public class FileDataTypeGroup
    {
        public int FileDataTypeGroupId { get; set; }

        /// <summary>
        /// Наименование группы типов данных файлов
        /// </summary>
        public string FileDataTypeGroupName { get; set; }

        /// <summary>
        /// Типы данных файлов группы
        /// </summary>
        public List<FileDataType> FileDataTypes { get; set; }
    }
}