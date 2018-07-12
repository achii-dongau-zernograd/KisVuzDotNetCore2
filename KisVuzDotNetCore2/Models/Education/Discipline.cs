using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Дисциплины по Учебному плану
    /// </summary>
    public class Discipline
    {
        public int DisciplineId { get; set; }
        [Display(Name = "Код дисциплины")]
        public string DisciplineCode { get; set; }
        [Display(Name = "Курс")]
        public List<Kurs> Kurses { get; set; }

        [Display(Name = "Наименование дисциплины")]
        public DisciplineName DisciplineName { get; set; }
        [Display(Name = "Наименование дисциплины")]
        public int DisciplineNameId { get; set; }
    }
}