using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Struct;
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

        /// <summary>
        /// Корпус
        /// </summary>
        public Korpus Korpus { get; set; }
        [Display(Name = "Наименование корпуса")]
        public int KorpusId { get; set; }

        [Display(Name = "Наименование помещения")]
        public string PomeshenieName { get; set; }

        [Display(Name = "Типы помещений")]
        public List<PomeshenieTypepomesheniya> PomeshenieTypes { get; set; }

        [Display(Name = "Приспособленность помещений для использования инвалидами и лицами с ОВЗ")]
        public string PomeshenieOvz { get; set; }

        /// <summary>
        /// Оборудование, размещенное в помещении
        /// </summary>
        public List<Oborudovanie> OborudovanieList { get; set; }

        /// <summary>
        /// Дисциплины, преподаваемые в помещении
        /// </summary>
        public List<DisciplinePomeshenie> DisciplinePomeshenies { get; set; }

        [Display(Name = "Наименование помещения")]
        public string PomeshenieFullName {
            get
            {
                return Korpus?.KorpusName + " - " + PomeshenieName;
            }
        }

        /// <summary>
        /// Структурное подразделение
        /// </summary>
        [Display(Name = "Структурное подразделение")]
        public StructSubvision StructSubvision { get; set; }
        public int? StructSubvisionId { get; set; }
    }
}
