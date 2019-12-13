using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Education
{
    /// <summary>
    /// Модель записи, связывающей направление подготовки, форму обучения и нормативный срок обучения
    /// </summary>
    public class EduNapravlEduFormEduSrok
    {
        #region Открытые свойства
        public int EduNapravlEduFormEduSrokId { get; set; }

        /// <summary>
        /// Направление подготовки
        /// </summary>
        [Display(Name = "Направление подготовки")]
        public int EduNapravlId { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name = "Форма обучения")]
        public int EduFormId { get; set; }

        /// <summary>
        /// Нормативный срок обучения
        /// </summary>
        [Display(Name = "Нормативный срок обучения")]
        public int EduSrokId { get; set; }
        #endregion

        #region Навигационные свойства
        /// <summary>
        /// Направление подготовки
        /// </summary>
        [Display(Name = "Направление подготовки")]
        public EduNapravl EduNapravl { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name = "Форма обучения")]
        public EduForm EduForm { get; set; }

        /// <summary>
        /// Нормативный срок обучения
        /// </summary>
        [Display(Name = "Нормативный срок обучения")]
        public EduSrok EduSrok { get; set; }
        #endregion
    }
}