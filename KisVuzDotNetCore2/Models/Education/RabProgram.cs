using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Рабочая программа дисциплины"
    /// </summary>
    public class RabProgram
    {
        /// <summary>
        /// УИД рабочей программы
        /// </summary>
        public int RabProgramId { get; set; }

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
        /// Файл рабочей программы
        /// </summary>
        [Display(Name = "Файл рабочей программы")]
        public FileModel FileModel { get; set; }
        /// <summary>
        /// Файл рабочей программы
        /// </summary>
        [Display(Name = "Файл рабочей программы")]
        public int FileModelId { get; set; }
    }
}
