using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Программы подготовки
    /// </summary>
    public class EduProgramPodg
    {
        public int EduProgramPodgId { get; set; }
        [Display(Name = "Программа подготовки")]
        public string EduProgramPodgName { get; set; }
    }
}