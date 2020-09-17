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
        /// Файл фонда оценочных средств
        /// </summary>
        [Display(Name = "Файл фонда оценочных средств")]
        public FileModel FileModel { get; set; }
        /// <summary>
        /// Файл фонда оценочных средств
        /// </summary>
        [Display(Name = "Файл фонда оценочных средств")]
        public int? FileModelId { get; set; }

        /// <summary>
        /// Файл листа переутверждения фонда оценочных средств
        /// </summary>
        [Display(Name = "Файл листа переутверждения фонда оценочных средств")]
        public FileModel FileModelListPereutverjdeniya { get; set; }
        /// <summary>
        /// Файл листа переутверждения фонда оценочных средств
        /// </summary>
        [Display(Name = "Файл листа переутверждения фонда оценочных средств")]
        public int? FileModelListPereutverjdeniyaId { get; set; }
    }
}
