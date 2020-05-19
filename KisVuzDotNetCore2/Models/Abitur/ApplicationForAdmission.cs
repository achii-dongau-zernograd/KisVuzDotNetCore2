using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Заявление о приёме
    /// </summary>
    public class ApplicationForAdmission
    {
        public int ApplicationForAdmissionId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegNumber { get; set; } //////////////////////////////////////////////////// int или string?

        /// <summary>
        /// Абитуриент
        /// </summary>
        [Display(Name = "Абитуриент")]
        public int AbiturientId { get; set; }
        public Abiturient Abiturient { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name = "Форма обучения")]
        public int EduFormId { get; set; }
        public EduForm EduForm { get; set; }

        /// <summary>
        /// Профиль подготовки
        /// </summary>
        [Display(Name ="Профиль подготовки")]
        public int EduProfileId { get; set; }
        public EduProfile EduProfile { get; set; }

        /// <summary>
        /// Тип квоты на выделение мест для обучения
        /// (в пределах квоты приема лиц, имеющих особое право,
        /// в пределах квоты целевого приема, основные в рамках контрольных цифр,
        /// по договорам об оказании платных образовательных услуг)
        /// </summary>
        [Display(Name = "Тип квоты на выделение мест для обучения")]
        public int QuotaTypeId { get; set; }
        public QuotaType QuotaType { get; set; }

        /// <summary>
        /// Приоритет
        /// </summary>
        [Display(Name = "Приоритет")]
        public int PriorityId { get; set; }

        /// <summary>
        /// Скан-копия заявления о приёме
        /// </summary>
        [Display(Name = "Скан-копия заявления о приёме")]
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
        /// Льготы
        /// </summary>
        [Display(Name = "Льготы")]
        public List<AdmissionPrivilege> AdmissionPrivileges { get; set; }
    }
}
