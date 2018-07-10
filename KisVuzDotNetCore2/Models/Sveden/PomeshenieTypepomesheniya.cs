using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Таблица для связи PomeshenieType с Pomeshenie
    /// </summary>
    public class PomeshenieTypepomesheniya
    {
        public int PomeshenieTypepomesheniyaId { get; set; }

        public int PomeshenieId { get; set; }
        [Display(Name = "Наименование помещения")]
        public Pomeshenie Pomeshenie { get; set; }
        
        public int PomeshenieTypeId { get; set; }
        [Display(Name = "Наименование типа помещения")]
        public PomeshenieType PomeshenieType { get; set; }
    }
}
