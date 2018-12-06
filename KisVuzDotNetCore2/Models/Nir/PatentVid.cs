using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Вид патента (свидетельства)
    /// </summary>
    public class PatentVid
    {
        public int PatentVidId { get; set; }

        /// <summary>
        /// Вид патента (свидетельства)
        /// </summary>
        [Display(Name = "Вид патента")]
        public string PatentVidName { get; set; }

        /// <summary>
        /// Патенты (свидетельства)
        /// </summary>
        [Display(Name = "Патенты (свидетельства)")]
        public List<Patent> Patents { get; set; }

        /// <summary>
        /// Вид объекта интеллектуальной собственности
        /// </summary>
        [Display(Name = "Вид объекта интеллектуальной собственности")]
        public PatentVidGroup PatentVidGroup { get; set; }
        /// <summary>
        /// Вид объекта интеллектуальной собственности
        /// </summary>
        [Display(Name = "Вид объекта интеллектуальной собственности")]
        public int PatentVidGroupId { get; set; }


    }
}