using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Abitur
{
    /// <summary>
    /// Тип льготы при приёме
    /// </summary>
    public class AdmissionPrivilegeType
    {
        public int AdmissionPrivilegeTypeId { get; set; }

        [Display(Name ="Наименование типа льготы")]
        public string AdmissionPrivilegeTypeName { get; set; }
    }
}
