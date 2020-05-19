using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Пол
    /// </summary>
    public class Gender
    {
        public int GenderId { get; set; }

        [Display(Name = "Наименование пола")]
        public string GenderName { get; set; }
    }
}
