using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    public class EduPriem
    {
        public int EduPriemId { get; set; }
        [Display(Name = "Направление подготовки")]
        public int EduNapravlId { get; set; }
        [Display(Name = "Наименование специальности, направления подготовки")]
        public EduNapravl EduNapravl { get; set; }
        [Display(Name = "Форма обучения")]
        public int EduFormId { get; set; } 
        [Display(Name = "Формы обучения: 1) очная; 2) очно-заочная; 3) заочная")]
        public EduForm EduForm { get; set; }
        [Display(Name = "Результаты приема обучающихся за счет бюджетных ассигнований федерального бюджета")]
        public string  FinBF { get; set; }
        [Display(Name = "Результаты приема обучающихся за счет бюджетов субъектов Российской Федерации")]
        public string FinBR { get; set; }
        [Display(Name = "Результаты приема обучающихся за счет местных бюджетов")]
        public string FinBM { get; set; }
        [Display(Name = "Результаты приема обучающихся за счет средств физических и (или) юридических лиц")]
        public string FinPV { get; set; }
        [Display(Name = "Средняя сумма набранных баллов по всем вступительным испытаниям")]
        public double SredSum { get; set; }
    }
}
