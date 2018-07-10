using KisVuzDotNetCore2.Models.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Корпус
    /// </summary>
    public class Korpus
    {
        public int KorpusId { get; set; }

        [Display(Name = "Наименование корпуса")]
        public string KorpusName { get; set; }

        [Display(Name = "Адрес корпуса")]
        public Address KorpusAddress { get; set; }
        [Display(Name = "Адрес корпуса")]
        public int AddressId { get; set; }

        public List<Pomeshenie> Pomesheniya { get; set; }
    }
}
