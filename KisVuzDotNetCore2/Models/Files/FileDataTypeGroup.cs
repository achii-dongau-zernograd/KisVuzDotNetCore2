using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Группа типов данных файлов
    /// </summary>
    public class FileDataTypeGroup
    {
        public int FileDataTypeGroupId { get; set; }

        /// <summary>
        /// Наименование группы типов содержимого файлов
        /// </summary>
        [Display(Name= "Наименование группы типов содержимого файлов")]
        public string FileDataTypeGroupName { get; set; }

        /// <summary>
        /// Типы содержимого файлов группы
        /// </summary>
        [Display(Name = "Типы содержимого файлов группы")]
        public List<FileDataType> FileDataTypes { get; set; }
    }
}