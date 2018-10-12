using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    public class UchPosobieVid
    {
        /// <summary>
        /// Справочник "Вид учебного пособия"
        /// </summary>
        /// 
        public int UchPosobieVidId { get; set; }

        /// <summary>
        /// Наименование вида учебного пособия 
        /// </summary>
        [Display(Name = "Вид учебного пособия ")]
        public string UchPosobieVidName { get; set; }

    }
}