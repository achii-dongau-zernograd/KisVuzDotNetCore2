using System;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Информация о профессионально-общественной и/или общественной аккредитации образовательной программы
    /// </summary>
    public class EduPOAccred
    {
        public int EduPOAccredId { get; set; }

        [Display(Name = "Наименование специальности, направления подготовки")]
        public EduNapravl EduNapravl { get; set; }
        [Display(Name = "Наименование специальности, направления подготовки")]
        public int EduNapravlId { get; set; }

        [Display(Name = "Наименование аккредитующей организации")]
        public string AccredOrgName { get; set; }

        [Display(Name = "Срок действия профессионально-общественной аккредитации (дата окончания действия свидетельства о профессионально-общественнойи/или общественной аккредитации)")]
        [DataType(DataType.Date)]
        public DateTime AccredDateEnd { get; set; } = DateTime.Now;

        /// <summary>
        /// Файл свидетельства о профессионально-общественной и/или общественной аккредитации образовательной программы (.pdf)
        /// </summary>
        [Display(Name = "Файл свидетельства")]
        public FileModel EduPOAccredPdf { get; set; }
        public int? EduPOAccredPdfId { get; set; }
    }
}
