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
        /// Годы начала подготовки
        /// </summary>
        [Display(Name = "Годы начала подготовки")]
        public List<EduPlanEduYearBeginningTraining> EduPlanEduYearBeginningTrainings { get; set; }

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
        /// Выпускающая кафедра
        /// </summary>
        [Display(Name = "Выпускающая кафедра")]
        public StructKaf StructKaf { get; set; }

        /// <summary>
        /// Программа подготовки
        /// </summary>
        [Display(Name = "Программа подготовки / Квалификация")]
        public EduProgramPodg EduProgramPodg { get; set; }
        [Display(Name = "Программа подготовки / Квалификация")]
        public int EduProgramPodgId { get; set; }

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
        public int EduSrokId { get; set; }

        /// <summary>
        /// Виды деятельности 
        /// </summary>
        [Display(Name = "Виды деятельности / Типы задач профессиональной деятельности")]
        public List<EduPlanEduVidDeyat> EduVidDeyatList { get; set; }

       
        /// <summary>
        /// Файл учебного плана (.pdf)
        /// </summary>
        [Display(Name = "Файл учебного плана")]
        public FileModel EduPlanPdf { get; set; }
        public int? EduPlanPdfId { get; set; }

        /// <summary>
        /// Файл рабочей программы по воспитательной работе (.pdf)
        /// </summary>
        [Display(Name = "Файл рабочей программы по воспитательной работе")]
        public FileModel RabProgramVospitaniePdf { get; set; }
        public int? RabProgramVospitaniePdfId { get; set; }


        /// <summary>
        /// Блоки дисциплин
        /// </summary>
        [Display(Name = "Блоки дисциплин")]
        public List<BlokDiscipl> BlokDiscipl { get; set; }

        /// <summary>
        /// Переходная таблица EduPlanEduYear
        /// </summary>
        [Display(Name = "Учебные годы")]
        public List<EduPlanEduYear> EduPlanEduYears { get; set; }
        
        public string EduYearBeginningTrainingsDescription
        {
            get
            {
                string years = "";
                if (EduPlanEduYearBeginningTrainings == null) return years;

                foreach (var g in EduPlanEduYearBeginningTrainings)
                {
                    years += " - " + g?.EduYearBeginningTraining?.EduYearBeginningTrainingName;
                }
                return years;
            }
        }

        public string EduPlanDescription
        {
            get
            {
                return EduProfile?.GetEduProfileFullName +  " - " + EduForm?.EduFormName + EduYearBeginningTrainingsDescription;
            }
        }
    }
}