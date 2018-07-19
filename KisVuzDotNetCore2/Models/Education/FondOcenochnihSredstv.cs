using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Фонд оценочных средств (ФОС)"
    /// </summary>
    public class FondOcenochnihSredstv
    {
        /// <summary>
        /// УИД ФОС
        /// </summary>
        public int FondOcenochnihSredstvId { get; set; }

        /// <summary>
        /// Дисциплина
        /// </summary>
        [Display(Name = "Дисциплина")]
        public Discipline Discipline { get; set; }
        /// <summary>
        /// Дисциплина
        /// </summary>
        [Display(Name = "Дисциплина")]
        public int DisciplineId { get; set; }

        /// <summary>
        /// Файл фонда оценчных средств
        /// </summary>
        [Display(Name = "Файл фонда оценчных средств")]
        public FileModel FileModel { get; set; }
        /// <summary>
        /// Файл фонда оценчных средств
        /// </summary>
        [Display(Name = "Файл фонда оценчных средств")]
        public int FileModelId { get; set; }
    }
}
