using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Индивидуальные достижения абитуриента
    /// </summary>
    public class AbiturientIndividualAchievment
    {
        public int AbiturientIndividualAchievmentId { get; set; }

        /// <summary>
        /// Абитуриент
        /// </summary>
        [Display(Name = "Абитуриент")]
        public int AbiturientId { get; set; }
        public Abiturient Abiturient { get; set; }

        /// <summary>
        /// Индивидуальное достижение
        /// </summary>
        [Display(Name = "Индивидуальное достижение")]
        public int AbiturientIndividualAchievmentTypeId { get; set; }
        public AbiturientIndividualAchievmentType AbiturientIndividualAchievmentType { get; set; }

        /// <summary>
        /// Скан-копия подтверждающего документа
        /// </summary>
        [Display(Name = "Скан-копия подтверждающего документа")]
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
