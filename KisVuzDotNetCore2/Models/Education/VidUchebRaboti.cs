using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Вид учебной работы
    /// </summary>
    public class VidUchebRaboti
    {
        public int VidUchebRabotiId { get; set; }
        [Display(Name = "Вид учебной работы")]
        public string VidUchebRabotiName { get; set; }
    }
}