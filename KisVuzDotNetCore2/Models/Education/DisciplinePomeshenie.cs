using KisVuzDotNetCore2.Models.Sveden;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель для связи отношением М:М
    /// моделей дисциплин и моделей помещений
    /// </summary>
    public class DisciplinePomeshenie
    {
        public int DisciplinePomeshenieId { get; set; }

        /// <summary>
        /// Дисциплина по учебному плану
        /// </summary>
        [Display(Name = "Дисциплина по учебному плану")]
        public Discipline Discipline { get; set; }
        /// <summary>
        /// Дисциплина по учебному плану
        /// </summary>
        [Display(Name = "Дисциплина по учебному плану")]
        public int DisciplineId { get; set; }

        /// <summary>
        /// Учебный план - учебный год
        /// </summary>
        [Display(Name = "Учебный план - учебный год")]
        public EduPlanEduYear EduPlanEduYear { get; set; }
        /// <summary>
        /// Учебный план - учебный год
        /// </summary>
        [Display(Name = "Учебный план - учебный год")]
        public int EduPlanEduYearId { get; set; }

        /// <summary>
        /// Помещение
        /// </summary>
        [Display(Name ="Помещение")]
        public Pomeshenie Pomeshenie { get; set; }
        /// <summary>
        /// Помещение
        /// </summary>
        [Display(Name = "Помещение")]
        public int PomeshenieId { get; set; }
    }
}
