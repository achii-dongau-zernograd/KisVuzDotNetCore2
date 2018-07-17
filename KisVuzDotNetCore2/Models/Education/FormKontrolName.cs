using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Формы контроля
    /// </summary>
    public class FormKontrolName
    {
        public int FormKontrolNameId { get; set; }
        [Display(Name = "Форма контроля")]
        public string FormKontrolNameName { get; set; }
    }
}