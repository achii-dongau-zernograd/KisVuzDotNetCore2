using KisVuzDotNetCore2.Models.Common;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [Required]
        [Display(Name = "Почтовый индекс")]
        public string PostCode { get; set; }

        /// <summary>
        /// Населённый пункт
        /// </summary>
        [ValidateNever]
        [Display(Name = "Населённый пункт")]        
        public int? PopulatedLocalityId { get; set; }
        public PopulatedLocality PopulatedLocality { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        [Required]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        /// <summary>
        /// Область, край, регион
        /// </summary>
        [Required]
        [Display(Name = "Область (край, регион)")]
        public string Region { get; set; }

        /// <summary>
        /// Населённый пункт (город, посёлок и пр.)
        /// </summary>
        [Required]
        [Display(Name = "Населённый пункт (город, посёлок и пр.)")]
        public string Settlement { get; set; }

        /// <summary>
        /// Улица (переулок, бульвар)
        /// </summary>
        [Required]
        [Display(Name = "Улица (переулок, бульвар)")]
        public string Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        [Required]
        [Display(Name = "Номер дома")]
        public string HouseNumber { get; set; }

        #region Навигационные свойства
        /// <summary>
        /// Паспортные данные
        /// </summary>
        public PassportData PassportData { get; set; }

        /// <summary>
        /// Пользователь, для которого данный адрес
        /// будет являться адресом фактического проживания
        /// </summary>
        public AppUser AppUser { get; set; }
        #endregion

        /// <summary>
        /// Строка адреса (только для чтения)
        /// </summary>
        [Display(Name = "Адрес")]
        public string GetAddress
        {
            get
            {
                return PostCode + ", "
                    + Region + ", "
                    + Settlement + ", "
                    + Street + ", "
                    + HouseNumber;
            }
        }
    }
}