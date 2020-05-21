using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Направление подготовки - тип квоты приёма
    /// </summary>
    public class EduNapravlQuotaType
    {
        public int EduNapravlQuotaTypeId { get; set; }

        /// <summary>
        /// Направление подготовки
        /// </summary>
        [Display(Name = "Направление подготовки")]
        public int EduNapravlId { get; set; }
        public EduNapravl EduNapravl { get; set; }

        /// <summary>
        /// Тип квоты приёма
        /// </summary>
        [Display(Name = "Тип квоты приёма")]
        public int QuotaTypeId { get; set; }
        public QuotaType QuotaType { get; set; }               

    }
}
