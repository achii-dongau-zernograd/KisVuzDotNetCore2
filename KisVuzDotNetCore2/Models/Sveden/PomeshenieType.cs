using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    public class PomeshenieType
    {
        public int PomeshenieTypeId { get; set; }

        [Display(Name = "Наименование типа помещения")]
        public string PomeshenieTypeName { get; set; } 
    }
}
