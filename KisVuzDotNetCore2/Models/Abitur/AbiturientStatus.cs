using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Статус абитуриента
    /// </summary>
    public class AbiturientStatus
    {
        public int AbiturientStatusId { get; set; }

        /// <summary>
        /// Наименование статуса абитуриента
        /// </summary>
        [Display(Name ="Статус абитуриента")]
        public string AbiturientStatusName { get; set; }
    }
}
