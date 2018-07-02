using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models
{
    /// <summary>
    /// Выпуск (результаты защиты ВКР)
    /// </summary>
    public class EduGraduate
    {
        public int EduGraduateId { get; set; }

        [Display(Name="Профиль подготовки")]
        public EduProfile EduProfile { get; set; }
        public int EduProfileId { get; set; }

        [Display(Name = "Год выпуска")]
        public GraduateYear GraduateYear { get; set; }
        public int GraduateYearId { get; set; }

        [Display(Name = "Количество выпускников")]
        public int GraduateNumber { get; set; }

        [Display(Name = "Средний балл защиты выпускников")]
        public string GraduateSredBall { get; set; }

    }
}
