using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Курс по учебному плану
    /// </summary>
    public class Kurs
    {
        /// <summary>
        /// УИД курса по учебному плану
        /// </summary>
        public int KursId { get; set; }

        /// <summary>
        /// Номер курса
        /// </summary>
        [Display(Name = "Курс")]
        public EduKurs EduKurs { get; set; }
        [Display(Name = "Курс")]
        public int EduKursId { get; set; }

        /// <summary>
        /// Дисциплина в составе учебного плана
        /// </summary>
        [Display(Name = "Дисциплина")]
        public Discipline Discipline { get; set; }
        [Display(Name = "Дисциплина")]
        public int DisciplineId { get; set; }

        /// <summary>
        /// Семестры, на которых ведётся дисциплина
        /// </summary>
        [Display(Name = "Семестры")]
        public List<Semestr> Semestres { get; set; }
    }
}