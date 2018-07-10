using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Календарный учебный график"
    /// </summary>
    public class EduShedule
    {        
        public int EduSheduleId { get; set; }

        /// <summary>
        /// Учебный год
        /// </summary>
        [Display(Name ="Учебный год")]
        public EduYear EduYear { get; set; }
        /// <summary>
        /// Учебный год
        /// </summary>
        [Display(Name = "Учебный год")]
        public int EduYearId { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name = "Форма обучения")]
        public EduForm EduForm { get; set; }
        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name = "Форма обучения")]
        public int EduFormId { get; set; }

        /// <summary>
        /// Профиль / направленность / специализация
        /// </summary>
        [Display(Name = "Профиль / направленность / специализация")]
        public EduProfile EduProfile { get; set; }
        /// <summary>
        /// Профиль / направленность / специализация
        /// </summary>
        [Display(Name = "Профиль / направленность / специализация")]
        public int EduProfileId { get; set; }

        /// <summary>
        /// Сканированная копия календарного учебного графика
        /// </summary>
        [Display(Name = "Сканированная копия календарного учебного графика")]
        public FileModel FileModel { get; set; }
        /// <summary>
        /// Сканированная копия календарного учебного графика
        /// </summary>
        [Display(Name = "Сканированная копия календарного учебного графика")]
        public int FileModelId { get; set; }
    }
}
