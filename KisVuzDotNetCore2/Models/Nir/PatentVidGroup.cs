using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Группа вида объекта интеллектуальной собственности
    /// </summary>
    public class PatentVidGroup
    {
        public int PatentVidGroupId { get; set; }

        /// <summary>
        /// Наименование группы вида объекта интеллектуальной собственности
        /// </summary>
        [Display(Name = "Группа вида объекта интеллектуальной собственности")]
        public string PatentVidGroupName { get; set; }

        /// <summary>
        /// Вид патента (свидетельства)
        /// </summary>
        [Display(Name = "Вид патента (свидетельства)")]
        public List<PatentVid> PatentVids { get; set; }
    }
}