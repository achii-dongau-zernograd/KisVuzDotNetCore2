using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Вид учебной работы
    /// </summary>
    public class VidUchebRabotiName
    {
        /// <summary>
        /// УИД учебной работы
        /// </summary>
        public int VidUchebRabotiNameId { get; set; }

        /// <summary>
        /// Вид учебной работы
        /// </summary>
        [Display(Name = "Вид учебной работы")]
        public string VidUchebRabotiNameName { get; set; }
    }
}