using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Отношение к военной службе
    /// </summary>
    public class MilitaryServiceStatus
    {
        public int MilitaryServiceStatusId { get; set; }

        /// <summary>
        /// Наименование типа отношения к военной службе
        /// </summary>
        [Display(Name = "Наименование типа отношения к военной службе")]
        public string MilitaryServiceStatusName { get; set; }
    }
}
