using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Платёж
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// УИД платежа
        /// </summary>
        [Display(Name = "УИД платежа")]
        public int PaymentId { get; set; }

        /// <summary>
        /// Описание платежа
        /// </summary>
        [Display(Name = "Описание платежа")]
        public string Description { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        [Display(Name = "Сумма платежа")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        [Display(Name = "Дата платежа")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Скан-копия подтверждающего документа (чека, платёжного поручения поручение и т.п.)
        /// </summary>
        [Display(Name = "Скан-копия подтверждающего документа (чека, платёжного поручения и т.п.)")]
        public int? FileModelId { get; set; }
        public FileModel FileModel { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        [Display(Name = "Договор")]
        public int ContractId { get; set; }
        public Contract Contract { get; set; }


        /// <summary>
        /// Статус записи
        /// </summary>
        [Display(Name = "Статус записи")]
        public int RowStatusId { get; set; } = (int)RowStatusEnum.NotConfirmed;
        public RowStatus RowStatus { get; set; }

        /// <summary>
        /// Замечание / комментарий
        /// </summary>
        [Display(Name = "Замечание / комментарий")]
        public string Remark { get; set; }
    }
}
