using KisVuzDotNetCore2.Models.Common;
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
        [Display(Name = "Дата/время загрузки")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        [Display(Name = "Регистрационный номер")]
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
        /// Возвращает полное наименование заявления о приёме
        /// </summary>
        public string ApplicationForAdmissionFullName
        {
            get
            {
                return $"{EduProfile?.GetEduProfileFullName} - {EduForm?.EduFormName} - {QuotaType?.QuotaTypeName}";
            }
        }

        /// <summary>
        /// Возвращает краткое наименование заявления о приёме
        /// </summary>
        public string ApplicationForAdmissionShortName
        {
            get
            {
                return $"{EduProfile?.EduNapravl.EduNapravlCode} - {EduProfile?.EduProfileName} - {EduForm?.EduFormName} - {QuotaType?.QuotaTypeName}";
            }
        }

        public string ApplicationForAdmissionFullNameWithAppUserInfo
        {
            get
            {
                string birthdate = Abiturient?.AppUser?.Birthdate == null ? "" : ", " + ((DateTime)Abiturient?.AppUser?.Birthdate).ToString("d");
                return $"{Abiturient?.AppUser?.GetFullName}{birthdate}. " + ApplicationForAdmissionFullName;
            }
        }

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

        /// <summary>
        /// Заявления о согласии на зачисление
        /// </summary>
        [Display(Name = "Заявления о согласии на зачисление")]
        public List<ConsentToEnrollment> ConsentToEnrollments { get; set; }

        /// <summary>
        /// Договоры
        /// </summary>
        [Display(Name = "Договоры")]
        public List<Contract> Contracts { get; set; }
    }
}
