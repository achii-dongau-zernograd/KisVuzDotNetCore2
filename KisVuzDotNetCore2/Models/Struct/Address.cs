using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Struct
{
    /// <summary>
    /// Адрес
    /// </summary>
    public class Address
    {
        /// <summary>
        /// УИД адреса
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        [Display(Name = "Почтовый индекс")]
        public string PostCode { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        [Display(Name = "Страна")]
        public string Country { get; set; }

        /// <summary>
        /// Область, край, регион
        /// </summary>
        [Display(Name = "Область (край, регион)")]
        public string Region { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [Display(Name = "Населённый пункт (город, посёлок и пр.)")]
        public string Settlement { get; set; }

        /// <summary>
        /// Улица (переулок, бульвар)
        /// </summary>
        [Display(Name = "Улица (переулок, бульвар)")]
        public string Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        [Display(Name = "Номер дома")]
        public string HouseNumber { get; set; }
    }
}