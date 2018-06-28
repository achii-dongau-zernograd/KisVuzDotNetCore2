using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    public class UchredLaw
    {
        public int UchredLawId { get; set; }
        [Display(Name = "Наименование учредителя")]
        public string NameUchred { get; set; }
        [Display(Name = "Фамилия, имя, отчество руководителя учредителя(ей) образовательной организации ")]
        public string FullnameUchred { get; set; }
        [Display(Name = "Адрес местанахождения учредителя(ей)")]
        public string AddressUchred { get; set; }
        [Display(Name = "Контактные телефоны")]
        public string TelUchred { get; set; }
        [Display(Name = "Адрес электронной почты")]
        public string mailUchred { get; set; }
        [Display(Name = "Адрес сайта учредителя(ей) в сети Интернет")]
        public string WebsiteUchred { get; set; }
    }
}
