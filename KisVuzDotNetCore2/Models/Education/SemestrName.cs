using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Семестр (справочник)
    /// </summary>
    public class SemestrName
    {
        /// <summary>
        /// УИД семестра
        /// </summary>
        public int SemestrNameId { get; set; }

        /// <summary>
        /// Номер семестра
        /// </summary>
        [Display(Name = "Номер семестра")]
        public int SemestrNameNumber { get; set; }

        /// <summary>
        /// Наименование семестра
        /// </summary>
        [Display(Name = "Наименование семестра")]
        public string SemestrNameName { get; set; }


    }
}
