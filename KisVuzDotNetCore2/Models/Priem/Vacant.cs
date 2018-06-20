using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    /// <summary>
    /// Модель "Количество вакантных мест"
    /// </summary>
    public class Vacant
    {
        public int VacantId { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование ")]
        public EduNapravl EduNapravl { get; set; }
        [Display(Name = "Наименование ")]
        public int EduNapravlId { get; set; }
        /// <summary>
        /// Курс
        /// </summary>
        [Display(Name = "Курс")]
        public EduKurs EduKurs { get; set; }
        [Display(Name = "Курс")]
        public int EduKursId { get; set; }
        /// <summary>
        /// Форма обучения(очная/заочная/очно-заочная
        /// </summary>
        [Display(Name = "Форма обучения(очная/заочная/очно-заочная)")]
        public EduForm EduForm { get; set; }
        [Display(Name = "Форма обучения(очная/заочная/очно-заочная)")]
        public int EduFormId { get; set; }
        /// <summary>
        /// бюджетных ассигнований федерального бюджета
        /// </summary>
        [Display(Name = "бюджетных ассигнований федерального бюджета ")]
        public int NumberBFVacant { get; set; }
        /// <summary>
        /// бюджетов субьектов Российской Федерации 
        /// </summary>
        [Display(Name = "бюджетов субьектов Российской Федерации ")]
        public int NumberBRVacant { get; set; }
        /// <summary>
        /// местных бюджетов 
        /// </summary>
        [Display(Name = "местных бюджетов ")]
        public int NumberBMVacant { get; set; }
        /// <summary>
        /// по договорам об образовании за счёт средств физических и (или) юридических лиц
        /// </summary>
        [Display(Name = "по договорам об образовании за счёт средств физических и (или) юридических лиц ")]
        public int NumberPVacant { get; set; }

    }
}
