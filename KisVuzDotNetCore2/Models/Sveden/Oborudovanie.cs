using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Оборудование
    /// </summary>
    public class Oborudovanie
    {
        public int OborudovanieId { get; set; }

        public Pomeshenie Pomeshenie { get; set; }
        [Display(Name = "Наименование помешения")]
        public int PomeshenieId { get; set; }

        [Display(Name = "Наименование оборудования")]
        public string OborudovanieName { get; set; }

        [Display(Name = "Количество оборудования")]
        public int OborudovanieCount { get; set; }
    }
}
