using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель связи М:М между таблицами Semestr и FormKontrolName
    /// </summary>
    public class SemestrFormKontrolName
    {
        /// <summary>
        /// УИД
        /// </summary>
        public int SemestrFormKontrolNameId { get; set; }

        /// <summary>
        /// Формы контроля по учебному плану
        /// </summary>
        public int FormKontrolNameId { get; set; }
        /// <summary>
        /// Формы контроля по учебному плану
        /// </summary>
        [Display(Name = "Формы контроля")]
        public FormKontrolName FormKontrolName { get; set; }

        /// <summary>
        /// Семестр по учебному плану
        /// </summary>
        public int SemestrId { get; set; }
        [Display(Name = "Семестр")]
        public Semestr Semestr { get; set; }

    }
}
