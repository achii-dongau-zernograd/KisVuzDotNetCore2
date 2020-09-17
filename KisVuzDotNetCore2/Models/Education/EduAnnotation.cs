using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Аннотация к рабочей программе дисциплины"
    /// </summary>
    public class EduAnnotation
    {
        /// <summary>
        /// УИД аннотации
        /// </summary>
        public int EduAnnotationId { get; set; }

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
        /// Файл аннотации
        /// </summary>
        [Display(Name = "Файл аннотации")]
        public FileModel FileModel { get; set; }
        /// <summary>
        /// Файл аннотации
        /// </summary>
        [Display(Name = "Файл аннотации")]
        public int? FileModelId { get; set; }
    }
}
