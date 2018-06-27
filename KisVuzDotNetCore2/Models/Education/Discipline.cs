using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        [Display(Name = "Код дисциплины")]
        public string DisciplineCode { get; set; }
        [Display(Name = "Наименование дисциплины")]
        public string DisciplineName { get; set; }
        [Display(Name = "Курс")]
        public List<EduKurs> EduKurses { get; set; }
    }
}