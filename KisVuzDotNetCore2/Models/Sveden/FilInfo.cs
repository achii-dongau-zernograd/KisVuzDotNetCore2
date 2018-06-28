using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    public class FilInfo
    {
        public int FilInfoId { get; set; }
        [Display(Name = "Наименование филиала")]
        public string NameFil { get; set; }
        [Display(Name = "Адрес местанахождения образовательной организации")]
        public string AddressFil { get; set; }
        [Display(Name = "Адрес офицального сайта в сети Интернет (при наличии)")]
        public string WebsiteFil { get; set; }
    }
}
