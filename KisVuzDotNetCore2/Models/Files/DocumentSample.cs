using KisVuzDotNetCore2.Models.Education;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Files
{
    /// <summary>
    /// Бланк или образец заполнения документа
    /// </summary>
    public class DocumentSample
    {
        public int DocumentSampleId { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        [Display(Name = "Тип документа")]
        public int FileDataTypeId { get; set; }
        public FileDataType FileDataType { get; set; }

        /// <summary>
        /// Наименование документа
        /// </summary>
        [Display(Name = "Наименование документа")]
        public string DocumentSampleName { get; set; }

        /// <summary>
        /// Флаг определения бланка документа
        /// </summary>
        [Display(Name = "Бланк")]
        public bool IsBlank { get; set; }

        /// <summary>
        /// Ссылка на внешний ресурс
        /// </summary>
        [Display(Name = "Ссылка на внешний ресурс")]
        public string Link { get; set; }

        /// <summary>
        /// Файл на сервере
        /// </summary>
        [Display(Name = "Файл на сервере")]
        public int? FileModelId { get; set; }
        public FileModel FileModel { get; set; }


        /// <summary>
        /// Направленность подготовки
        /// </summary>
        [Display(Name ="Направленность подготовки")]
        public int? EduProfileId { get; set; }
        public EduProfile EduProfile { get; set; }

        /////////////////////////////////////////////////////
        public string GetHref
        {
            get
            {
                string href = Link;
                if(FileModelId != null)
                {
                    href = $"/{FileModel.Path}";
                }

                return href;
            }
        }

    }
}
