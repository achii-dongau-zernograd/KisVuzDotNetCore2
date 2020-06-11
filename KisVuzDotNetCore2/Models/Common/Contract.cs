using KisVuzDotNetCore2.Models.Abitur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Договор
    /// </summary>
    public class Contract
    {
        public int ContractId { get; set; }

        /// <summary>
        /// Тип договора
        /// </summary>
        [Display(Name = "Тип договора")]
        public int ContractTypeId { get; set; }
        public ContractType ContractType { get; set; }

        /// <summary>
        /// Аккаунт пользователя, с которым заключён договор
        /// </summary>
        [Display(Name = "Аккаунт пользователя, с которым заключён договор")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Скан-копия договора
        /// </summary>
        [Display(Name = "Скан-копия договора")]
        public int? FileModelId { get; set; }
        public FileModel FileModel { get; set; }

        /// <summary>
        /// Дата начала действия договора
        /// </summary>
        [Display(Name = "Дата начала действия договора")]
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// Дата окончания действия договора
        /// </summary>
        [Display(Name = "Дата окончания действия договора")]
        public DateTime? DateEnd { get; set; }

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
        /// Заявление о приёме
        /// </summary>
        [Display(Name = "Заявление о приёме")]
        public int? ApplicationForAdmissionId { get; set; }
        public ApplicationForAdmission ApplicationForAdmission { get; set; }

        /// <summary>
        /// Оплаты по договору
        /// </summary>
        [Display(Name = "Оплаты по договору")]
        public List<Payment> Payments { get; set; }
    }
}
