using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Формы контроля
    /// </summary>
    public class FormKontrol
    {
        public int FormKontrolId { get; set; }
        [Display(Name = "Форма контроля")]
        public string FormKontrolName { get; set; }
    }
}