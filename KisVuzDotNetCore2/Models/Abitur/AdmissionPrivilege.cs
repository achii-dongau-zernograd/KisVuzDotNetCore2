using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Льгота при приёме
    /// </summary>
    public class AdmissionPrivilege
    {
        public int AdmissionPrivilegeId { get; set; }

        /// <summary>
        /// Заявление о приёме
        /// </summary>
        [Display(Name ="Заявление о приёме")]
        public int ApplicationForAdmissionId { get; set; }
        public ApplicationForAdmission ApplicationForAdmission { get; set; }

        /// <summary>
        /// Тип льготы
        /// </summary>
        [Display(Name = "Тип льготы")]
        public int AdmissionPrivilegeTypeId { get; set; }
        public AdmissionPrivilegeType AdmissionPrivilegeType { get; set; }

        /// <summary>
        /// Скан-копия подтверждающего документа в формате pdf
        /// </summary>
        [Display(Name = "Скан-копия подтверждающего документа в формате pdf")]
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
    }
}
