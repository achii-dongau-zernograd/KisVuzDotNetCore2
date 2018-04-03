using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Адрес электронной почты
    /// </summary>
    public class Email
    {
        /// <summary>
        /// УИД адреса электронной почты
        /// </summary>
        public int EmailId { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [Display(Name = "Адрес электронной почты")]
        public string EmailValue { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        [Display(Name = "Описание")]
        public string EmailComment { get; set; }
    }
}