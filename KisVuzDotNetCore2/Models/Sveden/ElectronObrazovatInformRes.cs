using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    public class ElectronObrazovatInformRes
    {
        public int ElectronObrazovatInformResId { get; set; }
        [Display(Name = "Название электронных образовательных и информационных ресурсов")]
        public string NameRes { get; set; }
        [Display(Name = "Ссылка на электронный ресурс")]
        public string LinkRes { get; set; }
        [Display(Name = "Электронный ресурс")]
        public FileModel Res { get; set; }
        [Display(Name = "Электронный ресурс")]
        public int? ResId { get; set; }
        [Display(Name = "Признак собственности (ресурс института или внешний)")]
        public bool IsSobstv { get; set; }
        [Display(Name = "Описание электронного ресурса")]
        public string DescriptionRes { get; set; }
    }
}
