using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.Sveden
{
    /// <summary>
    /// Место осуществления образовательной деятельности
    /// </summary>
    public class AddressPlace
    {
        public int AddressPlaceId { get; set; }
        [Display(Name = "Адрес")]
        public string AddressPlaceName { get; set; }

        /// <summary>
        /// Используется при сетевой форме реализации образовательных программ
        /// </summary>
        [Display(Name = "Используется при сетевой форме реализации образовательных программ")]
        public bool IsPlaceSet { get; set; }

        /// <summary>
        /// Используется при проведении практики
        /// </summary>
        [Display(Name = "Используется при проведении практики")]
        public bool IsPlacePrac { get; set; }

        /// <summary>
        /// Используется при практической подготовке
        /// </summary>
        [Display(Name = "Используется при практической подготовке")]
        public bool IsPlacePodg { get; set; }

        /// <summary>
        /// Используется при проведении государственной итоговой аттестации
        /// </summary>
        [Display(Name = "Используется при проведении государственной итоговой аттестации")]
        public bool IsPlaceGia { get; set; }

        /// <summary>
        /// Используется при осуществлении образовательной деятельности по дополнительным образовательным программам
        /// </summary>
        [Display(Name = "Используется при осуществлении образовательной деятельности по дополнительным образовательным программам")]
        public bool IsPlaceDop { get; set; }

        /// <summary>
        /// Используется при осуществлении образовательной деятельности по основным программам профессионального обучения
        /// </summary>
        [Display(Name = "Используется при осуществлении образовательной деятельности по основным программам профессионального обучения")]
        public bool IsPlaceOppo { get; set; }

    }
}
