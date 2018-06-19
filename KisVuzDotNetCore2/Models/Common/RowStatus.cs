using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    // Статус записи
    public class RowStatus
    {
        [Display(Name = "Идентификатор статуса записи")]
        public int RowStatusId { get; set; }
        [Display(Name = "Статус записи")]
        public string RowStatusName { get; set; }
    }
}