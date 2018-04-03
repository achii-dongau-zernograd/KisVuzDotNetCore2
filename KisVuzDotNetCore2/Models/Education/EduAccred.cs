using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Государственная аккредитация"
    /// </summary>
    public class EduAccred
    {
        public int EduAccredId { get; set; }

        /// <summary>
        /// Номер свидетельства
        /// </summary>
        [Display(Name = "Номер свидетельства")]
        public string EduAccredNumber { get; set; }

        /// <summary>
        /// Дата свидетельства
        /// </summary>
        [Display(Name = "Дата выдачи свидетельства")]
        [DataType(DataType.Date)]
        public DateTime EduAccredDate { get; set; }

        /// <summary>
        /// Срок действия свидетельства
        /// </summary>
        [Display(Name = "Срок действия свидетельства")]
        [DataType(DataType.Date)]
        public DateTime EduAccredDateExpiration { get; set; }

        [Display(Name = "Файл свидетельства")]
        public FileModel EduAccredFile { get; set; }
        [Display(Name = "Файл свидетельства")]
        public int EduAccredFileId { get; set; }

        public List<EduUgs> EduAccredUgses { get; set; }
    }
}
