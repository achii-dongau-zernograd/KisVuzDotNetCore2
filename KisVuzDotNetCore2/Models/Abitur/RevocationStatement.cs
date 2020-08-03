using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Заявление об отзыве документов
    /// </summary>
    public class RevocationStatement
    {
        public int RevocationStatementId { get; set; }

        /// <summary>
        /// Заявление о приёме
        /// </summary>
        [Display(Name = "Заявление о приёме")]
        public ApplicationForAdmission ApplicationForAdmission { get; set; }
        public int ApplicationForAdmissionId { get; set; }

        /// <summary>
        /// Причина отказа
        /// </summary>
        [Display(Name = "Причина отказа")]
        public string RejectionReason { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Скан-копия заявления об отзыве документов
        /// </summary>
        [Display(Name = "Скан-копия заявления об отзыве документов")]
        public FileModel FileModel { get; set; }
        public int FileModelId { get; set; }

        /// <summary>
        /// Путь к сгенерированному pdf-файлу заявления об отзыве документов относительно wwwroot
        /// </summary>
        [Display(Name = "Путь к сгенерированному pdf-файлу заявления об отзыве документов относительно wwwroot")]
        public string GeneratedPdfDocumentPath { get; set; }

        /// <summary>
        /// Замечания
        /// </summary>
        [Display(Name = "Замечания")]
        public string Remark { get; set; }

        /// <summary>
        /// Статус записи
        /// </summary>
        [Display(Name = "Статус записи")]
        public RowStatus RowStatus { get; set; }
        public int RowStatusId { get; set; }
    }
}
