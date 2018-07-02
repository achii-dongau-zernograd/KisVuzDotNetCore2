using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Помещение
    /// </summary>
    public class Pomeshenie
    {
        public int PomeshenieId { get; set; }

        public Korpus Korpus { get; set; }
        [Display(Name = "Наименование корпуса")]
        public int KorpusId { get; set; }

        [Display(Name = "Наименование помещения")]
        public string PomeshenieName { get; set; }

        [Display(Name = "Типы помещений")]
        public List<PomeshenieTypepomesheniya> PomeshenieTypes { get; set; }

        [Display(Name = "Приспособленность помещений для использования инвалидами и лицами с ОВЗ")]
        public string PomeshenieOvz { get; set; }

        public List<Oborudovanie> OborudovanieList { get; set; }
    }
}
