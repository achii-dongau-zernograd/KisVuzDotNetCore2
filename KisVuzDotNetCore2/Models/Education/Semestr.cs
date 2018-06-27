using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Семестр
    /// </summary>
    public class Semestr
    {
        public int SemestrId { get; set; }
        [Display(Name = "Номер семестра")]
        public int SemestrNumber { get; set; }
        [Display(Name = "Наименование семестра")]
        public string SemestrName { get; set; }
        [Display(Name = "Формы контроля")]
        public List<FormKontrol> FormKontrols { get; set; }
        [Display(Name = "Вид учебной работы")]
        public List<VidUchebRaboti> VidUchebRaboti { get; set; }
    }
}