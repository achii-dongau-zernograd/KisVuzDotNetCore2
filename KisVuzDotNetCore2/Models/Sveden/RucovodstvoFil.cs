﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    public class RucovodstvoFil
    {
        public int RucovodstvoFilId { get; set; }
        [Display (Name="Наименование филиала")]
        public string NameFil { get; set; }
        [Display(Name = "Ф.И.О.")]
        public string Fio { get; set; }
        [Display(Name = "Должность")]
        public string Post { get; set; }
        [Display(Name = "Телефон")]
        public string Telephone { get; set; }
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }
    }
}
