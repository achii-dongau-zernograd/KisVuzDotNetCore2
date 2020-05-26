using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Заявление о согласии на зачисление
    /// </summary>
    public class ConsentToEnrollment
    {
        public int ConsentToEnrollmentId { get; set; }

        /// <summary>
        /// Заявление о приёме
        /// </summary>
        [Display(Name ="Заявление о приёме")]
        public int ApplicationForAdmissionId { get; set; }
        public ApplicationForAdmission ApplicationForAdmission { get; set; }

        /// <summary>
        /// Скан-копия заявления о согласии на зачисление в формате pdf
        /// </summary>
        [Display(Name = "Скан-копия заявления о согласии на зачисление в формате pdf")]
        public int? FileModelId { get; set; }
        public FileModel FileModel { get; set; }

        /// <summary>
        /// Статус записи
        /// </summary>
        [Display(Name = "Статус записи")]
        public int RowStatusId { get; set; }
        public RowStatus RowStatus { get; set; }

        /// <summary>
        /// Замечание / комментарий
        /// </summary>
        [Display(Name = "Замечание / комментарий")]
        public string Remark { get; set; }

        /// <summary>
        /// Дата / время изменения объекта
        /// </summary>
        [Display(Name = "Дата / время изменения")]
        public DateTime ChangingDateTime { get; set; } = DateTime.Now;
    }
}
