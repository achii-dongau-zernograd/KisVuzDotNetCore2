using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Курс по учебному плану
    /// </summary>
    public class Kurs
    {
        public int KursId { get; set; }
        [Display(Name = "Курс")]
        public EduKurs EduKurs { get; set; }
    }
}