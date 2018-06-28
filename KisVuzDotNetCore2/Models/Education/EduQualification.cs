using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    public class EduQualification
    {
        public int EduQualificationId { get; set; }
        [Display(Name="Квалификация")]
        public string EduQualificationName { get; set; }
    }
}