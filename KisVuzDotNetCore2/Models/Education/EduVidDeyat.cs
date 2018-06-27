using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Виды деятельности по Учебному плану
    /// </summary>
    public class EduVidDeyat
    {
        public int EduVidDeyatId { get; set; }
        [Display(Name = "Вид деятельности по учебному плану")]
        public string EduVidDeyatName { get; set; }
    }
}