using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    public class UchPosobieEduNapravl
    {
        /// <summary>
        /// Связующая таблица между UchPosobie (учебными пособиями) и EduNapravl (направлениями)
        /// </summary>
        public int UchPosobieEduNapravlId { get; set; }

        public int UchPosobieId { get; set; }
        [Display(Name = "Учебное пособие")]
        public UchPosobie UchPosobie { get; set; }

        public int EduNapravlId { get; set; }
        [Display(Name = "Направление")]
        public EduNapravl EduNapravl { get; set; }
    }
    
}