using KisVuzDotNetCore2.Models.Education;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    public class UchPosobieEduNapravl
    {
        /// <summary>
        /// Связь между UchPosobie (учебными пособиями) и EduNapravl (направлениями)
        /// </summary>
        public int UchPosobieEduNapravlId { get; set; }

        [Display(Name = "Учебное пособие")]
        public int UchPosobieId { get; set; }
        [Display(Name = "Учебное пособие")]
        public UchPosobie UchPosobie { get; set; }

        [Display(Name = "Направление подготовки")]
        public int EduNapravlId { get; set; }
        [Display(Name = "Направление  подготовки")]
        public EduNapravl EduNapravl { get; set; }
    }
    
}