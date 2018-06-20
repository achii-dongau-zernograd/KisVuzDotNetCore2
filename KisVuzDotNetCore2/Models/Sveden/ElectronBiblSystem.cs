using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Электронная библиотечная система, к которой имеют доступ обучающиеся
    /// </summary>
    public class ElectronBiblSystem
    {
        public int ElectronBiblSystemId { get; set; }
        [Display(Name = "Название ЭБС")]
        public string NameEbs { get; set; }
        [Display(Name = "Ссылка на ЭБС")]
        public string LinkEbs { get; set; }
        [Display(Name = "Номер договора")]
        public string NumberDogovor { get; set; }
        [Display(Name = "Дата начала договора")]
        public DateTime DateStart { get; set; }
        [Display(Name = "Дата окончания договора")]
        public DateTime DateEnd { get; set; }
        [Display(Name = "Копия договора")]
        public FileModel CopyDogovor { get; set; }
        [Display(Name = "Копия договора")]
        public int? CopyDogovorId { get; set; }
    }
}
