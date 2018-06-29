using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Наименование дисциплин
    /// </summary>
    public class DisciplineName
    {
        public int DisciplineNameId { get; set; }
        [Display(Name = "Наименование дисциплины")]
        public string DisciplineNameName { get; set; }
    }
}
