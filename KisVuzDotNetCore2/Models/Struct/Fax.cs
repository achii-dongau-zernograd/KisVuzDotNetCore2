using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Факс
    /// </summary>
    public class Fax
    {
        /// <summary>
        /// УИД факса
        /// </summary>
        public int FaxId { get; set; }

        /// <summary>
        /// Факс
        /// </summary>
        [Display(Name = "Факс")]
        public string FaxValue { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        [Display(Name = "Описание")]
        public string FaxComment { get; set; }
    }
}