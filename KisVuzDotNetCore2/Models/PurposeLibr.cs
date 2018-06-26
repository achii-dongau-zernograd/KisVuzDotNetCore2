﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KisVuzDotNetCore2.Models
{
    public class PurposeLibr
    {
        public int PurposeLibrId { get; set; }
        [Display(Name = "Вид помещения")]
        public string VidPom { get; set; }
        [Display(Name = "Адрес места нахождения")]
        public string Adress { get; set; }
        [Display(Name = "Площадь, м")]
        public string Square { get; set; }
        [Display(Name = "Количество мест")]
        public string NumberPlaces { get; set; }
        [Display(Name = "Приспособленность для использования инвалидами и лицами с ограниченными возможностями здоровья")]
        public string PrisposoblOvz { get; set; }
        public string itemprop { get; set; }

    }
}
