using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Квалификация
    /// </summary>
    public class Qualification
    {
        public int QualificationId { get; set; }

        /// <summary>
        /// Наименование направления подготовки
        /// </summary>
        [Display(Name = "Наименование направления подготовки")]
        public string NapravlName { get; set; }

        /// <summary> 
        /// Наименование квалификации (инженер и пр.)
        /// </summary>
        [Display(Name ="Квалификация")]
        public string QualificationName { get; set; }
    }
}