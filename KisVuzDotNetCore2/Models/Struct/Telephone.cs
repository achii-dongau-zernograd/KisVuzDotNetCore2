using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Телефон
    /// </summary>
    public class Telephone
    {
        /// <summary>
        /// УИД телефона
        /// </summary>
        public int TelephoneId { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name="Телефон")]
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        [Display(Name = "Описание")]
        public string TelephoneComment { get; set; }
    }
}