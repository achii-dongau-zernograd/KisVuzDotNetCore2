using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Files
{
    /// <summary>
    /// Документ пользователя
    /// </summary>
    public class UserDocument
    {
        /// <summary>
        /// УИД документа пользователя
        /// </summary>
        public int UserDocumentId { get; set; }

        /// <summary>
        /// УИД пользователя
        /// </summary>
        [Display(Name = "Пользователь")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Файл пользователя
        /// </summary>
        [Display(Name = "Файл")]
        public int FileModelId { get; set; }
        public FileModel FileModel { get; set; }

        /// <summary>
        /// Тип документа пользователя
        /// </summary>
        [Display(Name = "Тип документа")]
        public int FileDataTypeId { get; set; }
        public FileDataType FileDataType { get; set; }
    }
}
