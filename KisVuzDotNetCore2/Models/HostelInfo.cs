using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models
{

    /// <summary>
    /// Модель "Наличие общежития, интерната, количество жилых помещений в общежитии, интернате для иногородних обучающихся"
    /// </summary>
    public class HostelInfo
    {
        public int HostelInfoId { get; set; }

        /// <summary>
        /// "Наименование показателя"
        /// </summary>
        [Display(Name = "Наименование показателя")]
        public string NaimenovanPokaz { get; set; }


        /// <summary>
        /// "Значение"
        /// </summary>
        [Display(Name = "Значение")]
        public string Znachenie { get; set; }
        
    }
}
