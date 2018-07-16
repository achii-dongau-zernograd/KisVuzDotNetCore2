using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.UchPosobiya
{
    public class UchPosobieFormaIzdaniya
    {
        /// <summary>
        /// Справочник "Форма издания учебного пособия"
        /// </summary>
        /// 
        public int UchPosobieFormaIzdaniyaId { get; set; }

        /// <summary>
        /// Ф.И.О. автора 
        /// </summary>
        [Display(Name = "Форма издания учебного пособия ")]
        public string UchPosobieFormaIzdaniyaName { get; set; }



    }
}