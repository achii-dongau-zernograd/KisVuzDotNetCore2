using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Сведения о поступлении финансовых и материальных средств и об их расходовании
    /// </summary>
    public class VolumeFinYearPostRas
    {
        public int VolumeFinYearPostRasId { get; set; }
        [Display(Name = "Год")]
        public int FinYear { get; set; }
        [Display(Name = "Поступившие финансовые и материальные средства (тыс. руб.)")]
        public double FinPost { get; set; }
        [Display(Name = "Расходованные финансовые и материальные средства (тыс. руб.)")]
        public double FinRas { get; set; }        
    }
}
