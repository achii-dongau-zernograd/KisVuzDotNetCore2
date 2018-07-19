using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Дисциплины по Учебному плану
    /// </summary>
    public class Discipline
    {
        /// <summary>
        /// УИД дисциплины в составе учебного плана
        /// </summary>
        public int DisciplineId { get; set; }

        /// <summary>
        /// Код дисциплины в составе учебного плана
        /// </summary>
        [Display(Name = "Код дисциплины")]
        public string DisciplineCode { get; set; }

        /// <summary>
        /// Наименование дисциплины в составе учебного плана
        /// </summary>
        [Display(Name = "Наименование дисциплины")]
        public DisciplineName DisciplineName { get; set; }
        [Display(Name = "Наименование дисциплины")]
        public int DisciplineNameId { get; set; }

        /// <summary>
        /// Часть блока дисциплин по учебному плану
        /// </summary>
        [Display(Name = "Часть блока дисциплин")]
        public BlokDisciplChast BlokDisciplChast { get; set; }
        [Display(Name = "Часть блока дисциплин")]
        public int BlokDisciplChastId { get; set; }

        /// <summary>
        /// Курсы, на которых ведётся дисциплина
        /// </summary>
        [Display(Name = "Курс")]
        public List<Kurs> Kurses { get; set; }

        /// <summary>
        /// Аннотации
        /// </summary>
        public List<EduAnnotation> EduAnnotations { get; set; }

        /// <summary>
        /// Рабочие программы
        /// </summary>
        public List<RabProgram> RabPrograms { get; set; }
    }
}