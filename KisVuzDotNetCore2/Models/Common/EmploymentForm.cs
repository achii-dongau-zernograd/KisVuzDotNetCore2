using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Common
{
    /// <summary>
    /// Форма занятости
    /// </summary>
    public class EmploymentForm
    {
        public int EmploymentFormId { get; set; }

        [Display(Name = "Форма занятости")]
        public string EmploymentFormName { get; set; }
    }
}
