using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Номер курса"
    /// </summary>
    public class EduKurs
    {
        public int EduKursId { get; set; }
        [Display(Name = "Курс (число)")]
        public int EduKursNumber { get; set; }
        [Display(Name = "Курс (строка)")]
        public string EduKursName { get; set; }
        public List<Semestr> Semestres { get; set; }
    }
}