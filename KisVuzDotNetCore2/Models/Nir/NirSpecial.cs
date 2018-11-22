using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KisVuzDotNetCore2.Models.Nir
{
    /// <summary>
    /// Специальность научных работников, согласно номенклатуре
    /// </summary>
    public class NirSpecial
    {
        public int NirSpecialId { get; set; }        

        /// <summary>
        /// Код специальности научных работников
        /// </summary>
        [Display(Name = "Код специальности научных работников")]
        public string NirSpecialCode { get; set; }

        /// <summary>
        /// Наименование специальности научных работников
        /// </summary>
        [Display(Name = "Наименование специальности научных работников")]
        public string NirSpecialName { get; set; }

        /// <summary>
        /// Набор объектов "Статья - Специальность научных работников, согласно номенклатуре"
        /// </summary>
        public List<ArticleNirSpecial> ArticleNirSpecials { get; set; }

        /// <summary>
        /// Набор объектов "Специальность научных работников, согласно номенклатуре - Профиль подготовки"
        /// </summary>
        public List<NirSpecialEduProfile> NirSpecialEduProfiles { get; set; }
    }
}