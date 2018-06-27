using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Срок обучения
    /// </summary>
    public class EduSrok
    {
        public int EduSrokId { get; set; }
        [Display(Name = "Срок обучения")]
        public string EduSrokName { get; set; }
    }
}