using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Users
{
    /// <summary>
    /// Наименование типа родственной связи (отец, мать и пр.)
    /// </summary>
    public class FamilyMemberType
    {
        public int FamilyMemberTypeId { get; set; }

        [Display(Name = "Наименование типа родственной связи (отец, мать и пр.)")]
        public string FamilyMemberTypeName { get; set; }
    }
}
