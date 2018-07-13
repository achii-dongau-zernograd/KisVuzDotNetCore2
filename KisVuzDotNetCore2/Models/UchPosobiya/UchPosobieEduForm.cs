using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    /// <summary>
    ///  Связь между UchPosobie (учебное пособие) и  EduForm (форма обучения)
    /// </summary>
    public class UchPosobieEduForm
    {
        /// <summary>
        /// Связь между UchPosobie (учебное пособие) и  EduForm (форма обучения)
        /// </summary>
        public int  UchPosobieEduFormId { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public int EduFormId { get; set; }
        /// <summary>
        /// Форма обучения
        /// </summary>
        [Display(Name = "Форма обучения")]
        public EduForm EduForm { get; set; }

        /// <summary>
        /// Учебное пособие
        /// </summary>
        public int UchPosobieId { get; set; }
        /// <summary>
        /// Учебное пособие
        /// </summary>
        [Display(Name = "Учебное пособие")]
        public UchPosobie UchPosobieName { get; set; }
    }
}