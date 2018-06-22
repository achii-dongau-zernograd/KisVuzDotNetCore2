using KisVuzDotNetCore2.Models.Education;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Priem
{
    public class PriemExam
    {
        /// <summary>
        /// Модель шаблона представления информации по различныфм условиям
        /// </summary>
        public int PriemExamId { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Направление подготовки(специальности)")]
        public EduNapravl EduNapravl { get; set; }
        [Display(Name = "Направление подготовки(специальности)")]
        public int EduNapravlId { get; set; }

        /// <summary>
        /// Вступительные испытания (в порядке приоритета)
        /// </summary>
        [Display(Name = "Вступительные испытания (в порядке приоритета) ")]
        public string VstupIsp { get; set; }

        /// <summary>
        /// Минимальное количество баллов
        /// </summary>
        [Display(Name = "Минимальное количество баллов ")]
        public string MinKol { get; set; }

        /// <summary>
        /// "Форма проведения вступительных испытаний, проводимых организацией самостоятельно"
        /// </summary>
        [Display(Name = "Форма проведения вступительных испытаний, проводимых организацией самостоятельно")]
        public string FormProv { get; set; }
    }
}
