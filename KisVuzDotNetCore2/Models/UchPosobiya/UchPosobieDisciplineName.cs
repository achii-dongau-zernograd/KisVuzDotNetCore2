using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    public class UchPosobieDisciplineName
    {
        /// <summary>
        /// Связь между UchPosobie (учебное пособие) и  DisciplineName (наименованиея дисциплин)
        /// </summary>
        /// 
        public int UchPosobieDisciplineNameId { get; set; }

        /// <summary>
        /// УИД учебного пособия
        /// </summary>
        [Display(Name = "Учебное пособие")]
        public int UchPosobieId { get; set; }
        /// <summary>
        /// Учебное пособие
        /// </summary>
        [Display(Name = "Учебное пособие")]
        public UchPosobie UchPosobie { get; set; }

        /// <summary>
        /// УИД наименования дисциплины
        /// </summary>
        [Display(Name = "Наименование дисциплины")]
        public int DisciplineNameId { get; set; }
        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        [Display(Name = "Наименование дисциплины")]
        public DisciplineName DisciplineName { get; set; }
    }
}