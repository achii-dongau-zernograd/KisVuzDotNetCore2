using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Год выпуска
    /// </summary>
    public class GraduateYear
    {
        public int GraduateYearId { get; set; }

        [Display(Name="Наименование года выпуска")]
        public string GraduateYearName { get; set; }
    }
}
