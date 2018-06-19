using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    public class Volume
    {
        public int VolumeId { get; set; }
        [Display(Name = "за счет бюджетных ассигнований федерального бюджета (тыс. руб.)")]
        public double FinBFVolume { get; set; }
        [Display(Name = "за счет  бюджетов субъектов Российской Федерации (тыс. руб.)")]
        public double FinBRVolume { get; set; }
        [Display(Name = "за счет местных бюджетов (тыс. руб.)")]
        public double FinBMVolume { get; set; }
        [Display(Name = "по договорам об образовании за счет средств физических и (или) юридических лиц (тыс. руб.)")]
        public double FinPVolume { get; set; }
    }
}
