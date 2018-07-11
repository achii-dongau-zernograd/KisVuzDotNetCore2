using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Образовательная программа"
    /// </summary>
    public class EduProgram
    {
        /// <summary>
        /// УИД образовательной программы
        /// </summary>
        public int EduProgramId { get; set; }

        /// <summary>
        /// Профиль / направленность образовательной программы
        /// </summary>
        [Display(Name = "Профиль / направленность образовательной программы")]
        public EduProfile EduProfile { get; set; }
        /// <summary>
        /// Профиль / направленность образовательной программы
        /// </summary>
        [Display(Name = "Профиль / направленность образовательной программы")]
        public int EduProfileId { get; set; }

        /// <summary>
        /// Программа подготовки
        /// </summary>
        [Display(Name ="Программа подготовки")]
        public EduProgramPodg EduProgramPodg { get; set; }
        /// <summary>
        /// Программа подготовки
        /// </summary>
        [Display(Name = "Программа подготовки")]
        public int EduProgramPodgId { get; set; }

        /// <summary>
        /// Признак адаптированной образовательной программы
        /// </summary>
        [Display(Name = "Признак адаптированной образовательной программы")]
        public bool IsAdopt { get; set; }

        /// <summary>
        /// Использование при реализации образовательных программ
        /// электронного обучения и дистанционных образовательных
        /// технологий (электронное обучение / дистанционные технологии / нет)
        /// </summary>
        [Display(Name = "Использование при реализации образовательных программ" +
            " электронного обучения и дистанционных образовательных технологий" +
            " (электронное обучение / дистанционные технологии / нет)")]
        public string UsingElAndDistEduTech { get; set; } = "нет";

        /// <summary>
        /// Сканированная копия образовательной программы
        /// </summary>
        [Display(Name = "Сканированная копия образовательной программы")]
        public FileModel FileModel { get; set; }
        /// <summary>
        /// Сканированная копия образовательной программы
        /// </summary>
        [Display(Name = "Сканированная копия образовательной программы")]
        public int FileModelId { get; set; }

        /// <summary>
        /// Дата утверждения
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name ="Дата утверждения")]
        public DateTime DateUtverjd { get; set; }

        /// <summary>
        /// Реализуемые формы обучения
        /// </summary>
        [Display(Name = "Реализуемые формы обучения")]
        public List<EduProgramEduForm> EduForms { get; set; }

        /// <summary>
        /// Учебные годы
        /// </summary>
        [Display(Name = "Учебные годы")]
        public List<EduProgramEduYear> EduProgramEduYears { get; set; }
    }
}
