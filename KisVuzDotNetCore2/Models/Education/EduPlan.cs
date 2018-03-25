using KisVuzDotNetCore2.Models.Struct;

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

        /// <summary>
        /// Год начала подготовки
        /// </summary>
        public EduYearBeginningTraining EduYearBeginningTraining { get; set; }

        /// <summary>
        /// Учебный год
        /// </summary>
        public EduYear EduYear { get; set; } 
        

        /// <summary>
        /// УИД формы обучения
        /// </summary>
        public int EduFormId { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public EduForm EduForm { get; set; }


        ///////////// Навигационные свойства //////////////
        /// <summary>
        /// Код профиля, которому принадлежит учебный план
        /// </summary>
        public int EduProfileId { get; set; }

        /// <summary>
        /// Профиль, которому принадлежит учебный план
        /// </summary>
        public EduProfile EduProfile { get; set; }


        /// <summary>
        /// Код выпускающей кафедры
        /// </summary>
        public int StructKafId { get; set; }

        /// <summary>
        /// Выпускающая кафедры
        /// </summary>
        public StructKaf StructKaf { get; set; }
    }
}