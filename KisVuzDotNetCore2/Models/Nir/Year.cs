using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Справочник годов
    /// </summary>
    public class Year
    {
        public int YearId { get; set; }
        /// <summary>
        /// Наименование года
        /// </summary>
        [Display(Name ="Год")]
        public string YearName { get; set; }
    }
}