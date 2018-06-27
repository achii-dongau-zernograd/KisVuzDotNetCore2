using KisVuzDotNetCore2.Models.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель "Учебный план"
    /// </summary>
    public class EduPlan
    {
        /// <summary>
        /// УИД учебного плана
        /// </summary>
        public int EduPlanId { get; set; }

        ///////////// Навигационные свойства //////////////
        /// <summary>
        /// Код профиля, которому принадлежит учебный план
        /// </summary>
        [Display(Name = "Код профиля, которому принадлежит учебный план")]
        public int EduProfileId { get; set; }

        /// <summary>
        /// Профиль, которому принадлежит учебный план
        /// </summary>
        [Display(Name = "Профиль, которому принадлежит учебный план")]
        public EduProfile EduProfile { get; set; }

        /// <summary>
        /// Год начала подготовки
        /// </summary>
        [Display(Name = "Год начала подготовки")]
        public List<EduYearBeginningTraining> EduYearBeginningTrainings { get; set; }

        /// <summary>
        /// Номер протокола Ученого Совета
        /// </summary>
        [Display(Name = "Номер протокола Ученого Совета")]
        public string ProtokolNumber { get; set; }

        /// <summary>
        /// Дата протокола Ученого Совета
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата протокола Ученого Совета")]
        public DateTime ProtokolDate { get; set; }

        /// <summary>
        /// Дата утверждения учебного плана
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Дата утверждения учебного плана")]
        public DateTime UtverjdDate { get; set; }

        /// <summary>
        /// Код выпускающей кафедры
        /// </summary>
        [Display(Name = "Код выпускающей кафедры")]
        public int StructKafId { get; set; }

        /// <summary>
        /// Выпускающая кафедры
        /// </summary>
        [Display(Name = "Выпускающая кафедры")]
        public StructKaf StructKaf { get; set; }

        /// <summary>
        /// Программа подготовки
        /// </summary>
        [Display(Name = "Программа подготовки")]
        public EduProgramPodg EduProgramPodg { get; set; }

        /// <summary>
        /// УИД формы обучения
        /// </summary>
        [Display(Name = "УИД формы обучения")]
        public int EduFormId { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name = "Форма обучения")]
        public EduForm EduForm { get; set; }

        /// <summary>
        /// Срок обучения
        /// </summary>
        [Display(Name = "Срок обучения")]
        public EduSrok EduSrok { get; set; }

        /// <summary>
        /// Виды деятельности 
        /// </summary>
        [Display(Name = "Виды деятельности по учебному плану")]
        public List<EduVidDeyat> EduVidDeyat { get; set; }

        /// <summary>
        /// Учебные годы
        /// </summary>
        [Display(Name = "Учебные годы")]
        public List<EduYear> EduYears { get; set; }

        /// <summary>
        /// Файл учебного плана (.pdf)
        /// </summary>
        [Display(Name = "Файл учебного плана")]
        public FileModel EduPlanPdf { get; set; }

        /// <summary>
        /// Блоки дисциплин
        /// </summary>
        [Display(Name = "Блоки дисциплин")]
        public List<BlokDiscipl> BlokDiscipl { get; set; }





    }
}