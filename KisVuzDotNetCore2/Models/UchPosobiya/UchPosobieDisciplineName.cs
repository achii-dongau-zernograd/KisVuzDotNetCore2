using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    public class UchPosobieDisciplineName
    {
        /// <summary>
        /// Связующая таблица между UchPosobie (учебное пособие) и  DisciplineName (наименованиея дисциплин)
        /// </summary>
        /// 
        public int UchPosobieDisciplineNameId { get; set; }

        public int UchPosobieId { get; set; }
        [Display(Name = "Учебное пособие")]
        public UchPosobie UchPosobie { get; set; }

        public int DisciplineNameId { get; set; }
        [Display(Name = "Наименование дисциплины")]
        public DisciplineName DisciplineName { get; set; }
    }
}